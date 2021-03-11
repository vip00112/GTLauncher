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

    public enum FullScreenMode
    {
        MainMonitor, ActiveMonitor, AllMonitor
    }

    public enum DrawMode 
    { 
        Pen, Highlighter 
    }
}
