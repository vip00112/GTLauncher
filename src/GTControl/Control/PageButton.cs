using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class PageButton : Label
    {
        #region Constructor
        public PageButton()
        {
            DoubleBuffered = true;

            AutoSize = false;
            Margin = new Padding(5);
            FlatStyle = FlatStyle.Flat;
            TextAlign = ContentAlignment.MiddleCenter;
            Cursor = Cursors.Hand;
        }
        #endregion

        #region Protected Method
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (Setting.IsEditMode) return;
            BackColor = Setting.GetBackColorHover(Setting.Theme);
            ForeColor = Setting.GetForeColorHover(Setting.Theme);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (Setting.IsEditMode) return;
            BackColor = Setting.GetBackColorCommon(Setting.Theme);
            ForeColor = Setting.GetForeColorCommon(Setting.Theme);
        }
        #endregion
    }
}
