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
    public class Server
    {
        private bool _acceptStarted;
        private Socket _listener;
        private IPEndPoint _endPoint;
        private UserManager _manager;

        private INetworkChatCodec _codec;
        private WaveIn _waveIn;
        private IWavePlayer _waveOut;
        private BufferedWaveProvider _waveProvider;

        #region Constuctor
        public Server(int port, int inputDeviceNumber, INetworkChatCodec codec)
        {
            _endPoint = new IPEndPoint(IPAddress.Any, port);
            _manager = new UserManager();

            _codec = codec;
            _waveIn = new WaveIn();
            _waveIn.BufferMilliseconds = 50;
            _waveIn.DeviceNumber = inputDeviceNumber;
            _waveIn.WaveFormat = _codec.RecordFormat;
            _waveIn.StartRecording();

            _waveOut = new WaveOut();
            _waveProvider = new BufferedWaveProvider(codec.RecordFormat);
            _waveOut.Init(_waveProvider);
            _waveOut.Play();
        }
        #endregion

        #region Public Method
        public void Start()
        {
            if (_listener != null) return;

            _acceptStarted = true;
            _listener = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _listener.Bind(_endPoint);
            _listener.Listen(20);

            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.Completed += AcceptComplete;
            StartAccept(e);
        }

        public void Close()
        {
            _manager.Dispose();

            if (_listener != null)
            {
                _listener.Close();
                _listener.Dispose();
            }

            if (_waveIn != null)
            {
                _waveIn.StopRecording();
                _waveIn.Dispose();
            }

            _endPoint = null;
            _codec = null;
            _acceptStarted = false;
        }
        #endregion

        #region Private Method
        private void StartAccept(SocketAsyncEventArgs e)
        {
            if (_listener == null || e.SocketError != SocketError.Success) return;

            e.AcceptSocket = null;
            if (!_listener.AcceptAsync(e))
            {
                AcceptProcess(e);
            }
        }

        private void AcceptComplete(object sender, SocketAsyncEventArgs e)
        {
            AcceptProcess(e);
        }

        private void AcceptProcess(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError != SocketError.Success) return;

                Socket clientSocket = e.AcceptSocket;
                if (!_acceptStarted)
                {
                    clientSocket.Disconnect(false);
                    clientSocket.Dispose();
                    return;
                }

                var user = new User(clientSocket, _codec);
                if (_manager.AddUser(user))
                {
                    user.OnDecoded += UserDecoded;
                    user.OnDisposed += UserDisposed;
                    _waveIn.DataAvailable += user.SendPacket;
                    user.ReceivePacket();
                }
                else
                {
                    user.Dispose();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                Logger.Error(ex);
            }
            finally
            {
                StartAccept(e);
            }
        }

        private void UserDecoded(object sender, DecodedEventArgs e)
        {
            var user = sender as User;
            if (user == null) return;

            // 서버측 output
            _waveProvider.AddSamples(e.Decodecd, 0, e.Decodecd.Length);

            // 접속된 사용자에게 output
            var onlineUsers = _manager.Users.Where(o => o.ID != user.ID).ToList();
            foreach (var online in onlineUsers)
            {
                online.SendPacket(this, new WaveInEventArgs(e.Decodecd, e.Decodecd.Length));
            }
        }

        private void UserDisposed(object sender, EventArgs e)
        {
            var user = sender as User;
            if (user == null) return;

            _waveIn.DataAvailable -= user.SendPacket;
            user.OnDecoded -= UserDecoded;
            user.OnDisposed -= UserDisposed;
            _manager.RemoveUser(user);
        }
        #endregion
    }
}
