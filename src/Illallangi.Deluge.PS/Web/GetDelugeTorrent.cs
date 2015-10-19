using System.Collections.Generic;
using System.Linq;
using System.Management.Automation;
using Illallangi.Deluge.Model.Web;

namespace Illallangi.Deluge.PS.Web
{
    [Cmdlet(VerbsCommon.Get, @"DelugeTorrent", DefaultParameterSetName = GetDelugeTorrent.FilterParameterSet)]
    public sealed class GetDelugeTorrent : DelugeCmdlet
    {
        private string currentLabel;

        private WildcardPattern currentLabelWildcardPattern;

        private string currentName;

        private WildcardPattern currentNameWildcardPattern;

        private string currentState;

        private WildcardPattern currentStateWildcardPattern;

        private string currentTrackerHost;

        private WildcardPattern currentTrackerHostWildcardPattern;

        public const string FilterParameterSet = @"Filter";

        [Parameter(Mandatory = false, ParameterSetName = GetDelugeTorrent.FilterParameterSet)]
        public string Label
        {
            get
            {
                return this.currentLabel;
            }
            set
            {
                this.currentLabel = value;
                this.currentLabelWildcardPattern = null;
            }
        }

        [Parameter(Mandatory = false, ParameterSetName = GetDelugeTorrent.FilterParameterSet)]
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

        [Parameter(Mandatory = false, ParameterSetName = GetDelugeTorrent.FilterParameterSet)]
        public string State
        {
            get
            {
                return this.currentState;
            }
            set
            {
                this.currentState = value;
                this.currentStateWildcardPattern = null;
            }
        }

        [Parameter(Mandatory = false, ParameterSetName = GetDelugeTorrent.FilterParameterSet)]
        public string TrackerHost
        {
            get
            {
                return this.currentTrackerHost;
            }
            set
            {
                this.currentTrackerHost = value;
                this.currentTrackerHostWildcardPattern = null;
            }
        }

        private WildcardPattern LabelWildcardPattern => this.currentLabelWildcardPattern
                                                        ?? (this.currentLabelWildcardPattern =
                                                            new WildcardPattern(this.Label ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));

        private WildcardPattern NameWildcardPattern => this.currentNameWildcardPattern
                                                        ?? (this.currentNameWildcardPattern =
                                                            new WildcardPattern(this.Name ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));

        private WildcardPattern StateWildcardPattern => this.currentStateWildcardPattern
                                                        ?? (this.currentStateWildcardPattern =
                                                            new WildcardPattern(this.State ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));

        private WildcardPattern TrackerHostWildcardPattern => this.currentTrackerHostWildcardPattern
                                                              ?? (this.currentTrackerHostWildcardPattern =
                                                                  new WildcardPattern(this.TrackerHost ?? @"*", WildcardOptions.Compiled | WildcardOptions.IgnoreCase));

        private IEnumerable<Torrent> GetTorrents()
        {
            switch (this.ParameterSetName)
            {
                case GetDelugeTorrent.FilterParameterSet:
                    return this.Deluge.Web.UpdateUi().Torrents.Where(this.IsMatch);
                default:
                    throw new PSNotImplementedException(this.ParameterSetName);
            }
        }

        private bool IsMatch(Torrent torrent)
        {
            return this.LabelWildcardPattern.IsMatch(torrent.Label) &&
                   this.NameWildcardPattern.IsMatch(torrent.Name) &&
                   this.StateWildcardPattern.IsMatch(torrent.State) &&
                   this.TrackerHostWildcardPattern.IsMatch(torrent.TrackerHost);
        }

        protected override void ProcessRecord()
        {
            this.WriteObject(this.GetTorrents(), true);
        }
    }
}