using GoodbyeDPI;
using GTCapture;
using GTControl;
using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTLauncher
{
    /// <summary>
    /// GTControl.PageContainer의 BuildLayout 함수에서 모든 PageControl을 셋팅한다.
    /// LayoutSetting의 데이터를 기준으로 UI가 생성된다.
    /// </summary>
    public partial class MainForm : PageContainer
    {
        private Capture _capture;
        private Manager _goodbyedpiManager;

        #region Constructor
        public MainForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Control Event
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (Runtime.DesignMode) return;

            _capture = new Capture(Handle);
            _capture.OnCaptured += OnCaptured;

            _goodbyedpiManager = new Manager();

            GeneralSetting.Load();
            LayoutSetting.Load();
            CaptureSetting.Load();
            
            BuildLayout();
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_goodbyedpiManager != null) _goodbyedpiManager.Kill();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Activate();
        }

        private void menuItem_setting_Click(object sender, EventArgs e)
        {
            if (FormUtil.FindForm<SettingDialog>() != null) return;

            using (var dialog = new SettingDialog())
            {
                dialog.ShowDialog(this);

                if (dialog.IsSavedLayout) BuildLayout();
            }
        }

        private void menuItem_exit_Click(object sender, EventArgs e)
        {
            if (!MessageBoxUtil.Confirm("Are you sure want to close?")) return;

            Close();
        }

        private void menuItem_captureFolder_Click(object sender, EventArgs e)
        {
            string dirPath = CaptureSetting.CaptureSaveDirectory;
            if (string.IsNullOrWhiteSpace(dirPath)) return;

            Process.Start(dirPath);
        }

        private void menuItem_recordFolder_Click(object sender, EventArgs e)
        {
            string dirPath = CaptureSetting.RecordSaveDirectory;
            if (string.IsNullOrWhiteSpace(dirPath)) return;

            Process.Start(dirPath);
        }

        private void menuItem_goodbyeDPI_Click(object sender, EventArgs e)
        {
            if (_goodbyedpiManager == null) return;

            if (Process.GetProcessesByName("goodbyedpi").Length > 0)
            {
                string msg = "'GoodbyeDPI' is already started.\r\nAre you sure want to close?";
                if (!MessageBoxUtil.Confirm(msg)) return;

                _goodbyedpiManager.Kill();
            }
            else
            {
                _goodbyedpiManager.Start();
            }
        }
        #endregion

        #region Event Handler
        private void OnCaptured(object sender, EventArgs e)
        {
            notifyIcon.BalloonTipText = "Capture completed.";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(500);
        }
        #endregion
    }
}