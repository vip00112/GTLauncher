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
        private Socket _socket;
        private IPEndPoint _endPoint;
        private List<User> _users;

        #region Constuctor
        public Server(int port, int inputDeviceNumber)
        {
            _endPoint = new IPEndPoint(IPAddress.Any, port);
            _users = new List<User>();
        }
        #endregion

        #region Public Method
        public void Start()
        {
            if (_socket != null) return;

            _acceptStarted = true;
            _socket = new Socket(AddressFamily.InterNetwork, SocketType.Stream, ProtocolType.Tcp);
            _socket.Bind(_endPoint);
            _socket.Listen(20);

            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.Completed += AcceptComplete;
            StartAccept(e);
        }

        public void Close()
        {
            if (_socket != null)
            {
                _socket.Close();
                _socket.Dispose();
            }

            _socket = null;
            _endPoint = null;
            _acceptStarted = false;
        }
        #endregion

        #region Private Method
        private void StartAccept(SocketAsyncEventArgs e)
        {
            if (_socket == null || e.SocketError != SocketError.Success) return;

            e.AcceptSocket = null;
            if (!_socket.AcceptAsync(e))
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

                var user = new User(clientSocket);
                if (AddUser(user))
                {
                    var onlineUsers = _users.Where(o => o.ID != user.ID).ToList();
                    foreach (var onlineUser in onlineUsers)
                    {
                        user.StartSend(new Packet() { Type = PacketType.Connected, ID = onlineUser.ID, Host = onlineUser.Host });
                    }
                    user.StartSend(new Packet() { Type = PacketType.LoginResult, ID = user.ID, Host = user.Host });
                    SendPacketToUsers(user, new Packet() { Type = PacketType.Connected, ID = user.ID, Host = user.Host });
                    user.StartReceive();
                }
                else
                {
                    user.Dispose();
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            finally
            {
                StartAccept(e);
            }
        }

        private bool AddUser(User user)
        {
            if (!_users.Any(o => o.ID == user.ID))
            {
                user.DataReceived += UserDataReceived;
                user.Disposed += UserDisposed;
                _users.Add(user);
                return true;
            }
            return false;
        }

        private bool RemoveUser(User user)
        {
            if (_users.Any(o => o.ID == user.ID))
            {
                user.DataReceived -= UserDataReceived;
                user.Disposed -= UserDisposed;
                _users.Remove(user);
                return true;
            }
            return false;
        }

        private void UserDataReceived(object sender, DataReceiveEventArgs e)
        {
            var sendUser = sender as User;
            if (sendUser == null) return;

            SendPacketToUsers(sendUser, new Packet() { Type = PacketType.Audiom, ID = sendUser.ID, AudioData = e.Data });
        }

        private void UserDisposed(object sender, DisposedEventArgs e)
        {
            var disposedUser = sender as User;
            if (disposedUser == null) return;

            RemoveUser(disposedUser);
            SendPacketToUsers(disposedUser, new Packet() { Type = PacketType.Disconnected, ID = disposedUser.ID });
        }

        private void SendPacketToUsers(User sendUser, Packet packet)
        {
            // TODO : 테스트 코드
            var users = _users/*.Where(o => o.ID != sendUser.ID)*/.ToList();
            foreach (var user in users)
            {
                user.StartSend(packet);
            }
        }
        #endregion
    }
}
