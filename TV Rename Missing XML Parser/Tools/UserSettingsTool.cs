using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

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

        public string WEBSITE_TORRENTZ2
        {
            get
            {
                return Properties.Settings.Default.WEBSITE_TORRENTZ2;
            }
            set
            {
                Properties.Settings.Default.WEBSITE_TORRENTZ2 = value;
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

        public string GetIMDB_ID_ByShowTitle(string showTitle)
        {
            foreach (string showItem in this.SHOW_DATA)
            {
                StringReader sr = new StringReader(showItem);
                if(sr.ReadLine() == showTitle)
                {
                    return sr.Peek() != -1 ? sr.ReadLine() : null;
                }
            }

            return null;
        }

        public string GetWebsiteByShowTitle(string showTitle)
        {
            StringCollection IMDB_IDs = this.SHOW_DATA;

            foreach (string showItem in IMDB_IDs)
            {
                StringReader sr = new StringReader(showItem);
                if (sr.ReadLine() == showTitle)
                {
                    if(sr.Peek() != -1) sr.ReadLine();
                    return sr.Peek() != -1 ? sr.ReadLine() : null;
                }
            }

            return null;
        }

        public void AddIMDB_ID_ByShowTitle(string showTitle, string IMDB_ID, string website)
        {            
            if(this.GetIMDB_ID_ByShowTitle(showTitle) != null)
            {
                this.RemoveIMDB_ID_ByShowTitle(showTitle, null, website);
            }

            string websiteClean = website.ToLower().Contains("rarbg") ? "rarbg" : "torrentz2";
            
            this.SHOW_DATA.Add(this.toIMDB_ID_StoredString(showTitle, IMDB_ID, websiteClean));
            this.Save();
        }

        public void RemoveIMDB_ID_ByShowTitle(string showTitle, string IMDB_ID, string website)
        {
            if(IMDB_ID != null)
            {
                this.SHOW_DATA.Remove(this.toIMDB_ID_StoredString(showTitle, IMDB_ID, website));
            }
            else
            {
                int index = 0;
                for (; index < this.SHOW_DATA.Count; index++)
                {
                    if (this.SHOW_DATA[index].StartsWith(showTitle)) break;
                }
                this.SHOW_DATA.RemoveAt(index);
            }

            this.Save();
        }

        private string toIMDB_ID_StoredString(string showTitle, string IMDB_ID, string website)
        {
            return showTitle + "\r\n" + IMDB_ID + "\r\n" + website;
        }

        private string getIMDB_ID_StoredStringByTitle(string showTitle)
        {
            StringCollection IMDB_IDs = Properties.Settings.Default.SHOW_DATA;

            foreach (string showItem in IMDB_IDs)
                if (showItem.StartsWith(showTitle)) return showItem;

            return null;
        }

        private void Save()
        {
            Properties.Settings.Default.Save();
        }
    }
}
