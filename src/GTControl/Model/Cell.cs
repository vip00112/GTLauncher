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
            int startX = Math.Min(start.X, end.X);
            int startY = Math.Min(start.Y, end.Y);
            int endX = Math.Max(start.X, end.X);
            int endY = Math.Max(start.Y, end.Y);
            return X >= startX && X <= endX && Y >= startY && Y <= endY;
        }
    }
}
