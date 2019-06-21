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
            Name = name;
        }

        public ConnectedEventArgs(string name, string[] onlineUserNames)
        {
            Name = name;
            OnlineUserNames = onlineUserNames;
        }

        public string Name { get; set; }

        public string[] OnlineUserNames { get; set; }
    }
}
