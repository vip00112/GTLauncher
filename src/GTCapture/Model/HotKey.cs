using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTUtil;

namespace GTCapture
{
    public class HotKey
    {
        #region Properties
        public CaptureMode CaptureMode { get; set; }

        public WindowNative.KeyModifiers Modifiers { get; private set; }

        public Keys Key { get; private set; }

        public bool IsRegistered { get; set; }
        #endregion

        #region Public Method
        public void Update(WindowNative.KeyModifiers modifiers, Keys key)
        {
            CaptureSetting.UnregisterHotKey(CaptureMode);

            if (modifiers == WindowNative.KeyModifiers.None && key == Keys.Escape)
            {
                key = Keys.None;
            }

            Modifiers = modifiers;
            Key = key;
            CaptureSetting.RegisterHotKey(CaptureMode);
        }

        public override string ToString()
        {
            string modifiers = "";
            foreach (WindowNative.KeyModifiers mod in Enum.GetValues(typeof(WindowNative.KeyModifiers)))
            {
                if (mod == WindowNative.KeyModifiers.None) continue;

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
            else if (Key == Keys.Escape)
            {
                key = "None";
            }

            return modifiers + key;
        }
        #endregion

        #region Static Method
        public static HotKey CreateDefault(CaptureMode mode)
        {
            switch (mode)
            {
                case CaptureMode.None:
                    return new HotKey() { CaptureMode = mode, Modifiers = WindowNative.KeyModifiers.None, Key = Keys.None };
                case CaptureMode.FullScreen:
                    return new HotKey() { CaptureMode = mode, Modifiers = WindowNative.KeyModifiers.Alt, Key = Keys.D1 };
                case CaptureMode.ActiveProcess:
                    return new HotKey() { CaptureMode = mode, Modifiers = WindowNative.KeyModifiers.Alt, Key = Keys.D2 };
                case CaptureMode.Region:
                    return new HotKey() { CaptureMode = mode, Modifiers = WindowNative.KeyModifiers.Alt, Key = Keys.D3 };
                case CaptureMode.RecordGif:
                    return new HotKey() { CaptureMode = mode, Modifiers = WindowNative.KeyModifiers.Alt, Key = Keys.D4 };
                case CaptureMode.RecordVideo:
                    return new HotKey() { CaptureMode = mode, Modifiers = WindowNative.KeyModifiers.Alt, Key = Keys.D5 };
                case CaptureMode.RecordStart:
                    return new HotKey() { CaptureMode = mode, Modifiers = WindowNative.KeyModifiers.Control, Key = Keys.D1 };
                case CaptureMode.RecordStop:
                    return new HotKey() { CaptureMode = mode, Modifiers = WindowNative.KeyModifiers.Control, Key = Keys.D2 };
                default: return null;
            }
        }

        public static HotKey CreateTemplate(Keys modifiers, Keys key)
        {
            var hotKey = new HotKey();
            hotKey.Key = key;
            hotKey.Modifiers = WindowNative.KeyModifiers.None;
            if (modifiers.HasFlag(Keys.Alt)) hotKey.Modifiers |= WindowNative.KeyModifiers.Alt;
            if (modifiers.HasFlag(Keys.Control)) hotKey.Modifiers |= WindowNative.KeyModifiers.Control;
            if (modifiers.HasFlag(Keys.Shift)) hotKey.Modifiers |= WindowNative.KeyModifiers.Shift;
            return hotKey;
        }
        #endregion
    }
}
