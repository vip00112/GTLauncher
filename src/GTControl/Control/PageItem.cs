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

namespace GTControl
{
    public partial class PageItem : UserControl
    {
        public MouseEventHandler OnMouseDownEvent;
        public PaintEventHandler OnPaintEvent;
        public EventHandler OnFolderClickEvent;

        private string _pageName;
        private Image _backgroundImage;
        private int _column;
        private int _row;
        private int _columnSpan;
        private int _rowSpan;

        #region Constructor
        public PageItem()
        {
            InitializeComponent();

            Padding = new Padding(0);
            Margin = new Padding(0);
            Dock = DockStyle.Fill;

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
        public int Column
        {
            get { return _column; }
            set
            {
                if (value + ColumnSpan > 10) return;
                _column = value;

                var body = Parent as PageBody;
                if (body != null) body.SetColumn(this, value);
            }
        }

        [Category("Page Option")]
        public int Row
        {
            get { return _row; }
            set
            {
                if (value + RowSpan > 10) return;
                _row = value;

                var body = Parent as PageBody;
                if (body != null) body.SetRow(this, value);
            }
        }

        [Category("Page Option")]
        public int ColumnSpan
        {
            get { return _columnSpan; }
            set
            {
                if (value < 1) return;
                _columnSpan = value;

                var body = Parent as PageBody;
                if (body != null) body.SetColumnSpan(this, value);
            }
        }

        [Category("Page Option")]
        public int RowSpan
        {
            get { return _rowSpan; }
            set
            {
                if (value < 1) return;
                _rowSpan = value;

                var body = Parent as PageBody;
                if (body != null) body.SetRowSpan(this, value);
            }
        }

        [Category("Page Option")]
        public int ItemWidth { get { return Width; } }

        [Category("Page Option")]
        public int ItemHeight { get { return Height; } }
        #endregion

        #region Control Event
        private void PageItem_Paint(object sender, PaintEventArgs e)
        {
            if (Setting.IsEditMode)
            {
                using (var b = new SolidBrush(Color.FromArgb(50, Color.Blue)))
                {
                    e.Graphics.FillRectangle(b, new Rectangle(0, 0, Width, Height));
                }
            }
            if (BackgroundImage != null)
            {
                e.Graphics.DrawImage(BackgroundImage, 0, 0, Width, Height);
            }
            if (OnPaintEvent != null) OnPaintEvent(this, e);
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
            if (OnMouseDownEvent != null) OnMouseDownEvent(this, e);
        }

        private void label_Click(object sender, EventArgs e)
        {
            if (Setting.IsEditMode) return;

            switch (ClickMode)
            {
                case ClickMode.Excute:
                    if (string.IsNullOrWhiteSpace(FilePath)) return;
                    
                    try
                    {
                        Process.Start(FilePath, Arguments);
                    }
                    catch (Exception ex)
                    {
                        MessageBoxUtil.Error(ex.Message);
                    }
                    break;
                case ClickMode.Folder:
                    if (OnFolderClickEvent != null) OnFolderClickEvent(this, EventArgs.Empty);
                    break;
            }
        }
        #endregion
    }
}
