using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTControl;
using GTLocalization;
using GTUtil;

namespace GTCapture
{
    // GDI grab + None
    //-rtbufsize 150M -f gdigrab -framerate 60 -offset_x 0 -offset_y 0 -video_size 1920x1080 -draw_mouse 1 -i desktop -c:v libx264 -r 60 -preset ultrafast -tune zerolatency -crf 28 -pix_fmt yuv420p -movflags +faststart -y "output.mp4"

    // GDI grab + 마이크
    //-rtbufsize 150M -f gdigrab -framerate 60 -offset_x 0 -offset_y 0 -video_size 1920x1080 -draw_mouse 1 -i desktop -f dshow -i audio="마이크(USB PnP Audio Device(EEPROM))" -c:v libx264 -r 60 -preset ultrafast -tune zerolatency -crf 28 -pix_fmt yuv420p -movflags +faststart -c:a aac -ac 2 -b:a 128k -y "output.mp4"

    // GDI grab + virtual_audio_capture
    //-rtbufsize 150M -f gdigrab -framerate 60 -offset_x 0 -offset_y 0 -video_size 1920x1080 -draw_mouse 1 -i desktop -f dshow -i audio="virtual-audio-capturer" -c:v libx264 -r 60 -preset ultrafast -tune zerolatency -crf 28 -pix_fmt yuv420p -movflags +faststart -c:a aac -ac 2 -b:a 128k -y "output.mp4"

    public class FFmpeg
    {
        public const string DefaultVideoSource = "GDI grab";
        public const string DefaultAudioSource = "None";

        public enum RecordMode { Gif, Mp4 }

        public EventHandler OnStartingConvertToGif;
        public EventHandler OnCompletedRecord;

        private Process _proc;
        private BackgroundWorker _recordThread;
        private string _downloadFilePath;
        private string _executeFilePath;
        private StringBuilder _output;

        #region Constructor
        public FFmpeg()
        {
            _downloadFilePath = Path.Combine(Application.StartupPath, "Tools", "FFmpeg.zip");
            _executeFilePath = Path.Combine(Application.StartupPath, "Tools", "ffmpeg.exe");
            _output = new StringBuilder();
        }
        #endregion

        #region Public Method
        public bool CheckAndDownloadExecuteFile()
        {
            if (!CheckExecuteFile())
            {
                string msg = string.Format(Resource.GetString(Key.DownloadConfirmMsg), "FFmpeg.exe");
                if (!MessageBoxUtil.Confirm(msg)) return false;
                return DownloadExecuteFile();
            }

            return true;
        }
        public bool CheckExecuteFile()
        {
            return File.Exists(_executeFilePath);
        }

        public bool StartRecord(RecordMode mode, Rectangle recordRegion, string outputFilePath)
        {
            if (_proc != null) return false;

            try
            {
                _recordThread = new BackgroundWorker();
                _recordThread.DoWork += delegate (object sender, DoWorkEventArgs e)
                {
                    string args = CreateFFmpegArgs(mode, recordRegion, outputFilePath);
                    Excute(args);
                };
                _recordThread.RunWorkerCompleted += delegate (object sender, RunWorkerCompletedEventArgs e)
                {
                    _recordThread = null;

                    // Gif 모드일 경우 컨버팅(mp4 -> gif) 작업 추가
                    if (mode == RecordMode.Gif)
                    {
                        ConvertMp4ToGif(outputFilePath);
                    }

                    OnCompletedRecord?.Invoke(this, EventArgs.Empty);
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

        public Dictionary<string , List<string>> GetDeviceNames()
        {
            var deviceNames = new Dictionary<string, List<string>>();
            var videoDeviceNames = new List<string>() { DefaultVideoSource };
            var audioDeviceNames = new List<string>() { DefaultAudioSource };
            deviceNames.Add("Video", videoDeviceNames);
            deviceNames.Add("Audio", audioDeviceNames);

            if (!CheckExecuteFile()) return deviceNames;

            string args = "-list_devices true -f dshow -i dummy";
            Excute(args);

            string result = _output.ToString();
            if (string.IsNullOrWhiteSpace(result)) return deviceNames;

            bool isVideoDevices = false;
            var regex = new Regex(@"\[dshow @ \w+\]  ""(.+)""", RegexOptions.Compiled | RegexOptions.CultureInvariant);
            var lines = result.Split(new string[] { "\r\n", "\n" }, StringSplitOptions.None);
            foreach (var line in lines)
            {
                if (string.IsNullOrWhiteSpace(line)) continue;
                if (!line.StartsWith("[dshow @")) continue;

                // Catch Video Devices
                if (line.Contains("DirectShow video devices"))
                {
                    isVideoDevices = true;
                    continue;
                }

                // Catch Audio Devices
                if (line.Contains("DirectShow audio devices"))
                {
                    isVideoDevices = false;
                    continue;
                }

                // Get Devices
                Match match = regex.Match(line);

                if (match.Success)
                {
                    string deviceName = match.Groups[1].Value;
                    if (isVideoDevices)
                    {
                        videoDeviceNames.Add(deviceName);
                    }
                    else
                    {
                        audioDeviceNames.Add(deviceName);
                    }
                }
            }

            return deviceNames;
        }
        #endregion

        #region Private Method
        private string CreateFFmpegArgs(RecordMode mode, Rectangle recordRegion, string output)
        {
            var rec = recordRegion;
            int width = (rec.Width % 2 == 0) ? rec.Width : rec.Width - 1;
            int height = (rec.Height % 2 == 0) ? rec.Height : rec.Height - 1;
            int fps = 0;
            string audio = "";
            string option = "";
            if (mode == RecordMode.Gif)
            {
                fps = CaptureSetting.GifFPS;
                option = " -qp 0";
            }
            else if (mode == RecordMode.Mp4)
            {
                fps = CaptureSetting.VideoFPS;
                option = " -crf 28 -pix_fmt yuv420p -movflags +faststart";

                // AudioSource
                if (!string.IsNullOrWhiteSpace(CaptureSetting.AudioSource) && CaptureSetting.AudioSource != DefaultAudioSource)
                {
                    var deviceNames = GetDeviceNames()["Audio"];
                    var audioSource = deviceNames.FirstOrDefault(o => o == CaptureSetting.AudioSource);
                    if (!string.IsNullOrWhiteSpace(audioSource))
                    {
                        audio = " -f dshow -i audio=\"" + audioSource + "\"";
                        option += " -c:a aac -ac 2 -b:a 128k";
                    }
                }
            }

            var sb = new StringBuilder();
            sb.Append("-rtbufsize 150M -f gdigrab -framerate " + fps);
            sb.Append(string.Format(" -offset_x {0} -offset_y {1} -video_size {2}x{3}", rec.X, rec.Y, width, height));
            sb.Append(" -draw_mouse 1 -i desktop");
            sb.Append(audio);
            sb.Append(" -c:v libx264 -r " + fps);
            sb.Append(" -preset ultrafast -tune zerolatency");
            sb.Append(option);
            sb.Append(" -y \"" + output + "\"");
            return sb.ToString();
        }

        private void Excute(string args)
        {
            try
            {
                // 기존 CommandLine 출력 값 초기화
                _output.Clear();

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

                    _proc.EnableRaisingEvents = true;
                    _proc.OutputDataReceived += FFmpegCommandLineReceive;
                    _proc.ErrorDataReceived += FFmpegCommandLineReceive;
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
            OnStartingConvertToGif?.Invoke(this, EventArgs.Empty);

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

            string url = GithubUtil.GetDownloadUrlForLatestAsset("vip00112", "GTLauncherDependency", "FFmpeg*.zip");
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

        #region Event Handler
        private void FFmpegCommandLineReceive(object sender, DataReceivedEventArgs e)
        {
            if (string.IsNullOrWhiteSpace(e.Data)) return;
            _output.AppendLine(e.Data);
        }
        #endregion
    }
}