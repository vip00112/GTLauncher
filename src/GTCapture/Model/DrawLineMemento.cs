using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTCapture
{
    public class DrawLineMemento : IMemento
    {
        #region Constructor
        public DrawLineMemento(Color color, int size)
        {
            Color = color;
            LineSize = size;
            Locations = new List<Point>();
        }
        #endregion

        #region Properties
        public Color Color { get; }

        public int LineSize { get; }

        public List<Point> Locations { get; private set; }
        #endregion

        #region Public Method
        public void AddLocation(Point loc)
        {
            Locations.Add(loc);
        }
        #endregion

        #region IMemento Method
        public bool CanDraw()
        {
            return Locations.Count > 1;
        }

        public IMemento Clone()
        {
            var clone = new DrawLineMemento(Color, LineSize);
            foreach (var loc in Locations)
            {
                clone.Locations.Add(loc);
            }
            return clone;
        }

        public void Draw(Graphics g)
        {
            if (!CanDraw()) return;

            using (Pen pen = new Pen(Color, LineSize))
            {
                pen.LineJoin = System.Drawing.Drawing2D.LineJoin.Round;
                pen.StartCap = System.Drawing.Drawing2D.LineCap.Round;
                pen.EndCap = System.Drawing.Drawing2D.LineCap.Round;

                g.DrawCurve(pen, Locations.ToArray());
            }
        }
        #endregion
    }
}