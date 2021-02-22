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
        public EventHandler OnCaptured;

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
            base.WndProc(ref m);
            if (m.Msg != 0x0312) return;
            if (_captureThread != null) return;

            var modifier = (KeyModifiers) ((int) m.LParam & 0xFFFF);
            var key = (Keys) (((int) m.LParam >> 16) & 0xFFFF);

            CaptureMode mode = CaptureSetting.GetCaptureMode(modifier, key);
            if (mode == CaptureMode.None) return;

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
        #endregion

        #region Public Method
        public string GetSaveFolderPath()
        {
            return CaptureSetting.SaveDirectory;
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
                    using (var dialog = new CaptureRegionDialog())
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
                string savePath = GetSaveFilePath(CaptureSetting.SaveImageFormat);
                img.Save(savePath, CaptureSetting.GetImageFormat());
                Clipboard.SetImage(img);
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
        }

        private string GetSaveFilePath(string extension)
        {
            if (!Directory.Exists(CaptureSetting.SaveDirectory))
            {
                Directory.CreateDirectory(CaptureSetting.SaveDirectory);
            }
            string fileName = string.Format("{0}.{1}", DateTime.Now.ToString("yyyy-MM-dd_HH.mm.ss"), extension);
            return Path.Combine(CaptureSetting.SaveDirectory, fileName);
        }
        #endregion

        #region Event Handler
        private void StartRecord(object s, EventArgs ev)
        {
            var form = FormUtil.FindForm<RecordForm>();
            if (form == null) return;
            if (_ffmpeg != null) return;

            var mode = (form.Mode == CaptureMode.RecordGif) ? FFmpeg.RecordMode.Gif : FFmpeg.RecordMode.Mp4;
            _ffmpeg = new FFmpeg(mode);
            _ffmpeg.OnRecordCompleted += FFmpegRecordCompleted;

            // FFmpeg.exe check and download
            if (!_ffmpeg.CheckExecuteFile()) return;

            string savePath = GetSaveFilePath("mp4");
            if (_ffmpeg.StartRecord(form.RecordRegion, savePath))
            {
                form.StartRecord();
            }
        }

        private void StopRecord(object sender, EventArgs e)
        {
            var form = FormUtil.FindForm<RecordForm>();
            if (form == null) return;
            if (_ffmpeg == null) return;

            if (_ffmpeg.StopRecord())
            {
                form.StopRecord();
            }
        }

        private void FFmpegRecordCompleted(object sender, EventArgs e)
        {
            _ffmpeg = null;
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