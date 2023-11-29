namespace TV_Rename_Missing_XML_Parser
{
    partial class Main
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
            this.btnXMLFilePicker = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.treeResults = new System.Windows.Forms.TreeView();
            this.lblMaxAge = new System.Windows.Forms.Label();
            this.btnReloadXMLFile = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.numMaxAge = new System.Windows.Forms.NumericUpDown();
            this.timer1 = new System.Windows.Forms.Timer(this.components);
            ((System.ComponentModel.ISupportInitialize)(this.numMaxAge)).BeginInit();
            this.SuspendLayout();
            // 
            // btnXMLFilePicker
            // 
            this.btnXMLFilePicker.Location = new System.Drawing.Point(9, 10);
            this.btnXMLFilePicker.Margin = new System.Windows.Forms.Padding(2);
            this.btnXMLFilePicker.Name = "btnXMLFilePicker";
            this.btnXMLFilePicker.Size = new System.Drawing.Size(66, 19);
            this.btnXMLFilePicker.TabIndex = 0;
            this.btnXMLFilePicker.Text = "Load File";
            this.btnXMLFilePicker.UseVisualStyleBackColor = true;
            this.btnXMLFilePicker.Click += new System.EventHandler(this.btnXMLFilePicker_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(9, 35);
            this.lblFile.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(80, 13);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "No file selected";
            // 
            // treeResults
            // 
            this.treeResults.Location = new System.Drawing.Point(9, 59);
            this.treeResults.Margin = new System.Windows.Forms.Padding(2);
            this.treeResults.Name = "treeResults";
            this.treeResults.Size = new System.Drawing.Size(583, 297);
            this.treeResults.TabIndex = 2;
            this.treeResults.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeResults_NodeMouseDoubleClick);
            this.treeResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeResults_KeyDown);
            // 
            // lblMaxAge
            // 
            this.lblMaxAge.AutoSize = true;
            this.lblMaxAge.Location = new System.Drawing.Point(427, 11);
            this.lblMaxAge.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblMaxAge.Name = "lblMaxAge";
            this.lblMaxAge.Size = new System.Drawing.Size(87, 13);
            this.lblMaxAge.TabIndex = 4;
            this.lblMaxAge.Text = "Max Age in Days";
            // 
            // btnReloadXMLFile
            // 
            this.btnReloadXMLFile.Location = new System.Drawing.Point(79, 10);
            this.btnReloadXMLFile.Margin = new System.Windows.Forms.Padding(2);
            this.btnReloadXMLFile.Name = "btnReloadXMLFile";
            this.btnReloadXMLFile.Size = new System.Drawing.Size(55, 19);
            this.btnReloadXMLFile.TabIndex = 5;
            this.btnReloadXMLFile.Text = "Reload";
            this.btnReloadXMLFile.UseVisualStyleBackColor = true;
            this.btnReloadXMLFile.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(235, 10);
            this.txtSearch.Margin = new System.Windows.Forms.Padding(2);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(148, 20);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(190, 12);
            this.lblSearch.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(41, 13);
            this.lblSearch.TabIndex = 7;
            this.lblSearch.Text = "Search";
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(535, 33);
            this.btnLog.Margin = new System.Windows.Forms.Padding(2);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(56, 21);
            this.btnLog.TabIndex = 8;
            this.btnLog.Text = "Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(474, 33);
            this.btnSettings.Margin = new System.Windows.Forms.Padding(2);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(56, 21);
            this.btnSettings.TabIndex = 9;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // numMaxAge
            // 
            this.numMaxAge.Location = new System.Drawing.Point(519, 8);
            this.numMaxAge.Maximum = new decimal(new int[] {
            99999,
            0,
            0,
            0});
            this.numMaxAge.Name = "numMaxAge";
            this.numMaxAge.Size = new System.Drawing.Size(73, 20);
            this.numMaxAge.TabIndex = 10;
            this.numMaxAge.Value = new decimal(new int[] {
            14,
            0,
            0,
            0});
            this.numMaxAge.ValueChanged += new System.EventHandler(this.numMaxAge_ValueChanged);
            this.numMaxAge.KeyUp += new System.Windows.Forms.KeyEventHandler(this.numMaxAge_KeyUp);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(600, 366);
            this.Controls.Add(this.numMaxAge);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnReloadXMLFile);
            this.Controls.Add(this.lblMaxAge);
            this.Controls.Add(this.treeResults);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnXMLFilePicker);
            this.Margin = new System.Windows.Forms.Padding(2);
            this.Name = "Main";
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
        private System.Windows.Forms.Button btnSettings;
        private System.Windows.Forms.NumericUpDown numMaxAge;
        private System.Windows.Forms.Timer timer1;
    }
}

