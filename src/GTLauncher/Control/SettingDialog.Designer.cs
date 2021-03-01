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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(SettingDialog));
            this.tabControl = new GTControl.ThemeTabControl();
            this.tabPage_general = new System.Windows.Forms.TabPage();
            this.button_checkUpdate = new System.Windows.Forms.Button();
            this.checkBox_autoUpdate = new System.Windows.Forms.CheckBox();
            this.label_updateResult = new System.Windows.Forms.Label();
            this.label28 = new System.Windows.Forms.Label();
            this.label30 = new System.Windows.Forms.Label();
            this.checkBox_runOnStartup = new System.Windows.Forms.CheckBox();
            this.label2 = new System.Windows.Forms.Label();
            this.tabPage_layout = new System.Windows.Forms.TabPage();
            this.label1 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.comboBox_theme = new System.Windows.Forms.ComboBox();
            this.button_layout = new System.Windows.Forms.Button();
            this.label4 = new System.Windows.Forms.Label();
            this.tabPage_capture = new System.Windows.Forms.TabPage();
            this.comboBox_fullScreenMode = new System.Windows.Forms.ComboBox();
            this.comboBox_imageFormat = new System.Windows.Forms.ComboBox();
            this.button_dirPathCapture = new System.Windows.Forms.Button();
            this.textBox_dirPathCapture = new System.Windows.Forms.TextBox();
            this.numericUpDown_timer = new System.Windows.Forms.NumericUpDown();
            this.label10 = new System.Windows.Forms.Label();
            this.label27 = new System.Windows.Forms.Label();
            this.label9 = new System.Windows.Forms.Label();
            this.label8 = new System.Windows.Forms.Label();
            this.label26 = new System.Windows.Forms.Label();
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
            resources.ApplyResources(this.tabControl, "tabControl");
            this.tabControl.ForeColor = System.Drawing.Color.FromArgb(((int)(((byte)(238)))), ((int)(((byte)(238)))), ((int)(((byte)(238)))));
            this.tabControl.Multiline = true;
            this.tabControl.Name = "tabControl";
            this.tabControl.SelectedIndex = 0;
            // 
            // tabPage_general
            // 
            this.tabPage_general.Controls.Add(this.button_checkUpdate);
            this.tabPage_general.Controls.Add(this.checkBox_autoUpdate);
            this.tabPage_general.Controls.Add(this.label_updateResult);
            this.tabPage_general.Controls.Add(this.label28);
            this.tabPage_general.Controls.Add(this.label30);
            this.tabPage_general.Controls.Add(this.checkBox_runOnStartup);
            this.tabPage_general.Controls.Add(this.label2);
            this.tabPage_general.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.tabPage_general, "tabPage_general");
            this.tabPage_general.Name = "tabPage_general";
            this.tabPage_general.UseVisualStyleBackColor = true;
            // 
            // button_checkUpdate
            // 
            resources.ApplyResources(this.button_checkUpdate, "button_checkUpdate");
            this.button_checkUpdate.Name = "button_checkUpdate";
            this.button_checkUpdate.UseVisualStyleBackColor = true;
            this.button_checkUpdate.Click += new System.EventHandler(this.button_checkUpdate_Click);
            // 
            // checkBox_autoUpdate
            // 
            resources.ApplyResources(this.checkBox_autoUpdate, "checkBox_autoUpdate");
            this.checkBox_autoUpdate.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox_autoUpdate.Name = "checkBox_autoUpdate";
            this.checkBox_autoUpdate.UseVisualStyleBackColor = true;
            this.checkBox_autoUpdate.CheckedChanged += new System.EventHandler(this.checkBox_autoUpdate_CheckedChanged);
            // 
            // label_updateResult
            // 
            resources.ApplyResources(this.label_updateResult, "label_updateResult");
            this.label_updateResult.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label_updateResult.Name = "label_updateResult";
            // 
            // label28
            // 
            resources.ApplyResources(this.label28, "label28");
            this.label28.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label28.Name = "label28";
            // 
            // label30
            // 
            resources.ApplyResources(this.label30, "label30");
            this.label30.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label30.Name = "label30";
            // 
            // checkBox_runOnStartup
            // 
            resources.ApplyResources(this.checkBox_runOnStartup, "checkBox_runOnStartup");
            this.checkBox_runOnStartup.ForeColor = System.Drawing.SystemColors.ControlText;
            this.checkBox_runOnStartup.Name = "checkBox_runOnStartup";
            this.checkBox_runOnStartup.UseVisualStyleBackColor = true;
            this.checkBox_runOnStartup.CheckedChanged += new System.EventHandler(this.checkBox_runOnStartup_CheckedChanged);
            // 
            // label2
            // 
            resources.ApplyResources(this.label2, "label2");
            this.label2.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label2.Name = "label2";
            // 
            // tabPage_layout
            // 
            this.tabPage_layout.Controls.Add(this.label1);
            this.tabPage_layout.Controls.Add(this.label3);
            this.tabPage_layout.Controls.Add(this.comboBox_theme);
            this.tabPage_layout.Controls.Add(this.button_layout);
            this.tabPage_layout.Controls.Add(this.label4);
            this.tabPage_layout.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.tabPage_layout, "tabPage_layout");
            this.tabPage_layout.Name = "tabPage_layout";
            this.tabPage_layout.UseVisualStyleBackColor = true;
            // 
            // label1
            // 
            resources.ApplyResources(this.label1, "label1");
            this.label1.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label1.Name = "label1";
            // 
            // label3
            // 
            resources.ApplyResources(this.label3, "label3");
            this.label3.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label3.Name = "label3";
            // 
            // comboBox_theme
            // 
            this.comboBox_theme.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            resources.ApplyResources(this.comboBox_theme, "comboBox_theme");
            this.comboBox_theme.FormattingEnabled = true;
            this.comboBox_theme.Name = "comboBox_theme";
            this.comboBox_theme.SelectedIndexChanged += new System.EventHandler(this.comboBox_theme_SelectedIndexChanged);
            // 
            // button_layout
            // 
            resources.ApplyResources(this.button_layout, "button_layout");
            this.button_layout.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_layout.Name = "button_layout";
            this.button_layout.UseVisualStyleBackColor = true;
            this.button_layout.Click += new System.EventHandler(this.button_layout_Click);
            // 
            // label4
            // 
            resources.ApplyResources(this.label4, "label4");
            this.label4.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label4.Name = "label4";
            // 
            // tabPage_capture
            // 
            this.tabPage_capture.Controls.Add(this.comboBox_fullScreenMode);
            this.tabPage_capture.Controls.Add(this.comboBox_imageFormat);
            this.tabPage_capture.Controls.Add(this.button_dirPathCapture);
            this.tabPage_capture.Controls.Add(this.textBox_dirPathCapture);
            this.tabPage_capture.Controls.Add(this.numericUpDown_timer);
            this.tabPage_capture.Controls.Add(this.label10);
            this.tabPage_capture.Controls.Add(this.label27);
            this.tabPage_capture.Controls.Add(this.label9);
            this.tabPage_capture.Controls.Add(this.label8);
            this.tabPage_capture.Controls.Add(this.label26);
            this.tabPage_capture.Controls.Add(this.label11);
            this.tabPage_capture.Controls.Add(this.label12);
            this.tabPage_capture.ForeColor = System.Drawing.SystemColors.ControlText;
            resources.ApplyResources(this.tabPage_capture, "tabPage_capture");
            this.tabPage_capture.Name = "tabPage_capture";
            this.tabPage_capture.UseVisualStyleBackColor = true;
            // 
            // comboBox_fullScreenMode
            // 
            this.comboBox_fullScreenMode.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_fullScreenMode.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_fullScreenMode, "comboBox_fullScreenMode");
            this.comboBox_fullScreenMode.Name = "comboBox_fullScreenMode";
            this.comboBox_fullScreenMode.SelectedIndexChanged += new System.EventHandler(this.comboBox_fullScreenMode_SelectedIndexChanged);
            // 
            // comboBox_imageFormat
            // 
            this.comboBox_imageFormat.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_imageFormat.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_imageFormat, "comboBox_imageFormat");
            this.comboBox_imageFormat.Name = "comboBox_imageFormat";
            this.comboBox_imageFormat.SelectedIndexChanged += new System.EventHandler(this.comboBox_imageFormat_SelectedIndexChanged);
            // 
            // button_dirPathCapture
            // 
            resources.ApplyResources(this.button_dirPathCapture, "button_dirPathCapture");
            this.button_dirPathCapture.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_dirPathCapture.Name = "button_dirPathCapture";
            this.button_dirPathCapture.UseVisualStyleBackColor = true;
            this.button_dirPathCapture.Click += new System.EventHandler(this.button_dirPathCapture_Click);
            // 
            // textBox_dirPathCapture
            // 
            resources.ApplyResources(this.textBox_dirPathCapture, "textBox_dirPathCapture");
            this.textBox_dirPathCapture.Name = "textBox_dirPathCapture";
            this.textBox_dirPathCapture.ReadOnly = true;
            this.textBox_dirPathCapture.TabStop = false;
            // 
            // numericUpDown_timer
            // 
            resources.ApplyResources(this.numericUpDown_timer, "numericUpDown_timer");
            this.numericUpDown_timer.Maximum = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_timer.Name = "numericUpDown_timer";
            this.numericUpDown_timer.ValueChanged += new System.EventHandler(this.numericUpDown_timer_ValueChanged);
            // 
            // label10
            // 
            resources.ApplyResources(this.label10, "label10");
            this.label10.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label10.Name = "label10";
            // 
            // label27
            // 
            resources.ApplyResources(this.label27, "label27");
            this.label27.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label27.Name = "label27";
            // 
            // label9
            // 
            resources.ApplyResources(this.label9, "label9");
            this.label9.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label9.Name = "label9";
            // 
            // label8
            // 
            resources.ApplyResources(this.label8, "label8");
            this.label8.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label8.Name = "label8";
            // 
            // label26
            // 
            resources.ApplyResources(this.label26, "label26");
            this.label26.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label26.Name = "label26";
            // 
            // label11
            // 
            resources.ApplyResources(this.label11, "label11");
            this.label11.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label11.Name = "label11";
            // 
            // label12
            // 
            resources.ApplyResources(this.label12, "label12");
            this.label12.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label12.Name = "label12";
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
            resources.ApplyResources(this.tabPage_record, "tabPage_record");
            this.tabPage_record.Name = "tabPage_record";
            this.tabPage_record.UseVisualStyleBackColor = true;
            // 
            // button_dirPathRecord
            // 
            resources.ApplyResources(this.button_dirPathRecord, "button_dirPathRecord");
            this.button_dirPathRecord.ForeColor = System.Drawing.SystemColors.ControlText;
            this.button_dirPathRecord.Name = "button_dirPathRecord";
            this.button_dirPathRecord.UseVisualStyleBackColor = true;
            this.button_dirPathRecord.Click += new System.EventHandler(this.button_dirPathRecord_Click);
            // 
            // textBox_dirPathRecord
            // 
            resources.ApplyResources(this.textBox_dirPathRecord, "textBox_dirPathRecord");
            this.textBox_dirPathRecord.Name = "textBox_dirPathRecord";
            this.textBox_dirPathRecord.ReadOnly = true;
            this.textBox_dirPathRecord.TabStop = false;
            // 
            // label25
            // 
            resources.ApplyResources(this.label25, "label25");
            this.label25.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label25.Name = "label25";
            // 
            // comboBox_sourceAudio
            // 
            this.comboBox_sourceAudio.DropDownStyle = System.Windows.Forms.ComboBoxStyle.DropDownList;
            this.comboBox_sourceAudio.FormattingEnabled = true;
            resources.ApplyResources(this.comboBox_sourceAudio, "comboBox_sourceAudio");
            this.comboBox_sourceAudio.Name = "comboBox_sourceAudio";
            this.comboBox_sourceAudio.SelectedIndexChanged += new System.EventHandler(this.comboBox_sourceAudio_SelectedIndexChanged);
            // 
            // numericUpDown_fpsVideo
            // 
            resources.ApplyResources(this.numericUpDown_fpsVideo, "numericUpDown_fpsVideo");
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
            this.numericUpDown_fpsVideo.Value = new decimal(new int[] {
            30,
            0,
            0,
            0});
            this.numericUpDown_fpsVideo.ValueChanged += new System.EventHandler(this.numericUpDown_fpsVideo_ValueChanged);
            // 
            // numericUpDown_fpsGif
            // 
            resources.ApplyResources(this.numericUpDown_fpsGif, "numericUpDown_fpsGif");
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
            this.numericUpDown_fpsGif.Value = new decimal(new int[] {
            15,
            0,
            0,
            0});
            this.numericUpDown_fpsGif.ValueChanged += new System.EventHandler(this.numericUpDown_fpsGif_ValueChanged);
            // 
            // label24
            // 
            resources.ApplyResources(this.label24, "label24");
            this.label24.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label24.Name = "label24";
            // 
            // label5
            // 
            resources.ApplyResources(this.label5, "label5");
            this.label5.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label5.Name = "label5";
            // 
            // label20
            // 
            resources.ApplyResources(this.label20, "label20");
            this.label20.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label20.Name = "label20";
            // 
            // label23
            // 
            resources.ApplyResources(this.label23, "label23");
            this.label23.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label23.Name = "label23";
            // 
            // label21
            // 
            resources.ApplyResources(this.label21, "label21");
            this.label21.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label21.Name = "label21";
            // 
            // label22
            // 
            resources.ApplyResources(this.label22, "label22");
            this.label22.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label22.Name = "label22";
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
            resources.ApplyResources(this.tabPage_hotKey, "tabPage_hotKey");
            this.tabPage_hotKey.Name = "tabPage_hotKey";
            this.tabPage_hotKey.UseVisualStyleBackColor = true;
            // 
            // label13
            // 
            resources.ApplyResources(this.label13, "label13");
            this.label13.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label13.Name = "label13";
            // 
            // label7
            // 
            resources.ApplyResources(this.label7, "label7");
            this.label7.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label7.Name = "label7";
            // 
            // textBox_recordStop
            // 
            this.textBox_recordStop.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.textBox_recordStop, "textBox_recordStop");
            this.textBox_recordStop.Name = "textBox_recordStop";
            this.textBox_recordStop.ReadOnly = true;
            this.textBox_recordStop.TabStop = false;
            this.textBox_recordStop.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // textBox_region
            // 
            this.textBox_region.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.textBox_region, "textBox_region");
            this.textBox_region.Name = "textBox_region";
            this.textBox_region.ReadOnly = true;
            this.textBox_region.TabStop = false;
            this.textBox_region.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // label14
            // 
            resources.ApplyResources(this.label14, "label14");
            this.label14.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label14.Name = "label14";
            // 
            // label15
            // 
            resources.ApplyResources(this.label15, "label15");
            this.label15.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label15.Name = "label15";
            // 
            // textBox_recordStart
            // 
            this.textBox_recordStart.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.textBox_recordStart, "textBox_recordStart");
            this.textBox_recordStart.Name = "textBox_recordStart";
            this.textBox_recordStart.ReadOnly = true;
            this.textBox_recordStart.TabStop = false;
            this.textBox_recordStart.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // textBox_activeProcess
            // 
            this.textBox_activeProcess.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.textBox_activeProcess, "textBox_activeProcess");
            this.textBox_activeProcess.Name = "textBox_activeProcess";
            this.textBox_activeProcess.ReadOnly = true;
            this.textBox_activeProcess.TabStop = false;
            this.textBox_activeProcess.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // label16
            // 
            resources.ApplyResources(this.label16, "label16");
            this.label16.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label16.Name = "label16";
            // 
            // label17
            // 
            resources.ApplyResources(this.label17, "label17");
            this.label17.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label17.Name = "label17";
            // 
            // textBox_recordVideo
            // 
            this.textBox_recordVideo.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.textBox_recordVideo, "textBox_recordVideo");
            this.textBox_recordVideo.Name = "textBox_recordVideo";
            this.textBox_recordVideo.ReadOnly = true;
            this.textBox_recordVideo.TabStop = false;
            this.textBox_recordVideo.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // textBox_recordGif
            // 
            this.textBox_recordGif.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.textBox_recordGif, "textBox_recordGif");
            this.textBox_recordGif.Name = "textBox_recordGif";
            this.textBox_recordGif.ReadOnly = true;
            this.textBox_recordGif.TabStop = false;
            this.textBox_recordGif.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // textBox_fullScreen
            // 
            this.textBox_fullScreen.Cursor = System.Windows.Forms.Cursors.Hand;
            resources.ApplyResources(this.textBox_fullScreen, "textBox_fullScreen");
            this.textBox_fullScreen.Name = "textBox_fullScreen";
            this.textBox_fullScreen.ReadOnly = true;
            this.textBox_fullScreen.TabStop = false;
            this.textBox_fullScreen.KeyDown += new System.Windows.Forms.KeyEventHandler(this.textBox_hotKey_KeyDown);
            // 
            // label6
            // 
            resources.ApplyResources(this.label6, "label6");
            this.label6.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label6.Name = "label6";
            // 
            // label18
            // 
            resources.ApplyResources(this.label18, "label18");
            this.label18.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label18.Name = "label18";
            // 
            // label19
            // 
            resources.ApplyResources(this.label19, "label19");
            this.label19.ForeColor = System.Drawing.SystemColors.ControlText;
            this.label19.Name = "label19";
            // 
            // listView
            // 
            resources.ApplyResources(this.listView, "listView");
            this.listView.FullRowSelect = true;
            this.listView.GridLines = true;
            this.listView.HideSelection = false;
            this.listView.Items.AddRange(new System.Windows.Forms.ListViewItem[] {
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView.Items"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView.Items1"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView.Items2"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView.Items3"))),
            ((System.Windows.Forms.ListViewItem)(resources.GetObject("listView.Items4")))});
            this.listView.MultiSelect = false;
            this.listView.Name = "listView";
            this.listView.OwnerDraw = true;
            this.listView.TileSize = new System.Drawing.Size(120, 30);
            this.listView.UseCompatibleStateImageBehavior = false;
            this.listView.View = System.Windows.Forms.View.Tile;
            this.listView.SelectedIndexChanged += new System.EventHandler(this.listView_SelectedIndexChanged);
            // 
            // SettingDialog
            // 
            resources.ApplyResources(this, "$this");
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.Controls.Add(this.tabControl);
            this.Controls.Add(this.listView);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.FixedDialog;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "SettingDialog";
            this.ShowIcon = false;
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
        private System.Windows.Forms.ComboBox comboBox_fullScreenMode;
        private System.Windows.Forms.Label label27;
        private System.Windows.Forms.Label label26;
        private System.Windows.Forms.CheckBox checkBox_autoUpdate;
        private System.Windows.Forms.Label label30;
        private System.Windows.Forms.Button button_checkUpdate;
        private System.Windows.Forms.Label label28;
        private System.Windows.Forms.Label label_updateResult;
    }
}