using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class ServerUser
    {
        #region Constructor
        public ServerUser(Socket socket, string name)
        {
            Socket = socket;
            Name = name;
            Host = Socket.RemoteEndPoint.ToString();
            CreatedOn = DateTime.Now;
        }
        #endregion

        #region Properties
        public Socket Socket { get; }

        public string Name { get; }

        public string Host { get; } // IP:PORT

        public DateTime CreatedOn { get; }
        #endregion

        #region Public Method
        public void Send(Packet packet)
        {
            byte[] data = Packet.Pack(packet);
            Socket.Send(data);
        }

        public void Dispose()
        {
            if (Socket != null) Socket.Close();
        }
        #endregion
    }
}
