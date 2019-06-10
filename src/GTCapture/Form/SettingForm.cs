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
        public Dictionary<CaptureMode, HotKey> _hotKeys;

        #region Constructor
        internal SettingForm()
        {
            InitializeComponent();

            _textBoxs = new List<TextBox>();
            _textBoxs.Add(textBox_fullScreen);
            _textBoxs.Add(textBox_activeProcess);
            _textBoxs.Add(textBox_region);
        }
        #endregion

        #region Control Event
        private void SettingForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            textBox_fullScreen.Tag = CaptureMode.FullScreen;
            textBox_activeProcess.Tag = CaptureMode.ActiveProcess;
            textBox_region.Tag = CaptureMode.Region;

            _hotKeys = Setting.HotKeys.ToDictionary(o => o.Key, o => o.Value);
            foreach (var mode in _hotKeys.Keys)
            {
                var owner = _textBoxs.FirstOrDefault(o => (CaptureMode) o.Tag == mode);
                if (owner == null) continue;

                owner.Text = _hotKeys[mode].ToString();
            }
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

        private void button_save_Click(object sender, EventArgs e)
        {
            if (!MessageBoxUtil.Confirm("Are you sure you want to save setting?")) return;

            Setting.HotKeys = _hotKeys;

            Setting.Save();

            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
