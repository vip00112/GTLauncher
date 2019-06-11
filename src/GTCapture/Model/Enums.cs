using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTCapture
{
    public enum KeyModifiers
    {
        None = 0, Alt = 1, Control = 2, Shift = 4, Windows = 8
    }

    public enum CaptureMode
    {
        None, FullScreen, ActiveProcess, Region
    }

    public enum DeviceCap
    {
        VERTRES = 10, DESKTOPVERTRES = 117,
    }
}
