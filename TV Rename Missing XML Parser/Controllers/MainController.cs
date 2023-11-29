using System;
using System.Collections.Generic;
using System.Linq;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Xml.Linq;
using TV_Rename_Missing_XML_Parser.Entities;

namespace TV_Rename_Missing_XML_Parser.Controllers
{
    public class MainController
    {
        private const string CAPTION_CANNOT_OPEN_FILE = "Error";
        private const string ERROR_MESSAGE_NO_XML_FILE = "No file loaded. Use the {0} button.";
        private const string MESSAGE_CANNOT_OPEN_FILE = "Something went wrong while trying to open this file.";
        private const string MESSAGE_NO_XML_FILE_SELECTED = "Load XML file to view results";

        //private readonly LogForm logForm;
        private readonly Main mainForm;
        private readonly ShowController showController;
        private readonly TreeViewController treeViewController;
        private readonly UserSettingsController userSettingsController;
        private readonly WebController webController;
        private readonly TVRenameXMLController xmlParsingController;

        private string prevMaxAge;

        public MainController(Main mainForm)
        {
            this.mainForm = mainForm;

            this.userSettingsController = new UserSettingsController();
            this.webController = new WebController();
            this.xmlParsingController = new TVRenameXMLController();
            this.showController = new ShowController(this.userSettingsController, this.xmlParsingController);
            this.treeViewController = new TreeViewController(this.mainForm.TreeResults, this.showController.Shows);
            //this.logForm = new LogForm();

            if (this.userSettingsController.XmlFilePath != null && this.userSettingsController.XmlFilePath != "")
            {
                this.mainForm.LblFile.Text = this.userSettingsController.XmlFilePath;
            }

            this.generateResults();

            this.prevMaxAge = this.mainForm.NumMaxAge.Value.ToString();
        }

        public void DoBtnLogClick(Form parent)
        {
            //this.logForm.TopMost = true;
            //this.logForm.StartPosition = FormStartPosition.CenterParent;
            //this.logForm.Show(parent);
            //this.logForm.ShowDialog(parent);

            //LogForm lg = new LogForm();

            //lg.add(this.showController.Errors);

            //lg.ShowDialog(this.mainForm);
        }

        public void DoBtnSettingsClick(object sender, EventArgs e)
        {
            new AppSettingsForm(this.userSettingsController, this.webController)
                .ShowDialog(this.mainForm);
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

                    this.generateResults();
                }
                catch (SecurityException ex)
                {
                    MessageBox.Show($"Security error.\n\nError message: {ex.Message}\n\n" +
                    $"Details:\n\n{ex.StackTrace}");
                }
                catch (Exception)
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
            if (this.mainForm.NumMaxAge.Value.ToString() != this.prevMaxAge)
            {
                this.prevMaxAge = this.mainForm.NumMaxAge.Value.ToString();
                this.generateResults();
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
            }
            else if (e.KeyCode == Keys.D)
            {
                this.treeViewController.removeNode(tv.SelectedNode);
            }
            else if (e.Control && e.KeyCode == Keys.C)
            {
                Clipboard.SetText(tv.SelectedNode.Name);
            }
        }

        public void DoTreeResultsNodeDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.treeViewController.removeSelectedNode((TreeView)sender);
        }

        public void DoTxtMaxAge(object sender, EventArgs e)
        {
            this.generateResults();
        }

        public void DoTxtSearchTextChanged(object sender, EventArgs e)
        {
            this.generateResults();
        }

        public void OpenShowSettings(Form parent, TreeView tv)
        {
            Show show = this.treeViewController.getShow(tv.SelectedNode);
            new ShowSettingsForm(show, this.showController, this.userSettingsController).ShowDialog(parent);
        }

        private void generateResults()
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

            this.userSettingsController.Save();

            int maxAge = decimal.ToInt32(this.mainForm.NumMaxAge.Value);

            this.treeViewController.GenerateNewTree(this.mainForm.TxtSearch.Text, maxAge);
        }

        private void openTorrentSearch(TreeNode node)
        {
            //StringCollection IMDB_IDs = this.userSettingsTool.SHOW_DATA;
            Show show = this.treeViewController.getShow(node);

            StringBuilder sb = new StringBuilder();
            sb.Append(show.Title);

            if (node.Tag is Episode)
            {
                Episode e = node.Tag as Episode;
                sb.Append(" ").Append(e.SeasonAndNumber);
            }

            this.webController.OpenUrl(
                this.showController.GetTorrentSite(show)
                .GetSearchUrl(sb.ToString()));
        }

        private void resetShows()
        {
            this.showController.Reset();
            this.generateResults();
        }
    }
}