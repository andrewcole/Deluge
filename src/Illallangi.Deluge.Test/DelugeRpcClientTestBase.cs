using Illallangi.Deluge.Client;
using NUnit.Framework;

namespace Illallangi.Deluge.Test
{
    public abstract class DelugeRpcClientTestBase
    {
        protected DelugeRpcClient Client { get; private set; }

        [SetUp]
        public void Setup()
        {
            this.Client = new DelugeRpcClient();
        }

        [TearDown]
        public void TearDown()
        {
            this.Client = null;
        }
    }
}
