using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public partial class CaptureRegionDialog : Form
    {
        private bool _isDown;
        private bool _isMove;
        private Point _start;
        private Point _end;

        #region Constructor
        public CaptureRegionDialog()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }
        #endregion

        #region Properties
        public Rectangle SelectedRegion { get; private set; }
        #endregion

        #region Control Event
        private void CaptureRegionForm_Load(object sender, EventArgs e)
        {
            // 듀얼 모니터 처리
            Size = new Size(SystemInformation.VirtualScreen.Width, SystemInformation.VirtualScreen.Height);
            Location = new Point(SystemInformation.VirtualScreen.Left, SystemInformation.VirtualScreen.Top);
        }

        private void CaptureRegionForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData != Keys.Escape) return;

            DialogResult = DialogResult.Cancel;
        }

        private void CaptureRegionForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            _isDown = true;
            _start = e.Location;
        }

        private void CaptureRegionForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;
            if (!_isDown) return;

            _isMove = true;
            _end = e.Location;
            Invalidate();
        }

        private void CaptureRegionForm_MouseUp(object sender, MouseEventArgs e)
        {
            if (e.Button != MouseButtons.Left) return;

            if (_isDown && _isMove)
            {
                SelectedRegion = CalcRectangle();
                DialogResult = DialogResult.OK;
            }
            else
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void CaptureRegionForm_Paint(object sender, PaintEventArgs e)
        {
            if (!_isDown) return;

            var rec = CalcRectangle();
            using (var b = new SolidBrush(Color.Lime))
            using (var p = new Pen(Color.Red, 2))
            {
                e.Graphics.FillRectangle(b, rec);
                e.Graphics.DrawRectangle(p, rec);
            }
        }
        #endregion

        #region Private Method
        private Rectangle CalcRectangle()
        {
            int startX = Math.Min(_start.X, _end.X);
            int startY = Math.Min(_start.Y, _end.Y);
            int endX = Math.Max(_start.X, _end.X);
            int endY = Math.Max(_start.Y, _end.Y);
            int width = endX - startX;
            int height = endY - startY;
            return new Rectangle(startX, startY, width, height);
        }
        #endregion
    }
}
