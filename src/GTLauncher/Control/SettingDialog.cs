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
            if (DesignMode) return;

            // Load From Settings
            LoadGeneralSetting();
            LoadLayoutSetting();
            LoadCaptureSetting();

            // Hide TabControl Header
            tabControl.Appearance = TabAppearance.FlatButtons;
            tabControl.ItemSize = new Size(0, 1);
            tabControl.SizeMode = TabSizeMode.Fixed;

            // Listview Setting
            foreach (ListViewItem li in listView.Items)
            {
                var tag = li.Tag as string;

                if (tag == "General") li.Tag = tabPage_general;
                else if (tag == "Layout") li.Tag = tabPage_layout;
                else if (tag == "Capture") li.Tag = tabPage_capture;
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
        }

        private void listView_DrawItem(object sender, DrawListViewItemEventArgs e)
        {
            if (e.Item.Focused)
            {
                e.Item.BackColor = Color.LightSkyBlue;
            }
            else
            {
                e.Item.BackColor = Color.White;
            }

            e.DrawBackground();
            e.DrawText();
        }

        #region GeneralSetting
        private void checkBox_runOnStartup_CheckedChanged(object sender, EventArgs e)
        {
            GeneralSetting.RunOnStartup = checkBox_runOnStartup.Checked;
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
        private void textBox_hotKey_Click(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            if (textBox.Tag == null) return;

            var mode = (CaptureMode) textBox.Tag;
            var hotKey = CaptureSetting.HotKeys[mode];
            using (var dialog = new HotKeySettingDialog(hotKey))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                var addeds = CaptureSetting.HotKeys.Where(o => o.Value.Modifiers == hotKey.Modifiers && o.Value.Key == hotKey.Key);
                foreach (var added in addeds)
                {
                    if (added.Value == hotKey) continue;

                    var owner = _textBoxs.FirstOrDefault(o => (CaptureMode) o.Tag == added.Key);
                    if (owner != null) owner.Text = "";

                    added.Value.Modifiers = KeyModifiers.None;
                    added.Value.Key = Keys.None;
                }

                hotKey.Modifiers = dialog.HotKey.Modifiers;
                hotKey.Key = dialog.HotKey.Key;
                textBox.Text = dialog.HotKey.ToString();
            }
        }

        private void numericUpDown_timer_ValueChanged(object sender, EventArgs e)
        {
            CaptureSetting.Timer = (int) numericUpDown_timer.Value;
        }

        private void comboBox_imageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (!_isLoaded) return;
            CaptureSetting.SaveImageFormat = (string) comboBox_imageFormat.SelectedItem;
        }

        private void button_dirPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.RootFolder = Environment.SpecialFolder.Desktop;
            fb.SelectedPath = CaptureSetting.SaveDirectory;
            if (fb.ShowDialog() != DialogResult.OK) return;

            CaptureSetting.SaveDirectory = fb.SelectedPath;
            textBox_dirPath.Text = fb.SelectedPath;
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
            textBox_dirPath.Text = CaptureSetting.SaveDirectory;
            comboBox_imageFormat.DataSource = CaptureSetting.ImageFormats;
            comboBox_imageFormat.SelectedItem = CaptureSetting.SaveImageFormat;
        }
        #endregion
    }
}