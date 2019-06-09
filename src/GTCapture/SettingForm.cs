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

        public SettingForm()
        {
            InitializeComponent();

            _textBoxs = new List<TextBox>();
            _textBoxs.Add(textBox_fullScreen);
            _textBoxs.Add(textBox_activeProcess);
            _textBoxs.Add(textBox_region);

            _hotKeys = new Dictionary<CaptureMode, HotKey>();
            foreach (CaptureMode mode in Enum.GetValues(typeof(CaptureMode)))
            {
                _hotKeys.Add(mode, new HotKey());
            }

            textBox_fullScreen.Tag = CaptureMode.FullScreen;
            textBox_activeProcess.Tag = CaptureMode.ActiveProcess;
            textBox_region.Tag = CaptureMode.Region;

            // TODO : CaptureMode별 핫키 등록 후 TextBox에 표기
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

                // TODO : 동일한 핫키 삭제
                var added = _hotKeys.FirstOrDefault(o => o.Value.Modifiers == hotKey.Modifiers && o.Value.Key == hotKey.Key);
                if (added.Value != null)
                {
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

    }
}
