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

            var form = new GTCapture.SettingForm();
            form.ShowDialog();

            _capture = new Capture(Handle);

            var modifier = KeyModifiers.Control | KeyModifiers.Shift;
            _capture.RegisterHotKey(CaptureMode.FullScreen, modifier, Keys.N);
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

        #region Protected Method
        protected override void WndProc(ref Message m)
        {
            if (_capture != null)
            {
                _capture.OnWndProc(ref m);
            }
            base.WndProc(ref m);
        }
        #endregion

    }
}
