using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using GTUtil;

namespace GTControl
{
    public partial class DownloadDialog : Form
    {
        private string _url;
        private string _filePath;

        #region Constructor
        private DownloadDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Control Event
        public DownloadDialog(string url, string filePath) : this()
        {
            _url = url;
            _filePath = filePath;

            label_title.Tag = label_title.Text;
        }

        private void DownloadDialog_Load(object sender, EventArgs e)
        {
            LayoutSetting.Invalidate(this);
        }

        private void DownloadDialog_Shown(object sender, EventArgs e)
        {
            StartDownload();
        }
        #endregion

        #region Private Method
        private void StartDownload()
        {
            using (var wc = new WebClient())
            {
                wc.DownloadFileCompleted += delegate (object sender, AsyncCompletedEventArgs e)
                {
                    if (e.Cancelled)
                    {
                        MessageBoxUtil.Error("Download has been cancelled.");
                        DialogResult = DialogResult.Cancel;
                        return;
                    }

                    DialogResult = DialogResult.OK;
                };
                wc.DownloadProgressChanged += delegate (object sender, DownloadProgressChangedEventArgs e)
                {
                    string title = string.Format(label_title.Tag as string, e.BytesReceived, e.TotalBytesToReceive);
                    label_title.Text = title;
                    label_per.Text = string.Format("{0}%", e.ProgressPercentage);
                    progressBar.Value = e.ProgressPercentage;
                    if (e.ProgressPercentage > 0) progressBar.Value = e.ProgressPercentage - 1;
                };

                try
                {
                    wc.DownloadFileAsync(new Uri(_url), _filePath);
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
