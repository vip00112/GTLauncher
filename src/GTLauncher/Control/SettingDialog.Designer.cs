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
            System.Windows.Forms.ListViewItem listViewItem4 = new System.Windows.Forms.ListViewItem("Record");
            System.Windows.Forms.ListViewItem listViewItem5 = new System.Windows.Forms.ListViewItem("HotKey");
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
            this.button_dirPathCapture = new System.Windows.Forms.Button();
            this.textBox_dirPathCapture = new System.Windows.Forms.TextBox();
            this.numericUpDown_timer = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label11 = new System.Windows.Forms.Label();
            this.label12 = new System.Windows.Forms.Label();
            this.tabPage_record = new System.Windows.Forms.TabPage();
            this.button_dirPathRecord = new System.Windows.Forms.Button();
            this.textBox_dirPathRecord = new System.Windows.Forms.TextBox();
            this.label25 = new System.Windows.Forms.Label();
            this.comboBox_sourceAudio = new System.Windows.Forms.ComboBox();
            this.numericUpDown_fpsVideo = new System.Windows.Forms.NumericUpDown();
            this.numericUpDown_fpsGif = new System.Windows.Forms.NumericUpDown();
            this.label24 = new System.Windows.Forms.Label();
            this.label5 = new System.Windows.Forms.Label();
            this.label20 = new System.Windows.Forms.Label();
            this.label23 = new System.Windows.Forms.Label();
            this.label21 = new System.Windows.Forms.Label();
            this.label22 = new System.Windows.Forms.Label();
            this.tabPage_hotKey = new System.Windows.Forms.TabPage();
            this.label13 = new System.Windows.Forms.Label();
            this.label7 = new System.Windows.Forms.Label();
            this.textBox_recordStop = new System.Windows.Forms.TextBox();
            this.textBox_region = new System.Windows.Forms.TextBox();
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
            this.tabPage_record.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fpsVideo)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fpsGif)).BeginInit();
            this.tabPage_hotKey.SuspendLayout();
            this.SuspendLayout();
            // 
            // tabControl
            // 
            this.tabControl.Controls.Add(this.tabPage_general);
            this.tabControl.Controls.Add(this.tabPage_layout);
            this.tabControl.Controls.Add(this.tabPage_capture);
            this.tabControl.Controls.Add(this.tabPage_record);
            this.tabControl.Controls.Add(this.tabPage_hotKey);
            this.tabControl.Dock = System.Windows.Forms.DockStyle.Fill;
            this.tabControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.tabControl.Location = new System.Drawing.Point(125, 0);
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.Padding = new System.Drawing.Point(0, 0);
            this.tabControl.SelectedIndex = 0;
            this.tabControl.Size = new System.Drawing.Size(329, 331);
            this.tabControl.TabIndex = 1;
            // 
            // tabPage_general
            // 
            this.tabPage_general.Controls.Add(this.checkBox_runOnStartup);
            this.tabPage_general.Controls.Add(this.label2);
            this.tabPage_general.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_general.Location = new System.Drawing.Point(1, 18);
            this.tabPage_general.Name = "tabPage_general";
            this.tabPage_general.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_general.Size = new System.Drawing.Size(327, 312);
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
            this.checkBox_runOnStartup.TabIndex = 0;
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
            this.label2.TabIndex = 0;
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
            this.tabPage_layout.Location = new System.Drawing.Point(1, 18);
            this.tabPage_layout.Name = "tabPage_layout";
            this.tabPage_layout.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_layout.Size = new System.Drawing.Size(327, 312);
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
            this.label1.TabIndex = 0;
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
            this.label3.TabIndex = 0;
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
            this.comboBox_theme.TabIndex = 1;
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
            this.button_layout.TabIndex = 0;
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
            this.label4.TabIndex = 0;
            this.label4.Text = "- Change main layout.";
            // 
            // tabPage_capture
            // 
            this.tabPage_capture.Controls.Add(this.comboBox_imageFormat);
            this.tabPage_capture.Controls.Add(this.button_dirPathCapture);
            this.tabPage_capture.Controls.Add(this.textBox_dirPathCapture);
            this.tabPage_capture.Controls.Add(this.numericUpDown_timer);
            this.tabPage_capture.Controls.Add(this.label10);
            this.tabPage_capture.Controls.Add(this.label9);
            this.tabPage_capture.Controls.Add(this.label8);
            this.tabPage_capture.Controls.Add(this.label11);
            this.tabPage_capture.Controls.Add(this.label12);
            this.tabPage_capture.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_capture.Location = new System.Drawing.Point(1, 18);
            this.tabPage_capture.Name = "tabPage_capture";
            this.tabPage_capture.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_capture.Size = new System.Drawing.Size(327, 312);
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
            this.comboBox_imageFormat.TabIndex = 1;
            this.comboBox_imageFormat.SelectedIndexChanged += new System.EventHandler(this.comboBox_imageFormat_SelectedIndexChanged);
            // 
            // button_dirPathCapture
            // 
            this.button_dirPathCapture.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_dirPathCapture.Location = new System.Drawing.Point(292, 151);
            this.button_dirPathCapture.Margin = new System.Windows.Forms.Padding(5);
            this.button_dirPathCapture.Name = "button_dirPathCapture";
            this.button_dirPathCapture.Size = new System.Drawing.Size(23, 23);
            this.button_dirPathCapture.TabIndex = 2;
            this.button_dirPathCapture.Text = "≡";
            this.button_dirPathCapture.UseVisualStyleBackColor = true;
            this.button_dirPathCapture.Click += new System.EventHandler(this.button_dirPathCapture_Click);
            // 
            // textBox_dirPathCapture
            // 
            this.textBox_dirPathCapture.Location = new System.Drawing.Point(10, 151);
            this.textBox_dirPathCapture.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_dirPathCapture.Name = "textBox_dirPathCapture";
            this.textBox_dirPathCapture.ReadOnly = true;
            this.textBox_dirPathCapture.Size = new System.Drawing.Size(272, 21);
            this.textBox_dirPathCapture.TabIndex = 0;
            this.textBox_dirPathCapture.TabStop = false;
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
            this.numericUpDown_timer.TabIndex = 0;
            this.numericUpDown_timer.ValueChanged += new System.EventHandler(this.numericUpDown_timer_ValueChanged);
            // 
            // label10
            // 
            this.label10.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Location = new System.Drawing.Point(10, 177);
            this.label10.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label10.Name = "label10";
            this.label10.Size = new System.Drawing.Size(305, 21);
            this.label10.TabIndex = 0;
            this.label10.Text = "- Save directory for capture file";
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
            this.label9.TabIndex = 0;
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
            this.label8.TabIndex = 0;
            this.label8.Text = "- Delay before capture";
            this.label8.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label11
            // 
            this.label11.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Location = new System.Drawing.Point(8, 80);
            this.label11.Margin = new System.Windows.Forms.Padding(5);
            this.label11.Name = "label11";
            this.label11.Size = new System.Drawing.Size(118, 21);
            this.label11.TabIndex = 0;
            this.label11.Text = "Image Format";
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
            this.label12.TabIndex = 0;
            this.label12.Text = "Timer";
            this.label12.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage_record
            // 
            this.tabPage_record.Controls.Add(this.button_dirPathRecord);
            this.tabPage_record.Controls.Add(this.textBox_dirPathRecord);
            this.tabPage_record.Controls.Add(this.label25);
            this.tabPage_record.Controls.Add(this.comboBox_sourceAudio);
            this.tabPage_record.Controls.Add(this.numericUpDown_fpsVideo);
            this.tabPage_record.Controls.Add(this.numericUpDown_fpsGif);
            this.tabPage_record.Controls.Add(this.label24);
            this.tabPage_record.Controls.Add(this.label5);
            this.tabPage_record.Controls.Add(this.label20);
            this.tabPage_record.Controls.Add(this.label23);
            this.tabPage_record.Controls.Add(this.label21);
            this.tabPage_record.Controls.Add(this.label22);
            this.tabPage_record.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_record.Location = new System.Drawing.Point(1, 18);
            this.tabPage_record.Name = "tabPage_record";
            this.tabPage_record.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_record.Size = new System.Drawing.Size(327, 312);
            this.tabPage_record.TabIndex = 3;
            this.tabPage_record.Text = "Record";
            this.tabPage_record.UseVisualStyleBackColor = true;
            // 
            // button_dirPathRecord
            // 
            this.button_dirPathRecord.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_dirPathRecord.Location = new System.Drawing.Point(292, 193);
            this.button_dirPathRecord.Margin = new System.Windows.Forms.Padding(5);
            this.button_dirPathRecord.Name = "button_dirPathRecord";
            this.button_dirPathRecord.Size = new System.Drawing.Size(23, 23);
            this.button_dirPathRecord.TabIndex = 3;
            this.button_dirPathRecord.Text = "≡";
            this.button_dirPathRecord.UseVisualStyleBackColor = true;
            this.button_dirPathRecord.Click += new System.EventHandler(this.button_dirPathRecord_Click);
            // 
            // textBox_dirPathRecord
            // 
            this.textBox_dirPathRecord.Location = new System.Drawing.Point(10, 182);
            this.textBox_dirPathRecord.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_dirPathRecord.Name = "textBox_dirPathRecord";
            this.textBox_dirPathRecord.ReadOnly = true;
            this.textBox_dirPathRecord.Size = new System.Drawing.Size(272, 21);
            this.textBox_dirPathRecord.TabIndex = 0;
            this.textBox_dirPathRecord.TabStop = false;
            // 
            // label25
            // 
            this.label25.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label25.Location = new System.Drawing.Point(10, 208);
            this.label25.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label25.Name = "label25";
            this.label25.Size = new System.Drawing.Size(305, 21);
            this.label25.TabIndex = 0;
            this.label25.Text = "- Save directory for record file";
            this.label25.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // comboBox_sourceAudio
            // 
            this.comboBox_sourceAudio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sourceAudio.FormattingEnabled = true;
            this.comboBox_sourceAudio.Location = new System.Drawing.Point(138, 111);
            this.comboBox_sourceAudio.Margin = new System.Windows.Forms.Padding(5);
            this.comboBox_sourceAudio.Name = "comboBox_sourceAudio";
            this.comboBox_sourceAudio.Size = new System.Drawing.Size(175, 20);
            this.comboBox_sourceAudio.TabIndex = 2;
            this.comboBox_sourceAudio.SelectedIndexChanged += new System.EventHandler(this.comboBox_sourceAudio_SelectedIndexChanged);
            // 
            // numericUpDown_fpsVideo
            // 
            this.numericUpDown_fpsVideo.Location = new System.Drawing.Point(138, 39);
            this.numericUpDown_fpsVideo.Margin = new System.Windows.Forms.Padding(5);
            this.numericUpDown_fpsVideo.Maximum = new decimal(new int[] {
            60,
            0,
            0,
            0});
            this.numericUpDown_fpsVideo.Minimum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_fpsVideo.Name = "numericUpDown_fpsVideo";
            this.numericUpDown_fpsVideo.Size = new System.Drawing.Size(175, 21);
            this.numericUpDown_fpsVideo.TabIndex = 1;
            this.numericUpDown_fpsVideo.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_fpsVideo.ValueChanged += new System.EventHandler(this.numericUpDown_fpsVideo_ValueChanged);
            // 
            // numericUpDown_fpsGif
            // 
            this.numericUpDown_fpsGif.Location = new System.Drawing.Point(138, 8);
            this.numericUpDown_fpsGif.Margin = new System.Windows.Forms.Padding(5);
            this.numericUpDown_fpsGif.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_fpsGif.Minimum = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_fpsGif.Name = "numericUpDown_fpsGif";
            this.numericUpDown_fpsGif.Size = new System.Drawing.Size(175, 21);
            this.numericUpDown_fpsGif.TabIndex = 0;
            this.numericUpDown_fpsGif.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_fpsGif.ValueChanged += new System.EventHandler(this.numericUpDown_fpsGif_ValueChanged);
            // 
            // label24
            // 
            this.label24.AutoSize = true;
            this.label24.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label24.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label24.Location = new System.Drawing.Point(8, 244);
            this.label24.Margin = new System.Windows.Forms.Padding(0);
            this.label24.Name = "label24";
            this.label24.Size = new System.Drawing.Size(310, 60);
            this.label24.TabIndex = 0;
            this.label24.Text = "YOU WANT RECORD DESKTOP SOUND TOGETHER ?\r\n\r\n1. Download \'virtual-audio-capturer\' " +
    "from google.\r\n2. Install \'virtual-audio-capturer\'.\r\n3. Apply audio source this s" +
    "etting page.";
            this.label24.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label5
            // 
            this.label5.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Location = new System.Drawing.Point(8, 137);
            this.label5.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label5.Name = "label5";
            this.label5.Size = new System.Drawing.Size(305, 20);
            this.label5.TabIndex = 0;
            this.label5.Text = "- Record with audio source";
            this.label5.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label20
            // 
            this.label20.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Location = new System.Drawing.Point(8, 65);
            this.label20.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label20.Name = "label20";
            this.label20.Size = new System.Drawing.Size(307, 21);
            this.label20.TabIndex = 0;
            this.label20.Text = "- Frames Per Seconds";
            this.label20.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label23
            // 
            this.label23.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label23.Location = new System.Drawing.Point(8, 39);
            this.label23.Margin = new System.Windows.Forms.Padding(5);
            this.label23.Name = "label23";
            this.label23.Size = new System.Drawing.Size(120, 21);
            this.label23.TabIndex = 0;
            this.label23.Text = "Video FPS";
            this.label23.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label21
            // 
            this.label21.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label21.Location = new System.Drawing.Point(8, 111);
            this.label21.Margin = new System.Windows.Forms.Padding(5);
            this.label21.Name = "label21";
            this.label21.Size = new System.Drawing.Size(118, 21);
            this.label21.TabIndex = 31;
            this.label21.Text = "Audio Source";
            this.label21.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label22
            // 
            this.label22.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label22.Location = new System.Drawing.Point(8, 8);
            this.label22.Margin = new System.Windows.Forms.Padding(5);
            this.label22.Name = "label22";
            this.label22.Size = new System.Drawing.Size(120, 21);
            this.label22.TabIndex = 0;
            this.label22.Text = "Gif FPS";
            this.label22.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // tabPage_hotKey
            // 
            this.tabPage_hotKey.Controls.Add(this.label13);
            this.tabPage_hotKey.Controls.Add(this.label7);
            this.tabPage_hotKey.Controls.Add(this.textBox_recordStop);
            this.tabPage_hotKey.Controls.Add(this.textBox_region);
            this.tabPage_hotKey.Controls.Add(this.label14);
            this.tabPage_hotKey.Controls.Add(this.label15);
            this.tabPage_hotKey.Controls.Add(this.textBox_recordStart);
            this.tabPage_hotKey.Controls.Add(this.textBox_activeProcess);
            this.tabPage_hotKey.Controls.Add(this.label16);
            this.tabPage_hotKey.Controls.Add(this.label17);
            this.tabPage_hotKey.Controls.Add(this.textBox_recordVideo);
            this.tabPage_hotKey.Controls.Add(this.textBox_recordGif);
            this.tabPage_hotKey.Controls.Add(this.textBox_fullScreen);
            this.tabPage_hotKey.Controls.Add(this.label6);
            this.tabPage_hotKey.Controls.Add(this.label18);
            this.tabPage_hotKey.Controls.Add(this.label19);
            this.tabPage_hotKey.ForeColor = System.Drawing.SystemColors.ControlText;
            this.tabPage_hotKey.Location = new System.Drawing.Point(1, 18);
            this.tabPage_hotKey.Name = "tabPage_hotKey";
            this.tabPage_hotKey.Padding = new System.Windows.Forms.Padding(3);
            this.tabPage_hotKey.Size = new System.Drawing.Size(327, 312);
            this.tabPage_hotKey.TabIndex = 4;
            this.tabPage_hotKey.Text = "HotKey";
            this.tabPage_hotKey.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            this.label13.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Location = new System.Drawing.Point(8, 279);
            this.label13.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label13.Name = "label13";
            this.label13.Size = new System.Drawing.Size(305, 21);
            this.label13.TabIndex = 0;
            this.label13.Text = "- HotKeys for record";
            this.label13.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label7
            // 
            this.label7.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Location = new System.Drawing.Point(8, 114);
            this.label7.Margin = new System.Windows.Forms.Padding(0, 0, 0, 20);
            this.label7.Name = "label7";
            this.label7.Size = new System.Drawing.Size(305, 21);
            this.label7.TabIndex = 0;
            this.label7.Text = "- HotKeys for capture";
            this.label7.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_recordStop
            // 
            this.textBox_recordStop.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordStop.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordStop.Location = new System.Drawing.Point(138, 253);
            this.textBox_recordStop.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_recordStop.Name = "textBox_recordStop";
            this.textBox_recordStop.ReadOnly = true;
            this.textBox_recordStop.Size = new System.Drawing.Size(175, 21);
            this.textBox_recordStop.TabIndex = 0;
            this.textBox_recordStop.TabStop = false;
            this.textBox_recordStop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // textBox_region
            // 
            this.textBox_region.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_region.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_region.Location = new System.Drawing.Point(138, 88);
            this.textBox_region.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_region.Name = "textBox_region";
            this.textBox_region.ReadOnly = true;
            this.textBox_region.Size = new System.Drawing.Size(175, 21);
            this.textBox_region.TabIndex = 0;
            this.textBox_region.TabStop = false;
            this.textBox_region.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // label14
            // 
            this.label14.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Location = new System.Drawing.Point(8, 253);
            this.label14.Margin = new System.Windows.Forms.Padding(5);
            this.label14.Name = "label14";
            this.label14.Size = new System.Drawing.Size(118, 21);
            this.label14.TabIndex = 0;
            this.label14.Text = "Record Stop";
            this.label14.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label15
            // 
            this.label15.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Location = new System.Drawing.Point(8, 88);
            this.label15.Margin = new System.Windows.Forms.Padding(5);
            this.label15.Name = "label15";
            this.label15.Size = new System.Drawing.Size(120, 21);
            this.label15.TabIndex = 0;
            this.label15.Text = "Capture Region";
            this.label15.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // textBox_recordStart
            // 
            this.textBox_recordStart.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordStart.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordStart.Location = new System.Drawing.Point(138, 222);
            this.textBox_recordStart.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_recordStart.Name = "textBox_recordStart";
            this.textBox_recordStart.ReadOnly = true;
            this.textBox_recordStart.Size = new System.Drawing.Size(175, 21);
            this.textBox_recordStart.TabIndex = 0;
            this.textBox_recordStart.TabStop = false;
            this.textBox_recordStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // textBox_activeProcess
            // 
            this.textBox_activeProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_activeProcess.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_activeProcess.Location = new System.Drawing.Point(138, 48);
            this.textBox_activeProcess.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_activeProcess.Name = "textBox_activeProcess";
            this.textBox_activeProcess.ReadOnly = true;
            this.textBox_activeProcess.Size = new System.Drawing.Size(175, 21);
            this.textBox_activeProcess.TabIndex = 0;
            this.textBox_activeProcess.TabStop = false;
            this.textBox_activeProcess.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // label16
            // 
            this.label16.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Location = new System.Drawing.Point(8, 222);
            this.label16.Margin = new System.Windows.Forms.Padding(5);
            this.label16.Name = "label16";
            this.label16.Size = new System.Drawing.Size(118, 21);
            this.label16.TabIndex = 0;
            this.label16.Text = "Record Start";
            this.label16.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label17
            // 
            this.label17.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Location = new System.Drawing.Point(8, 48);
            this.label17.Margin = new System.Windows.Forms.Padding(5);
            this.label17.Name = "label17";
            this.label17.Size = new System.Drawing.Size(120, 30);
            this.label17.TabIndex = 0;
            this.label17.Text = "Capture\r\nActiveProcess";
            // 
            // textBox_recordVideo
            // 
            this.textBox_recordVideo.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordVideo.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordVideo.Location = new System.Drawing.Point(138, 191);
            this.textBox_recordVideo.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_recordVideo.Name = "textBox_recordVideo";
            this.textBox_recordVideo.ReadOnly = true;
            this.textBox_recordVideo.Size = new System.Drawing.Size(175, 21);
            this.textBox_recordVideo.TabIndex = 0;
            this.textBox_recordVideo.TabStop = false;
            this.textBox_recordVideo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // textBox_recordGif
            // 
            this.textBox_recordGif.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_recordGif.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_recordGif.Location = new System.Drawing.Point(138, 160);
            this.textBox_recordGif.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_recordGif.Name = "textBox_recordGif";
            this.textBox_recordGif.ReadOnly = true;
            this.textBox_recordGif.Size = new System.Drawing.Size(175, 21);
            this.textBox_recordGif.TabIndex = 0;
            this.textBox_recordGif.TabStop = false;
            this.textBox_recordGif.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // textBox_fullScreen
            // 
            this.textBox_fullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            this.textBox_fullScreen.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.textBox_fullScreen.Location = new System.Drawing.Point(138, 8);
            this.textBox_fullScreen.Margin = new System.Windows.Forms.Padding(5);
            this.textBox_fullScreen.Name = "textBox_fullScreen";
            this.textBox_fullScreen.ReadOnly = true;
            this.textBox_fullScreen.Size = new System.Drawing.Size(175, 21);
            this.textBox_fullScreen.TabIndex = 0;
            this.textBox_fullScreen.TabStop = false;
            this.textBox_fullScreen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // label6
            // 
            this.label6.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Location = new System.Drawing.Point(8, 191);
            this.label6.Margin = new System.Windows.Forms.Padding(5);
            this.label6.Name = "label6";
            this.label6.Size = new System.Drawing.Size(118, 21);
            this.label6.TabIndex = 0;
            this.label6.Text = "Record Video";
            this.label6.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label18
            // 
            this.label18.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Location = new System.Drawing.Point(8, 160);
            this.label18.Margin = new System.Windows.Forms.Padding(5);
            this.label18.Name = "label18";
            this.label18.Size = new System.Drawing.Size(118, 21);
            this.label18.TabIndex = 0;
            this.label18.Text = "Record Gif";
            this.label18.TextAlign = System.Drawing.ContentAlignment.MiddleLeft;
            // 
            // label19
            // 
            this.label19.Font = new System.Drawing.Font("굴림", 9F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(129)));
            this.label19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label19.Location = new System.Drawing.Point(8, 8);
            this.label19.Margin = new System.Windows.Forms.Padding(5);
            this.label19.Name = "label19";
            this.label19.Size = new System.Drawing.Size(120, 30);
            this.label19.TabIndex = 0;
            this.label19.Text = "Capture\r\nFullScreen";
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
            listViewItem4.Tag = "Record";
            listViewItem5.Tag = "HotKey";
            this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            listViewItem1,
            listViewItem2,
            listViewItem3,
            listViewItem4,
            listViewItem5});
            this.listView.Location = new System.Drawing.Point(0, 0);
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.OwnerDraw = true;
            this.listView.Size = new System.Drawing.Size(125, 331);
            this.listView.TabIndex = 0;
            this.listView.TileSize = new System.Drawing.Size(120, 30);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // SettingDialog
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(7F, 12F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(454, 331);
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
            this.tabPage_record.ResumeLayout(false);
            this.tabPage_record.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fpsVideo)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.numericUpDown_fpsGif)).EndInit();
            this.tabPage_hotKey.ResumeLayout(false);
            this.tabPage_hotKey.PerformLayout();
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
        private System.Windows.Forms.Button button_dirPathCapture;
        private System.Windows.Forms.TextBox textBox_dirPathCapture;
        private System.Windows.Forms.NumericUpDown numericUpDown_timer;
        private System.Windows.Forms.Label label10;
        private System.Windows.Forms.Label label9;
        private System.Windows.Forms.Label label8;
        private System.Windows.Forms.Label label11;
        private System.Windows.Forms.Label label12;
        private System.Windows.Forms.TabPage tabPage_record;
        private System.Windows.Forms.TabPage tabPage_hotKey;
        private System.Windows.Forms.Label label13;
        private System.Windows.Forms.Label label7;
        private System.Windows.Forms.TextBox textBox_recordStop;
        private System.Windows.Forms.TextBox textBox_region;
        private System.Windows.Forms.Label label14;
        private System.Windows.Forms.Label label15;
        private System.Windows.Forms.TextBox textBox_recordStart;
        private System.Windows.Forms.TextBox textBox_activeProcess;
        private System.Windows.Forms.Label label16;
        private System.Windows.Forms.Label label17;
        private System.Windows.Forms.TextBox textBox_recordVideo;
        private System.Windows.Forms.TextBox textBox_recordGif;
        private System.Windows.Forms.TextBox textBox_fullScreen;
        private System.Windows.Forms.Label label6;
        private System.Windows.Forms.Label label18;
        private System.Windows.Forms.Label label19;
        private System.Windows.Forms.Button button_dirPathRecord;
        private System.Windows.Forms.TextBox textBox_dirPathRecord;
        private System.Windows.Forms.Label label25;
        private System.Windows.Forms.ComboBox comboBox_sourceAudio;
        private System.Windows.Forms.NumericUpDown numericUpDown_fpsVideo;
        private System.Windows.Forms.NumericUpDown numericUpDown_fpsGif;
        private System.Windows.Forms.Label label5;
        private System.Windows.Forms.Label label20;
        private System.Windows.Forms.Label label23;
        private System.Windows.Forms.Label label21;
        private System.Windows.Forms.Label label22;
        private System.Windows.Forms.Label label24;
    }
}