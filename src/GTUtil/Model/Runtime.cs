using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public class Runtime
    {
        public static bool DesignMode { get { return DesignModeForWpf || DesignModeForWinForm; } }

        private static bool DesignModeForWpf { get { return LicenseManager.UsageMode == LicenseUsageMode.Designtime; } }

        private static bool DesignModeForWinForm { get { return Process.GetCurrentProcess().ProcessName == "devenv"; } }
    }
}
