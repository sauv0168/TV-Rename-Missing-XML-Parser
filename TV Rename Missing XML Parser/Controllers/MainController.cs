using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TV_Rename_Missing_XML_Parser.Entities;
using TV_Rename_Missing_XML_Parser.Forms;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class MainController
    {
        private const string CAPTION_CANNOT_OPEN_FILE = "Error";
        private const string ERROR_MESSAGE_NO_XML_FILE = "No file loaded. Use the {0} button.";
        private const string MESSAGE_CANNOT_OPEN_FILE = "Something went wrong while trying to open this file.";
        private const string MESSAGE_NO_XML_FILE_SELECTED = "Load XML file to view results";

        private readonly MainForm mainForm;
        private readonly ShowController showController;
        private readonly SearchController searchController;
        private readonly TreeViewController treeViewController;
        private readonly UserSettingsController userSettingsController;
        private readonly WebController webController;
        private readonly TVRenameXMLController xmlParsingController;
        private readonly LogController logController;

        public MainController(MainForm mainForm)
        {
            this.mainForm = mainForm;
            this.logController = new LogController();

            this.userSettingsController = new UserSettingsController(this.logController);
            this.webController = new WebController(this.userSettingsController);
            this.xmlParsingController = new TVRenameXMLController();
            this.showController = new ShowController(this.userSettingsController, this.xmlParsingController);
            this.searchController = new SearchController();
            this.treeViewController = new TreeViewController(this.mainForm.TreeResults, this.showController.Shows, this.showController, this.searchController);

            this.CheckBrowserPath();
            this.userSettingsController.Validate();

            if (this.logController.HasLogs())
            {
                this.ShowLogForm();
            }

            if (this.userSettingsController.XmlFilePath != null && this.userSettingsController.XmlFilePath != "")
            {
                this.mainForm.LblFile.Text = this.userSettingsController.XmlFilePath;
            }

            this.InitMaxSearchAge();

            this.LoadXML();
        }

        public void DoBtnLogClick(Form parent)
        {
            this.logController.ShowLogForm(parent);
        }

        /// <summary>
        /// Toggle ignored episodes filter and update results.
        /// </summary>
        public void ToggleSwitchIgnored(Button button)
        {
            this.searchController.ToggleShowIgnored();
            button.Text = this.searchController.IsShowingIgnoredEpisodes ? "Hide Ignored" : "Show Ignored";
            this.treeViewController.RefreshTree(this.mainForm.TxtSearch.Text, this.GetMaxSearchAge());
        }

        /// <summary>
        /// Toggle special episodes filter and update results.
        /// </summary>
        public void ToggleSwitchSpecials(Button button)
        {
            this.searchController.ToggleShowSpecials();
            button.Text = this.searchController.IsShowingSpecialEpisodes ? "Hide Specials" : "Show Specials";
            this.treeViewController.RefreshTree(this.mainForm.TxtSearch.Text, this.GetMaxSearchAge());
        }

        public void DoBtnXMLFilePickerClick()
        {
            OpenFileDialog openFileDialog = new OpenFileDialog();

            if (openFileDialog.ShowDialog() == DialogResult.OK)
            {
                try
                {
                    this.mainForm.LblFile.Text = this.userSettingsController.XmlFilePath = openFileDialog.FileName;
                    this.userSettingsController.Save();

                    //if (this.userSettingsController.XmlFilePath != null)
                    //{
                    //    //can throw Exceptions if file cannot be loaded
                    //    this.xmlParsingController = new TVRenameXMLController();
                    //}

                    this.LoadXML();
                }
                catch (SecurityException se)
                {
                    MessageBox.Show($"Security error.\n\nError message: {se.Message}\n\n" +
                    $"Details:\n\n{se.StackTrace}");
                }
                catch (Exception ex)
                {
                    MessageBox.Show(
                        MESSAGE_CANNOT_OPEN_FILE,
                        CAPTION_CANNOT_OPEN_FILE,
                        MessageBoxButtons.OK,
                        MessageBoxIcon.Error
                    );
                }
            }
        }

        public void DoNumMaxAgeValueChanged(object sender, EventArgs e)
        {
            if (this.userSettingsController.SearchDelay <= 0)
            {
                this.logController.Add($"Error: Invalid SearchDelay: {this.userSettingsController.SearchDelay}");
                return;
            }

            int maxAge = this.GetMaxSearchAge();
            if (this.searchController.HasMaxAgeChanged(maxAge))
            {
                this.StartSearchDelayTimer();
            }
        }

        public void DoReloadXMLFileClick()
        {
            if (this.userSettingsController.XmlFilePath == "")
            {
                //TODO: ERROR
            }
            else
            {
                resetShows();
            }
        }

        public void DoTreeResultsKeyDown(object sender, KeyEventArgs e)
        {
            TreeView tv = (TreeView)sender;

            if (e.Shift)
            {
                this.OpenShowSettings(this.mainForm, (TreeView)sender);
                //this.displayShowSettings(tv.SelectedNode);
            }
            else if (e.Alt)
            {
                this.openTorrentSearch(tv.SelectedNode);
                this.RecordActivity();
            }
            else if (e.KeyCode == Keys.D)
            {
                if (tv.SelectedNode != null && this.treeViewController.IsEpisodeNode(tv.SelectedNode))
                {
                    Episode episode = (Episode)tv.SelectedNode.Tag;
                    this.showController.ToggleIgnoreEpisode(episode);

                    bool isNowIgnored = episode.Show.Settings?.Episodes?
                        .FirstOrDefault(e => e.SeasonAndNumber == episode.SeasonAndNumber)?.Ignore ?? false;

                    if (isNowIgnored)
                    {
                        this.logController.Add($"Ignored {episode.Show.Title} {episode.SeasonAndNumber}");
                        this.treeViewController.removeNode(tv.SelectedNode);
                    }
                    else
                    {
                        this.logController.Add($"Unignored {episode.Show.Title} {episode.SeasonAndNumber}");
                    }

                    this.userSettingsController.Save();
                }
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                if (tv.SelectedNode != null)
                {
                    string textToCopy;
                    if (this.treeViewController.IsEpisodeNode(tv.SelectedNode))
                    {
                        Episode episode = (Episode)tv.SelectedNode.Tag;
                        textToCopy = this.webController.GetSearchString(episode);
                    }
                    else
                    {
                        Show show = this.treeViewController.getShow(tv.SelectedNode);
                        textToCopy = this.webController.GetSearchString(show);
                    }
                    Clipboard.SetText(textToCopy);
                    this.RecordActivity();
                }
                else
                {
                    MessageBox.Show("Select a show or episode to copy.");
                }
            }
        }

        public void DoTreeResultsNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.treeViewController.removeSelectedNode((TreeView)sender);
        }

        public void DoTxtSearchTextChanged(object sender, EventArgs e)
        {
            if (this.userSettingsController.SearchDelay <= 0)
            {
                this.logController.Add($"Error: Invalid SearchDelay: {this.userSettingsController.SearchDelay}");
                return;
            }

            this.StartSearchDelayTimer();
        }

        /// <summary>
        /// Disable tree and start search delay timer.
        /// </summary>
        private void StartSearchDelayTimer()
        {
            this.treeViewController.Disable();
            this.searchController.StartDelayTimer(
                this.userSettingsController.SearchDelay,
                () => {
                    this.treeViewController.RefreshTree(this.mainForm.TxtSearch.Text, this.GetMaxSearchAge());
                    this.treeViewController.Enable();
                }
            );
        }

        public void OpenShowSettings(Form parent, TreeView tv)
        {
            Show show = this.treeViewController.getShow(tv.SelectedNode);
            new ShowSettingsForm(show, this.showController, this.userSettingsController).ShowDialog(parent);
        }

        private void LoadXML()
        {
            if (this.userSettingsController.XmlFilePath == null || this.userSettingsController.XmlFilePath == "")
            {
                //MessageBox.Show(String.Format(ERROR_MESSAGE_NO_XML_FILE, this.mainForm.BtnXMLFilePicker.Text));
                this.treeViewController.ShowMessage(MESSAGE_NO_XML_FILE_SELECTED);
                return;
            }

            this.xmlParsingController.ParseXMLFile(
                this.userSettingsController.XmlFilePath,
                this.showController
                );

            this.showController.SortShowsByTitle();
            this.userSettingsController.Save();

            this.treeViewController.GenerateNewTree(this.mainForm.TxtSearch.Text, this.GetMaxSearchAge());
        }

        private void openTorrentSearch(TreeNode node)
        {
            Show show = this.treeViewController.getShow(node);
            TorrentSiteSettings site = this.showController.GetTorrentSite(show);
            string url;

            if (node.Tag is Episode)
            {
                Episode episode = (Episode)node.Tag;
                url = this.webController.GetSearchUrl(site, episode);
            }
            else
            {
                url = this.webController.GetSearchUrl(site, show);
            }

            this.webController.OpenUrl(url);
        }

        private void resetShows()
        {
            this.showController.ClearAndReloadShows();
            this.LoadXML();
        }

        /// <summary>
        /// Get the max search age from NumMaxAge control.
        /// </summary>
        private int GetMaxSearchAge()
        {
            return this.mainForm.NumMaxAge.Text == "" ? 0 : decimal.ToInt32(this.mainForm.NumMaxAge.Value);
        }

        private void CheckBrowserPath()
        {
            if (!string.IsNullOrEmpty(this.userSettingsController.BrowserPath))
            {
                if (!File.Exists(this.userSettingsController.BrowserPath))
                {
                    this.logController.Add($"Warning: Browser path not found: {this.userSettingsController.BrowserPath}");
                }
            }
        }

        private void ShowLogForm()
        {
            this.logController.ShowLogForm(this.mainForm);
        }

        private void RecordActivity()
        {
            this.userSettingsController.LastActivityTime = DateTime.Now.ToString("O");
            this.userSettingsController.Save();
        }

        /// <summary>
        /// Initialize max search age from settings. If settings load fails, use safe default.
        /// </summary>
        private void InitMaxSearchAge()
        {
            try
            {
                DateTime lastActivity = DateTime.Parse(this.userSettingsController.LastActivityTime);
                int daysSinceActivity = (int)(DateTime.Now - lastActivity).TotalDays;

                //Adds 1 to the number of days to ensure that day is included
                this.mainForm.NumMaxAge.Value = daysSinceActivity + 1;
            }
            catch
            {
                this.mainForm.NumMaxAge.Value = 14;
            }
        }

        public void DoNumMaxAgeLeave(object sender, EventArgs e)
        {
            if (this.mainForm.NumMaxAge.Text == "")
            {
                this.mainForm.NumMaxAge.Value = 0;
            }
        }
    }
}