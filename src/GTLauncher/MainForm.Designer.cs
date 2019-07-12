namespace GTLauncher
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
            this.menuItem_chat = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_chatJoin = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_chatCreate = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_captureSetting = new System.Windows.Forms.ToolStripMenuItem();
            this.menuItem_exit = new System.Windows.Forms.ToolStripMenuItem();
            this.notifyIconMenu.SuspendLayout();
            this.SuspendLayout();
            // 
            // notifyIcon
            // 
            this.notifyIcon.ContextMenuStrip = this.notifyIconMenu;
            this.notifyIcon.Icon = ((System.Drawing.Icon)(resources.GetObject("notifyIcon.Icon")));
            this.notifyIcon.Text = "GTLauncher";
            this.notifyIcon.Visible = true;
            this.notifyIcon.MouseDoubleClick += new System.Windows.Forms.MouseEventHandler(this.notifyIcon_MouseDoubleClick);
            // 
            // notifyIconMenu
            // 
            this.notifyIconMenu.Items.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_chat,
            this.menuItem_captureSetting,
            this.menuItem_exit});
            this.notifyIconMenu.Name = "notifyIconMenu";
            this.notifyIconMenu.Size = new System.Drawing.Size(181, 92);
            // 
            // menuItem_chat
            // 
            this.menuItem_chat.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.menuItem_chatJoin,
            this.menuItem_chatCreate});
            this.menuItem_chat.Name = "menuItem_chat";
            this.menuItem_chat.Size = new System.Drawing.Size(180, 22);
            this.menuItem_chat.Text = "Chat";
            this.menuItem_chat.Visible = false;
            // 
            // menuItem_chatJoin
            // 
            this.menuItem_chatJoin.Name = "menuItem_chatJoin";
            this.menuItem_chatJoin.Size = new System.Drawing.Size(180, 22);
            this.menuItem_chatJoin.Text = "Join";
            this.menuItem_chatJoin.Click += new System.EventHandler(this.menuItem_chatJoin_Click);
            // 
            // menuItem_chatCreate
            // 
            this.menuItem_chatCreate.Name = "menuItem_chatCreate";
            this.menuItem_chatCreate.Size = new System.Drawing.Size(180, 22);
            this.menuItem_chatCreate.Text = "Create";
            this.menuItem_chatCreate.Click += new System.EventHandler(this.menuItem_chatCreate_Click);
            // 
            // menuItem_captureSetting
            // 
            this.menuItem_captureSetting.Name = "menuItem_captureSetting";
            this.menuItem_captureSetting.Size = new System.Drawing.Size(180, 22);
            this.menuItem_captureSetting.Text = "Capture";
            this.menuItem_captureSetting.Click += new System.EventHandler(this.menuItem_captureSetting_Click);
            // 
            // menuItem_exit
            // 
            this.menuItem_exit.Name = "menuItem_exit";
            this.menuItem_exit.Size = new System.Drawing.Size(180, 22);
            this.menuItem_exit.Text = "Exit";
            this.menuItem_exit.Click += new System.EventHandler(this.menuItem_exit_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 400);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.None;
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Location = new System.Drawing.Point(0, 0);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainForm";
            this.ShowInTaskbar = false;
            this.SizeModeHeight = GTControl.SizeMode.Medium;
            this.SizeModeWidth = GTControl.SizeMode.Medium;
            this.Text = "MainForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.MainForm_FormClosing);
            this.Load += new System.EventHandler(this.MainForm_Load);
            this.notifyIconMenu.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.NotifyIcon notifyIcon;
        private System.Windows.Forms.ContextMenuStrip notifyIconMenu;
        private System.Windows.Forms.ToolStripMenuItem menuItem_exit;
        private System.Windows.Forms.ToolStripMenuItem menuItem_captureSetting;
        private System.Windows.Forms.ToolStripMenuItem menuItem_chat;
        private System.Windows.Forms.ToolStripMenuItem menuItem_chatJoin;
        private System.Windows.Forms.ToolStripMenuItem menuItem_chatCreate;
    }
}

