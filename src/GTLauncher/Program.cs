using GTUtil;
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
        private const string ShowEventName = "GTLauncher.Show";

        private static Mutex _mutex;
        private static EventWaitHandle _showEvent;

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
            if (RemoveHighDpiRegistryOverride()) return;

            bool createdNew;
            _mutex = new Mutex(true, "GTLauncher", out createdNew);
            if (!createdNew)
            {
                // 이미 실행 중이면 기존 인스턴스를 화면에 띄우고 종료한다.
                SignalExistingInstance();
                return;
            }

            Application.EnableVisualStyles();
            Application.SetCompatibleTextRenderingDefault(false);

            var form = new MainForm();
            StartSecondInstanceListener(form);
            Application.Run(form);

            _mutex.ReleaseMutex();
#endif
        }

        // 두 번째 인스턴스가 실행되면 기존 인스턴스에 창 활성화를 요청한다.
        private static void SignalExistingInstance()
        {
            try
            {
                var signal = EventWaitHandle.OpenExisting(ShowEventName);
                signal.Set();
            }
            catch
            {
                // 신호용 핸들이 아직 없으면 조용히 종료한다.
            }
        }

        // 다른 인스턴스의 활성화 요청을 대기하여 메인 창을 앞으로 가져온다.
        private static void StartSecondInstanceListener(MainForm form)
        {
            _showEvent = new EventWaitHandle(false, EventResetMode.AutoReset, ShowEventName);
            var thread = new Thread(() =>
            {
                while (_showEvent.WaitOne())
                {
                    if (form.IsDisposed) break;
                    try
                    {
                        form.BeginInvoke((MethodInvoker) form.ActivateFromOtherInstance);
                    }
                    catch
                    {
                        break;
                    }
                }
            });
            thread.IsBackground = true;
            thread.Start();
        }

        // 구버전이 추가했을 수 있는 HIGHDPIAWARE 호환 플래그를 제거한다.
        // 이 플래그가 남아 있으면 매니페스트의 PerMonitorV2 선언을 덮어써(System-DPI로 고정) 버린다.
        // 플래그를 지운 뒤에는 플래그 없이 다시 실행되어야 PerMonitorV2가 적용되므로 1회만 재시작한다.
        // 신규 설치(플래그 없음)에서는 즉시 false를 반환하여 재시작 없이 그대로 진행한다.
        static bool RemoveHighDpiRegistryOverride()
        {
            try
            {
                var exePath = Process.GetCurrentProcess().MainModule.FileName;
                const string subKey = @"Software\Microsoft\Windows NT\CurrentVersion\AppCompatFlags\Layers";

                var regValue = Registry.GetValue(@"HKEY_CURRENT_USER\" + subKey, exePath, null) as string;
                if (string.IsNullOrEmpty(regValue) || !regValue.Contains("HIGHDPIAWARE")) return false;

                // HIGHDPIAWARE 토큰만 제거하고 다른 호환 설정은 유지한다.
                var tokens = regValue.Split(new[] { ' ' }, StringSplitOptions.RemoveEmptyEntries)
                                     .Where(t => t != "HIGHDPIAWARE" && t != "~")
                                     .ToArray();

                using (var key = Registry.CurrentUser.OpenSubKey(subKey, true))
                {
                    if (key == null) return false;

                    if (tokens.Length == 0)
                    {
                        key.DeleteValue(exePath, false);
                    }
                    else
                    {
                        key.SetValue(exePath, "~ " + string.Join(" ", tokens), RegistryValueKind.String);
                    }
                }

                // 플래그 없이 다시 실행되도록 1회 재시작한다.
                string filePath = Path.Combine(Application.StartupPath, "GTAutoUpdate.exe");
                if (!File.Exists(filePath)) return false;

                var proc = new Process();
                proc.StartInfo.FileName = filePath;
                proc.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\"", "RESTART", "None");
                proc.Start();
                return true;
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
            return false;
        }
    }
}
