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
        public EventHandler<ConnectedEventArgs> OnConnected;
        public EventHandler OnDisposed;

        private Socket _socket;
        private IPEndPoint _endPoint;

        private INetworkChatCodec _codec;
        private WaveIn _waveIn;
        private IWavePlayer _waveOut;
        private BufferedWaveProvider _waveProvider;

        #region Constuctor
        public Client(string ip, int port, int inputDeviceNumber, INetworkChatCodec codec)
        {
            _endPoint = new IPEndPoint(IPAddress.Parse(ip), port);

            _codec = codec;
            _waveIn = new WaveIn();
            _waveIn.BufferMilliseconds = 50;
            _waveIn.DeviceNumber = inputDeviceNumber;
            _waveIn.WaveFormat = _codec.RecordFormat;
            _waveIn.StartRecording();

            _waveOut = new WaveOut();
            _waveProvider = new BufferedWaveProvider(_codec.RecordFormat);
            _waveOut.Init(_waveProvider);
            _waveOut.Play();

            Buffer = new byte[1024 * 4];
        }
        #endregion

        #region Properties
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
            StartConnect(e);
        }

        public void Close()
        {
            if (_socket != null)
            {
                _socket.Close();
                _socket.Dispose();
                if (OnDisposed != null) OnDisposed(this, EventArgs.Empty);
            }
            if (_waveIn != null)
            {
                _waveIn.StopRecording();
                _waveIn.Dispose();
            }
            if (_waveOut != null)
            {
                _waveOut.Dispose();
            }

            _codec = null;
        }
        #endregion

        #region Private Method
        private void StartConnect(SocketAsyncEventArgs e)
        {
            if (_socket == null || e.SocketError != SocketError.Success) return;

            if (!_socket.ConnectAsync(e))
            {
                ConnectProcess(e);
            }
        }

        private void ConnectComplete(object sender, SocketAsyncEventArgs e)
        {
            ConnectProcess(e);
        }

        private void ConnectProcess(SocketAsyncEventArgs e)
        {
            try
            {
                if (OnConnected != null) OnConnected(this, new ConnectedEventArgs(e.SocketError));
                if (e.SocketError != SocketError.Success)
                {
                    Close();
                    return;
                }

                StartReceive();
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void StartReceive()
        {
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.SetBuffer(Buffer, BufferOffset, BufferSize);
            e.Completed += ReceiveComplete;
            if (!_socket.ReceiveAsync(e))
            {
                ReceiveProcess(e);
            }

            _waveIn.DataAvailable += SendPacket;
        }

        private void ReceiveComplete(object sender, SocketAsyncEventArgs e)
        {
            ReceiveProcess(e);
        }

        private void ReceiveProcess(SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success || e.BytesTransferred == 0 || !IsAlive)
            {
                Close();
                return;
            }

            int size = e.BytesTransferred;

            // 패킷 처리
            byte[] packet = new byte[size];
            System.Buffer.BlockCopy(Buffer, 0, packet, 0, size);
            System.Buffer.BlockCopy(Buffer, size, Buffer, 0, size);
            e.SetBuffer(0, BufferSize);

            byte[] decoded = _codec.Decode(packet, 0, packet.Length);
            _waveProvider.AddSamples(decoded, 0, decoded.Length);

            if (!_socket.ReceiveAsync(e))
            {
                ReceiveProcess(e);
            }
        }

        private void SendPacket(object sender, WaveInEventArgs e)
        {
            if (!IsAlive)
            {
                Close();
                return;
            }

            byte[] encoded = _codec.Encode(e.Buffer, 0, e.BytesRecorded);
            bool isSuccess = _socket.Send(encoded) > 0;
            if (!isSuccess) Close();
        }
        #endregion
    }
}
