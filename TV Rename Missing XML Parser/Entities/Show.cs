using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV_Rename_Missing_XML_Parser.Entities
{
    public partial class Show
    {
        public SortedList<string, Episode> Episodes { get; }

        public Show(string id, string imdbId, string title, string torrentSiteUrl)
        {
            this.Id = id;
            this.ImdbId = imdbId;
            this.Title = title;
            this.TorrentSiteUrl = torrentSiteUrl;
            this.Episodes = new SortedList<string, Episode>();
        }

        public Show() : this(null, null, null, null)
        {
            //only calls other constructor
        }

        public Show(string id, string imdbId, string title) : this(id, imdbId, title, null)
        {
            //only calls other constructor
        }

        /// <summary>
        /// Compares the two Show objects by title and IMDB ID
        /// </summary>
        /// <param name="obj">The object to compare (should be a Show)</param>
        /// <returns></returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Show)) return false;

            Show comp = obj as Show;

            return comp.Id == this.Id;
        }

        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        

        public Episode FindEpisodeBySeasonAndNumber(Show show, string seasonAndNumber)
        {
            foreach (KeyValuePair<string, Episode> episodeInfo in show.Episodes)
            {
                Episode episode = episodeInfo.Value;
                if (episode.SeasonAndNumber == seasonAndNumber) return episode;
            }

            return null;
        }
    }
}
