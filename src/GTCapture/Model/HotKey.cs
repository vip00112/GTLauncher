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
        #region Properties
        public CaptureMode CaptureMode { get; set; }

        public KeyModifiers Modifiers { get; set; }

        public Keys Key { get; set; }

        public bool IsRegistered { get; set; }
        #endregion

        #region Public Method
        public override string ToString()
        {
            string modifiers = "";
            foreach (KeyModifiers mod in Enum.GetValues(typeof(KeyModifiers)))
            {
                if (mod == KeyModifiers.None) continue;

                if (Modifiers.HasFlag(mod))
                {
                    modifiers += string.Format("{0} + ", mod.ToString());
                }
            }

            string key = Key.ToString();
            if (Key >= Keys.D0 && Key <= Keys.D9)
            {
                key = key.Replace("D", "");
            }

            return modifiers + key;
        }
        #endregion

        #region Static Method
        public static HotKey GetDefault(CaptureMode mode)
        {
            switch (mode)
            {
                case CaptureMode.None:
                    return new HotKey() { CaptureMode = mode, Modifiers = KeyModifiers.None, Key = Keys.None };
                case CaptureMode.FullScreen:
                    return new HotKey() { CaptureMode = mode, Modifiers = KeyModifiers.Alt, Key = Keys.D1 };
                case CaptureMode.ActiveProcess:
                    return new HotKey() { CaptureMode = mode, Modifiers = KeyModifiers.Alt, Key = Keys.D2 };
                case CaptureMode.Region:
                    return new HotKey() { CaptureMode = mode, Modifiers = KeyModifiers.Alt, Key = Keys.D3 };
                case CaptureMode.RecordGif:
                    return new HotKey() { CaptureMode = mode, Modifiers = KeyModifiers.Alt, Key = Keys.D4 };
                case CaptureMode.RecordVideo:
                    return new HotKey() { CaptureMode = mode, Modifiers = KeyModifiers.Alt, Key = Keys.D5 };
                case CaptureMode.RecordStart:
                    return new HotKey() { CaptureMode = mode, Modifiers = KeyModifiers.Control, Key = Keys.D1 };
                case CaptureMode.RecordStop:
                    return new HotKey() { CaptureMode = mode, Modifiers = KeyModifiers.Control, Key = Keys.D2 };
                default: return null;
            }
        }
        #endregion
    }
}
