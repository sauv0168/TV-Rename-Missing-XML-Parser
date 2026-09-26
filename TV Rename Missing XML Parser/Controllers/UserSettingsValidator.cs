using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class UserSettingsValidator
    {
        private readonly GeneralSettings userSettings;
        private readonly GeneralSettings defaultSettings;
        private readonly LogController logController;

        public UserSettingsValidator(GeneralSettings userSettings, GeneralSettings defaultSettings, LogController logController)
        {
            if (userSettings == null)
                throw new ArgumentNullException(nameof(userSettings));

            if (defaultSettings == null)
                throw new ArgumentNullException(nameof(defaultSettings));

            if (logController == null)
                throw new ArgumentNullException(nameof(logController));

            this.userSettings = userSettings;
            this.defaultSettings = defaultSettings;
            this.logController = logController;
        }

        /// <summary>
        /// Validate both user settings and defaults. Log any issues found.
        /// </summary>
        public void Validate()
        {
            ValidateUserSettings();
            ValidateDefaultSettings();
        }

        /// <summary>
        /// Log any validation issues found in user settings.
        /// </summary>
        private void ValidateUserSettings()
        {
            if (!IsValidSearchDelay(this.userSettings.SearchDelay))
            {
                this.logController.Add($"Warning: User SearchDelay is invalid: {this.userSettings.SearchDelay}");
            }

            if (!IsValidDefaultMaxSearchAge(this.userSettings.DefaultMaxSearchAge))
            {
                this.logController.Add($"Warning: User DefaultMaxSearchAge is invalid: {this.userSettings.DefaultMaxSearchAge}");
            }

            if (!IsValidTorrentSites(this.userSettings.TorrentSites))
            {
                this.logController.Add("Warning: User TorrentSites contain invalid entries");
            }

            if (!IsValidDefaultSite(this.userSettings.DefaultSite, this.userSettings.TorrentSites))
            {
                this.logController.Add($"Warning: User DefaultSite does not match any torrent site: {this.userSettings.DefaultSite}");
            }
        }

        /// <summary>
        /// Log any validation issues found in default settings.
        /// </summary>
        private void ValidateDefaultSettings()
        {
            if (!IsValidSearchDelay(this.defaultSettings.SearchDelay))
            {
                this.logController.Add($"ERROR: Default SearchDelay is invalid: {this.defaultSettings.SearchDelay}");
            }

            if (!IsValidDefaultMaxSearchAge(this.defaultSettings.DefaultMaxSearchAge))
            {
                this.logController.Add($"ERROR: Default DefaultMaxSearchAge is invalid: {this.defaultSettings.DefaultMaxSearchAge}");
            }

            if (!IsValidTorrentSites(this.defaultSettings.TorrentSites))
            {
                this.logController.Add("ERROR: Default TorrentSites contain invalid entries");
            }

            if (!IsValidDefaultSite(this.defaultSettings.DefaultSite, this.defaultSettings.TorrentSites))
            {
                this.logController.Add($"ERROR: Default DefaultSite does not match any torrent site: {this.defaultSettings.DefaultSite}");
            }
        }

        /// <summary>
        /// SearchDelay must be greater than 0 (Timer.Interval requires > 0).
        /// </summary>
        public bool IsValidSearchDelay(int searchDelay)
        {
            return searchDelay > 0;
        }

        /// <summary>
        /// DefaultMaxSearchAge must be non-negative.
        /// </summary>
        public bool IsValidDefaultMaxSearchAge(int maxSearchAge)
        {
            return maxSearchAge >= 0;
        }

        /// <summary>
        /// TorrentSites must exist and each must have valid Name and Url.
        /// </summary>
        public bool IsValidTorrentSites(List<TorrentSiteSettings> torrentSites)
        {
            if (torrentSites == null || torrentSites.Count == 0)
                return false;

            return !torrentSites.Any(site => string.IsNullOrEmpty(site.Name) || string.IsNullOrEmpty(site.Url));
        }

        /// <summary>
        /// DefaultSite must be empty or match a site name in the list.
        /// </summary>
        public bool IsValidDefaultSite(string defaultSite, List<TorrentSiteSettings> torrentSites)
        {
            if (string.IsNullOrEmpty(defaultSite))
                return true;

            if (torrentSites == null || torrentSites.Count == 0)
                return false;

            return torrentSites.Any(site => site.Name == defaultSite);
        }

        /// <summary>
        /// Remove shows with no custom settings (stale entries that would never be displayed).
        /// </summary>
        public bool RemoveStaleShows()
        {
            if (this.userSettings.Shows == null || this.userSettings.Shows.Count == 0)
                return false;

            bool anyRemoved = false;

            foreach (ShowSettings show in this.userSettings.Shows.ToList())
            {
                if (show.Episodes != null)
                {
                    show.Episodes.RemoveAll(e => !e.Ignore);
                    if (show.Episodes.Count == 0)
                    {
                        show.Episodes = null;
                    }
                }

                if (string.IsNullOrEmpty(show.CleanTitle) &&
                    string.IsNullOrEmpty(show.TorrentSiteName) &&
                    (show.Episodes == null || show.Episodes.Count == 0))
                {
                    this.userSettings.Shows.Remove(show);
                    anyRemoved = true;
                }
            }

            if (anyRemoved)
            {
                this.logController.Add("Cleaned stale show setting(s) from JSON");
                return true;
            }

            return false;
        }
    }
}
