using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class ChatSetting
    {
        public ChatSetting()
        {
            IP = "127.0.0.1";
        }

        public string Name { get; set; }

        public string IP { get; set; }

        public int Port { get; set; }

        public int InputDeviceNumber { get; set; }

        public int OutputDeviceNumber { get; set; }
    }
}
