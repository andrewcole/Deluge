using System;
using System.Net;

namespace Illallangi.Deluge.Client
{
    public abstract class ClientBase
    {
        protected ClientBase(string uri)
            : this(new JsonRpcClient(uri), null)
        {
        }

        protected ClientBase(Uri uri)
            : this(new JsonRpcClient(uri), null)
        {
        }

        protected ClientBase(JsonRpcClient jsonRpcClient, DelugeRpcClient delugeRpcClient)
        {
            this.DelugeRpcClient = delugeRpcClient;
            this.JsonRpcClient = jsonRpcClient;
        }

        protected DelugeRpcClient DelugeRpcClient { get; }
        protected JsonRpcClient JsonRpcClient { get; }
    }
}