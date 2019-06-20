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

        private TcpServer _myServer;
        private TcpClient _myClient;

        public void StartServer(int port)
        {
            //if (_server != null) return;
            //_server = new Server(port);
            //_server.Start();
            if (_myServer != null) return;
            _myServer = new TcpServer(port);
            _myServer.Start();
        }

        public void StopServer()
        {
            //if (_server == null) return;
            //_server.Close();
            if (_myServer == null) return;
            _myServer.Stop();
        }

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
            if (_myClient != null) return false;
            _myClient = new TcpClient(ip, port, name, inputDeviceNumber);
            _myClient.Connected += OnConnected;
            _myClient.Disconnected += OnDisconnected;
            _myClient.OtherClientConnected += OnOtherClientConnected;
            _myClient.OtherClientDisconnected += OnOtherClientDisconnected;

            if (!_myClient.Start())
            {
                _myClient.Connected -= OnConnected;
                _myClient.Disconnected -= OnDisconnected;
                _myClient.OtherClientConnected -= OnOtherClientConnected;
                _myClient.OtherClientDisconnected -= OnOtherClientDisconnected;
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
            if (_myClient == null) return;
            _myClient.Connected -= OnConnected;
            _myClient.Disconnected -= OnDisconnected;
            _myClient.OtherClientConnected -= OnOtherClientConnected;
            _myClient.OtherClientDisconnected -= OnOtherClientDisconnected;
            _myClient.Stop();
        }

        private void OnConnected(object sender, ConnectedEventArgs e)
        {
            if (Connected != null) Connected(sender, e);
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            if (Disconnected != null) Disconnected(sender, e);
        }

        private void OnOtherClientConnected(object sender, ConnectedEventArgs e)
        {
            if (OtherClientConnected != null) OtherClientConnected(sender, e);
        }

        private void OnOtherClientDisconnected(object sender, DisconnectedEventArgs e)
        {
            if (OtherClientDisconnected != null) OtherClientDisconnected(sender, e);
        }
    }
}
