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
        public const int ProcessingBufferSize = 1024 * 4 * 512;

        public StateObject(Socket workSocket)
        {
            WorkSocket = workSocket;
            Buffer = new byte[BufferSize];
            ProcessingBuffer = new byte[ProcessingBufferSize];
        }

        public Socket WorkSocket { get; }

        public byte[] Buffer { get; }

        public byte[] ProcessingBuffer { get; }

        public int ProcessingOffset { get; set; }
    }
}
