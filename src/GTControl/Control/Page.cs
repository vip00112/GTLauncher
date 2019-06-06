using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public partial class Page : UserControl
    {
        public EventHandler OnHidden;
        public EventHandler OnDisposed;

        private bool _visibleHeader;
        private bool _visibleBackButton;
        private bool _visibleOptionButton;
        private PageCloseMode _cloeMode;

        #region Constuctor
        public Page()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Dock = DockStyle.Fill;

            PageName = "Main";
            VisibleHeader = true;
            VisibleBackButton = true;
            VisibleOptionButton = false;
            CloseMode = PageCloseMode.Hide;
        }
        #endregion

        #region Properties
        new public Color ForeColor { get; }

        new public Color BackColor { get; }

        public string PageName { get; set; }

        [Category("Page Option"), DefaultValue(true)]
        public bool VisibleHeader
        {
            get { return _visibleHeader; }
            set
            {
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
                _visibleBackButton = value;
                pageButton_back.Visible = value;
            }
        }

        [Category("Page Option"), DefaultValue(false)]
        public bool VisibleOptionButton
        {
            get { return _visibleOptionButton; }
            set
            {
                _visibleOptionButton = value;
                pageButton_option.Visible = value;
            }
        }

        [Category("Page Option")]
        public PageCloseMode CloseMode
        {
            get { return _cloeMode; }
            set
            {
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
            if (pageBody.IsEditMode) return;

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
            if (pageBody.IsEditMode) return;

            using (var dialog = new SettingForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                dialog.SaveSetting();
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
                item.IsEditMode = pageItem.IsEditMode;

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
