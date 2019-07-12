using GTUtil;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTVoiceChat
{
    public partial class SettingForm : Form
    {
        #region Constructor
        private SettingForm()
        {
            InitializeComponent();

            ChatSetting = new ChatSetting();
        }

        public SettingForm(ChatSetting setting, bool isServerSetting) : this()
        {
            ChatSetting = setting;
            if (isServerSetting)
            {
                textBox_ip.Enabled = false;
            }
        }
        #endregion

        #region Properties
        public ChatSetting ChatSetting { get; }
        #endregion

        #region Control Event
        private void SettingForm_Load(object sender, EventArgs e)
        {
            for (int i = 0; i < WaveIn.DeviceCount; i++)
            {
                WaveInCapabilities device = WaveIn.GetCapabilities(i);
                comboBox_inputDevice.Items.Add(device.ProductName);
            }
            if (comboBox_inputDevice.Items.Count > 0)
            {
                comboBox_inputDevice.SelectedIndex = 0;
            }

            for (int i = 0; i < WaveOut.DeviceCount; i++)
            {
                WaveOutCapabilities device = WaveOut.GetCapabilities(i);
                comboBox_outputDevice.Items.Add(device.ProductName);
            }
            if (comboBox_outputDevice.Items.Count > 0)
            {
                comboBox_outputDevice.SelectedIndex = 0;
            }

            textBox_name.Text = ChatSetting.Name;
            textBox_ip.Text = ChatSetting.IP;
            if (ChatSetting.Port < numericUpDown_port.Minimum)
            {
                numericUpDown_port.Value = numericUpDown_port.Minimum;
            }
            else if (ChatSetting.Port > numericUpDown_port.Maximum)
            {
                numericUpDown_port.Value = numericUpDown_port.Maximum;
            }
            else
            {
                numericUpDown_port.Value = ChatSetting.Port;
            }
            comboBox_inputDevice.SelectedIndex = ChatSetting.InputDeviceNumber;
            comboBox_outputDevice.SelectedIndex = ChatSetting.OutputDeviceNumber;
        }

        private void button_ok_Click(object sender, EventArgs e)
        {
            string name = textBox_name.Text;
            if (string.IsNullOrWhiteSpace(name) || name.Length > Manager.MaxLengthName)
            {
                MessageBoxUtil.Error(string.Format("Name length must between 1 to {0}.", Manager.MaxLengthName));
                return;
            }

            string ip = textBox_ip.Text; ;
            IPAddress address;
            if (!IPAddress.TryParse(ip, out address))
            {
                MessageBoxUtil.Error("IP address is invalide format.");
                return;
            }

            ChatSetting.Name = name;
            ChatSetting.IP = ip;
            ChatSetting.Port = (int) numericUpDown_port.Value;
            ChatSetting.InputDeviceNumber = comboBox_inputDevice.SelectedIndex;
            ChatSetting.OutputDeviceNumber = comboBox_outputDevice.SelectedIndex;
            DialogResult = DialogResult.OK;
        }
        #endregion
    }
}
