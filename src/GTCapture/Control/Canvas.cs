using GTUtil;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.Linq;
using System.Text;
using System.Windows.Forms;

namespace GTCapture
{
    public class Canvas : PictureBox
    {
        enum HoldMode { None, Horizontal, Vertical }

        public EventHandler OnDrawAction;

        private DrawCommand _command;
        private DrawLineMemento _drawLine;

        private Point _startLoc;
        private HoldMode _holdMode;

        #region Constructor
        private Canvas()
        {
            DoubleBuffered = true;
            SetStyle(ControlStyles.UserPaint, true);
            SetStyle(ControlStyles.AllPaintingInWmPaint, true);
            SetStyle(ControlStyles.ResizeRedraw, true);
            SetStyle(ControlStyles.ContainerControl, true);
            SetStyle(ControlStyles.OptimizedDoubleBuffer, true);
            SetStyle(ControlStyles.SupportsTransparentBackColor, true);
            SizeMode = PictureBoxSizeMode.Zoom;

            _command = new DrawCommand();
        }

        public Canvas(Image img) : this()
        {
            Width = img.Width;
            Height = img.Height;
            Image = img;
        }
        #endregion

        #region Properties
        public Color Color { get; set; }

        public Color HighlightColor { get { return Color.FromArgb(50, Color); } }

        public int LineSize { get; set; }

        public DrawMode Mode { get; set; }

        public bool IsPressedShiftKey { get; set; }

        public bool CanUndo { get { return _command.CanUndo; } }

        public bool CanRedo { get { return _command.CanRedo; } }
        #endregion

        #region Protected Method
        protected override void OnMouseDown(MouseEventArgs e)
        {
            base.OnMouseDown(e);

            if (e.Button != MouseButtons.Left) return;

            _startLoc = e.Location;
            _holdMode = HoldMode.None;

            switch (Mode)
            {
                case DrawMode.Pen:
                    _drawLine = new DrawLineMemento(Color, LineSize);
                    break;
                case DrawMode.Highlighter:
                    _drawLine = new DrawLineMemento(HighlightColor, LineSize);
                    break;
            }
        }

        protected override void OnMouseMove(MouseEventArgs e)
        {
            base.OnMouseMove(e);

            if (e.Button != MouseButtons.Left) return;
            switch (Mode)
            {
                case DrawMode.Pen:
                case DrawMode.Highlighter:
                    if (_drawLine != null)
                    {
                        var loc = e.Location;
                        if (IsPressedShiftKey && _holdMode == HoldMode.None)
                        {
                            _holdMode = CalcHoldMode(e.Location);
                        }

                        if (_holdMode == HoldMode.Horizontal) loc.Y = _startLoc.Y;
                        else if (_holdMode == HoldMode.Vertical) loc.X = _startLoc.X;

                        _drawLine.AddLocation(loc);
                    }
                    break;
            }
            Invalidate();
        }

        protected override void OnMouseUp(MouseEventArgs e)
        {
            base.OnMouseUp(e);

            if (e.Button != MouseButtons.Left) return;

            _holdMode = HoldMode.None;

            switch (Mode)
            {
                case DrawMode.Pen:
                case DrawMode.Highlighter:
                    if (_drawLine != null && _drawLine.CanDraw())
                    {
                        AddMemento(_drawLine);
                        _drawLine = null;
                    }
                    break;
            }
            Invalidate();
        }

        protected override void OnPaint(PaintEventArgs e)
        {
            base.OnPaint(e);

            if (Runtime.DesignMode) return;

            _command.Draw(e.Graphics);

            if (_drawLine != null && _drawLine.CanDraw())
            {
                _drawLine.Draw(e.Graphics);
            }
        }
        #endregion

        #region Private Method
        private void AddMemento(IMemento memento)
        {
            _command.AddMemento(memento.Clone());
            OnDrawAction?.Invoke(this, EventArgs.Empty);
        }

        private HoldMode CalcHoldMode(Point loc)
        {
            int diffX = Math.Abs(loc.X - _startLoc.X);
            int diffY = Math.Abs(loc.Y - _startLoc.Y);

            if (diffX == diffY) return HoldMode.Horizontal;
            if (diffX > diffY) return HoldMode.Horizontal;
            else  return HoldMode.Vertical;
        }
        #endregion

        #region Public Method
        public void Undo()
        {
            _command.Undo();
            Invalidate();
            OnDrawAction?.Invoke(this, EventArgs.Empty);
        }

        public void Redo()
        {
            _command.Redo();
            Invalidate();
            OnDrawAction?.Invoke(this, EventArgs.Empty);
        }

        public byte[] GetImageData()
        {
            var bitmap = new Bitmap(Width, Height);
            using (var g = Graphics.FromImage(bitmap))
            {
                g.DrawImage(Image, 0, 0);
                _command.Draw(g);
            }

            return ImageUtil.ToByteArray(bitmap, Image.RawFormat);
        }
        #endregion
    }
}