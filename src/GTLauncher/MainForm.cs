using GTCapture;
using GTControl;
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

namespace GTLauncher
{
    public partial class MainForm : PageContainer
    {
        private Capture _capture;
        private Manager _chatManager;

        public MainForm()
        {
            InitializeComponent();
        }

        #region Control Event
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _capture = new Capture(Handle);
            _capture.OnCaptured += OnCaptured;

            _chatManager = new Manager();
            _chatManager.ServerClosed += OnServerClosed;
            _chatManager.Disconnected += OnDisconnected;
        }

        private void MainForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (_chatManager != null)
            {
                _chatManager.StopClient();
                _chatManager.StopServer();
            }
        }

        private void notifyIcon_MouseDoubleClick(object sender, MouseEventArgs e)
        {
            Activate();
        }

        private void menuItem_exit_Click(object sender, EventArgs e)
        {
            if (!MessageBoxUtil.Confirm("Are you sure you want to close?")) return;

            Close();
        }

        private void menuItem_captureSetting_Click(object sender, EventArgs e)
        {
            _capture.ShowSettingForm();
        }

        private void menuItem_chatJoin_Click(object sender, EventArgs e)
        {
            if (!_chatManager.InitSetting(false)) return;

            menuItem_chatJoin.Enabled = false;
            menuItem_chatCreate.Enabled = false;
            _chatManager.ShowClientForm();
        }

        private void menuItem_chatCreate_Click(object sender, EventArgs e)
        {
            if (!_chatManager.InitSetting(true)) return;
            if (!_chatManager.StartServer()) return;

            menuItem_chatJoin.Enabled = false;
            menuItem_chatCreate.Enabled = false;
            _chatManager.ShowClientForm();
        }
        #endregion

        #region GTCapture.Capture Event
        private void OnCaptured(object sender, EventArgs e)
        {
            notifyIcon.BalloonTipText = "Capture completed.";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(500);
        }
        #endregion

        #region GTVoiceChat.Manager Event
        private void OnServerClosed(object sender, DisconnectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) delegate { OnDisconnected(sender, e); });
            }
            else
            {
                _chatManager.StopClient();
                _chatManager.StopServer();
                menuItem_chatJoin.Enabled = true;
                menuItem_chatCreate.Enabled = true;
            }
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            if (InvokeRequired)
            {
                Invoke((MethodInvoker) delegate { OnDisconnected(sender, e); });
            }
            else
            {
                _chatManager.StopClient();
                _chatManager.StopServer();
                menuItem_chatJoin.Enabled = true;
                menuItem_chatCreate.Enabled = true;
            }
        }
        #endregion
    }
}
