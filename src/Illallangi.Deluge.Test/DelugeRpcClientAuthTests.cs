using System;
using System.Diagnostics;
using NUnit.Framework;

namespace Illallangi.Deluge.Test
{
    [TestFixture]
    public sealed class DelugeRpcClientAuthTests : DelugeRpcClientTestBase
    {
        [Test]
        public void LoginReturnsTrue()
        {
            Assert.IsTrue(this.Client.Auth.Login());
        }

        [Test]
        public void LoginReturnsSessionKeyAndExpiry()
        {
            string session;
            DateTime expiry;
            var result = this.Client.Auth.Login(out session, out expiry);
            Assert.IsTrue(result, @"Logon attempt failed.");
            Assert.IsNotNullOrEmpty(session);
            Assert.IsNotNull(expiry);
            Assert.Greater(expiry, DateTime.Now, @"Session Key expires in the past.");
            Debug.WriteLine($"Session cookie returned {session} with an expiry of {expiry}");
        }

        [Test]
        public void BadLoginReturnsFalse()
        {
            Assert.IsFalse(this.Client.Auth.Login(password: Guid.NewGuid().ToString()));
        }

        [Test]
        public void BadLoginReturnsNoSessionKeyNorExpiry()
        {
            string session;
            DateTime expiry;
            var result = this.Client.Auth.Login(out session, out expiry, password: Guid.NewGuid().ToString());
            Assert.IsFalse(result, @"Logon attempt succeded.");
            Assert.IsNull(session);
            Assert.IsNotNull(expiry);
            Assert.AreEqual(expiry, DateTime.MinValue);
        }

        [Test]
        public void AuthCheckSessionReturnsTrueAfterLogin()
        {
            Assert.IsTrue(this.Client.Auth.CheckSession(this.Client.GetSessionKey()));
        }

        [Test]
        public void AuthCheckSessionReturnsFalse()
        {
            Assert.IsFalse(this.Client.Auth.CheckSession(Guid.NewGuid().ToString()));
        }

        [Test]
        public void LogonReturnsSessionKey()
        {
            Assert.NotNull(this.Client.GetSessionKey());
        }
    }
}