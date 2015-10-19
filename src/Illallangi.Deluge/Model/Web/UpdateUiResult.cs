using Illallangi.Deluge.Model.Core;
using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Web
{
    public class UpdateUiResult
    {
        [JsonProperty(@"stats")]
        public Stats Stats { get; set; }

        [JsonProperty(@"filters")]
        public FilterTree Filters { get; set; }

        [JsonProperty(@"torrents")]
        public TorrentCollection Torrents { get; set; }
    }
}