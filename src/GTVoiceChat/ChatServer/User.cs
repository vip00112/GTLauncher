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
        public EventHandler OnDisposed;
        public EventHandler<DecodedEventArgs> OnDecoded;

        private INetworkChatCodec _codec;

        #region Constructor
        public User(Socket socket, INetworkChatCodec codec)
        {
            _codec = codec;

            ID = Guid.NewGuid().ToString();
            Socket = socket;
            Host = socket.RemoteEndPoint.ToString();
            Buffer = new byte[1024 * 4];
        }
        #endregion

        #region Properties
        public string ID { get; }

        public Socket Socket { get; private set; }

        public bool IsAlive { get { return Socket != null && Socket.Connected; } }

        public string Host { get; } // IP:PORT

        public byte[] Buffer { get; }

        public int BufferOffset { get; set; }

        public int BufferSize { get { return Buffer.Length - BufferOffset; } }
        #endregion

        #region Public Method
        public void Dispose()
        {
            if (Socket != null)
            {
                Socket.Disconnect(false);
                Socket.Dispose();
                Socket = null;
            }
            _codec = null;

            if (OnDisposed != null) OnDisposed(this, EventArgs.Empty);
        }

        public void ReceivePacket()
        {
            SocketAsyncEventArgs e = new SocketAsyncEventArgs();
            e.SetBuffer(Buffer, BufferOffset, BufferSize);
            e.Completed += ReceiveComplete;
            if (!Socket.ReceiveAsync(e))
            {
                ReceiveProcess(e);
            }
        }

        public void SendPacket(object sender, WaveInEventArgs e)
        {
            if (!IsAlive)
            {
                Dispose();
                return;
            }

            byte[] encoded = _codec.Encode(e.Buffer, 0, e.BytesRecorded);
            bool isSuccess = Socket.Send(encoded) > 0;
            if (!isSuccess) Dispose();
        }
        #endregion

        #region Private Method
        private void ReceiveComplete(object sender, SocketAsyncEventArgs e)
        {
            ReceiveProcess(e);
        }

        private void ReceiveProcess(SocketAsyncEventArgs e)
        {
            if (e.SocketError != SocketError.Success || e.BytesTransferred == 0 || !IsAlive)
            {
                Dispose();
                return;
            }

            int size = e.BytesTransferred;

            // 패킷 처리
            byte[] packet = new byte[size];
            System.Buffer.BlockCopy(Buffer, 0, packet, 0, size);
            System.Buffer.BlockCopy(Buffer, size, Buffer, 0, size);
            e.SetBuffer(0, BufferSize);

            byte[] decoded = _codec.Decode(packet, 0, packet.Length);
            if (OnDecoded != null) OnDecoded(this, new DecodedEventArgs(decoded));
            //_waveProvider.AddSamples(decoded, 0, decoded.Length);

            if (!Socket.ReceiveAsync(e))
            {
                ReceiveProcess(e);
            }
        }
        #endregion
    }
}
