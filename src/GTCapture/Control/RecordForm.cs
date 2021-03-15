using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public partial class RecordForm : Form
    {
        private const int LineSize = 4;

        public EventHandler OnStart;
        public EventHandler OnStop;
        public EventHandler OnClose;

        private Point _startLoc;
        private Size _minSize;
        private bool _isConvertToGif;

        #region Constructor
        public RecordForm(CaptureMode mode)
        {
            InitializeComponent();

            DoubleBuffered = true;

            _minSize = new Size(80 + (LineSize * 2), 80 + (LineSize * 2) + HeaderSize);
            Mode = mode;

            if (Mode == CaptureMode.RecordGif) label_type.Text = "GIF";
            else if (Mode == CaptureMode.RecordVideo) label_type.Text = "VIDEO";
        }
        #endregion

        #region Properties
        private MoveResizeAction FormActionType { get; set; }

        private int HeaderSize { get { return panel_header.Height; } }

        private Rectangle LeftRec
        {
            get { return new Rectangle(0, HeaderSize, LineSize, Height - HeaderSize); }
        }

        private Rectangle TopRec
        {
            get { return new Rectangle(0, HeaderSize, Width, LineSize); }
        }

        private Rectangle RightRec
        {
            get { return new Rectangle(Width - LineSize, HeaderSize, LineSize, Height - HeaderSize); }
        }

        private Rectangle BottomRec
        {
            get { return new Rectangle(0, Height - LineSize, Width, LineSize); }
        }

        private Rectangle TopLeftRec
        {
            get { return new Rectangle(0, HeaderSize, LineSize, LineSize); }
        }

        private Rectangle TopRightRec
        {
            get { return new Rectangle(Width - LineSize, HeaderSize, LineSize, LineSize); }
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
                int x = (Width / 2) - (value / 2);
                int y = (HeaderSize / 2) + (Height / 2) - (value / 2);
                return new Rectangle(x, y, value, value);
            }
        }

        public bool IsStartedRecord { get; private set; }

        public Rectangle RecordRegion
        {
            get 
            {
                int x = Location.X + LineSize;
                int y = Location.Y + LineSize + HeaderSize;
                int w = Width - (LineSize * 2);
                int h = Height - (LineSize * 2) - HeaderSize;
                return new Rectangle(x, y, w, h); 
            }
        }

        public CaptureMode Mode { get; private set; }

        new public int Width
        {
            get { return base.Width; }
            set
            {
                if (value < _minSize.Width) value = _minSize.Width;
                base.Width = value;
            }
        }

        new public int Height
        {
            get { return base.Height; }
            set
            {
                if (value < _minSize.Height) value = _minSize.Height;
                base.Height = value;
            }
        }

        new public int Left
        {
            get { return base.Left; }
            set
            {
                int diff = value - base.Left;
                if (Width <= _minSize.Width && diff > 0) value = base.Left;
                base.Left = value;
            }
        }

        new public int Top
        {
            get { return base.Top; }
            set
            {
                int diff = value - base.Top;
                if (Height <= _minSize.Height && diff > 0) value = base.Top;
                base.Top = value;
            }
        }
        #endregion

        #region Control Event
        private void RecordForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            OnClose?.Invoke(this, EventArgs.Empty);
        }

        private void CaptureBorderForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Cancel();
            }
        }

        private void CaptureBorderForm_MouseDown(object sender, MouseEventArgs e)
        {
            if (IsStartedRecord)
            {
                Cursor = Cursors.Default;
                return;
            }

            _startLoc = e.Location;

            if (CenterRec.Contains(e.X, e.Y))
            {
                FormActionType = MoveResizeAction.Move;
            }
            else if (TopLeftRec.Contains(e.X, e.Y))
            {
                FormActionType = MoveResizeAction.ResizeTopLeft;
            }
            else if (TopRightRec.Contains(e.X, e.Y))
            {
                FormActionType = MoveResizeAction.ResizeTopRight;
            }
            else if (BottomRightRec.Contains(e.X, e.Y))
            {
                FormActionType = MoveResizeAction.ResizeBottomRight;
            }
            else if (BottomLeftRec.Contains(e.X, e.Y))
            {
                FormActionType = MoveResizeAction.ResizeBottomLeft;
            }
            else if (LeftRec.Contains(e.X, e.Y))
            {
                FormActionType = MoveResizeAction.ResizeLeft;
            }
            else if (TopRec.Contains(e.X, e.Y))
            {
                FormActionType = MoveResizeAction.ResizeTop;
            }
            else if (RightRec.Contains(e.X, e.Y))
            {
                FormActionType = MoveResizeAction.ResizeRight;
            }
            else if (BottomRec.Contains(e.X, e.Y))
            {
                FormActionType = MoveResizeAction.ResizeBottom;
            }
            else
            {
                FormActionType = MoveResizeAction.None;
            }
        }

        private void CaptureBorderForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (IsStartedRecord)
            {
                Cursor = Cursors.Default;
                return;
            }

            switch (FormActionType)
            {
                case MoveResizeAction.Move:
                case MoveResizeAction.ResizeLeft:
                case MoveResizeAction.ResizeTop:
                case MoveResizeAction.ResizeRight:
                case MoveResizeAction.ResizeBottom:
                case MoveResizeAction.ResizeTopLeft:
                case MoveResizeAction.ResizeTopRight:
                case MoveResizeAction.ResizeBottomRight:
                case MoveResizeAction.ResizeBottomLeft:
                    Bounds = ControlMoveResizeUtil.CalcBounds(FormActionType, this, _startLoc, e.Location);
                    break;
                default:
                    ChangeCursor(e.X, e.Y);
                    break;
            }
            Invalidate();
        }

        private void CaptureBorderForm_MouseUp(object sender, MouseEventArgs e)
        {
            FormActionType = MoveResizeAction.None;
        }

        private void CaptureBorderForm_Paint(object sender, PaintEventArgs e)
        {
            var rec = new Rectangle(LineSize, LineSize + HeaderSize, RecordRegion.Width, RecordRegion.Height);
            using (var b = new SolidBrush(Color.Lime))
            {
                e.Graphics.FillRectangle(b, rec);
            }

            if (!IsStartedRecord)
            {
                using (var b = new SolidBrush(BackColor))
                {
                    e.Graphics.FillRectangle(b, CenterRec);

                    var recordRec = RecordRegion;
                    e.Graphics.DrawString(string.Format("{0}x{1}", recordRec.Width, recordRec.Height), Font, b, LineSize + 1, HeaderSize + LineSize + 1);
                }
            }

            if (_isConvertToGif)
            {
                using (var b = new SolidBrush(Color.Red))
                {
                    var recordRec = RecordRegion;
                    e.Graphics.DrawString("Processing\r\nConvert to gif.", Font, b, LineSize + 1, HeaderSize + LineSize + 1);
                }
            }
        }

        private void label_start_Click(object sender, EventArgs e)
        {
            if (IsStartedRecord)
            {
                OnStop?.Invoke(this, EventArgs.Empty);
            }
            else
            {
                OnStart?.Invoke(this, EventArgs.Empty);
            }
        }

        private void label_close_Click(object sender, EventArgs e)
        {
            Cancel();
        }
        #endregion

        #region Public Method
        public void StartRecord()
        {
            if (IsStartedRecord) return;

            label_start.Text = "■";
            IsStartedRecord = true;
            BackColor = Color.Green;
            label_start.ForeColor = Color.Green;
            label_type.ForeColor = Color.Green;
            label_time.ForeColor = Color.Green;
            label_close.ForeColor = Color.Green;

            var timerThread = new BackgroundWorker();
            timerThread.WorkerReportsProgress = true;
            timerThread.DoWork += delegate (object sender, DoWorkEventArgs e)
            {
                int time = 0;
                while (IsStartedRecord)
                {
                    Thread.Sleep(1000);
                    timerThread.ReportProgress(++time);
                }
            };
            timerThread.ProgressChanged += delegate (object sender, ProgressChangedEventArgs e)
            {
                label_time.Text = TimeSpan.FromSeconds(e.ProgressPercentage).ToString("mm':'ss");
            };
            timerThread.RunWorkerAsync();
        }

        public void StopRecord()
        {
            if (!IsStartedRecord) return;

            IsStartedRecord = false;
            BackColor = Color.Red;
            label_start.ForeColor = Color.Red;
            label_type.ForeColor = Color.Red;
            label_time.ForeColor = Color.Red;
            label_close.ForeColor = Color.Red;

            Close();
        }

        public void StartingConvertToGif()
        {
            if (Mode != CaptureMode.RecordGif) return;

            _isConvertToGif = true;
            Invalidate();
        }

        public void Cancel()
        {
            if (IsStartedRecord)
            {
                OnStop?.Invoke(this, EventArgs.Empty);
                return;
            }

            Close();
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
        #endregion
    }
}