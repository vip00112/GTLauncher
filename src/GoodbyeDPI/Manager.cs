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
        private string _fileNameExe;
        private string _fileNameDll;
        private string _fileNameSys;
        private Process _proc;

        #region Properties
        public bool IsStarted { get; private set; }
        #endregion

        #region Constructor
        public Manager()
        {
            _fileNameExe = Path.Combine(Application.StartupPath, "goodbyedpi.exe");
            _fileNameDll = Path.Combine(Application.StartupPath, "WinDivert.dll");
            _fileNameSys = Path.Combine(Application.StartupPath, "WinDivert64.sys");
        }
        #endregion

        #region Public Method
        public void Start()
        {
            if (_proc != null) return;

            var startedProcs = Process.GetProcessesByName("goodbyedpi");
            foreach (var proc in startedProcs)
            {
                proc.Kill();
            }

            File.WriteAllBytes(_fileNameExe, Properties.Resources.goodbyedpi);
            File.WriteAllBytes(_fileNameDll, Properties.Resources.WinDivert);
            File.WriteAllBytes(_fileNameSys, Properties.Resources.WinDivert64);

            _proc = new Process();
            _proc.StartInfo.FileName = _fileNameExe;
            _proc.StartInfo.UseShellExecute = true;
            _proc.StartInfo.Verb = "runas";
            _proc.StartInfo.WindowStyle = ProcessWindowStyle.Hidden;
            _proc.Start();
            IsStarted = true;
        }

        public void Stop()
        {
            if (_proc == null) return;

            _proc.Kill();
            _proc.WaitForExit();

            try { File.Delete(_fileNameExe); } catch { }
            try { File.Delete(_fileNameDll); } catch { }
            try { File.Delete(_fileNameSys); } catch { }

            _proc = null;
            IsStarted = false;
        }
        #endregion
    }
}