using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Diagnostics;
using System.Drawing;
using System.IO;
using System.IO.Compression;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTAutoUpdate
{
    public partial class AutoUpdateForm : Form
    {
        private string _url;
        private string _savePath;
        private WebClient _wc;

        #region Constructor
        public AutoUpdateForm(string url, string savePath)
        {
            InitializeComponent();

            _url = url;
            _savePath = savePath;

            string fileName = Path.GetFileName(savePath);
            label_title.Text = string.Format(label_title.Text, fileName);

            label_bytes.Tag = label_bytes.Text;
            string title = string.Format(label_bytes.Tag as string, 0, 0);
            label_bytes.Text = title;

            WindowState = FormWindowState.Minimized;
            ShowInTaskbar = false;
        }
        #endregion

        #region Control Event
        private async void AutoUpdateForm_Shown(object sender, EventArgs e)
        {
            // GTLauncher 종료 대기
            await Task.Delay(500);

            if (_url == "RESTART")
            {
                StartApplication();
                return;
            }

            WindowState = FormWindowState.Normal;
            ShowInTaskbar = true;
            StartDownload();
        }

        private void button_cancel_Click(object sender, EventArgs e)
        {
            if (_wc != null)
            {
                _wc.CancelAsync();
            }
            else
            {
                Application.Exit();
            }
        }
        #endregion

        #region Private Method
        private void StartDownload()
        {
            using (_wc = new WebClient())
            {
                _wc.DownloadFileCompleted += delegate (object sender, AsyncCompletedEventArgs e)
                {
                    _wc = null;
                    if (e.Cancelled)
                    {
                        File.Delete(_savePath);
                        MessageBox.Show("Download canceld.", Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                        Application.Exit();
                        return;
                    }

                    AppendLog("Download completed.");

                    AppendLog("Start extract zip file.");
                    StartExtract();
                    AppendLog("Extract completed.");
                    Thread.Sleep(1000);

                    AppendLog("Start Application.");
                    StartApplication();
                };
                _wc.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
                {
                    string title = string.Format(label_bytes.Tag as string, e.BytesReceived, e.TotalBytesToReceive);
                    label_bytes.Text = title;
                    label_per.Text = string.Format("{0}%", e.ProgressPercentage);

                    progressBar.Value = e.ProgressPercentage;
                    if (e.ProgressPercentage > 0)
                    {
                        progressBar.Value = e.ProgressPercentage - 1;
                        progressBar.Value += 1;
                    }

                    AppendLog(string.Format("Downloaded {0}% ({1} of {2})", e.ProgressPercentage, e.BytesReceived, e.TotalBytesToReceive));
                };

                try
                {
                    if (_url.StartsWith("https://"))
                    {
                        ServicePointManager.Expect100Continue = true;
                        ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                        ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;
                    }
                    _wc.DownloadFileAsync(new Uri(_url), _savePath);
                    AppendLog("Start download file.");
                }
                catch (Exception e)
                {
                    MessageBox.Show(e.Message, Text, MessageBoxButtons.OK, MessageBoxIcon.Error);
                    Application.Exit();
                }
            }
        }

        private void StartExtract()
        {
            if (!File.Exists(_savePath)) return;

            string dirPath = Path.GetDirectoryName(_savePath);
            using (ZipArchive archive = ZipFile.OpenRead(_savePath))
            {
                foreach (ZipArchiveEntry entry in archive.Entries)
                {
                    string fileName = Path.Combine(dirPath, entry.FullName);
                    if (entry.Name == "")
                    {
                        Directory.CreateDirectory(Path.GetDirectoryName(fileName));
                        continue;
                    }
                    if (entry.Name == "GTAutoUpdate.exe")
                    {
                        fileName += ".update.tmp";
                    }
                    entry.ExtractToFile(fileName, true);
                    AppendLog(string.Format("Extract to '{0}'.", entry.Name));
                }
            }

            try { File.Delete(_savePath); } catch { }
        }

        private void StartApplication()
        {
            string dirPath = Path.GetDirectoryName(_savePath);
            string filePath = Path.Combine(dirPath, "GTLauncher.exe");

            Process.Start(filePath);
            Application.Exit();
        }

        private void AppendLog(string log)
        {
            Invoke((MethodInvoker) delegate ()
            {
                richTextBox_log.AppendText(log + "\r\n");
                richTextBox_log.SelectionStart = richTextBox_log.Text.Length;
                richTextBox_log.ScrollToCaret();
            });
        }
        #endregion
    }
}