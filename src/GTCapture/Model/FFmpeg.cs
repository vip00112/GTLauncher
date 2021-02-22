using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTControl;
using GTUtil;

namespace GTCapture
{
    public class FFmpeg
    {
        public enum RecordMode { Gif, Mp4 }

        public EventHandler OnRecordCompleted;

        private Process _proc;
        private RecordMode _mode;
        private BackgroundWorker _recordThread;
        private string _downloadFilePath;
        private string _executeFilePath;

        #region Constructor
        public FFmpeg(RecordMode mode)
        {
            _mode = mode;
            _downloadFilePath = Path.Combine(Application.StartupPath, "Tools", "FFmpeg.zip");
            _executeFilePath = Path.Combine(Application.StartupPath, "Tools", "ffmpeg.exe");
        }
        #endregion

        #region Public Method
        public bool CheckExecuteFile()
        {
            if (!File.Exists(_executeFilePath))
            {
                string msg = "Can't find 'FFmpeg.exe'\r\nAre you download 'FFmpeg.exe'?";
                if (!MessageBoxUtil.Confirm(msg)) return false;
                return DownloadExecuteFile();
            }

            return true;
        }

        public bool StartRecord(Rectangle recordRegion, string outputFilePath)
        {
            if (_proc != null) return false;

            try
            {
                _recordThread = new BackgroundWorker();
                _recordThread.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    string args = CreateFFmpegArgs(recordRegion, outputFilePath);
                    Excute(args);
                };
                _recordThread.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
                {
                    _recordThread = null;

                    if (_mode == RecordMode.Gif)
                    {
                        ConvertMp4ToGif(outputFilePath);
                    }

                    OnRecordCompleted?.Invoke(this, EventArgs.Empty);
                };
                _recordThread.RunWorkerAsync();
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }

        public bool StopRecord()
        {
            if (_proc == null) return false;

            // _proc이 살아있으나, _recordThread가 죽은상태 : 녹화 완료 후 gif 변환중
            // gif 변환중에는 취소할 수 없다.
            if (_recordThread == null) return false;

            try
            {
                _proc.StandardInput.WriteLine("q");
                return true;
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }
        }
        #endregion

        #region Private Method
        private string CreateFFmpegArgs(Rectangle recordRegion, string output)
        {
            var rec = recordRegion;
            int width = (rec.Width % 2 == 0) ? rec.Width : rec.Width - 1;
            int height = (rec.Height % 2 == 0) ? rec.Height : rec.Height - 1;
            int framerate = 0;
            string option = "";
            if (_mode == RecordMode.Gif)
            {
                framerate = 15;
                option = " -qp 0 -y";
            }
            else if (_mode == RecordMode.Mp4)
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

        private void Excute(string args)
        {
            try
            {

                using (_proc = new Process())
                {
                    var psi = new ProcessStartInfo()
                    {
                        FileName = _executeFilePath,
                        WorkingDirectory = Path.GetDirectoryName(_executeFilePath),
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
                    _proc.StartInfo = psi;
                    _proc.Start();

                    _proc.BeginOutputReadLine();
                    _proc.BeginErrorReadLine();
                    _proc.WaitForExit();
                }
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            finally
            {
                _proc = null;
            }
        }

        private void ConvertMp4ToGif(string srcFilePath)
        {
            // gif변환 args
            string dirPath = Path.GetDirectoryName(srcFilePath);
            string fileName = Path.GetFileNameWithoutExtension(srcFilePath) + ".gif";
            string newFilePath = Path.Combine(dirPath, fileName);
            string args = string.Format("-i \"{0}\" -lavfi \"palettegen=stats_mode=full[palette],[0:v][palette]paletteuse=dither=sierra2_4a\" -y \"{1}\"",
                                        srcFilePath, newFilePath);
            Excute(args);

            try { File.Delete(srcFilePath); } catch { }
        }

        private bool DownloadExecuteFile()
        {
            string dirPath = Path.GetDirectoryName(_downloadFilePath);
            if (!Directory.Exists(dirPath))
            {
                Directory.CreateDirectory(dirPath);
            }

            string url = GithubUtil.GetDownloadUrlForLastReleaseAsset("vip00112", "GTLauncherDependency", "FFmpeg-4.3.1-x64.zip");
            if (string.IsNullOrWhiteSpace(url)) return false;

            using (var dialog = new DownloadDialog(url, _downloadFilePath))
            {
                if (dialog.ShowDialog() == DialogResult.OK)
                {
                    // Unzip
                    return ZipUtil.Unzip(_downloadFilePath, Path.GetDirectoryName(_downloadFilePath), true);
                }
            }

            return false;
        }
        #endregion
    }
}
