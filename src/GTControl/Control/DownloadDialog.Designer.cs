
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
            this.label_title = new System.Windows.Forms.Label();
            this.progressBar1 = new System.Windows.Forms.ProgressBar();
            this.label_per = new System.Windows.Forms.Label();
            this.themeTabControl1 = new GTControl.ThemeTabControl();
            this.tabPage1 = new System.Windows.Forms.TabPage();
            this.tabPage2 = new System.Windows.Forms.TabPage();
            this.themeTabControl1.SuspendLayout();
            this.SuspendLayout();
            // 
            // label_title
            // 
            this.label_title.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_title.Location = new System.Drawing.Point(14, 14);
            this.label_title.Margin = new System.Windows.Forms.Padding(5);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(300, 20);
            this.label_title.TabIndex = 0;
            this.label_title.Text = "Title";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // progressBar1
            // 
            this.progressBar1.Location = new System.Drawing.Point(16, 44);
            this.progressBar1.Margin = new System.Windows.Forms.Padding(5);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(300, 20);
            this.progressBar1.TabIndex = 1;
            // 
            // label_per
            // 
            this.label_per.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_per.Location = new System.Drawing.Point(14, 74);
            this.label_per.Margin = new System.Windows.Forms.Padding(5);
            this.label_per.Name = "label_per";
            this.label_per.Size = new System.Drawing.Size(300, 20);
            this.label_per.TabIndex = 0;
            this.label_per.Text = "0%";
            this.label_per.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // themeTabControl1
            // 
            this.themeTabControl1.Controls.Add(this.tabPage1);
            this.themeTabControl1.Controls.Add(this.tabPage2);
            this.themeTabControl1.Location = new System.Drawing.Point(82, 34);
            this.themeTabControl1.Name = "themeTabControl1";
            this.themeTabControl1.SelectedIndex = 0;
            this.themeTabControl1.Size = new System.Drawing.Size(200, 100);
            this.themeTabControl1.TabIndex = 2;
            // 
            // tabPage1
            // 
            this.tabPage1.Location = new System.Drawing.Point(0, 18);
            this.tabPage1.Name = "tabPage1";
            this.tabPage1.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage1.Size = new System.Drawing.Size(199, 81);
            this.tabPage1.TabIndex = 0;
            this.tabPage1.Text = "tabPage1";
            this.tabPage1.UseVisualStyleBackColor = true;
            // 
            // tabPage2
            // 
            this.tabPage2.Location = new System.Drawing.Point(0, 18);
            this.tabPage2.Name = "tabPage2";
            this.tabPage2.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage2.Size = new System.Drawing.Size(199, 81);
            this.tabPage2.TabIndex = 1;
            this.tabPage2.Text = "tabPage2";
            this.tabPage2.UseVisualStyleBackColor = true;
            // 
            // DownloadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(330, 108);
            this.Controls.Add(this.themeTabControl1);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label_per);
            this.Controls.Add(this.label_title);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.DownloadDialog_FormClosing);
            this.Load += new System.EventHandler(this.DownloadDialog_Load);
            this.themeTabControl1.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.ProgressBar progressBar1;
        private System.Windows.Forms.Label label_per;
        private ThemeTabControl themeTabControl1;
        private System.Windows.Forms.TabPage tabPage1;
        private System.Windows.Forms.TabPage tabPage2;
    }
}