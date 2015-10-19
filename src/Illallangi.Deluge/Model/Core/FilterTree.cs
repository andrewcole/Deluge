using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Core
{
    public sealed class FilterTree
    {
        [JsonProperty(@"label")]
        public FilterCollection Label { get; set; }

        [JsonProperty(@"state")]
        public FilterCollection State { get; set; }

        [JsonProperty(@"tracker_host")]
        public FilterCollection Tracker { get; set; }
    }
}