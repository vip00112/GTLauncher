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
        public void Start()
        {
            if (_server != null) return;

            var mainThread = new Thread(MainThread);
            mainThread.Start();
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
        }
        #endregion

        #region Thread
        private void MainThread()
        {
            _server = new TcpListener(_endPoint);
            _server.Start();

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
                socket.Send(Packet.ToData(new Packet() { Type = PacketType.JoinFail }));
                DisconnectClient(socket);
                return;
            }

            var user = new ServerUser(socket, name);
            _users.Add(user);

            // 접속 성공 알림
            var onlineUserNames = _users.Where(o => o.Name != user.Name).Select(o => o.Name).ToArray();
            user.Send(new Packet() { Type = PacketType.JoinSuccess, OnlineUserNames = onlineUserNames });

            // 접속된 Client들에게 새로운 Client의 접속 알림
            SendToUsers(user, new Packet() { Type = PacketType.Connected, SendUserName = user.Name });

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
                DisconnectClient(socket);
                return;
            }

            try
            {
                var size = socket.EndReceive(ar);
                if (size <= 0) return;

                if (!PacketHandler(state))
                {
                    DisconnectClient(socket);
                    return;
                }
                socket.BeginReceive(state.Buffer, 0, StateObject.BufferSize, 0, OnReceive, state);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                DisconnectClient(socket);
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
                    case PacketType.Exit: // 서버퇴장
                        DisconnectClient(state.Socket);
                        break;
                    case PacketType.Audio: // 음성정보
                        // Client로부터 받은 음성 정보를 모든 유저에ㅔ 전송
                        var sendUser = _users.FirstOrDefault(o => o.Name == packet.SendUserName);
                        SendToUsers(sendUser, new Packet() { Type = PacketType.Audio, SendUserName = sendUser.Name, AudioData = packet.AudioData });
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
                SendToUsers(user, new Packet() { Type = PacketType.Disconnected, SendUserName = user.Name });
            }
            if (socket != null) socket.Close();
        }

        private void SendToUsers(ServerUser sendUser, Packet packet)
        {
            var users = _users.Where(o => o.Name != sendUser.Name).ToList();
            foreach (var user in users)
            {
                user.Send(packet);
            }
        }
        #endregion
    }
}