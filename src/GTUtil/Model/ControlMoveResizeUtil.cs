using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTUtil
{
    public static class ControlMoveResizeUtil
    {
        public static Rectangle CalcBounds(MoveResizeAction type, Control control, Point startLoc, Point currentLoc)
        {
            var bounds = new Rectangle(control.Location.X, control.Location.Y, control.Width, control.Height);
            switch (type)
            {
                case MoveResizeAction.Move:
                    bounds.X += currentLoc.X - startLoc.X;
                    bounds.Y += currentLoc.Y - startLoc.Y;
                    break;
                case MoveResizeAction.ResizeLeft:
                    bounds.Width -= currentLoc.X - startLoc.X;
                    bounds.X += currentLoc.X - startLoc.X;
                    break;
                case MoveResizeAction.ResizeTop:
                    bounds.Height -= currentLoc.Y - startLoc.Y;
                    bounds.Y += currentLoc.Y - startLoc.Y;
                    break;
                case MoveResizeAction.ResizeRight:
                    bounds.Width = currentLoc.X;
                    break;
                case MoveResizeAction.ResizeBottom:
                    bounds.Height = currentLoc.Y;
                    break;
                case MoveResizeAction.ResizeTopLeft:
                    bounds.Height -= currentLoc.Y - startLoc.Y;
                    bounds.Y += currentLoc.Y - startLoc.Y;
                    bounds.Width -= currentLoc.X - startLoc.X;
                    bounds.X += currentLoc.X - startLoc.X;
                    break;
                case MoveResizeAction.ResizeTopRight:
                    bounds.Height -= currentLoc.Y - startLoc.Y;
                    bounds.Y += currentLoc.Y - startLoc.Y;
                    bounds.Width = currentLoc.X;
                    break;
                case MoveResizeAction.ResizeBottomRight:
                    bounds.Height = currentLoc.Y;
                    bounds.Width = currentLoc.X;
                    break;
                case MoveResizeAction.ResizeBottomLeft:
                    bounds.Height = currentLoc.Y;
                    bounds.Width -= currentLoc.X - startLoc.X;
                    bounds.X += currentLoc.X - startLoc.X;
                    break;
            }
            return bounds;
        }
    }
}
