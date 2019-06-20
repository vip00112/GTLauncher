using GTUtil;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Runtime.Serialization.Formatters.Binary;
using System.Text;
using System.Threading.Tasks;

namespace GTVoiceChat
{
    [Serializable]
    public class Packet
    {
        public PacketType Type { get; set; }

        public string SendUserName { get; set; }

        public string[] OnlineUserNames { get; set; }

        public byte[] AudioData { get; set; }

        #region Static Method
        public static int GetPacketSize(byte[] data)
        {
            int size = data[0] & 0xff;
            size |= data[1] << 8 & 0xff00;
            return size;
        }

        public static byte[] ToData(Packet packet)
        {
            try
            {
                byte[] data = null;
                using (var ms = new MemoryStream())
                {
                    BinaryFormatter bf = new BinaryFormatter();
                    bf.Serialize(ms, packet);
                    byte[] packetData =  ms.ToArray();

                    int size = packetData.Length;
                    data = new byte[size + 2];
                    data[0] = (byte) (size & 0xff);
                    data[1] = (byte) (size >> 8 & 0xff);
                    System.Buffer.BlockCopy(packetData, 0, data, 2, packetData.Length);
                }
                return data;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }

        public static Packet ToPacket(byte[] data)
        {
            try
            {
                Packet packet = null;
                using (var ms = new MemoryStream())
                {
                    int size = GetPacketSize(data);
                    byte[] packetData = new byte[size];
                    System.Buffer.BlockCopy(data, 2, packetData, 0, size);

                    ms.Write(packetData, 0, packetData.Length);
                    ms.Position = 0;

                    BinaryFormatter bf = new BinaryFormatter();
                    packet = (Packet) bf.Deserialize(ms);
                }
                return packet;
            }
            catch (Exception e)
            {
                Logger.Error(e);
            }
            return null;
        }
        #endregion
    }
}
