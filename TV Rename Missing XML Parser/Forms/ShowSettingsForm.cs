using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TV_Rename_Missing_XML_Parser.Entities;
using TV_Rename_Missing_XML_Parser.Controllers;

namespace TV_Rename_Missing_XML_Parser.Forms
{
    public partial class ShowSettingsForm : Form
    {
        private readonly Show show;
        private readonly ShowController showController;
        private readonly UserSettingsController userSettingsController;

        public ShowSettingsForm(Show show, ShowController showController, UserSettingsController userSettingsController)
        {
            InitializeComponent();

            this.show = show;
            this.showController = showController;
            this.userSettingsController = userSettingsController;
        }


        /// <summary>
        /// Load show settings and populate controls.
        /// </summary>
        private void ShowSettingsForm_Load(object sender, EventArgs e)
        {
            txtCleanTitle.Text = this.showController.GetDisplayCleanTitle(this.show);
            this.PopulateSiteDropdown();
        }

        /// <summary>
        /// Populate site dropdown and select appropriate site.
        /// </summary>
        private void PopulateSiteDropdown()
        {
            this.ddlSite.Items.Clear();
            this.ddlSite.Items.Add("(use default)");

            foreach (TorrentSiteSettings site in this.userSettingsController.TorrentSites)
            {
                this.ddlSite.Items.Add(site.Name);
            }

            string siteName = this.showController.GetTorrentSiteName(this.show);
            if (siteName == null)
            {
                this.ddlSite.SelectedIndex = 0;
            }
            else
            {
                TorrentSiteSettings site = this.userSettingsController.GetSiteByName(siteName);
                if (site != null && this.ddlSite.Items.Contains(site.Name))
                {
                    this.ddlSite.SelectedItem = site.Name;
                }
            }
        }

        private void ShowSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.showController.SetCleanTitle(this.show, txtCleanTitle.Text);
            this.userSettingsController.Save();
        }

        private void txtIMDB_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private void rbRARBG_CheckedChanged(object sender, EventArgs e)
        {
            this.updateWebsite(((RadioButton)sender).Text);
        }

        private void rbTorrentz2_CheckedChanged(object sender, EventArgs e)
        {
            this.updateWebsite(((RadioButton)sender).Text);
        }

        private void updateWebsite(string website)
        {
            //TODO: website not saved or some other issue
            //this.userSettingsController.AddOrUpdateTVShow
            //    this.show.Title, this.show.IMDB_ID, website
            //    );
        }

        /// <summary>
        /// Save selected torrent site to show settings.
        /// </summary>
        private void ddlSite_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (this.ddlSite.SelectedIndex == 0)
            {
                this.showController.SetTorrentSiteName(this.show, null);
            }
            else if (this.ddlSite.SelectedItem != null)
            {
                this.showController.SetTorrentSiteName(this.show, this.ddlSite.SelectedItem.ToString());
            }

            this.userSettingsController.Save();
        }
    }
}
