namespace TV_Rename_Missing_XML_Parser.Forms
{
    partial class MainForm
    {
        /// <summary>
        /// Required designer variable.
        /// </summary>
        private System.ComponentModel.IContainer components = null;

        /// <summary>
        /// Clean up any resources being used.
        /// </summary>
        /// <param name="disposing">true if managed resources should be disposed; otherwise, false.</param>
        protected override void Dispose(bool disposing)
        {
            if (disposing && (components != null))
            {
                components.Dispose();
            }
            base.Dispose(disposing);
        }

        #region Windows Form Designer generated code

        /// <summary>
        /// Required method for Designer support - do not modify
        /// the contents of this method with the code editor.
        /// </summary>
        private void InitializeComponent()
        {
            this.components = new System.ComponentModel.Container();
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainForm));
            this.btnXMLFilePicker = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.treeResults = new System.Windows.Forms.TreeView();
            this.lblMaxAge = new System.Windows.Forms.Label();
            this.btnReloadXMLFile = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnLog = new System.Windows.Forms.Button();
            this.numMaxAge = new System.Windows.Forms.NumericUpDown();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            this.btnHelp = new System.Windows.Forms.Button();
            this.btnSwitchSpecials = new System.Windows.Forms.Button();
            this.btnSwitchIgnored = new System.Windows.Forms.Button();
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAge)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXMLFilePicker
            // 
            this.btnXMLFilePicker.Location = new System.Drawing.Point(12, 12);
            this.btnXMLFilePicker.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnXMLFilePicker.Name = "btnXMLFilePicker";
            this.btnXMLFilePicker.Size = new System.Drawing.Size(88, 23);
            this.btnXMLFilePicker.TabIndex = 0;
            this.btnXMLFilePicker.Text = "Load File";
            this.btnXMLFilePicker.UseVisualStyleBackColor = true;
            this.btnXMLFilePicker.Click += new System.EventHandler(this.btnXMLFilePicker_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 43);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(100, 16);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "No file selected";
            // 
            // treeResults
            // 
            this.treeResults.Location = new System.Drawing.Point(11, 117);
            this.treeResults.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.treeResults.Name = "treeResults";
            this.treeResults.Size = new System.Drawing.Size(776, 365);
            this.treeResults.TabIndex = 2;
            this.treeResults.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeResults_NodeMouseDoubleClick);
            this.treeResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeResults_KeyDown);
            // 
            // lblMaxAge
            // 
            this.lblMaxAge.AutoSize = true;
            this.lblMaxAge.Location = new System.Drawing.Point(599, 15);
            this.lblMaxAge.Name = "lblMaxAge";
            this.lblMaxAge.Size = new System.Drawing.Size(108, 16);
            this.lblMaxAge.TabIndex = 4;
            this.lblMaxAge.Text = "Max Age in Days";
            // 
            // btnReloadXMLFile
            // 
            this.btnReloadXMLFile.Location = new System.Drawing.Point(105, 12);
            this.btnReloadXMLFile.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnReloadXMLFile.Name = "btnReloadXMLFile";
            this.btnReloadXMLFile.Size = new System.Drawing.Size(73, 23);
            this.btnReloadXMLFile.TabIndex = 5;
            this.btnReloadXMLFile.Text = "Reload";
            this.btnReloadXMLFile.UseVisualStyleBackColor = true;
            this.btnReloadXMLFile.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(248, 12);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(196, 22);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(188, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(50, 16);
            this.lblSearch.TabIndex = 7;
            this.lblSearch.Text = "Search";
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(712, 87);
            this.btnLog.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 26);
            this.btnLog.TabIndex = 8;
            this.btnLog.Text = "Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // numMaxAge
            // 
            this.numMaxAge.Location = new System.Drawing.Point(722, 11);
            this.numMaxAge.Margin = new System.Windows.Forms.Padding(4);
            this.numMaxAge.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numMaxAge.Name = "numMaxAge";
            this.numMaxAge.Size = new System.Drawing.Size(65, 22);
            this.numMaxAge.TabIndex = 10;
            this.numMaxAge.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numMaxAge.ValueChanged += new System.EventHandler(this.numMaxAge_ValueChanged);
            this.numMaxAge.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numMaxAge_KeyUp);
            this.numMaxAge.Leave += new System.EventHandler(this.numMaxAge_Leave);
            // 
            // btnHelp
            // 
            this.btnHelp.Location = new System.Drawing.Point(674, 87);
            this.btnHelp.Name = "btnHelp";
            this.btnHelp.Size = new System.Drawing.Size(32, 26);
            this.btnHelp.TabIndex = 11;
            this.btnHelp.Text = "?";
            this.btnHelp.UseVisualStyleBackColor = true;
            this.btnHelp.Click += new System.EventHandler(this.btnHelp_Click);
            // 
            // btnSwitchSpecials
            // 
            this.btnSwitchSpecials.Location = new System.Drawing.Point(418, 43);
            this.btnSwitchSpecials.Name = "btnSwitchSpecials";
            this.btnSwitchSpecials.Size = new System.Drawing.Size(124, 23);
            this.btnSwitchSpecials.TabIndex = 12;
            this.btnSwitchSpecials.Text = "Hide Specials";
            this.btnSwitchSpecials.UseVisualStyleBackColor = true;
            this.btnSwitchSpecials.Click += new System.EventHandler(this.btnSwitchSpecials_Click);
            // 
            // btnSwitchIgnored
            // 
            this.btnSwitchIgnored.Location = new System.Drawing.Point(548, 42);
            this.btnSwitchIgnored.Name = "btnSwitchIgnored";
            this.btnSwitchIgnored.Size = new System.Drawing.Size(111, 24);
            this.btnSwitchIgnored.TabIndex = 13;
            this.btnSwitchIgnored.Text = "Show Ignored";
            this.btnSwitchIgnored.UseVisualStyleBackColor = true;
            this.btnSwitchIgnored.Click += new System.EventHandler(this.btnSwitchIgnored_Click);
            // 
            // MainForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 493);
            this.Controls.Add(this.btnSwitchIgnored);
            this.Controls.Add(this.btnSwitchSpecials);
            this.Controls.Add(this.btnHelp);
            this.Controls.Add(this.numMaxAge);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnReloadXMLFile);
            this.Controls.Add(this.lblMaxAge);
            this.Controls.Add(this.treeResults);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnXMLFilePicker);
            this.Icon = ((System.Drawing.Icon)(resources.GetObject("$this.Icon")));
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "MainForm";
            this.Text = "TV Rename Missing XML Viewer";
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAge)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnXMLFilePicker;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TreeView treeResults;
        private System.Windows.Forms.Label lblMaxAge;
        private System.Windows.Forms.Button btnReloadXMLFile;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.NumericUpDown numMaxAge;
        private System.Windows.Forms.Timer timer1;
        private System.Windows.Forms.Button btnHelp;
        private System.Windows.Forms.Button btnSwitchSpecials;
        private System.Windows.Forms.Button btnSwitchIgnored;
    }
}

