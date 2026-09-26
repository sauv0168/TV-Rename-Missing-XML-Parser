using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TV_Rename_Missing_XML_Parser.Entities
{
    public class GeneralSettings
    {
        /// <summary>
        /// Path to XML file.
        /// </summary>
        public string XmlFilePath { get; set; }

        /// <summary>
        /// Search delay in milliseconds.
        /// </summary>
        public int SearchDelay { get; set; }

        /// <summary>
        /// Path to browser executable.
        /// </summary>
        public string BrowserPath { get; set; }

        /// <summary>
        /// Last recorded activity time.
        /// </summary>
        public string LastActivityTime { get; set; }

        /// <summary>
        /// Default maximum search age in days.
        /// </summary>
        public int DefaultMaxSearchAge { get; set; }

        /// <summary>
        /// Default torrent site name.
        /// </summary>
        public string DefaultSite { get; set; }

        /// <summary>
        /// Whether to omit nulls in JSON output.
        /// </summary>
        public bool OmitNullsInJson { get; set; }

        /// <summary>
        /// List of torrent sites.
        /// </summary>
        public List<TorrentSiteSettings> TorrentSites { get; set; }

        /// <summary>
        /// List of custom show settings.
        /// </summary>
        public List<ShowSettings> Shows { get; set; }
    }

    public partial class TorrentSiteSettings
    {
        /// <summary>
        /// Site name.
        /// </summary>
        public string Name { get; set; }

        /// <summary>
        /// Base URL for the site.
        /// </summary>
        public string Url { get; set; }

        /// <summary>
        /// Path prefix before search terms.
        /// </summary>
        public string PathStart { get; set; }

        /// <summary>
        /// Character(s) to use instead of spaces in searches.
        /// </summary>
        public string SearchDelimiter { get; set; }

        /// <summary>
        /// Path suffix after search terms.
        /// </summary>
        public string PathEnd { get; set; }
    }

    public class ShowSettings
    {
        /// <summary>
        /// Unique show identifier.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// Show title from XML.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Clean display title (custom override).
        /// </summary>
        /// <save_trigger/>
        public string CleanTitle { get; set; }

        /// <summary>
        /// Custom torrent site name for this show.
        /// </summary>
        /// <save_trigger/>
        public string TorrentSiteName { get; set; }

        /// <summary>
        /// List of custom episode settings.
        /// </summary>
        /// <save_trigger/>
        public List<EpisodeSettings> Episodes { get; set; }
    }

    public class EpisodeSettings
    {
        /// <summary>
        /// Season and episode number as S##E##.
        /// </summary>
        public string SeasonAndNumber { get; set; }

        /// <summary>
        /// Whether to ignore this episode.
        /// </summary>
        /// <save_trigger/>
        public bool Ignore { get; set; }
    }
}
