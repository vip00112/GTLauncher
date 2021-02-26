
namespace GTControl
{
    partial class DownloadDialog
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(DownloadDialog));
            this.label_title = new System.Windows.Forms.Label();
            this.label_per = new System.Windows.Forms.Label();
            this.progressBar = new GTControl.ThemeProgressBar();
            this.SuspendLayout();
            // 
            // label_title
            // 
            resources.ApplyResources(this.label_title, "label_title");
            this.label_title.Name = "label_title";
            // 
            // label_per
            // 
            resources.ApplyResources(this.label_per, "label_per");
            this.label_per.Name = "label_per";
            // 
            // progressBar
            // 
            resources.ApplyResources(this.progressBar, "progressBar");
            this.progressBar.Name = "progressBar";
            // 
            // DownloadDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.progressBar);
            this.Controls.Add(this.label_per);
            this.Controls.Add(this.label_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadDialog";
            this.ShowIcon = false;
            this.TopMost = true;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadDialog_FormClosing);
            this.Load += new System.EventHandler(this.DownloadDialog_Load);
            this.Shown += new System.EventHandler(this.DownloadDialog_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private GTControl.ThemeProgressBar progressBar;
        private System.Windows.Forms.Label label_per;
    }
}