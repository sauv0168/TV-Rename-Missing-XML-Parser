using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using TV_Rename_Missing_XML_Parser.Entities;
using TV_Rename_Missing_XML_Parser.Tools;

namespace TV_Rename_Missing_XML_Parser
{
    public partial class Main : Form
    {
        private const string RARBG_URL = "https://rarbg.to/torrents.php?search={0}&page=1";
        private UserSettingsTool userSettingsTool;
        private string MISSING_ID_INDICATOR;

        private string fileName;
        private List<Show> shows;

        private LogForm logForm;

        public Main()
        {
            InitializeComponent();
            this.userSettingsTool = UserSettingsTool.Get();
            this.fileName = this.userSettingsTool.XML_FILE_PATH;
            if (this.fileName != null) this.lblFile.Text = this.fileName;
            this.shows = new List<Show>();
            this.MISSING_ID_INDICATOR = this.userSettingsTool.MISSING_ID_INDICATOR;
            treeResults.Nodes.Add("Load XML file to view results");
            this.logForm = new LogForm();
            
        }

        public void UpdateTreeResultsShowTitle(Show show)
        {
            TreeNode node = this.findShowTreeView(show);
            //bool nodeHasMissingIDIndicator = node.Text.StartsWith(this.MISSING_ID_INDICATOR);
            if (show.IMDB_ID == "")
                node.Text = this.generateMissingIDIndicatorTitle(show);
            else
                node.Text = show.Title;
        }

        private void openFileDialog1_FileOk(object sender, CancelEventArgs e)
        {

        }

        private void btnFile_Click(object sender, EventArgs e)
        {
            OpenFileDialog openFileDialog1 = new OpenFileDialog();

            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    lblFile.Text = this.fileName = openFileDialog1.FileName;
                    this.generateResults();
                    this.userSettingsTool.XML_FILE_PATH = this.fileName;
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
                catch (Exception)
                {
                    MessageBox.Show(
                            "Something went wrong while trying to open this file.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
                }
            }
        }

        private void generateResults()
        {
            if (this.fileName == "")
            {
                MessageBox.Show("No file loaded. Use the Load button.");
                return;
            }

            if (this.shows.Count == 0)
            {
                this.shows.Clear();
                XElement missingItems = (XElement)XElement.Load(this.fileName).FirstNode;

                //Create show objects by title
                foreach (var title in missingItems.Descendants("title"))
                {
                    if (this.findShow(title.Value) == null)
                    {
                        Show show = new Show();
                        show.Title = title.Value;
                        string storedID = userSettingsTool.GetIMDB_ID_ByShowTitle(show.Title);
                        show.IMDB_ID = storedID == null ? "" : storedID;

                        if (show.Website == null)
                        {
                            string website = this.userSettingsTool.WEBSITE_RARBG;
                            this.
                                userSettingsTool.
                                AddIMDB_ID_ByShowTitle(
                                    show.Title, 
                                    show.IMDB_ID, 
                                    website
                                    );

                            show.Website = website;
                        }
                        
                        
                        
                        this.shows.Add(show);
                        Console.WriteLine("added: " + show.Title);
                    }
                }

                Show currentShow = shows.First();
                foreach (var missingItem in missingItems.Elements())
                {
                    if (currentShow.Title != missingItem.Element("title").Value)
                        currentShow = findShow(missingItem.Element("title").Value);

                    Episode episode = new Episode();
                    episode.Season = missingItem.Element("season").Value;
                    episode.Number = missingItem.Element("episode").Value;
                    episode.Name = missingItem.Element("episodeName").Value;
                    if (missingItem.Element("pubDate").Value != "")
                        episode.PubDate = missingItem.Element("pubDate").Value;

                    if(currentShow.Episodes.ContainsKey(episode.SeasonAndNumber))
                    {
                        logForm.add(currentShow.Title + " " + episode.SeasonAndNumberWithName + 
                            " has identical season and episode numbers as another episode. Try \"Force Refresh\" that show.");

                        btnLog.BackColor = System.Drawing.Color.Red;
                    }
                    else
                    {
                        currentShow.Episodes.Add(
                                                episode.SeasonAndNumber,
                                                episode
                                                );
                    }
                }
            }

            treeResults.BeginUpdate();
            treeResults.Nodes.Clear();

            foreach (Show show in shows)
            {
                string shownTitle = show.IMDB_ID == "" ? this.generateMissingIDIndicatorTitle(show) : show.Title;
                TreeNode tree = new TreeNode(shownTitle);

                foreach (Episode episode in show.Episodes.Values)
                {
                    int maxAge = -1;
                    maxAge = int.TryParse(txtMaxAge.Text, out maxAge) ? maxAge : 14;

                    if (episode.Age <= maxAge)
                        tree.Nodes.Add(episode.SeasonAndNumber + " - " + episode.Name + " (" + episode.Age + ")");
                }

                if (tree.Nodes.Count != 0 && (txtSearch.Text != "" ? show.Title.Contains(txtSearch.Text) : true))
                    treeResults.Nodes.Add(tree);
            }

            // Reset the cursor to the default for all controls.
            Cursor.Current = Cursors.Default;

            // Begin repainting the TreeView.
            treeResults.EndUpdate();
        }

        private TreeNode findShowTreeView(Show show)
        {
            foreach (TreeNode node in treeResults.Nodes)
                if (node.Text.Contains(show.Title))
                    return node;

            return null;
        }

        private Show findShow(TreeView tv)
        {
            TreeNode tvShowNode = null;

            if (tv.SelectedNode != null)
                if (tv.SelectedNode.Parent == null)
                {
                    tvShowNode = tv.SelectedNode;
                }
                else
                {
                    TreeNode episodeNode = tv.SelectedNode;
                    tvShowNode = episodeNode.Parent;
                }

            return this.findShow(tvShowNode.Text);
        }

        private Show findShow(string title)
        {
            foreach (Show s in shows)
                if (s.Title == title) return s;

            return null;
        }

        private Episode findEpisode(Show show, string seasonAndNumber)
        {
            foreach (KeyValuePair<string, Episode> episodeInfo in show.Episodes)
            {
                Episode episode = episodeInfo.Value;
                if (episode.SeasonAndNumber == seasonAndNumber) return episode;
            }

            return null;
        }

        private void txtMaxAge_TextChanged(object sender, EventArgs e)
        {
            this.generateResults();
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            if (this.fileName == null) this.fileName = this.userSettingsTool.XML_FILE_PATH;
            this.shows.Clear();
            this.generateResults();
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.generateResults();
        }

        private void treeResults_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.removeTreeNode((TreeView) sender);
        }

        private void removeTreeNode(TreeView tv)
        {
            Show show = this.findShow(tv);

            if (tv.SelectedNode.Parent == null)
            {
                TreeNode tvShowNode = tv.SelectedNode;
                tvShowNode.Remove();
                if (show != null) this.shows.Remove(show);
            }
            else
            {
                TreeNode episodeNode = tv.SelectedNode;
                TreeNode tvShowNode = episodeNode.Parent;
                if (show != null) show.Episodes.Remove(episodeNode.Text.Substring(0, 6));
                episodeNode.Remove();
                if (tvShowNode.GetNodeCount(false) == 0) tvShowNode.Remove();
            }
        }

        private void treeResults_KeyDown(object sender, KeyEventArgs e)
        {
            TreeView tv = (TreeView)sender;
            if (e.Shift) this.displayShowSettings(tv);
            else if (e.Alt)
            {
                StringCollection IMDB_IDs = this.userSettingsTool.SHOW_DATA;
                Show show = this.findShow(tv);

                string showID = this.userSettingsTool.GetIMDB_ID_ByShowTitle(show.Title);

                if (showID != null)
                {
                    string searchString = "";
                    if (this.isNode(tv))
                    {
                        Regex pattern = new Regex(@"S\d+E\d+");
                        Match match = pattern.Match(tv.SelectedNode.Text);
                        searchString = showID + " " + match.Value;
                    }
                    else
                    {
                        searchString = showID;
                    }

                    WebTools.Get().OpenUrl(String.Format(RARBG_URL, searchString).Replace(" ", "%20"));
                }
                else
                    MessageBox.Show(
                            "No IMDB ID for this Show, try adding it by pushing CTRL while the show is selected.",
                            "Error",
                            MessageBoxButtons.OK,
                            MessageBoxIcon.Error
                        );
            }
            else if(e.KeyCode == Keys.D)
            {
                this.removeTreeNode(tv);
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(tv.SelectedNode.Text);
            }
        }

        private void displayShowSettings(TreeView sender)
        {
            TreeView tv = (TreeView)sender;
            Show show = null;
            //Episode epidode = null;
            TreeNode tvShowNode = null;

            if (tv.SelectedNode != null)
                if (tv.SelectedNode.Parent == null) //Show
                {
                    tvShowNode = tv.SelectedNode;
                }
                else //Episode
                {
                    TreeNode episodeNode = tv.SelectedNode;
                    tvShowNode = episodeNode.Parent;
                }

            string showTitle = tvShowNode.Text.StartsWith(this.MISSING_ID_INDICATOR) ? tvShowNode.Text.Remove(0, this.MISSING_ID_INDICATOR.Length) : tvShowNode.Text;
            show = this.findShow(showTitle);

            ShowSettingsForm showSettingsForm = new ShowSettingsForm(show);
            
            showSettingsForm.ShowDialog(this);
        }

        /**
        private void loadSettings()
        {
            XDocument doc = new XDocument(new XDeclaration("1.0", Encoding.UTF8.HeaderName, String.Empty),
              new XComment("Xml Document"),
              new XElement("settings",
                    new XElement("book", new XAttribute("id", "bk001"),
                          new XElement("title", "Book Title")
                    )
              ));
        }
        */

        private bool isNode(TreeView tv)
        {
            return tv.SelectedNode.Parent != null;
        }

        private string generateMissingIDIndicatorTitle(Show show)
        {
            return this.MISSING_ID_INDICATOR + show.Title;
        }

        private void btnLog_Click(object sender, EventArgs e)
        {
            this.logForm.ShowDialog(this);
        }

        private void btnSettings_Click(object sender, EventArgs e)
        {
            AppSettingsForm appSettingsForm = new AppSettingsForm();

            appSettingsForm.ShowDialog(this);
        }
    }
}
