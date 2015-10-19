using System;
using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Core
{
    public sealed class FilterCollectionJsonConverter : JsonConverter
    {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            // reader.Read();
            var result = new FilterCollection(serializer.Deserialize<Filter[]>(reader));
            //reader.Read();
            return result;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}