using System;
using System.Net;
using Illallangi.Deluge.Model.Profile;

namespace Illallangi.Deluge.Client
{
    public sealed class AuthRpcClient : ClientBase
    {
        public bool CheckSession(string session)
        {
            return bool.Parse(this.JsonRpcClient.InvokeMethod<string>(@"auth.check_session", session));
        }

        public bool Login(string password = null)
        {
            string session;
            DateTime expiry;
            return this.Login(out session, out expiry, password);
        }

        public bool Login(out string session, out DateTime expiry, string password = null)
        {
            var cookieContainer = new CookieContainer();
            var result = bool.Parse(this.JsonRpcClient.InvokeMethod<string>(@"auth.login", cookieContainer, password ?? DelugeProfile.GetActiveProfile().Password));
            var cookie = cookieContainer.GetCookies(this.JsonRpcClient.Uri)[@"_session_id"];
            session = cookie?.Value;
            expiry = cookie?.Expires ?? DateTime.MinValue;
            return result;
        }

        public AuthRpcClient(JsonRpcClient jsonRpcClient, DelugeRpcClient delugeRpcClient) : base(jsonRpcClient, delugeRpcClient)
        {
        }
    }
}