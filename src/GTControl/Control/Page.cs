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

        #region Constuctor
        public Page()
        {
            InitializeComponent();
            DoubleBuffered = true;

            Dock = DockStyle.Fill;

            VisibleHeader = true;
            VisibleBackButton = true;
            VisibleOptionButton = false;
            CloseMode = PageCloseMode.Hide;
        }
        #endregion

        #region Properties
        new public Color ForeColor { get; }

        new public Color BackColor { get; }

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
        public PageCloseMode CloseMode { get; set; }
        #endregion

        #region Control Event
        private void pageButton_back_Click(object sender, EventArgs e)
        {
            switch (CloseMode)
            {
                case PageCloseMode.Hide:
                    Hide();
                    if (OnHidden != null) OnHidden(this, EventArgs.Empty);
                    break;
                case PageCloseMode.Dispose:
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
            using (var dialog = new SettingForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                dialog.SaveSetting();
            }
        }
        #endregion
    }
}
