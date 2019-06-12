﻿namespace GTControl
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
            this.propertyGrid_page = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1 = new System.Windows.Forms.MenuStrip();
            this.menuItem_add = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_addPage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_addItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_delete = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_deletePage = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_deleteItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_save = new System.Windows.Forms.ToolStripMenuItem();
            this.tabControl_pages = new System.Windows.Forms.TabControl();
            this.panel1 = new System.Windows.Forms.Panel();
            this.propertyGrid_layout = new System.Windows.Forms.PropertyGrid();
            this.menuStrip1.SuspendLayout();
            this.panel1.SuspendLayout();
            this.SuspendLayout();
            // 
            // propertyGrid_page
            // 
            this.propertyGrid_page.Dock = System.Windows.Forms.DockStyle.Fill;
            this.propertyGrid_page.HelpVisible = false;
            this.propertyGrid_page.Location = new System.Drawing.Point(0, 149);
            this.propertyGrid_page.Name = "propertyGrid_page";
            this.propertyGrid_page.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid_page.Size = new System.Drawing.Size(250, 277);
            this.propertyGrid_page.TabIndex = 1;
            this.propertyGrid_page.ToolbarVisible = false;
            this.propertyGrid_page.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
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
            this.menuItem_add.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_addPage,
            this.menuItem_addItem});
            this.menuItem_add.Name = "menuItem_add";
            this.menuItem_add.Size = new System.Drawing.Size(45, 20);
            this.menuItem_add.Text = "ADD";
            // 
            // menuItem_addPage
            // 
            this.menuItem_addPage.Name = "menuItem_addPage";
            this.menuItem_addPage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D1)));
            this.menuItem_addPage.Size = new System.Drawing.Size(141, 22);
            this.menuItem_addPage.Text = "Page";
            this.menuItem_addPage.Click += new System.EventHandler(this.menuItem_addPage_Click);
            // 
            // menuItem_addItem
            // 
            this.menuItem_addItem.Name = "menuItem_addItem";
            this.menuItem_addItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D2)));
            this.menuItem_addItem.Size = new System.Drawing.Size(141, 22);
            this.menuItem_addItem.Text = "Item";
            this.menuItem_addItem.Click += new System.EventHandler(this.menuItem_addItem_Click);
            // 
            // menuItem_delete
            // 
            this.menuItem_delete.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_deletePage,
            this.menuItem_deleteItem});
            this.menuItem_delete.Name = "menuItem_delete";
            this.menuItem_delete.Size = new System.Drawing.Size(58, 20);
            this.menuItem_delete.Text = "DELETE";
            // 
            // menuItem_deletePage
            // 
            this.menuItem_deletePage.Name = "menuItem_deletePage";
            this.menuItem_deletePage.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D3)));
            this.menuItem_deletePage.Size = new System.Drawing.Size(141, 22);
            this.menuItem_deletePage.Text = "Page";
            this.menuItem_deletePage.Click += new System.EventHandler(this.menuItem_deletePage_Click);
            // 
            // menuItem_deleteItem
            // 
            this.menuItem_deleteItem.Name = "menuItem_deleteItem";
            this.menuItem_deleteItem.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.D4)));
            this.menuItem_deleteItem.Size = new System.Drawing.Size(141, 22);
            this.menuItem_deleteItem.Text = "Item";
            this.menuItem_deleteItem.Click += new System.EventHandler(this.menuItem_deleteItem_Click);
            // 
            // menuItem_save
            // 
            this.menuItem_save.Name = "menuItem_save";
            this.menuItem_save.ShortcutKeys = ((System.Windows.Forms.Keys)((System.Windows.Forms.Keys.Control | System.Windows.Forms.Keys.S)));
            this.menuItem_save.Size = new System.Drawing.Size(48, 20);
            this.menuItem_save.Text = "SAVE";
            this.menuItem_save.Click += new System.EventHandler(this.menuItem_save_Click);
            // 
            // tabControl_pages
            // 
            this.tabControl_pages.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl_pages.Location = new System.Drawing.Point(0, 24);
            this.tabControl_pages.Name = "tabControl_pages";
            this.tabControl_pages.SelectedIndex = 0;
            this.tabControl_pages.Size = new System.Drawing.Size(587, 426);
            this.tabControl_pages.TabIndex = 1;
            this.tabControl_pages.SelectedIndexChanged += new System.EventHandler(this.tabControl_pages_SelectedIndexChanged);
            // 
            // panel1
            // 
            this.panel1.Controls.Add(this.propertyGrid_page);
            this.panel1.Controls.Add(this.propertyGrid_layout);
            this.panel1.Dock = System.Windows.Forms.DockStyle.Right;
            this.panel1.Location = new System.Drawing.Point(587, 24);
            this.panel1.Name = "panel1";
            this.panel1.Size = new System.Drawing.Size(250, 426);
            this.panel1.TabIndex = 3;
            // 
            // propertyGrid_layout
            // 
            this.propertyGrid_layout.Dock = System.Windows.Forms.DockStyle.Top;
            this.propertyGrid_layout.HelpVisible = false;
            this.propertyGrid_layout.Location = new System.Drawing.Point(0, 0);
            this.propertyGrid_layout.Name = "propertyGrid_layout";
            this.propertyGrid_layout.PropertySort = System.Windows.Forms.PropertySort.NoSort;
            this.propertyGrid_layout.Size = new System.Drawing.Size(250, 149);
            this.propertyGrid_layout.TabIndex = 1;
            this.propertyGrid_layout.ToolbarVisible = false;
            this.propertyGrid_layout.PropertyValueChanged += new System.Windows.Forms.PropertyValueChangedEventHandler(this.propertyGrid_PropertyValueChanged);
            // 
            // LayoutSettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(837, 450);
            this.Controls.Add(this.tabControl_pages);
            this.Controls.Add(this.panel1);
            this.Controls.Add(this.menuStrip1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedSingle;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "LayoutSettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Layout Setting";
            this.Load += new System.EventHandler(this.LayoutSettingForm_Load);
            this.menuStrip1.ResumeLayout(false);
            this.menuStrip1.PerformLayout();
            this.panel1.ResumeLayout(false);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.PropertyGrid propertyGrid_page;
        private System.Windows.Forms.MenuStrip menuStrip1;
        private System.Windows.Forms.ToolStripMenuItem menuItem_add;
        private System.Windows.Forms.ToolStripMenuItem menuItem_delete;
        private System.Windows.Forms.ToolStripMenuItem menuItem_save;
        private System.Windows.Forms.ToolStripMenuItem menuItem_addPage;
        private System.Windows.Forms.ToolStripMenuItem menuItem_addItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_deletePage;
        private System.Windows.Forms.ToolStripMenuItem menuItem_deleteItem;
        private System.Windows.Forms.TabControl tabControl_pages;
        private System.Windows.Forms.Panel panel1;
        private System.Windows.Forms.PropertyGrid propertyGrid_layout;
    }
}