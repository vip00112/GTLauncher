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
    public partial class Form1 : Form
    {
        private Manager _chatManager;

        public Form1()
        {
            InitializeComponent();
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            _chatManager = new Manager();
            _chatManager.Disconnected += OnDisconnected;
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
            if (!_chatManager.InitSetting(false)) return;

            button1.Enabled = false;
            _chatManager.ShowClientForm();
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            button1.Enabled = true;
        }
    }
}
