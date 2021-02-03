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
using TV_Rename_Missing_XML_Parser.Tools;

namespace TV_Rename_Missing_XML_Parser
{
    public partial class ShowSettingsForm : Form
    {
        private Show show;
        private UserSettingsTool userSettingsTool;

        public ShowSettingsForm(Show show)
        {
            InitializeComponent();
            this.show = show;
            this.userSettingsTool = UserSettingsTool.Get();
            txtIMDB_ID.Text = show.IMDB_ID;
            rbRARBG.Text = this.userSettingsTool.WEBSITE_RARBG;
            rbTorrentz2.Text = this.userSettingsTool.WEBSITE_TORRENTZ2;

            if (this.userSettingsTool.WEBSITE_RARBG.ToLower().Contains(show.Website))
            {
                rbRARBG.Checked = true;
                rbTorrentz2.Checked = false;
            }
            else{
                rbRARBG.Checked = false;
                rbTorrentz2.Checked = true;
            }
        }

        private void txtIMDB_ID_TextChanged(object sender, EventArgs e)
        {
            this.userSettingsTool.AddIMDB_ID_ByShowTitle(this.show.Title, txtIMDB_ID.Text, this.getWebsite());
        }

        private void ShowSettingsForm_FormClosing(object sender, FormClosingEventArgs e)
        {
            this.show.IMDB_ID = txtIMDB_ID.Text;
            
            ((Main) this.Owner).UpdateTreeResultsShowTitle(show);
        }

        private void txtIMDB_ID_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.KeyCode == Keys.Escape) this.Close();
        }

        private string getWebsite()
        {
            return rbRARBG.Checked ? rbRARBG.Text : rbTorrentz2.Text;
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
            this.userSettingsTool.AddIMDB_ID_ByShowTitle(
                this.show.Title, this.show.IMDB_ID, website
                );
        }
    }
}
