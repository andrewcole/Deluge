using System;
using System.Collections.Generic;
using System.Linq;
using Illallangi.Deluge.Model.Core;
using Newtonsoft.Json;

namespace Illallangi.Deluge.Client
{
    public sealed class CoreRpcClient : ClientBase
    {
        public CoreRpcClient(JsonRpcClient jsonRpcClient, DelugeRpcClient delugeRpcClient)
            : base(jsonRpcClient, delugeRpcClient)
        {
        }

        public FilterTree GetFilterTree(bool showZeroHits = true, string[] hideCat = null)
        {
            return this.JsonRpcClient.InvokeMethod<FilterTree>(@"core.get_filter_tree", this.DelugeRpcClient.GetSessionKey());
        }
    }
}