using GTUtil;
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
    public class TcpServer
    {
        public EventHandler<DisconnectedEventArgs> ServerClosed;

        private TcpListener _server;
        private readonly IPEndPoint _endPoint;
        private readonly List<ServerUser> _users;
        private bool _isRunning;

        #region Constructor
        public TcpServer(int port)
        {
            _endPoint = new IPEndPoint(IPAddress.Any, port);
            _users = new List<ServerUser>();
        }
        #endregion

        #region Public Method
        public bool Start()
        {
            if (_server != null) return false;

            try
            {
                _server = new TcpListener(_endPoint);
                _server.Start();
            }
            catch (Exception e)
            {
                Logger.Error(e);
                MessageBoxUtil.Error(string.Format("Failed to create server.\r\n\r\n{0}", e.Message));
                Stop();
                return false;
            }

            var mainThread = new Thread(MainThread);
            mainThread.Start();
            return true;
        }

        public void Stop()
        {
            _isRunning = false;

            foreach (var user in _users)
            {
                user.Dispose();
            }
            _users.Clear();

            if (_server != null)
            {
                _server.Stop();
                _server = null;
            }
            ServerClosed?.Invoke(this, new DisconnectedEventArgs("Server"));
        }
        #endregion

        #region Thread
        private void MainThread()
        {
            _isRunning = true;
            while (_isRunning)
            {
                if (!_server.Pending())
                {
                    Thread.Sleep(100);
                    continue;
                }

                var joinThread = new Thread(JoinThread);
                var client = _server.AcceptTcpClient();
                client.ReceiveTimeout = 1000 * 5;
                client.SendTimeout = 1000 * 5;
                joinThread.Start(client.Client);
            }
        }

        private void JoinThread(object param)
        {
            var socket = param as Socket;
            if (socket == null) return;

            var data = new byte[1024];
            int size = socket.Receive(data);
            string name = Encoding.UTF8.GetString(data, 0, size);
            if (_users.Any(o => o.Name == name))
            {
                socket.Send(Packet.Pack(new Packet() { Type = PacketType.JoinFail }));
                DisconnectClient(socket);
                return;
            }

            var user = new ServerUser(socket, name);
            _users.Add(user);

            // 접속 성공 알림
            var onlineUserNames = _users.Where(o => o.Name != user.Name).Select(o => o.Name).ToArray();
            user.Send(new Packet() { Type = PacketType.JoinSuccess, OnlineUserNames = onlineUserNames });

            // 접속된 Client들에게 새로운 Client의 접속 알림
            SendToUsers(socket, new Packet() { Type = PacketType.Connected, SendUserName = user.Name });

            var state = new StateObject(socket);
            socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, OnReceive, state);
        }
        #endregion

        #region Private Method
        private void OnReceive(IAsyncResult ar)
        {
            var state = ar.AsyncState as StateObject;
            if (state == null) return;

            var socket = state.WorkSocket;
            if (!socket.Connected)
            {
                DisconnectClient(socket);
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

                    if (!PacketHandler(data, socket))
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
                DisconnectClient(socket);
            }
        }

        private bool PacketHandler(byte[] data, Socket socket)
        {
            try
            {
                var packet = Packet.UnPack(data);
                if (packet == null) return false;

                switch (packet.Type)
                {
                    case PacketType.Exit: // 서버퇴장
                        DisconnectClient(socket);
                        break;
                    case PacketType.Text: // 텍스트
                        {
                            SendToUsers(socket, new Packet() { Type = PacketType.Text, SendUserName = GetName(socket), SendText = packet.SendText });
                        }
                        break;
                    case PacketType.Audio: // 음성정보
                        {
                            SendToUsers(socket, new Packet() { Type = PacketType.Audio, SendUserName = GetName(socket), AudioData = packet.AudioData });
                        }
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

        private void DisconnectClient(Socket socket)
        {
            var user = _users.FirstOrDefault(o => o.Socket == socket);
            if (user != null)
            {
                _users.Remove(user);
                SendToUsers(user.Name, new Packet() { Type = PacketType.Disconnected, SendUserName = user.Name });
            }
            if (socket != null) socket.Close();
        }

        private void SendToUsers(Socket socket, Packet packet)
        {
            var users = _users.Where(o => o.Socket != socket).ToList();
            SendToUsers(users, packet);
        }

        private void SendToUsers(string name, Packet packet)
        {
            var users = _users.Where(o => o.Name != name).ToList();
            SendToUsers(users, packet);
        }

        private void SendToUsers(List<ServerUser> targetUsers, Packet packet)
        {
            foreach (var user in targetUsers)
            {
                user.Send(packet);
            }
        }

        private string GetName(Socket socket)
        {
            var user = _users.FirstOrDefault(o => o.Socket == socket);
            return (user == null) ? null : user.Name;
        }
        #endregion
    }
}