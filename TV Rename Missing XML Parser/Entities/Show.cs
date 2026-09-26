using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV_Rename_Missing_XML_Parser.Entities
{
    public partial class Show
    {
        ////////////////
        // ATTRIBUTES //
        ////////////////

        /// <summary>
        /// Unique identifier for the show.
        /// </summary>
        public string Id { get; set; }

        /// <summary>
        /// IMDB identifier for the show.
        /// </summary>
        public string ImdbId { get; set; }

        /// <summary>
        /// Show title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Collection of episodes in this show.
        /// </summary>
        public SortedList<string, Episode> Episodes { get; }

        /// <summary>
        /// Custom settings for this show.
        /// </summary>
        public ShowSettings Settings { get; set; }

        //////////////////
        // CONSTRUCTORS //
        //////////////////

        /// <summary>
        /// Initializes a show with ID, IMDB ID, title, and empty episodes.
        /// </summary>
        public Show(string id, string imdbId, string title)
        {
            this.Id = id;
            this.ImdbId = imdbId;
            this.Title = title;
            this.Episodes = new SortedList<string, Episode>();
        }

        /// <summary>
        /// Default constructor; calls main constructor with all nulls.
        /// </summary>
        public Show() : this(null, null, null)
        {
            // Calls delegating constructor
        }

        ////////////////////
        // PUBLIC METHODS //
        ////////////////////

        /// <summary>
        /// Compare shows by ID.
        /// </summary>
        public override bool Equals(object obj)
        {
            if (obj == null || !(obj is Show)) return false;

            Show comp = obj as Show;

            return comp.Id == this.Id;
        }

        /// <summary>
        /// Get hash code for this show.
        /// </summary>
        public override int GetHashCode()
        {
            return base.GetHashCode();
        }

        /// <summary>
        /// Find episode by season and number (S##E## format).
        /// </summary>
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
