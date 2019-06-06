using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTControl
{
    public class PageBody : TableLayoutPanel
    {
        #region Constructor
        public PageBody()
        {
            DoubleBuffered = true;

            Padding = new Padding(0);
            Margin = new Padding(0);
            Dock = DockStyle.Fill;
            IsEditMode = false;

            ColumnCount = 10;
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));
            ColumnStyles.Add(new ColumnStyle(SizeType.Percent, 10F));

            RowCount = 10;
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
            RowStyles.Add(new RowStyle(SizeType.Percent, 10F));
        }
        #endregion

        #region Properties
        public bool IsEditMode { get; set; }
        #endregion

        #region Protected Method
        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (IsEditMode)
            {
                DrawLine(e.Graphics);
            }
        }
        #endregion

        #region Private Method
        private void DrawLine(Graphics g)
        {
            float width = Width / 10f;
            float height = Height / 10f;
            Color lineColor = Setting.GetForeColorCommon(Setting.Theme);

            using (var pen = new Pen(Color.Red))
            {
                g.DrawLine(pen, 0, 0, Width, 0);
                g.DrawLine(pen, 0, 0, 0, Height);
                g.DrawLine(pen, Width - 1, 0, Width - 1, Height);
                g.DrawLine(pen, 0, Height - 1, Width, Height - 1);
                for (int i = 1; i <= 9; i++)
                {
                    var x = i * width;
                    var y = i * height;
                    g.DrawLine(pen, x, 0, x, Height);
                    g.DrawLine(pen, 0, y, Width, y);
                }
            }
        }
        #endregion
    }
}
