using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
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
        private BackgroundWorker _captureThread;
        private FFmpeg _ffmpeg;

        #region Constructor
        public Capture(IntPtr hWnd)
        {
            // WndProc 이벤트가 발생하도록 핸들 등록
            AssignHandle(hWnd);

            CaptureSetting.Handle = hWnd;
        }
        #endregion

        #region Protected Method
        protected override void WndProc(ref Message m)
        {
            if (FindHotKey(m)) return;

            base.WndProc(ref m);
        }
        #endregion

        #region Private Method
        private bool FindHotKey(Message m)
        {
            if (m.Msg != WindowNative.WM_HOTKEY) return false;
            if (_captureThread != null) return false;

            var modifier = (WindowNative.KeyModifiers) ((int) m.LParam & 0xFFFF);
            var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);

            CaptureMode mode = CaptureSetting.GetCaptureMode(modifier, key);
            if (mode == CaptureMode.None) return false;

            DoCapture(mode);

            return true;
        }

        private void DoCapture(CaptureMode mode)
        {
            switch (mode)
            {
                #region Capture
                case CaptureMode.FullScreen:
                case CaptureMode.ActiveProcess:
                case CaptureMode.Region:
                    _captureThread = new BackgroundWorker();
                    _captureThread.DoWork += delegate (object sender, DoWorkEventArgs e)
                    {
                        int delay = CaptureSetting.Timer;
                        while (delay > 0)
                        {
                            Thread.Sleep(1000);
                            delay--;
                        }
                        e.Result = CaptureWindow(mode);
                    };
                    _captureThread.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
                    {
                        try
                        {
                            using (var img = e.Result as Image)
                            {
                                if (img == null) return;

                                SaveImage(img);
                            }
                        }
                        catch (Exception ex)
                        {
                            Logger.Error(ex);
                        }
                        finally
                        {
                            _captureThread = null;
                        }
                    };
                    _captureThread.RunWorkerAsync();
                    break;
                #endregion

                #region Record
                case CaptureMode.RecordGif:
                case CaptureMode.RecordVideo:
                    if (_ffmpeg != null) return;

                    var form = FormUtil.FindForm<RecordForm>();
                    if (form != null)
                    {
                        form.Cancel();
                        return;
                    }

                    form = new RecordForm(mode);
                    form.OnStart += StartRecord;
                    form.OnStop += StopRecord;
                    form.OnClose += CloseRecordForm;
                    form.Show();
                    break;
                case CaptureMode.RecordStart:
                    StartRecord(null, EventArgs.Empty);
                    break;
                case CaptureMode.RecordStop:
                    StopRecord(null, EventArgs.Empty);
                    break;
                    #endregion
            }
        }

        private Image CaptureWindow(CaptureMode mode)
        {
            Image img = null;
            IntPtr handle = IntPtr.Zero;
            Rectangle monitor = Rectangle.Empty;
            switch (mode)
            {
                case CaptureMode.FullScreen:
                    handle = WindowNative.GetDesktopWindow();
                    if (CaptureSetting.FullScreenMode == FullScreenMode.MainMonitor)
                    {
                        monitor = Screen.PrimaryScreen.Bounds;
                    }
                    else if (CaptureSetting.FullScreenMode == FullScreenMode.ActiveMonitor)
                    {
                        monitor = Screen.GetBounds(Cursor.Position);
                    }
                    else if (CaptureSetting.FullScreenMode == FullScreenMode.AllMonitor)
                    {
                        monitor = SystemInformation.VirtualScreen;
                    }
                    img = CaptureWindow(handle, monitor.X, monitor.Y, monitor.Width, monitor.Height);
                    break;
                case CaptureMode.ActiveProcess:
                    handle = WindowNative.GetForegroundWindow();
                    img = CaptureWindow(handle, 0, 0, 0, 0);
                    break;
                case CaptureMode.Region:
                    handle = WindowNative.GetDesktopWindow();
                    monitor = SystemInformation.VirtualScreen;

                    using (var tmp = CaptureWindow(handle, monitor.X, monitor.Y, monitor.Width, monitor.Height))
                    using (var dialog = new CaptureBackgroundDialog(new Point(monitor.X, monitor.Y), tmp))
                    {
                        dialog.TopMost = true;
                        dialog.BringToFront();
                        if (dialog.ShowDialog() != DialogResult.OK) return null;

                        img = dialog.Image;
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
                    var windowRect = new WindowNative.Rect();
                    var clientRect = new WindowNative.Rect();
                    WindowNative.GetWindowRect(handle, ref windowRect);
                    WindowNative.GetClientRect(handle, ref clientRect);
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

                handle = WindowNative.GetDesktopWindow();
                srcDC = WindowNative.GetWindowDC(handle);
                memoryDC = WindowNative.CreateCompatibleDC(srcDC);
                bitmap = WindowNative.CreateCompatibleBitmap(srcDC, width, height);

                IntPtr oldBitmap = WindowNative.SelectObject(memoryDC, bitmap);
                WindowNative.BitBlt(memoryDC, 0, 0, width, height, srcDC, x, y, WindowNative.SRCCOPY | WindowNative.CAPTUREBLT);
                WindowNative.SelectObject(memoryDC, oldBitmap);

                img = Image.FromHbitmap(bitmap);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                WindowNative.DeleteObject(bitmap);
                WindowNative.DeleteDC(memoryDC);
                WindowNative.ReleaseDC(handle, srcDC);
            }

            return img;
        }

        private float CalcScale()
        {
            using (var g = Graphics.FromHwnd(IntPtr.Zero))
            {
                IntPtr desktop = g.GetHdc();
                int logicalScreenHeight = WindowNative.GetDeviceCaps(desktop, (int) WindowNative.DeviceCap.VERTRES);
                int physicalScreenHeight = WindowNative.GetDeviceCaps(desktop, (int) WindowNative.DeviceCap.DESKTOPVERTRES);
                return (float) physicalScreenHeight / logicalScreenHeight;
            }
        }

        private void SaveImage(Image img)
        {
            if (img == null) return;
            try
            {
                string savePath = GetCaptureSaveFilePath(CaptureSetting.SaveImageFormat);
                img.Save(savePath, CaptureSetting.GetImageFormat());

                Clipboard.SetImage(img);
                ToastMessageForm.ShowMessage(savePath);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private string GetCaptureSaveFilePath(string extension)
        {
            if (!Directory.Exists(CaptureSetting.CaptureSaveDirectory))
            {
                Directory.CreateDirectory(CaptureSetting.CaptureSaveDirectory);
            }
            string fileName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss"), extension);
            return Path.Combine(CaptureSetting.CaptureSaveDirectory, fileName);
        }

        private string GetRecordSaveFilePath(string extension)
        {
            if (!Directory.Exists(CaptureSetting.RecordSaveDirectory))
            {
                Directory.CreateDirectory(CaptureSetting.RecordSaveDirectory);
            }
            string fileName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss"), extension);
            return Path.Combine(CaptureSetting.RecordSaveDirectory, fileName);
        }
        #endregion

        #region Event Handler
        private void StartRecord(object s, EventArgs ev)
        {
            var form = FormUtil.FindForm<RecordForm>();
            if (form == null) return;
            if (_ffmpeg != null) return;

            _ffmpeg = new FFmpeg();
            _ffmpeg.OnCompletedRecord += FFmpegOnCompletedRecord;
            _ffmpeg.OnStartingConvertToGif += FFmpegOnStartingConvertToGif;

            // FFmpeg.exe check and download
            if (!_ffmpeg.CheckAndDownloadExecuteFile())
            {
                FFmpegOnCompletedRecord(null, EventArgs.Empty);
                return;
            }

            var mode = (form.Mode == CaptureMode.RecordGif) ? FFmpeg.RecordMode.Gif : FFmpeg.RecordMode.Mp4;
            string savePath = GetRecordSaveFilePath("mp4");
            if (_ffmpeg.StartRecord(mode, form.RecordRegion, savePath))
            {
                form.StartRecord();
            }
        }

        private void StopRecord(object sender, EventArgs e)
        {
            var form = FormUtil.FindForm<RecordForm>();
            if (form == null) return;
            if (_ffmpeg == null) return;

            _ffmpeg.StopRecord();
        }

        private void FFmpegOnCompletedRecord(object sender, EventArgs e)
        {
            _ffmpeg.OnCompletedRecord -= FFmpegOnCompletedRecord;
            _ffmpeg = null;

            var form = FormUtil.FindForm<RecordForm>();
            if (form != null) form.StopRecord();
        }

        private void FFmpegOnStartingConvertToGif(object sender, EventArgs e)
        {
            var form = FormUtil.FindForm<RecordForm>();
            if (form != null) form.StartingConvertToGif();
        }

        private void CloseRecordForm(object sender, EventArgs e)
        {
            var form = sender as RecordForm;
            if (form == null) return;

            form.OnStart -= StartRecord;
            form.OnStop -= StopRecord;
            form.OnClose -= CloseRecordForm;
        }
        #endregion
    }
}