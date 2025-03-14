﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTUtil
{
    public static class WinAPI
    {
        #region Public Method
        public static void SetTitleBarTheme(IntPtr handle, bool isDarkMode)
        {
            if (IsWindows10OrGreater(17763))
            {
                int attr = WindowNative.DWMWA_USE_IMMERSIVE_DARK_MODE_BEFORE_20H1;
                if (IsWindows10OrGreater(18985))
                {
                    attr = WindowNative.DWMWA_USE_IMMERSIVE_DARK_MODE;
                }

                int value = isDarkMode ? 1 : 0;
                WindowNative.DwmSetWindowAttribute(handle, attr, ref value, sizeof(int));
            }
        }

        public static void SetFormShadow(IntPtr handle, int margin = 1)
        {
            int attr = 2;
            int value = 2;
            WindowNative.DwmSetWindowAttribute(handle, attr, ref value, sizeof(int));

            var margins = new WindowNative.Margins()
            {
                bottomHeight = margin,
                leftWidth = margin,
                rightWidth = margin,
                topHeight = margin,
            };
            WindowNative.DwmExtendFrameIntoClientArea(handle, ref margins);
        }

        public static float GetMonitorScale()
        {
            var screen = Screen.AllScreens.FirstOrDefault(o => o.Primary);
            if (screen == null) screen = Screen.AllScreens[0];

            IntPtr hMonitor = WindowNative.MonitorFromPoint(screen.Bounds.Location, 2);
            if (WindowNative.GetDpiForMonitor(hMonitor, 0, out uint dpiX, out _) == 0)
            {
                return dpiX / 96f;
            }
            return 1.0f;
        }
        #endregion

        #region Private Method
        private static bool IsWindows10OrGreater(int build = -1)
        {
            var version = Environment.OSVersion.Version;
            return version.Major >= 10 && version.Build >= build;
        }
        #endregion
    }
}
