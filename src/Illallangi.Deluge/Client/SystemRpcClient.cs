using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Remoting.Channels;

namespace Illallangi.Deluge.Client
{
    public sealed class SystemRpcClient : ClientBase
    { 
        public IEnumerable<string> ListMethods()
        {
            return this.JsonRpcClient.InvokeMethod<List<string>>(@"system.listMethods", this.DelugeRpcClient.GetSessionKey());
        }
        
        public SystemRpcClient(JsonRpcClient jsonRpcClient, DelugeRpcClient delugeRpcClient) : base(jsonRpcClient, delugeRpcClient)
        {
        }
    }
}