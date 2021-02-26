
namespace GTCapture
{
    partial class RecordForm
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
            this.panel_header = new System.Windows.Forms.Panel();
            this.label_close = new System.Windows.Forms.Label();
            this.label_type = new System.Windows.Forms.Label();
            this.label_time = new System.Windows.Forms.Label();
            this.label_start = new System.Windows.Forms.Label();
            this.panel_header.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_header
            // 
            this.panel_header.BackColor = System.Drawing.Color.Lime;
            this.panel_header.Controls.Add(this.label_close);
            this.panel_header.Controls.Add(this.label_type);
            this.panel_header.Controls.Add(this.label_time);
            this.panel_header.Controls.Add(this.label_start);
            this.panel_header.Dock = System.Windows.Forms.DockStyle.Top;
            this.panel_header.Location = new System.Drawing.Point(0, 0);
            this.panel_header.Name = "panel_header";
            this.panel_header.Size = new System.Drawing.Size(308, 30);
            this.panel_header.TabIndex = 0;
            // 
            // label_close
            // 
            this.label_close.AutoSize = true;
            this.label_close.BackColor = System.Drawing.Color.Lime;
            this.label_close.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_close.Dock = System.Windows.Forms.DockStyle.Right;
            this.label_close.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_close.ForeColor = System.Drawing.Color.Red;
            this.label_close.Location = new System.Drawing.Point(292, 0);
            this.label_close.Margin = new System.Windows.Forms.Padding(0);
            this.label_close.Name = "label_close";
            this.label_close.Size = new System.Drawing.Size(16, 12);
            this.label_close.TabIndex = 0;
            this.label_close.Text = "×";
            this.label_close.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            this.label_close.Click += new System.EventHandler(this.label_close_Click);
            // 
            // label_type
            // 
            this.label_type.BackColor = System.Drawing.Color.Lime;
            this.label_type.Font = new System.Drawing.Font("굴림", 8F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_type.ForeColor = System.Drawing.Color.Red;
            this.label_type.Location = new System.Drawing.Point(25, 0);
            this.label_type.Margin = new System.Windows.Forms.Padding(0);
            this.label_type.Name = "label_type";
            this.label_type.Size = new System.Drawing.Size(55, 10);
            this.label_type.TabIndex = 0;
            this.label_type.Text = "GIF";
            this.label_type.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_time
            // 
            this.label_time.BackColor = System.Drawing.Color.Lime;
            this.label_time.Font = new System.Drawing.Font("굴림", 10F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_time.ForeColor = System.Drawing.Color.Red;
            this.label_time.Location = new System.Drawing.Point(25, 10);
            this.label_time.Margin = new System.Windows.Forms.Padding(0);
            this.label_time.Name = "label_time";
            this.label_time.Size = new System.Drawing.Size(55, 15);
            this.label_time.TabIndex = 0;
            this.label_time.Text = "00:00";
            this.label_time.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // label_start
            // 
            this.label_start.BackColor = System.Drawing.Color.Lime;
            this.label_start.Cursor = System.Windows.Forms.Cursors.Hand;
            this.label_start.Font = new System.Drawing.Font("굴림", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label_start.ForeColor = System.Drawing.Color.Red;
            this.label_start.Location = new System.Drawing.Point(0, 0);
            this.label_start.Margin = new System.Windows.Forms.Padding(0);
            this.label_start.Name = "label_start";
            this.label_start.Size = new System.Drawing.Size(25, 25);
            this.label_start.TabIndex = 0;
            this.label_start.Text = "●";
            this.label_start.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            this.label_start.Click += new System.EventHandler(this.label_start_Click);
            // 
            // RecordForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(308, 338);
            this.Controls.Add(this.panel_header);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "RecordForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capture";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.RecordForm_FormClosing);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CaptureBorderForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CaptureBorderForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CaptureBorderForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CaptureBorderForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CaptureBorderForm_MouseUp);
            this.panel_header.ResumeLayout(false);
            this.panel_header.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_header;
        private System.Windows.Forms.Label label_time;
        private System.Windows.Forms.Label label_start;
        private System.Windows.Forms.Label label_type;
        private System.Windows.Forms.Label label_close;
    }
}