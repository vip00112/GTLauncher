using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTUtil;

namespace GTCapture
{
    public class FFmpeg
    {
        public enum RecordMode { Gif, Mp4 }

        private Process _proc;
        private RecordMode _mode;
        private string _ffmpegPath;

        public FFmpeg(RecordMode mode)
        {
            _mode = mode;
            _ffmpegPath = Path.Combine(Application.StartupPath, "Tools", "FFmpeg.exe");
        }

        public void StartRecord()
        {
            if (!File.Exists(_ffmpegPath))
            {
                string msg = "Can't find 'FFmpeg.exe'\r\nAre you download 'FFmpeg.exe'?";
                if (!MessageBoxUtil.Confirm(msg)) return;
            }

            switch (_mode)
            {
                case RecordMode.Gif:
                    RecordGif();
                    break;
                case RecordMode.Mp4:
                    RecordMp4();
                    break;
            }
        }

        private void RecordGif()
        {

        }

        private void RecordMp4()
        {

        }

        private void ExcuteFFmpeg(string args)
        {
            try
            {
                using (_proc = new Process())
                {
                    var psi = new ProcessStartInfo()
                    {
                        FileName = _ffmpegPath,
                        WorkingDirectory = Path.GetDirectoryName(_ffmpegPath),
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

    }
}
