using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Runtime.CompilerServices;
using Microsoft.Win32;

namespace Illallangi.Deluge.Model.Profile
{
    public sealed class DelugeProfile
    {
        public static IEnumerable<DelugeProfile> GetProfiles()
        {
            var subKeyNames = Registry.CurrentUser.CreateSubKey($@"Software\Illallangi Enterprises\Deluge Client\Profiles")
                .GetSubKeyNames();
            return subKeyNames
                .Select(key => new DelugeProfile { Key = key });
        }

        public static DelugeProfile GetActiveProfile()
        {
            return DelugeProfile.GetProfiles().Single(p => p.Active);
        }

        public static void DeleteProfile(string key)
        {
            Registry.CurrentUser.DeleteSubKey($@"Software\Illallangi Enterprises\Deluge Client\Profiles\{key}");
        }

        private string currentIndex;
        
        public string Key
        {
            get { return this.currentIndex ?? (this.currentIndex = Guid.NewGuid().ToString()); }
            set { this.currentIndex = value; }
        }

        public string Name
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    string.Empty,
                    string.Empty);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    string.Empty,
                    value);
            }
        }

        public string Uri
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"Uri",
                    string.Empty);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"Uri",
                    value);
            }
        }


        public string Password
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"Password",
                    string.Empty);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"Password",
                    value);
            }
        }

        public bool Active
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"Active",
                    @"False").Equals(@"True");
            }

            set
            {
                if (value)
                {
                    foreach (var profile in DelugeProfile.GetProfiles())
                    {
                        profile.Active = false;
                    }
                }

                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"Active",
                    value.ToString());

            }
        }
        public string SessionKey
        {
            get
            {
                return Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"SessionKey",
                    string.Empty);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"SessionKey",
                    value);
            }
        }

        public DateTime SessionExpiry
        {
            get
            {
                return DateTime.ParseExact(Registry.CurrentUser.GetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"SessionExpiry",
                    DateTime.MinValue.ToString(@"O", CultureInfo.InvariantCulture)), @"O", CultureInfo.InvariantCulture, DateTimeStyles.RoundtripKind);
            }

            set
            {
                Registry.CurrentUser.SetValue(
                    $@"Software\Illallangi Enterprises\Deluge Client\Profiles\{this.Key}",
                    @"SessionExpiry",
                    value.ToString(@"O", CultureInfo.InvariantCulture));
            }
        }

        public override string ToString()
        {
            return $"Deluge Profile {this.Name} ({this.Uri})";
        }
    }
}