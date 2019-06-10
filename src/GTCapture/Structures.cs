using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GTCapture
{
    [StructLayout(LayoutKind.Sequential)]
    public struct WinStructRect
    {
        public int Left;
        public int Top;
        public int Right;
        public int Bottom;
    }
}
