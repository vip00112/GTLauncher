namespace GTControl
{
    partial class CreatePageDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(CreatePageDialog));
            this.textBox_pageName = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.button_create = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // textBox_pageName
            // 
            resources.ApplyResources(this.textBox_pageName, "textBox_pageName");
            this.textBox_pageName.Name = "textBox_pageName";
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.Name = "label1";
            // 
            // button_create
            // 
            resources.ApplyResources(this.button_create, "button_create");
            this.button_create.Name = "button_create";
            this.button_create.UseVisualStyleBackColor = true;
            this.button_create.Click += new System.EventHandler(this.button_create_Click);
            // 
            // CreatePageDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.button_create);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.textBox_pageName);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "CreatePageDialog";
            this.ShowIcon = false;
            this.Load += new System.EventHandler(this.CreatePageDialog_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.CreatePageDialog_KeyDown);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox textBox_pageName;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Button button_create;
    }
}