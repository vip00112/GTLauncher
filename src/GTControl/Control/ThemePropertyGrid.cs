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

        #region Public Method
        public new object SelectedObject
        {
            get 
            {
                return base.SelectedObject;
            }
            set
            {
                var wrapper = new PropertyGridObjectWrapper(value);
                base.SelectedObject = wrapper;
            }
        }

        public new object[] SelectedObjects
        {
            get
            {
                return base.SelectedObjects;
            }
            set
            {
                var wrappers = new List<PropertyGridObjectWrapper>();
                foreach (var obj in value) 
                {
                    wrappers.Add(new PropertyGridObjectWrapper(obj));
                }
                base.SelectedObjects = wrappers.ToArray();
            }
        }
        #endregion

        #region Protected Method
        protected override void OnHandleCreated(EventArgs e)
        {
            base.OnHandleCreated(e);

            if (Runtime.DesignMode) return;

            _doccomment.Resize += delegate (object sender, EventArgs ex)
            {
                //var renderer = ToolStripRenderer as ThemeToolStripRenderer;
                //LineColor = renderer.ColorTable.ToolStripGradientBegin;
                //HelpBackColor = renderer.ColorTable.ToolStripGradientBegin;

                LineColor = LayoutSetting.GetBackColorCommon(LayoutSetting.Theme);
                HelpBackColor = LayoutSetting.GetBackColorCommon(LayoutSetting.Theme);
                CategoryForeColor = LayoutSetting.GetForeColorCommon(LayoutSetting.Theme);
                SelectedItemWithFocusBackColor = LayoutSetting.GetBackColorHover(LayoutSetting.Theme);
                SelectedItemWithFocusForeColor = LayoutSetting.GetForeColorHover(LayoutSetting.Theme);
            };
            _doccomment.Height++;
            _doccomment.Height--;
        }
        #endregion
    }
}
