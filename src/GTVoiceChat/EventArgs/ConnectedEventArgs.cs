using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class ConnectedEventArgs : EventArgs
    {
        public ConnectedEventArgs(string name)
        {
            ID = name;
        }

        public ConnectedEventArgs(string name, string[] onlineUserNames)
        {
            ID = name;
            OnlineUserNames = onlineUserNames;
        }

        public string ID { get; set; }

        public string[] OnlineUserNames { get; set; }
    }
}
