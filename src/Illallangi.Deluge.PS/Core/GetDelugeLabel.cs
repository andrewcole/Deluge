using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Illallangi.Deluge.Model.Core;
using Illallangi.Deluge.PS.Web;

namespace Illallangi.Deluge.PS.Core
{
    [Cmdlet(VerbsCommon.Get, @"DelugeLabel", DefaultParameterSetName = GetDelugeLabel.FilterParameterSet)]
    public sealed class GetDelugeLabel : DelugeCmdlet
    {
        private string currentName;

        private WildcardPattern currentNameWildcardPattern;

        public const string FilterParameterSet = @"Filter";

        [Parameter(Mandatory = false, ParameterSetName = GetDelugeLabel.FilterParameterSet)]
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
            this.WriteObject(this.GetLabels(), true);
        }

        private IEnumerable<Filter> GetLabels()
        {
            switch (this.ParameterSetName)
            {
                case GetDelugeLabel.FilterParameterSet:
                    return this.Deluge.Core.GetFilterTree().Label.Where(this.IsMatch);
                default:
                    throw new PSNotImplementedException(this.ParameterSetName);
            }
        }

        private bool IsMatch(Filter label)
        {
            return this.NameWildcardPattern.IsMatch(label.Name);
        }
    }
}