using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Illallangi.Deluge.Model.Core;

namespace Illallangi.Deluge.PS.Core
{
    [Cmdlet(VerbsCommon.Get, @"DelugeTracker", DefaultParameterSetName = GetDelugeTrackerHost.FilterParameterSet)]
    public sealed class GetDelugeTrackerHost : DelugeCmdlet
    {
        private string currentName;

        private WildcardPattern currentNameWildcardPattern;

        public const string FilterParameterSet = @"Filter";

        [Parameter(Mandatory = false, ParameterSetName = GetDelugeTrackerHost.FilterParameterSet)]
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

        private WildcardPattern NameWildcardPattern => this.currentNameWildcardPattern
                                                              ?? (this.currentNameWildcardPattern =
                                                                  new WildcardPattern(this.Name ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));

        protected override void BeginProcessing()
        {
            this.WriteObject(this.GetTrackerHosts(), true);
        }

        private IEnumerable<Filter> GetTrackerHosts()
        {
            switch (this.ParameterSetName)
            {
                case GetDelugeTrackerHost.FilterParameterSet:
                    return this.Deluge.Core.GetFilterTree().Tracker.Where(this.IsMatch);
                default:
                    throw new PSNotImplementedException(this.ParameterSetName);
            }
        }

        private bool IsMatch(Filter tracker)
        {
            return this.NameWildcardPattern.IsMatch(tracker.Name);
        }
    }
}
