using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV_Rename_Missing_XML_Parser.Entities
{
    public class Show
    {
        public string Title { get; set; }
        public string IMDB_ID { get; set; }
        public string Website { get; set; }

        private SortedList<string, Episode> episodes;
        public SortedList<string, Episode> Episodes { get { return this.episodes; } }

        public Show()
        {
            this.episodes = new SortedList<string, Episode>();
        }

    }
}
