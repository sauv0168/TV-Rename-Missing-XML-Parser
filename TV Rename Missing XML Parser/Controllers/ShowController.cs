using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class ShowController
    {
        private const string ERROR_MESSAGE_DUPLICATE_EPISODE_NUMBER = "{0} {1} has identical season and episode numbers as another episode. Try \"Force Refresh\" that show.";

        private readonly TVRenameXMLController tVRenameXMLController;
        private readonly UserSettingsController userSettingsController;
        /// <summary>
        /// WARNING: Get errors after parsing the XML file
        /// </summary>
        public List<string> Errors { get; }

        public List<Show> Shows { get; private set; }
        public ShowController(UserSettingsController userSettingsController, TVRenameXMLController tVRenameXMLController)
        {
            this.userSettingsController = userSettingsController;
            this.tVRenameXMLController = tVRenameXMLController;

            this.Errors = new List<string>();
            this.Shows = new List<Show>();

            this.LoadShowsFromSettings();
        }

        private void LoadShowsFromSettings()
        {
            Dictionary<string, ShowSettings> settingsLookup = this.userSettingsController.ShowSettingsLookup;

            foreach (ShowSettings settings in settingsLookup.Values)
            {
                Show show = new Show(settings.Id, null, settings.Title);
                show.Settings = settings;
                this.Shows.Add(show);
            }
        }

        public Episode AddEpisode(string showId, string showImdbCode, string showTitle, string seasonNumber, string episodeNumber, string episodeName, string publicationDate)
        {
            Show show = this.AddOrUpdateShowById(showId, showImdbCode, showTitle);

            Episode episode = new Episode();

            episode.Season = seasonNumber;
            episode.Number = episodeNumber;
            episode.Title = episodeName;
            episode.PubDate = publicationDate;

            if (show.Episodes.ContainsKey(episode.SeasonAndNumber))
            {
                this.Errors.Add(
                    String.Format(
                        ERROR_MESSAGE_DUPLICATE_EPISODE_NUMBER,
                        show.Title, episode.SeasonAndNumberWithName
                        )
                    );
                return null;
            }

            show.Episodes.Add(episode.SeasonAndNumber, episode);
            episode.Show = show;

            return episode;
        }

        public Show AddOrUpdateShowById(string showId, string showImdbCode, string showTitle)
        {
            Show show = this.Shows.Find(s => s.Id == showId);

            if (show != null)
            {
                this.updateAttributesIfNotNull(showId, showTitle);
                if (showImdbCode != null) show.ImdbId = showImdbCode;
                return show;
            }

            Show newShow = new Show(showId, showImdbCode, showTitle);
            this.Shows.Add(newShow);
            return newShow;
        }

        public TorrentSiteSettings GetTorrentSite(Show show)
        {
            if (show.Settings != null && show.Settings.TorrentSiteName != null)
            {
                return this.userSettingsController.GetSiteByName(show.Settings.TorrentSiteName);
            }

            return this.userSettingsController.GetDefaultSite();
        }

        public void ClearAndReloadShows()
        {
            this.Shows.Clear();
            this.tVRenameXMLController.ParseXMLFile(this.userSettingsController.XmlFilePath, this);
            this.SortShowsByTitle();
            this.Errors.Clear();
        }

        //public Show findShowById(string id)
        //{
        //    return this.Shows.Single<Show>(show => show.Id == id);

        //    //foreach (Show s in this.Shows)
        //    //    if (s.Id == id)
        //    //        return s;

        //    //return null;
        //}

        //public Show GetShowByImdbId(string imdbId)
        //{
        //    //StringCollection showItems = this.SHOW_DATA;

        //    //foreach (string showItem in showItems)
        //    //{
        //    //    var item = new TVShowStoredString(showItem);
        //    //    if (item.IMDB_ID == IMDB_ID) return item;
        //    //}

        //    return null;
        //}

        //public Show GetShowByTitle(string title)
        //{
        //    //StringCollection showItems = this.SHOW_DATA;

        //    //foreach (string showItem in showItems)
        //    //{
        //    //    var item = new TVShowStoredString(showItem);
        //    //    if (item.ShowTitle == showTitle) return item;
        //    //}

        //    throw new NotImplementedException();

        //    return null;
        //}

        public void updateAttributesIfNotNull(string id, string title)
        {
            Show show = this.Shows.Find(s => s.Id == id);

            if (id != null) show.Id = id;

            if (title != null) show.Title = title;
        }

        public void SortShowsByTitle()
        {
            this.Shows.Sort((a, b) => a.Title.CompareTo(b.Title));
        }

        /// <summary>
        /// Get clean title for display; fallback to show title if not set.
        /// </summary>
        public string GetDisplayCleanTitle(Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            string cleanTitle = show.Settings?.CleanTitle;
            return string.IsNullOrEmpty(cleanTitle) ? show.Title : cleanTitle;
        }

        /// <summary>
        /// Set clean title and persist to settings.
        /// </summary>
        public void SetCleanTitle(Show show, string cleanTitle)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            if (show.Settings != null)
            {
                show.Settings.CleanTitle = cleanTitle;
            }
        }

        /// <summary>
        /// Get torrent site name for show; null if using default.
        /// </summary>
        public string GetTorrentSiteName(Show show)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            return show.Settings?.TorrentSiteName;
        }

        /// <summary>
        /// Set torrent site name and persist to settings.
        /// </summary>
        public void SetTorrentSiteName(Show show, string siteName)
        {
            if (show == null)
                throw new ArgumentNullException(nameof(show));

            if (show.Settings != null)
            {
                show.Settings.TorrentSiteName = siteName;
            }
        }

        /// <summary>
        /// Toggle the ignore flag for an episode.
        /// </summary>
        public void ToggleIgnoreEpisode(Episode episode)
        {
            if (episode == null)
                throw new ArgumentNullException(nameof(episode));

            Show show = episode.Show;
            if (show == null)
                return;

            if (show.Settings == null)
            {
                show.Settings = new ShowSettings { Id = show.Id, Title = show.Title };
                this.userSettingsController.Add(show.Settings);
            }

            EpisodeSettings episodeSetting = show.Settings.Episodes?.FirstOrDefault(e => e.SeasonAndNumber == episode.SeasonAndNumber);

            if (episodeSetting != null)
            {
                episodeSetting.Ignore = !episodeSetting.Ignore;

                if (!episodeSetting.Ignore)
                {
                    show.Settings.Episodes.Remove(episodeSetting);

                    if (show.Settings.Episodes.Count == 0 &&
                        string.IsNullOrEmpty(show.Settings.CleanTitle) &&
                        string.IsNullOrEmpty(show.Settings.TorrentSiteName))
                    {
                        this.userSettingsController.Remove(show.Settings);
                    }
                }
            }
            else
            {
                if (show.Settings.Episodes == null)
                {
                    show.Settings.Episodes = new List<EpisodeSettings>();
                }
                show.Settings.Episodes.Add(new EpisodeSettings
                {
                    SeasonAndNumber = episode.SeasonAndNumber,
                    Ignore = true
                });
            }
        }

    }
}
