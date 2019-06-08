using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public class HotKey
    {
        public KeyModifiers Modifier { get; set; }

        public Keys Key { get; set; }

        public bool IsRegistered { get; set; }
    }
}
