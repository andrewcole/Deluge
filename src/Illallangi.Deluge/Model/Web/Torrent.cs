using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Web
{
    public class Torrent
    {
        [JsonIgnore]
        public string Hash { get; set; }

        [JsonProperty("name")]
        public string Name { get; set; }

        [JsonProperty("state")]
        public string State { get; set; }

        [JsonProperty("max_download_speed")]
        public int MaxDownloadSpeed { get; set; }
        [JsonProperty("upload_payload_rate")]
        public int UploadPayloadRate { get; set; }
        [JsonProperty("download_payload_rate")]
        public int DownloadPayloadRate { get; set; }
        [JsonProperty("num_peers")]
        public int NumPeers { get; set; }
        [JsonProperty("ratio")]
        public double Ratio { get; set; }
        [JsonProperty("total_peers")]
        public int TotalPeers { get; set; }
        [JsonProperty("label")]
        public string Label { get; set; }
        [JsonProperty("max_upload_speed")]
        public int MaxUploadSpeed { get; set; }
        [JsonProperty("eta")]
        public int Eta { get; set; }
        [JsonProperty("save_path")]
        public string SavePath { get; set; }
        [JsonProperty("progress")]
        public double Progress { get; set; }
        [JsonProperty("time_added")]
        public double TimeAdded { get; set; }
        [JsonProperty("tracker_host")]
        public string TrackerHost { get; set; }
        [JsonProperty("total_uploaded")]
        public long TotalUploaded { get; set; }
        [JsonProperty("total_done")]
        public long TotalDone { get; set; }
        [JsonProperty("total_wanted")]
        public long TotalWanted { get; set; }
        [JsonProperty("total_seeds")]
        public int TotalSeeds { get; set; }
        [JsonProperty("seeds_peers_ratio")]
        public double SeedsPeersRatio { get; set; }
        [JsonProperty("num_seeds")]
        public int NumSeeds { get; set; }
        [JsonProperty("is_auto_managed")]
        public bool IsAutoManaged { get; set; }
        [JsonProperty("queue")]
        public int Queue { get; set; }
        [JsonProperty("distributed_copies")]
        public double DistributedCopies { get; set; }
    }
}