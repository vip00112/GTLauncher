
namespace GTLauncher
{
    partial class AboutDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AboutDialog));
            this.pictureBox_bitcoin = new System.Windows.Forms.PictureBox();
            this.label1 = new System.Windows.Forms.Label();
            this.pictureBox1 = new System.Windows.Forms.PictureBox();
            this.label_desc = new System.Windows.Forms.Label();
            this.linkLabel_github = new System.Windows.Forms.LinkLabel();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bitcoin)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_bitcoin
            // 
            resources.ApplyResources(this.pictureBox_bitcoin, "pictureBox_bitcoin");
            this.pictureBox_bitcoin.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_bitcoin.Image = global::GTLauncher.Properties.Resources.donate_bitcoin;
            this.pictureBox_bitcoin.Name = "pictureBox_bitcoin";
            this.pictureBox_bitcoin.TabStop = false;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // pictureBox1
            // 
            resources.ApplyResources(this.pictureBox1, "pictureBox1");
            this.pictureBox1.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox1.Name = "pictureBox1";
            this.pictureBox1.TabStop = false;
            // 
            // label_desc
            // 
            resources.ApplyResources(this.label_desc, "label_desc");
            this.label_desc.Name = "label_desc";
            // 
            // linkLabel_github
            // 
            resources.ApplyResources(this.linkLabel_github, "linkLabel_github");
            this.linkLabel_github.Name = "linkLabel_github";
            this.linkLabel_github.TabStop = true;
            this.linkLabel_github.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel_github_LinkClicked);
            // 
            // AboutDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.linkLabel_github);
            this.Controls.Add(this.label_desc);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.pictureBox1);
            this.Controls.Add(this.pictureBox_bitcoin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AboutDialog";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.AboutDialog_Load);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bitcoin)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox1)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_bitcoin;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.PictureBox pictureBox1;
        private System.Windows.Forms.Label label_desc;
        private System.Windows.Forms.LinkLabel linkLabel_github;
    }
}