
namespace GTCapture
{
    partial class CaptureBorderForm
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
            this.SuspendLayout();
            // 
            // CaptureBorderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.Red;
            this.ClientSize = new System.Drawing.Size(400, 400);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CaptureBorderForm";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Capture";
            this.TopMost = true;
            this.TransparencyKey = System.Drawing.Color.Lime;
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.CaptureBorderForm_Paint);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CaptureBorderForm_KeyDown);
            this.MouseDown += new System.Windows.Forms.MouseEventHandler(this.CaptureBorderForm_MouseDown);
            this.MouseMove += new System.Windows.Forms.MouseEventHandler(this.CaptureBorderForm_MouseMove);
            this.MouseUp += new System.Windows.Forms.MouseEventHandler(this.CaptureBorderForm_MouseUp);
            this.ResumeLayout(false);

        }

        #endregion
    }
}