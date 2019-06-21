using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class DisconnectedEventArgs : EventArgs
    {
        public DisconnectedEventArgs(string name)
        {
            Name = name;
        }

        public DisconnectedEventArgs(Exception exception)
        {
            Exception = exception;
        }

        public string Name { get; set; }

        public Exception Exception { get; set; }
    }
}
