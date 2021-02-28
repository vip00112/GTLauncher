using GTControl;
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
    public partial class AboutDialog : Form
    {
        public AboutDialog()
        {
            InitializeComponent();
        }

        private void AboutDialog_Load(object sender, EventArgs e)
        {
            if (Runtime.DesignMode) return;

            LayoutSetting.Invalidate(this);

            var version = Assembly.GetExecutingAssembly().GetName().Version;
            label_desc.Text = string.Format(label_desc.Text, version.ToString());
        }

        private void linkLabel_github_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            Process.Start("https://github.com/vip00112/GTLauncher");
        }
    }
}
