using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using TV_Rename_Missing_XML_Parser.Tools;

namespace TV_Rename_Missing_XML_Parser
{
    public partial class AppSettingsForm : Form
    {
        UserSettingsTool userSettingsTool;

        public AppSettingsForm()
        {
            InitializeComponent();
            this.userSettingsTool = UserSettingsTool.Get();
            txtWebsiteRARBG.Text = this.userSettingsTool.WEBSITE_RARBG;
            txtWebsiteTorrentz2.Text = this.userSettingsTool.WEBSITE_TORRENTZ2;

        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            string RARBG = "rarbg.to";
            string TORRENTZ2 = "torrentz2.";

            this.userSettingsTool.WEBSITE_RARBG = RARBG;
            txtWebsiteRARBG.Text = RARBG;

            this.userSettingsTool.WEBSITE_TORRENTZ2 = TORRENTZ2;
            txtWebsiteTorrentz2.Text = TORRENTZ2;
        }

        private void linkTorrentFreak_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebTools.Get().OpenUrl("https://torrentfreak.com");
        }
    }
}
