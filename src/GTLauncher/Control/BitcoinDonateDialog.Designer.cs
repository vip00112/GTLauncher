
namespace GTLauncher
{
    partial class BitcoinDonateDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(BitcoinDonateDialog));
            this.pictureBox_bitcoin = new System.Windows.Forms.PictureBox();
            this.textBox_address = new System.Windows.Forms.TextBox();
            this.button_copy = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bitcoin)).BeginInit();
            this.SuspendLayout();
            // 
            // pictureBox_bitcoin
            // 
            this.pictureBox_bitcoin.Cursor = System.Windows.Forms.Cursors.Default;
            this.pictureBox_bitcoin.Image = global::GTLauncher.Properties.Resources.donate_bitcoin_qr;
            resources.ApplyResources(this.pictureBox_bitcoin, "pictureBox_bitcoin");
            this.pictureBox_bitcoin.Name = "pictureBox_bitcoin";
            this.pictureBox_bitcoin.TabStop = false;
            // 
            // textBox_address
            // 
            resources.ApplyResources(this.textBox_address, "textBox_address");
            this.textBox_address.Name = "textBox_address";
            this.textBox_address.ReadOnly = true;
            this.textBox_address.TabStop = false;
            // 
            // button_copy
            // 
            resources.ApplyResources(this.button_copy, "button_copy");
            this.button_copy.Name = "button_copy";
            this.button_copy.UseVisualStyleBackColor = true;
            this.button_copy.Click += new System.EventHandler(this.button_copy_Click);
            // 
            // BitcoinDonateDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_copy);
            this.Controls.Add(this.textBox_address);
            this.Controls.Add(this.pictureBox_bitcoin);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "BitcoinDonateDialog";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.BitcoinDonateDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.BitcoinDonateDialog_KeyDown);
            ((System.ComponentModel.ISupportInitialize)(this.pictureBox_bitcoin)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.PictureBox pictureBox_bitcoin;
        private System.Windows.Forms.TextBox textBox_address;
        private System.Windows.Forms.Button button_copy;
    }
}