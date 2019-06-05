﻿using System;
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

namespace GTControl
{
    public partial class PageItem : UserControl
    {
        public MouseEventHandler OnMouseDownEvent;

        private Image _backgroundImage;

        #region Constructor
        public PageItem()
        {
            InitializeComponent();

            Padding = new Padding(0);
            Margin = new Padding(0);
            Dock = DockStyle.Fill;

            BackgroundImage = null;
            TextContent = "Content";
            TextAlign = ContentAlignment.TopCenter;
            TextFont = label.Font;
            Cursor = Cursors.Hand;
        }
        #endregion

        #region Properties
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

        [Browsable(false)]
        [Category("Page Option")]
        public bool IsEditMode { get; set; }

        [Category("Page Option")]
        public int Column
        {
            get
            {
                var body = Parent as PageBody;
                if (body == null) return 0;

                return body.GetColumn(this);
            }
            set
            {
                var body = Parent as PageBody;
                if (body == null) return;

                body.SetColumn(this, value);
            }
        }

        [Category("Page Option")]
        public int Row
        {
            get
            {
                var body = Parent as PageBody;
                if (body == null) return 0;

                return body.GetRow(this);
            }
            set
            {
                var body = Parent as PageBody;
                if (body == null) return;

                body.SetRow(this, value);
            }
        }

        [Category("Page Option")]
        public int ColumnSpan
        {
            get
            {
                var body = Parent as PageBody;
                if (body == null) return 0;

                return body.GetColumnSpan(this);
            }
            set
            {
                if (value < 1) return;

                var body = Parent as PageBody;
                if (body == null) return;

                body.SetColumnSpan(this, value);
            }
        }

        [Category("Page Option")]
        public int RowSpan
        {
            get
            {
                var body = Parent as PageBody;
                if (body == null) return 0;

                return body.GetRowSpan(this);
            }
            set
            {
                if (value < 1) return;

                var body = Parent as PageBody;
                if (body == null) return;

                body.SetRowSpan(this, value);
            }
        }
        #endregion

        #region Control Event
        private void PageItem_Paint(object sender, PaintEventArgs e)
        {
            if (IsEditMode)
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
        #endregion
    }
}
