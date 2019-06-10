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
        private bool _visibleHeader;
        private bool _visibleBackButton;
        private PageCloseMode _cloeMode;

        #region Constuctor
        public Page()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Dock = DockStyle.Fill;

            Title = "Title";
            VisibleHeader = true;
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
                    pageButton_option.Visible = true;
                    VisibleHeader = true;
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
        public bool VisibleHeader
        {
            get { return _visibleHeader; }
            set
            {
                if (PageName == "Main") return;

                _visibleHeader = value;
                pageHeader.Visible = value;
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
        public PageHeader PageHeader { get { return pageHeader; } }

        [Browsable(false)]
        public PageBody PageBody { get { return pageBody; } }

        [Browsable(false)]
        public List<PageItem> PageItems
        {
            get
            {
                var items = new List<PageItem>();
                foreach (Control control in pageBody.Controls)
                {
                    var item = control as PageItem;
                    if (item == null) continue;

                    items.Add(item);
                }
                return items;
            }
        }
        #endregion

        #region Control Event
        private void pageButton_back_Click(object sender, EventArgs e)
        {
            if (Setting.IsEditMode) return;

            switch (CloseMode)
            {
                case PageCloseMode.Hide:
                    Hide();
                    if (OnHidden != null) OnHidden(this, EventArgs.Empty);
                    break;
                case PageCloseMode.Dispose:
                    if (!MessageBoxUtil.Confirm("Are you sure you want to close?")) return;

                    if (Parent != null)
                    {
                        Parent.Controls.Remove(this);
                    }
                    Dispose();
                    if (OnDisposed != null) OnDisposed(this, EventArgs.Empty);
                    break;
            }
        }

        private void pageButton_option_Click(object sender, EventArgs e)
        {
            if (Setting.IsEditMode) return;

            using (var dialog = new SettingForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
            }
        }
        #endregion

        #region Public Method
        public PageItem AddItem(PageItem pageItem)
        {
            if (pageBody == null) return null;

            var item = new PageItem();
            try
            {
                item.PageName = pageItem.PageName;
                item.BackgroundImage = pageItem.BackgroundImage;
                item.TextContent = pageItem.TextContent;
                item.TextAlign = pageItem.TextAlign;
                item.TextFont = pageItem.TextFont;
                item.ClickMode = pageItem.ClickMode;
                item.FilePath = pageItem.FilePath;
                item.Arguments = pageItem.Arguments;
                item.LinkPageName = pageItem.LinkPageName;

                pageBody.Controls.Add(item);
                item.Column = pageItem.Column;
                item.Row = pageItem.Row;
                item.ColumnSpan = pageItem.ColumnSpan;
                item.RowSpan = pageItem.RowSpan;
                return item;
            }
            catch
            {
                pageBody.Controls.Remove(item);
                item.Dispose();
            }
            return null;
        }

        public void RemoveItem(PageItem item)
        {
            pageBody.Controls.Remove(item);
            item.Dispose();
        }
        #endregion
    }
}
