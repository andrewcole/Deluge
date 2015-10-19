using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using System.Text;
using System.Threading.Tasks;
using Illallangi.Deluge.Model.Core;
using Illallangi.Deluge.Model.Profile;
using Illallangi.Deluge.PS.Core;

namespace Illallangi.Deluge.PS.Profiles
{
    [Cmdlet(VerbsCommon.Get, @"DelugeProfile")]
    public class GetDelugeProfile : PSCmdlet
    {
        private string currentUri;
        private string currentName;
        private WildcardPattern currentUriWildcardPattern;
        private WildcardPattern currentNameWildcardPattern;

        [Parameter()]
        public string Name
        {
            get
            {
                return this.currentName;
            }
            set
            {
                this.currentName = value;
                this.currentNameWildcardPattern = null;
            }
        }

        [Parameter()]
        public string Uri
        {
            get
            {
                return this.currentUri;
            }
            set
            {
                this.currentUri = value;
                this.currentUriWildcardPattern = null;
            }
        }

        [Parameter]
        public SwitchParameter Active { get; set; }

        private WildcardPattern NameWildcardPattern => this.currentNameWildcardPattern
                                                              ?? (this.currentNameWildcardPattern =
                                                                  new WildcardPattern(this.Name ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));

        private WildcardPattern UriWildcardPattern => this.currentUriWildcardPattern
                                                              ?? (this.currentUriWildcardPattern =
                                                                  new WildcardPattern(this.Uri ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));
        
        protected override void ProcessRecord()
        {
            this.WriteObject(this.GetDelugeProfiles(), true);
        }

        private IEnumerable<DelugeProfile> GetDelugeProfiles()
        {
            return DelugeProfile.GetProfiles().Where(this.IsMatch);
        }

        protected virtual bool IsMatch(DelugeProfile delugeProfile)
        {
            return this.NameWildcardPattern.IsMatch(delugeProfile.Name) &&
                   this.UriWildcardPattern.IsMatch(delugeProfile.Uri) &&
                   (!this.Active.IsPresent || delugeProfile.Active);
        }
    }
}
