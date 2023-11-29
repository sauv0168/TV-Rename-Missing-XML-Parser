using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class TVRenameXMLController
    {
        private const string DESCRIPTION = "description";
        private const string EPISODE_NAME = "episodeName";
        private const string EPISODE_NUMBER = "episode";

        private const string ID = "id";
        private const string IMDB_CODE = "imdbCode";
        private const string PUBLICATION_DATE = "pubDate";
        private const string SEASON_NUMBER = "season";
        private const string TITLE = "title";

        public TVRenameXMLController() { }

        public void ParseXMLFile(string xmlFilePath, ShowController showController)
        {
            if (xmlFilePath == null || xmlFilePath == "") return;

            XElement rootNode = XElement.Load(xmlFilePath);
            XElement missingItemsNode = (XElement)rootNode.FirstNode;

            foreach (var missingItemNode in missingItemsNode.Elements())
            {
                addEpisode(showController, missingItemNode);
            }
        }

        private void addEpisode(ShowController showController, XElement missingItemNode)
        {
            string showId = missingItemNode.Element(ID).Value;
            string showImdbId = missingItemNode.Element(IMDB_CODE).Value;
            string showTitle = missingItemNode.Element(TITLE).Value;

            string episodeSeason = missingItemNode.Element(SEASON_NUMBER).Value;
            string episodeNumber = missingItemNode.Element(EPISODE_NUMBER).Value;
            string episodeName = missingItemNode.Element(EPISODE_NAME).Value;

            string episodePubDate = missingItemNode.Element(PUBLICATION_DATE).Value;

            //TODO: drill through the episodes to find a date to use as a guess
            episodePubDate = episodePubDate == "" ? "December 16, 2022 3:00:00 AM" : episodePubDate;

            showController.AddEpisode(
                showId, showImdbId, showTitle,
                episodeSeason, episodeNumber, episodeName, episodePubDate
                );
        }
    }
}
