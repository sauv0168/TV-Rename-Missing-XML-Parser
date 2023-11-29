using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TV_Rename_Missing_XML_Parser.Entities
{
    public class Episode
    {
        public string Season { get; set; }
        public string Number { get; set; }
        public string Name { get; set; }

        private string pubDate;
        public string PubDate
        {
            get { return this.pubDate; }
            set
            {
                this.pubDate = value;
                //September 23, 2019 8:00:00 
                DateTime date = DateTime.ParseExact(value, "MMMM d, yyyy h:mm:ss tt", null);
                DateTime now = DateTime.Now;
                this.age = (int)(now.Date - date.Date).TotalDays;
                Console.WriteLine(now.Date + " - " + date.Date + " = " + this.age);
            }
        }

        private int age;
        public int Age { get { return this.age; } }
        public string SeasonAndNumber { get { return "S" + this.Season + "E" + this.Number; } }
        public string SeasonAndNumberWithName { get { return this.SeasonAndNumber + " - " + this.Name; } }
        public string FullName { get { return this.SeasonAndNumber + " - " + this.Name; } }
        public Show Show { get; set; }

        public Episode()
        {
            this.age = -1;
            this.pubDate = null;
        }
    }
}
