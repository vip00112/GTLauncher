using GTVoiceChat;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VoiceChat
{
    public partial class Form1 : Form
    {
        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            using (var dialog = new SettingForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                var m = new Manager();
                int deviceNum = dialog.InputDeviceNumber;
                m.OnDisposed += delegate (object sender2, EventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { Enable(true); });
                };
                m.StartClient(textBox1.Text, 7080, deviceNum);
                button1.Enabled = false;
            }
        }

        private void Enable(bool enable)
        {
            button1.Enabled = enable;
        }
    }
}
