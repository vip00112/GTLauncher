﻿using GTUtil;
using System;
using System.Collections.Generic;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public class CaptureSetting
    {
        private const string SaveFileName = "Setting.Capture.json";
        public static readonly string[] ImageFormats = new string[] { "png", "jpg", "bmp" };

        #region Properties
        public static IntPtr Handle { get; set; }

        public static Dictionary<CaptureMode, HotKey> HotKeys { get; set; }

        public static int Timer { get; set; }

        public static string CaptureSaveDirectory { get; set; }

        public static string SaveImageFormat { get; set; }

        public static int GifFPS { get; set; }

        public static int VideoFPS { get; set; }

        public static string AudioSource { get; set; }

        public static string RecordSaveDirectory { get; set; }
        #endregion

        #region Public Method
        public static void Save()
        {
            try
            {
                var properties = new Dictionary<string, object>();
                properties.Add("Timer", Timer);
                properties.Add("CaptureSaveDirectory", CaptureSaveDirectory);
                properties.Add("SaveImageFormat", SaveImageFormat);
                properties.Add("GifFPS", GifFPS);
                properties.Add("VideoFPS", VideoFPS);
                properties.Add("AudioSource", AudioSource);
                properties.Add("RecordSaveDirectory", RecordSaveDirectory);

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

                string path = Path.Combine(Application.StartupPath, SaveFileName);
                string json = JsonUtil.FromProperties(properties);
                File.WriteAllText(path, json);
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
                string path = Path.Combine(Application.StartupPath, SaveFileName);
                if (!File.Exists(path)) return;

                string json = File.ReadAllText(path);
                var properties = JsonUtil.FromJson(json);
                Timer = (int) JsonUtil.GetValue<long>(properties, "Timer");
                CaptureSaveDirectory = JsonUtil.GetValue<string>(properties, "CaptureSaveDirectory");
                SaveImageFormat = JsonUtil.GetValue<string>(properties, "SaveImageFormat");
                GifFPS = (int) JsonUtil.GetValue<long>(properties, "GifFPS");
                VideoFPS = (int) JsonUtil.GetValue<long>(properties, "VideoFPS");
                AudioSource = JsonUtil.GetValue<string>(properties, "AudioSource");
                RecordSaveDirectory = JsonUtil.GetValue<string>(properties, "RecordSaveDirectory");

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

        public static CaptureMode GetCaptureMode(WindowNative.KeyModifiers modifiers, Keys key)
        {
            var hotKey = HotKeys.FirstOrDefault(o => o.Value.Modifiers == modifiers && o.Value.Key == key);
            return (hotKey.Value != null) ? hotKey.Key : CaptureMode.None;
        }

        public static ImageFormat GetImageFormat()
        {
            switch (SaveImageFormat)
            {
                case "jpg": return ImageFormat.Jpeg;
                case "png": return ImageFormat.Png;
                case "gif": return ImageFormat.Gif;
                case "bmp": return ImageFormat.Bmp;
                default: return ImageFormat.Jpeg;
            }
        }

        public static void RegisterHotKey(CaptureMode mode)
        {
            if (Handle == IntPtr.Zero) return;
            if (!HotKeys.ContainsKey(mode)) return;

            var hotKey = HotKeys[mode];
            if (hotKey == null) return;

            if (hotKey.IsRegistered)
            {
                UnregisterHotKey(mode);
            }

            if (hotKey.Modifiers == WindowNative.KeyModifiers.None && hotKey.Key == Keys.None) return;

            if (WindowNative.RegisterHotKey(Handle, (int) mode, hotKey.Modifiers, hotKey.Key))
            {
                hotKey.IsRegistered = true;
            }
        }

        public static void UnregisterHotKey(CaptureMode mode)
        {
            if (Handle == IntPtr.Zero) return;
            if (!HotKeys.ContainsKey(mode)) return;

            var hotKey = HotKeys[mode];
            if (hotKey == null) return;

            if (WindowNative.UnregisterHotKey(Handle, (int) mode))
            {
                hotKey.IsRegistered = false;
            }
        }
        #endregion

        #region Private Method
        private static void LoadHotKeys(Dictionary<string, object> properties)
        {
            if (!properties.ContainsKey("HotKeyProperties")) return;

            var hotKeyProperties = JsonUtil.FromJArray(properties["HotKeyProperties"]);
            if (hotKeyProperties == null && hotKeyProperties.Count == 0) return;

            HotKeys = new Dictionary<CaptureMode, HotKey>();
            foreach (var props in hotKeyProperties)
            {
                var hotKey = new HotKey();
                hotKey.CaptureMode = JsonUtil.GetValue<CaptureMode>(props, "CaptureMode");
                var modifiers = JsonUtil.GetValue<WindowNative.KeyModifiers>(props, "Modifiers");
                var key = JsonUtil.GetValue<Keys>(props, "Key");
                hotKey.Update(modifiers, key);

                HotKeys.Add(hotKey.CaptureMode, hotKey);
            }
        }

        private static void SetDefault()
        {
            if (string.IsNullOrWhiteSpace(CaptureSaveDirectory))
            {
                CaptureSaveDirectory = Path.Combine(Application.StartupPath, "Capture");
            }
            if (string.IsNullOrWhiteSpace(SaveImageFormat))
            {
                SaveImageFormat = ImageFormats[0];
            }
            if (GifFPS == 0)
            {
                GifFPS = 15;
            }
            if (VideoFPS == 0)
            {
                VideoFPS = 30;
            }
            if (string.IsNullOrWhiteSpace(AudioSource))
            {
                AudioSource = FFmpeg.DefaultAudioSource;
            }
            if (string.IsNullOrWhiteSpace(RecordSaveDirectory))
            {
                RecordSaveDirectory = Path.Combine(Application.StartupPath, "Capture");
            }

            if (HotKeys == null) HotKeys = new Dictionary<CaptureMode, HotKey>();
            foreach (CaptureMode mode in Enum.GetValues(typeof(CaptureMode)))
            {
                if (!HotKeys.ContainsKey(mode))
                {
                    HotKeys.Add(mode, HotKey.CreateDefault(mode));
                    RegisterHotKey(mode);
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