
namespace GTCapture
{
    partial class ImageEditForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(ImageEditForm));
            this.panel_canvas = new System.Windows.Forms.Panel();
            this.panel_menu = new System.Windows.Forms.Panel();
            this.numericUpDown_size = new System.Windows.Forms.NumericUpDown();
            this.themeButton_redo = new GTControl.ThemeButton();
            this.themeButton_undo = new GTControl.ThemeButton();
            this.colorPicker = new GTControl.ColorPicker();
            this.comboBox_type = new System.Windows.Forms.ComboBox();
            this.panel_menu.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_size)).BeginInit();
            this.SuspendLayout();
            // 
            // panel_canvas
            // 
            resources.ApplyResources(this.panel_canvas, "panel_canvas");
            this.panel_canvas.Name = "panel_canvas";
            // 
            // panel_menu
            // 
            resources.ApplyResources(this.panel_menu, "panel_menu");
            this.panel_menu.Controls.Add(this.numericUpDown_size);
            this.panel_menu.Controls.Add(this.themeButton_redo);
            this.panel_menu.Controls.Add(this.themeButton_undo);
            this.panel_menu.Controls.Add(this.colorPicker);
            this.panel_menu.Controls.Add(this.comboBox_type);
            this.panel_menu.Name = "panel_menu";
            // 
            // numericUpDown_size
            // 
            resources.ApplyResources(this.numericUpDown_size, "numericUpDown_size");
            this.numericUpDown_size.Increment = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_size.Maximum = new decimal(new int[] {
            50,
            0,
            0,
            0});
            this.numericUpDown_size.Minimum = new decimal(new int[] {
            5,
            0,
            0,
            0});
            this.numericUpDown_size.Name = "numericUpDown_size";
            this.numericUpDown_size.TabStop = false;
            this.numericUpDown_size.Value = new decimal(new int[] {
            10,
            0,
            0,
            0});
            this.numericUpDown_size.ValueChanged += new System.EventHandler(this.numericUpDown_size_ValueChanged);
            // 
            // themeButton_redo
            // 
            resources.ApplyResources(this.themeButton_redo, "themeButton_redo");
            this.themeButton_redo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.themeButton_redo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.themeButton_redo.Name = "themeButton_redo";
            this.themeButton_redo.Click += new System.EventHandler(this.themeButton_redo_Click);
            // 
            // themeButton_undo
            // 
            resources.ApplyResources(this.themeButton_undo, "themeButton_undo");
            this.themeButton_undo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.themeButton_undo.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.themeButton_undo.Name = "themeButton_undo";
            this.themeButton_undo.Click += new System.EventHandler(this.themeButton_undo_Click);
            // 
            // colorPicker
            // 
            resources.ApplyResources(this.colorPicker, "colorPicker");
            this.colorPicker.Color = System.Drawing.Color.Red;
            this.colorPicker.Cursor = System.Windows.Forms.Cursors.Hand;
            this.colorPicker.Name = "colorPicker";
            // 
            // comboBox_type
            // 
            resources.ApplyResources(this.comboBox_type, "comboBox_type");
            this.comboBox_type.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_type.FormattingEnabled = true;
            this.comboBox_type.Name = "comboBox_type";
            this.comboBox_type.TabStop = false;
            this.comboBox_type.SelectedIndexChanged += new System.EventHandler(this.comboBox_type_SelectedIndexChanged);
            // 
            // ImageEditForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.panel_canvas);
            this.Controls.Add(this.panel_menu);
            this.Name = "ImageEditForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ImageEditForm_FormClosing);
            this.Load += new System.EventHandler(this.ImageEditForm_Load);
            this.panel_menu.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_size)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Panel panel_canvas;
        private System.Windows.Forms.Panel panel_menu;
        private System.Windows.Forms.ComboBox comboBox_type;
        private GTControl.ColorPicker colorPicker;
        private GTControl.ThemeButton themeButton_redo;
        private GTControl.ThemeButton themeButton_undo;
        private System.Windows.Forms.NumericUpDown numericUpDown_size;
    }
}