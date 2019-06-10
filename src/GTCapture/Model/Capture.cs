using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public class Capture : NativeWindow
    {
        private BackgroundWorker _bw;

        #region Constructor
        public Capture(IntPtr hWnd)
        {
            // WndProc 이벤트가 발생하도록 핸들 등록
            AssignHandle(hWnd);

            Setting.Handle = hWnd;
            Setting.Load();
        }
        #endregion

        #region Protected Method
        protected override void WndProc(ref Message m)
        {
            base.WndProc(ref m);
            if (_bw != null) return;

            var modifier = (KeyModifiers) ((int) m.LParam & 0xFFFF);
            var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);

            CaptureMode mode = Setting.GetCaptureMode(modifier, key);
            if (mode == CaptureMode.None) return;

            _bw = new BackgroundWorker();
            _bw.DoWork += delegate (object sender, DoWorkEventArgs e)
            {
                int delay = Setting.Timer;
                while (delay > 0)
                {
                    Thread.Sleep(1000);
                    delay--;
                }
                e.Result = CaptureWindow(mode);
            };
            _bw.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
            {
                using (var img = e.Result as Image)
                {
                    if (img == null) return;

                    SaveImage(img);
                }
                _bw = null;
            };
            _bw.RunWorkerAsync();
        }
        #endregion

        #region Public Method
        public void ShowSettingForm()
        {
            using (var dialog = new SettingForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
            }
        }
        #endregion

        #region Private Method
        private Image CaptureWindow(CaptureMode mode)
        {
            Image img = null;
            IntPtr handle = IntPtr.Zero;
            switch (mode)
            {
                case CaptureMode.FullScreen:
                    handle = WindowsAPI.GetDesktopWindow();
                    img = CaptureWindow(handle, 0, 0, 0, 0);
                    break;
                case CaptureMode.ActiveProcess:
                    handle = WindowsAPI.GetForegroundWindow();
                    img = CaptureWindow(handle, 0, 0, 0, 0);
                    break;
                case CaptureMode.Region:
                    // TODO : 선택 영역 캡쳐
                    break;
                default: return null;
            }
            return img;
        }

        private Image CaptureWindow(IntPtr handle, int x, int y, int width, int height)
        {
            // TODO : 작은 윈도우 핸들로 캡쳐시 테두리가 더 큼
            // TODO : 폴더브라우저 캡쳐시 타이틀이 검은색으로 표시됨
            Image img = null;
            IntPtr srcDC = IntPtr.Zero;
            IntPtr memoryDC = IntPtr.Zero;
            IntPtr bitmap = IntPtr.Zero;
            try
            {
                srcDC = WindowsAPI.GetWindowDC(handle);
                memoryDC = WindowsAPI.CreateCompatibleDC(srcDC);

                if (width == 0 || height == 0)
                {
                    var rec = new WinStructRect();
                    WindowsAPI.GetWindowRect(handle, ref rec);
                    if (width == 0 ) width = rec.Right - rec.Left;
                    if (height == 0 ) height = rec.Bottom - rec.Top;
                }
                bitmap = WindowsAPI.CreateCompatibleBitmap(srcDC, width, height);

                IntPtr oldBitmap = WindowsAPI.SelectObject(memoryDC, bitmap);
                WindowsAPI.BitBlt(memoryDC, 0, 0, width, height, srcDC, x, y, WindowsAPI.SRCCOPY | WindowsAPI.CAPTUREBLT);
                WindowsAPI.SelectObject(memoryDC, oldBitmap);

                img = Image.FromHbitmap(bitmap);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                WindowsAPI.DeleteObject(bitmap);
                WindowsAPI.DeleteDC(memoryDC);
                WindowsAPI.ReleaseDC(handle, srcDC);
            }

            return img;
        }

        private void SaveImage(Image img)
        {
            if (img == null) return;
            try
            {
                if (!Directory.Exists(Setting.SaveDirectory))
                {
                    Directory.CreateDirectory(Setting.SaveDirectory);
                }
                string fileName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyy-MM-dd HH.mm.ss"), Setting.SaveImageFormat);
                string savePath = Path.Combine(Setting.SaveDirectory, fileName);
                img.Save(savePath, Setting.GetImageFormat());
                Clipboard.SetImage(img);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }
        #endregion
    }
}