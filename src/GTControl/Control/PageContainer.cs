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
            InitSetting();

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
            }
        }
        #endregion

        #region Protected Method
        protected override void OnLoad(EventArgs e)
        {
            base.OnLoad(e);
            if (DesignMode) return;

            InitLocation();
            Setting.SetTheme(this, Setting.Theme);
        }

        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (DesignMode) return;

            var page = e.Control as Page;
            if (page == null) return;

            _pages.Add(page);
            Setting.SetTheme(page, Setting.Theme);
            SetMoveEvent(page);
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            if (DesignMode) return;

            var page = e.Control as Page;
            if (page == null) return;

            _pages.Remove(page);
        }
        #endregion

        #region Private Method
        private void InitSetting()
        {
            Setting.CanMove = true;
            Setting.Theme = Theme.Dark;
            Setting.SizeModeWidth = SizeMode.Medium;
            Setting.SizeModeHeight = SizeMode.Medium;
        }

        private void InitLocation()
        {
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
                if (child is PageHeader)
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
    }
}
