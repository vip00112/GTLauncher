using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class DecodedEventArgs : EventArgs
    {
        public DecodedEventArgs(byte[] decoded)
        {
            Decodecd = decoded;
        }

        public byte[] Decodecd { get; set; }
    }
}
