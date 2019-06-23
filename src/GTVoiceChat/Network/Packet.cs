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

        public string SendText { get; set; }

        public byte[] AudioData { get; set; }

        public string FileName { get; set; }

        public byte[] FileData { get; set; }

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

        public static byte[] Pack(Packet packet)
        {
            var result = new List<byte>();

            result.AddRange(BitConverter.GetBytes((int) packet.Type));
            Pack(result, packet.SendUserName);
            Pack(result, packet.OnlineUserNames);
            Pack(result, packet.SendText);
            Pack(result, packet.AudioData);
            Pack(result, packet.FileName);
            Pack(result, packet.FileData);
            result.InsertRange(0, BitConverter.GetBytes(result.Count));
            return result.ToArray();
        }

        public static Packet UnPack(byte[] packetData)
        {
            int size = BitConverter.ToInt32(packetData, 0);
            byte[] data = new byte[size];
            Buffer.BlockCopy(packetData, 4, data, 0, size);

            int offset = 0;
            var packet = new Packet();
            packet.Type = (PacketType) BitConverter.ToInt32(data, 0);
            offset += 4;

            packet.SendUserName = UnPack<string>(data, ref offset);

            var onlineUserNames = UnPack<string>(data, ref offset);
            if (!string.IsNullOrWhiteSpace(onlineUserNames))
            {
                packet.OnlineUserNames = onlineUserNames.Split(new string[] { "</EndStr>" }, StringSplitOptions.RemoveEmptyEntries);
            }
            else
            {
                packet.OnlineUserNames = new string[] { };
            }

            packet.SendText = UnPack<string>(data, ref offset);
            packet.AudioData = UnPack<byte[]>(data, ref offset);
            packet.FileName = UnPack<string>(data, ref offset);
            packet.FileData = UnPack<byte[]>(data, ref offset);
            return packet;
        }

        private static void Pack(List<byte> result, object value)
        {
            if (value == null)
            {
                result.AddRange(BitConverter.GetBytes(0));
                return;
            }

            if (value is string)
            {
                var data = value as string;
                if (string.IsNullOrWhiteSpace(data))
                {
                    result.AddRange(BitConverter.GetBytes(0));
                    return;
                }

                byte[] bytes = Encoding.UTF8.GetBytes(data);
                result.AddRange(BitConverter.GetBytes(bytes.Length));
                result.AddRange(bytes);
            }
            else if (value is string[])
            {
                var data = value as string[];
                if (data.Length == 0)
                {
                    result.AddRange(BitConverter.GetBytes(0));
                    return;
                }

                string line = string.Join("</EndStr>", data);
                byte[] bytes = Encoding.UTF8.GetBytes(line);
                result.AddRange(BitConverter.GetBytes(bytes.Length));
                result.AddRange(bytes);
            }
            else if (value is byte[])
            {
                var data = value as byte[];
                if (data.Length == 0)
                {
                    result.AddRange(BitConverter.GetBytes(0));
                    return;
                }

                result.AddRange(BitConverter.GetBytes(data.Length));
                result.AddRange(data);
            }
        }

        private static T UnPack<T>(byte[] data, ref int offset)
        {
            int length = BitConverter.ToInt32(data, offset);
            offset += 4;
            if (length == 0) return default(T);

            offset += length;
            if (typeof(T) == typeof(string))
            {
                string value = Encoding.UTF8.GetString(data, offset, length);
                return (T) (object) value;
            }
            else if (typeof(T) == typeof(byte[]))
            {
                byte[] value = new byte[length];
                Buffer.BlockCopy(data, offset, value, 0, length);
                return (T) (object) value;
            }

            return default(T);
        }
        #endregion
    }
}
