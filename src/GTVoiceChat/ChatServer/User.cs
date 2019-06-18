using GTUtil;
using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Sockets;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class User
    {
        public EventHandler<DataReceiveEventArgs> DataReceived;
        public EventHandler<DisposedEventArgs> Disposed;

        private object _lock;

        #region Constructor
        public User(Socket socket)
        {
            _lock = new object();
            Socket = socket;
            Buffer = new byte[1024 * 4];
            UserData = new UserData(socket.RemoteEndPoint.ToString());
        }
        #endregion

        #region Properties
        public Socket Socket { get; private set; }

        public bool IsAlive { get { return Socket != null && Socket.Connected; } }

        public byte[] Buffer { get; }

        public int BufferOffset { get; set; }

        public int BufferSize { get { return Buffer.Length - BufferOffset; } }

        public UserData UserData { get; }

        public string ID { get { return UserData.ID; } }

        public string Host { get { return UserData.Host; } }
        #endregion

        #region Public Method
        public void Dispose(Exception e = null)
        {
            lock(_lock)
            {
                if (Socket != null)
                {
                    Socket.Disconnect(false);
                    Socket.Dispose();
                    Socket = null;
                }
                if (Disposed != null) Disposed(this, new DisposedEventArgs(e));
            }
        }

        public void StartReceive()
        {
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.SetBuffer(Buffer, 0, Buffer.Length);
            e.Completed += ReceiveComplete;
            try
            {
                if (!Socket.ReceiveAsync(e))
                {
                    ReceiveProcess(e);
                }
            }
            catch (Exception ex)
            {
                Dispose(ex);
            }
        }

        public void StartSend(Packet packet)
        {
            if (packet == null) return;

            byte[] data = Packet.ToData(packet);
            if (data == null) return;

            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.SetBuffer(data, 0, data.Length);
            e.Completed += SendComplete;
            try
            {
                if (!Socket.SendAsync(e))
                {
                    SendProcess(e);
                }
            }
            catch (Exception ex)
            {
                Dispose(ex);
            }
        }
        #endregion

        #region Private Method
        private void ReceiveComplete(object sender, SocketAsyncEventArgs e)
        {
            ReceiveProcess(e);
        }

        private void ReceiveProcess(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError != SocketError.Success || e.BytesTransferred == 0 || !IsAlive)
                {
                    Dispose();
                    return;
                }

                BufferOffset += e.BytesTransferred;
                int size = Packet.GetPacketSize(Buffer) + 2;
                if (size != 0 && size <= BufferOffset)
                {
                    byte[] data = new byte[size];
                    System.Buffer.BlockCopy(Buffer, 0, data, 0, size);
                    System.Buffer.BlockCopy(Buffer, size, Buffer, 0, BufferOffset - size);
                    BufferOffset -= size;
                    e.SetBuffer(BufferOffset, BufferSize);

                    PacketHandler(data);
                }

                //int size = e.BytesTransferred;

                //// 패킷 처리
                //byte[] data = new byte[size];
                //System.Buffer.BlockCopy(Buffer, 0, data, 0, size);
                //Array.Clear(Buffer, 0, Buffer.Length);
                //e.SetBuffer(0, Buffer.Length);

                //PacketHandler(data);

                if (!Socket.ReceiveAsync(e))
                {
                    ReceiveProcess(e);
                }
            }
            catch (Exception ex)
            {
                Dispose(ex);
            }
        }

        private void SendComplete(object sender, SocketAsyncEventArgs e)
        {
            SendProcess(e);
        }

        private void SendProcess(SocketAsyncEventArgs e)
        {
            try
            {
                if (e.SocketError != SocketError.Success || e.BytesTransferred == 0 || !IsAlive)
                {
                    Dispose();
                    return;
                }
            }
            catch (Exception ex)
            {
                Dispose(ex);
            }
        }

        private void PacketHandler(byte[] data)
        {
            var packet = Packet.ToPacket(data);
            if (packet == null) return;

            switch (packet.Type)
            {
                case PacketType.Exit: // 서버퇴장
                    Dispose();
                    break;
                case PacketType.Audiom: // 음성정보
                    if (DataReceived != null) DataReceived(this, new DataReceiveEventArgs(data));
                    break;
            }
        }
        #endregion
    }

    public class UserData
    {
        public UserData(string host)
        {
            ID = Guid.NewGuid().ToString();
            Host = host;
        }

        public string ID { get; }

        public string Host { get; } // IP:PORT
    }
}
