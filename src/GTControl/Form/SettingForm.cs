using GTCapture;
using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public partial class SettingForm : Form
    {
        private DockMode _dockMode;
        private SizeMode _sizeModeWidth;
        private SizeMode _sizeModeHeight;
        private List<Page> _pages;
        private List<PageItem> _pageItems;

        #region Constructor
        internal SettingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Control Event
        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _dockMode = Setting.DockMode;
            _sizeModeWidth = Setting.SizeModeWidth;
            _sizeModeHeight = Setting.SizeModeHeight;
            _pages = Setting.Pages.ToList();
            _pageItems = Setting.PageItems.ToList();
            checkBox_runOnStartup.Checked = Setting.RunOnStartup;
            checkBox_canMove.Checked = Setting.CanMove;
            comboBox_theme.DataSource = Enum.GetValues(typeof(Theme));
            comboBox_theme.SelectedItem = Setting.Theme;

            // Hide TabControl Header
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;

            foreach (ListViewItem li in listView.Items)
            {
                if (li.Text == "General")
                {
                    li.Tag = tabPage_general;
                }
                else if (li.Text == "Layout")
                {
                    li.Tag = tabPage_layout;
                }
                else if (li.Text == "Capture")
                {
                    li.Tag = tabPage_capture;
                }
            }

            listView.Items[0].Focused = true;
            listView.Items[0].Selected = true;
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.FocusedItem == null) return;

            tabControl.SelectedTab = (TabPage) listView.FocusedItem.Tag;
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item.Focused)
            {
                e.Item.BackColor = Color.LightSkyBlue;
            }
            else
            {
                e.Item.BackColor = Color.White;
            }

            e.DrawBackground();
            e.DrawText();
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!MessageBoxUtil.Confirm("Are you sure you want to save setting?")) return;

            Setting.RunOnStartup = checkBox_runOnStartup.Checked;
            Setting.CanMove = checkBox_canMove.Checked;
            Setting.Theme = (Theme) comboBox_theme.SelectedItem;
            Setting.Save();

            DialogResult = DialogResult.OK;
        }

        private void button_layout_Click(object sender, EventArgs e)
        {
            using (var dialog = new LayoutSettingForm(_dockMode, _sizeModeWidth, _sizeModeHeight, _pages, _pageItems))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                if (dialog.PageItems == null) return;

                _dockMode = dialog.DockMode;
                _sizeModeWidth = dialog.SizeModeWidth;
                _sizeModeHeight = dialog.SizeModeHeight;
                _pages = dialog.Pages;
                _pageItems = dialog.PageItems;

                Setting.DockMode = _dockMode;
                Setting.SizeModeWidth = _sizeModeWidth;
                Setting.SizeModeHeight = _sizeModeHeight;
                Setting.Pages = _pages;
                Setting.PageItems = _pageItems;
                Setting.Save();
            }
        }

        private void button_capture_Click(object sender, EventArgs e)
        {
            var capture = new Capture(Handle);
            capture.ShowSettingForm();
        }
        #endregion
    }
}
