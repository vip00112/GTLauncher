using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTCapture
{
    public class Capture : NativeWindow
    {
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

            var modifier = (KeyModifiers) ((int) m.LParam & 0xFFFF);
            var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);

            CaptureMode mode = Setting.GetCaptureMode(modifier, key);
            if (mode == CaptureMode.None) return;

            Image img = null;
            switch (mode)
            {
                case CaptureMode.FullScreen:
                    img = CaptureWindow(0, 0, 0, 0);
                    break;
                case CaptureMode.ActiveProcess:
                    // TODO : 활성화 프로세스 화면 캡쳐
                    break;
                case CaptureMode.Region:
                    // TODO : 선택 영역 캡쳐
                    break;
            }
            if (img == null) return;

            // TODO : 캡쳐 후 처리 (클립보드, 뷰화면, 파일로저장 등등)
            img.Save("Capture.jpg", System.Drawing.Imaging.ImageFormat.Jpeg);
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
        public Image CaptureWindow(int x, int y, int width, int height)
        {
            Image img = null;
            IntPtr handle = IntPtr.Zero;
            IntPtr src = IntPtr.Zero;
            IntPtr hdcDest = IntPtr.Zero;
            IntPtr hBitmap = IntPtr.Zero;
            try
            {
                handle = WindowsAPI.GetDesktopWindow();
                src = WindowsAPI.GetWindowDC(handle);
                hdcDest = WindowsAPI.CreateCompatibleDC(src);

                if (width == 0 || height == 0)
                {
                    var rec = new WinStructRect();
                    WindowsAPI.GetWindowRect(handle, ref rec);
                    if (width == 0 ) width = rec.Right - rec.Left;
                    if (height == 0 ) height = rec.Bottom - rec.Top;
                }
                hBitmap = WindowsAPI.CreateCompatibleBitmap(src, width, height);

                IntPtr hOld = WindowsAPI.SelectObject(hdcDest, hBitmap);
                WindowsAPI.BitBlt(hdcDest, 0, 0, width, height, src, x, y, WindowsAPI.SRCCOPY);
                WindowsAPI.SelectObject(hdcDest, hOld);

                img = Image.FromHbitmap(hBitmap);
            }
            catch (Exception e)
            {
                Console.WriteLine(e);
            }
            finally
            {
                WindowsAPI.DeleteObject(hBitmap);
                WindowsAPI.DeleteDC(hdcDest);
                WindowsAPI.ReleaseDC(handle, src);
            }

            return img;
        }
        #endregion
    }
}