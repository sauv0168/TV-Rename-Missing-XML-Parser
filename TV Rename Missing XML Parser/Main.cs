using System;
using System.Collections;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.ComponentModel;
using System.Diagnostics;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Runtime.InteropServices;
using System.Security;
using System.Text;
using System.Text.RegularExpressions;
using System.Windows.Forms;
using System.Xml.Linq;
using TV_Rename_Missing_XML_Parser.Entities;
using TV_Rename_Missing_XML_Parser.Controllers;

namespace TV_Rename_Missing_XML_Parser
{
    public partial class Main : Form
    {
        private readonly MainController controller;

        public Button BtnXMLFilePicker
        {
            get
            {
                return this.btnXMLFilePicker;
            }
        }

        public Label LblFile
        {
            get
            {
                return this.lblFile;
            }
        }

        public NumericUpDown NumMaxAge
        {
            get
            {
                return this.numMaxAge;
            }
        }

        public TreeView TreeResults
        {
            get
            {
                return this.treeResults;
            }
        }

        public TextBox TxtSearch
        {
            get
            {
                return this.txtSearch;
            }
        }

        public Main()
        {
            InitializeComponent();

            this.controller = new MainController(this);
        }

        /// <summary>
        /// Show Log Form dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnLog_Click(object sender, EventArgs e)
        {
            this.controller.DoBtnLogClick(this);
        }

        private void btnReload_Click(object sender, EventArgs e)
        {
            this.controller.DoReloadXMLFileClick();
        }

        /// <summary>
        /// Show the AppSettingForm dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void btnSettings_Click(object sender, EventArgs e)
        {
            this.controller.DoBtnSettingsClick(sender, e);
        }

        private void btnXMLFilePicker_Click(object sender, EventArgs e)
        {
            this.controller.DoBtnXMLFilePickerClick();
        }

        private void numMaxAge_KeyUp(object sender, KeyEventArgs e)
        {
            this.numMaxAge_ValueChanged(sender, e);
        }

        private void numMaxAge_ValueChanged(object sender, EventArgs e)
        {
            this.controller.DoNumMaxAgeValueChanged(sender, e);
        }

        private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        {
        }

        private void treeResults_KeyDown(object sender, KeyEventArgs e)
        {
            this.controller.DoTreeResultsKeyDown(sender, e);
        }

        private void treeResults_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            this.controller.DoTreeResultsNodeDoubleClick(sender, e);
        }

        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.controller.DoTxtSearchTextChanged(sender, e);
        }


    }
}