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

namespace GTVoiceChat
{
    public partial class ChatClientForm : Form
    {
        public EventHandler InputDeviceMuted;

        private bool _isMouseDown;
        private Point _mouseDownPoint;
        private Manager _manager;
        private bool _isInputDeviceMuted;
        private bool _isOutputDeviceMuted;
        private List<OnlineUser> _users;

        #region Constructor
        private ChatClientForm()
        {
            InitializeComponent();

            _users = new List<OnlineUser>();
            textBox_input.MaxLength = Manager.MaxLengthText;
        }

        public ChatClientForm(Manager manager) : this()
        {
            _manager = manager;
        }
        #endregion

        #region Control Event
        private void ChatClientForm_Load(object sender, EventArgs e)
        {
            _manager.Connected += OnConnected;
            _manager.Disconnected += OnDisconnected;
            _manager.OtherClientConnected += OnOtherClientConnected;
            _manager.OtherClientDisconnected += OnOtherClientDisconnected;
            _manager.ReceiveMessage += OnReceiveMessage;

            if (!_manager.StartClient())
            {
                _manager.Connected -= OnConnected;
                _manager.Disconnected -= OnDisconnected;
                _manager.OtherClientConnected -= OnOtherClientConnected;
                _manager.OtherClientDisconnected -= OnOtherClientDisconnected;
                _manager.ReceiveMessage -= OnReceiveMessage;
                Close();
            }
        }

        private void ChatClientForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            _manager.StopClient();
        }

        private void button_close_Click(object sender, EventArgs e)
        {
            if (!MessageBoxUtil.Confirm("Are you sure you want to close?")) return;
            Close();
        }

        private void textBox_input_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode != Keys.Enter) return;

            string text = textBox_input.Text;
            textBox_input.Text = null;

            if (string.IsNullOrWhiteSpace(text)) return;

            if (text.Length > Manager.MaxLengthText) text = text.Substring(0, Manager.MaxLengthText);
            _manager.SendMessageToServer(text);

            AddChat(_manager.ChatSetting.Name, text);
        }

        private void ChatClientForm_MouseDown(object sender, MouseEventArgs e)
        {
            _isMouseDown = true;
            _mouseDownPoint = e.Location;
        }

        private void ChatClientForm_MouseMove(object sender, MouseEventArgs e)
        {
            if (!_isMouseDown) return;

            int diffX = _mouseDownPoint.X - e.Location.X;
            int diffY = _mouseDownPoint.Y - e.Location.Y;

            int x = Location.X - diffX;
            int y = Location.Y - diffY;
            Location = new Point(x, y);
        }

        private void ChatClientForm_MouseUp(object sender, MouseEventArgs e)
        {
            _isMouseDown = false;
        }

        private void trackBar_volumeOut_ValueChanged(object sender, EventArgs e)
        {
            int value = trackBar_volumeOut.Value * 10;
            float volume = value / 100F;
            _manager.ChangeGeneralVolume(volume);
        }

        private void pictureBox_out_Click(object sender, EventArgs e)
        {
            if (_isOutputDeviceMuted)
            {
                pictureBox_out.Image = Properties.Resources.out_on_64x64;
                _isOutputDeviceMuted = false;
                _manager.ChangeGeneralVolume(1);
            }
            else
            {
                pictureBox_out.Image = Properties.Resources.out_off_64x64;
                _isOutputDeviceMuted = true;
                _manager.ChangeGeneralVolume(0);
            }
        }

        private void pictureBox_in_Click(object sender, EventArgs e)
        {
            if (_isInputDeviceMuted)
            {
                pictureBox_in.Image = Properties.Resources.in_on_64x64;
                _isInputDeviceMuted = false;
            }
            else
            {
                pictureBox_in.Image = Properties.Resources.in_off_64x64;
                _isInputDeviceMuted = true;
            }
            _manager.MuteInputDevice(_isInputDeviceMuted);
        }
        #endregion

        #region ChatManager Event
        private void OnConnected(object sender, ConnectedEventArgs e)
        {
            if (e.OnlineUserNames != null)
            {
                foreach (var onlineUserName in e.OnlineUserNames)
                {
                    AddUser(onlineUserName);
                }
            }
            AddUser(e.Name);
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            if (e.Exception != null) MessageBoxUtil.Error(e.Exception.Message);

            var names = _users.Select(o => o.UserName).ToArray();
            foreach (var name in names)
            {
                RemoveUser(name);
            }
        }

        private void OnOtherClientConnected(object sender, ConnectedEventArgs e)
        {
            AddUser(e.Name);
        }

        private void OnOtherClientDisconnected(object sender, DisconnectedEventArgs e)
        {
            RemoveUser(e.Name);
        }

        private void OnReceiveMessage(object sender, MessageEventArgs e)
        {
            AddChat(e.Name, e.Text);
        }

        private void OnVolumeChanged(object sender, ChangeVolumeEventArgs e)
        {
            _manager.ChangeVolume(e.Name, e.Volume);
        }
        #endregion

        #region Private Method
        private void AddChat(string name, string text)
        {
            if (richTextBox_output.InvokeRequired)
            {
                richTextBox_output.Invoke((MethodInvoker) delegate { AddChat(name, text); });
            }
            else
            {
                if (richTextBox_output.TextLength > 2000000000)
                {
                    richTextBox_output.Clear();
                }

                string chat = string.Format("[{0}] : {1}\r\n", name, text);
                richTextBox_output.AppendText(chat);
                richTextBox_output.ScrollToCaret();
            }
        }

        private void AddUser(string name)
        {
            if (flowLayoutPanel_user.InvokeRequired)
            {
                flowLayoutPanel_user.Invoke((MethodInvoker) delegate { AddUser(name); });
            }
            else
            {
                var user = new OnlineUser(name);
                user.VolumeChanged += OnVolumeChanged;
                _users.Add(user);
                flowLayoutPanel_user.Controls.Add(user);
            }
        }

        private void RemoveUser(string name)
        {
            if (flowLayoutPanel_user.InvokeRequired)
            {
                flowLayoutPanel_user.Invoke((MethodInvoker) delegate { RemoveUser(name); });
            }
            else
            {
                var user = _users.FirstOrDefault(o => o.UserName == name);
                if (user == null) return;

                user.VolumeChanged -= OnVolumeChanged;
                _users.Remove(user);
                flowLayoutPanel_user.Controls.Add(user);
                user.Dispose();
            }
        }
        #endregion
    }
}
