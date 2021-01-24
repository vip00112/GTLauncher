using GTCapture;
using GTControl;
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

namespace GTLauncher
{
    /// <summary>
    /// GTControl.PageContainer에서 모든 Control을 불러오도록 처리한다.
    /// Form에 GTControl.PageContainer를 상속받으면 자동 처리된다.
    /// </summary>
    public partial class MainForm : PageContainer
    {
        private Capture _capture;

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Control Event
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _capture = new Capture(Handle);
            _capture.OnCaptured += OnCaptured;
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Activate();
        }

        private void menuItem_exit_Click(object sender, EventArgs e)
        {
            if (!MessageBoxUtil.Confirm("Are you sure you want to close?")) return;

            Close();
        }

        private void menuItem_captureFolder_Click(object sender, EventArgs e)
        {
            string dirPath = _capture.GetSaveFolderPath();
            if (string.IsNullOrWhiteSpace(dirPath)) return;

            System.Diagnostics.Process.Start(dirPath);
        }
        #endregion

        #region GTCapture.Capture Event Handler
        private void OnCaptured(object sender, EventArgs e)
        {
            notifyIcon.BalloonTipText = "Capture completed.";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(500);
        }
        #endregion
    }
}
