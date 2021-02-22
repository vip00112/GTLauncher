using GTControl;
using GTUtil;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GoodbyeDPI
{
    public class Manager
    {
        private string _downloadFilePath;
        private string _fileNameExe;
        private string _fileNameDll;
        private string _fileNameSys;

        #region Constructor
        public Manager()
        {
            _downloadFilePath = Path.Combine(Application.StartupPath, "Tools", "GoodbyeDPI.zip");
            _fileNameExe = Path.Combine(Application.StartupPath, "Tools", "goodbyedpi.exe");
            _fileNameDll = Path.Combine(Application.StartupPath, "Tools", "WinDivert.dll");
            _fileNameSys = Path.Combine(Application.StartupPath, "Tools", "WinDivert64.sys");
        }
        #endregion

        #region Public Method
        public void Start()
        {
            if (!CheckExecuteFile()) return;

            Kill();

            var proc = new Process();
            proc.StartInfo.FileName = _fileNameExe;
            proc.StartInfo.WorkingDirectory = Application.StartupPath;
            proc.StartInfo.UseShellExecute = true;
            proc.StartInfo.Verb = "runas";
            proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            proc.Start();
        }

        public void Kill()
        {
            var startedProcs = Process.GetProcessesByName("goodbyedpi");
            foreach (var proc in startedProcs)
            {
                proc.Kill();
            }
        }
        #endregion

        #region Private Method
        private bool CheckExecuteFile()
        {
            if (!File.Exists(_fileNameExe) || !File.Exists(_fileNameDll) || !File.Exists(_fileNameSys))
            {
                string msg = "Can't find required files for execute.\r\nAre you download files?";
                if (!MessageBoxUtil.Confirm(msg)) return false;
                return DownloadExecuteFile();
            }

            return true;
        }

        private bool DownloadExecuteFile()
        {
            string dirPath = Path.GetDirectoryName(_downloadFilePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string url = GithubUtil.GetDownloadUrlForLastReleaseAsset("vip00112", "GTLauncherDependency", "GoodbyeDPI-0.1.6-x64.zip");
            if (string.IsNullOrWhiteSpace(url)) return false;

            using (var dialog = new DownloadDialog(url, _downloadFilePath))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Unzip
                    return ZipUtil.Unzip(_downloadFilePath, Path.GetDirectoryName(_downloadFilePath), true);
                }
            }

            return false;
        }
        #endregion
    }
}