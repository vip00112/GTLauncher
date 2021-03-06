﻿using GoodbyeDPI;
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
            _goodbyedpiManager = new Manager();

            GeneralSetting.Load();
            LayoutSetting.Load();
            CaptureSetting.Load();
            
            BuildLayout();

            // 업데이트 tmp 파일 삭제
            var di = new DirectoryInfo(Application.StartupPath);
            var fis = di.GetFiles("*.update.tmp");
            foreach (var fi in fis)
            {
                string filePath = fi.FullName.Replace(".update.tmp", "");
                fi.CopyTo(filePath, true);
                fi.Delete();
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

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_goodbyedpiManager != null) _goodbyedpiManager.Kill();
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Activate();
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
                if (!MessageBoxUtil.Confirm(Resource.GetString(Key.GoodbyeDPICloseConfirmMsg))) return;

                _goodbyedpiManager.Kill();
            }
            else
            {
                _goodbyedpiManager.Start();
            }
        }
        #endregion
    }
}