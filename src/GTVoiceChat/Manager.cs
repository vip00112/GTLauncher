using GTUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class Manager
    {
        // Client EventHandler
        public EventHandler<ConnectedEventArgs> Connected;
        public EventHandler<DisconnectedEventArgs> Disconnected;
        public EventHandler<ConnectedEventArgs> OtherClientConnected;
        public EventHandler<DisconnectedEventArgs> OtherClientDisconnected;
        public EventHandler<MessageEventArgs> ReceiveMessage;

        private TcpServer _server;
        private TcpClient _client;

        #region Public Method
        #region Server
        public void StartServer(int port)
        {
            //if (_server != null) return;
            //_server = new Server(port);
            //_server.Start();
            if (_server != null) return;
            _server = new TcpServer(port);
            _server.Start();
        }

        public void StopServer()
        {
            //if (_server == null) return;
            //_server.Close();
            if (_server == null) return;
            _server.Stop();
        }
        #endregion

        #region Client
        public void StartClient(string ip, int port, int inputDeviceNumber)
        {
            //if (_client != null) return;
            //_client = new Client(ip, port, inputDeviceNumber);
            //_client.Connected += OnConnected;
            //_client.Disconnected += OnDisconnected;
            //_client.OtherClientConnected += OnOtherClientConnected;
            //_client.OtherClientDisconnected += OnOtherClientDisconnected;
            //_client.Start();
        }

        public bool StartClient(string ip, int port, string name, int inputDeviceNumber)
        {
            if (_client != null) return false;
            _client = new TcpClient(ip, port, name, inputDeviceNumber);
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.OtherClientConnected += OnOtherClientConnected;
            _client.OtherClientDisconnected += OnOtherClientDisconnected;
            _client.ReceiveMessage += OnReceiveMessage;

            if (!_client.Start())
            {
                _client.Connected -= OnConnected;
                _client.Disconnected -= OnDisconnected;
                _client.OtherClientConnected -= OnOtherClientConnected;
                _client.OtherClientDisconnected -= OnOtherClientDisconnected;
                _client.ReceiveMessage -= OnReceiveMessage;
                return false;
            }
            return true;
        }

        public void StopClient()
        {
            //if (_client == null) return;
            //_client.Connected -= OnConnected;
            //_client.Disconnected -= OnDisconnected;
            //_client.OtherClientConnected -= OnOtherClientConnected;
            //_client.OtherClientDisconnected -= OnOtherClientDisconnected;
            //_client.Close();
            if (_client == null) return;
            _client.Connected -= OnConnected;
            _client.Disconnected -= OnDisconnected;
            _client.OtherClientConnected -= OnOtherClientConnected;
            _client.OtherClientDisconnected -= OnOtherClientDisconnected;
            _client.ReceiveMessage -= OnReceiveMessage;
            _client.Stop();
        }

        public void SendMessageToServer(string name, string text)
        {
            if (_client == null) return;
            _client.SendPacket(new Packet() { Type = PacketType.Text, SendUserName = name, SendText = text });
        }

        public void SendFileToServer(string name, string fileName, byte[] fileData)
        {
            if (_client == null) return;
            _client.SendPacket(new Packet() { Type = PacketType.File, SendUserName = name, FileName = fileName, FileData = fileData });
        }
        #endregion
        #endregion

        #region Private Method
        private void OnConnected(object sender, ConnectedEventArgs e)
        {
            Connected?.Invoke(sender, e);
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            Disconnected?.Invoke(sender, e);
        }

        private void OnOtherClientConnected(object sender, ConnectedEventArgs e)
        {
            OtherClientConnected?.Invoke(sender, e);
        }

        private void OnOtherClientDisconnected(object sender, DisconnectedEventArgs e)
        {
            OtherClientDisconnected?.Invoke(sender, e);
        }

        private void OnReceiveMessage(object sender, MessageEventArgs e)
        {
            ReceiveMessage?.Invoke(sender, e);
        }
        #endregion
    }
}
