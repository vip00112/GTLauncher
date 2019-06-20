using GTUtil;
using NAudio.Wave;
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

        private System.Net.Sockets.TcpClient _client;
        private readonly IPEndPoint _endPoint;
        private readonly string _name;
        private bool _isConnected;
        private bool _isProcessing;

        private readonly int _inputDeviceNumber;
        private INetworkChatCodec _codec;
        private WaveIn _waveIn;
        private IWavePlayer _waveOut;
        private BufferedWaveProvider _waveProvider;

        #region Constructor
        public TcpClient(string ip, int port, string name, int inputDeviceNumber)
        {
            _endPoint = new IPEndPoint(IPAddress.Parse(ip), port);
            _name = name;
            _inputDeviceNumber = inputDeviceNumber;
        }
        #endregion

        #region Public Method
        public bool Start()
        {
            try
            {
                _client = new System.Net.Sockets.TcpClient();
                _client.Connect(_endPoint);
            }
            catch (Exception e)
            {
                MessageBoxUtil.Error("Connect failed.");
                Logger.Error(e);
                Stop(e);
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

                AudioSetting();

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
            _isConnected = false;

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
        #endregion

        #region Thread
        private void ReceiveThread()
        {
            _isConnected = true;

            var socket = _client.Client;
            var state = new StateObject(socket);
            socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, OnReceive, state);

            //while (_isConnected)
            //{
            //    if (_isProcessing) continue;

            //    _isProcessing = true;
            //    socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, OnReceive, state);
            //}
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
                var size = socket.EndReceive(ar);
                if (size <= 0) return;

                if (!PacketHandler(state))
                {
                    Stop();
                    return;
                }
                socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, OnReceive, state);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                Stop(e);
            }
        }

        private bool PacketHandler(StateObject state)
        {
            try
            {
                var packet = Packet.ToPacket(state.Buffer);
                if (packet == null) return false;

                switch (packet.Type)
                {
                    case PacketType.Connected: // 다른 유저의 접속 : 화면에 생성
                        OtherClientConnected?.Invoke(this, new ConnectedEventArgs(packet.SendUserName));
                        break;
                    case PacketType.Disconnected: // 다른 유저의 종료 : 화면에서 삭제
                        OtherClientDisconnected?.Invoke(this, new DisconnectedEventArgs(packet.SendUserName));
                        break;
                    case PacketType.Audio: // 음성정보
                        byte[] decoded = _codec.Decode(packet.AudioData, 0, packet.AudioData.Length);
                        _waveProvider.AddSamples(decoded, 0, decoded.Length);
                        _waveOut.Play();
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

        private void SendPacket(Packet packet)
        {
            if (packet == null) return;
            if (_client == null || !_client.Connected) return;
            if (_client.Client == null || !_client.Client.Connected) return;

            _client.Client.Send(Packet.ToData(packet));
        }

        private void OnAudioCaptured(object sender, WaveInEventArgs e)
        {
            byte[] encoded = _codec.Encode(e.Buffer, 0, e.BytesRecorded);
            SendPacket(new Packet() { Type = PacketType.Audio, SendUserName = _name, AudioData = encoded });
        }

        private void AudioSetting()
        {
            _codec = new UncompressedPcmChatCodec();
            //_codec = new UltraWideBandSpeexCodec();
            //_codec = new WideBandSpeexCodec();
            //_codec = new NarrowBandSpeexCodec();

            _waveIn = new WaveIn();
            _waveIn.BufferMilliseconds = 50;
            _waveIn.DeviceNumber = _inputDeviceNumber;
            _waveIn.WaveFormat = _codec.RecordFormat;
            _waveIn.DataAvailable += OnAudioCaptured;
            _waveIn.StartRecording();

            _waveOut = new WaveOut();
            _waveProvider = new BufferedWaveProvider(_codec.RecordFormat);
            _waveOut.Init(_waveProvider);
        }
        #endregion
    }
}
