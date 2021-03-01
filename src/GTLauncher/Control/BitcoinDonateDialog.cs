using GTControl;
using GTLocalization;
using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTLauncher
{
    public partial class BitcoinDonateDialog : Form
    {
        private const string Address = "33DW2z6crajNSX6zRCpScijHdEoaeh5Wap";

        #region Constructor
        public BitcoinDonateDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Control Event
        private void BitcoinDonateDialog_Load(object sender, EventArgs e)
        {
            if (Runtime.DesignMode) return;

            LayoutSetting.Invalidate(this);
            textBox_address.Text = Address;
        }

        private void BitcoinDonateDialog_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) DialogResult = DialogResult.Cancel;
        }

        private void button_copy_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(Address);
            MessageBoxUtil.Info(Resource.GetString(Key.BitcoinAddressCopiedMsg));
        }
        #endregion
    }
}