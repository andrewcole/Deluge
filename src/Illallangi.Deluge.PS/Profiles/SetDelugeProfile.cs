using System.Linq;
using System.Management.Automation;
using Illallangi.Deluge.Model.Profile;

namespace Illallangi.Deluge.PS.Profiles
{
    [Cmdlet(VerbsCommon.Set, @"DelugeProfile")]
    public sealed class SetDelugeProfile : PSCmdlet
    {
        [Parameter(Mandatory = true, ValueFromPipelineByPropertyName = true)]
        public string Key { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Name { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Uri { get; set; }

        [Parameter(Mandatory = false, ValueFromPipelineByPropertyName = true)]
        public string Password { get; set; }

        [Parameter(Mandatory = false)]
        public SwitchParameter Active { get; set; }

        protected override void ProcessRecord()
        {
            foreach (var profile in DelugeProfile.GetProfiles().Where(p => p.Key.Equals(this.Key)))
            {
                profile.Name = this.Name ?? profile.Name;
                profile.Uri = this.Uri ?? profile.Uri;
                profile.Password = this.Password ?? profile.Password;
                if (this.Active.IsPresent)
                {
                    profile.Active = true;
                }
            }

            this.WriteObject(DelugeProfile.GetProfiles().Where(p => p.Key.Equals(this.Key)));
        }
    }
}