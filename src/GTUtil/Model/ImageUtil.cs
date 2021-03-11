using System;
using System.Collections.Generic;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public static class ImageUtil
    {
        public static string ToBase64(Image img)
        {
            if (img == null) return null;

            byte[] data = ToByteArray(img);
            if (data == null) return null;
            return Convert.ToBase64String(data);
        }

        public static Image FromBase64(string base64)
        {
            if (string.IsNullOrWhiteSpace(base64)) return null;

            byte[] data = Convert.FromBase64String(base64);
            return FromByteArray(data);
        }

        public static byte[] ToByteArray(Image img, ImageFormat format = null)
        {
            if (img == null) return null;

            using (var ms = new MemoryStream())
            {
                if (format == null) format = img.RawFormat;
                img.Save(ms, format);
                return ms.ToArray();
            }
        }

        public static Image FromByteArray(byte[] data)
        {
            if (data == null) return null;

            var ms = new MemoryStream(data, 0, data.Length);
            return Image.FromStream(ms, true);
        }
    }
}
