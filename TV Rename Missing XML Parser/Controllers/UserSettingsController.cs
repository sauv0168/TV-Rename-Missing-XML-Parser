using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class UserSettingsController
    {
        private const string JSON_PATH = "settings.json";
        private const string SITE_1337X_PATH = "/search/";
        private const string SITE_1337X_SEARCH_DELIMITER = "+";
        private const string SITE_1337X_URL = "https://1337x.to";

        private JsonSettings jsonSettings;

        public List<Show> Shows
        {
            get
            {
                return this.jsonSettings.Shows;
            }
        }

        public List<TorrentSite> TorrentSites
        {
            get
            {
                return this.jsonSettings.TorrentSites;
            }
        }

        public string XmlFilePath
        {
            get
            {
                return jsonSettings.XmlFilePath;
            }
            set
            {
                jsonSettings.XmlFilePath = value;
            }
        }

        public UserSettingsController()
        {
            this.Reset();
        }

        //public Show AddOrUpdateTVShowById(Show showData)
        //{
        //    Show match = this.jsonSettings.Shows.First<Show>(s => s.Id == showData.Id);

        //    if (match != null)
        //    {
        //        match.updateAttributesIfNotNull(showData);
        //        return match;
        //    }

        //    this.jsonSettings.Shows.Add(showData);
        //    return showData;
        //}

        public void Reset()
        {
            string jsonText = File.ReadAllText(JSON_PATH);
            this.jsonSettings = fastJSON.JSON.ToObject<JsonSettings>(jsonText);
        }

        public void RestoreDefaultGeneralSettings()
        {
            this.jsonSettings.XmlFilePath = "";
        }

        public void RestoreDefaults()
        {
            this.RestoreDefaultGeneralSettings();
            this.RestoreDefaultTorrentSites();
            this.RestoreDefaultShows();
        }

        public void RestoreDefaultShows()
        {
            this.jsonSettings.Shows.Clear();
        }

        public void RestoreDefaultTorrentSites()
        {
            this.jsonSettings.TorrentSites.Clear();
            this.jsonSettings.TorrentSites.Add(
                new TorrentSite(SITE_1337X_URL, SITE_1337X_PATH, SITE_1337X_SEARCH_DELIMITER)
                );
        }

        public void Save()
        {
            string jsonText = fastJSON.JSON.ToJSON(this.jsonSettings);
            File.WriteAllText(JSON_PATH, jsonText);
        }
    }
}
