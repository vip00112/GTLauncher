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
                // 부분 다시그리기(ClipRectangle)가 아닌 전체 영역 기준으로 진행률을 계산해야
                // 진행 막대가 항상 올바른 폭으로 렌더링된다.
                var rec = ClientRectangle;
                e.Graphics.FillRectangle(backBrush, rec);

                int ratio = (Maximum > 0) ? Value : 0;
                int fillWidth = (int) (rec.Width * ((double) ratio / (Maximum > 0 ? Maximum : 1))) - 4;
                int fillHeight = rec.Height - 4;
                if (fillWidth > 0 && fillHeight > 0)
                {
                    e.Graphics.FillRectangle(innerBrush, 2, 2, fillWidth, fillHeight);
                }
            }
        }
        #endregion
    }
}
