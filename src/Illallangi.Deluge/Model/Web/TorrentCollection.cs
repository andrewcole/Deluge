using System.Collections.Generic;
using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Web
{
    [JsonConverter(typeof(TorrentCollectionJsonConverter))]
    public sealed class TorrentCollection : List<Torrent>
    {
        public TorrentCollection(IEnumerable<Torrent> torrents)
            : base(torrents)
        {
        }
    }
}