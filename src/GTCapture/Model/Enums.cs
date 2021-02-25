using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTCapture
{
    public enum CaptureMode
    {
        None, FullScreen, ActiveProcess, Region, RecordGif, RecordVideo, RecordStart, RecordStop
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
