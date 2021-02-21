using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTUtil;

namespace GTControl
{
    public partial class DownloadDialog : Form
    {
        private DownloadDialog()
        {
            InitializeComponent();
        }

        public DownloadDialog(string url, string filePath) : this()
        {

        }

        private void DownloadDialog_Load(object sender, EventArgs e)
        {
            LayoutSetting.Invalidate(this);
        }

        private void DownloadDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!MessageBoxUtil.Confirm("Are you sure want to close?"))
            {
                e.Cancel = true;
                return;
            }
        }
    }
}
