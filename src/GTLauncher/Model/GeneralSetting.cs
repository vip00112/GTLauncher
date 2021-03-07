using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTLocalization;
using GTUtil;
using Microsoft.Win32;

namespace GTLauncher
{
    public class GeneralSetting
    {
        private const string SaveFileName = "Setting.General.json";

        #region Properties
        /// <summary>
        /// Windows 시작시 프로그램 구동 여부
        /// </summary>
        public static bool RunOnStartup { get; set; }

        /// <summary>
        /// 프로그램 구동시 최신버전 자동 업데이트 여부
        /// </summary>
        public static bool AutoUpdate { get; set; }
        #endregion

        #region Public Method
        public static void Save()
        {
            try
            {
                var properties = new Dictionary<string, object>();
                properties.Add("RunOnStartup", RunOnStartup);
                properties.Add("AutoUpdate", AutoUpdate);

                string path = Path.Combine(Application.StartupPath, SaveFileName);
                string json = JsonUtil.FromProperties(properties);
                File.WriteAllText(path, json);

#if DEBUG
#else
                // 시작프로그램 등록
                string name = "GTLauncher";
                var reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (reg.GetValue(name) != null) reg.DeleteValue(name, false);
                if (RunOnStartup) reg.SetValue(name, Application.ExecutablePath);
#endif
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static void Load()
        {
            try
            {
                string path = Path.Combine(Application.StartupPath, SaveFileName);
                if (!File.Exists(path))
                {
                    SetDefault();
                    return;
                }

                string json = File.ReadAllText(path);
                Dictionary<string, object> properties = JsonUtil.FromJson(json);

                RunOnStartup = JsonUtil.GetValue<bool>(properties, "RunOnStartup");
                AutoUpdate = JsonUtil.GetValue<bool>(properties, "AutoUpdate");
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        public static async Task<bool> CheckVersionAndUpdate()
        {
            var task = Task.Run(() =>
            {
                System.Threading.Thread.Sleep(2000);
                var currentVersion = Assembly.GetExecutingAssembly().GetName().Version;
                var releaseVersion = GithubUtil.GetLatestVersion("vip00112", "GTLauncher");
                if (currentVersion >= releaseVersion) return null;

                return GithubUtil.GetDownloadUrlForLatestAsset("vip00112", "GTLauncher", "GTLauncher*.zip");
            });

            var needUpdate = await task.ContinueWith((result) =>
            {
                string filePath = Path.Combine(Application.StartupPath, "GTAutoUpdate.exe");
                if (!File.Exists(filePath)) return false;

                string url = result.Result;
                if (string.IsNullOrWhiteSpace(url)) return false;

                int idx = url.LastIndexOf("/");
                if (idx == -1) return false;

                string fileName = url.Substring(idx + 1);
                string savePath = Path.Combine(Application.StartupPath, fileName);

                string msg = Resource.GetString(Key.NewVersionDownloadConfirmMsg);
                if (!MessageBoxUtil.Confirm(msg)) return false;

                var proc = new Process();
                proc.StartInfo.FileName = filePath;
                proc.StartInfo.Arguments = string.Format("\"{0}\" \"{1}\"", url, savePath);
                proc.Start();
                return true;
            }, TaskScheduler.FromCurrentSynchronizationContext());

            return needUpdate;
        }
        #endregion

        #region Private Method
        private static void SetDefault()
        {
            RunOnStartup = true;
            AutoUpdate = true;
        }
        #endregion
    }
}