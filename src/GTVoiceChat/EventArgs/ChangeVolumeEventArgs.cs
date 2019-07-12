using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class ChangeVolumeEventArgs : EventArgs
    {
        public ChangeVolumeEventArgs(string name, float volume)
        {
            Name = name;
            Volume = volume;
        }

        public string Name { get; set; }

        /// <summary>
        /// Max : 1.0f
        /// </summary>
        public float Volume { get; set; }
    }
}
