using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTUtil;

namespace GTControl
{
    public partial class Page : UserControl
    {
        public EventHandler OnHidden;
        public EventHandler OnDisposed;

        private string _pageName;
        private string _title;
        private bool _visibleTitle;
        private bool _visibleBackButton;
        private PageCloseMode _cloeMode;

        #region Constuctor
        public Page()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Dock = DockStyle.Fill;

            Title = "Title";
            VisibleBackButton = true;
            CloseMode = PageCloseMode.Hide;
        }

        public Page(string pageName) : this()
        {
            PageName = pageName;
        }
        #endregion

        #region Properties
        new public Color ForeColor { get; }

        new public Color BackColor { get; }

        [Category("Page Option"), DefaultValue("Main")]
        public string PageName
        {
            get { return _pageName; }
            set
            {
                if (string.IsNullOrWhiteSpace(value)) return;

                // Main 페이지는 변경 불가
                if (_pageName == "Main") return;

                // 지정된 이름이 있는데 Main 페이지로 변경 불가
                if (!string.IsNullOrWhiteSpace(_pageName) && value == "Main") return;

                if (value == "Main")
                {
                    VisibleBackButton = true;
                    CloseMode = PageCloseMode.Dispose;
                }
                _pageName = value;
            }
        }

        [Category("Page Option"), DefaultValue("Title")]
        public string Title
        {
            get { return _title; }
            set
            {
                _title = value;
                label_title.Text = value;
            }
        }

        [Category("Page Option"), DefaultValue(true)]
        public bool VisibleTitle
        {
            get { return _visibleTitle; }
            set
            {
                _visibleTitle = value;
                label_title.Visible = value;
            }
        }

        [Category("Page Option"), DefaultValue(true)]
        public bool VisibleBackButton
        {
            get { return _visibleBackButton; }
            set
            {
                if (PageName == "Main") return;

                _visibleBackButton = value;
                pageButton_back.Visible = value;
            }
        }

        [Category("Page Option")]
        public PageCloseMode CloseMode
        {
            get { return _cloeMode; }
            set
            {
                if (PageName == "Main") return;

                _cloeMode = value;
                switch (_cloeMode)
                {
                    case PageCloseMode.Hide:
                        pageButton_back.Text = "◀";
                        break;
                    case PageCloseMode.Dispose:
                        pageButton_back.Text = "ⓧ";
                        break;
                }
            }
        }

        [Browsable(false)]
        public PageBody PageBody { get { return pageBody; } }

        [Browsable(false)]
        public List<PageItem> PageItems { get { return pageBody.Items; } }
        #endregion

        #region Control Event
        private void pageButton_back_Click(object sender, EventArgs e)
        {
            if (LayoutSetting.IsEditMode) return;

            switch (CloseMode)
            {
                case PageCloseMode.Hide:
                    Hide();
                    OnHidden?.Invoke(this, EventArgs.Empty);
                    break;
                case PageCloseMode.Dispose:
                    if (!MessageBoxUtil.Confirm("Are you sure want to close?")) return;

                    if (Parent != null)
                    {
                        Parent.Controls.Remove(this);
                    }
                    Dispose();
                    OnDisposed?.Invoke(this, EventArgs.Empty);
                    break;
            }
        }
        #endregion

        #region Public Method
        public PageItem AddItemAfterCopy(PageItem src)
        {
            var copy = src.Copy();
            if (copy == null) return null;

            pageBody.AddItem(copy);
            return copy;
        }

        public void RemoveItem(PageItem item)
        {
            pageBody.RemoveItem(item);
        }
        #endregion
    }
}