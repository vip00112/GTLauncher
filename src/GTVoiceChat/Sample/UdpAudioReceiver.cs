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
    public class UdpAudioReceiver
    {
        private Action<byte[]> _handler;
        private readonly UdpClient _receiver;
        private bool _isListening;

        public UdpAudioReceiver(int port)
        {
            var endPoint = new IPEndPoint(IPAddress.Any, port);

            _receiver = new UdpClient();
            _receiver.Client.SetSocketOption(SocketOptionLevel.Socket, SocketOptionName.ReuseAddress, true);
            _receiver.Client.Bind(endPoint);

            ThreadPool.QueueUserWorkItem(ListenerThread, endPoint);
            _isListening = true;
        }

        private void ListenerThread(object state)
        {
            var endPoint = (IPEndPoint) state;
            try
            {
                while (_isListening)
                {
                    byte[] data = _receiver.Receive(ref endPoint);
                    _handler?.Invoke(data);
                }
            }
            catch (SocketException)
            {
                // usually not a problem - just means we have disconnected
            }
        }

        public void Dispose()
        {
            _isListening = false;
            _receiver?.Close();
        }

        public void OnReceived(Action<byte[]> action)
        {
            _handler = action;
        }
    }
}
