namespace GTVoiceChat
{
    partial class ChatClientForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ChatClientForm));
            this.richTextBox_output = new System.Windows.Forms.RichTextBox();
            this.flowLayoutPanel_user = new System.Windows.Forms.FlowLayoutPanel();
            this.button_close = new System.Windows.Forms.Button();
            this.textBox_input = new System.Windows.Forms.TextBox();
            this.pictureBox_out = new System.Windows.Forms.PictureBox();
            this.pictureBox_in = new System.Windows.Forms.PictureBox();
            this.trackBar_volumeOut = new System.Windows.Forms.TrackBar();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_out)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_in)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volumeOut)).BeginInit();
            this.SuspendLayout();
            // 
            // richTextBox_output
            // 
            this.richTextBox_output.BackColor = System.Drawing.Color.White;
            this.richTextBox_output.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.richTextBox_output.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.richTextBox_output.Location = new System.Drawing.Point(293, 77);
            this.richTextBox_output.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.richTextBox_output.Name = "richTextBox_output";
            this.richTextBox_output.ReadOnly = true;
            this.richTextBox_output.Size = new System.Drawing.Size(250, 237);
            this.richTextBox_output.TabIndex = 0;
            this.richTextBox_output.TabStop = false;
            this.richTextBox_output.Text = "";
            // 
            // flowLayoutPanel_user
            // 
            this.flowLayoutPanel_user.AutoScroll = true;
            this.flowLayoutPanel_user.BorderStyle = System.Windows.Forms.BorderStyle.FixedSingle;
            this.flowLayoutPanel_user.FlowDirection = System.Windows.Forms.FlowDirection.TopDown;
            this.flowLayoutPanel_user.Location = new System.Drawing.Point(12, 77);
            this.flowLayoutPanel_user.Name = "flowLayoutPanel_user";
            this.flowLayoutPanel_user.Padding = new System.Windows.Forms.Padding(5);
            this.flowLayoutPanel_user.Size = new System.Drawing.Size(275, 267);
            this.flowLayoutPanel_user.TabIndex = 0;
            // 
            // button_close
            // 
            this.button_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.button_close.Location = new System.Drawing.Point(12, 12);
            this.button_close.Name = "button_close";
            this.button_close.Size = new System.Drawing.Size(75, 23);
            this.button_close.TabIndex = 0;
            this.button_close.TabStop = false;
            this.button_close.Text = "EXIT";
            this.button_close.UseVisualStyleBackColor = true;
            this.button_close.Click += new System.EventHandler(this.button_close_Click);
            // 
            // textBox_input
            // 
            this.textBox_input.Location = new System.Drawing.Point(293, 321);
            this.textBox_input.Name = "textBox_input";
            this.textBox_input.Size = new System.Drawing.Size(250, 23);
            this.textBox_input.TabIndex = 1;
            this.textBox_input.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_input_KeyDown);
            // 
            // pictureBox_out
            // 
            this.pictureBox_out.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_out.Image = global::GTVoiceChat.Properties.Resources.out_on_64x64;
            this.pictureBox_out.Location = new System.Drawing.Point(48, 38);
            this.pictureBox_out.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_out.Name = "pictureBox_out";
            this.pictureBox_out.Size = new System.Drawing.Size(36, 36);
            this.pictureBox_out.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_out.TabIndex = 4;
            this.pictureBox_out.TabStop = false;
            this.pictureBox_out.Click += new System.EventHandler(this.pictureBox_out_Click);
            // 
            // pictureBox_in
            // 
            this.pictureBox_in.Cursor = System.Windows.Forms.Cursors.Hand;
            this.pictureBox_in.Image = global::GTVoiceChat.Properties.Resources.in_on_64x64;
            this.pictureBox_in.Location = new System.Drawing.Point(12, 38);
            this.pictureBox_in.Margin = new System.Windows.Forms.Padding(0);
            this.pictureBox_in.Name = "pictureBox_in";
            this.pictureBox_in.Size = new System.Drawing.Size(36, 36);
            this.pictureBox_in.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.pictureBox_in.TabIndex = 5;
            this.pictureBox_in.TabStop = false;
            this.pictureBox_in.Click += new System.EventHandler(this.pictureBox_in_Click);
            // 
            // trackBar_volumeOut
            // 
            this.trackBar_volumeOut.AutoSize = false;
            this.trackBar_volumeOut.Location = new System.Drawing.Point(87, 56);
            this.trackBar_volumeOut.Name = "trackBar_volumeOut";
            this.trackBar_volumeOut.Size = new System.Drawing.Size(200, 15);
            this.trackBar_volumeOut.TabIndex = 6;
            this.trackBar_volumeOut.Value = 10;
            this.trackBar_volumeOut.ValueChanged += new System.EventHandler(this.trackBar_volumeOut_ValueChanged);
            // 
            // ChatClientForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 15F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(555, 355);
            this.Controls.Add(this.trackBar_volumeOut);
            this.Controls.Add(this.pictureBox_out);
            this.Controls.Add(this.pictureBox_in);
            this.Controls.Add(this.textBox_input);
            this.Controls.Add(this.flowLayoutPanel_user);
            this.Controls.Add(this.button_close);
            this.Controls.Add(this.richTextBox_output);
            this.Font = new System.Drawing.Font("맑은 고딕", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 4, 3, 4);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "ChatClientForm";
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "ChatClient";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ChatClientForm_FormClosing);
            this.Load += new System.EventHandler(this.ChatClientForm_Load);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.ChatClientForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.ChatClientForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.ChatClientForm_MouseUp);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_out)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_in)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.trackBar_volumeOut)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_output;
        private System.Windows.Forms.FlowLayoutPanel flowLayoutPanel_user;
        private System.Windows.Forms.Button button_close;
        private System.Windows.Forms.TextBox textBox_input;
        private System.Windows.Forms.PictureBox pictureBox_out;
        private System.Windows.Forms.PictureBox pictureBox_in;
        private System.Windows.Forms.TrackBar trackBar_volumeOut;
    }
}