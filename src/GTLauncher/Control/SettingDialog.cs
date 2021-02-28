using GTCapture;
using GTControl;
using GTUtil;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTLauncher
{
    public partial class SettingDialog : Form
    {
        private bool _isLoaded;
        private bool _isChkecedFFmpeg;
        private List<TextBox> _textBoxs;

        #region Constructor
        internal SettingDialog()
        {
            InitializeComponent();
        }
        #endregion

        #region Properties
        public bool IsSavedLayout { get; private set; }
        #endregion

        #region Control Event
        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (Runtime.DesignMode) return;

            LayoutSetting.Invalidate(this);

            // Load From Settings
            LoadGeneralSetting();
            LoadLayoutSetting();
            LoadCaptureSetting();

            // Hide TabControl Header
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;

            // Listview Setting
            for (int i = 0; i < listView.Items.Count; i++)
            {
                var li = listView.Items[i];
                if (i == 0) li.Tag = tabPage_general;
                else if (i == 1) li.Tag = tabPage_layout;
                else if (i == 2) li.Tag = tabPage_capture;
                else if (i == 3) li.Tag = tabPage_record;
                else if (i == 4) li.Tag = tabPage_hotKey;
            }
            foreach (ListViewItem li in listView.Items)
            {
                var tag = li.Tag as string;

                if (tag == "General") li.Tag = tabPage_general;
                else if (tag == "Layout") li.Tag = tabPage_layout;
                else if (tag == "Capture") li.Tag = tabPage_capture;
                else if (tag == "Record") li.Tag = tabPage_record;
                else if (tag == "HotKey") li.Tag = tabPage_hotKey;
            }
            listView.Items[0].Focused = true;
            listView.Items[0].Selected = true;

            _isLoaded = true;
        }

        private void SettingDialog_FormClosing(object sender, FormClosingEventArgs e)
        {
            GeneralSetting.Save();
            CaptureSetting.Save();
            LayoutSetting.Save();
        }

        private void listView_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (listView.FocusedItem == null) return;

            var tabPage = listView.FocusedItem.Tag as TabPage;
            if (tabPage == null) return;

            tabControl.SelectedTab = tabPage;

            // Record 페이지일시 FFmpeg 확인
            if (tabPage == tabPage_record && !_isChkecedFFmpeg)
            {
                var ffmpeg = new FFmpeg();
                if (ffmpeg.CheckAndDownloadExecuteFile())
                {
                    EnableRecordTabPage(true);
                    comboBox_sourceAudio.DataSource = GetAudioSources();
                    comboBox_sourceAudio.SelectedItem = CaptureSetting.AudioSource;
                    _isChkecedFFmpeg = true;
                }
                else
                {
                    EnableRecordTabPage(false);
                }
            }
        }

        #region GeneralSetting
        private void checkBox_runOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            GeneralSetting.RunOnStartup = checkBox_runOnStartup.Checked;
        }

        private void checkBox_autoUpdate_CheckedChanged(object sender, EventArgs e)
        {
            GeneralSetting.AutoUpdate = checkBox_autoUpdate.Checked;
        }
        #endregion

        #region LayoutSetting
        private void comboBox_theme_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isLoaded) return;
            LayoutSetting.Theme = (Theme) comboBox_theme.SelectedItem;
        }

        private void button_layout_Click(object sender, EventArgs e)
        {
            using (var dialog = new LayoutSettingDialog())
            {
                if (dialog.ShowDialog() == DialogResult.OK) IsSavedLayout = true;
            }
        }
        #endregion

        #region CaptureSetting
        private void textBox_hotKey_KeyDown(object sender, KeyEventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            if (textBox.Tag == null) return;

            e.SuppressKeyPress = true;
            if (e.KeyCode != Keys.Escape && !InvalidateHotKey(e.Modifiers, e.KeyCode)) return;

            var tmp = HotKey.CreateTemplate(e.Modifiers, e.KeyCode);
            if (e.KeyCode != Keys.Escape)
            {
                var addeds = CaptureSetting.HotKeys.Where(o => o.Value.Modifiers == tmp.Modifiers && o.Value.Key == tmp.Key);
                foreach (var added in addeds)
                {
                    if (added.Value == tmp) continue;

                    added.Value.Update(WindowNative.KeyModifiers.None, Keys.None);

                    var owner = _textBoxs.FirstOrDefault(o => (CaptureMode) o.Tag == added.Key);
                    if (owner != null) owner.Text = added.Value.ToString();
                }
            }

            var mode = (CaptureMode) textBox.Tag;
            var hotKey = CaptureSetting.HotKeys[mode];
            hotKey.Update(tmp.Modifiers, tmp.Key);
            textBox.Text = tmp.ToString();
        }

        private void numericUpDown_timer_ValueChanged(object sender, EventArgs e)
        {
            CaptureSetting.Timer = (int) numericUpDown_timer.Value;
        }

        private void comboBox_fullScreenMode_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isLoaded) return;
            CaptureSetting.FullScreenMode = (FullScreenMode) comboBox_fullScreenMode.SelectedItem;
        }

        private void comboBox_imageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isLoaded) return;
            CaptureSetting.SaveImageFormat = (string) comboBox_imageFormat.SelectedItem;
        }

        private void button_dirPathCapture_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.RootFolder = Environment.SpecialFolder.Desktop;
            fb.SelectedPath = CaptureSetting.CaptureSaveDirectory;
            if (fb.ShowDialog() != DialogResult.OK) return;

            CaptureSetting.CaptureSaveDirectory = fb.SelectedPath;
            textBox_dirPathCapture.Text = fb.SelectedPath;
        }

        private void numericUpDown_fpsGif_ValueChanged(object sender, EventArgs e)
        {
            CaptureSetting.GifFPS = (int) numericUpDown_fpsGif.Value;
        }

        private void numericUpDown_fpsVideo_ValueChanged(object sender, EventArgs e)
        {
            CaptureSetting.VideoFPS = (int) numericUpDown_fpsVideo.Value;
        }

        private void comboBox_sourceAudio_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isLoaded || !_isChkecedFFmpeg) return;
            CaptureSetting.AudioSource = (string) comboBox_sourceAudio.SelectedItem;
        }

        private void button_dirPathRecord_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.RootFolder = Environment.SpecialFolder.Desktop;
            fb.SelectedPath = CaptureSetting.RecordSaveDirectory;
            if (fb.ShowDialog() != DialogResult.OK) return;

            CaptureSetting.RecordSaveDirectory = fb.SelectedPath;
            textBox_dirPathRecord.Text = fb.SelectedPath;
        }
        #endregion
        #endregion

        #region Private Method
        private void LoadGeneralSetting()
        {
            checkBox_runOnStartup.Checked = GeneralSetting.RunOnStartup;
        }

        private void LoadLayoutSetting()
        {
            comboBox_theme.DataSource = Enum.GetValues(typeof(Theme));
            comboBox_theme.SelectedItem = LayoutSetting.Theme;
        }

        private void LoadCaptureSetting()
        {
            _textBoxs = new List<TextBox>();
            _textBoxs.Add(textBox_fullScreen);
            _textBoxs.Add(textBox_activeProcess);
            _textBoxs.Add(textBox_region);
            _textBoxs.Add(textBox_recordGif);
            _textBoxs.Add(textBox_recordVideo);
            _textBoxs.Add(textBox_recordStart);
            _textBoxs.Add(textBox_recordStop);

            textBox_fullScreen.Tag = CaptureMode.FullScreen;
            textBox_activeProcess.Tag = CaptureMode.ActiveProcess;
            textBox_region.Tag = CaptureMode.Region;
            textBox_recordGif.Tag = CaptureMode.RecordGif;
            textBox_recordVideo.Tag = CaptureMode.RecordVideo;
            textBox_recordStart.Tag = CaptureMode.RecordStart;
            textBox_recordStop.Tag = CaptureMode.RecordStop;

            foreach (var mode in CaptureSetting.HotKeys.Keys)
            {
                var owner = _textBoxs.FirstOrDefault(o => (CaptureMode) o.Tag == mode);
                if (owner == null) continue;

                owner.Text = CaptureSetting.HotKeys[mode].ToString();
            }

            numericUpDown_timer.Value = CaptureSetting.Timer;
            textBox_dirPathCapture.Text = CaptureSetting.CaptureSaveDirectory;
            comboBox_imageFormat.DataSource = CaptureSetting.ImageFormats;
            comboBox_imageFormat.SelectedItem = CaptureSetting.SaveImageFormat;

            numericUpDown_fpsGif.Value = CaptureSetting.GifFPS;
            numericUpDown_fpsVideo.Value = CaptureSetting.VideoFPS;
            textBox_dirPathRecord.Text = CaptureSetting.RecordSaveDirectory;
            // comboBox_sourceAudio의 값은 listView_SelectedIndexChanged이벤트에서 FFmpeg 체크 후 처리함

            comboBox_fullScreenMode.DataSource = Enum.GetValues(typeof(FullScreenMode));
            comboBox_fullScreenMode.SelectedItem = CaptureSetting.FullScreenMode;
        }

        private List<string> GetAudioSources()
        {
            var ffmpeg = new FFmpeg();
            var deviceNames = ffmpeg.GetDeviceNames();
            return deviceNames["Audio"];
        }

        private void EnableRecordTabPage(bool isUse)
        {
            foreach (Control control in tabPage_record.Controls)
            {
                control.Enabled = isUse;
            }
        }

        private bool InvalidateHotKey(Keys modifiers, Keys key)
        {
            if (modifiers == Keys.None) return false;

            var nums = new Keys[] { Keys.D0, Keys.D1, Keys.D2, Keys.D3, Keys.D4, Keys.D5, Keys.D6, Keys.D7, Keys.D8, Keys.D9 };
            return nums.Contains(key);
        }
        #endregion
    }
}