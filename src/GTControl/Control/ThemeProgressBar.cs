using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class ThemeProgressBar : ProgressBar
    {
        #region Connstructor
        public ThemeProgressBar()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
        }
        #endregion

        #region Protected Method
        protected override void OnPaint(PaintEventArgs e)
        {
            using (var backBrush = new SolidBrush(LayoutSetting.GetBackColorHover(LayoutSetting.Theme)))
            using (var innerBrush = new SolidBrush(LayoutSetting.GetBackColorCommon(LayoutSetting.Theme)))
            {
                var rec = e.ClipRectangle;
                e.Graphics.FillRectangle(backBrush, rec);

                rec.Width = (int) (rec.Width * ((double) Value / Maximum)) - 4;
                rec.Height = rec.Height - 4;
                e.Graphics.FillRectangle(innerBrush, 2, 2, rec.Width, rec.Height);
            }
        }
        #endregion
    }
}
