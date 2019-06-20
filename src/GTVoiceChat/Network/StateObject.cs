using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class StateObject
    {
        public const int BufferSize = 1024 * 5120;

        public StateObject(Socket socket)
        {
            Socket = socket;
            Buffer = new byte[BufferSize];
        }

        public Socket Socket { get; }

        public byte[] Buffer { get; }
    }
}
