using System.Collections.Generic;
using Illallangi.Deluge.Model.Web;

namespace Illallangi.Deluge.Client
{
    public sealed class WebRpcClient : ClientBase
    {
        public WebRpcClient(JsonRpcClient jsonRpcClient, DelugeRpcClient delugeRpcClient) : base(jsonRpcClient, delugeRpcClient)
        {
        }

        public UpdateUiResult UpdateUi(string trackerHost = null, string label = null, string[] properties = null)
        {
            var parameters = new Dictionary<string, string>();

            if (!string.IsNullOrWhiteSpace(trackerHost))
            {
                parameters.Add(@"tracker_host", trackerHost);
            }
            
            if (!string.IsNullOrWhiteSpace(label))
            {
                parameters.Add(@"label", label);
            }

            return this.JsonRpcClient.InvokeMethod<UpdateUiResult>(
                @"web.update_ui",
                this.DelugeRpcClient.GetSessionKey(),
                properties ?? new[]
                {
                    "queue", "name", "total_wanted", "state", "progress", "num_seeds", "total_seeds", "num_peers",
                    "total_peers", "download_payload_rate", "upload_payload_rate", "eta", "ratio", "distributed_copies",
                    "is_auto_managed", "time_added", "tracker_host", "save_path", "total_done", "total_uploaded",
                    "max_download_speed", "max_upload_speed", "seeds_peers_ratio", "label"
                },
                parameters);
        }
    }
}