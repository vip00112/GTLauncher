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
        None, FullScreen, ActiveProcess, Region, RecordGif, RecordVideo, RecordStart, RecordStop
    }

    public enum DeviceCap
    {
        VERTRES = 10, DESKTOPVERTRES = 117,
    }

    public enum FormActionType
    {
        None,

        Move,

        ResizeLeft,
        ResizeTop,
        ResizeRight,
        ResizeBottom,

        ResizeTopLeft,
        ResizeTopRight,
        ResizeBottomRight,
        ResizeBottomLeft
    }
}
