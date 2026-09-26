using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV_Rename_Missing_XML_Parser.Entities
{
    public class Episode
    {
        ////////////////
        // ATTRIBUTES //
        ////////////////

        /// <summary>
        /// Season number as "##".
        /// </summary>
        public string Season { get; set; }

        /// <summary>
        /// Episode number within season as "##".
        /// </summary>
        public string Number { get; set; }

        /// <summary>
        /// Episode title.
        /// </summary>
        public string Title { get; set; }

        /// <summary>
        /// Publication date string.
        /// </summary>
        private string pubDate;

        /// <summary>
        /// Publication date; automatically calculates age when set.
        /// </summary>
        public string PubDate
        {
            get { return this.pubDate; }
            set
            {
                this.pubDate = value;
                this.age = CalculateAge(value);
            }
        }

        /// <summary>
        /// Days since episode published.
        /// </summary>
        private int age;

        /// <summary>
        /// Days since publication (read-only).
        /// </summary>
        public int Age
        {
            get { return this.age; }
        }

        /// <summary>
        /// The show this episode belongs to.
        /// </summary>
        public Show Show { get; set; }

        //////////////////
        // CONSTRUCTORS //
        //////////////////

        public Episode()
        {
            this.Season = null;
            this.Number = null;
            this.Title = null;
            this.pubDate = null;
            this.age = -1;
            this.Show = null;
        }

        /////////////////////////
        // COMPUTED PROPERTIES //
        /////////////////////////

        /// <summary>
        /// Season and episode number formatted as S##E##.
        /// </summary>
        public string SeasonAndNumber
        {
            get { return "S" + this.Season + "E" + this.Number; }
        }

        /// <summary>
        /// Season, episode, and name formatted as S##E## - Name.
        /// </summary>
        public string SeasonAndNumberWithName
        {
            get { return this.SeasonAndNumber + " - " + this.Title; }
        }

        /// <summary>
        /// Full display name (same as SeasonAndNumberWithName).
        /// </summary>
        public string FullName
        {
            get { return this.SeasonAndNumber + " - " + this.Title; }
        }

        ////////////////////
        // PUBLIC METHODS //
        ////////////////////

        /// <summary>
        /// Check if episode is a special (season 00).
        /// </summary>
        public bool IsSpecial()
        {
            return this.Season == "00";
        }

        /////////////////////
        // PRIVATE METHODS //
        /////////////////////

        /// <summary>
        /// Calculate days since publication date.
        /// </summary>
        private int CalculateAge(string dateString)
        {
            //TODO: Decide if this should be in a more global utility class
            DateTime date = DateTime.ParseExact(dateString, "MMMM d, yyyy h:mm:ss tt", null);
            DateTime now = DateTime.Now;
            return (int)(now.Date - date.Date).TotalDays;
        }
    }
}
