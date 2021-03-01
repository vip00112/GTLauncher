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

        public static Version GetLatestVersion(string userName, string repoName)
        {
            string url = string.Format(API, userName, repoName);
            using (var wc = new WebClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                try
                {
                    wc.Headers.Set(HttpRequestHeader.Accept, "application/vnd.github.v3+json");
                    wc.Headers.Set(HttpRequestHeader.UserAgent, "request");
                    var response = wc.DownloadString(new Uri(url));
                    if (string.IsNullOrWhiteSpace(response)) return null;

                    var container = JsonConvert.DeserializeObject<GithubJsonContainer>(response);

                    Version version;
                    if (!Version.TryParse(container.Version, out version)) return null;

                    return version;
                }
                catch (Exception e)
                {
                    Logger.Error(e);
                    return null;
                }

            }
        }

        public static string GetDownloadUrlForLatestAsset(string userName, string repoName, string findPattern)
        {
            string url = string.Format(API, userName, repoName);
            using (var wc = new WebClient())
            {
                ServicePointManager.Expect100Continue = true;
                ServicePointManager.ServerCertificateValidationCallback = delegate { return true; };
                ServicePointManager.SecurityProtocol = SecurityProtocolType.Tls | SecurityProtocolType.Tls11 | SecurityProtocolType.Tls12 | SecurityProtocolType.Ssl3;

                try
                {
                    wc.Headers.Set(HttpRequestHeader.Accept, "application/vnd.github.v3+json");
                    wc.Headers.Set(HttpRequestHeader.UserAgent, "request");
                    var response = wc.DownloadString(new Uri(url));
                    if (string.IsNullOrWhiteSpace(response)) return null;

                    var container = JsonConvert.DeserializeObject<GithubJsonContainer>(response);
                    var asset = container.Assets.FirstOrDefault(o => o.FileName.IsMatchByPattern(findPattern));
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