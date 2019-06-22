using GTUtil;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace GTVoiceChat
{
    public class Manager
    {
        public const int MaxLengthName = 8;
        public const int MaxLengthText = 65535;
        public const int MaxLengthFile = 1024 * 50; // 50MB

        // Server EventHandler
        public EventHandler<DisconnectedEventArgs> ServerClosed;

        // Client EventHandler
        public EventHandler<ConnectedEventArgs> Connected;
        public EventHandler<DisconnectedEventArgs> Disconnected;
        public EventHandler<ConnectedEventArgs> OtherClientConnected;
        public EventHandler<DisconnectedEventArgs> OtherClientDisconnected;
        public EventHandler<MessageEventArgs> ReceiveMessage;

        private TcpServer _server;
        private TcpClient _client;
        private bool _startedServer;
        private bool _startedClient;

        #region Constructor
        public Manager()
        {
            ChatSetting = new ChatSetting();
        }
        #endregion

        #region Properties
        public ChatSetting ChatSetting { get; private set; }
        #endregion

        #region Public Method
        public bool InitSetting(bool isServerSetting)
        {
            using (var dialog = new SettingForm(ChatSetting, isServerSetting))
            {
                if (dialog.ShowDialog() != DialogResult.OK) return false;

                ChatSetting = dialog.ChatSetting;
                return true;
            }
        }

        #region Server
        public bool StartServer()
        {
            if (ChatSetting == null) return false;
            if (_startedServer) return false;

            _startedServer = true;
            _server = new TcpServer(ChatSetting.Port);
            _server.ServerClosed += OnServerClosed;
            if (!_server.Start())
            {
                _startedServer = false;
                _server.ServerClosed -= OnServerClosed;
                _server = null;
                return false;
            }
            return true;
        }

        public void StopServer()
        {
            if (!_startedServer) return;

            _startedServer = false;
            _server.Stop();
            _server.ServerClosed -= OnServerClosed;
        }
        #endregion

        #region Client
        public void ShowClientForm()
        {
            var form = new ChatClientForm(this);
            form.Show();
            form.Activate();
        }

        public bool StartClient()
        {
            if (ChatSetting == null) return false;
            if (_startedClient) return false;

            string name = ChatSetting.Name;
            string ip = ChatSetting.IP;
            int port = ChatSetting.Port;
            int deviceNum = ChatSetting.InputDeviceNumber;
            if (string.IsNullOrWhiteSpace(name) || name.Length > MaxLengthName)
            {
                MessageBoxUtil.Error(string.Format("Name length must between 1 to {0}.", MaxLengthName));
                return false;
            }

            _startedClient = true;
            _client = new TcpClient(ip, port, name, deviceNum);
            _client.Connected += OnConnected;
            _client.Disconnected += OnDisconnected;
            _client.OtherClientConnected += OnOtherClientConnected;
            _client.OtherClientDisconnected += OnOtherClientDisconnected;
            _client.ReceiveMessage += OnReceiveMessage;

            if (!_client.Start())
            {
                _startedClient = false;
                _client.Connected -= OnConnected;
                _client.Disconnected -= OnDisconnected;
                _client.OtherClientConnected -= OnOtherClientConnected;
                _client.OtherClientDisconnected -= OnOtherClientDisconnected;
                _client.ReceiveMessage -= OnReceiveMessage;
                _client = null;
                return false;
            }
            return true;
        }

        public void StopClient()
        {
            if (!_startedClient) return;

            _startedClient = false;
            _client.Stop();
            _client.Connected -= OnConnected;
            _client.Disconnected -= OnDisconnected;
            _client.OtherClientConnected -= OnOtherClientConnected;
            _client.OtherClientDisconnected -= OnOtherClientDisconnected;
            _client.ReceiveMessage -= OnReceiveMessage;
        }

        public void SendMessageToServer(string text)
        {
            if (_client == null) return;

            if (text.Length > MaxLengthText) text = text.Substring(0, MaxLengthText);
            _client.SendPacket(new Packet() { Type = PacketType.Text, SendUserName = ChatSetting.Name, SendText = text });
        }

        public void SendFileToServer(string fileName, byte[] fileData)
        {
            if (_client == null) return;
            if (fileData.Length > MaxLengthFile)
            {
                string mb = (MaxLengthFile / 1024) + "MB";
                MessageBoxUtil.Error(string.Format("Maximun file size is {0}.", mb));
                return;
            }

            _client.SendPacket(new Packet() { Type = PacketType.File, SendUserName = ChatSetting.Name, FileName = fileName, FileData = fileData });
        }

        public void ChangeVolume(string name, float volume)
        {
            _client.ChangeVolume(name, volume);
        }

        public void ChangeGeneralVolume(float generalVolume)
        {
            _client.ChangeGeneralVolume(generalVolume);
        }

        public void MuteInputDevice(bool isMute)
        {
            _client.MuteInputDevice(isMute);
        }
        #endregion
        #endregion

        #region Private Method
        private void OnServerClosed(object sender, DisconnectedEventArgs e)
        {
            ServerClosed?.Invoke(sender, e);
        }

        private void OnConnected(object sender, ConnectedEventArgs e)
        {
            Connected?.Invoke(sender, e);
        }

        private void OnDisconnected(object sender, DisconnectedEventArgs e)
        {
            Disconnected?.Invoke(sender, e);
        }

        private void OnOtherClientConnected(object sender, ConnectedEventArgs e)
        {
            OtherClientConnected?.Invoke(sender, e);
        }

        private void OnOtherClientDisconnected(object sender, DisconnectedEventArgs e)
        {
            OtherClientDisconnected?.Invoke(sender, e);
        }

        private void OnReceiveMessage(object sender, MessageEventArgs e)
        {
            ReceiveMessage?.Invoke(sender, e);
        }
        #endregion
    }
}
