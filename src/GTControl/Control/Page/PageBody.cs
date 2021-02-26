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

        public const int DotSize = 10;
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

            Items = new List<PageItem>();
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

        public List<PageItem> Items { get; }

        public int ColumnCount { get { return Width / Grid; } }

        public int RowCount { get { return Height / Grid; } }
        #endregion

        #region Protected Method
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (LayoutSetting.IsEditMode)
            {
                DrawLine(e.Graphics);
            }
        }
        #endregion

        #region Public Method
        public void AddItem(PageItem item)
        {
            if (item == null) return;
            Items.Add(item);
            Controls.Add(item);
        }

        public void RemoveItem(PageItem item)
        {
            if (item == null) return;
            Items.Remove(item);
            Controls.Remove(item);
            item.Dispose();
        }

        public void StartEditItem(PageItem item, MouseEventHandler mouseDownEvent, PaintEventHandler paintEvent)
        {
            string key = CreateKey(item);
            if (_wrappers.ContainsKey(key)) return;

            item.OnMouseDownEventForEdit += mouseDownEvent;
            item.OnPaintEventForEdit += paintEvent;

            var wrapper = new Wrapper(item);
            _wrappers.Add(key, wrapper);
            wrapper.Init();
        }

        public void StopEditItem(PageItem item, MouseEventHandler mouseDownEvent, PaintEventHandler paintEvent)
        {
            string key = CreateKey(item);
            if (!_wrappers.ContainsKey(key)) return;

            item.OnMouseDownEventForEdit -= mouseDownEvent;
            item.OnPaintEventForEdit -= paintEvent;

            var wrapper = _wrappers[key];
            _wrappers.Remove(key);
            wrapper.Dispose();
        }
        #endregion

        #region Private Method
        private void DrawLine(Graphics g)
        {
            using (var normal = new Pen(Color.LightGray))
            using (var highlight = new Pen(Color.DarkGray))
            {
                for (int col = 0; col < ColumnCount; col++)
                {
                    if (col % 5 == 0)
                    {
                        g.DrawLine(highlight, col * Grid, 0, col * Grid, Height);
                    }
                    else
                    {
                        g.DrawLine(normal, col * Grid, 0, col * Grid, Height);
                    }
                }
                g.DrawLine(highlight, Width - 1, 0, Width - 1, Height);
                for (int row = 0; row < RowCount; row++)
                {
                    if (row % 5 == 0)
                    {
                        g.DrawLine(highlight, 0, row * Grid, Width, row * Grid);
                    }
                    else
                    {
                        g.DrawLine(normal, 0, row * Grid, Width, row * Grid);
                    }
                }
                g.DrawLine(highlight, 0, Height - 1, Width, Height - 1);
            }
        }

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
            private Edge _edge;

            public Wrapper(PageItem item)
            {
                _item = item;
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

                _item.CalcLimit();
                if (_item.Width < _item.MinSize.Width) _item.Width = _item.MinSize.Width;
                if (_item.Height < _item.MinSize.Height) _item.Height = _item.MinSize.Height;
                if (_item.Width > _item.MaxSize.Width) _item.Width = _item.MaxSize.Width;
                if (_item.Height > _item.MaxSize.Height) _item.Height = _item.MaxSize.Height;
            }

            public void Dispose()
            {
                _item.WrapperControl.MouseDown -= MouseDown;
                _item.WrapperControl.MouseMove -= MouseMove;
                _item.WrapperControl.MouseUp -= MouseUp;
                _item.WrapperControl.Paint -= Paint;
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
                if (bounds.X < _item.MinPoint.X) bounds.X = _item.MinPoint.X;
                if (bounds.Y < _item.MinPoint.Y) bounds.Y = _item.MinPoint.Y;
                if (bounds.X > _item.MaxPoint.X) bounds.X = _item.MaxPoint.X;
                if (bounds.Y > _item.MaxPoint.Y) bounds.Y = _item.MaxPoint.Y;
                if (bounds.Width < _item.MinSize.Width) bounds.Width = _item.MinSize.Width;
                if (bounds.Height < _item.MinSize.Height) bounds.Height = _item.MinSize.Height;
                if (bounds.Width > _item.MaxSize.Width) bounds.Width = _item.MaxSize.Width;
                if (bounds.Height > _item.MaxSize.Height) bounds.Height = _item.MaxSize.Height;
                _item.Bounds = bounds;

                _item.CalcLimit();
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
                    _item.Cursor = Cursors.Hand;
                }
            }
            #endregion
        }
        #endregion

    }
}