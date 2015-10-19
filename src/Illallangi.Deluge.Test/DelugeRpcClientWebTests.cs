using NUnit.Framework;

namespace Illallangi.Deluge.Test
{
    [TestFixture]
    public sealed class DelugeRpcClientWebTests : DelugeRpcClientTestBase
    {
        [Test]
        public void UpdateUi()
        {
            var result = this.Client.Web.UpdateUi(trackerHost: @"what.cd");
            Assert.NotNull(result);
        }
    }
}