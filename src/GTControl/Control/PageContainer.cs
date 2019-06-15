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
        private DockMode _dockMode;
        private SizeMode _sizeModeWidth;
        private SizeMode _sizeModeHeight;
        private List<Page> _pages;

        #region Constuctor
        public PageContainer()
        {
            DoubleBuffered = true;

            _pages = new List<Page>();

            DockMode = DockMode.BottomCenter;
            SizeModeWidth = SizeMode.Small;
            SizeModeHeight = SizeMode.Small;
        }
        #endregion

        #region Properties
        new public Size Size { get { return base.Size; } }

        [Category("Page Option"), DefaultValue(DockMode.BottomCenter)]
        public DockMode DockMode
        {
            get { return _dockMode; }
            set
            {
                _dockMode = value;
                InitLocation();
            }
        }

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
            int top = 20;
            int middel = (screen.WorkingArea.Height / 2) - (Height / 2);
            int bottom = screen.WorkingArea.Height - Height - 20;
            int left = 20;
            int center = (screen.WorkingArea.Width / 2) - (Width / 2);
            int right = screen.WorkingArea.Right - Width - 20;
            switch (DockMode)
            {
                case DockMode.TopLeft:
                    Left = left;
                    Top = top;
                    break;
                case DockMode.TopCenter:
                    Left = center;
                    Top = top;
                    break;
                case DockMode.TopRight:
                    Left = right;
                    Top = top;
                    break;

                case DockMode.MiddleLeft:
                    Left = left;
                    Top = middel;
                    break;
                case DockMode.MiddleCenter:
                    Left = center;
                    Top = middel;
                    break;
                case DockMode.MiddleRight:
                    Left = right;
                    Top = middel;
                    break;

                case DockMode.BottomLeft:
                    Left = left;
                    Top = bottom;
                    break;
                case DockMode.BottomCenter:
                    Left = center;
                    Top = bottom;
                    break;
                case DockMode.BottomRight:
                    Left = right;
                    Top = bottom;
                    break;
            }
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
        public void ResetLayout()
        {
            DockMode = Setting.DockMode;
            SizeModeWidth = Setting.SizeModeWidth;
            SizeModeHeight = Setting.SizeModeHeight;

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
