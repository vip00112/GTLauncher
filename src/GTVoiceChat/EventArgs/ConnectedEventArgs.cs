﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class ConnectedEventArgs : EventArgs
    {
        public ConnectedEventArgs(string id, string host)
        {
            ID = id;
            Host = host;
        }

        public string ID { get; set; }

        public string Host { get; set; }
    }
}
