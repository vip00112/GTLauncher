using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTUtil;
using Microsoft.Win32;

namespace GTControl
{
    public class PageContainer : Form
    {
        private const int WM_DPICHANGED = 0x02E0;

        private DockMode _dockMode;
        private SizeMode _sizeModeWidth;
        private SizeMode _sizeModeHeight;
        private List<Page> _pages;

        private bool _displayHooked;
        private bool _isRebuilding;
        private float _appliedScale;
        private Rectangle _appliedPrimaryBounds;

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

                var scale = WinAPI.GetMonitorScale();
                Width = (int)(LayoutSetting.GetWidth(value) * scale);
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

                var scale = WinAPI.GetMonitorScale();
                Height = (int)(LayoutSetting.GetHeight(value) * scale);
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

            var otherPages = _pages.Where(o => o.PageName != "Main").ToList();
            otherPages.ForEach(o => o.Hide());

            linkPage.Show();
            linkPage.BringToFront();
        }
        #endregion

        #region Protected Method
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (Runtime.DesignMode) return;
            if (_displayHooked) return;

            // 모니터 연결/해제, 주 모니터 변경, 해상도/배율 변경에 반응한다.
            SystemEvents.DisplaySettingsChanged += OnDisplaySettingsChanged;
            _displayHooked = true;
        }

        protected override void OnHandleDestroyed(EventArgs e)
        {
            if (_displayHooked)
            {
                SystemEvents.DisplaySettingsChanged -= OnDisplaySettingsChanged;
                _displayHooked = false;
            }

            base.OnHandleDestroyed(e);
        }

        protected override void WndProc(ref Message m)
        {
            // PerMonitor-V2 인식 상태에서 창의 DPI가 바뀌면(모니터 이동/배율 변경) 전달된다.
            if (m.Msg == WM_DPICHANGED)
            {
                base.WndProc(ref m);
                RebuildForDisplayChange();
                return;
            }

            base.WndProc(ref m);
        }

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

            // 레이아웃 구성 시 모니터 배율을 1회 적용한다.
            LayoutSetting.Invalidate(this, true);

            // 현재 적용된 배율/주 모니터 상태를 기록해 디스플레이 변경 감지에 사용한다.
            var primary = Screen.AllScreens.FirstOrDefault(o => o.Primary) ?? Screen.AllScreens[0];
            _appliedPrimaryBounds = primary.Bounds;
            _appliedScale = WinAPI.GetMonitorScale();
        }
        #endregion

        #region Private Method
        private void OnDisplaySettingsChanged(object sender, EventArgs e)
        {
            if (IsDisposed) return;
            if (InvokeRequired)
            {
                try { BeginInvoke((MethodInvoker) RebuildForDisplayChange); }
                catch { /* 창이 닫히는 중이면 무시 */ }
                return;
            }
            RebuildForDisplayChange();
        }

        // 주 모니터 또는 배율이 실제로 바뀐 경우에만 레이아웃을 다시 구성한다(위치 재배치 + 재스케일).
        private void RebuildForDisplayChange()
        {
            if (Runtime.DesignMode) return;
            if (_isRebuilding) return;
            if (!IsHandleCreated || IsDisposed) return;

            var primary = Screen.AllScreens.FirstOrDefault(o => o.Primary) ?? Screen.AllScreens[0];
            float scale = WinAPI.GetMonitorScale();
            if (scale == _appliedScale && primary.Bounds == _appliedPrimaryBounds) return;

            _isRebuilding = true;
            try
            {
                BuildLayout();
            }
            finally
            {
                _isRebuilding = false;
            }
        }

        private void BuildLocation()
        {
            if (Runtime.DesignMode) return;

            var screen = Screen.AllScreens.FirstOrDefault(o => o.Primary);
            if (screen == null) screen = Screen.AllScreens[0];

            // 작업 영역(WorkingArea)의 시작 오프셋을 반영해야
            // 작업표시줄이 상단/좌측에 있어도 런처가 올바르게 배치된다.
            var wa = screen.WorkingArea;
            const int margin = 20;
            int top = wa.Top + margin;
            int middle = wa.Top + (wa.Height / 2) - (Height / 2);
            int bottom = wa.Bottom - Height - margin;
            int left = wa.Left + margin;
            int center = wa.Left + (wa.Width / 2) - (Width / 2);
            int right = wa.Right - Width - margin;
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
                    Top = middle;
                    break;
                case DockMode.MiddleCenter:
                    Left = center;
                    Top = middle;
                    break;
                case DockMode.MiddleRight:
                    Left = right;
                    Top = middle;
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