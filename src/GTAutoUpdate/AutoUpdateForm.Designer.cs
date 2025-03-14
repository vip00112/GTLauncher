
namespace GTAutoUpdate
{
    partial class AutoUpdateForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(AutoUpdateForm));
            this.progressBar = new System.Windows.Forms.ProgressBar();
            this.label_per = new System.Windows.Forms.Label();
            this.label_title = new System.Windows.Forms.Label();
            this.label_bytes = new System.Windows.Forms.Label();
            this.richTextBox_log = new System.Windows.Forms.RichTextBox();
            this.button_cancel = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // progressBar
            // 
            this.progressBar.Location = new System.Drawing.Point(12, 35);
            this.progressBar.Name = "progressBar";
            this.progressBar.Size = new System.Drawing.Size(476, 23);
            this.progressBar.TabIndex = 0;
            // 
            // label_per
            // 
            this.label_per.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_per.Location = new System.Drawing.Point(440, 61);
            this.label_per.Name = "label_per";
            this.label_per.Size = new System.Drawing.Size(50, 23);
            this.label_per.TabIndex = 1;
            this.label_per.Text = "0%";
            this.label_per.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_title
            // 
            this.label_title.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_title.Location = new System.Drawing.Point(12, 9);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(476, 23);
            this.label_title.TabIndex = 1;
            this.label_title.Text = "Download for {0}";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label_bytes
            // 
            this.label_bytes.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_bytes.Location = new System.Drawing.Point(12, 61);
            this.label_bytes.Name = "label_bytes";
            this.label_bytes.Size = new System.Drawing.Size(422, 23);
            this.label_bytes.TabIndex = 1;
            this.label_bytes.Text = "{0} of {1} bytes";
            this.label_bytes.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // richTextBox_log
            // 
            this.richTextBox_log.BackColor = System.Drawing.Color.White;
            this.richTextBox_log.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_log.Location = new System.Drawing.Point(12, 87);
            this.richTextBox_log.Name = "richTextBox_log";
            this.richTextBox_log.ReadOnly = true;
            this.richTextBox_log.Size = new System.Drawing.Size(476, 106);
            this.richTextBox_log.TabIndex = 0;
            this.richTextBox_log.TabStop = false;
            this.richTextBox_log.Text = "";
            // 
            // button_cancel
            // 
            this.button_cancel.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_cancel.Location = new System.Drawing.Point(413, 199);
            this.button_cancel.Name = "button_cancel";
            this.button_cancel.Size = new System.Drawing.Size(75, 23);
            this.button_cancel.TabIndex = 2;
            this.button_cancel.Text = "Cancel";
            this.button_cancel.UseVisualStyleBackColor = true;
            this.button_cancel.Click += new System.EventHandler(this.button_cancel_Click);
            // 
            // AutoUpdateForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.ClientSize = new System.Drawing.Size(500, 237);
            this.Controls.Add(this.button_cancel);
            this.Controls.Add(this.richTextBox_log);
            this.Controls.Add(this.label_title);
            this.Controls.Add(this.label_bytes);
            this.Controls.Add(this.label_per);
            this.Controls.Add(this.progressBar);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "AutoUpdateForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Update";
            this.Shown += new System.EventHandler(this.AutoUpdateForm_Shown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.ProgressBar progressBar;
        private System.Windows.Forms.Label label_per;
        private System.Windows.Forms.Label label_title;
        private System.Windows.Forms.Label label_bytes;
        private System.Windows.Forms.RichTextBox richTextBox_log;
        private System.Windows.Forms.Button button_cancel;
    }
}

