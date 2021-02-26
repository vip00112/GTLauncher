using ICSharpCode.SharpZipLib.Zip;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public static class ZipUtil
    {
        public static bool Unzip(string zipPath, string targetDir, bool deleteZip)
        {
            try
            {
                var fastZip = new FastZip();
                fastZip.ExtractZip(zipPath, targetDir, null);
            }
            catch (Exception e)
            {
                Logger.Error(e);
                return false;
            }

            try { File.Delete(zipPath); } catch { }
            return true;
        }
    }
}