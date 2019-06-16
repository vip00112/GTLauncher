using NAudio.Wave;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    public class UncompressedPcmChatCodec : INetworkChatCodec
    {
        public UncompressedPcmChatCodec()
        {
            RecordFormat = new WaveFormat(8000, 16, 1);
        }

        #region Properties
        public string Name { get { return "PCM 8kHz 16 bit uncompressed"; } }

        public bool IsAvailable { get { return true; } }

        public int BitsPerSecond { get { return RecordFormat.AverageBytesPerSecond * 8; } }

        public WaveFormat RecordFormat { get; }
        #endregion

        #region Public Method
        public byte[] Encode(byte[] data, int offset, int length)
        {
            var encoded = new byte[length];
            Array.Copy(data, offset, encoded, 0, length);
            return encoded;
        }

        public byte[] Decode(byte[] data, int offset, int length)
        {
            var decoded = new byte[length];
            Array.Copy(data, offset, decoded, 0, length);
            return decoded;
        }

        public void Dispose() { }
        #endregion
    }
}
