namespace GTCapture
{
    partial class HotKeySettingDialog
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
            this.checkBox_alt = new System.Windows.Forms.CheckBox();
            this.checkBox_control = new System.Windows.Forms.CheckBox();
            this.checkBox_shift = new System.Windows.Forms.CheckBox();
            this.comboBox_key = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_ok = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // checkBox_alt
            // 
            this.checkBox_alt.AutoSize = true;
            this.checkBox_alt.Location = new System.Drawing.Point(12, 12);
            this.checkBox_alt.Name = "checkBox_alt";
            this.checkBox_alt.Size = new System.Drawing.Size(38, 16);
            this.checkBox_alt.TabIndex = 0;
            this.checkBox_alt.Text = "Alt";
            this.checkBox_alt.UseVisualStyleBackColor = true;
            // 
            // checkBox_control
            // 
            this.checkBox_control.AutoSize = true;
            this.checkBox_control.Location = new System.Drawing.Point(56, 12);
            this.checkBox_control.Name = "checkBox_control";
            this.checkBox_control.Size = new System.Drawing.Size(64, 16);
            this.checkBox_control.TabIndex = 0;
            this.checkBox_control.Text = "Control";
            this.checkBox_control.UseVisualStyleBackColor = true;
            // 
            // checkBox_shift
            // 
            this.checkBox_shift.AutoSize = true;
            this.checkBox_shift.Location = new System.Drawing.Point(126, 12);
            this.checkBox_shift.Name = "checkBox_shift";
            this.checkBox_shift.Size = new System.Drawing.Size(48, 16);
            this.checkBox_shift.TabIndex = 0;
            this.checkBox_shift.Text = "Shift";
            this.checkBox_shift.UseVisualStyleBackColor = true;
            // 
            // comboBox_key
            // 
            this.comboBox_key.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_key.FormattingEnabled = true;
            this.comboBox_key.Location = new System.Drawing.Point(43, 34);
            this.comboBox_key.Name = "comboBox_key";
            this.comboBox_key.Size = new System.Drawing.Size(131, 20);
            this.comboBox_key.TabIndex = 1;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 37);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(27, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "Key";
            // 
            // button_ok
            // 
            this.button_ok.Location = new System.Drawing.Point(99, 60);
            this.button_ok.Name = "button_ok";
            this.button_ok.Size = new System.Drawing.Size(75, 23);
            this.button_ok.TabIndex = 3;
            this.button_ok.Text = "OK";
            this.button_ok.UseVisualStyleBackColor = true;
            this.button_ok.Click += new System.EventHandler(this.button_ok_Click);
            // 
            // HotKeySettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(185, 92);
            this.Controls.Add(this.button_ok);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.comboBox_key);
            this.Controls.Add(this.checkBox_shift);
            this.Controls.Add(this.checkBox_control);
            this.Controls.Add(this.checkBox_alt);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HotKeySettingDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Hotkey";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.CheckBox checkBox_alt;
        private System.Windows.Forms.CheckBox checkBox_control;
        private System.Windows.Forms.CheckBox checkBox_shift;
        private System.Windows.Forms.ComboBox comboBox_key;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_ok;
    }
}