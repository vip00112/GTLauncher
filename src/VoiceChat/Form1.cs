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

                button1.Enabled = false;

                var m = new Manager();
                int deviceNum = dialog.InputDeviceNumber;
                m.Connected += delegate (object sender2, ConnectedEventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { Connected(e2.Host); });
                };
                m.Disconnected += delegate (object sender2, DisconnectedEventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { Disconnected(e2.Exception); });
                };
                m.OtherClientConnected += delegate (object sender2, ConnectedEventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { OtherClientConnected(e2.Host); });
                };
                m.OtherClientDisconnected += delegate (object sender2, DisconnectedEventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { OtherClientDisconnected(e2.Host); });
                };
                m.StartClient(textBox1.Text, 7080, deviceNum);
            }
        }

        private void Connected(string host)
        {
            listBox1.Items.Add(string.Format("-> {0}", host));
        }

        private void Disconnected(Exception e)
        {
            if (e != null)
            {
                MessageBox.Show(e.Message);
            }

            button1.Enabled = true;
            listBox1.Items.Clear();
        }

        private void OtherClientConnected(string host)
        {
            listBox1.Items.Add(host);
        }

        private void OtherClientDisconnected(string host)
        {
            listBox1.Items.Remove(host);
        }
    }
}
