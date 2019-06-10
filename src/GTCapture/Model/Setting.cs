using GTUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public static class Setting
    {
        private const string SaveFile = "Setting.Capture.json";

        #region Properties
        public static IntPtr Handle { get; set; }

        public static Dictionary<CaptureMode, HotKey> HotKeys { get; set; }

        public static int Timer { get; set; }
        #endregion

        #region Public Method
        public static void Save()
        {
            try
            {
                var properties = new Dictionary<string, object>();
                properties.Add("Timer", Timer);

                var hotKeyProperties = new List<Dictionary<string, object>>();
                foreach (var hotKey in HotKeys.Values)
                {
                    var props = new Dictionary<string, object>();
                    props.Add("CaptureMode", hotKey.CaptureMode.ToString());
                    props.Add("Modifiers", hotKey.Modifiers.ToString());
                    props.Add("Key", hotKey.Key.ToString());
                    hotKeyProperties.Add(props);
                }
                properties.Add("HotKeyProperties", hotKeyProperties);

                string json = JsonUtil.FromProperties(properties);
                File.WriteAllText(SaveFile, json);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                SetDefault();
            }
        }

        public static void Load()
        {
            try
            {
                if (!File.Exists(SaveFile)) return;

                string json = File.ReadAllText(SaveFile);
                var properties = JsonUtil.FromJson(json);
                Timer = (int) JsonUtil.GetValue<long>(properties, "Timer");

                LoadHotKeys(properties);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                SetDefault();
            }
        }

        public static CaptureMode GetCaptureMode(KeyModifiers modifiers, Keys key)
        {
            var hotKey = HotKeys.FirstOrDefault(o => o.Value.Modifiers == modifiers && o.Value.Key == key);
            return (hotKey.Value != null) ? hotKey.Key : CaptureMode.None;
        }
        #endregion

        #region Private Method
        private static void LoadHotKeys(Dictionary<string, object> properties)
        {
            if (!properties.ContainsKey("HotKeyProperties")) return;

            var hotKeyProperties = JsonUtil.FromJarray(properties["HotKeyProperties"]);
            if (hotKeyProperties == null && hotKeyProperties.Count == 0) return;

            HotKeys = new Dictionary<CaptureMode, HotKey>();
            foreach (var props in hotKeyProperties)
            {
                var hotKey = new HotKey();
                hotKey.CaptureMode = JsonUtil.GetValue<CaptureMode>(props, "CaptureMode");
                hotKey.Modifiers = JsonUtil.GetValue<KeyModifiers>(props, "Modifiers");
                hotKey.Key = JsonUtil.GetValue<Keys>(props, "Key");

                HotKeys.Add(hotKey.CaptureMode, hotKey);
            }
        }

        private static void RegisterHotKey(CaptureMode mode)
        {
            if (Handle == IntPtr.Zero) return;
            if (!HotKeys.ContainsKey(mode)) return;

            var hotKey = HotKeys[mode];
            if (hotKey == null) return;

            if (hotKey.IsRegistered)
            {
                UnregisterHotKey(mode);
            }

            if (WindowsAPI.RegisterHotKey(Handle, (int) mode, hotKey.Modifiers, hotKey.Key))
            {
                hotKey.IsRegistered = true;
            }
        }

        private static void UnregisterHotKey(CaptureMode mode)
        {
            if (Handle == IntPtr.Zero) return;
            if (!HotKeys.ContainsKey(mode)) return;

            var hotKey = HotKeys[mode];
            if (hotKey == null) return;

            if (WindowsAPI.UnregisterHotKey(Handle, (int) mode))
            {
                hotKey.IsRegistered = false;
            }
        }

        private static void SetDefault()
        {
            if (HotKeys == null)
            {
                HotKeys = new Dictionary<CaptureMode, HotKey>();
                foreach (CaptureMode mode in Enum.GetValues(typeof(CaptureMode)))
                {
                    HotKeys.Add(mode, HotKey.GetDefault(mode));
                }
            }

            // 설정된 핫키 등록
            foreach (var mode in HotKeys.Keys)
            {
                RegisterHotKey(mode);
            }
        }
        #endregion
    }
}