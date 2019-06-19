using GTCapture;
using GTControl;
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

namespace GTLauncher
{
    public partial class MainForm : PageContainer
    {
        private Capture _capture;

        public MainForm()
        {
            InitializeComponent();

            using (var dialog = new GTVoiceChat.SettingForm())
            {
                if (dialog.ShowDialog() != DialogResult.OK) return;


                //var m = new Manager();
                //var endPoint = new System.Net.IPEndPoint(System.Net.IPAddress.Parse("127.0.0.1"), 7080);
                //int deviceNum = comboBox_inputDevice.SelectedIndex;
                //m.Connect(endPoint, deviceNum);

                // 서버 개설
                var m = new GTVoiceChat.Manager();
                int deviceNum = dialog.InputDeviceNumber;
                m.StartServer(7080, deviceNum);

                // 내 서버에 참가
                //m.StartClient("127.0.0.1", 7080, deviceNum);

                // TODO : 다중 클라이언트 접속시 Codec의 OutOfIndex 이슈
            }
        }

        #region Control Event
        private void MainForm_Load(object sender, EventArgs e)
        {
            if (DesignMode) return;

            _capture = new Capture(Handle);
            _capture.OnCaptured += OnCaptured;
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

        private void OnCaptured(object sender, EventArgs e)
        {
            notifyIcon.BalloonTipText = "Capture completed.";
            notifyIcon.BalloonTipIcon = ToolTipIcon.Info;
            notifyIcon.ShowBalloonTip(500);
        }
        #endregion
    }
}
