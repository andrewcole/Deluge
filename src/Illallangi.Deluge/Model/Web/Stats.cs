using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Web
{
    public class Stats
    {
        [JsonProperty("upload_protocol_rate")]
        public int UploadProtocolRate { get; set; }
        [JsonProperty("max_upload")]
        public double MaxUpload { get; set; }
        [JsonProperty("download_protocol_rate")]
        public int DownloadProtocolRate { get; set; }
        [JsonProperty("download_rate")]
        public int DownloadRate { get; set; }
        [JsonProperty("has_incoming_connections")]
        public bool HasIncomingConnections { get; set; }
        [JsonProperty("num_connections")]
        public int NumConnections { get; set; }
        [JsonProperty("max_download")]
        public double MaxDownload { get; set; }
        [JsonProperty("upload_rate")]
        public int upload_rate { get; set; }
        [JsonProperty("dht_nodes")]
        public int DhtNodes { get; set; }
        [JsonProperty("free_space")]
        public long FreeSpace { get; set; }
        [JsonProperty("max_num_connections")]
        public int MaxNumConnections { get; set; }
    }
}