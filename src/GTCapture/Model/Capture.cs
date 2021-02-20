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
        private BackgroundWorker _recordThread;
        private Process _ffmpegProc;
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
                    if (_recordThread != null) return;

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

        private string CreateFFmpegArgs(RecordForm form, string output)
        {
            var rec = form.RecordRec;
            int width = (rec.Width % 2 == 0) ? rec.Width : rec.Width - 1;
            int height = (rec.Height % 2 == 0) ? rec.Height : rec.Height - 1;
            int framerate = 0;
            string option = "";
            if (form.Mode == CaptureMode.RecordGif)
            {
                framerate = 15;
                option = " -qp 0 -y";
            }
            else if (form.Mode == CaptureMode.RecordVideo)
            {
                framerate = 30;
                option = " -crf 28 -pix_fmt yuv420p -movflags +faststart -y";
            }

            var sb = new StringBuilder();
            sb.Append("-rtbufsize 150M -f gdigrab -framerate " + framerate);
            sb.Append(string.Format(" -offset_x {0} -offset_y {1} -video_size {2}x{3}", rec.X, rec.Y, width, height));
            sb.Append(" -draw_mouse 1 -i desktop -c:v libx264 -r " + framerate);
            sb.Append(" -preset ultrafast -tune zerolatency");
            sb.Append(option + " \"" + output + "\"");
            return sb.ToString();
        }

        private void ConvertGif(string srcFilePath)
        {
            string ffmpegPath = Path.Combine(Application.StartupPath, "FFmpeg.exe");
            if (!File.Exists(ffmpegPath))
            {
                MessageBoxUtil.Error("Can't find 'FFmpeg.exe'");
                return;
            }

            // gif변환 args
            string dirPath = Path.GetDirectoryName(srcFilePath);
            string fileName = Path.GetFileNameWithoutExtension(srcFilePath) + ".gif";
            string newFilePath = Path.Combine(dirPath, fileName);
            string args = string.Format("-i \"{0}\" -lavfi \"palettegen=stats_mode=full[palette],[0:v][palette]paletteuse=dither=sierra2_4a\" -y \"{1}\"",
                                        srcFilePath, newFilePath);
            using (var proc = new Process())
            {
                var psi = new ProcessStartInfo()
                {
                    FileName = ffmpegPath,
                    WorkingDirectory = Path.GetDirectoryName(ffmpegPath),
                    Arguments = args,
                    UseShellExecute = false,
                    CreateNoWindow = true,
                    RedirectStandardInput = true,
                    RedirectStandardOutput = true,
                    RedirectStandardError = true,
                    StandardOutputEncoding = Encoding.UTF8,
                    StandardErrorEncoding = Encoding.UTF8
                };

                // TEST
                //proc.EnableRaisingEvents = true;
                //proc.ErrorDataReceived += delegate (object debugSender, DataReceivedEventArgs debugEvent)
                //{
                //    if (string.IsNullOrWhiteSpace(debugEvent.Data)) return;
                //    Console.WriteLine(debugEvent.Data);
                //};
                proc.StartInfo = psi;
                proc.Start();

                proc.BeginOutputReadLine();
                proc.BeginErrorReadLine();
                proc.WaitForExit();
            }

            File.Delete(srcFilePath);
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

            string ffmpegPath = Path.Combine(Application.StartupPath, "FFmpeg.exe");
            if (!File.Exists(ffmpegPath))
            {
                MessageBoxUtil.Error("Can't find 'FFmpeg.exe'");
                return;
            }

            if (!Directory.Exists(CaptureSetting.SaveDirectory))
            {
                Directory.CreateDirectory(CaptureSetting.SaveDirectory);
            }
            string savePath = GetSaveFilePath("mp4");

            form.StartRecord();

            if (_recordThread != null) return;

            _recordThread = new BackgroundWorker();
            _recordThread.DoWork += delegate (object sender, DoWorkEventArgs e)
            {
                string args = CreateFFmpegArgs(form, savePath);
                using (_ffmpegProc = new Process())
                {
                    var psi = new ProcessStartInfo()
                    {
                        FileName = ffmpegPath,
                        WorkingDirectory = Path.GetDirectoryName(ffmpegPath),
                        Arguments = args,
                        UseShellExecute = false,
                        CreateNoWindow = true,
                        RedirectStandardInput = true,
                        RedirectStandardOutput = true,
                        RedirectStandardError = true,
                        StandardOutputEncoding = Encoding.UTF8,
                        StandardErrorEncoding = Encoding.UTF8
                    };

                    // TEST
                    //_ffmpegProc.EnableRaisingEvents = true;
                    //_ffmpegProc.ErrorDataReceived += delegate (object debugSender, DataReceivedEventArgs debugEvent)
                    //{
                    //    if (string.IsNullOrWhiteSpace(debugEvent.Data)) return;
                    //    Console.WriteLine(debugEvent.Data);
                    //};
                    _ffmpegProc.StartInfo = psi;
                    _ffmpegProc.Start();

                    _ffmpegProc.BeginOutputReadLine();
                    _ffmpegProc.BeginErrorReadLine();
                    _ffmpegProc.WaitForExit();
                }
            };
            _recordThread.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
            {
                _ffmpegProc = null;
                _recordThread = null;

                if (form.Mode == CaptureMode.RecordGif)
                {
                    ConvertGif(savePath);
                }
            };
            _recordThread.RunWorkerAsync();

            var mode = FFmpeg.RecordMode.Mp4;
            if (form.Mode == CaptureMode.RecordGif) mode = FFmpeg.RecordMode.Gif;
            _ffmpeg = new FFmpeg(mode);

            string outputFilePath = "";
            _ffmpeg.StartRecord(outputFilePath);
        }

        private void StopRecord(object sender, EventArgs e)
        {
            var form = FormUtil.FindForm<RecordForm>();
            if (form == null) return;

            form.StopRecord();

            if (_ffmpegProc != null) _ffmpegProc.StandardInput.WriteLine("q");
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