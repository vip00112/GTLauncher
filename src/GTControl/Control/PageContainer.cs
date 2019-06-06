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
    public class PageContainer : Form
    {
        private bool _isMouseDown;
        private Point _mouseDownPoint;
        private SizeMode _sizeModeWidth;
        private SizeMode _sizeModeHeight;
        private List<Page> _pages;

        #region Constuctor
        public PageContainer()
        {
            DoubleBuffered = true;

            _pages = new List<Page>();

            SizeModeWidth = SizeMode.Small;
            SizeModeHeight = SizeMode.Small;
        }
        #endregion

        #region Properties
        new public Size Size { get { return base.Size; } }

        [Category("Page Option"), DefaultValue(SizeMode.Small)]
        public SizeMode SizeModeWidth
        {
            get { return _sizeModeWidth; }
            set
            {
                _sizeModeWidth = value;
                Width = Setting.GetWidth(value);
                InitLocation();
            }
        }

        [Category("Page Option"), DefaultValue(SizeMode.Small)]
        public SizeMode SizeModeHeight
        {
            get { return _sizeModeHeight; }
            set
            {
                _sizeModeHeight = value;
                Height = Setting.GetHeight(value);
                InitLocation();
            }
        }
        #endregion

        #region Protected Method
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            Setting.Load();
            InitLocation();
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (DesignMode) return;

            var page = e.Control as Page;
            if (page == null) return;

            _pages.Add(page);
            SetMoveEvent(page.PageHeader);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            if (DesignMode) return;

            var page = e.Control as Page;
            if (page == null) return;

            _pages.Remove(page);
            page.Dispose();
        }
        #endregion

        #region Private Method
        private void InitLocation()
        {
            if (DesignMode) return;

            var screen = Screen.AllScreens[0];
            Left = (screen.WorkingArea.Width / 2) - (Width / 2);
            Top = screen.WorkingArea.Height - Height - 20;
        }

        private void SetMoveEvent(Control control)
        {
            control.MouseDown += MouseDownEvent;
            control.MouseMove += MouseMoveEvent;
            control.MouseUp += MouseUpEvent;

            foreach (Control child in control.Controls)
            {
                if (child is Label)
                {
                    SetMoveEvent(child);
                }
            }
        }

        private void MouseDownEvent(object sender, MouseEventArgs e)
        {
            if (!Setting.CanMove) return;

            _isMouseDown = true;
            _mouseDownPoint = e.Location;
        }

        private void MouseMoveEvent(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown) return;

            int diffX = _mouseDownPoint.X - e.Location.X;
            int diffY = _mouseDownPoint.Y - e.Location.Y;

            int x = Location.X - diffX;
            int y = Location.Y - diffY;
            Location = new Point(x, y);
        }

        private void MouseUpEvent(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
        }
        #endregion

        #region Public Method
        public void ResetLayout(SizeMode width, SizeMode height)
        {
            SizeModeWidth = width;
            SizeModeHeight = height;

            // 기존 페이지 삭제
            var pages = _pages.ToArray();
            foreach (var page in pages)
            {
                Controls.Remove(page);
            }

            // 페이지 등록
            foreach (var p in Setting.Pages)
            {
                var page = new Page(p.PageName);
                page.Title = p.Title;
                page.VisibleTitle = p.VisibleTitle;
                page.VisibleHeader = p.VisibleHeader;
                page.VisibleBackButton = p.VisibleBackButton;
                page.CloseMode = p.CloseMode;
                if (page.CloseMode == PageCloseMode.Dispose)
                {
                    page.OnDisposed += delegate (object sender, EventArgs e) { Application.Exit(); };
                }

                // 해당 페이지의 아이템 등록
                var pageItems = Setting.PageItems.Where(o => o.PageName == p.PageName);
                foreach (var pageItem in pageItems)
                {
                    var item = page.AddItem(pageItem);
                    if (item != null)
                    {
                        item.OnFolderClickEvent += delegate (object sender, EventArgs e)
                        {
                            string pageName = item.LinkPageName;
                            if (string.IsNullOrWhiteSpace(pageName)) return;

                            var linkPage = _pages.FirstOrDefault(o => o.PageName == pageName);
                            if (linkPage == null) return;

                            linkPage.Show();
                            linkPage.BringToFront();
                        };
                    }
                }

                Controls.Add(page);
            }

            var main = _pages.FirstOrDefault(o => o.PageName == "Main");
            if (main != null) main.BringToFront();
        }
        #endregion
    }
}
