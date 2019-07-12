namespace GTVoiceChat
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
            this.label1 = new System.Windows.Forms.Label();
            this.comboBox_inputDevice = new System.Windows.Forms.ComboBox();
            this.button_ok = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_outputDevice = new System.Windows.Forms.ComboBox();
            this.flowLayoutPanel = new System.Windows.Forms.FlowLayoutPanel();
            this.label_name = new System.Windows.Forms.Label();
            this.textBox_name = new System.Windows.Forms.TextBox();
            this.label_ip = new System.Windows.Forms.Label();
            this.textBox_ip = new System.Windows.Forms.TextBox();
            this.label_port = new System.Windows.Forms.Label();
            this.numericUpDown_port = new System.Windows.Forms.NumericUpDown();
            this.flowLayoutPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_port)).BeginInit();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(11, 81);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Device";
            // 
            // comboBox_inputDevice
            // 
            this.comboBox_inputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_inputDevice.FormattingEnabled = true;
            this.comboBox_inputDevice.Location = new System.Drawing.Point(100, 78);
            this.comboBox_inputDevice.Name = "comboBox_inputDevice";
            this.comboBox_inputDevice.Size = new System.Drawing.Size(285, 20);
            this.comboBox_inputDevice.TabIndex = 1;
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(310, 130);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 3;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(11, 107);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Output Device";
            // 
            // comboBox_outputDevice
            // 
            this.comboBox_outputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_outputDevice.FormattingEnabled = true;
            this.comboBox_outputDevice.Location = new System.Drawing.Point(100, 104);
            this.comboBox_outputDevice.Name = "comboBox_outputDevice";
            this.comboBox_outputDevice.Size = new System.Drawing.Size(285, 20);
            this.comboBox_outputDevice.TabIndex = 2;
            // 
            // flowLayoutPanel
            // 
            this.flowLayoutPanel.Controls.Add(this.label_name);
            this.flowLayoutPanel.Controls.Add(this.textBox_name);
            this.flowLayoutPanel.Controls.Add(this.label_ip);
            this.flowLayoutPanel.Controls.Add(this.textBox_ip);
            this.flowLayoutPanel.Controls.Add(this.label_port);
            this.flowLayoutPanel.Controls.Add(this.numericUpDown_port);
            this.flowLayoutPanel.Dock = System.Windows.Forms.DockStyle.Top;
            this.flowLayoutPanel.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel.Location = new System.Drawing.Point(0, 0);
            this.flowLayoutPanel.Name = "flowLayoutPanel";
            this.flowLayoutPanel.Padding = new System.Windows.Forms.Padding(10);
            this.flowLayoutPanel.Size = new System.Drawing.Size(399, 72);
            this.flowLayoutPanel.TabIndex = 0;
            this.flowLayoutPanel.TabStop = true;
            // 
            // label_name
            // 
            this.label_name.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_name.Location = new System.Drawing.Point(13, 10);
            this.label_name.Name = "label_name";
            this.label_name.Size = new System.Drawing.Size(120, 23);
            this.label_name.TabIndex = 4;
            this.label_name.Text = "NAME";
            this.label_name.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_name
            // 
            this.textBox_name.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_name.Location = new System.Drawing.Point(13, 36);
            this.textBox_name.Name = "textBox_name";
            this.textBox_name.Size = new System.Drawing.Size(120, 23);
            this.textBox_name.TabIndex = 0;
            this.textBox_name.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_ip
            // 
            this.label_ip.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_ip.Location = new System.Drawing.Point(139, 10);
            this.label_ip.Name = "label_ip";
            this.label_ip.Size = new System.Drawing.Size(120, 23);
            this.label_ip.TabIndex = 0;
            this.label_ip.Text = "IP";
            this.label_ip.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // textBox_ip
            // 
            this.textBox_ip.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_ip.Location = new System.Drawing.Point(139, 36);
            this.textBox_ip.Name = "textBox_ip";
            this.textBox_ip.Size = new System.Drawing.Size(120, 23);
            this.textBox_ip.TabIndex = 1;
            this.textBox_ip.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            // 
            // label_port
            // 
            this.label_port.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_port.Location = new System.Drawing.Point(265, 10);
            this.label_port.Name = "label_port";
            this.label_port.Size = new System.Drawing.Size(120, 23);
            this.label_port.TabIndex = 2;
            this.label_port.Text = "PORT";
            this.label_port.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // numericUpDown_port
            // 
            this.numericUpDown_port.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.numericUpDown_port.Location = new System.Drawing.Point(265, 36);
            this.numericUpDown_port.Maximum = new decimal(new int[] {
            49151,
            0,
            0,
            0});
            this.numericUpDown_port.Minimum = new decimal(new int[] {
            1024,
            0,
            0,
            0});
            this.numericUpDown_port.Name = "numericUpDown_port";
            this.numericUpDown_port.Size = new System.Drawing.Size(120, 23);
            this.numericUpDown_port.TabIndex = 2;
            this.numericUpDown_port.TextAlign = System.Windows.Forms.HorizontalAlignment.Center;
            this.numericUpDown_port.Value = new decimal(new int[] {
            7080,
            0,
            0,
            0});
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(399, 164);
            this.Controls.Add(this.flowLayoutPanel);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.comboBox_outputDevice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_inputDevice);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Chat Setting";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.flowLayoutPanel.ResumeLayout(false);
            this.flowLayoutPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_port)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_inputDevice;
        private System.Windows.Forms.Button button_ok;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_outputDevice;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel;
        private System.Windows.Forms.Label label_name;
        private System.Windows.Forms.TextBox textBox_name;
        private System.Windows.Forms.Label label_ip;
        private System.Windows.Forms.TextBox textBox_ip;
        private System.Windows.Forms.Label label_port;
        private System.Windows.Forms.NumericUpDown numericUpDown_port;
    }
}