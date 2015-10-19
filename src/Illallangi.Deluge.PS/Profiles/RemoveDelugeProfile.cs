using System.Linq;
using System.Management.Automation;
using Illallangi.Deluge.Model.Profile;

namespace Illallangi.Deluge.PS.Profiles
{
    [Cmdlet(VerbsCommon.Remove, @"DelugeProfile", SupportsShouldProcess = true, ConfirmImpact = ConfirmImpact.High)]
    public class RemoveDelugeProfile : GetDelugeProfile
    {
        protected override void ProcessRecord()
        {
            foreach (var profile in DelugeProfile.GetProfiles().Where(this.IsMatch))
            {
                DelugeProfile.DeleteProfile(profile.Key);
            }
        }

        protected override bool IsMatch(DelugeProfile profile)
        {
            return base.IsMatch(profile) &&
                   this.ShouldProcess(profile.ToString(), VerbsCommon.Remove);
        }
    }
}