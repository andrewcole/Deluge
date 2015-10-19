using System;
using System.Net;
using Illallangi.Deluge.Model.Profile;

namespace Illallangi.Deluge.Client
{
    public sealed class DelugeRpcClient : ClientBase
    {
        private AuthRpcClient currentAuth;
        private SystemRpcClient currentSystem;
        private CoreRpcClient currentCore;
        private WebRpcClient currentWeb;
        
        public DelugeRpcClient()
            : base(DelugeProfile.GetActiveProfile().Uri)
        {
        }
        
        public AuthRpcClient Auth => this.currentAuth ??
                                     (this.currentAuth = new AuthRpcClient(this.JsonRpcClient, this));

        public SystemRpcClient System => this.currentSystem ??
                                         (this.currentSystem = new SystemRpcClient(this.JsonRpcClient, this));

        public CoreRpcClient Core => this.currentCore ??
                                     (this.currentCore = new CoreRpcClient(this.JsonRpcClient, this));

        public WebRpcClient Web => this.currentWeb ??
                                   (this.currentWeb = new WebRpcClient(this.JsonRpcClient, this));
        public string GetSessionKey()
        {
            var profile = DelugeProfile.GetActiveProfile();

            if (profile.SessionExpiry > DateTime.Now)
            {
                return profile.SessionKey;
            }

            string session;
            DateTime expiry;
            
            if (!this.Auth.Login(out session, out expiry))
            {
                throw new Exception("Logon failed");
            }

            try
            {
                if (!this.Auth.CheckSession(session))
                {
                    throw new Exception(@"Session not OK after logon.");
                }
            }
            catch (NotImplementedException)
            {
                //noop
            }

            profile.SessionKey = session;
            profile.SessionExpiry = expiry;

            return session;
        }
    }
}