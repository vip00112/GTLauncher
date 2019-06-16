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
            this.button_save = new System.Windows.Forms.Button();
            this.label2 = new System.Windows.Forms.Label();
            this.comboBox_outputDevice = new System.Windows.Forms.ComboBox();
            this.SuspendLayout();
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(74, 12);
            this.label1.TabIndex = 0;
            this.label1.Text = "Input Device";
            // 
            // comboBox_inputDevice
            // 
            this.comboBox_inputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_inputDevice.FormattingEnabled = true;
            this.comboBox_inputDevice.Location = new System.Drawing.Point(99, 57);
            this.comboBox_inputDevice.Name = "comboBox_inputDevice";
            this.comboBox_inputDevice.Size = new System.Drawing.Size(250, 20);
            this.comboBox_inputDevice.TabIndex = 1;
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(12, 12);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 2;
            this.button_save.Text = "SAVE";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 86);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(83, 12);
            this.label2.TabIndex = 0;
            this.label2.Text = "Output Device";
            // 
            // comboBox_outputDevice
            // 
            this.comboBox_outputDevice.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_outputDevice.FormattingEnabled = true;
            this.comboBox_outputDevice.Location = new System.Drawing.Point(99, 83);
            this.comboBox_outputDevice.Name = "comboBox_outputDevice";
            this.comboBox_outputDevice.Size = new System.Drawing.Size(250, 20);
            this.comboBox_outputDevice.TabIndex = 1;
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(362, 120);
            this.Controls.Add(this.button_save);
            this.Controls.Add(this.comboBox_outputDevice);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.comboBox_inputDevice);
            this.Controls.Add(this.label1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Audio Setting";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.ComboBox comboBox_inputDevice;
        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.ComboBox comboBox_outputDevice;
    }
}