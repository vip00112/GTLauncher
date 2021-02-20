
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
            this.pageButton_cancel = new GTControl.ThemeButton();
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
            this.progressBar1.Location = new System.Drawing.Point(16, 42);
            this.progressBar1.Name = "progressBar1";
            this.progressBar1.Size = new System.Drawing.Size(297, 23);
            this.progressBar1.TabIndex = 1;
            // 
            // label_per
            // 
            this.label_per.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_per.Location = new System.Drawing.Point(14, 73);
            this.label_per.Margin = new System.Windows.Forms.Padding(5);
            this.label_per.Name = "label_per";
            this.label_per.Size = new System.Drawing.Size(111, 20);
            this.label_per.TabIndex = 0;
            this.label_per.Text = "0%";
            this.label_per.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // pageButton_cancel
            // 
            this.pageButton_cancel.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pageButton_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pageButton_cancel.Location = new System.Drawing.Point(214, 73);
            this.pageButton_cancel.Margin = new System.Windows.Forms.Padding(5);
            this.pageButton_cancel.Name = "pageButton_cancel";
            this.pageButton_cancel.Size = new System.Drawing.Size(100, 20);
            this.pageButton_cancel.TabIndex = 2;
            this.pageButton_cancel.Text = "Cancel";
            this.pageButton_cancel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DownloadDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(326, 104);
            this.Controls.Add(this.pageButton_cancel);
            this.Controls.Add(this.progressBar1);
            this.Controls.Add(this.label_per);
            this.Controls.Add(this.label_title);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "DownloadDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Download";
            this.Load += new System.EventHandler(this.DownloadDialog_Load);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.ProgressBar progressBar1;
        private ThemeButton pageButton_cancel;
        private System.Windows.Forms.Label label_per;
    }
}