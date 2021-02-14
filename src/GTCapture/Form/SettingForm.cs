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

namespace GTCapture
{
    public partial class SettingForm : Form
    {
        private List<TextBox> _textBoxs;
        private Dictionary<CaptureMode, HotKey> _hotKeys;
        private static int _timer;
        private static string _saveDirectory;
        private static string _saveImageFormat;

        #region Constructor
        internal SettingForm()
        {
            InitializeComponent();
        }
        #endregion

        #region Control Event
        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            Screen screen = Screen.AllScreens[0];
            Left = screen.WorkingArea.Right - Width;
            Top = screen.WorkingArea.Bottom - Height;

            _textBoxs = new List<TextBox>();
            _textBoxs.Add(textBox_fullScreen);
            _textBoxs.Add(textBox_activeProcess);
            _textBoxs.Add(textBox_region);
            _textBoxs.Add(textBox_recordRegion);
            _textBoxs.Add(textBox_recordStart);
            _textBoxs.Add(textBox_recordStop);

            textBox_fullScreen.Tag = CaptureMode.FullScreen;
            textBox_activeProcess.Tag = CaptureMode.ActiveProcess;
            textBox_region.Tag = CaptureMode.Region;
            textBox_recordRegion.Tag = CaptureMode.RecordRegion;
            textBox_recordStart.Tag = CaptureMode.RecordStart;
            textBox_recordStop.Tag = CaptureMode.RecordStop;

            _hotKeys = Setting.HotKeys.ToDictionary(o => o.Key, o => o.Value);
            foreach (var mode in _hotKeys.Keys)
            {
                var owner = _textBoxs.FirstOrDefault(o => (CaptureMode) o.Tag == mode);
                if (owner == null) continue;

                owner.Text = _hotKeys[mode].ToString();
            }

            _timer = Setting.Timer;
            numericUpDown_timer.Value = _timer;

            _saveDirectory = Setting.SaveDirectory;
            textBox_dirPath.Text = _saveDirectory;

            _saveImageFormat = Setting.SaveImageFormat;
            foreach (string format in Setting.ImageFormats)
            {
                comboBox_imageFormat.Items.Add(format);
            }
            comboBox_imageFormat.SelectedItem = _saveImageFormat;
        }

        private void textBox_hotKey_Click(object sender, EventArgs e)
        {
            var textBox = sender as TextBox;
            if (textBox == null) return;
            if (textBox.Tag == null) return;

            var mode = (CaptureMode) textBox.Tag;
            var hotKey = _hotKeys[mode];
            using (var dialog = new HotKeySettingForm(hotKey))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;
                
                var addeds = _hotKeys.Where(o => o.Value.Modifiers == hotKey.Modifiers && o.Value.Key == hotKey.Key);
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
                button_save.Focus();
            }
        }

        private void numericUpDown_timer_ValueChanged(object sender, EventArgs e)
        {
            _timer = (int) numericUpDown_timer.Value;
        }

        private void comboBox_imageFormat_SelectedIndexChanged(object sender, EventArgs e)
        {
            _saveImageFormat = (string) comboBox_imageFormat.SelectedItem;
        }

        private void button_dirPath_Click(object sender, EventArgs e)
        {
            FolderBrowserDialog fb = new FolderBrowserDialog();
            fb.RootFolder = Environment.SpecialFolder.Desktop;
            fb.SelectedPath = _saveDirectory;
            if (fb.ShowDialog() != DialogResult.OK) return;

            _saveDirectory = fb.SelectedPath;
            textBox_dirPath.Text = _saveDirectory;
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!MessageBoxUtil.Confirm("Are you sure you want to save setting?")) return;

            Setting.HotKeys = _hotKeys;
            Setting.Timer = _timer;
            Setting.SaveDirectory = _saveDirectory;
            Setting.SaveImageFormat = _saveImageFormat;

            Setting.Save();

            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
