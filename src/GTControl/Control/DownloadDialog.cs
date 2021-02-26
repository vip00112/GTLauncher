using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTLocalization;
using GTUtil;

namespace GTControl
{
    public partial class DownloadDialog : Form
    {
        private string _url;
        private string _filePath;
        private WebClient _wc;

        #region Constructor
        private DownloadDialog()
        {
            InitializeComponent();
        }

        public DownloadDialog(string url, string filePath) : this()
        {
            _url = url;
            _filePath = filePath;

            string fileName = Path.GetFileName(filePath);
            Text += " for " + fileName;
            label_title.Tag = label_title.Text;
        }
        #endregion

        #region Control Event
        private void DownloadDialog_Load(object sender, EventArgs e)
        {
            LayoutSetting.Invalidate(this);

            string title = string.Format(label_title.Tag as string, 0, 0);
            label_title.Text = title;
        }

        private void DownloadDialog_Shown(object sender, EventArgs e)
        {
            StartDownload();
        }

        private void DownloadDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_wc != null)
            {
                _wc.CancelAsync();
                e.Cancel = true;
                return;
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
                        File.Delete(_filePath);
                        MessageBoxUtil.Error(Resource.GetString(Key.DownloadCancelMsg));
                        DialogResult = DialogResult.Cancel;
                    }
                    else
                    {
                        DialogResult = DialogResult.OK;
                    }
                };
                _wc.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
                {
                    string title = string.Format(label_title.Tag as string, e.BytesReceived, e.TotalBytesToReceive);
                    label_title.Text = title;
                    label_per.Text = string.Format("{0}%", e.ProgressPercentage);
                    progressBar.Value = e.ProgressPercentage;
                    if (e.ProgressPercentage > 0) progressBar.Value = e.ProgressPercentage - 1;
                };

                try
                {
                    _wc.DownloadFileAsync(new Uri(_url), _filePath);
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    DialogResult = DialogResult.Cancel;
                }
            }
        }
        #endregion
    }
}