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

            if (hotKey.Modifier.HasFlag(KeyModifiers.Alt))
            {
                checkBox_alt.Checked = true;
            }
            if (hotKey.Modifier.HasFlag(KeyModifiers.Control))
            {
                checkBox_control.Checked = true;
            }
            if (hotKey.Modifier.HasFlag(KeyModifiers.Shift))
            {
                checkBox_shift.Checked = true;
            }
            comboBox_key.SelectedItem = hotKey.Key;
        }
        #endregion

        #region Properties
        public KeyModifiers Modifiers { get; private set; }

        public Keys Key { get; private set; }

        public string Result { get; set; }
        #endregion

        #region Control Event
        private void button_ok_Click(object sender, EventArgs e)
        {
            if (comboBox_key.SelectedItem == null)
            {
                MessageBoxUtil.Error("You must select key.");
                return;
            }

            Result = null;
            if (checkBox_alt.Checked)
            {
                Result += "Alt + ";
                Modifiers |= KeyModifiers.Alt;
            }
            if (checkBox_control.Checked)
            {
                Result += "Control + ";
                Modifiers |= KeyModifiers.Control;
            }
            if (checkBox_shift.Checked)
            {
                Result += "Shift + ";
                Modifiers |= KeyModifiers.Shift;
            }
            var key = (Keys) comboBox_key.SelectedItem;
            Key = key;
            Result += key.ToString();

            string msg = string.Format("Are you save this setting?\r\n({0})", Result);
            if (!MessageBoxUtil.Confirm(msg)) return;

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
