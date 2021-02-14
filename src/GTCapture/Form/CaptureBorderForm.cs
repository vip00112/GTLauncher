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
    public partial class CaptureBorderForm : Form
    {
        private const int LineSize = 4;
        private const int MinSize = LineSize * 10;

        private Point _moveLoc;

        #region Constructor
        public CaptureBorderForm()
        {
            InitializeComponent();

            DoubleBuffered = true;
        }
        #endregion

        #region Properties
        private ActionType ActionType { get; set; }

        private Rectangle LeftRec
        {
            get { return new Rectangle(0, 0, LineSize, Height); }
        }

        private Rectangle TopRec
        {
            get { return new Rectangle(0, 0, Width, LineSize); }
        }

        private Rectangle RightRec
        {
            get { return new Rectangle(Width - LineSize, 0, LineSize, Height); }
        }

        private Rectangle BottomRec
        {
            get { return new Rectangle(0, Height - LineSize, Width, LineSize); }
        }

        private Rectangle TopLeftRec
        {
            get { return new Rectangle(0, 0, LineSize, LineSize); }
        }

        private Rectangle TopRightRec
        {
            get { return new Rectangle(Width - LineSize, 0, LineSize, LineSize); }
        }

        private Rectangle BottomRightRec
        {
            get { return new Rectangle(Width - LineSize, Height - LineSize, LineSize, LineSize); }
        }

        private Rectangle BottomLeftRec
        {
            get { return new Rectangle(0, Height - LineSize, LineSize, LineSize); }
        }

        private Rectangle CenterRec
        {
            get
            {
                int value = LineSize * 2;
                return new Rectangle(Width / 2 - value / 2, Height / 2 - value / 2, value, value);
            }
        }

        public bool IsStartedRecored { get; private set; }

        public Rectangle RecordRec
        {
            get 
            {
                int x = Location.X + LineSize;
                int y = Location.Y + LineSize;
                int w = Width - (LineSize * 2);
                int h = Height - (LineSize * 2);
                return new Rectangle(x, y, w, h); 
            }
        }

        new public int Width
        {
            get { return base.Width; }
            set
            {
                if (value < MinSize) value = MinSize;
                base.Width = value;
            }
        }

        new public int Height
        {
            get { return base.Height; }
            set
            {
                if (value < MinSize) value = MinSize;
                base.Height = value;
            }
        }

        new public int Left
        {
            get { return base.Left; }
            set
            {
                int diff = value - base.Left;
                if (Width <= MinSize && diff > 0) value = base.Left;
                base.Left = value;
            }
        }

        new public int Top
        {
            get { return base.Top; }
            set
            {
                int diff = value - base.Top;
                if (Height <= MinSize && diff > 0) value = base.Top;
                base.Top = value;
            }
        }
        #endregion

        #region Control Event
        private void CaptureBorderForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                DialogResult = DialogResult.Cancel;
            }
        }

        private void CaptureBorderForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (CenterRec.Contains(e.X, e.Y))
            {
                ActionType = ActionType.Move;
                _moveLoc = e.Location;
            }
            else if (TopLeftRec.Contains(e.X, e.Y))
            {
                ActionType = ActionType.ResizeTopLeft;
            }
            else if (TopRightRec.Contains(e.X, e.Y))
            {
                ActionType = ActionType.ResizeTopRight;
            }
            else if (BottomRightRec.Contains(e.X, e.Y))
            {
                ActionType = ActionType.ResizeBottomRight;
            }
            else if (BottomLeftRec.Contains(e.X, e.Y))
            {
                ActionType = ActionType.ResizeBottomLeft;
            }
            else if (LeftRec.Contains(e.X, e.Y))
            {
                ActionType = ActionType.ResizeLeft;
            }
            else if (TopRec.Contains(e.X, e.Y))
            {
                ActionType = ActionType.ResizeTop;
            }
            else if (RightRec.Contains(e.X, e.Y))
            {
                ActionType = ActionType.ResizeRight;
            }
            else if (BottomRec.Contains(e.X, e.Y))
            {
                ActionType = ActionType.ResizeBottom;
            }
            else
            {
                ActionType = ActionType.None;
            }
        }

        private void CaptureBorderForm_MouseMove(object sender, MouseEventArgs e)
        {
            switch (ActionType)
            {
                case ActionType.Move:
                    Location = new Point(Location.X - (_moveLoc.X - e.X), Location.Y - (_moveLoc.Y - e.Y));
                    break;
                case ActionType.ResizeLeft:
                case ActionType.ResizeTop:
                case ActionType.ResizeRight:
                case ActionType.ResizeBottom:
                case ActionType.ResizeTopLeft:
                case ActionType.ResizeTopRight:
                case ActionType.ResizeBottomRight:
                case ActionType.ResizeBottomLeft:
                    ResizeAction();
                    break;
                default:
                    ChangeCursor(e.X, e.Y);
                    break;
            }
            Invalidate();
        }

        private void CaptureBorderForm_MouseUp(object sender, MouseEventArgs e)
        {
            ActionType = ActionType.None;
        }

        private void CaptureBorderForm_Paint(object sender, PaintEventArgs e)
        {
            var rec = new Rectangle(LineSize, LineSize, Width - (LineSize * 2), Height - (LineSize * 2));
            using (var b = new SolidBrush(Color.Lime))
            {
                e.Graphics.FillRectangle(b, rec);
            }

            if (!IsStartedRecored)
            {
                using (var b = new SolidBrush(BackColor))
                {
                    e.Graphics.FillRectangle(b, CenterRec);

                    var recordRec = RecordRec;
                    e.Graphics.DrawString(string.Format("{0}x{1}", recordRec.Width, recordRec.Height), Font, b, LineSize + 1, LineSize + 1);
                }
            }
        }
        #endregion

        #region Public Method
        public void StartRecord()
        {
            if (IsStartedRecored) return;

            IsStartedRecored = true;
            BackColor = Color.Green;
        }

        public void StopRecord()
        {
            if (!IsStartedRecored) return;

            IsStartedRecored = false;
            BackColor = Color.Red;

            DialogResult = DialogResult.OK;
        }
        #endregion

        #region Private Method
        private void ChangeCursor(int x, int y)
        {
            if (TopLeftRec.Contains(x, y))
            {
                Cursor = Cursors.SizeNWSE;
            }
            else if (TopRightRec.Contains(x, y))
            {
                Cursor = Cursors.SizeNESW;
            }
            else if (BottomRightRec.Contains(x, y))
            {
                Cursor = Cursors.SizeNWSE;
            }
            else if (BottomLeftRec.Contains(x, y))
            {
                Cursor = Cursors.SizeNESW;
            }
            else if (CenterRec.Contains(x, y))
            {
                Cursor = Cursors.SizeAll;
            }
            else if (LeftRec.Contains(x, y))
            {
                Cursor = Cursors.SizeWE;
            }
            else if (TopRec.Contains(x, y))
            {
                Cursor = Cursors.SizeNS;
            }
            else if (RightRec.Contains(x, y))
            {
                Cursor = Cursors.SizeWE;
            }
            else if (BottomRec.Contains(x, y))
            {
                Cursor = Cursors.SizeNS;
            }
            else
            {
                Cursor = Cursors.Default;
            }
        }

        private void ResizeAction()
        {
            int locX = Location.X;
            int locY = Location.Y;
            int currentX = Cursor.Position.X;
            int currentY = Cursor.Position.Y;
            switch (ActionType)
            {
                case ActionType.ResizeLeft:
                    Width = (locX + Width) - currentX;
                    Left = currentX;
                    break;
                case ActionType.ResizeTop:
                    Height = (locY + Height) - currentY;
                    Top = currentY;
                    break;
                case ActionType.ResizeRight:
                    Width = currentX - locX;
                    break;
                case ActionType.ResizeBottom:
                    Height = currentY - locY;
                    break;
                case ActionType.ResizeTopLeft:
                    Height = (locY + Height) - currentY;
                    Top = currentY;
                    Width = (locX + Width) - currentX;
                    Left = currentX;
                    break;
                case ActionType.ResizeTopRight:
                    Height = (locY + Height) - currentY;
                    Top = currentY;
                    Width = currentX - locX;
                    break;
                case ActionType.ResizeBottomRight:
                    Height = currentY - locY;
                    Width = currentX - locX;
                    break;
                case ActionType.ResizeBottomLeft:
                    Height = currentY - locY;
                    Width = (locX + Width) - currentX;
                    Left = currentX;
                    break;
            }
        }
        #endregion
    }
}