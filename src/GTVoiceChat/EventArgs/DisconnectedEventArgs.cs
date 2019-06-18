using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class DisconnectedEventArgs : EventArgs
    {
        public DisconnectedEventArgs(string id, string host)
        {
            ID = id;
            Host = host;
        }

        public DisconnectedEventArgs(Exception exception)
        {
            Exception = exception;
        }

        public string ID { get; set; }

        public string Host { get; set; }

        public Exception Exception { get; set; }
    }
}
