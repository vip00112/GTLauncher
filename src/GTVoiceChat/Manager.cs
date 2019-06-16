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
        public EventHandler OnDisposed;

        private volatile bool _connected;
        private AudioReceiver _receiver;
        private AudioSender _audioSender;
        private INetworkChatCodec _serverCodec;
        private INetworkChatCodec _clientCodec;

        private Server _server;
        private Client _client;

        public Manager()
        {
            //_codec = new UncompressedPcmChatCodec();
            //_codec = new UltraWideBandSpeexCodec();
            //_codec = new WideBandSpeexCodec();
            _serverCodec = new NarrowBandSpeexCodec();
            _clientCodec = new NarrowBandSpeexCodec();
        }

        public void StartServer(int port, int inputDeviceNumber)
        {
            if (_server != null) return;
            _server = new Server(port, inputDeviceNumber, _serverCodec);
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
            _client = new Client(ip, port, inputDeviceNumber, _clientCodec);
            _client.OnConnected += ClientConnected;
            _client.OnDisposed += ClientDisposed;
            _client.Start();
        }

        public void StopClient()
        {
            if (_client == null) return;
            _client.Close();
        }

        //public void Connect(IPEndPoint endPoint, int inputDeviceNumber)
        //{
        //    if (_connected) return;

        //    var receiver = new UdpAudioReceiver(endPoint.Port);
        //    var sender = new UdpAudioSender(endPoint);

        //    _receiver = new AudioReceiver(_servercodec, receiver);
        //    _audioSender = new AudioSender(_servercodec, inputDeviceNumber, sender);
        //    _connected = true;
        //}

        //public void Disconnect()
        //{
        //    if (_connected)
        //    {
        //        _connected = false;

        //        _receiver.Dispose();
        //        _audioSender.Dispose();
        //    }
        //}

        private void ClientConnected(object sender, ConnectedEventArgs e)
        {
            switch (e.Code)
            {
                case System.Net.Sockets.SocketError.ConnectionRefused:
                    MessageBoxUtil.Error("Not found EndPoint.");
                    break;
            }
        }

        private void ClientDisposed(object sender, EventArgs e)
        {
            if (OnDisposed != null) OnDisposed(this, e);
        }
    }
}
