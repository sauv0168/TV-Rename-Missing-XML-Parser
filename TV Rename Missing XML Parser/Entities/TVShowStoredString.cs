//using System;
//using System.Collections.Generic;
//using System.IO;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using TV_Rename_Missing_XML_Parser.Tools;

//namespace TV_Rename_Missing_XML_Parser.Entities
//{
//    class TVShowStoredString
//    {
//        public string ShowTitle { get; set; }

//        private string imdb_id;

//        public string IMDB_ID { 
//            get
//            {
//                return this.imdb_id;
//            }
//            set
//            {
//                this.imdb_id = value;
//            }
//        }

//        private string downloadWebsite;
//        public string DownloadWebsite
//        {
//            get
//            {
//                return this.downloadWebsite;
//            }
//            set
//            {
//                string str = value == null ? null : value.Split('.')[0];
//                this.downloadWebsite = str;
//            }
//        }

//        public TVShowStoredString(string storedString)
//        {
//            StringReader sr = new StringReader(storedString);
//            this.ShowTitle = sr.ReadLine();
//            string storedIMDB_ID = sr.ReadLine();
//            this.IMDB_ID = storedIMDB_ID == "" ? null : storedIMDB_ID;
//            this.DownloadWebsite = sr.ReadLine();
//        }

//        public TVShowStoredString(string showName, string IMDB_ID, string dlWebsite)
//        {
//            this.ShowTitle = showName;
//            this.IMDB_ID = IMDB_ID;
//            this.DownloadWebsite = dlWebsite;
//        }

//        public string GetStoredFormat()
//        {
//            string str = "";
//            str += this.ShowTitle + Environment.NewLine;
//            str += (this.IMDB_ID == null ? "" : this.IMDB_ID) + Environment.NewLine;
//            str += this.DownloadWebsite == null ? "" : this.DownloadWebsite;
//            return str;
//        }

//        //public string GetFullDownloadWebsite()
//        //{
//        //    switch (this.downloadWebsite)
//        //    {
//        //        case "rarbg":
//        //            return UserSettingsTool.Get().WEBSITE_RARBG;
//        //        default:
//        //            return null;
//        //    }
//        //}

//        public override bool Equals(object obj)
//        {
//            var comp = (TVShowStoredString)obj;
//            return this.ShowTitle == comp.ShowTitle && this.IMDB_ID == comp.IMDB_ID && this.DownloadWebsite == comp.DownloadWebsite;
//        }

//        public override int GetHashCode()
//        {
//            int hashIMDB_ID = this.IMDB_ID == null ? "".GetHashCode() : this.IMDB_ID.GetHashCode();
//            int hashDownloadWebsite = this.DownloadWebsite == null ? "".GetHashCode() : this.DownloadWebsite.GetHashCode();
//            return this.ShowTitle.GetHashCode() ^ hashIMDB_ID ^ hashDownloadWebsite;
//        }
//    }
//}
