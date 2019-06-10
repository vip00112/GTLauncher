using GTCapture;
using GTControl;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTLauncher
{
    public partial class MainForm : PageContainer
    {
        private Capture _capture;

        public MainForm()
        {
            InitializeComponent();

            _capture = new Capture(Handle);
            _capture.ShowSettingForm();

        }

        #region Control Event
        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Activate();
        }

        private void menuItem_exit_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

    }
}
