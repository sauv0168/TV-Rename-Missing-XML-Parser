using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TV_Rename_Missing_XML_Parser.Controllers;

namespace TV_Rename_Missing_XML_Parser
{
    public partial class AppSettingsForm : Form
    {
        private readonly UserSettingsController userSettingsTool;
        private readonly WebController webController;

        public AppSettingsForm(UserSettingsController userSettingsController, WebController webController)
        {
            InitializeComponent();

            this.userSettingsTool = userSettingsController;
            this.webController = webController;
            //txtWebsiteRARBG.Text = this.userSettingsTool.WEBSITE_RARBG;
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            //string RARBG = "rarbg.to";

            //this.userSettingsTool.WEBSITE_RARBG = RARBG;
            //txtWebsiteRARBG.Text = RARBG;
        }

        private void linkTorrentFreak_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            this.webController.OpenUrl("https://torrentfreak.com");
        }
    }
}
