using GTCapture;
using GTControl;
using GTLocalization;
using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Reflection;
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

            GTCapture.Capture.Regist(Handle);
            GeneralSetting.Load();
            LayoutSetting.Load();
            CaptureSetting.Load();
            
            BuildLayout();

            // 업데이트 tmp 파일 복사
            var di = new DirectoryInfo(Application.StartupPath);
            var fis = di.GetFiles("*.update.tmp", SearchOption.AllDirectories);
            foreach (var fi in fis)
            {
                try
                {
                    string fileName = Path.GetFileName(fi.Name).Replace(".update.tmp", "");
                    string filePath = Path.Combine(Application.StartupPath, fileName);
                    fi.CopyTo(filePath, true);
                    fi.Delete();
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
            }
        }

        private async void MainForm_Shown(object sender, EventArgs e)
        {
            if (GeneralSetting.AutoUpdate)
            {
                var needUpdate = await GeneralSetting.CheckVersionAndUpdate();
                if (needUpdate) Application.Exit();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Activate();
        }

        /// <summary>
        /// 다른 인스턴스가 실행을 시도했을 때 현재 창을 복원하고 앞으로 가져온다.
        /// </summary>
        public void ActivateFromOtherInstance()
        {
            if (WindowState == FormWindowState.Minimized)
            {
                WindowState = FormWindowState.Normal;
            }
            if (!Visible) Show();
            Activate();
            BringToFront();
        }

        private void menuItem_about_Click(object sender, EventArgs e)
        {
            using (var dialog = new AboutDialog())
            {
                dialog.ShowDialog(this);
            }
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
            if (!MessageBoxUtil.Confirm(Resource.GetString(Key.CloseConfirmMsg))) return;

            Close();
        }

        private void menuItem_captureFolder_Click(object sender, EventArgs e)
        {
            string dirPath = CaptureSetting.CaptureSaveDirectory;
            if (string.IsNullOrWhiteSpace(dirPath)) return;
            if (!Directory.Exists(dirPath)) return;

            Process.Start(dirPath);
        }

        private void menuItem_recordFolder_Click(object sender, EventArgs e)
        {
            string dirPath = CaptureSetting.RecordSaveDirectory;
            if (string.IsNullOrWhiteSpace(dirPath)) return;
            if (!Directory.Exists(dirPath)) return;

            Process.Start(dirPath);
        }
        #endregion
    }
}