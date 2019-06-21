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
        public const int BufferSize = 1024 * 4;

        public StateObject(Socket socket)
        {
            Socket = socket;
            Buffer = new byte[BufferSize];
            ProcessingBuffer = new byte[BufferSize * 4];
        }

        public Socket Socket { get; }

        public byte[] Buffer { get; }

        public byte[] ProcessingBuffer { get; }

        public int ProcessingOffset { get; set; }
    }
}
