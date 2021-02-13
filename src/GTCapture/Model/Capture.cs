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
        public EventHandler OnCaptured;

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
                try
                {
                    using (var img = e.Result as Image)
                    {
                        if (img == null) return;

                        OnCaptured?.Invoke(this, EventArgs.Empty);
                        SaveImage(img);
                    }
                }
                catch (Exception ex)
                {
                    Logger.Error(ex);
                }
                finally
                {
                    _bw = null;
                }
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

        public string GetSaveFolderPath()
        {
            return Setting.SaveDirectory;
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
                    using (var dialog = new CaptureRegionForm())
                    {
                        if (dialog.ShowDialog() != DialogResult.OK) return null;

                        handle = WindowsAPI.GetDesktopWindow();
                        var region = dialog.SelectedRegion;
                        img = CaptureWindow(handle, region.X, region.Y, region.Width, region.Height);
                    }
                    break;
                default: return null;
            }
            return img;
        }

        private Image CaptureWindow(IntPtr handle, int x, int y, int width, int height)
        {
            Image img = null;
            IntPtr srcDC = IntPtr.Zero;
            IntPtr memoryDC = IntPtr.Zero;
            IntPtr bitmap = IntPtr.Zero;
            try
            {
                // 해당 Handle의 크기 취득
                if (width == 0 || height == 0)
                {
                    var windowRect = new WinStructRect();
                    var clientRect = new WinStructRect();
                    WindowsAPI.GetWindowRect(handle, ref windowRect);
                    WindowsAPI.GetClientRect(handle, ref clientRect);
                    if (x == 0) x = windowRect.Left;
                    if (y == 0) y = windowRect.Top;
                    if (width == 0) width = clientRect.Width;
                    if (height == 0) height = windowRect.Height;

                    // Border 사이즈 제외
                    int diff = windowRect.Width - clientRect.Width;
                    if (diff > 0)
                    {
                        int borderSize = diff / 2;
                        x += borderSize;
                        y += borderSize;
                        height -= borderSize * 2;
                    }
                }

                // 해상도 스케일에 의한 크기 변경
                float scale = CalcScale();
                if (scale > 1)
                {
                    width = (int) (width * scale);
                    height = (int) (height * scale);
                }

                handle = WindowsAPI.GetDesktopWindow();
                srcDC = WindowsAPI.GetWindowDC(handle);
                memoryDC = WindowsAPI.CreateCompatibleDC(srcDC);
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

        private float CalcScale()
        {
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                IntPtr desktop = g.GetHdc();
                int logicalScreenHeight = WindowsAPI.GetDeviceCaps(desktop, (int) DeviceCap.VERTRES);
                int physicalScreenHeight = WindowsAPI.GetDeviceCaps(desktop, (int) DeviceCap.DESKTOPVERTRES);
                return (float) physicalScreenHeight / logicalScreenHeight;
            }
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