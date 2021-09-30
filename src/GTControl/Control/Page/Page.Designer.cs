namespace GTControl
{
    partial class Page
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
            this.panel_header = new System.Windows.Forms.Panel();
            this.label_title = new System.Windows.Forms.Label();
            this.pageButton_back = new GTControl.ThemeButton();
            this.pageBody = new GTControl.PageBody();
            this.panel_header.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_header
            // 
            this.panel_header.Controls.Add(this.label_title);
            this.panel_header.Controls.Add(this.pageButton_back);
            this.panel_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_header.Location = new System.Drawing.Point(0, 0);
            this.panel_header.Margin = new System.Windows.Forms.Padding(0);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(800, 30);
            this.panel_header.TabIndex = 0;
            // 
            // label_title
            // 
            this.label_title.Dock = System.Windows.Forms.DockStyle.Fill;
            this.label_title.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_title.Location = new System.Drawing.Point(30, 0);
            this.label_title.Name = "label_title";
            this.label_title.Size = new System.Drawing.Size(770, 30);
            this.label_title.TabIndex = 2;
            this.label_title.Text = "Title";
            this.label_title.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // pageButton_back
            // 
            this.pageButton_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pageButton_back.Dock = System.Windows.Forms.DockStyle.Left;
            this.pageButton_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pageButton_back.Location = new System.Drawing.Point(0, 0);
            this.pageButton_back.Margin = new System.Windows.Forms.Padding(5);
            this.pageButton_back.Name = "pageButton_back";
            this.pageButton_back.Size = new System.Drawing.Size(30, 30);
            this.pageButton_back.TabIndex = 1;
            this.pageButton_back.Text = "◀";
            this.pageButton_back.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.pageButton_back.Click += new System.EventHandler(this.pageButton_back_Click);
            // 
            // pageBody
            // 
            this.pageBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageBody.Location = new System.Drawing.Point(0, 30);
            this.pageBody.Margin = new System.Windows.Forms.Padding(0);
            this.pageBody.Name = "pageBody";
            this.pageBody.Size = new System.Drawing.Size(800, 370);
            this.pageBody.TabIndex = 1;
            // 
            // Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pageBody);
            this.Controls.Add(this.panel_header);
            this.Name = "Page";
            this.Size = new System.Drawing.Size(800, 400);
            this.panel_header.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_header;
        private ThemeButton pageButton_back;
        private System.Windows.Forms.Label label_title;
        private PageBody pageBody;
    }
}
