using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTUtil;

namespace GTControl
{
    public class PageContainer : Form
    {
        private DockMode _dockMode;
        private SizeMode _sizeModeWidth;
        private SizeMode _sizeModeHeight;
        private List<Page> _pages;

        #region Constuctor
        public PageContainer()
        {
            DoubleBuffered = true;

            FormBorderStyle = FormBorderStyle.None;
            MinimizeBox = false;
            MaximizeBox = false;

            _pages = new List<Page>();

            DockMode = DockMode.BottomCenter;
            SizeModeWidth = SizeMode.Small;
            SizeModeHeight = SizeMode.Small;

            WinAPI.SetFormShadow(Handle);
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
                BuildLocation();
            }
        }

        [Category("Page Option"), DefaultValue(SizeMode.Small)]
        public SizeMode SizeModeWidth
        {
            get { return _sizeModeWidth; }
            set
            {
                _sizeModeWidth = value;
                Width = LayoutSetting.GetWidth(value);
                BuildLocation();
            }
        }

        [Category("Page Option"), DefaultValue(SizeMode.Small)]
        public SizeMode SizeModeHeight
        {
            get { return _sizeModeHeight; }
            set
            {
                _sizeModeHeight = value;
                Height = LayoutSetting.GetHeight(value);
                BuildLocation();
            }
        }
        #endregion

        #region Event Handler
        private void PageClose(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void PageItemFolderClick(object sender, EventArgs e)
        {
            var item = sender as PageItem;

            string pageName = item.LinkPageName;
            if (string.IsNullOrWhiteSpace(pageName)) return;

            var linkPage = _pages.FirstOrDefault(o => o.PageName == pageName);
            if (linkPage == null) return;

            linkPage.Show();
            linkPage.BringToFront();
        }
        #endregion

        #region Protected Method
        protected override void OnControlAdded(ControlEventArgs e)
        {
            base.OnControlAdded(e);

            if (Runtime.DesignMode) return;

            var page = e.Control as Page;
            if (page == null) return;

            _pages.Add(page);
            if (page.CloseMode == PageCloseMode.Dispose)
            {
                page.OnDisposed += PageClose;
            }
        }

        protected override void OnControlRemoved(ControlEventArgs e)
        {
            base.OnControlRemoved(e);

            if (Runtime.DesignMode) return;

            var page = e.Control as Page;
            if (page == null) return;

            var items = page.PageItems.ToList();
            foreach (var item in items)
            {
                item.OnFolderClickEvent -= PageItemFolderClick;
                page.RemoveItem(item);
            }

            _pages.Remove(page);
            if (page.CloseMode == PageCloseMode.Dispose)
            {
                page.OnDisposed -= PageClose;
            }
            page.Dispose();
        }
        #endregion

        #region Public Method
        public void BuildLayout()
        {
            DockMode = LayoutSetting.DockMode;
            SizeModeWidth = LayoutSetting.SizeModeWidth;
            SizeModeHeight = LayoutSetting.SizeModeHeight;

            // 기존 페이지 삭제
            var pages = _pages.ToList();
            foreach (var page in pages)
            {
                Controls.Remove(page);
            }

            // 페이지 등록
            foreach (var srcPage in LayoutSetting.Pages)
            {
                var page = new Page(srcPage.PageName);
                page.Title = srcPage.Title;
                page.VisibleTitle = srcPage.VisibleTitle;
                page.VisibleBackButton = srcPage.VisibleBackButton;
                page.CloseMode = srcPage.CloseMode;

                // 해당 페이지의 아이템 등록
                var srcPageItems = LayoutSetting.PageItems.Where(o => o.PageName == srcPage.PageName).ToList();
                foreach (var srcItem in srcPageItems)
                {
                    var item = page.AddItemAfterCopy(srcItem);
                    if (item == null) continue;

                    item.OnFolderClickEvent += PageItemFolderClick;
                }

                Controls.Add(page);
            }

            var main = _pages.FirstOrDefault(o => o.PageName == "Main");
            if (main != null) main.BringToFront();

            LayoutSetting.Invalidate(this);
        }
        #endregion

        #region Private Method
        private void BuildLocation()
        {
            if (Runtime.DesignMode) return;

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
        #endregion
    }
}