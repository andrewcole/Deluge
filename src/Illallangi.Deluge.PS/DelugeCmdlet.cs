using System.Management.Automation;
using Illallangi.Deluge.Client;

namespace Illallangi.Deluge.PS
{
    public abstract class DelugeCmdlet : PSCmdlet
    {
        private DelugeRpcClient currentDeluge;
        protected DelugeRpcClient Deluge
            => this.currentDeluge ?? (this.currentDeluge = new DelugeRpcClient());
    }
}