using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public static class GithubUtil
    {
        private const string API = "https://api.github.com/repos/{0}/{1}/releases/latest";

        public static string GetDownloadUrlForLastReleaseAsset(string userName, string repoName, string fileName)
        {
            string url = string.Format(API, userName, repoName);
            using (var wc = new WebClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                wc.Headers.Set(HttpRequestHeader.Accept, "application/vnd.github.v3+json");
                wc.Headers.Set(HttpRequestHeader.UserAgent, "request");
                var response = wc.DownloadString(new Uri(url));
                if (string.IsNullOrWhiteSpace(response)) return null;

                try
                {
                    var continer = JsonConvert.DeserializeObject<GithubJsonContainer>(response);
                    var asset = continer.Assets.FirstOrDefault(o => o.FileName == fileName);
                    if (asset == null) return null;

                    return asset.DownloadUrl;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    return null;
                }

            }
        }
    }
}
