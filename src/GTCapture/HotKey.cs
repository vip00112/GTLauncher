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
        public KeyModifiers Modifiers { get; set; }

        public Keys Key { get; set; }

        public bool IsRegistered { get; set; }

        public override string ToString()
        {
            string result = "";
            foreach (KeyModifiers modifier in Enum.GetValues(typeof(KeyModifiers)))
            {
                if (modifier == KeyModifiers.None) continue;

                if (Modifiers.HasFlag(modifier))
                {
                    result += string.Format("{0} + ", modifier.ToString());
                }
            }
            result += Key.ToString();
            return result;
        }
    }
}
