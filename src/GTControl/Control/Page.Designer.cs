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
            this.pageBody = new GTControl.PageBody();
            this.pageHeader = new GTControl.PageHeader();
            this.pageButton_option = new GTControl.PageButton();
            this.pageButton_back = new GTControl.PageButton();
            this.pageHeader.SuspendLayout();
            this.SuspendLayout();
            // 
            // pageBody
            // 
            this.pageBody.ColumnCount = 10;
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageBody.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.pageBody.IsEditMode = false;
            this.pageBody.Location = new System.Drawing.Point(0, 30);
            this.pageBody.Margin = new System.Windows.Forms.Padding(0);
            this.pageBody.Name = "pageBody";
            this.pageBody.RowCount = 10;
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.Size = new System.Drawing.Size(800, 370);
            this.pageBody.TabIndex = 1;
            // 
            // pageHeader
            // 
            this.pageHeader.Controls.Add(this.pageButton_option);
            this.pageHeader.Controls.Add(this.pageButton_back);
            this.pageHeader.Dock = System.Windows.Forms.DockStyle.Top;
            this.pageHeader.Location = new System.Drawing.Point(0, 0);
            this.pageHeader.Margin = new System.Windows.Forms.Padding(0);
            this.pageHeader.Name = "pageHeader";
            this.pageHeader.Size = new System.Drawing.Size(800, 30);
            this.pageHeader.TabIndex = 0;
            // 
            // pageButton_option
            // 
            this.pageButton_option.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pageButton_option.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pageButton_option.FlatAppearance.BorderSize = 0;
            this.pageButton_option.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pageButton_option.Location = new System.Drawing.Point(775, 5);
            this.pageButton_option.Margin = new System.Windows.Forms.Padding(5);
            this.pageButton_option.Name = "pageButton_option";
            this.pageButton_option.Size = new System.Drawing.Size(20, 20);
            this.pageButton_option.TabIndex = 1;
            this.pageButton_option.Text = "▤";
            this.pageButton_option.UseVisualStyleBackColor = true;
            this.pageButton_option.Click += new System.EventHandler(this.pageButton_option_Click);
            // 
            // pageButton_back
            // 
            this.pageButton_back.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pageButton_back.FlatAppearance.BorderColor = System.Drawing.Color.FromArgb(((int)(((byte)(0)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))), ((int)(((byte)(255)))));
            this.pageButton_back.FlatAppearance.BorderSize = 0;
            this.pageButton_back.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.pageButton_back.Location = new System.Drawing.Point(5, 5);
            this.pageButton_back.Margin = new System.Windows.Forms.Padding(5);
            this.pageButton_back.Name = "pageButton_back";
            this.pageButton_back.Size = new System.Drawing.Size(20, 20);
            this.pageButton_back.TabIndex = 1;
            this.pageButton_back.Text = "◀";
            this.pageButton_back.UseVisualStyleBackColor = true;
            this.pageButton_back.Click += new System.EventHandler(this.pageButton_back_Click);
            // 
            // Page
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.pageBody);
            this.Controls.Add(this.pageHeader);
            this.Name = "Page";
            this.Size = new System.Drawing.Size(800, 400);
            this.pageHeader.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private PageHeader pageHeader;
        private PageButton pageButton_back;
        private PageButton pageButton_option;
        private PageBody pageBody;
    }
}
