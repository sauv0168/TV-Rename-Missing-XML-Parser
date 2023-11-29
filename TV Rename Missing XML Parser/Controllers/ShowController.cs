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

            this.Reset();
        }

        public Episode AddEpisode(string showId, string showImdbCode, string showTitle, string seasonNumber, string episodeNumber, string episodeName, string publicationDate)
        {
            Show show = this.AddOrUpdateShowById(showId, showImdbCode, showTitle);

            Episode episode = new Episode();

            episode.Season = seasonNumber;
            episode.Number = episodeNumber;
            episode.Name = episodeName;
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
                this.updateAttributesIfNotNull(showId, showImdbCode, showTitle, null);
                return show;
            }

            this.Shows.Add(new Show(showId, showImdbCode, showTitle));
            return this.Shows.Last();
        }

        public TorrentSite GetTorrentSite(Show show)
        {
            TorrentSite torrentSite = this.userSettingsController.TorrentSites.
                FirstOrDefault(ts => ts.Url == show.TorrentSiteUrl);

            return torrentSite != null ? 
                torrentSite :
                this.userSettingsController.TorrentSites.First();
        }

        public void Reset()
        {
            this.userSettingsController.Reset();
            this.Shows = this.userSettingsController.Shows;
            this.tVRenameXMLController.ParseXMLFile(this.userSettingsController.XmlFilePath, this);
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

        public void updateAttributesIfNotNull(string id, string imdbCode, string title, string torrentSiteUrl)
        {
            Show show = this.Shows.Find(s => s.Id == id);

            if (id != null) show.Id = id;

            if (imdbCode != null) show.ImdbId = imdbCode;

            if (title != null) show.Title = title;

            if (torrentSiteUrl != null) show.TorrentSiteUrl = torrentSiteUrl;
        }
    }
}
