using GTUtil;
using NAudio.Wave;
using NAudio.Wave.SampleProviders;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class TcpClient
    {
        public EventHandler<ConnectedEventArgs> Connected;
        public EventHandler<DisconnectedEventArgs> Disconnected;
        public EventHandler<ConnectedEventArgs> OtherClientConnected;
        public EventHandler<DisconnectedEventArgs> OtherClientDisconnected;
        public EventHandler<MessageEventArgs> ReceiveMessage;

        private System.Net.Sockets.TcpClient _client;
        private readonly IPEndPoint _endPoint;
        private readonly string _name;

        private readonly int _inputDeviceNumber;
        private INetworkChatCodec _codec;
        private WaveIn _waveIn;
        private MixingSampleProvider _mixer;
        private IWavePlayer _waveOut;
        private Dictionary<string, MyProvider> _providers;

        #region Constructor
        public TcpClient(string ip, int port, string name, int inputDeviceNumber)
        {
            _endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _name = name;
            _inputDeviceNumber = inputDeviceNumber;

            _codec = new UncompressedPcmChatCodec();
            //_codec = new UltraWideBandSpeexCodec();
            //_codec = new WideBandSpeexCodec();
            //_codec = new NarrowBandSpeexCodec();

            _mixer = new MixingSampleProvider(MyProvider.CreateFormat(_codec));
            _waveOut = new WaveOut();
            _waveOut.Init(_mixer);

            _providers = new Dictionary<string, MyProvider>();
        }
        #endregion

        #region Public Method
        public bool Start()
        {
            try
            {
                _client = new System.Net.Sockets.TcpClient();
                _client.ReceiveTimeout = 1000 * 5;
                _client.SendTimeout = 1000 * 5;
                _client.Connect(_endPoint);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                MessageBoxUtil.Error(string.Format("Failed to connect to server.\r\n\r\n{0}", e.Message));
                Stop();
                return false;
            }

            try
            {
                // Name으로 접속 결과 요청
                var bytes = Encoding.UTF8.GetBytes(_name);
                _client.Client.Send(bytes);

                // 접속 결과 수신 : JoinFail / JoinSuccess
                var data = new byte[StateObject.BufferSize];
                int size = _client.Client.Receive(data);
                var buffer = new byte[size];
                Buffer.BlockCopy(data, 0, buffer, 0, size);
                var packet = Packet.ToPacket(buffer);
                if (packet == null || packet.Type == PacketType.JoinFail)
                {
                    MessageBoxUtil.Error(string.Format("Your name '{0}' is already exist on server.", _name));
                    Stop();
                    return false;
                }

                // 접속 성공 이벤트 : Client 화면에 표기
                string[] onlineUserNames = packet.OnlineUserNames;
                Connected?.Invoke(this, new ConnectedEventArgs(_name, onlineUserNames));
                foreach (var name in onlineUserNames)
                {
                    AddProvier(name);
                }

                StartRecording();

                var receiveThread = new Thread(ReceiveThread);
                receiveThread.Start();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Stop(e);
            }
            return false;
        }

        public void Stop(Exception e = null)
        {
            if (_client != null)
            {
                if (_client.Client != null)
                {
                    SendPacket(new Packet() { Type = PacketType.Exit });
                    if (_client.Client.Connected) _client.Client.Disconnect(true);
                }
                _client.Close();
                _client = null;
            }
            if (_waveIn != null)
            {
                _waveIn.DataAvailable -= OnAudioCaptured;
                _waveIn.StopRecording();
                //_waveIn.Dispose();
                _waveIn = null;
            }
            if (_waveOut != null)
            {
                _waveOut.Stop();
                //_waveOut.Dispose();
                _waveOut = null;
            }
            if (_codec != null)
            {
                _codec.Dispose();
                _codec = null;
            }
            Disconnected?.Invoke(this, new DisconnectedEventArgs(e));
        }

        public void SendPacket(Packet packet)
        {
            if (packet == null) return;
            if (_client == null || !_client.Connected) return;
            if (_client.Client == null || !_client.Client.Connected) return;

            _client.Client.Send(Packet.ToData(packet));
        }

        public void ChangeVolume(string name, float volume)
        {
            if (!_providers.ContainsKey(name)) return;

            var provider = _providers[name];
            provider.ChangeVolume(volume);
        }

        public void ChangeGeneralVolume(float generalVolume)
        {
            foreach (var provider in _providers.Values)
            {
                provider.ChangeGeneralVolume(generalVolume);
            }
        }

        public void MuteInputDevice(bool isMute)
        {
            if (isMute)
            {
                _waveIn.DataAvailable -= OnAudioCaptured;
            }
            else
            {
                _waveIn.DataAvailable += OnAudioCaptured;
            }
        }
        #endregion

        #region Thread
        private void ReceiveThread()
        {
            var socket = _client.Client;
            var state = new StateObject(socket);
            socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, OnReceive, state);
        }
        #endregion

        #region Private Method
        private void OnReceive(IAsyncResult ar)
        {
            var state = ar.AsyncState as StateObject;
            if (state == null) return;

            var socket = state.Socket;
            if (!socket.Connected)
            {
                Stop();
                return;
            }

            try
            {
                var receiveSize = socket.EndReceive(ar);
                if (receiveSize <= 0) return;

                // Receive Data를 Buffer에 저장
                Buffer.BlockCopy(state.Buffer, 0, state.ProcessingBuffer, state.ProcessingOffset, receiveSize);
                state.ProcessingOffset += receiveSize;

                // Buffer에 저장된 길이값으로 패킷 취득
                int size = Packet.GetPacketSize(state.ProcessingBuffer) + 2;
                if (size != 0 && size <= state.ProcessingOffset)
                {
                    byte[] data = new byte[size];
                    Buffer.BlockCopy(state.ProcessingBuffer, 0, data, 0, size);
                    Buffer.BlockCopy(state.ProcessingBuffer, size, state.ProcessingBuffer, 0, state.ProcessingOffset - size);
                    state.ProcessingOffset -= size;

                    if (!PacketHandler(data))
                    {
                        Stop();
                        return;
                    }
                }

                socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, OnReceive, state);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Stop(e);
            }
        }

        private bool PacketHandler(byte[] data)
        {
            try
            {
                var packet = Packet.ToPacket(data);
                if (packet == null) return false;

                switch (packet.Type)
                {
                    case PacketType.Connected: // 다른 유저의 접속 : 화면에 생성
                        OtherClientConnected?.Invoke(this, new ConnectedEventArgs(packet.SendUserName));
                        AddProvier(packet.SendUserName);
                        break;
                    case PacketType.Disconnected: // 다른 유저의 종료 : 화면에서 삭제
                        OtherClientDisconnected?.Invoke(this, new DisconnectedEventArgs(packet.SendUserName));
                        RemoveProvider(packet.SendUserName);
                        break;
                    case PacketType.Text: // 텍스트
                        ReceiveMessage?.Invoke(this, new MessageEventArgs(packet.SendUserName, packet.SendText));
                        break;
                    case PacketType.Audio: // 음성정보
                        StartPlay(packet);
                        break;
                    case PacketType.File: // 파일전송
                        // TODO : 파일 전송 및 저장
                        break;
                }
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return false;
        }

        private void OnAudioCaptured(object sender, WaveInEventArgs e)
        {
            byte[] encoded = _codec.Encode(e.Buffer, 0, e.BytesRecorded);
            SendPacket(new Packet() { Type = PacketType.Audio, SendUserName = _name, AudioData = encoded });
        }

        private void StartRecording()
        {
            _waveIn = new WaveIn();
            _waveIn.BufferMilliseconds = 50;
            _waveIn.DeviceNumber = _inputDeviceNumber;
            _waveIn.WaveFormat = _codec.RecordFormat;
            _waveIn.DataAvailable += OnAudioCaptured;
            _waveIn.StartRecording();
        }

        private void StartPlay(Packet packet)
        {
            if (!_providers.ContainsKey(packet.SendUserName)) return;

            byte[] decoded = _codec.Decode(packet.AudioData, 0, packet.AudioData.Length);
            _providers[packet.SendUserName].AddSamples(decoded);
            _waveOut.Play();
        }

        private void AddProvier(string name)
        {
            if (_providers.ContainsKey(name)) return;

            var provider = new MyProvider(_codec.RecordFormat);
            _mixer.AddMixerInput(provider.Sample);
            _providers.Add(name, provider);
        }

        private void RemoveProvider(string name)
        {
            if (!_providers.ContainsKey(name)) return;

            var provider = _providers[name];
            _mixer.RemoveMixerInput(provider.Sample);
            _providers.Remove(name);
        }
        #endregion
    }
}
