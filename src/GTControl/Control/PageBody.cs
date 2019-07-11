using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class PageBody : Panel
    {
        public enum Edge { None, TopLeft, BottomRight }

        private const int DotSize = 10;
        public const int Grid = 10;

        private Dictionary<string, Wrapper> _wrappers;

        #region Constructor
        public PageBody()
        {
            DoubleBuffered = true;

            _wrappers = new Dictionary<string, Wrapper>();

            Padding = new Padding(0);
            Margin = new Padding(0);
            Dock = DockStyle.Fill;
        }
        #endregion

        #region Properties
        public string PageName
        {
            get
            {
                var page = Parent as Page;
                if (page == null) return null;

                return page.PageName;
            }
        }

        public int ColumnCount { get { return Width / Grid; } }

        public int RowCount { get { return Height / Grid; } }
        #endregion

        #region Public Method
        public void StartEditItem(PageItem item)
        {
            string key = CreateKey(item);
            if (_wrappers.ContainsKey(key)) return;

            var wrapper = new Wrapper(item);
            _wrappers.Add(key, wrapper);
            wrapper.Init();
        }

        public void StopEditItem(PageItem item)
        {
            string key = CreateKey(item);
            if (!_wrappers.ContainsKey(key)) return;

            var wrapper = _wrappers[key];
            _wrappers.Remove(key);
            wrapper.Dispose();
        }
        #endregion

        #region Private Method
        private string CreateKey(Control control)
        {
            string key = control.GetHashCode().ToString();

            Control parent = control.Parent;
            while (parent != null)
            {
                key.Insert(0, parent.Name + ".");
                parent = parent.Parent;
            }
            return key;
        }
        #endregion

        #region Inner Class
        private class Wrapper
        {
            private PageItem _item;
            private Control _parent;
            private Point _minPoint;
            private Point _maxPoint;
            private Size _minSize;
            private Size _maxSize;
            private Edge _edge;

            public Wrapper(PageItem item)
            {
                _item = item;
                _parent = item.Parent;
            }

            #region Properties
            public Rectangle TopLeft
            {
                get
                {
                    return new Rectangle(0, 0, DotSize, DotSize);
                }
            }

            public Rectangle BottomRight
            {
                get
                {
                    int right = _item.Width - DotSize;
                    int bottom = _item.Height - DotSize;
                    return new Rectangle(right, bottom, DotSize, DotSize);
                }
            }
            #endregion

            #region Public Method
            public void Init()
            {
                _item.WrapperControl.MouseDown += MouseDown;
                _item.WrapperControl.MouseMove += MouseMove;
                _item.WrapperControl.MouseUp += MouseUp;
                _item.WrapperControl.Paint += Paint;

                CalcLimit();
                if (_item.Width < _minSize.Width) _item.Width = _minSize.Width;
                if (_item.Width > _maxSize.Width) _item.Width = _maxSize.Width;
                if (_item.Height < _minSize.Height) _item.Height = _minSize.Height;
                if (_item.Height > _maxSize.Height) _item.Height = _maxSize.Height;
            }

            public void Dispose()
            {
                _item.WrapperControl.MouseDown -= MouseDown;
                _item.WrapperControl.MouseMove -= MouseMove;
                _item.WrapperControl.MouseUp -= MouseUp;
                _item.WrapperControl.Paint -= Paint;
                _item.Parent = _parent;
            }
            #endregion

            #region Control Event
            private void MouseDown(object sender, MouseEventArgs e)
            {
                _edge = CalcEdge(e.Location);
                _item.BringToFront();
            }

            private void MouseMove(object sender, MouseEventArgs e)
            {
                ChangeCursor(e.Location);

                if (_edge == Edge.None) return;

                var bounds = Rectangle.Empty;
                switch (_edge)
                {
                    case Edge.TopLeft:
                        bounds.X = e.X;
                        if (bounds.X % Grid != 0) bounds.X -= bounds.X % Grid;
                        bounds.X = _item.Left + bounds.X;

                        bounds.Y = e.Y;
                        if (bounds.Y % Grid != 0) bounds.Y -= bounds.Y % Grid;
                        bounds.Y = _item.Top + bounds.Y;

                        bounds.Width = _item.Width;
                        bounds.Height = _item.Height;
                        break;
                    case Edge.BottomRight:
                        bounds.X = _item.Left;
                        bounds.Y = _item.Top;

                        bounds.Width = _item.Width - e.X;
                        if (bounds.Width % Grid != 0) bounds.Width -= bounds.Width % Grid;
                        bounds.Width = _item.Width - bounds.Width;

                        bounds.Height = _item.Height - e.Y;
                        if (bounds.Height % Grid != 0) bounds.Height -= bounds.Height % Grid;
                        bounds.Height = _item.Height - bounds.Height;
                        break;
                }
                if (bounds.X < _minPoint.X) bounds.X = _minPoint.X;
                if (bounds.Y < _minPoint.Y) bounds.Y = _minPoint.Y;
                if (bounds.X > _maxPoint.X) bounds.X = _maxPoint.X;
                if (bounds.Y > _maxPoint.Y) bounds.Y = _maxPoint.Y;
                if (bounds.Width < _minSize.Width) bounds.Width = _minSize.Width;
                if (bounds.Height < _minSize.Height) bounds.Height = _minSize.Height;
                if (bounds.Width > _maxSize.Width) bounds.Width = _maxSize.Width;
                if (bounds.Height > _maxSize.Height) bounds.Height = _maxSize.Height;
                _item.Bounds = bounds;

                CalcLimit();
                _item.Invalidate();
            }

            private void MouseUp(object sender, MouseEventArgs e)
            {
                _edge = Edge.None;
                _item.Invalidate();
            }

            private void Paint(object sender, PaintEventArgs e)
            {
                using (var b = new SolidBrush(Color.Red))
                using (var p = new Pen(Color.Red))
                {
                    // TopLeft
                    e.Graphics.FillRectangle(b, 0, 0, DotSize, DotSize);

                    // BottomRight
                    int step = 5;
                    int piece = DotSize / step;
                    int startX = _item.Width - DotSize;
                    int startY = _item.Height - DotSize;
                    for (int i = 0; i < step; i++)
                    {
                        e.Graphics.DrawLine(p, startX + (piece * i), _item.Height, _item.Width, startY + (piece * i));
                    }
                }
            }
            #endregion

            #region Private Method
            private Edge CalcEdge(Point p)
            {
                var type = Edge.None;
                if (IsInRegion(BottomRight, p)) type = Edge.BottomRight;
                else if (IsInRegion(TopLeft, p)) type = Edge.TopLeft;

                return type;
            }

            private bool IsInRegion(Rectangle rec, Point p)
            {
                int minX = rec.X;
                int maxX = rec.X + rec.Width;
                int minY = rec.Y;
                int maxY = rec.Y + rec.Height;
                return p.X >= minX && p.X <= maxX && p.Y >= minY && p.Y <= maxY;
            }

            private void CalcLimit()
            {
                if (_item.Parent != null)
                {
                    var parent = _item.Parent;
                    _minPoint = new Point(parent.ClientRectangle.Left, parent.ClientRectangle.Top);
                    _maxPoint = new Point(parent.ClientRectangle.Right - _item.Width, parent.ClientRectangle.Bottom - _item.Height);
                    _minSize = new Size(DotSize * 2, DotSize * 2);
                    _maxSize = new Size(parent.ClientRectangle.Right - _item.Left, parent.ClientRectangle.Bottom - _item.Top);
                }
            }

            private void ChangeCursor(Point p)
            {
                if (_edge != Edge.None) return;
                if (IsInRegion(TopLeft, p))
                {
                    _item.Cursor = Cursors.SizeAll;
                }
                else if (IsInRegion(BottomRight, p))
                {
                    _item.Cursor = Cursors.SizeNWSE;
                }
                else
                {
                    _item.Cursor = Cursors.Default;
                }
            }
            #endregion
        }
        #endregion
    }
}
