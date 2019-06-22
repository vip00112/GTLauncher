namespace GTVoiceChat
{
    partial class OnlineUser
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

        #region 구성 요소 디자이너에서 생성한 코드

        /// <summary> 
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.label_name = new System.Windows.Forms.Label();
            this.trackBar_volumeOut = new System.Windows.Forms.TrackBar();
            this.pictureBox_volumeOut = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volumeOut)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_volumeOut)).BeginInit();
            this.SuspendLayout();
            // 
            // label_name
            // 
            this.label_name.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_name.Location = new System.Drawing.Point(5, 5);
            this.label_name.Margin = new System.Windows.Forms.Padding(5, 5, 0, 5);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(120, 20);
            this.label_name.TabIndex = 0;
            this.label_name.Text = "UserName";
            this.label_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // trackBar_volumeOut
            // 
            this.trackBar_volumeOut.AutoSize = false;
            this.trackBar_volumeOut.Location = new System.Drawing.Point(154, 5);
            this.trackBar_volumeOut.Name = "trackBar_volumeOut";
            this.trackBar_volumeOut.Size = new System.Drawing.Size(100, 20);
            this.trackBar_volumeOut.TabIndex = 1;
            this.trackBar_volumeOut.Value = 10;
            this.trackBar_volumeOut.ValueChanged += new System.EventHandler(this.trackBar_volumeOut_ValueChanged);
            // 
            // pictureBox_volumeOut
            // 
            this.pictureBox_volumeOut.Image = global::GTVoiceChat.Properties.Resources.out_on_64x64;
            this.pictureBox_volumeOut.Location = new System.Drawing.Point(128, 5);
            this.pictureBox_volumeOut.Name = "pictureBox_volumeOut";
            this.pictureBox_volumeOut.Size = new System.Drawing.Size(20, 20);
            this.pictureBox_volumeOut.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_volumeOut.TabIndex = 2;
            this.pictureBox_volumeOut.TabStop = false;
            // 
            // OnlineUser
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pictureBox_volumeOut);
            this.Controls.Add(this.trackBar_volumeOut);
            this.Controls.Add(this.label_name);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.Name = "OnlineUser";
            this.Size = new System.Drawing.Size(265, 30);
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volumeOut)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_volumeOut)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TrackBar trackBar_volumeOut;
        private System.Windows.Forms.PictureBox pictureBox_volumeOut;
    }
}
