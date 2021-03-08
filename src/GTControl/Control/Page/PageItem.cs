using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.Design;
using System.Diagnostics;
using System.Drawing;
using System.Drawing.Design;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class PageItem : Panel
    {
        private const int LineSize = 4;

        public MouseEventHandler OnMouseDownEventForEdit;
        public EventHandler OnFolderClickEvent;

        private string _pageName;
        private Image _img;
        private string _text;
        private ContentAlignment _textAlignment;
        private Font _textFont;
        private bool _isLoaded;
        private bool _isSelected;
        private Point _startLoc;
        private Point _resizeDiffLoc;
        private bool _isMouseOver;
        private bool _disposed;

        #region Constructor
        public PageItem()
        {
            DoubleBuffered = true;

            Padding = new Padding(0);
            Margin = new Padding(0);

            int dotSize = PageBody.DotSize;
            Width = dotSize * 2;
            Height = dotSize * 2;
            BackgroundImage = null;
            TextContent = "Content";
            TextAlign = ContentAlignment.MiddleCenter;
            TextFont = Font;
            Cursor = Cursors.Hand;
        }

        ~PageItem()
        {
            Dispose(false);
        }
        #endregion

        #region Properties
        [Category("Page Option")]
        public string PageName
        {
            get { return _pageName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                var pageBody = Parent as PageBody;
                if (pageBody != null)
                {
                    if (pageBody.PageName != value) return;
                }

                _pageName = value;
            }
        }

        [Category("Page Option")]
        new public Image BackgroundImage
        {
            get { return _img; }
            set
            {
                if (_img != null)
                {
                    ImageAnimator.StopAnimate(_img, OnFrameChangedHandler);
                    _img.Dispose();
                }

                _img = value;
                if (ImageAnimator.CanAnimate(_img))
                {
                    ImageAnimator.Animate(_img, OnFrameChangedHandler);
                }
                Invalidate();
            }
        }

        [Category("Page Option"), DefaultValue("Content")]
        [Editor(typeof(MultilineStringEditor), typeof(UITypeEditor))]
        public string TextContent
        {
            get { return _text; }
            set
            {
                _text = value;
                Invalidate();
            }
        }

        [Category("Page Option"), DefaultValue(ContentAlignment.TopCenter)]
        public ContentAlignment TextAlign
        {
            get { return _textAlignment; }
            set
            {
                _textAlignment = value;
                Invalidate();
            }
        }

        [Category("Page Option")]
        public Font TextFont
        {
            get { return _textFont; }
            set
            {
                _textFont = value;
                Invalidate();
            }
        }

        [Category("Page Option")]
        public ClickMode ClickMode { get; set; }

        [Category("Page Option")]
        public bool StartWithAdministrator { get; set; }

        /// <summary>
        /// 실행 파일 경로
        /// </summary>
        [Category("Page Option")]
        public string FilePath { get; set; }

        /// <summary>
        /// 파일 실행시 넘길 커맨드 라인
        /// </summary>
        [Category("Page Option")]
        public string Arguments { get; set; }

        /// <summary>
        /// ClickMode가 Folder일때 보여줄 PageName
        /// </summary>
        [Category("Page Option")]
        public string LinkPageName { get; set; }

        [Category("Page Option")]
        public int X
        {
            get { return Left; }
            set
            {
                if (_isLoaded && LayoutSetting.IsEditMode)
                {
                    if (value % PageBody.Grid != 0) return;
                    if (value < MinPoint.X) value = MinPoint.X;
                    else if (value > MaxPoint.X) value = MaxPoint.X;
                    Left = value;
                    CalcLimit();
                }
                else
                {
                    Left = value;
                }
            }
        }

        [Category("Page Option")]
        public int Y
        {
            get { return Top; }
            set
            {
                if (_isLoaded && LayoutSetting.IsEditMode)
                {
                    if (value % PageBody.Grid != 0) return;
                    if (value < MinPoint.Y) value = MinPoint.Y;
                    else if (value > MaxPoint.Y) value = MaxPoint.Y;
                    Top = value;
                    CalcLimit();
                }
                else
                {
                    Top = value;
                }
            }
        }

        [Category("Page Option")]
        [Browsable(true)] // Control.Width의 Browsable이 false이므로, true로 재정의 해야함
        new public int Width
        {
            get { return base.Width; }
            set
            {
                if (_isLoaded && LayoutSetting.IsEditMode)
                {
                    if (value % PageBody.Grid != 0) return;
                    if (value < MinSize.Width) value = MinSize.Width;
                    else if (value > MaxSize.Width) value = MaxSize.Width;
                    base.Width = value;
                    CalcLimit();
                }
                else
                {
                    base.Width = value;
                }
            }
        }

        [Category("Page Option")]
        [Browsable(true)] // Control.Width의 Browsable이 false이므로, true로 재정의 해야함
        new public int Height
        {
            get { return base.Height; }
            set
            {
                if (_isLoaded && LayoutSetting.IsEditMode)
                {
                    if (value % PageBody.Grid != 0) return;
                    if (value < MinSize.Height) value = MinSize.Height;
                    else if (value > MaxSize.Height) value = MaxSize.Height;
                    base.Height = value;
                    CalcLimit();
                }
                else
                {
                    base.Height = value;
                }
            }
        }

        public bool IsSelected
        {
            get { return _isSelected; }
            set
            {
                _isSelected = value;
                Invalidate();
            }
        }

        public Point MinPoint { get; private set; }

        public Point MaxPoint { get; private set; }

        public Size MinSize { get; private set; }

        public Size MaxSize { get; private set; }

        private Rectangle ResizeRec
        {
            get
            {
                int value = LineSize * 4;
                return new Rectangle(Width - value, Height - value, value, value); 
            }
        }        

        private Rectangle MoveRec
        {
            get
            {
                int value = LineSize * 2;
                int x = (Width / 2) - (value / 2);
                int y = (Height / 2) - (value / 2);
                return new Rectangle(0, 0, value, value);
            }
        }

        private MoveResizeAction MoveResizeAction { get; set; }
        #endregion

        #region Event Handler
        private void OnFrameChangedHandler(object sender, EventArgs e)
        {
            if (BackgroundImage != null) Invalidate();
        }
        #endregion

        #region Protected Method
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (LayoutSetting.IsEditMode)
            {
                CalcLimit();
                _isLoaded = true;
            }
        }

        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            _isMouseOver = true;
            Invalidate();
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            _isMouseOver = false;
            Invalidate();
        }

        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (!LayoutSetting.IsEditMode) return;

            _startLoc = e.Location;
            if (MoveRec.Contains(e.X, e.Y))
            {
                MoveResizeAction = MoveResizeAction.Move;
            }
            else if (ResizeRec.Contains(e.X, e.Y))
            {
                var rec = ResizeRec;
                int diffX = (rec.X + rec.Width) - _startLoc.X;
                int diffY = (rec.Y + rec.Height) - _startLoc.Y;
                _resizeDiffLoc = new Point(diffX, diffY);
                MoveResizeAction = MoveResizeAction.ResizeBottomRight;
            }
            else
            {
                MoveResizeAction = MoveResizeAction.None;
            }
            OnMouseDownEventForEdit?.Invoke(this, e);
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (!LayoutSetting.IsEditMode) return;

            switch (MoveResizeAction)
            {
                case MoveResizeAction.Move:
                    DoAction(e.Location);
                    break;
                case MoveResizeAction.ResizeBottomRight:
                    int currentX = e.X + _resizeDiffLoc.X;
                    int currentY = e.Y + _resizeDiffLoc.Y;
                    DoAction(new Point(currentX, currentY));
                    break;
                default:
                    ChangeCursor(e.X, e.Y);
                    break;
            }

            CalcLimit();
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (!LayoutSetting.IsEditMode) return;

            MoveResizeAction = MoveResizeAction.None;
        }

        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            if (LayoutSetting.IsEditMode) return;

            switch (ClickMode)
            {
                case ClickMode.Excute:
                    if (string.IsNullOrWhiteSpace(FilePath)) return;

                    StartProcess();
                    break;
                case ClickMode.Folder:
                    OnFolderClickEvent?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (BackgroundImage != null)
            {
                ImageAnimator.UpdateFrames(BackgroundImage);
                e.Graphics.DrawImage(BackgroundImage, 0, 0, Width, Height);
            }

            // 에디트 모드에서 이미지가 없을시 파란색 표기
            else if (LayoutSetting.IsEditMode)
            {
                using (var b = new SolidBrush(Color.FromArgb(50, Color.Blue)))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(0, 0, Width, Height));
                }
            }

            // Text 표기
            if (!string.IsNullOrWhiteSpace(TextContent))
            {
                var color = LayoutSetting.GetForeColorCommon(LayoutSetting.Theme);
                using (var b = new SolidBrush(color))
                {
                    e.Graphics.DrawString(TextContent, TextFont, b, ClientRectangle, GetStringFormat());
                }
            }

            // 마우스 오버 표기
            if (_isMouseOver)
            {
                var color = Color.FromArgb(50, LayoutSetting.GetBackColorHover(LayoutSetting.Theme));
                using (var b = new SolidBrush(color))
                {
                    e.Graphics.FillRectangle(b, 0, 0, Width, Height);
                }
            }

            // 에디트 모드에서 Resize, 선택시 빨간색 표기
            if (LayoutSetting.IsEditMode)
            {
                using (var b = new SolidBrush(Color.Red))
                using (var p = new Pen(Color.Red, 2))
                {
                    var bottom = ResizeRec;
                    Point[] locs = new Point[]
                    {
                        new Point(bottom.X, bottom.Y+bottom.Height),
                        new Point(bottom.X+bottom.Width, bottom.Y),
                        new Point(bottom.X+bottom.Width, bottom.Y+bottom.Height)
                    };
                    e.Graphics.FillPolygon(b, locs);
                    e.Graphics.FillRectangle(b, MoveRec);
                    e.Graphics.DrawRectangle(p, new Rectangle(0, 0, Width, Height));
                }

                if (IsSelected)
                {
                    using (var b = new SolidBrush(Color.FromArgb(100, Color.Red)))
                    {
                        e.Graphics.FillRectangle(b, 0, 0, Width, Height);
                    }
                }
            }
        }
        #endregion

        #region Private Method
        private void StartProcess()
        {
            try
            {
                // SpecialFolder 경로 취득
                var matches = Regex.Matches(FilePath, @"\{(.*?)\}");
                foreach (Match m in matches)
                {
                    string origin = m.Groups[0].ToString();

                    Environment.SpecialFolder folder;
                    if (!Enum.TryParse(m.Groups[1].ToString(), true, out folder)) continue;
                    string realPath = Environment.GetFolderPath(folder);
                    FilePath = FilePath.Replace(origin, realPath);
                }

                var si = new ProcessStartInfo();
                si.FileName = FilePath;

                // 네트워크 연결
                if (!FilePath.Contains("://"))
                {
                    if (StartWithAdministrator)
                    {
                        si.UseShellExecute = true;
                        si.Verb = "runas";
                    }
                    si.Arguments = Arguments;
                    si.WorkingDirectory = Path.GetDirectoryName(FilePath);
                }

                var proc = new Process();
                proc.StartInfo = si;
                proc.Start();
            }
            catch (Exception ex)
            {
                MessageBoxUtil.Error(ex.Message);
            }
        }

        private StringFormat GetStringFormat()
        {
            var sf = new StringFormat();
            switch (TextAlign)
            {
                case ContentAlignment.TopLeft:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.TopCenter:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.TopRight:
                    sf.LineAlignment = StringAlignment.Near;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.MiddleLeft:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.MiddleCenter:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.MiddleRight:
                    sf.LineAlignment = StringAlignment.Center;
                    sf.Alignment = StringAlignment.Far;
                    break;
                case ContentAlignment.BottomLeft:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Near;
                    break;
                case ContentAlignment.BottomCenter:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Center;
                    break;
                case ContentAlignment.BottomRight:
                    sf.LineAlignment = StringAlignment.Far;
                    sf.Alignment = StringAlignment.Far;
                    break;
            }
            return sf;
        }

        private void ChangeCursor(int x, int y)
        {
            if (MoveRec.Contains(x, y))
            {
                Cursor = Cursors.SizeAll;
            }
            else if (ResizeRec.Contains(x, y))
            {
                Cursor = Cursors.SizeNWSE;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void DoAction(Point currentLoc)
        {
            var bounds = ControlMoveResizeUtil.CalcBounds(MoveResizeAction, this, _startLoc, currentLoc);
            int diffX = bounds.X % PageBody.Grid;
            int diffY = bounds.Y % PageBody.Grid;
            int diffWidth = bounds.Width % PageBody.Grid;
            int diffHeight = bounds.Height % PageBody.Grid;

            switch (MoveResizeAction)
            {
                case MoveResizeAction.Move:
                    if (diffX != 0) bounds.X -= diffX;
                    if (diffY != 0) bounds.Y -= diffY;
                    break;
                case MoveResizeAction.ResizeLeft:
                    if (diffX != 0)
                    {
                        bounds.X -= diffX;
                        bounds.Width += diffX;
                    }
                    break;
                case MoveResizeAction.ResizeTop:
                    if (diffY != 0)
                    {
                        bounds.Y -= diffY;
                        bounds.Height += diffY;
                    }
                    break;
                case MoveResizeAction.ResizeRight:
                    if (diffWidth != 0) bounds.Width -= diffWidth;
                    break;
                case MoveResizeAction.ResizeBottom:
                    if (diffHeight != 0) bounds.Height -= diffHeight;
                    break;
                case MoveResizeAction.ResizeTopLeft:
                    if (diffY != 0)
                    {
                        bounds.Y -= diffY;
                        bounds.Height += diffY;
                    }
                    if (diffX != 0)
                    {
                        bounds.X -= diffX;
                        bounds.Width += diffX;
                    }
                    break;
                case MoveResizeAction.ResizeTopRight:
                    if (diffY != 0)
                    {
                        bounds.Y -= diffY;
                        bounds.Height += diffY;
                    }
                    if (diffWidth != 0) bounds.Width -= diffWidth;
                    break;
                case MoveResizeAction.ResizeBottomRight:
                    if (diffHeight != 0) bounds.Height -= diffHeight;
                    if (diffWidth != 0) bounds.Width -= diffWidth;
                    break;
                case MoveResizeAction.ResizeBottomLeft:
                    if (diffHeight != 0) bounds.Height -= diffHeight;
                    if (diffX != 0)
                    {
                        bounds.X -= diffX;
                        bounds.Width += diffX;
                    }
                    break;
            }

            if (bounds.X < MinPoint.X) bounds.X = MinPoint.X;
            else if (bounds.X > MaxPoint.X) bounds.X = MaxPoint.X;

            if (bounds.Y < MinPoint.Y) bounds.Y = MinPoint.Y;
            else if (bounds.Y > MaxPoint.Y) bounds.Y = MaxPoint.Y;

            if (bounds.Width < MinSize.Width) bounds.Width = MinSize.Width;
            else if (bounds.Width > MaxSize.Width) bounds.Width = MaxSize.Width;

            if (bounds.Height < MinSize.Height) bounds.Height = MinSize.Height;
            else if (bounds.Height > MaxSize.Height) bounds.Height = MaxSize.Height;

            Bounds = bounds;
        }

        private Image CopyBackgroundImage()
        {
            using (var ms = new MemoryStream())
            {
                BackgroundImage.Save(ms, BackgroundImage.RawFormat);

                var data = ms.ToArray();
                return Image.FromStream(new MemoryStream(data, 0, data.Length), true);
            }
        }
        #endregion

        #region Public Method
        public void CalcLimit()
        {
            if (Parent != null)
            {
                int dotSize = PageBody.DotSize;
                MinPoint = new Point(Parent.ClientRectangle.Left, Parent.ClientRectangle.Top);
                MaxPoint = new Point(Parent.ClientRectangle.Right - Width, Parent.ClientRectangle.Bottom - Height);
                MinSize = new Size(dotSize * 2, dotSize * 2);
                MaxSize = new Size(Parent.ClientRectangle.Right - Left, Parent.ClientRectangle.Bottom - Top);
            }
        }

        public PageItem Copy()
        {
            var result = new PageItem();
            try
            {
                var categoryFilter = new string[] { "Page Option" };
                var ignorePropertyFilter = new string[] { "BackgroundImage" };
                ReflectionUtil.CopyProperties(this, result, categoryFilter, ignorePropertyFilter);
                if (BackgroundImage != null)
                {
                    result.BackgroundImage = CopyBackgroundImage();
                }
                return result;
            }
            catch
            {
                result.Dispose();
                return null;
            }
        }
        #endregion

        #region IDisposable Method
        new public void Dispose()
        {
            Dispose(true);
            GC.SuppressFinalize(this);
        }

        new protected virtual void Dispose(bool disposing)
        {
            if (_disposed) return;

            if (disposing)
            {
                if (BackgroundImage != null)
                {
                    BackgroundImage.Dispose();
                    BackgroundImage = null;
                }
            }

            _disposed = true;
        }
        #endregion
    }
}
