using GTControl;
using GTLocalization;
using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public partial class ImageEditForm : Form
    {
        [DllImport("user32.dll", CharSet = CharSet.Auto)]
        private static extern bool DestroyIcon(IntPtr handle);

        private bool _isLoaded;
        private Canvas _canvas;
        private readonly string _filePath;
        private IntPtr _cursorIconHandle = IntPtr.Zero;

        #region Constructor
        private ImageEditForm()
        {
            InitializeComponent();
        }

        public ImageEditForm(string filePath) : this()
        {
            _filePath = filePath;
        }
        #endregion

        #region Control Event
        private void ImageEditForm_Load(object sender, EventArgs e)
        {
            LayoutSetting.Invalidate(this);

            Image img = null;
            try
            {
                if (File.Exists(_filePath))
                {
                    var data = File.ReadAllBytes(_filePath);
                    img = ImageUtil.FromByteArray(data);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }

            // 파일이 없거나 손상되어 이미지를 만들 수 없으면 편집 창을 열지 않고 닫는다.
            if (img == null)
            {
                BeginInvoke((MethodInvoker) Close);
                return;
            }

            Rectangle workingArea = Screen.GetWorkingArea(this);
            if (img.Width >= workingArea.Width - 18 || img.Height >= workingArea.Height - 64)
            {
                Width = workingArea.Width;
                Height = workingArea.Height;
                Location = new Point(workingArea.Left, workingArea.Top);
            }
            else
            {
                Width = img.Width + 18;
                Height = img.Height + 64;

                int x = workingArea.Width / 2 - Width / 2;
                int y = workingArea.Height / 2 - Height / 2;
                Location = new Point(x, y);
            }

            _canvas = new Canvas(img);
            _canvas.OnDrawAction += canvas_OnDrawAction;
            _canvas.Mode = CaptureSetting.EditDrawMode;
            _canvas.Color = CaptureSetting.EditLineColor;
            _canvas.LineSize = CaptureSetting.EditLineSize;
            panel_canvas.Controls.Add(_canvas);
            ChangeCanvasCursor();

            colorPicker.OnChangedColor += colorPicker_OnChangedColor; 

            comboBox_type.DataSource = Enum.GetValues(typeof(DrawMode));
            comboBox_type.SelectedItem = CaptureSetting.EditDrawMode;

            colorPicker.Color = CaptureSetting.EditLineColor;
            numericUpDown_size.Value = CaptureSetting.EditLineSize;

            _isLoaded = true;
        }

        private void ImageEditForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_canvas != null && _canvas.CanUndo)
            {
                var result = MessageBoxUtil.ConfirmYesNoCancel(Resource.GetString(Key.CloseOrSaveConfirmMsg));
                if (result == DialogResult.Cancel)
                {
                    e.Cancel = true;
                    return;
                }

                if (result == DialogResult.Yes)
                {
                    try
                    {
                        byte[] data = _canvas.GetImageData();
                        if (data != null) File.WriteAllBytes(_filePath, data);
                    }
                    catch (Exception ex)
                    {
                        Logger.Error(ex);
                    }
                }
            }

            CaptureSetting.Save();

            // 커스텀 커서용 아이콘 핸들 정리
            if (_cursorIconHandle != IntPtr.Zero)
            {
                DestroyIcon(_cursorIconHandle);
                _cursorIconHandle = IntPtr.Zero;
            }
        }

        private void canvas_OnDrawAction(object sender, EventArgs e)
        {
            themeButton_undo.Enabled = _canvas.CanUndo;
            themeButton_redo.Enabled = _canvas.CanRedo;

            byte[] data = _canvas.GetImageData();
            if (data == null) return;

            try
            {
                using (var img = ImageUtil.FromByteArray(data))
                {
                    if (img != null) Clipboard.SetImage(img);
                }
            }
            catch (Exception ex)
            {
                Logger.Error(ex);
            }
        }

        private void comboBox_type_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isLoaded) return;

            _canvas.Mode = (DrawMode) comboBox_type.SelectedItem;
            CaptureSetting.EditDrawMode = (DrawMode) comboBox_type.SelectedItem;
            ChangeCanvasCursor();
        }

        private void colorPicker_OnChangedColor(object sender, EventArgs e)
        {
            _canvas.Color = colorPicker.Color;
            CaptureSetting.EditLineColor = colorPicker.Color;
            ChangeCanvasCursor();
        }

        private void numericUpDown_size_ValueChanged(object sender, EventArgs e)
        {
            _canvas.LineSize = (int) numericUpDown_size.Value;
            CaptureSetting.EditLineSize = (int) numericUpDown_size.Value;
            ChangeCanvasCursor();
        }

        private void themeButton_undo_Click(object sender, EventArgs e)
        {
            _canvas.Undo();
        }

        private void themeButton_redo_Click(object sender, EventArgs e)
        {
            _canvas.Redo();
        }
        #endregion

        #region Private Method
        private void ChangeCanvasCursor()
        {
            int size = _canvas.LineSize;
            if (size == 0) return;

            Cursor newCursor;
            IntPtr newIconHandle = IntPtr.Zero;

            switch (_canvas.Mode)
            {
                case DrawMode.Pen:
                case DrawMode.Highlighter:
                    using (var bitmap = new Bitmap(size, size))
                    {
                        using (var g = Graphics.FromImage(bitmap))
                        using (var brush = new SolidBrush(_canvas.Color))
                        {
                            g.FillEllipse(brush, 0, 0, bitmap.Width, bitmap.Height);
                        }
                        newIconHandle = bitmap.GetHicon();
                        newCursor = new Cursor(newIconHandle);
                    }
                    break;
                default:
                    newCursor = Cursors.Default;
                    break;
            }

            var oldCursor = _canvas.Cursor;
            _canvas.Cursor = newCursor;

            // 이전에 만든 커스텀 커서의 아이콘 핸들과 Cursor 객체를 정리한다.
            if (_cursorIconHandle != IntPtr.Zero)
            {
                DestroyIcon(_cursorIconHandle);
            }
            if (oldCursor != null && oldCursor != Cursors.Default && oldCursor != newCursor)
            {
                oldCursor.Dispose();
            }
            _cursorIconHandle = newIconHandle;
        }
        #endregion

        private void ImageEditForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                _canvas.IsPressedShiftKey = true;
            }
        }

        private void ImageEditForm_KeyUp(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.ShiftKey)
            {
                _canvas.IsPressedShiftKey = false;
            }
        }
    }
}
