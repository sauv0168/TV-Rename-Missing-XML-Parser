using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Tools
{
    class UserSettingsTool
    {
        private static UserSettingsTool instance = null;

        public static UserSettingsTool Get()
        {
            return instance = instance == null ? new UserSettingsTool() : instance;
        }

        protected UserSettingsTool()
        {

        }

        public string XML_FILE_PATH
        {
            get
            {
                return Properties.Settings.Default.XML_FILE;
            }
            set
            {
                Properties.Settings.Default.XML_FILE = value;
                this.Save();
            }
        }

        public string MISSING_ID_INDICATOR
        {
            get
            {
                return Properties.Settings.Default.MISSING_ID_INDICATOR;
            }
        }

        public string WEBSITE_RARBG
        {
            get
            {
                return Properties.Settings.Default.WEBSITE_RARBG;
            }
            set
            {
                Properties.Settings.Default.WEBSITE_RARBG = value;
                this.Save();
            }
        }

        public StringCollection SHOW_DATA
        {
            get
            {
                return Properties.Settings.Default.SHOW_DATA;
            }
        }

        public void AddOrUpdateTVShowStoredString(string showTitle, string IMDB_ID, string website)
        {
            var item = new TVShowStoredString(showTitle, IMDB_ID, website);
            var storedItem = GetTVShowStoredStringByTitle(showTitle);
            
            if (storedItem != null)
            {
                if (item.Equals(storedItem)) return;

                this.RemoveTVShowStoredString(storedItem);
            }
            
            this.SHOW_DATA.Add(new TVShowStoredString(showTitle, IMDB_ID, website).GetStoredFormat());
            this.Save();
        }

        public void RemoveTVShowStoredString(TVShowStoredString item)
        {
            for(int index = 0; index < this.SHOW_DATA.Count; index++)
            {
                if (this.SHOW_DATA[index].StartsWith(item.ShowTitle))
                {
                    this.SHOW_DATA.RemoveAt(index);

                    this.Save();
                    return;
                }
            }

            throw new KeyNotFoundException();
        }

        public TVShowStoredString GetTVShowStoredStringByTitle(string showTitle)
        {
            StringCollection showItems = this.SHOW_DATA;

            foreach (string showItem in showItems)
            {
                var item = new TVShowStoredString(showItem);
                if (item.ShowTitle == showTitle) return item;
            }

            return null;
        }

        public TVShowStoredString GetTVShowStoredStringByIMDB_ID(string IMDB_ID)
        {
            StringCollection showItems = this.SHOW_DATA;

            foreach (string showItem in showItems)
            {
                var item = new TVShowStoredString(showItem);
                if (item.IMDB_ID == IMDB_ID) return item;
            }

            return null;
        }

        private void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
