using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class SearchController
    {
        private bool isShowingIgnoredEpisodes;
        private bool isShowingSpecialEpisodes;
        private Timer searchDelayTimer;
        private int lastKnownMaxAge = -1;

        /// <summary>
        /// Initializes a new instance of the SearchController class with specified settings.
        /// </summary>
        public SearchController(bool isShowingIgnoredEpisodes, bool isShowingSpecialEpisodes)
        {
            this.isShowingIgnoredEpisodes = isShowingIgnoredEpisodes;
            this.isShowingSpecialEpisodes = isShowingSpecialEpisodes;
            this.searchDelayTimer = null;
        }

        /// <summary>
        /// Initializes a new instance of the SearchController class with default settings.
        /// </summary>
        public SearchController() : this(false, true)
        {
            // Empty on purpose
        }

        public bool IsShowingIgnoredEpisodes
        {
            get { return this.isShowingIgnoredEpisodes; }
        }

        public bool IsShowingSpecialEpisodes
        {
            get { return this.isShowingSpecialEpisodes; }
        }

        /// <summary>
        /// Toggle display of ignored episodes.
        /// </summary>
        public void ToggleShowIgnored()
        {
            this.isShowingIgnoredEpisodes = !this.isShowingIgnoredEpisodes;
        }

        /// <summary>
        /// Toggle display of special episodes (S00).
        /// </summary>
        public void ToggleShowSpecials()
        {
            this.isShowingSpecialEpisodes = !this.isShowingSpecialEpisodes;
        }

        /// <summary>
        /// Check if episode should be displayed based on filters.
        /// </summary>
        public bool ShouldShowEpisode(Episode episode)
        {
            if (episode == null)
                return false;

            if (!this.isShowingIgnoredEpisodes)
            {
                var ignored = episode.Show.Settings?.Episodes?.FirstOrDefault(e => e.SeasonAndNumber == episode.SeasonAndNumber);
                if (ignored?.Ignore == true)
                    return false;
            }

            if (!this.isShowingSpecialEpisodes && episode.IsSpecial())
            {
                return false;
            }

            return true;
        }

        /// <summary>
        /// Get display/filter text for a show (clean title or title).
        /// </summary>
        public string GetFilterText(Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            string cleanTitle = show.Settings?.CleanTitle;
            return string.IsNullOrEmpty(cleanTitle) ? show.Title : cleanTitle;
        }

        /// <summary>
        /// Start search delay timer with callback.
        /// </summary>
        public void StartDelayTimer(int delayMs, Action callback)
        {
            if (callback == null)
                throw new ArgumentNullException(nameof(callback));
            if (delayMs <= 0)
                throw new ArgumentException("delayMs must be greater than 0", nameof(delayMs));

            if (this.searchDelayTimer == null)
            {
                this.searchDelayTimer = new Timer();
                this.searchDelayTimer.Tick += (s, args) => {
                    callback();
                    this.searchDelayTimer.Stop();
                };
            }
            this.searchDelayTimer.Interval = delayMs;
            this.searchDelayTimer.Stop();
            this.searchDelayTimer.Start();
        }

        /// <summary>
        /// Stop search delay timer.
        /// </summary>
        public void StopDelayTimer()
        {
            if (this.searchDelayTimer != null)
            {
                this.searchDelayTimer.Stop();
            }
        }

        /// <summary>
        /// Check if max age has changed and update the tracked value.
        /// </summary>
        public bool HasMaxAgeChanged(int currentMaxAge)
        {
            if (this.lastKnownMaxAge != currentMaxAge)
            {
                this.lastKnownMaxAge = currentMaxAge;
                return true;
            }
            return false;
        }
    }
}
