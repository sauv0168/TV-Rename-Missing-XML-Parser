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

namespace TV_Rename_Missing_XML_Parser
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
            //txtIMDB_ID.Text = show.IMDB_ID;
            //rbRARBG.Text = this.userSettingsTool.WEBSITE_RARBG;

            //TODO: This seems bad
            //if (this.userSettingsController.WEBSITE_RARBG.ToLower().Contains(show.Website))
            //{
            //    rbRARBG.Checked = true;
            //}
            //else{
            //    rbRARBG.Checked = false;
            //}
        }

        private void txtIMDB_ID_TextChanged(object sender, EventArgs e)
        {
            this.show.ImdbId = txtIMDB_ID.Text;
        }

        private void ShowSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            //this.show.IMDB_ID = txtIMDB_ID.Text;

            //((Main) this.Owner).UpdateTreeResultsShowTitle(show);
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
    }
}
