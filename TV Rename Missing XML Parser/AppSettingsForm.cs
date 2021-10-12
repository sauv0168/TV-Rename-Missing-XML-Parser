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
        readonly UserSettingsTool userSettingsTool;

        public AppSettingsForm()
        {
            InitializeComponent();
            this.userSettingsTool = UserSettingsTool.Get();
            txtWebsiteRARBG.Text = this.userSettingsTool.WEBSITE_RARBG;
        }

        private void btnRestoreDefaults_Click(object sender, EventArgs e)
        {
            string RARBG = "rarbg.to";

            this.userSettingsTool.WEBSITE_RARBG = RARBG;
            txtWebsiteRARBG.Text = RARBG;
        }

        private void linkTorrentFreak_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            WebTools.Get().OpenUrl("https://torrentfreak.com");
        }
    }
}
