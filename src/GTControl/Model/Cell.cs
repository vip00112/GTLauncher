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

        public int Column { get; set; }

        public int Row { get; set; }

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
            int startCol = start.Column;
            int endCol = end.Column;
            int startRow = start.Row;
            int endRow = end.Row;
            return Column >= startCol && Column <= endCol && Row >= startRow && Row <= endRow;
        }
    }
}
