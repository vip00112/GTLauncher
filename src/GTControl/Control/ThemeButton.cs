using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class ThemeButton : Label
    {
        private string _text;

        #region Constructor
        public ThemeButton()
        {
            DoubleBuffered = true;

            base.AutoSize = false;
            Size = new Size(100, 20);
            Margin = new Padding(5);
            FlatStyle = FlatStyle.Flat;
            TextAlign = ContentAlignment.MiddleCenter;
            Cursor = Cursors.Hand;
        }
        #endregion

        #region Properties
        // Label 더블클릭시 Text 복사 버그 방지
        public override string Text
        {
            get { return _text; }
            set
            {
                if (value == null) value = "";
                if (_text != value)
                {
                    _text = value;
                    Refresh();
                    OnTextChanged(EventArgs.Empty);
                }
            }
        }

        #endregion

        #region Protected Method
        protected override void OnMouseEnter(EventArgs e)
        {
            base.OnMouseEnter(e);

            if (LayoutSetting.IsEditMode) return;
            BackColor = LayoutSetting.GetBackColorHover(LayoutSetting.Theme);
            ForeColor = LayoutSetting.GetForeColorHover(LayoutSetting.Theme);
        }

        protected override void OnMouseLeave(EventArgs e)
        {
            base.OnMouseLeave(e);

            if (LayoutSetting.IsEditMode) return;
            BackColor = LayoutSetting.GetBackColorCommon(LayoutSetting.Theme);
            ForeColor = LayoutSetting.GetForeColorCommon(LayoutSetting.Theme);
        }
        #endregion
    }
}
