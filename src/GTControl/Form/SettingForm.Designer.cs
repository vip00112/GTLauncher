namespace GTControl
{
    partial class SettingForm
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
            System.Windows.Forms.ListViewItem listViewItem1 = new System.Windows.Forms.ListViewItem(new string[] {
            "General",
            "General"}, -1);
            System.Windows.Forms.ListViewItem listViewItem2 = new System.Windows.Forms.ListViewItem("Layout");
            System.Windows.Forms.ListViewItem listViewItem3 = new System.Windows.Forms.ListViewItem("Capture");
            this.button_save = new System.Windows.Forms.Button();
            this.checkBox_canMove = new System.Windows.Forms.CheckBox();
            this.comboBox_theme = new System.Windows.Forms.ComboBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.button_layout = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.checkBox_runOnStartup = new System.Windows.Forms.CheckBox();
            this.label5 = new System.Windows.Forms.Label();
            this.listView = new System.Windows.Forms.ListView();
            this.columnHeader1 = ((System.Windows.Forms.ColumnHeader)(new System.Windows.Forms.ColumnHeader()));
            this.tabControl = new System.Windows.Forms.TabControl();
            this.tabPage_general = new System.Windows.Forms.TabPage();
            this.tabPage_layout = new System.Windows.Forms.TabPage();
            this.tabPage_capture = new System.Windows.Forms.TabPage();
            this.button_capture = new System.Windows.Forms.Button();
            this.label6 = new System.Windows.Forms.Label();
            this.tabControl.SuspendLayout();
            this.tabPage_general.SuspendLayout();
            this.tabPage_layout.SuspendLayout();
            this.tabPage_capture.SuspendLayout();
            this.SuspendLayout();
            // 
            // button_save
            // 
            this.button_save.Location = new System.Drawing.Point(6, 6);
            this.button_save.Name = "button_save";
            this.button_save.Size = new System.Drawing.Size(75, 23);
            this.button_save.TabIndex = 0;
            this.button_save.Text = "SAVE";
            this.button_save.UseVisualStyleBackColor = true;
            this.button_save.Click += new System.EventHandler(this.button_save_Click);
            // 
            // checkBox_canMove
            // 
            this.checkBox_canMove.AutoSize = true;
            this.checkBox_canMove.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_canMove.Location = new System.Drawing.Point(6, 79);
            this.checkBox_canMove.Name = "checkBox_canMove";
            this.checkBox_canMove.Size = new System.Drawing.Size(85, 16);
            this.checkBox_canMove.TabIndex = 2;
            this.checkBox_canMove.Text = "CanMove";
            this.checkBox_canMove.UseVisualStyleBackColor = true;
            // 
            // comboBox_theme
            // 
            this.comboBox_theme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_theme.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_theme.FormattingEnabled = true;
            this.comboBox_theme.Location = new System.Drawing.Point(62, 123);
            this.comboBox_theme.Name = "comboBox_theme";
            this.comboBox_theme.Size = new System.Drawing.Size(121, 20);
            this.comboBox_theme.TabIndex = 3;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.Location = new System.Drawing.Point(6, 126);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(50, 12);
            this.label1.TabIndex = 3;
            this.label1.Text = "Theme";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(4, 54);
            this.label2.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "- Launcher run on windows startup.";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.Location = new System.Drawing.Point(6, 146);
            this.label3.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "- Launcher change color by selected Theme.";
            // 
            // button_layout
            // 
            this.button_layout.Location = new System.Drawing.Point(6, 6);
            this.button_layout.Name = "button_layout";
            this.button_layout.Size = new System.Drawing.Size(75, 23);
            this.button_layout.TabIndex = 4;
            this.button_layout.Text = "Setting";
            this.button_layout.UseVisualStyleBackColor = true;
            this.button_layout.Click += new System.EventHandler(this.button_layout_Click);
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.Location = new System.Drawing.Point(4, 32);
            this.label4.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "- Change main layout.";
            // 
            // checkBox_runOnStartup
            // 
            this.checkBox_runOnStartup.AutoSize = true;
            this.checkBox_runOnStartup.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_runOnStartup.Location = new System.Drawing.Point(6, 35);
            this.checkBox_runOnStartup.Name = "checkBox_runOnStartup";
            this.checkBox_runOnStartup.Size = new System.Drawing.Size(113, 16);
            this.checkBox_runOnStartup.TabIndex = 1;
            this.checkBox_runOnStartup.Text = "RunOnStartup";
            this.checkBox_runOnStartup.UseVisualStyleBackColor = true;
            // 
            // label5
            // 
            this.label5.AutoSize = true;
            this.label5.Location = new System.Drawing.Point(4, 98);
            this.label5.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(301, 12);
            this.label5.TabIndex = 4;
            this.label5.Text = "- Launcher can move when mouse click on header.";
            // 
            // listView
            // 
            this.listView.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1});
            this.listView.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HeaderStyle = System.Windows.Forms.ColumnHeaderStyle.None;
            listViewItem3.StateImageIndex = 0;
            this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.OwnerDraw = true;
            this.listView.Size = new System.Drawing.Size(155, 196);
            this.listView.TabIndex = 5;
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Details;
            this.listView.DrawItem += new System.Windows.Forms.DrawListViewItemEventHandler(this.listView_DrawItem);
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Option";
            this.columnHeader1.Width = 150;
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_layout);
            this.tabControl.Controls.Add(this.tabPage_general);
            this.tabControl.Controls.Add(this.tabPage_capture);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.Location = new System.Drawing.Point(155, 0);
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(348, 196);
            this.tabControl.TabIndex = 6;
            // 
            // tabPage_general
            // 
            this.tabPage_general.Controls.Add(this.button_save);
            this.tabPage_general.Controls.Add(this.checkBox_runOnStartup);
            this.tabPage_general.Controls.Add(this.checkBox_canMove);
            this.tabPage_general.Controls.Add(this.label3);
            this.tabPage_general.Controls.Add(this.comboBox_theme);
            this.tabPage_general.Controls.Add(this.label5);
            this.tabPage_general.Controls.Add(this.label2);
            this.tabPage_general.Controls.Add(this.label1);
            this.tabPage_general.Location = new System.Drawing.Point(4, 22);
            this.tabPage_general.Name = "tabPage_general";
            this.tabPage_general.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_general.Size = new System.Drawing.Size(340, 170);
            this.tabPage_general.TabIndex = 0;
            this.tabPage_general.Text = "General";
            this.tabPage_general.UseVisualStyleBackColor = true;
            // 
            // tabPage_layout
            // 
            this.tabPage_layout.Controls.Add(this.button_layout);
            this.tabPage_layout.Controls.Add(this.label4);
            this.tabPage_layout.Location = new System.Drawing.Point(4, 22);
            this.tabPage_layout.Name = "tabPage_layout";
            this.tabPage_layout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_layout.Size = new System.Drawing.Size(340, 170);
            this.tabPage_layout.TabIndex = 1;
            this.tabPage_layout.Text = "Layout";
            this.tabPage_layout.UseVisualStyleBackColor = true;
            // 
            // tabPage_capture
            // 
            this.tabPage_capture.Controls.Add(this.button_capture);
            this.tabPage_capture.Controls.Add(this.label6);
            this.tabPage_capture.Location = new System.Drawing.Point(4, 22);
            this.tabPage_capture.Name = "tabPage_capture";
            this.tabPage_capture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_capture.Size = new System.Drawing.Size(340, 170);
            this.tabPage_capture.TabIndex = 2;
            this.tabPage_capture.Text = "Capture";
            this.tabPage_capture.UseVisualStyleBackColor = true;
            // 
            // button_capture
            // 
            this.button_capture.Location = new System.Drawing.Point(6, 6);
            this.button_capture.Name = "button_capture";
            this.button_capture.Size = new System.Drawing.Size(75, 23);
            this.button_capture.TabIndex = 5;
            this.button_capture.Text = "Setting";
            this.button_capture.UseVisualStyleBackColor = true;
            this.button_capture.Click += new System.EventHandler(this.button_capture_Click);
            // 
            // label6
            // 
            this.label6.AutoSize = true;
            this.label6.Location = new System.Drawing.Point(4, 32);
            this.label6.Margin = new System.Windows.Forms.Padding(3, 0, 3, 10);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(150, 12);
            this.label6.TabIndex = 6;
            this.label6.Text = "- Change capture setting.";
            // 
            // SettingForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(503, 196);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.listView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingForm";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage_general.ResumeLayout(false);
            this.tabPage_general.PerformLayout();
            this.tabPage_layout.ResumeLayout(false);
            this.tabPage_layout.PerformLayout();
            this.tabPage_capture.ResumeLayout(false);
            this.tabPage_capture.PerformLayout();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button button_save;
        private System.Windows.Forms.CheckBox checkBox_canMove;
        private System.Windows.Forms.ComboBox comboBox_theme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_layout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_runOnStartup;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.ListView listView;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.TabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_general;
        private System.Windows.Forms.TabPage tabPage_layout;
        private System.Windows.Forms.TabPage tabPage_capture;
        private System.Windows.Forms.Button button_capture;
        private System.Windows.Forms.Label label6;
    }
}