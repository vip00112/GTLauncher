using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class ThemeToolStripRenderer : ToolStripProfessionalRenderer
    {
        #region Constructor
        public ThemeToolStripRenderer() : base(new ThemeColorTable())
        {

        }
        #endregion

        #region Protected Method
        protected override void OnRenderItemText(ToolStripItemTextRenderEventArgs e)
        {
            if (e.ToolStrip is ContextMenuStrip)
            {
                if (e.Item.Selected)
                {
                    e.TextColor = LayoutSetting.GetForeColorHover(LayoutSetting.Theme);
                }
                else
                {
                    e.TextColor = LayoutSetting.GetForeColorCommon(LayoutSetting.Theme);
                }
            }
            else
            {
                if (e.Item.Selected || e.Item.Pressed)
                {
                    e.TextColor = LayoutSetting.GetForeColorHover(LayoutSetting.Theme);
                }
                else
                {
                    e.TextColor = LayoutSetting.GetForeColorCommon(LayoutSetting.Theme);
                }
            }
            base.OnRenderItemText(e);
        }

        protected override void OnRenderArrow(ToolStripArrowRenderEventArgs e)
        {
            if (e.Item.Owner is ContextMenuStrip)
            {
                if (e.Item.Selected)
                {
                    e.ArrowColor = LayoutSetting.GetForeColorHover(LayoutSetting.Theme);
                }
                else
                {
                    e.ArrowColor = LayoutSetting.GetForeColorCommon(LayoutSetting.Theme);
                }
            }
            else
            {
                if (e.Item.Selected || e.Item.Pressed)
                {
                    e.ArrowColor = LayoutSetting.GetForeColorHover(LayoutSetting.Theme);
                }
                else
                {
                    e.ArrowColor = LayoutSetting.GetForeColorCommon(LayoutSetting.Theme);
                }
            }
            base.OnRenderArrow(e);
        }
        #endregion
    }
}