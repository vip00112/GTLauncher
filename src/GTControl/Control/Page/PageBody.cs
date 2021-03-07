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

        #region Constructor
        public PageBody()
        {
            DoubleBuffered = true;

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
        public void BringToFront(PageItem item)
        {
            item.BringToFront();
            if (Items.Contains(item))
            {
                Items.Remove(item);
                Items.Insert(0, item);
            }
        }

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
        #endregion

    }
}