namespace GTControl
{
    partial class LayoutSettingDialog
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(LayoutSettingDialog));
            this.splitContainer = new System.Windows.Forms.SplitContainer();
            this.tabControl_pages = new GTControl.ThemeTabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.propertyGrid_page = new GTControl.ThemePropertyGrid();
            this.propertyGrid_layout = new GTControl.ThemePropertyGrid();
            this.menuStrip = new System.Windows.Forms.MenuStrip();
            this.editToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_addPage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_deletePage = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_addItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_deleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_copy = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_paste = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_save = new System.Windows.Forms.ToolStripMenuItem();
            this.helpToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_specialFolder = new System.Windows.Forms.ToolStripMenuItem();
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).BeginInit();
            this.splitContainer.Panel1.SuspendLayout();
            this.splitContainer.Panel2.SuspendLayout();
            this.splitContainer.SuspendLayout();
            this.panel1.SuspendLayout();
            this.menuStrip.SuspendLayout();
            this.SuspendLayout();
            // 
            // splitContainer
            // 
            resources.ApplyResources(this.splitContainer, "splitContainer");
            this.splitContainer.Name = "splitContainer";
            // 
            // splitContainer.Panel1
            // 
            resources.ApplyResources(this.splitContainer.Panel1, "splitContainer.Panel1");
            this.splitContainer.Panel1.Controls.Add(this.tabControl_pages);
            // 
            // splitContainer.Panel2
            // 
            resources.ApplyResources(this.splitContainer.Panel2, "splitContainer.Panel2");
            this.splitContainer.Panel2.Controls.Add(this.panel1);
            // 
            // tabControl_pages
            // 
            resources.ApplyResources(this.tabControl_pages, "tabControl_pages");
            this.tabControl_pages.Name = "tabControl_pages";
            this.tabControl_pages.SelectedIndex = 0;
            this.tabControl_pages.SelectedIndexChanged += new System.EventHandler(this.tabControl_pages_SelectedIndexChanged);
            // 
            // panel1
            // 
            resources.ApplyResources(this.panel1, "panel1");
            this.panel1.Controls.Add(this.propertyGrid_page);
            this.panel1.Controls.Add(this.propertyGrid_layout);
            this.panel1.Name = "panel1";
            // 
            // propertyGrid_page
            // 
            resources.ApplyResources(this.propertyGrid_page, "propertyGrid_page");
            this.propertyGrid_page.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.propertyGrid_page.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.propertyGrid_page.Name = "propertyGrid_page";
            this.propertyGrid_page.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid_page.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // propertyGrid_layout
            // 
            resources.ApplyResources(this.propertyGrid_layout, "propertyGrid_layout");
            this.propertyGrid_layout.HelpBackColor = System.Drawing.Color.FromArgb(((int)(((byte)(18)))), ((int)(((byte)(18)))), ((int)(((byte)(18)))));
            this.propertyGrid_layout.LineColor = System.Drawing.Color.FromArgb(((int)(((byte)(250)))), ((int)(((byte)(250)))), ((int)(((byte)(250)))));
            this.propertyGrid_layout.Name = "propertyGrid_layout";
            this.propertyGrid_layout.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid_layout.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // menuStrip
            // 
            resources.ApplyResources(this.menuStrip, "menuStrip");
            this.menuStrip.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.editToolStripMenuItem,
            this.helpToolStripMenuItem});
            this.menuStrip.Name = "menuStrip";
            // 
            // editToolStripMenuItem
            // 
            resources.ApplyResources(this.editToolStripMenuItem, "editToolStripMenuItem");
            this.editToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_addPage,
            this.menuItem_deletePage,
            this.toolStripSeparator3,
            this.menuItem_addItem,
            this.menuItem_deleteItem,
            this.toolStripSeparator2,
            this.menuItem_copy,
            this.menuItem_paste,
            this.toolStripSeparator1,
            this.menuItem_save});
            this.editToolStripMenuItem.Name = "editToolStripMenuItem";
            // 
            // menuItem_addPage
            // 
            resources.ApplyResources(this.menuItem_addPage, "menuItem_addPage");
            this.menuItem_addPage.Name = "menuItem_addPage";
            this.menuItem_addPage.Click += new System.EventHandler(this.menuItem_addPage_Click);
            // 
            // menuItem_deletePage
            // 
            resources.ApplyResources(this.menuItem_deletePage, "menuItem_deletePage");
            this.menuItem_deletePage.Name = "menuItem_deletePage";
            this.menuItem_deletePage.Click += new System.EventHandler(this.menuItem_deletePage_Click);
            // 
            // toolStripSeparator3
            // 
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            // 
            // menuItem_addItem
            // 
            resources.ApplyResources(this.menuItem_addItem, "menuItem_addItem");
            this.menuItem_addItem.Name = "menuItem_addItem";
            this.menuItem_addItem.Click += new System.EventHandler(this.menuItem_addItem_Click);
            // 
            // menuItem_deleteItem
            // 
            resources.ApplyResources(this.menuItem_deleteItem, "menuItem_deleteItem");
            this.menuItem_deleteItem.Name = "menuItem_deleteItem";
            this.menuItem_deleteItem.Click += new System.EventHandler(this.menuItem_deleteItem_Click);
            // 
            // toolStripSeparator2
            // 
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            // 
            // menuItem_copy
            // 
            resources.ApplyResources(this.menuItem_copy, "menuItem_copy");
            this.menuItem_copy.Name = "menuItem_copy";
            this.menuItem_copy.Click += new System.EventHandler(this.menuItem_copy_Click);
            // 
            // menuItem_paste
            // 
            resources.ApplyResources(this.menuItem_paste, "menuItem_paste");
            this.menuItem_paste.Name = "menuItem_paste";
            this.menuItem_paste.Click += new System.EventHandler(this.menuItem_paste_Click);
            // 
            // toolStripSeparator1
            // 
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            // 
            // menuItem_save
            // 
            resources.ApplyResources(this.menuItem_save, "menuItem_save");
            this.menuItem_save.Name = "menuItem_save";
            this.menuItem_save.Click += new System.EventHandler(this.menuItem_save_Click);
            // 
            // helpToolStripMenuItem
            // 
            resources.ApplyResources(this.helpToolStripMenuItem, "helpToolStripMenuItem");
            this.helpToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_specialFolder});
            this.helpToolStripMenuItem.Name = "helpToolStripMenuItem";
            // 
            // menuItem_specialFolder
            // 
            resources.ApplyResources(this.menuItem_specialFolder, "menuItem_specialFolder");
            this.menuItem_specialFolder.Name = "menuItem_specialFolder";
            this.menuItem_specialFolder.Click += new System.EventHandler(this.menuItem_specialFolder_Click);
            // 
            // LayoutSettingDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.splitContainer);
            this.Controls.Add(this.menuStrip);
            this.KeyPreview = true;
            this.Name = "LayoutSettingDialog";
            this.ShowIcon = false;
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.LayoutSettingForm_FormClosing);
            this.Load += new System.EventHandler(this.LayoutSettingForm_Load);
            this.KeyDown += new System.Windows.Forms.KeyEventHandler(this.LayoutSettingForm_KeyDown);
            this.KeyUp += new System.Windows.Forms.KeyEventHandler(this.LayoutSettingForm_KeyUp);
            this.splitContainer.Panel1.ResumeLayout(false);
            this.splitContainer.Panel2.ResumeLayout(false);
            ((System.ComponentModel.ISupportInitialize)(this.splitContainer)).EndInit();
            this.splitContainer.ResumeLayout(false);
            this.panel1.ResumeLayout(false);
            this.menuStrip.ResumeLayout(false);
            this.menuStrip.PerformLayout();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private GTControl.ThemePropertyGrid propertyGrid_page;
        private System.Windows.Forms.MenuStrip menuStrip;
        private System.Windows.Forms.ToolStripMenuItem editToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_addPage;
        private System.Windows.Forms.ToolStripMenuItem menuItem_addItem;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
        private System.Windows.Forms.ToolStripMenuItem menuItem_deletePage;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem menuItem_deleteItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_copy;
        private System.Windows.Forms.ToolStripMenuItem menuItem_paste;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItem_save;
        private System.Windows.Forms.ToolStripMenuItem helpToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_specialFolder;
        private GTControl.ThemeTabControl tabControl_pages;
        private System.Windows.Forms.Panel panel1;
        private GTControl.ThemePropertyGrid propertyGrid_layout;
        private System.Windows.Forms.SplitContainer splitContainer;
    }
}