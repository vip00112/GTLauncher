using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTUtil;

namespace GTControl
{
    public class ThemePropertyGrid : PropertyGrid
    {
        private Control _doccomment;

        #region Constructor
        public ThemePropertyGrid()
        {
            base.ToolStripRenderer = new ThemeToolStripRenderer();
            var flags = BindingFlags.NonPublic | BindingFlags.GetField | BindingFlags.Instance;
            _doccomment = base.GetType().BaseType.InvokeMember("doccomment" , flags, null, this, null) as Control;
        }
        #endregion

        #region Protected Method
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (Runtime.DesignMode) return;

            _doccomment.Resize += delegate (object sender, EventArgs ex)
            {
                var renderer = ToolStripRenderer as ThemeToolStripRenderer;
                LineColor = renderer.ColorTable.ToolStripGradientMiddle;
                HelpBackColor = renderer.ColorTable.ToolStripGradientBegin;
            };
            _doccomment.Height++;
            _doccomment.Height--;
        }
        #endregion
    }
}
