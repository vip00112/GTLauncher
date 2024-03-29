﻿namespace GTLauncher
{
    partial class MainForm
    {
        /// <summary>
        /// 필수 디자이너 변수입니다.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// 사용 중인 모든 리소스를 정리합니다.
        /// </summary>
        /// <param name="disposing">관리되는 리소스를 삭제해야 하면 true이고, 그렇지 않으면 false입니다.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form 디자이너에서 생성한 코드

        /// <summary>
        /// 디자이너 지원에 필요한 메서드입니다. 
        /// 이 메서드의 내용을 코드 편집기로 수정하지 마세요.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.notifyIcon = new System.Windows.Forms.NotifyIcon(this.components);
            this.notifyIconMenu = new System.Windows.Forms.ContextMenuStrip(this.components);
            this.menuItem_about = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator1 = new System.Windows.Forms.ToolStripSeparator();
            this.saveFolderToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_captureFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_recordFolder = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator2 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_setting = new System.Windows.Forms.ToolStripMenuItem();
            this.toolStripSeparator3 = new System.Windows.Forms.ToolStripSeparator();
            this.menuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconMenu;
            resources.ApplyResources(this.notifyIcon, "notifyIcon");
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyIconMenu
            // 
            resources.ApplyResources(this.notifyIconMenu, "notifyIconMenu");
            this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_about,
            this.toolStripSeparator1,
            this.saveFolderToolStripMenuItem,
            this.toolStripSeparator2,
            this.menuItem_setting,
            this.toolStripSeparator3,
            this.menuItem_exit});
            this.notifyIconMenu.Name = "notifyIconMenu";
            this.notifyIconMenu.ShowImageMargin = false;
            // 
            // menuItem_about
            // 
            this.menuItem_about.Name = "menuItem_about";
            resources.ApplyResources(this.menuItem_about, "menuItem_about");
            this.menuItem_about.Click += new System.EventHandler(this.menuItem_about_Click);
            // 
            // toolStripSeparator1
            // 
            this.toolStripSeparator1.Name = "toolStripSeparator1";
            resources.ApplyResources(this.toolStripSeparator1, "toolStripSeparator1");
            // 
            // saveFolderToolStripMenuItem
            // 
            this.saveFolderToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_captureFolder,
            this.menuItem_recordFolder});
            this.saveFolderToolStripMenuItem.Name = "saveFolderToolStripMenuItem";
            resources.ApplyResources(this.saveFolderToolStripMenuItem, "saveFolderToolStripMenuItem");
            // 
            // menuItem_captureFolder
            // 
            this.menuItem_captureFolder.Name = "menuItem_captureFolder";
            resources.ApplyResources(this.menuItem_captureFolder, "menuItem_captureFolder");
            this.menuItem_captureFolder.Click += new System.EventHandler(this.menuItem_captureFolder_Click);
            // 
            // menuItem_recordFolder
            // 
            this.menuItem_recordFolder.Name = "menuItem_recordFolder";
            resources.ApplyResources(this.menuItem_recordFolder, "menuItem_recordFolder");
            this.menuItem_recordFolder.Click += new System.EventHandler(this.menuItem_recordFolder_Click);
            // 
            // toolStripSeparator2
            // 
            this.toolStripSeparator2.Name = "toolStripSeparator2";
            resources.ApplyResources(this.toolStripSeparator2, "toolStripSeparator2");
            // 
            // menuItem_setting
            // 
            this.menuItem_setting.Name = "menuItem_setting";
            resources.ApplyResources(this.menuItem_setting, "menuItem_setting");
            this.menuItem_setting.Click += new System.EventHandler(this.menuItem_setting_Click);
            // 
            // toolStripSeparator3
            // 
            this.toolStripSeparator3.Name = "toolStripSeparator3";
            resources.ApplyResources(this.toolStripSeparator3, "toolStripSeparator3");
            // 
            // menuItem_exit
            // 
            this.menuItem_exit.Name = "menuItem_exit";
            resources.ApplyResources(this.menuItem_exit, "menuItem_exit");
            this.menuItem_exit.Click += new System.EventHandler(this.menuItem_exit_Click);
            // 
            // MainForm
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Name = "MainForm";
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.Shown += new System.EventHandler(this.MainForm_Shown);
            this.notifyIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItem_exit;
        private System.Windows.Forms.ToolStripMenuItem menuItem_captureFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator2;
        private System.Windows.Forms.ToolStripMenuItem menuItem_setting;
        private System.Windows.Forms.ToolStripMenuItem menuItem_recordFolder;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator3;
        private System.Windows.Forms.ToolStripMenuItem saveFolderToolStripMenuItem;
        private System.Windows.Forms.ToolStripMenuItem menuItem_about;
        private System.Windows.Forms.ToolStripSeparator toolStripSeparator1;
    }
}

