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
            this.SuspendLayout();
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(12, 12);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 1;
            this.button_save.Text = "SAVE";
            this.button_save.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 60);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(65, 12);
            this.label1.TabIndex = 2;
            this.label1.Text = "FullScreen";
            // 
            // textBox_fullScreen
            // 
            this.textBox_fullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_fullScreen.Location = new System.Drawing.Point(102, 57);
            this.textBox_fullScreen.Name = "textBox_fullScreen";
            this.textBox_fullScreen.ReadOnly = true;
            this.textBox_fullScreen.Size = new System.Drawing.Size(150, 21);
            this.textBox_fullScreen.TabIndex = 3;
            this.textBox_fullScreen.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(10, 87);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(86, 12);
            this.label2.TabIndex = 2;
            this.label2.Text = "ActiveProcess";
            // 
            // textBox_activeProcess
            // 
            this.textBox_activeProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_activeProcess.Location = new System.Drawing.Point(102, 84);
            this.textBox_activeProcess.Name = "textBox_activeProcess";
            this.textBox_activeProcess.ReadOnly = true;
            this.textBox_activeProcess.Size = new System.Drawing.Size(150, 21);
            this.textBox_activeProcess.TabIndex = 3;
            this.textBox_activeProcess.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(10, 114);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(44, 12);
            this.label3.TabIndex = 2;
            this.label3.Text = "Region";
            // 
            // textBox_region
            // 
            this.textBox_region.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_region.Location = new System.Drawing.Point(102, 111);
            this.textBox_region.Name = "textBox_region";
            this.textBox_region.ReadOnly = true;
            this.textBox_region.Size = new System.Drawing.Size(150, 21);
            this.textBox_region.TabIndex = 3;
            this.textBox_region.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(267, 450);
            this.Controls.Add(this.textBox_region);
            this.Controls.Add(this.label3);
            this.Controls.Add(this.textBox_activeProcess);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.textBox_fullScreen);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button_save);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Capture Setting";
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
    }
}