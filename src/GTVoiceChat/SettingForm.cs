using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTVoiceChat
{
    public partial class SettingForm : Form
    {
        public SettingForm()
        {
            InitializeComponent();
        }

        public int InputDeviceNumber { get; private set; }

        public int OutputDeviceNumber { get; private set; }

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
        }

        private void button_save_Click(object sender, EventArgs e)
        {
            //InputDeviceNumber = comboBox_inputDevice.SelectedIndex;
            //OutputDeviceNumber = comboBox_outputDevice.SelectedIndex;
            //DialogResult = DialogResult.OK;

            //var m = new Manager();
            //var endPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 7080);
            //int deviceNum = comboBox_inputDevice.SelectedIndex;
            //m.Connect(endPoint, deviceNum);

            var m = new Manager();
            int deviceNum = comboBox_inputDevice.SelectedIndex;
            m.StartServer(7080, deviceNum);
            //m.StartClient("127.0.0.1", 7080, deviceNum);
        }
    }
}
