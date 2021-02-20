﻿using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public class WinAPI
    {
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

        public static void SetFormShadow(IntPtr handle)
        {
            int attr = 2;
            int value = 2;
            WindowNative.DwmSetWindowAttribute(handle, attr, ref value, sizeof(int));

            var margins = new WindowNative.MARGINS()
            {
                bottomHeight = 1,
                leftWidth = 0,
                rightWidth = 0,
                topHeight = 0,
            };
            WindowNative.DwmExtendFrameIntoClientArea(handle, ref margins);
        }

        private static bool IsWindows10OrGreater(int build = -1)
        {
            var version = Environment.OSVersion.Version;
            return version.Major >= 10 && version.Build >= build;
        }
    }
}
