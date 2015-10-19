using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Core
{
    [JsonConverter(typeof(FilterJsonConverter))]
    public sealed class Filter
    {
        public string Name { get; set; }
        public int Count { get; set; }
    }
}