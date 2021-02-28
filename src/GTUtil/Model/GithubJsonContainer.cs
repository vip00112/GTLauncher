using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GTUtil
{
    public class GithubJsonContainer
    {
        [JsonProperty("message")]
        public string Message { get; set; }

        [JsonProperty("tag_name")]
        public string Version { get; set; }

        [JsonProperty("assets")]
        public List<GithubJsonAsset> Assets { get; set; }
    }

    public class GithubJsonAsset
    {
        [JsonProperty("id")]
        public int ID { get; set; }

        [JsonProperty("name")]
        public string FileName { get; set; }

        [JsonProperty("browser_download_url")]
        public string DownloadUrl { get; set; }
    }
}
