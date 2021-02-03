using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GoodbyeDPI
{
    public class Manager
    {
        private const string FileName = "goodbyedpi.exe";

        private Process _proc;

        #region Properties
        public bool IsStarted { get; private set; }
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

            File.WriteAllBytes(FileName, Properties.Resources.goodbyedpi);
            File.WriteAllBytes("WinDivert.dll", Properties.Resources.WinDivert);
            File.WriteAllBytes("WinDivert64.sys", Properties.Resources.WinDivert64);

            _proc = new Process();
            _proc.StartInfo.FileName = FileName;
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

            try
            {
                File.Delete(FileName);
                File.Delete("WinDivert.dll");
                File.Delete("WinDivert64.sys");
                _proc = null;
            }
            catch { }

            IsStarted = false;
        }
        #endregion
    }
}