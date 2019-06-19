using GTUtil;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class Client
    {
        public EventHandler<ConnectedEventArgs> Connected;
        public EventHandler<DisconnectedEventArgs> Disconnected;
        public EventHandler<ConnectedEventArgs> OtherClientConnected;
        public EventHandler<DisconnectedEventArgs> OtherClientDisconnected;

        private object _lock;
        private Socket _socket;
        private IPEndPoint _endPoint;
        private INetworkChatCodec _codec;
        private WaveIn _waveIn;
        private IWavePlayer _waveOut;
        private BufferedWaveProvider _waveProvider;
        private Dictionary<string, AudioOutput> _outputs;

        #region Constuctor
        public Client(string ip, int port, int inputDeviceNumber)
        {
            _lock = new object();
            _endPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            _codec = new UncompressedPcmChatCodec();
            //_codec = new UltraWideBandSpeexCodec();
            //_codec = new WideBandSpeexCodec();
            //_codec = new NarrowBandSpeexCodec();

            _waveIn = new WaveIn();
            _waveIn.BufferMilliseconds = 50;
            _waveIn.DeviceNumber = inputDeviceNumber;
            _waveIn.WaveFormat = _codec.RecordFormat;
            _waveIn.DataAvailable += OnAudioCaptured;
            _waveIn.StartRecording();

            _waveOut = new WaveOut();
            _waveProvider = new BufferedWaveProvider(_codec.RecordFormat);
            _waveOut.Init(_waveProvider);
            _waveOut.Play();
            //_outputs = new Dictionary<string, AudioOutput>();

            Buffer = new byte[1024 * 4];
        }
        #endregion

        #region Properties
        public string ID { get; private set; }

        public string Host { get; private set; }

        public bool IsAlive { get { return _socket != null && _socket.Connected; } }

        public byte[] Buffer { get; }

        public int BufferOffset { get; set; }

        public int BufferSize { get { return Buffer.Length - BufferOffset; } }
        #endregion

        #region Public Method
        public void Start()
        {
            if (_socket != null) return;

            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);

            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.RemoteEndPoint = _endPoint;
            e.Completed += ConnectComplete;
            try
            {
                if (!_socket.ConnectAsync(e))
                {
                    ConnectProcess(e);
                }
            }
            catch (Exception ex)
            {
                Close(ex);
            }
        }

        public void Close(Exception e = null)
        {
            lock (_lock)
            {
                if (_socket != null)
                {
                    _socket.Close();
                    _socket.Dispose();
                }
                if (_waveIn != null)
                {
                    _waveIn.StopRecording();
                    //_waveIn.Dispose();
                }
                if (_waveOut != null)
                {
                    _waveOut.Stop();
                    //_waveOut.Dispose();
                }
                if (_codec != null)
                {
                    _codec.Dispose();
                }
                if (Disconnected != null) Disconnected(this, new DisconnectedEventArgs(e));
            }
        }
        #endregion

        #region Private Method
        private void ConnectComplete(object sender, SocketAsyncEventArgs e)
        {
            ConnectProcess(e);
        }

        private void ConnectProcess(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError != SocketError.Success)
                {
                    Close();
                    return;
                }

                StartReceive();
            }
            catch (Exception ex)
            {
                Close(ex);
            }
        }

        private void StartReceive()
        {
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.SetBuffer(Buffer, 0, Buffer.Length);
            e.Completed += ReceiveComplete;
            try
            {
                if (!_socket.ReceiveAsync(e))
                {
                    ReceiveProcess(e);
                }
            }
            catch (Exception ex)
            {
                Close(ex);
            }
        }

        private void ReceiveComplete(object sender, SocketAsyncEventArgs e)
        {
            ReceiveProcess(e);
        }

        private void ReceiveProcess(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError != SocketError.Success || e.BytesTransferred == 0 || !IsAlive)
                {
                    Close();
                    return;
                }

                BufferOffset += e.BytesTransferred;
                int size = Packet.GetPacketSize(Buffer) + 2;
                if (size != 0 && size <= BufferOffset)
                {
                    byte[] data = new byte[size];
                    System.Buffer.BlockCopy(Buffer, 0, data, 0, size);
                    System.Buffer.BlockCopy(Buffer, size, Buffer, 0, BufferOffset - size);
                    BufferOffset -= size;
                    e.SetBuffer(BufferOffset, BufferSize);

                    PacketHandler(data);
                }

                if (!_socket.ReceiveAsync(e))
                {
                    ReceiveProcess(e);
                }
            }
            catch (Exception ex)
            {
                Close(ex);
            }
        }

        private void OnAudioCaptured(object sender, WaveInEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(ID)) return;

            byte[] encoded = _codec.Encode(e.Buffer, 0, e.BytesRecorded);
            StartSend(new Packet() { Type = PacketType.Audio, ID = ID, AudioData = encoded });
        }

        private void StartSend(Packet packet)
        {
            if (packet == null) return;

            byte[] data = Packet.ToData(packet);
            if (data == null) return;

            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.SetBuffer(data, 0, data.Length);
            e.Completed += SendComplete;
            try
            {
                if (!_socket.SendAsync(e))
                {
                    SendProcess(e);
                }
            }
            catch (Exception ex)
            {
                Close(ex);
            }
        }

        private void SendComplete(object sender, SocketAsyncEventArgs e)
        {
            SendProcess(e);
        }

        private void SendProcess(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError != SocketError.Success || e.BytesTransferred == 0 || !IsAlive)
                {
                    Close();
                    return;
                }
            }
            catch (Exception ex)
            {
                Close(ex);
            }
        }

        private void PacketHandler(byte[] data)
        {
            var packet = Packet.ToPacket(data);
            if (packet == null) return;

            switch (packet.Type)
            {
                case PacketType.LoginResult: // 서버 입장 결과
                    ID = packet.ID;
                    Host = packet.Host;
                    if (Connected != null) Connected(this, new ConnectedEventArgs(ID, Host));
                    break;
                case PacketType.Connected: // 다른 유저의 접속
                    //if (_outputs.ContainsKey(packet.ID)) return;
                    //_outputs.Add(packet.ID, new AudioOutput(_codec.Clone()));

                    // Host정보 화면에 생성
                    if (OtherClientConnected != null) OtherClientConnected(this, new ConnectedEventArgs(packet.ID, packet.Host));
                    break;
                case PacketType.Disconnected: // 다른 유저의 종료
                    //if (!_outputs.ContainsKey(packet.ID)) return;
                    //_outputs[packet.ID].Dispose();
                    //_outputs.Remove(packet.ID);

                    // Host정보 화면에서 삭제
                    if (OtherClientDisconnected != null) OtherClientDisconnected(this, new DisconnectedEventArgs(packet.ID, packet.Host));
                    break;
                case PacketType.Audio: // 음성정보
                    //if (!_outputs.ContainsKey(packet.ID)) return;
                    //_outputs[packet.ID].Play(packet.AudioData);
                    byte[] decoded = _codec.Decode(packet.AudioData, 0, packet.AudioData.Length);
                    _waveProvider.AddSamples(decoded, 0, decoded.Length);
                    _waveOut.Play();
                    break;
            }
        }
        #endregion
    }
}
