using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Permissions;
using System.Text;
using System.Threading.Tasks;

namespace TV_Rename_Missing_XML_Parser.Entities
{
    public class JsonSettings
    {
        public string XmlFilePath { get; set; }
        public List<TorrentSite> TorrentSites { get; set; }
        public List<Show> Shows { get; set; }
    }

    public partial class TorrentSite
    {
        public string Url { get; set; }
        public string PathStart { get; set; }
        public string SearchDelimiter { get; set; }
        public string PathEnd {  get; set; }
    }

    public partial class Show
    {
        public string Id { get; set; }
        public string ImdbId { get; set; }
        public string Title { get; set; }
        public string TorrentSiteUrl { get; set; }
    }
}
