using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Drawing;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTVoiceChat
{
    // TODO : 패킷 구조 최대한 가볍게 변경
    // TODO : 음성 채팅 프로토콜은 UDP로 개별 채널 뚫어서 사용
    // TODO : 스테레오믹스 꺼져있는데 내 목소리가 상대방 마이크로 들어가는 이슈
    public partial class OnlineUser : UserControl
    {
        public EventHandler<ChangeVolumeEventArgs> VolumeChanged;

        private string _userName;
        private bool _isAdmin;

        #region Constructor
        private OnlineUser()
        {
            InitializeComponent();
        }

        public OnlineUser(string name) : this()
        {
            UserName = name;
            IsAdmin = false;
        }
        #endregion

        #region Properties
        public string UserName
        {
            get { return _userName; }
            set
            {
                _userName = value;
                label_name.Text = _userName;
            }
        }

        public bool IsAdmin
        {
            get { return _isAdmin; }
            set
            {
                _isAdmin = value;
                if (_isAdmin) label_name.ForeColor = Color.Red;
            }
        }
        #endregion

        #region Control Event
        private void trackBar_volumeOut_ValueChanged(object sender, EventArgs e)
        {
            int value = trackBar_volumeOut.Value * 10;
            float volume = value / 100F;
            VolumeChanged?.Invoke(this, new ChangeVolumeEventArgs(UserName, volume));
        }
        #endregion
    }
}