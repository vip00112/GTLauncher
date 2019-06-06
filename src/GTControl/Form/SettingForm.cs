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
        private List<PageItem> _pageItems;

        public SettingForm()
        {
            InitializeComponent();
        }

        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            checkBox_canMove.Checked = Setting.CanMove;

            comboBox_theme.DataSource = Enum.GetValues(typeof(Theme));
            comboBox_theme.SelectedItem = Setting.Theme;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            DialogResult = DialogResult.OK;
        }

        private void button_layout_Click(object sender, EventArgs e)
        {
            using (var dialog = new LayoutSettingForm(_pageItems))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                if (dialog.PageItems == null) return;

                _pageItems = dialog.PageItems;
            }
        }

        public void SaveSetting()
        {
            Setting.CanMove = checkBox_canMove.Checked;
            Setting.Theme = (Theme) comboBox_theme.SelectedItem;
            Setting.PageItems = _pageItems;

            Setting.Save();
        }
    }
}
