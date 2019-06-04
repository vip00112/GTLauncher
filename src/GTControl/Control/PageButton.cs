using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class PageButton : Button
    {
        #region Constructor
        public PageButton()
        {
            DoubleBuffered = true;

            Margin = new Padding(5);
            FlatStyle = FlatStyle.Flat;
            FlatAppearance.BorderSize = 0;
            FlatAppearance.BorderColor = Color.FromArgb(0, 255, 255, 255);
            Cursor = Cursors.Hand;
        }
        #endregion

        #region Protected Method
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            BackColor = Setting.GetBackColorHover(Setting.Theme);
            ForeColor = Setting.GetForeColorHover(Setting.Theme);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            BackColor = Setting.GetBackColorCommon(Setting.Theme);
            ForeColor = Setting.GetForeColorCommon(Setting.Theme);
        }
        #endregion
    }
}
