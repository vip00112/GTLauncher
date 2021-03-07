using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public enum MoveResizeAction
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
