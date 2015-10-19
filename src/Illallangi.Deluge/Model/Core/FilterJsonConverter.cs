using System;
using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Core
{
    public sealed class FilterJsonConverter : JsonConverter {
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            //reader.Read();
            var obj = new Filter();
            obj.Name = reader.ReadAsString();
            obj.Count = reader.ReadAsInt32().GetValueOrDefault();

            reader.Read();

            return obj;
        }

        public override bool CanConvert(Type objectType)
        {
            throw new NotImplementedException();
        }
    }
}