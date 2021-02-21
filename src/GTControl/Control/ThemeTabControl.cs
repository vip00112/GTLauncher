using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTUtil;

namespace GTControl
{
    public class ThemeTabControl : TabControl
    {
        private const int TCM_ADJUSTRECT = 0x1328;

        #region Protected Method
        // Remove Border
        protected override void WndProc(ref Message m)
        {
            if (m.Msg == TCM_ADJUSTRECT)
            {
                var rect = (WindowNative.RECT) (m.GetLParam(typeof(WindowNative.RECT)));
                rect.Left -= 3;
                rect.Right += 3;
                rect.Top -= 4;
                rect.Bottom += 3;

                System.Runtime.InteropServices.Marshal.StructureToPtr(rect, m.LParam, true);
            }
            base.WndProc(ref m);
        }
        #endregion
    }
}
