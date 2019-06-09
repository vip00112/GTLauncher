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
    public partial class HotKeySettingForm : Form
    {
        private List<CheckBox> _checkBoxs;
        private HotKey _hotKey;

        #region Constructor
        private HotKeySettingForm()
        {
            InitializeComponent();

            _checkBoxs = new List<CheckBox>();

            _checkBoxs.Add(checkBox_alt);
            _checkBoxs.Add(checkBox_control);
            _checkBoxs.Add(checkBox_shift);

            foreach (Keys key in Enum.GetValues(typeof(Keys)))
            {
                if (IsUseKey(key))
                {
                    comboBox_key.Items.Add(key);
                }
            }
        }

        public HotKeySettingForm(HotKey hotKey) : this()
        {
            if (hotKey == null) return;

            HotKey = hotKey;
        }
        #endregion

        #region Properties
        public HotKey HotKey
        {
            get { return _hotKey; }
            private set
            {
                _hotKey = value;
                if (_hotKey.Modifiers.HasFlag(KeyModifiers.Alt))
                {
                    checkBox_alt.Checked = true;
                }
                if (_hotKey.Modifiers.HasFlag(KeyModifiers.Control))
                {
                    checkBox_control.Checked = true;
                }
                if (_hotKey.Modifiers.HasFlag(KeyModifiers.Shift))
                {
                    checkBox_shift.Checked = true;
                }
                comboBox_key.SelectedItem = _hotKey.Key;
            }
        }
        #endregion

        #region Control Event
        private void button_ok_Click(object sender, EventArgs e)
        {
            if (comboBox_key.SelectedItem == null)
            {
                MessageBoxUtil.Error("You must select key.");
                return;
            }
            if (!MessageBoxUtil.Confirm("Are you save this setting?")) return;

            if (checkBox_alt.Checked)
            {
                HotKey.Modifiers |= KeyModifiers.Alt;
            }
            if (checkBox_control.Checked)
            {
                HotKey.Modifiers |= KeyModifiers.Control;
            }
            if (checkBox_shift.Checked)
            {
                HotKey.Modifiers |= KeyModifiers.Shift;
            }
            HotKey.Key = (Keys) comboBox_key.SelectedItem;

            DialogResult = DialogResult.OK;
        }
        #endregion

        #region Private Method
        private bool IsUseKey(Keys key)
        {
            // 0 ~ 1
            if (key >= Keys.D0 && key <= Keys.D9) return true;

            // A ~ Z
            if (key >= Keys.A && key <= Keys.Z) return true;

            // F1 ~ F12
            if (key >= Keys.F1 && key <= Keys.F12) return true;
            return false;
        }
        #endregion
    }
}
