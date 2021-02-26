using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTLocalization
{
    public static class Resource
    {
        public static string GetString(string key)
        {
            if (string.IsNullOrWhiteSpace(key)) return "";

            string result = Properties.Resource.ResourceManager.GetString(key);
            return string.IsNullOrWhiteSpace(result) ? "" : result;
        }
    }
}
