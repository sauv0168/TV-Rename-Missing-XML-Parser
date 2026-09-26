using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using System.Runtime.InteropServices;
using System.Text;
using System.Threading.Tasks;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class WebController
    {
        private readonly UserSettingsController userSettingsController;
        public string BrowserWarning { get; private set; }

        public WebController(UserSettingsController userSettingsController)
        {
            this.userSettingsController = userSettingsController;
        }

        /// <summary>
        /// Get search string for show-only web queries.
        /// </summary>
        public string GetSearchString(Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            string cleanTitle = show.Settings?.CleanTitle;
            return string.IsNullOrEmpty(cleanTitle) ? show.Title : cleanTitle;
        }

        /// <summary>
        /// Get search string for episode web queries (format: cleanName s00e00).
        /// </summary>
        public string GetSearchString(Episode episode)
        {
            if (episode == null)
                throw new ArgumentNullException(nameof(episode));

            Show show = episode.Show;
            string cleanName = this.GetSearchString(show);

            return $"{cleanName} {episode.SeasonAndNumber}";
        }

        /// <summary>
        /// Build search URL for a show on a torrent site.
        /// </summary>
        public string GetSearchUrl(TorrentSiteSettings site, Show show)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site));
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            string searchTerms = this.GetSearchString(show);
            return new StringBuilder()
                .Append(site.Url)
                .Append(site.PathStart)
                .Append(searchTerms.Replace(" ", site.SearchDelimiter))
                .Append(site.PathEnd)
                .ToString();
        }

        /// <summary>
        /// Build search URL for an episode on a torrent site.
        /// </summary>
        public string GetSearchUrl(TorrentSiteSettings site, Episode episode)
        {
            if (site == null)
                throw new ArgumentNullException(nameof(site));
            if (episode == null)
                throw new ArgumentNullException(nameof(episode));

            string searchTerms = this.GetSearchString(episode);
            return new StringBuilder()
                .Append(site.Url)
                .Append(site.PathStart)
                .Append(searchTerms.Replace(" ", site.SearchDelimiter))
                .Append(site.PathEnd)
                .ToString();
        }

        public void OpenUrl(string url)
        {
            if (!string.IsNullOrEmpty(this.userSettingsController.BrowserPath))
            {
                if (File.Exists(this.userSettingsController.BrowserPath))
                {
                    Process.Start(this.userSettingsController.BrowserPath, url);
                    return;
                }
                else
                {
                    this.BrowserWarning = $"Browser path not found: {this.userSettingsController.BrowserPath}";
                }
            }

            try
            {
                Process.Start(url);
            }
            catch
            {
                // hack because of this: https://github.com/dotnet/corefx/issues/10361
                if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
                {
                    url = url.Replace("&", "^&");
                    Process.Start(new ProcessStartInfo("cmd", $"/c start {url}") { CreateNoWindow = true });
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
                {
                    Process.Start("xdg-open", url);
                }
                else if (RuntimeInformation.IsOSPlatform(OSPlatform.OSX))
                {
                    Process.Start("open", url);
                }
                else
                {
                    //TODO: BE BETTER!
                    throw;
                }
            }
        }
    }
}
