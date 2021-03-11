using GTControl;
using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public partial class ToastMessageForm : Form
    {
        private bool _isRunning;
        private bool _isMouseEnter;
        private readonly string _filePath;

        #region Constructor
        private ToastMessageForm()
        {
            InitializeComponent();
        }

        private ToastMessageForm(string filePath) : this()
        {
            _filePath = filePath;
        }
        #endregion

        #region Control Event
        private void ToastMessageForm_Load(object sender, EventArgs e)
        {
            LayoutSetting.Invalidate(this);

            var data = File.ReadAllBytes(_filePath);
            if (data == null) return;

            var img = ImageUtil.FromByteArray(data);
            if (img == null) return;

            int resizeWidth = img.Width;
            int resizeHeight = img.Height;

            int width = Width - 2;
            if (resizeWidth > width)
            {
                float per = (float) width / img.Width;
                resizeWidth = (int) (per * img.Width);
                resizeHeight = (int) (per * img.Height);
            }

            int height = Height - 2 - 20;
            if (resizeHeight > height)
            {
                float per = (float) height / img.Height;
                resizeWidth = (int) (per * img.Width);
                resizeHeight = (int) (per * img.Height);
            }

            pictureBox.Width = resizeWidth;
            pictureBox.Height = resizeHeight;
            pictureBox.Image = img;

            int x = (width / 2) - (pictureBox.Width / 2) + 1;
            int y = (height / 2) - (pictureBox.Height / 2) + 1;
            pictureBox.Location = new Point(x, y);

            Rectangle workingArea = Screen.GetWorkingArea(this);
            Location = new Point(workingArea.Right - Size.Width - 5, workingArea.Bottom - Size.Height - 5);
        }

        private async void ToastMessageForm_Shown(object sender, EventArgs e)
        {
            if (pictureBox.Image == null)
            {
                Close();
                return;
            }

            _isRunning = true;
            await StartCloseTimer();

            if (IsDisposed || !IsHandleCreated) return;

            Close();
        }

        private void ToastMessageForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (pictureBox.Image == null) return;

            pictureBox.Image.Dispose();

            if (!_isRunning)
            {
                var editForm = new ImageEditForm(_filePath);
                editForm.Show();
            }
        }

        private void ToastMessageForm_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyData == Keys.Escape)
            {
                Close();
            }
        }

        private void pictureBox_MouseEnter(object sender, EventArgs e)
        {
            _isMouseEnter = true;
        }

        private void pictureBox_MouseLeave(object sender, EventArgs e)
        {
            _isMouseEnter = false;
        }

        private void pictureBox_Click(object sender, EventArgs e)
        {
            _isRunning = false;
            Close();
        }

        private void label_Click(object sender, EventArgs e)
        {
            Close();
        }
        #endregion

        #region Public Method
        public static void ShowMessage(string filePath)
        {
            if (!File.Exists(filePath)) return;

            try
            {
                var form = new ToastMessageForm(filePath);
                form.Show();
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
        }
        #endregion

        #region Private Method
        private async Task StartCloseTimer()
        {
            await Task.Run(() =>
            {
                int delay = 300;
                while (_isRunning && delay > 0)
                {
                    Thread.Sleep(10);
                    if (!_isMouseEnter) delay--;
                }
            });
        }
        #endregion
    }
}