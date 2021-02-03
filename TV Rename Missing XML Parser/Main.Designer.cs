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
            this.btnFile = new System.Windows.Forms.Button();
            this.lblFile = new System.Windows.Forms.Label();
            this.treeResults = new System.Windows.Forms.TreeView();
            this.txtMaxAge = new System.Windows.Forms.TextBox();
            this.lblMaxAge = new System.Windows.Forms.Label();
            this.btnReload = new System.Windows.Forms.Button();
            this.txtSearch = new System.Windows.Forms.TextBox();
            this.lblSearch = new System.Windows.Forms.Label();
            this.btnLog = new System.Windows.Forms.Button();
            this.btnSettings = new System.Windows.Forms.Button();
            this.SuspendLayout();
            // 
            // btnFile
            // 
            this.btnFile.Location = new System.Drawing.Point(12, 12);
            this.btnFile.Name = "btnFile";
            this.btnFile.Size = new System.Drawing.Size(75, 23);
            this.btnFile.TabIndex = 0;
            this.btnFile.Text = "Load File";
            this.btnFile.UseVisualStyleBackColor = true;
            this.btnFile.Click += new System.EventHandler(this.btnFile_Click);
            // 
            // lblFile
            // 
            this.lblFile.AutoSize = true;
            this.lblFile.Location = new System.Drawing.Point(12, 43);
            this.lblFile.Name = "lblFile";
            this.lblFile.Size = new System.Drawing.Size(105, 17);
            this.lblFile.TabIndex = 1;
            this.lblFile.Text = "No file selected";
            // 
            // treeResults
            // 
            this.treeResults.Location = new System.Drawing.Point(12, 73);
            this.treeResults.Name = "treeResults";
            this.treeResults.Size = new System.Drawing.Size(776, 365);
            this.treeResults.TabIndex = 2;
            this.treeResults.NodeMouseDoubleClick += new System.Windows.Forms.TreeNodeMouseClickEventHandler(this.treeResults_NodeMouseDoubleClick);
            this.treeResults.KeyDown += new System.Windows.Forms.KeyEventHandler(this.treeResults_KeyDown);
            // 
            // txtMaxAge
            // 
            this.txtMaxAge.Location = new System.Drawing.Point(688, 13);
            this.txtMaxAge.Name = "txtMaxAge";
            this.txtMaxAge.Size = new System.Drawing.Size(100, 22);
            this.txtMaxAge.TabIndex = 3;
            this.txtMaxAge.Text = "14";
            this.txtMaxAge.TextChanged += new System.EventHandler(this.txtMaxAge_TextChanged);
            // 
            // lblMaxAge
            // 
            this.lblMaxAge.AutoSize = true;
            this.lblMaxAge.Location = new System.Drawing.Point(569, 13);
            this.lblMaxAge.Name = "lblMaxAge";
            this.lblMaxAge.Size = new System.Drawing.Size(113, 17);
            this.lblMaxAge.TabIndex = 4;
            this.lblMaxAge.Text = "Max Age in Days";
            // 
            // btnReload
            // 
            this.btnReload.Location = new System.Drawing.Point(93, 12);
            this.btnReload.Name = "btnReload";
            this.btnReload.Size = new System.Drawing.Size(75, 23);
            this.btnReload.TabIndex = 5;
            this.btnReload.Text = "Reload";
            this.btnReload.UseVisualStyleBackColor = true;
            this.btnReload.Click += new System.EventHandler(this.btnReload_Click);
            // 
            // txtSearch
            // 
            this.txtSearch.Location = new System.Drawing.Point(313, 12);
            this.txtSearch.Name = "txtSearch";
            this.txtSearch.Size = new System.Drawing.Size(196, 22);
            this.txtSearch.TabIndex = 6;
            this.txtSearch.TextChanged += new System.EventHandler(this.txtSearch_TextChanged);
            // 
            // lblSearch
            // 
            this.lblSearch.AutoSize = true;
            this.lblSearch.Location = new System.Drawing.Point(254, 15);
            this.lblSearch.Name = "lblSearch";
            this.lblSearch.Size = new System.Drawing.Size(53, 17);
            this.lblSearch.TabIndex = 7;
            this.lblSearch.Text = "Search";
            // 
            // btnLog
            // 
            this.btnLog.Location = new System.Drawing.Point(713, 41);
            this.btnLog.Name = "btnLog";
            this.btnLog.Size = new System.Drawing.Size(75, 26);
            this.btnLog.TabIndex = 8;
            this.btnLog.Text = "Log";
            this.btnLog.UseVisualStyleBackColor = true;
            this.btnLog.Click += new System.EventHandler(this.btnLog_Click);
            // 
            // btnSettings
            // 
            this.btnSettings.Location = new System.Drawing.Point(632, 41);
            this.btnSettings.Name = "btnSettings";
            this.btnSettings.Size = new System.Drawing.Size(75, 26);
            this.btnSettings.TabIndex = 9;
            this.btnSettings.Text = "Settings";
            this.btnSettings.UseVisualStyleBackColor = true;
            this.btnSettings.Click += new System.EventHandler(this.btnSettings_Click);
            // 
            // Main
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.btnSettings);
            this.Controls.Add(this.btnLog);
            this.Controls.Add(this.lblSearch);
            this.Controls.Add(this.txtSearch);
            this.Controls.Add(this.btnReload);
            this.Controls.Add(this.lblMaxAge);
            this.Controls.Add(this.txtMaxAge);
            this.Controls.Add(this.treeResults);
            this.Controls.Add(this.lblFile);
            this.Controls.Add(this.btnFile);
            this.Name = "Main";
            this.Text = "TV Rename Missing XML Viewer";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button btnFile;
        private System.Windows.Forms.Label lblFile;
        private System.Windows.Forms.TreeView treeResults;
        private System.Windows.Forms.TextBox txtMaxAge;
        private System.Windows.Forms.Label lblMaxAge;
        private System.Windows.Forms.Button btnReload;
        private System.Windows.Forms.TextBox txtSearch;
        private System.Windows.Forms.Label lblSearch;
        private System.Windows.Forms.Button btnLog;
        private System.Windows.Forms.Button btnSettings;
    }
}

