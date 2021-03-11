using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class ColorPicker : Panel
    {
        public EventHandler OnChangedColor;

        private Color _color;

        #region Constructor
        public ColorPicker()
        {
            Cursor = Cursors.Hand;
            Color = Color.Red;
        }
        #endregion

        #region Properties
        public Color Color
        {
            get { return _color; }
            set
            {
                _color = value;
                Invalidate();
            }
        }

        public string ColorName
        {
            get
            {
                if (Color == Color.Empty) return "None";
                return Color.Name;
            }
        }
        #endregion

        #region Protected Method
        protected override void OnClick(EventArgs e)
        {
            base.OnClick(e);

            using (ColorDialog cd = new ColorDialog())
            {
                cd.Color = Color;
                if (cd.ShowDialog() != DialogResult.OK) return;

                Color = cd.Color;
                OnChangedColor?.Invoke(this, EventArgs.Empty);
            }
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            using (var b= new SolidBrush(Color))
            {
                e.Graphics.FillRectangle(b, 0, 0, Width, Height);
            }
        }
        #endregion
    }
}
