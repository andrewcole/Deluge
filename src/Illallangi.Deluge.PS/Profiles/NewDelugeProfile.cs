using System.Management.Automation;
using Illallangi.Deluge.Model.Profile;

namespace Illallangi.Deluge.PS.Profiles
{
    [Cmdlet(VerbsCommon.New, @"DelugeProfile")]
    public sealed class NewDelugeProfile : PSCmdlet
    {
        [Parameter(Mandatory = true)]
        public string Name { get; set; }

        [Parameter(Mandatory = true)]
        public string Uri { get; set; }

        [Parameter(Mandatory = true)]
        public string Password { get; set; }

        [Parameter]
        public SwitchParameter Active { get; set; }

        protected override void ProcessRecord()
        {
            this.WriteObject(new DelugeProfile { Name = this.Name, Uri = this.Uri, Password = this.Password, Active = this.Active.IsPresent });
        }
    }
}