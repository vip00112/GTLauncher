using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class MessageEventArgs : EventArgs
    {
        public MessageEventArgs(string name, string text)
        {
            Name = name;
            Text = text;
        }

        public string Name { get; set; }

        public string Text { get; set; }
    }
}
