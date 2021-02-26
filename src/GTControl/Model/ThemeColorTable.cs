using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class ThemeColorTable : ProfessionalColorTable
    {
        public override Color MenuStripGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color MenuItemSelected { get { return LayoutSetting.GetBackColorHover(LayoutSetting.Theme); } }
        public override Color MenuItemBorder { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color MenuBorder { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color MenuItemSelectedGradientBegin { get { return LayoutSetting.GetBackColorHover(LayoutSetting.Theme); } }
        public override Color MenuItemSelectedGradientEnd { get { return LayoutSetting.GetBackColorHover(LayoutSetting.Theme); } }
        public override Color MenuItemPressedGradientBegin { get { return LayoutSetting.GetBackColorHover(LayoutSetting.Theme); } }
        public override Color MenuItemPressedGradientMiddle { get { return LayoutSetting.GetBackColorHover(LayoutSetting.Theme); } }
        public override Color MenuItemPressedGradientEnd { get { return LayoutSetting.GetBackColorHover(LayoutSetting.Theme); } }
        public override Color RaftingContainerGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color RaftingContainerGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color SeparatorDark { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color SeparatorLight { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color StatusStripGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color StatusStripGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ToolStripBorder { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color ToolStripDropDownBackground { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ToolStripGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ToolStripGradientMiddle { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ToolStripGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ToolStripContentPanelGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ToolStripContentPanelGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ToolStripPanelGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ToolStripPanelGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color OverflowButtonGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color MenuStripGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ImageMarginRevealedGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ImageMarginRevealedGradientMiddle { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ImageMarginRevealedGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonSelectedHighlight { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonSelectedHighlightBorder { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonPressedHighlight { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonPressedHighlightBorder { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonCheckedHighlight { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonCheckedHighlightBorder { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonPressedBorder { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonSelectedBorder { get { return LayoutSetting.GetForeColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonCheckedGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonCheckedGradientMiddle { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonCheckedGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color OverflowButtonGradientMiddle { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonSelectedGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonSelectedGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonPressedGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonPressedGradientMiddle { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonPressedGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color CheckBackground { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color CheckSelectedBackground { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color CheckPressedBackground { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color GripDark { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color GripLight { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ImageMarginGradientBegin { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ImageMarginGradientMiddle { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ImageMarginGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color ButtonSelectedGradientMiddle { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
        public override Color OverflowButtonGradientEnd { get { return LayoutSetting.GetBackColorCommon(LayoutSetting.Theme); } }
    }
}
