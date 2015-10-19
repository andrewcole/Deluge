using System.Linq;
using NUnit.Framework;

namespace Illallangi.Deluge.Test
{
    [TestFixture]
    public sealed class DelugeRpcClientSystemTests : DelugeRpcClientTestBase
    {
        [Test]
        public void ListMethods()
        {
            var methods = this.Client.System.ListMethods();
            Assert.IsNotNull(methods);
            Assert.Greater(methods.Count(), 0);
        }
    }
}