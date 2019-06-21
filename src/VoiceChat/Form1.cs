using GTUtil;
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
    // TODO : 임시 프로젝트 삭제 후 ClientControl 만들것
    public partial class Form1 : Form
    {
        private GTVoiceChat.Manager _chatManager;
        private string _name;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_chatManager != null)
            {
                _chatManager.StopClient();
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            _name = textBox2.Text;
            if (string.IsNullOrWhiteSpace(_name))
            {
                MessageBoxUtil.Error("Please input your name.");
                return;
            }

            using (var dialog = new SettingForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;

                button1.Enabled = false;

                _chatManager = new Manager();
                int deviceNum = dialog.InputDeviceNumber;
                _chatManager.Connected += delegate (object sender2, ConnectedEventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { Connected(e2.Name, e2.OnlineUserNames); });
                };
                _chatManager.Disconnected += delegate (object sender2, DisconnectedEventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { Disconnected(e2.Exception); });
                };
                _chatManager.OtherClientConnected += delegate (object sender2, ConnectedEventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { OtherClientConnected(e2.Name); });
                };
                _chatManager.OtherClientDisconnected += delegate (object sender2, DisconnectedEventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { OtherClientDisconnected(e2.Name); });
                };
                _chatManager.ReceiveMessage += delegate (object sender2, MessageEventArgs e2)
                {
                    Invoke((MethodInvoker) delegate () { ReceiveMessage(e2.Name, e2.Text); });
                };
                _chatManager.StartClient(textBox1.Text, 7080, _name, deviceNum);
            }
        }

        private void Connected(string name, string[] onlineUserNames)
        {
            listBox1.Items.Add(string.Format("-> {0}", name));

            if (onlineUserNames == null) return;
            foreach (var onlineUserName in onlineUserNames)
            {
                listBox1.Items.Add(onlineUserName);
            }
        }

        private void Disconnected(Exception e)
        {
            if (e != null)
            {
                MessageBoxUtil.Error(e.Message);
            }

            button1.Enabled = true;
            listBox1.Items.Clear();
        }

        private void OtherClientConnected(string name)
        {
            listBox1.Items.Add(name);
        }

        private void OtherClientDisconnected(string name)
        {
            listBox1.Items.Remove(name);
        }

        private void ReceiveMessage(string name, string text)
        {
            richTextBox1.AppendText(string.Format("[{0}] : {1}\r\n", name, text));
            richTextBox1.ScrollToCaret();
        }

        private void textBox3_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string text = textBox3.Text;
            textBox3.Text = null;
            if (_chatManager == null) return;
            if (string.IsNullOrWhiteSpace(text)) return;

            richTextBox1.AppendText(string.Format("[{0}] : {1}\r\n", _name, text));
            richTextBox1.ScrollToCaret();
            _chatManager.SendMessageToServer(_name, text);
        }
    }
}
