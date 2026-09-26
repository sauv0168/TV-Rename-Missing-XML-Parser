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

namespace TV_Rename_Missing_XML_Parser.Forms
{
    public partial class MainForm : Form
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

        public MainForm()
        {
            InitializeComponent();

            this.controller = new MainController(this);
        }

        /// <summary>
        /// Show Log Form dialog
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        /// <summary>
        /// Fires when log button is clicked.
        /// </summary>
        private void btnLog_Click(object sender, EventArgs e)
        {
            this.controller.DoBtnLogClick(this);
        }

        /// <summary>
        /// Fires when reload button is clicked.
        /// </summary>
        private void btnReload_Click(object sender, EventArgs e)
        {
            this.controller.DoReloadXMLFileClick();
        }

        /// <summary>
        /// Fires when XML file picker button is clicked.
        /// </summary>
        private void btnXMLFilePicker_Click(object sender, EventArgs e)
        {
            this.controller.DoBtnXMLFilePickerClick();
        }

        /// <summary>
        /// Fires when spinner buttons change value (not when typing in textbox).
        /// </summary>
        private void numMaxAge_ValueChanged(object sender, EventArgs e)
        {
            if (this.controller != null)
                this.controller.DoNumMaxAgeValueChanged(sender, e);
        }

        /* TODO: Test the program to see if this is needed */
        //private void openFileDialog_FileOk(object sender, CancelEventArgs e)
        //{
        //}

        /// <summary>
        /// Fires when a key is pressed while tree has focus.
        /// </summary>
        private void treeResults_KeyDown(object sender, KeyEventArgs e)
        {
            this.controller.DoTreeResultsKeyDown(sender, e);
        }

        private void treeResults_NodeMouseDoubleClick(object sender, TreeNodeMouseClickEventArgs e)
        {
            //REMOVED: This seems to be buggy
            //this.controller.DoTreeResultsNodeDoubleClick(sender, e);
        }

        /// <summary>
        /// Fires when text is typed, pasted, or changed in search field.
        /// </summary>
        private void txtSearch_TextChanged(object sender, EventArgs e)
        {
            this.controller.DoTxtSearchTextChanged(sender, e);
        }

        /// <summary>
        /// Fires when help button is clicked.
        /// </summary>
        private void btnHelp_Click(object sender, EventArgs e)
        {
            string helpText = "TV Rename Missing XML Tool - Controls\n\n" +
                "KEYBOARD SHORTCUTS:\n" +
                "Ctrl+C - Copy selected show or episode to clipboard\n" +
                "Alt - Open selected show/episode in torrent browser\n" +
                "Shift - Open show settings (IMDB ID, Clean Title)\n" +
                "D - Toggle ignore on selected episode (shows cannot be ignored)\n\n" +
                "CONTROLS:\n" +
                "Load XML - Browse and select TV Rename XML file\n" +
                "Search - Filter shows by title (or Clean Title if set)\n" +
                "Max Age - Filter episodes by age in days (0 = show all)\n" +
                "Visit https://torrentfreak.com for website URLs";

            MessageBox.Show(helpText, "Help", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        /// <summary>
        /// Fires when a key is released; regenerates results if field is empty.
        /// </summary>
        private void numMaxAge_KeyUp(object sender, KeyEventArgs e)
        {
            this.controller.DoNumMaxAgeValueChanged(sender, e);
        }

        /// <summary>
        /// Fires when ignore switch button is clicked.
        /// </summary>
        private void btnSwitchIgnored_Click(object sender, EventArgs e)
        {
            this.controller.ToggleSwitchIgnored((Button)sender);
        }

        /// <summary>
        /// Fires when specials switch button is clicked.
        /// </summary>
        private void btnSwitchSpecials_Click(object sender, EventArgs e)
        {
            this.controller.ToggleSwitchSpecials((Button)sender);
        }

        private void numMaxAge_Leave(object sender, EventArgs e)
        {
            //this.controller.DoNumMaxAgeLeave(sender, e);
        }
    }
}