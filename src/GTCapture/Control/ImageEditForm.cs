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
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public partial class ImageEditForm : Form
    {
        private bool _isLoaded;
        private Canvas _canvas;
        private readonly string _filePath;

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

            var data = File.ReadAllBytes(_filePath);
            if (data == null) return;

            var img = ImageUtil.FromByteArray(data);
            if (img == null) return;

            Rectangle workingArea = Screen.GetWorkingArea(this);
            if (img.Width >= workingArea.Width - 18 || img.Height >= workingArea.Width - 64)
            {
                Width = workingArea.Width;
                Height = workingArea.Height;
            }
            else
            {
                Width = img.Width + 18;
                Height = img.Height + 64;
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
            if (_canvas.CanUndo)
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
                        if (data == null) return;

                        File.WriteAllBytes(_filePath, data);
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine(ex);
                    }
                }
            }
        }

        private void canvas_OnDrawAction(object sender, EventArgs e)
        {
            themeButton_undo.Enabled = _canvas.CanUndo;
            themeButton_redo.Enabled = _canvas.CanRedo;

            byte[] data = _canvas.GetImageData();
            if (data == null) return;

            Clipboard.SetImage(ImageUtil.FromByteArray(data));
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

            switch (_canvas.Mode)
            {
                case DrawMode.Pen:
                    {
                        var bitmap = new Bitmap(size, size);
                        using (var g = Graphics.FromImage(bitmap))
                        using (var brush = new SolidBrush(_canvas.Color))
                        {
                            g.FillEllipse(brush, 0, 0, bitmap.Width, bitmap.Height);
                        }
                        _canvas.Cursor = new Cursor(bitmap.GetHicon());
                    }
                    break;
                case DrawMode.Highlighter:
                    {
                        var bitmap = new Bitmap(size, size);
                        using (var g = Graphics.FromImage(bitmap))
                        using (var brush = new SolidBrush(_canvas.HighlightColor))
                        {
                            g.FillEllipse(brush, 0, 0, bitmap.Width, bitmap.Height);
                        }
                        _canvas.Cursor = new Cursor(bitmap.GetHicon());
                    }
                    break;
                default:
                    _canvas.Cursor = Cursors.Default;
                    break;
            }
        }
        #endregion
    }
}
