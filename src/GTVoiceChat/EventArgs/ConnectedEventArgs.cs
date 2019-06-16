using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class ConnectedEventArgs : EventArgs
    {
        public ConnectedEventArgs(SocketError code)
        {
            Code = code;
        }

        public SocketError Code { get; set; }
    }
}
