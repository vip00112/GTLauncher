namespace GTControl
{
    partial class LayoutSettingForm
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
            this.panel_background = new System.Windows.Forms.Panel();
            this.panel_container = new System.Windows.Forms.Panel();
            this.pageBody = new GTControl.PageBody();
            this.propertyGrid = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItem_add = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_save = new System.Windows.Forms.ToolStripMenuItem();
            this.panel_background.SuspendLayout();
            this.panel_container.SuspendLayout();
            this.menuStrip1.SuspendLayout();
            this.SuspendLayout();
            // 
            // panel_background
            // 
            this.panel_background.AutoScroll = true;
            this.panel_background.Controls.Add(this.panel_container);
            this.panel_background.Dock = System.Windows.Forms.DockStyle.Fill;
            this.panel_background.Location = new System.Drawing.Point(0, 24);
            this.panel_background.Name = "panel_background";
            this.panel_background.Size = new System.Drawing.Size(587, 426);
            this.panel_background.TabIndex = 0;
            // 
            // panel_container
            // 
            this.panel_container.Controls.Add(this.pageBody);
            this.panel_container.Location = new System.Drawing.Point(0, 0);
            this.panel_container.Name = "panel_container";
            this.panel_container.Size = new System.Drawing.Size(400, 200);
            this.panel_container.TabIndex = 0;
            // 
            // pageBody
            // 
            this.pageBody.ColumnCount = 10;
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.ColumnStyles.Add(new System.Windows.Forms.ColumnStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.Dock = System.Windows.Forms.DockStyle.Fill;
            this.pageBody.GrowStyle = System.Windows.Forms.TableLayoutPanelGrowStyle.FixedSize;
            this.pageBody.IsEditMode = true;
            this.pageBody.Location = new System.Drawing.Point(0, 0);
            this.pageBody.Margin = new System.Windows.Forms.Padding(0);
            this.pageBody.Name = "pageBody";
            this.pageBody.RowCount = 10;
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.RowStyles.Add(new System.Windows.Forms.RowStyle(System.Windows.Forms.SizeType.Percent, 10F));
            this.pageBody.Size = new System.Drawing.Size(400, 200);
            this.pageBody.TabIndex = 2;
            this.pageBody.Paint += new System.Windows.Forms.PaintEventHandler(this.pageBody_Paint);
            this.pageBody.MouseDown += new System.Windows.Forms.MouseEventHandler(this.pageBody_MouseDown);
            this.pageBody.MouseMove += new System.Windows.Forms.MouseEventHandler(this.pageBody_MouseMove);
            this.pageBody.MouseUp += new System.Windows.Forms.MouseEventHandler(this.pageBody_MouseUp);
            this.pageBody.Resize += new System.EventHandler(this.pageBody_Resize);
            // 
            // propertyGrid
            // 
            this.propertyGrid.Dock = System.Windows.Forms.DockStyle.Right;
            this.propertyGrid.HelpVisible = false;
            this.propertyGrid.Location = new System.Drawing.Point(587, 24);
            this.propertyGrid.Name = "propertyGrid";
            this.propertyGrid.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid.Size = new System.Drawing.Size(250, 426);
            this.propertyGrid.TabIndex = 1;
            this.propertyGrid.ToolbarVisible = false;
            this.propertyGrid.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // menuStrip1
            // 
            this.menuStrip1.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_add,
            this.menuItem_delete,
            this.menuItem_save});
            this.menuStrip1.Location = new System.Drawing.Point(0, 0);
            this.menuStrip1.Name = "menuStrip1";
            this.menuStrip1.Size = new System.Drawing.Size(837, 24);
            this.menuStrip1.TabIndex = 2;
            this.menuStrip1.Text = "menuStrip1";
            // 
            // menuItem_add
            // 
            this.menuItem_add.Name = "menuItem_add";
            this.menuItem_add.Size = new System.Drawing.Size(45, 20);
            this.menuItem_add.Text = "ADD";
            this.menuItem_add.Click += new System.EventHandler(this.menuItem_add_Click);
            // 
            // menuItem_delete
            // 
            this.menuItem_delete.Name = "menuItem_delete";
            this.menuItem_delete.Size = new System.Drawing.Size(58, 20);
            this.menuItem_delete.Text = "DELETE";
            this.menuItem_delete.Click += new System.EventHandler(this.menuItem_delete_Click);
            // 
            // menuItem_save
            // 
            this.menuItem_save.Name = "menuItem_save";
            this.menuItem_save.Size = new System.Drawing.Size(48, 20);
            this.menuItem_save.Text = "SAVE";
            this.menuItem_save.Click += new System.EventHandler(this.menuItem_save_Click);
            // 
            // LayoutSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 450);
            this.Controls.Add(this.panel_background);
            this.Controls.Add(this.propertyGrid);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayoutSettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "LayoutSettingForm";
            this.Load += new System.EventHandler(this.LayoutSettingForm_Load);
            this.panel_background.ResumeLayout(false);
            this.panel_container.ResumeLayout(false);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Panel panel_background;
        private System.Windows.Forms.PropertyGrid propertyGrid;
        private System.Windows.Forms.Panel panel_container;
        private PageBody pageBody;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItem_add;
        private System.Windows.Forms.ToolStripMenuItem menuItem_delete;
        private System.Windows.Forms.ToolStripMenuItem menuItem_save;
    }
}