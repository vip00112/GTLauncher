namespace GTControl
{
    partial class NoteDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(NoteDialog));
            this.richTextBox_content = new System.Windows.Forms.RichTextBox();
            this.SuspendLayout();
            // 
            // richTextBox_content
            // 
            resources.ApplyResources(this.richTextBox_content, "richTextBox_content");
            this.richTextBox_content.Name = "richTextBox_content";
            this.richTextBox_content.ReadOnly = true;
            // 
            // NoteDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.richTextBox_content);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "NoteDialog";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.NoteDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.NoteDialog_KeyDown);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.RichTextBox richTextBox_content;
    }
}