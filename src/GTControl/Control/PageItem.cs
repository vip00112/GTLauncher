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

        #region Constructor
        public PageItem()
        {
            InitializeComponent();

            Padding = new Padding(0);
            Margin = new Padding(0);

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
            set { Left = value; }
        }

        [Category("Page Option")]
        public int Y
        {
            get { return Top; }
            set { Top = value; }
        }

        [Category("Page Option")]
        new public int Width
        {
            get { return base.Width; }
            set { base.Width = value; }
        }

        [Category("Page Option")]
        new public int Height
        {
            get { return base.Height; }
            set { base.Height = value; }
        }

        public Control WrapperControl { get { return label; } }
        #endregion

        #region Control Event
        private void PageItem_Paint(object sender, PaintEventArgs e)
        {
            if (Setting.IsEditMode)
            {
                using (var b = new SolidBrush(Color.FromArgb(50, Color.Blue)))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(0, 0, base.Width, base.Height));
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
    }
}
