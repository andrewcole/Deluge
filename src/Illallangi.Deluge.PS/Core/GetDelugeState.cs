using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Illallangi.Deluge.Model.Core;

namespace Illallangi.Deluge.PS.Core
{
    [Cmdlet(VerbsCommon.Get, @"DelugeState", DefaultParameterSetName = GetDelugeState.FilterParameterSet)]
    public sealed class GetDelugeState : DelugeCmdlet
    {
        private string currentName;

        private WildcardPattern currentNameWildcardPattern;

        public const string FilterParameterSet = @"Filter";

        [Parameter(Mandatory = false, ParameterSetName = GetDelugeState.FilterParameterSet)]
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
            this.WriteObject(this.GetStates(), true);
        }

        private IEnumerable<Filter> GetStates()
        {
            switch (this.ParameterSetName)
            {
                case GetDelugeState.FilterParameterSet:
                    return this.Deluge.Core.GetFilterTree().State.Where(this.IsMatch);
                default:
                    throw new PSNotImplementedException(this.ParameterSetName);
            }
        }

        private bool IsMatch(Filter state)
        {
            return this.NameWildcardPattern.IsMatch(state.Name);
        }
    }
}
