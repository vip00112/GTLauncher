using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class ThemeListView : ListView
    {
        #region Constructor
        public ThemeListView()
        {
            View = View.Tile;
            MultiSelect = false;
            OwnerDraw = true;
            FullRowSelect = true;
        }
        #endregion

        #region Protected Method
        protected override void OnDrawItem(DrawListViewItemEventArgs e)
        {
            if (e.Item.Focused)
            {
                e.Item.BackColor = LayoutSetting.GetBackColorHover(LayoutSetting.Theme);
                e.Item.ForeColor = LayoutSetting.GetForeColorHover(LayoutSetting.Theme);
            }
            else
            {
                e.Item.BackColor = LayoutSetting.GetBackColorCommon(LayoutSetting.Theme);
                e.Item.ForeColor = LayoutSetting.GetForeColorCommon(LayoutSetting.Theme);
            }
            e.DrawBackground();
            e.DrawText(TextFormatFlags.HorizontalCenter | TextFormatFlags.VerticalCenter);
        }
        #endregion
    }
}
