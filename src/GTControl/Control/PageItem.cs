using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Windows.Forms.Design;
using System.Collections;
using System.Diagnostics;
using GTUtil;
using System.IO;
using System.Text.RegularExpressions;

namespace GTControl
{
    public partial class PageItem : UserControl
    {
        public MouseEventHandler OnMouseDownEvent;
        public PaintEventHandler OnPaintEvent;
        public EventHandler OnFolderClickEvent;

        private string _pageName;
        private Image _backgroundImage;
        private bool _isLoaded;

        #region Constructor
        public PageItem()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Padding = new Padding(0);
            Margin = new Padding(0);

            int dotSize = PageBody.DotSize;
            Width = dotSize * 2;
            Height = dotSize * 2;
            BackgroundImage = null;
            TextContent = "Content";
            TextAlign = ContentAlignment.MiddleCenter;
            TextFont = label.Font;
            Cursor = Cursors.Hand;
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
            get { return _backgroundImage; }
            set
            {
                _backgroundImage = value;
                Invalidate();
            }
        }

        [Category("Page Option"), DefaultValue("Content")]
        public string TextContent
        {
            get { return label.Text; }
            set { label.Text = value; }
        }

        [Category("Page Option"), DefaultValue(ContentAlignment.TopCenter)]
        public ContentAlignment TextAlign
        {
            get { return label.TextAlign; }
            set { label.TextAlign = value; }
        }

        [Category("Page Option")]
        public Font TextFont
        {
            get { return label.Font; }
            set { label.Font = value; }
        }

        [Category("Page Option")]
        public ClickMode ClickMode { get; set; }

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
                if (_isLoaded && Setting.IsEditMode)
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
                if (_isLoaded && Setting.IsEditMode)
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
        new public int Width
        {
            get { return base.Width; }
            set
            {
                if (_isLoaded && Setting.IsEditMode)
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
        new public int Height
        {
            get { return base.Height; }
            set
            {
                if (_isLoaded && Setting.IsEditMode)
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

        public Point MinPoint { get; private set; }

        public Point MaxPoint { get; private set; }

        public Size MinSize { get; private set; }

        public Size MaxSize { get; private set; }

        public Control WrapperControl { get { return label; } }
        #endregion

        #region Control Event
        private void PageItem_Load(object sender, EventArgs e)
        {
            if (!Setting.IsEditMode) return;

            CalcLimit();
            _isLoaded = true;
        }

        private void PageItem_Paint(object sender, PaintEventArgs e)
        {
            if (Setting.IsEditMode)
            {
                using (var b = new SolidBrush(Color.FromArgb(50, Color.Blue)))
                using (var p = new Pen(Color.Blue, 2))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(0, 0, base.Width, base.Height));
                    e.Graphics.DrawRectangle(p, new Rectangle(0, 0, base.Width, base.Height));
                }
            }
            if (BackgroundImage != null)
            {
                e.Graphics.DrawImage(BackgroundImage, 0, 0, base.Width, base.Height);
            }
            OnPaintEvent?.Invoke(this, e);
        }

        private void label_MouseEnter(object sender, EventArgs e)
        {
            label.BackColor = Color.FromArgb(50, Setting.GetBackColorHover(Setting.Theme));
        }

        private void label_MouseLeave(object sender, EventArgs e)
        {
            label.BackColor = Color.Transparent;
        }

        private void label_MouseDown(object sender, MouseEventArgs e)
        {
            OnMouseDownEvent?.Invoke(this, e);
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (Setting.IsEditMode) return;

            switch (ClickMode)
            {
                case ClickMode.Excute:
                    if (string.IsNullOrWhiteSpace(FilePath)) return;

                    StartProcess();
                    break;
                case ClickMode.Folder:
                    OnFolderClickEvent.Invoke(this, EventArgs.Empty);
                    break;
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
        #endregion
    }
}
