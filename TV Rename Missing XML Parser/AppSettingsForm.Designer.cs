namespace TV_Rename_Missing_XML_Parser
{
    partial class AppSettingsForm
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
            this.lblWebsiteRARBG = new System.Windows.Forms.Label();
            this.lblWebsiteTorrentz2 = new System.Windows.Forms.Label();
            this.txtWebsiteRARBG = new System.Windows.Forms.TextBox();
            this.txtWebsiteTorrentz2 = new System.Windows.Forms.TextBox();
            this.btnRestoreDefaults = new System.Windows.Forms.Button();
            this.linkTorrentFreak = new System.Windows.Forms.LinkLabel();
            this.SuspendLayout();
            // 
            // lblWebsiteRARBG
            // 
            this.lblWebsiteRARBG.AutoSize = true;
            this.lblWebsiteRARBG.Location = new System.Drawing.Point(26, 16);
            this.lblWebsiteRARBG.Name = "lblWebsiteRARBG";
            this.lblWebsiteRARBG.Size = new System.Drawing.Size(89, 17);
            this.lblWebsiteRARBG.TabIndex = 0;
            this.lblWebsiteRARBG.Text = "RARBG URL";
            // 
            // lblWebsiteTorrentz2
            // 
            this.lblWebsiteTorrentz2.AutoSize = true;
            this.lblWebsiteTorrentz2.Location = new System.Drawing.Point(13, 44);
            this.lblWebsiteTorrentz2.Name = "lblWebsiteTorrentz2";
            this.lblWebsiteTorrentz2.Size = new System.Drawing.Size(102, 17);
            this.lblWebsiteTorrentz2.TabIndex = 1;
            this.lblWebsiteTorrentz2.Text = "Torrentz2 URL";
            // 
            // txtWebsiteRARBG
            // 
            this.txtWebsiteRARBG.Location = new System.Drawing.Point(122, 13);
            this.txtWebsiteRARBG.Name = "txtWebsiteRARBG";
            this.txtWebsiteRARBG.Size = new System.Drawing.Size(100, 22);
            this.txtWebsiteRARBG.TabIndex = 2;
            // 
            // txtWebsiteTorrentz2
            // 
            this.txtWebsiteTorrentz2.Location = new System.Drawing.Point(122, 41);
            this.txtWebsiteTorrentz2.Name = "txtWebsiteTorrentz2";
            this.txtWebsiteTorrentz2.Size = new System.Drawing.Size(100, 22);
            this.txtWebsiteTorrentz2.TabIndex = 3;
            // 
            // btnRestoreDefaults
            // 
            this.btnRestoreDefaults.Location = new System.Drawing.Point(662, 10);
            this.btnRestoreDefaults.Name = "btnRestoreDefaults";
            this.btnRestoreDefaults.Size = new System.Drawing.Size(126, 23);
            this.btnRestoreDefaults.TabIndex = 4;
            this.btnRestoreDefaults.Text = "Restore Defaults";
            this.btnRestoreDefaults.UseVisualStyleBackColor = true;
            this.btnRestoreDefaults.Click += new System.EventHandler(this.btnRestoreDefaults_Click);
            // 
            // linkTorrentFreak
            // 
            this.linkTorrentFreak.AutoSize = true;
            this.linkTorrentFreak.LinkArea = new System.Windows.Forms.LinkArea(6, 24);
            this.linkTorrentFreak.Location = new System.Drawing.Point(16, 75);
            this.linkTorrentFreak.Name = "linkTorrentFreak";
            this.linkTorrentFreak.Size = new System.Drawing.Size(277, 20);
            this.linkTorrentFreak.TabIndex = 5;
            this.linkTorrentFreak.TabStop = true;
            this.linkTorrentFreak.Text = "Visit https://torrentfreak.com for website URLs";
            this.linkTorrentFreak.UseCompatibleTextRendering = true;
            this.linkTorrentFreak.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkTorrentFreak_LinkClicked);
            // 
            // AppSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(800, 450);
            this.Controls.Add(this.linkTorrentFreak);
            this.Controls.Add(this.btnRestoreDefaults);
            this.Controls.Add(this.txtWebsiteTorrentz2);
            this.Controls.Add(this.txtWebsiteRARBG);
            this.Controls.Add(this.lblWebsiteTorrentz2);
            this.Controls.Add(this.lblWebsiteRARBG);
            this.Name = "AppSettingsForm";
            this.Text = "AppSettings";
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label lblWebsiteRARBG;
        private System.Windows.Forms.Label lblWebsiteTorrentz2;
        private System.Windows.Forms.TextBox txtWebsiteRARBG;
        private System.Windows.Forms.TextBox txtWebsiteTorrentz2;
        private System.Windows.Forms.Button btnRestoreDefaults;
        private System.Windows.Forms.LinkLabel linkTorrentFreak;
    }
}