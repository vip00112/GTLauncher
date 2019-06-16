using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class UdpAudioSender
    {
        private readonly UdpClient _sender;

        public UdpAudioSender(IPEndPoint endPoint)
        {
            _sender = new UdpClient();
            _sender.Connect(endPoint);
        }

        public void Send(byte[] payload)
        {
            _sender.Send(payload, payload.Length);
        }

        public void Dispose()
        {
            _sender?.Close();
        }
    }
}
