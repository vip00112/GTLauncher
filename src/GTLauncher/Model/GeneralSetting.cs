using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
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
        #endregion

        #region Public Method
        public static void Save()
        {
            try
            {
                var properties = new Dictionary<string, object>();
                properties.Add("RunOnStartup", RunOnStartup);

                string path = Path.Combine(Application.StartupPath, SaveFileName);
                string json = JsonUtil.FromProperties(properties);
                File.WriteAllText(path, json);

                // 시작프로그램 등록
                string name = "GTLauncher";
                var reg = Registry.CurrentUser.OpenSubKey("SOFTWARE\\Microsoft\\Windows\\CurrentVersion\\Run", true);
                if (reg.GetValue(name) != null)
                {
                    reg.DeleteValue(name, false);
                }
                if (RunOnStartup)
                {
                    reg.SetValue(name, Application.ExecutablePath);
                }
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
                if (!File.Exists(path)) return;

                string json = File.ReadAllText(path);
                Dictionary<string, object> properties = JsonUtil.FromJson(json);

                RunOnStartup = JsonUtil.GetValue<bool>(properties, "RunOnStartup");
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
        #endregion
    }
}