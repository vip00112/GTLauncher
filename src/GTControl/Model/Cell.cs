using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTControl
{
    public class Cell
    {
        public int X { get; set; }

        public int Y { get; set; }

        public int Width { get; set; }

        public int Height { get; set; }

        public bool IsSelected { get; set; }

        public bool IsInLocation(Point loc)
        {
            int startX = X;
            int endX = startX + Width;
            int startY = Y;
            int endY = startY + Height;
            return loc.X >= startX && loc.X <= endX && loc.Y >= startY && loc.Y <= endY;
        }

        public bool IsInRange(Cell start, Cell end)
        {
            int startX = start.X;
            int endX = end.X;
            int startY = start.Y;
            int endY = end.Y;
            return X >= startX && X <= endX && Y >= startY && Y <= endY;
        }
    }
}
