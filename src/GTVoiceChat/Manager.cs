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

        private Server _server;
        private Client _client;

        public void StartServer(int port, int inputDeviceNumber)
        {
            if (_server != null) return;
            _server = new Server(port, inputDeviceNumber);
            _server.Start();
        }

        public void StopServer()
        {
            if (_server == null) return;
            _server.Close();
        }

        public void StartClient(string ip, int port, int inputDeviceNumber)
        {
            if (_client != null) return;
            _client = new Client(ip, port, inputDeviceNumber);
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.OtherClientConnected += OnOtherClientConnected;
            _client.OtherClientDisconnected += OnOtherClientDisconnected;
            _client.Start();
        }

        public void StopClient()
        {
            if (_client == null) return;
            _client.Connected -= OnConnected;
            _client.Disconnected -= OnDisconnected;
            _client.OtherClientConnected -= OnOtherClientConnected;
            _client.OtherClientDisconnected -= OnOtherClientDisconnected;
            _client.Close();
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
