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
    }
}
