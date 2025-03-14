using Microsoft.Win32;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Threading;
using System.Windows.Forms;

namespace GTLauncher
{
    static class Program
    {
        /// <summary>
        /// 해당 응용 프로그램의 주 진입점입니다.
        /// </summary>
        [STAThread]
        static void Main()
        {
#if DEBUG
            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());
#else
            if (SetHighDpiToRegistry()) return;

            bool createdNew;
            var mutex = new Mutex(true, "GTLauncher", out createdNew);
            if (!createdNew) return;

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);
            Application.Run(new MainForm());

            mutex.ReleaseMutex();
#endif
        }

        // App 호환성 HighDPI를 레지스트리에 추가
        static bool SetHighDpiToRegistry()
        {
            try
            {
                var exePath = Process.GetCurrentProcess().MainModule.FileName;
                var regPath = @"HKEY_CURRENT_USER\Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers";

                // 레지스트리에 저장된 DPI 설정 확인
                var regValue = Registry.GetValue(regPath, exePath, null) as string ?? "";
                if (!regValue.Contains("HIGHDPIAWARE"))
                {
                    var newValue = "~ HIGHDPIAWARE";
                    if (regValue.Length > 0)
                    {
                        newValue = $"{regValue} HIGHDPIAWARE";
                    }
                    Registry.SetValue(regPath, exePath, newValue, RegistryValueKind.String);

                    string filePath = Path.Combine(Application.StartupPath, "GTAutoUpdate.exe");
                    if (!File.Exists(filePath)) return false;

                    var proc = new Process();
                    proc.StartInfo.FileName = filePath;
                    proc.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\"", "RESTART", "None");
                    proc.Start();
                    return true;
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
            }
            return false;
        }
    }
}
