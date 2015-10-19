using System;
using System.Collections.Generic;
using System.Linq;
using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Web
{
    public class TorrentCollectionJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            var result = serializer.Deserialize<Dictionary<string, Torrent>>(reader);
            return new TorrentCollection(result.Select(x =>
            {
                x.Value.Hash = x.Key;
                return x.Value;
            }));
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}