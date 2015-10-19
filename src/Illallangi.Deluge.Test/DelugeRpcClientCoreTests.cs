using NUnit.Framework;

namespace Illallangi.Deluge.Test
{
    [TestFixture]
    public sealed class DelugeRpcClientCoreTests : DelugeRpcClientTestBase
    {
        [Test]
        public void CoreGetFilterTree()
        {
            var tree = this.Client.Core.GetFilterTree();
            Assert.IsNotNull(tree);
            Assert.IsNotNull(tree.Tracker);
            Assert.Greater(tree.Tracker.Count, 0);
            Assert.IsNotNull(tree.Label);
            Assert.Greater(tree.Label.Count, 0);
            Assert.IsNotNull(tree.State);
            Assert.Greater(tree.State.Count, 0);
        }
    }
}