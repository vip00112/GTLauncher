namespace GTLauncher
{
    partial class SettingDialog
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
            this.tabControl = new GTControl.ThemeTabControl();
            this.tabPage_general = new System.Windows.Forms.TabPage();
            this.checkBox_runOnStartup = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage_layout = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_theme = new System.Windows.Forms.ComboBox();
            this.button_layout = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage_capture = new System.Windows.Forms.TabPage();
            this.comboBox_imageFormat = new System.Windows.Forms.ComboBox();
            this.button_dirPath = new System.Windows.Forms.Button();
            this.textBox_dirPath = new System.Windows.Forms.TextBox();
            this.numericUpDown_timer = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_recordStop = new System.Windows.Forms.TextBox();
            this.textBox_region = new System.Windows.Forms.TextBox();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.label14 = new System.Windows.Forms.Label();
            this.label15 = new System.Windows.Forms.Label();
            this.textBox_recordStart = new System.Windows.Forms.TextBox();
            this.textBox_activeProcess = new System.Windows.Forms.TextBox();
            this.label16 = new System.Windows.Forms.Label();
            this.label17 = new System.Windows.Forms.Label();
            this.textBox_recordVideo = new System.Windows.Forms.TextBox();
            this.textBox_recordGif = new System.Windows.Forms.TextBox();
            this.textBox_fullScreen = new System.Windows.Forms.TextBox();
            this.label6 = new System.Windows.Forms.Label();
            this.label18 = new System.Windows.Forms.Label();
            this.label19 = new System.Windows.Forms.Label();
            this.listView = new GTControl.ThemeListView();
            this.tabControl.SuspendLayout();
            this.tabPage_general.SuspendLayout();
            this.tabPage_layout.SuspendLayout();
            this.tabPage_capture.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timer)).BeginInit();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_general);
            this.tabControl.Controls.Add(this.tabPage_layout);
            this.tabControl.Controls.Add(this.tabPage_capture);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.tabControl.Location = new System.Drawing.Point(125, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(329, 541);
            this.tabControl.TabIndex = 6;
            // 
            // tabPage_general
            // 
            this.tabPage_general.Controls.Add(this.checkBox_runOnStartup);
            this.tabPage_general.Controls.Add(this.label2);
            this.tabPage_general.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_general.Location = new System.Drawing.Point(4, 22);
            this.tabPage_general.Name = "tabPage_general";
            this.tabPage_general.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_general.Size = new System.Drawing.Size(321, 515);
            this.tabPage_general.TabIndex = 0;
            this.tabPage_general.Text = "General";
            this.tabPage_general.UseVisualStyleBackColor = true;
            // 
            // checkBox_runOnStartup
            // 
            this.checkBox_runOnStartup.AutoSize = true;
            this.checkBox_runOnStartup.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.checkBox_runOnStartup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox_runOnStartup.Location = new System.Drawing.Point(8, 8);
            this.checkBox_runOnStartup.Margin = new System.Windows.Forms.Padding(5);
            this.checkBox_runOnStartup.Name = "checkBox_runOnStartup";
            this.checkBox_runOnStartup.Size = new System.Drawing.Size(113, 16);
            this.checkBox_runOnStartup.TabIndex = 1;
            this.checkBox_runOnStartup.Text = "RunOnStartup";
            this.checkBox_runOnStartup.UseVisualStyleBackColor = true;
            this.checkBox_runOnStartup.CheckedChanged += new System.EventHandler(this.checkBox_runOnStartup_CheckedChanged);
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Location = new System.Drawing.Point(6, 29);
            this.label2.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(209, 12);
            this.label2.TabIndex = 4;
            this.label2.Text = "- Launcher run on windows startup.";
            // 
            // tabPage_layout
            // 
            this.tabPage_layout.Controls.Add(this.label1);
            this.tabPage_layout.Controls.Add(this.label3);
            this.tabPage_layout.Controls.Add(this.comboBox_theme);
            this.tabPage_layout.Controls.Add(this.button_layout);
            this.tabPage_layout.Controls.Add(this.label4);
            this.tabPage_layout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_layout.Location = new System.Drawing.Point(4, 22);
            this.tabPage_layout.Name = "tabPage_layout";
            this.tabPage_layout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_layout.Size = new System.Drawing.Size(321, 515);
            this.tabPage_layout.TabIndex = 1;
            this.tabPage_layout.Text = "Layout";
            this.tabPage_layout.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            this.label1.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Location = new System.Drawing.Point(6, 73);
            this.label1.Margin = new System.Windows.Forms.Padding(5);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(120, 20);
            this.label1.TabIndex = 3;
            this.label1.Text = "Theme";
            this.label1.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Location = new System.Drawing.Point(6, 98);
            this.label3.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(264, 12);
            this.label3.TabIndex = 4;
            this.label3.Text = "- Launcher change color by selected Theme.";
            // 
            // comboBox_theme
            // 
            this.comboBox_theme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_theme.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.comboBox_theme.FormattingEnabled = true;
            this.comboBox_theme.Location = new System.Drawing.Point(136, 73);
            this.comboBox_theme.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox_theme.Name = "comboBox_theme";
            this.comboBox_theme.Size = new System.Drawing.Size(175, 20);
            this.comboBox_theme.TabIndex = 3;
            this.comboBox_theme.SelectedIndexChanged += new System.EventHandler(this.comboBox_theme_SelectedIndexChanged);
            // 
            // button_layout
            // 
            this.button_layout.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.button_layout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_layout.Location = new System.Drawing.Point(8, 8);
            this.button_layout.Margin = new System.Windows.Forms.Padding(5);
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
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Location = new System.Drawing.Point(6, 36);
            this.label4.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(133, 12);
            this.label4.TabIndex = 4;
            this.label4.Text = "- Change main layout.";
            // 
            // tabPage_capture
            // 
            this.tabPage_capture.Controls.Add(this.comboBox_imageFormat);
            this.tabPage_capture.Controls.Add(this.button_dirPath);
            this.tabPage_capture.Controls.Add(this.textBox_dirPath);
            this.tabPage_capture.Controls.Add(this.numericUpDown_timer);
            this.tabPage_capture.Controls.Add(this.label10);
            this.tabPage_capture.Controls.Add(this.label9);
            this.tabPage_capture.Controls.Add(this.label8);
            this.tabPage_capture.Controls.Add(this.label13);
            this.tabPage_capture.Controls.Add(this.label7);
            this.tabPage_capture.Controls.Add(this.textBox_recordStop);
            this.tabPage_capture.Controls.Add(this.textBox_region);
            this.tabPage_capture.Controls.Add(this.label11);
            this.tabPage_capture.Controls.Add(this.label12);
            this.tabPage_capture.Controls.Add(this.label14);
            this.tabPage_capture.Controls.Add(this.label15);
            this.tabPage_capture.Controls.Add(this.textBox_recordStart);
            this.tabPage_capture.Controls.Add(this.textBox_activeProcess);
            this.tabPage_capture.Controls.Add(this.label16);
            this.tabPage_capture.Controls.Add(this.label17);
            this.tabPage_capture.Controls.Add(this.textBox_recordVideo);
            this.tabPage_capture.Controls.Add(this.textBox_recordGif);
            this.tabPage_capture.Controls.Add(this.textBox_fullScreen);
            this.tabPage_capture.Controls.Add(this.label6);
            this.tabPage_capture.Controls.Add(this.label18);
            this.tabPage_capture.Controls.Add(this.label19);
            this.tabPage_capture.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_capture.Location = new System.Drawing.Point(4, 22);
            this.tabPage_capture.Name = "tabPage_capture";
            this.tabPage_capture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_capture.Size = new System.Drawing.Size(321, 515);
            this.tabPage_capture.TabIndex = 2;
            this.tabPage_capture.Text = "Capture";
            this.tabPage_capture.UseVisualStyleBackColor = true;
            // 
            // comboBox_imageFormat
            // 
            this.comboBox_imageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_imageFormat.FormattingEnabled = true;
            this.comboBox_imageFormat.Location = new System.Drawing.Point(138, 80);
            this.comboBox_imageFormat.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox_imageFormat.Name = "comboBox_imageFormat";
            this.comboBox_imageFormat.Size = new System.Drawing.Size(175, 20);
            this.comboBox_imageFormat.TabIndex = 30;
            this.comboBox_imageFormat.SelectedIndexChanged += new System.EventHandler(this.comboBox_imageFormat_SelectedIndexChanged);
            // 
            // button_dirPath
            // 
            this.button_dirPath.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_dirPath.Location = new System.Drawing.Point(290, 450);
            this.button_dirPath.Margin = new System.Windows.Forms.Padding(5);
            this.button_dirPath.Name = "button_dirPath";
            this.button_dirPath.Size = new System.Drawing.Size(23, 23);
            this.button_dirPath.TabIndex = 29;
            this.button_dirPath.Text = "≡";
            this.button_dirPath.UseVisualStyleBackColor = true;
            this.button_dirPath.Click += new System.EventHandler(this.button_dirPath_Click);
            // 
            // textBox_dirPath
            // 
            this.textBox_dirPath.Location = new System.Drawing.Point(8, 450);
            this.textBox_dirPath.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_dirPath.Name = "textBox_dirPath";
            this.textBox_dirPath.ReadOnly = true;
            this.textBox_dirPath.Size = new System.Drawing.Size(272, 21);
            this.textBox_dirPath.TabIndex = 28;
            // 
            // numericUpDown_timer
            // 
            this.numericUpDown_timer.Location = new System.Drawing.Point(138, 8);
            this.numericUpDown_timer.Margin = new System.Windows.Forms.Padding(5);
            this.numericUpDown_timer.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_timer.Name = "numericUpDown_timer";
            this.numericUpDown_timer.Size = new System.Drawing.Size(175, 21);
            this.numericUpDown_timer.TabIndex = 27;
            this.numericUpDown_timer.ValueChanged += new System.EventHandler(this.numericUpDown_timer_ValueChanged);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(8, 476);
            this.label10.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(305, 21);
            this.label10.TabIndex = 26;
            this.label10.Text = "- Save directory for capture, gif file";
            this.label10.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label9
            // 
            this.label9.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Location = new System.Drawing.Point(8, 106);
            this.label9.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label9.Name = "label9";
            this.label9.Size = new System.Drawing.Size(305, 20);
            this.label9.TabIndex = 25;
            this.label9.Text = "- Image format at save";
            this.label9.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label8
            // 
            this.label8.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Location = new System.Drawing.Point(6, 34);
            this.label8.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label8.Name = "label8";
            this.label8.Size = new System.Drawing.Size(307, 21);
            this.label8.TabIndex = 24;
            this.label8.Text = "- Delay before capture";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(8, 404);
            this.label13.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(305, 21);
            this.label13.TabIndex = 23;
            this.label13.Text = "- Shortcuts by record";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(8, 239);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(305, 21);
            this.label7.TabIndex = 22;
            this.label7.Text = "- Shortcuts by capture mode";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_recordStop
            // 
            this.textBox_recordStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordStop.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordStop.Location = new System.Drawing.Point(138, 378);
            this.textBox_recordStop.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_recordStop.Name = "textBox_recordStop";
            this.textBox_recordStop.ReadOnly = true;
            this.textBox_recordStop.Size = new System.Drawing.Size(175, 21);
            this.textBox_recordStop.TabIndex = 12;
            this.textBox_recordStop.TabStop = false;
            this.textBox_recordStop.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // textBox_region
            // 
            this.textBox_region.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_region.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_region.Location = new System.Drawing.Point(138, 213);
            this.textBox_region.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_region.Name = "textBox_region";
            this.textBox_region.ReadOnly = true;
            this.textBox_region.Size = new System.Drawing.Size(175, 21);
            this.textBox_region.TabIndex = 13;
            this.textBox_region.TabStop = false;
            this.textBox_region.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(8, 80);
            this.label11.Margin = new System.Windows.Forms.Padding(5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 21);
            this.label11.TabIndex = 14;
            this.label11.Text = "Format";
            this.label11.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label12
            // 
            this.label12.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Location = new System.Drawing.Point(8, 8);
            this.label12.Margin = new System.Windows.Forms.Padding(5);
            this.label12.Name = "label12";
            this.label12.Size = new System.Drawing.Size(120, 21);
            this.label12.TabIndex = 19;
            this.label12.Text = "Timer";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(8, 378);
            this.label14.Margin = new System.Windows.Forms.Padding(5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 21);
            this.label14.TabIndex = 18;
            this.label14.Text = "Record Stop";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(8, 213);
            this.label15.Margin = new System.Windows.Forms.Padding(5);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(120, 21);
            this.label15.TabIndex = 17;
            this.label15.Text = "Region";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_recordStart
            // 
            this.textBox_recordStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordStart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordStart.Location = new System.Drawing.Point(138, 347);
            this.textBox_recordStart.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_recordStart.Name = "textBox_recordStart";
            this.textBox_recordStart.ReadOnly = true;
            this.textBox_recordStart.Size = new System.Drawing.Size(175, 21);
            this.textBox_recordStart.TabIndex = 11;
            this.textBox_recordStart.TabStop = false;
            this.textBox_recordStart.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // textBox_activeProcess
            // 
            this.textBox_activeProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_activeProcess.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_activeProcess.Location = new System.Drawing.Point(138, 182);
            this.textBox_activeProcess.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_activeProcess.Name = "textBox_activeProcess";
            this.textBox_activeProcess.ReadOnly = true;
            this.textBox_activeProcess.Size = new System.Drawing.Size(175, 21);
            this.textBox_activeProcess.TabIndex = 10;
            this.textBox_activeProcess.TabStop = false;
            this.textBox_activeProcess.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(8, 347);
            this.label16.Margin = new System.Windows.Forms.Padding(5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(118, 21);
            this.label16.TabIndex = 16;
            this.label16.Text = "Record Start";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(8, 182);
            this.label17.Margin = new System.Windows.Forms.Padding(5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 21);
            this.label17.TabIndex = 15;
            this.label17.Text = "ActiveProcess";
            this.label17.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_recordVideo
            // 
            this.textBox_recordVideo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordVideo.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordVideo.Location = new System.Drawing.Point(138, 316);
            this.textBox_recordVideo.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_recordVideo.Name = "textBox_recordVideo";
            this.textBox_recordVideo.ReadOnly = true;
            this.textBox_recordVideo.Size = new System.Drawing.Size(175, 21);
            this.textBox_recordVideo.TabIndex = 9;
            this.textBox_recordVideo.TabStop = false;
            this.textBox_recordVideo.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // textBox_recordGif
            // 
            this.textBox_recordGif.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordGif.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordGif.Location = new System.Drawing.Point(138, 285);
            this.textBox_recordGif.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_recordGif.Name = "textBox_recordGif";
            this.textBox_recordGif.ReadOnly = true;
            this.textBox_recordGif.Size = new System.Drawing.Size(175, 21);
            this.textBox_recordGif.TabIndex = 9;
            this.textBox_recordGif.TabStop = false;
            this.textBox_recordGif.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // textBox_fullScreen
            // 
            this.textBox_fullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_fullScreen.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_fullScreen.Location = new System.Drawing.Point(138, 151);
            this.textBox_fullScreen.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_fullScreen.Name = "textBox_fullScreen";
            this.textBox_fullScreen.ReadOnly = true;
            this.textBox_fullScreen.Size = new System.Drawing.Size(175, 21);
            this.textBox_fullScreen.TabIndex = 8;
            this.textBox_fullScreen.TabStop = false;
            this.textBox_fullScreen.Click += new System.EventHandler(this.textBox_hotKey_Click);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(8, 316);
            this.label6.Margin = new System.Windows.Forms.Padding(5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 21);
            this.label6.TabIndex = 21;
            this.label6.Text = "Record Video";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(8, 285);
            this.label18.Margin = new System.Windows.Forms.Padding(5);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(118, 21);
            this.label18.TabIndex = 21;
            this.label18.Text = "Record Gif";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label19.Location = new System.Drawing.Point(8, 151);
            this.label19.Margin = new System.Windows.Forms.Padding(5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(120, 21);
            this.label19.TabIndex = 20;
            this.label19.Text = "FullScreen";
            this.label19.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // listView
            // 
            this.listView.Dock = System.Windows.Forms.DockStyle.Left;
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            listViewItem1.Tag = "General";
            listViewItem2.Tag = "Layout";
            listViewItem3.StateImageIndex = 0;
            listViewItem3.Tag = "Capture";
            this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3});
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.OwnerDraw = true;
            this.listView.Size = new System.Drawing.Size(125, 541);
            this.listView.TabIndex = 5;
            this.listView.TileSize = new System.Drawing.Size(120, 30);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // SettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 541);
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.listView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingDialog";
            this.ShowIcon = false;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "Setting";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.SettingDialog_FormClosing);
            this.Load += new System.EventHandler(this.SettingForm_Load);
            this.tabControl.ResumeLayout(false);
            this.tabPage_general.ResumeLayout(false);
            this.tabPage_general.PerformLayout();
            this.tabPage_layout.ResumeLayout(false);
            this.tabPage_layout.PerformLayout();
            this.tabPage_capture.ResumeLayout(false);
            this.tabPage_capture.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_timer)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion
        private System.Windows.Forms.ComboBox comboBox_theme;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label3;
        private System.Windows.Forms.Button button_layout;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.CheckBox checkBox_runOnStartup;
        private GTControl.ThemeListView listView;
        private GTControl.ThemeTabControl tabControl;
        private System.Windows.Forms.TabPage tabPage_general;
        private System.Windows.Forms.TabPage tabPage_layout;
        private System.Windows.Forms.TabPage tabPage_capture;
        private System.Windows.Forms.ComboBox comboBox_imageFormat;
        private System.Windows.Forms.Button button_dirPath;
        private System.Windows.Forms.TextBox textBox_dirPath;
        private System.Windows.Forms.NumericUpDown numericUpDown_timer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_recordStop;
        private System.Windows.Forms.TextBox textBox_region;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_recordStart;
        private System.Windows.Forms.TextBox textBox_activeProcess;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_recordGif;
        private System.Windows.Forms.TextBox textBox_fullScreen;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.TextBox textBox_recordVideo;
        private System.Windows.Forms.Label label6;
    }
}