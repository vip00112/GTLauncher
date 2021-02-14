namespace GTCapture
{
    partial class SettingForm
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
            this.button_save = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.textBox_fullScreen = new System.Windows.Forms.TextBox();
            this.label2 = new System.Windows.Forms.Label();
            this.textBox_activeProcess = new System.Windows.Forms.TextBox();
            this.label3 = new System.Windows.Forms.Label();
            this.textBox_region = new System.Windows.Forms.TextBox();
            this.label4 = new System.Windows.Forms.Label();
            this.numericUpDown_timer = new System.Windows.Forms.NumericUpDown();
            this.textBox_dirPath = new System.Windows.Forms.TextBox();
            this.button_dirPath = new System.Windows.Forms.Button();
            this.comboBox_imageFormat = new System.Windows.Forms.ComboBox();
            this.label5 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label10 = new System.Windows.Forms.Label();
            this.label6 = new System.Windows.Forms.Label();
            this.textBox_recordRegion = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.textBox_recordStart = new System.Windows.Forms.TextBox();
            this.label12 = new System.Windows.Forms.Label();
            this.textBox_recordStop = new System.Windows.Forms.TextBox();
            this.label13 = new System.Windows.Forms.Label();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timer)).BeginInit();
            this.SuspendLayout();
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(14, 12);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(86, 23);
            this.button_save.TabIndex = 0;
            this.button_save.Text = "SAVE";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(12, 79);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(75, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "FullScreen";
            // 
            // textBox_fullScreen
            // 
            this.textBox_fullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_fullScreen.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_fullScreen.Location = new System.Drawing.Point(118, 76);
            this.textBox_fullScreen.Name = "textBox_fullScreen";
            this.textBox_fullScreen.ReadOnly = true;
            this.textBox_fullScreen.Size = new System.Drawing.Size(171, 21);
            this.textBox_fullScreen.TabIndex = 0;
            this.textBox_fullScreen.TabStop = false;
            this.textBox_fullScreen.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label2.Location = new System.Drawing.Point(12, 106);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(99, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ActiveProcess";
            // 
            // textBox_activeProcess
            // 
            this.textBox_activeProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_activeProcess.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_activeProcess.Location = new System.Drawing.Point(118, 103);
            this.textBox_activeProcess.Name = "textBox_activeProcess";
            this.textBox_activeProcess.ReadOnly = true;
            this.textBox_activeProcess.Size = new System.Drawing.Size(171, 21);
            this.textBox_activeProcess.TabIndex = 0;
            this.textBox_activeProcess.TabStop = false;
            this.textBox_activeProcess.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label3.Location = new System.Drawing.Point(12, 133);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(50, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Region";
            // 
            // textBox_region
            // 
            this.textBox_region.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_region.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_region.Location = new System.Drawing.Point(118, 130);
            this.textBox_region.Name = "textBox_region";
            this.textBox_region.ReadOnly = true;
            this.textBox_region.Size = new System.Drawing.Size(171, 21);
            this.textBox_region.TabIndex = 0;
            this.textBox_region.TabStop = false;
            this.textBox_region.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label4
            // 
            this.label4.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label4.Location = new System.Drawing.Point(12, 52);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(277, 21);
            this.label4.TabIndex = 3;
            this.label4.Text = "- Shortcuts by capture mode";
            this.label4.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // numericUpDown_timer
            // 
            this.numericUpDown_timer.Location = new System.Drawing.Point(68, 157);
            this.numericUpDown_timer.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_timer.Name = "numericUpDown_timer";
            this.numericUpDown_timer.Size = new System.Drawing.Size(80, 21);
            this.numericUpDown_timer.TabIndex = 4;
            this.numericUpDown_timer.ValueChanged += new System.EventHandler(this.numericUpDown_timer_ValueChanged);
            // 
            // textBox_dirPath
            // 
            this.textBox_dirPath.Location = new System.Drawing.Point(14, 362);
            this.textBox_dirPath.Name = "textBox_dirPath";
            this.textBox_dirPath.ReadOnly = true;
            this.textBox_dirPath.Size = new System.Drawing.Size(246, 21);
            this.textBox_dirPath.TabIndex = 5;
            // 
            // button_dirPath
            // 
            this.button_dirPath.Location = new System.Drawing.Point(266, 362);
            this.button_dirPath.Name = "button_dirPath";
            this.button_dirPath.Size = new System.Drawing.Size(23, 23);
            this.button_dirPath.TabIndex = 6;
            this.button_dirPath.Text = "≡";
            this.button_dirPath.UseVisualStyleBackColor = true;
            this.button_dirPath.Click += new System.EventHandler(this.button_dirPath_Click);
            // 
            // comboBox_imageFormat
            // 
            this.comboBox_imageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_imageFormat.FormattingEnabled = true;
            this.comboBox_imageFormat.Location = new System.Drawing.Point(68, 184);
            this.comboBox_imageFormat.Name = "comboBox_imageFormat";
            this.comboBox_imageFormat.Size = new System.Drawing.Size(80, 20);
            this.comboBox_imageFormat.TabIndex = 7;
            this.comboBox_imageFormat.SelectedIndexChanged += new System.EventHandler(this.comboBox_imageFormat_SelectedIndexChanged);
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.Location = new System.Drawing.Point(12, 159);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(43, 12);
            this.label5.TabIndex = 2;
            this.label5.Text = "Timer";
            // 
            // label7
            // 
            this.label7.AutoSize = true;
            this.label7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.Location = new System.Drawing.Point(12, 187);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(50, 12);
            this.label7.TabIndex = 2;
            this.label7.Text = "Format";
            // 
            // label8
            // 
            this.label8.AutoSize = true;
            this.label8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.Location = new System.Drawing.Point(154, 159);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(132, 12);
            this.label8.TabIndex = 3;
            this.label8.Text = "- Delay before capture";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.AutoSize = true;
            this.label9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.Location = new System.Drawing.Point(154, 187);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(134, 12);
            this.label9.TabIndex = 3;
            this.label9.Text = "- Image format at save";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label10
            // 
            this.label10.AutoSize = true;
            this.label10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.Location = new System.Drawing.Point(12, 347);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(201, 12);
            this.label10.TabIndex = 3;
            this.label10.Text = "- Save directory for capture, gif file";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.Location = new System.Drawing.Point(12, 251);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(101, 12);
            this.label6.TabIndex = 2;
            this.label6.Text = "Record Region";
            // 
            // textBox_recordRegion
            // 
            this.textBox_recordRegion.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordRegion.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordRegion.Location = new System.Drawing.Point(118, 248);
            this.textBox_recordRegion.Name = "textBox_recordRegion";
            this.textBox_recordRegion.ReadOnly = true;
            this.textBox_recordRegion.Size = new System.Drawing.Size(171, 21);
            this.textBox_recordRegion.TabIndex = 0;
            this.textBox_recordRegion.TabStop = false;
            this.textBox_recordRegion.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label11
            // 
            this.label11.AutoSize = true;
            this.label11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.Location = new System.Drawing.Point(12, 278);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(86, 12);
            this.label11.TabIndex = 2;
            this.label11.Text = "Record Start";
            // 
            // textBox_recordStart
            // 
            this.textBox_recordStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordStart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordStart.Location = new System.Drawing.Point(118, 275);
            this.textBox_recordStart.Name = "textBox_recordStart";
            this.textBox_recordStart.ReadOnly = true;
            this.textBox_recordStart.Size = new System.Drawing.Size(171, 21);
            this.textBox_recordStart.TabIndex = 0;
            this.textBox_recordStart.TabStop = false;
            this.textBox_recordStart.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label12
            // 
            this.label12.AutoSize = true;
            this.label12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.Location = new System.Drawing.Point(12, 305);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(85, 12);
            this.label12.TabIndex = 2;
            this.label12.Text = "Record Stop";
            // 
            // textBox_recordStop
            // 
            this.textBox_recordStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordStop.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordStop.Location = new System.Drawing.Point(118, 302);
            this.textBox_recordStop.Name = "textBox_recordStop";
            this.textBox_recordStop.ReadOnly = true;
            this.textBox_recordStop.Size = new System.Drawing.Size(171, 21);
            this.textBox_recordStop.TabIndex = 0;
            this.textBox_recordStop.TabStop = false;
            this.textBox_recordStop.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.Location = new System.Drawing.Point(12, 224);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(277, 21);
            this.label13.TabIndex = 3;
            this.label13.Text = "- Shortcuts by gif record";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(305, 401);
            this.Controls.Add(this.comboBox_imageFormat);
            this.Controls.Add(this.button_dirPath);
            this.Controls.Add(this.textBox_dirPath);
            this.Controls.Add(this.numericUpDown_timer);
            this.Controls.Add(this.label10);
            this.Controls.Add(this.label9);
            this.Controls.Add(this.label8);
            this.Controls.Add(this.label13);
            this.Controls.Add(this.label4);
            this.Controls.Add(this.textBox_recordStop);
            this.Controls.Add(this.textBox_region);
            this.Controls.Add(this.label7);
            this.Controls.Add(this.label5);
            this.Controls.Add(this.label12);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_recordStart);
            this.Controls.Add(this.textBox_activeProcess);
            this.Controls.Add(this.label11);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_recordRegion);
            this.Controls.Add(this.textBox_fullScreen);
            this.Controls.Add(this.label6);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_save);
            this.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Capture Setting";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timer)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.TextBox textBox_fullScreen;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.TextBox textBox_activeProcess;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.TextBox textBox_region;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.NumericUpDown numericUpDown_timer;
        private System.Windows.Forms.TextBox textBox_dirPath;
        private System.Windows.Forms.Button button_dirPath;
        private System.Windows.Forms.ComboBox comboBox_imageFormat;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.TextBox textBox_recordRegion;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.TextBox textBox_recordStart;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TextBox textBox_recordStop;
        private System.Windows.Forms.Label label13;
    }
}