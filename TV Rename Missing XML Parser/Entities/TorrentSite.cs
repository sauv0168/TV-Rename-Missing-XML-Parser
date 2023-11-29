using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV_Rename_Missing_XML_Parser.Entities
{
    public partial class TorrentSite
    {
        public TorrentSite(string url, string path, string searchDelimiter)
        {
            this.Url = url;
            this.PathStart = path;
            this.SearchDelimiter = searchDelimiter;
        }

        public TorrentSite() : this(null, null, null)
        {
            //only calls other constructor
        }

        public string GetSearchUrl(string searchTerms)
        {
            return new StringBuilder()
                .Append(this.Url)
                .Append(this.PathStart)
                .Append(searchTerms.Replace(" ", this.SearchDelimiter))
                .Append(this.PathEnd)
                .ToString();
        }
    }
}
