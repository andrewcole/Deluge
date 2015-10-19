using System.Collections.Generic;
using Newtonsoft.Json;

namespace Illallangi.Deluge.Model.Core
{
    [JsonConverter(typeof(FilterCollectionJsonConverter))]
    public sealed class FilterCollection : List<Filter>
    {
        public FilterCollection(IEnumerable<Filter> filters)
            : base(filters)
        {
        }
    }
}