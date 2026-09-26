namespace TV_Rename_Missing_XML_Parser.Forms
{
    partial class ShowSettingsForm
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
            this.lblCleanTitle = new System.Windows.Forms.Label();
            this.txtCleanTitle = new System.Windows.Forms.TextBox();
            this.ddlSite = new System.Windows.Forms.ComboBox();
            this.lblSite = new System.Windows.Forms.Label();
            this.SuspendLayout();
            // 
            // lblCleanTitle
            // 
            this.lblCleanTitle.AutoSize = true;
            this.lblCleanTitle.Location = new System.Drawing.Point(12, 22);
            this.lblCleanTitle.Name = "lblCleanTitle";
            this.lblCleanTitle.Size = new System.Drawing.Size(71, 16);
            this.lblCleanTitle.TabIndex = 3;
            this.lblCleanTitle.Text = "Clean Title";
            // 
            // txtCleanTitle
            // 
            this.txtCleanTitle.Location = new System.Drawing.Point(91, 19);
            this.txtCleanTitle.Name = "txtCleanTitle";
            this.txtCleanTitle.Size = new System.Drawing.Size(121, 22);
            this.txtCleanTitle.TabIndex = 4;
            // 
            // ddlSite
            // 
            this.ddlSite.FormattingEnabled = true;
            this.ddlSite.Location = new System.Drawing.Point(91, 67);
            this.ddlSite.Name = "ddlSite";
            this.ddlSite.Size = new System.Drawing.Size(121, 24);
            this.ddlSite.TabIndex = 5;
            this.ddlSite.SelectedIndexChanged += new System.EventHandler(this.ddlSite_SelectedIndexChanged);
            // 
            // lblSite
            // 
            this.lblSite.AutoSize = true;
            this.lblSite.Location = new System.Drawing.Point(53, 70);
            this.lblSite.Name = "lblSite";
            this.lblSite.Size = new System.Drawing.Size(30, 16);
            this.lblSite.TabIndex = 6;
            this.lblSite.Text = "Site";
            // 
            // ShowSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(245, 160);
            this.Controls.Add(this.lblSite);
            this.Controls.Add(this.ddlSite);
            this.Controls.Add(this.txtCleanTitle);
            this.Controls.Add(this.lblCleanTitle);
            this.Margin = new System.Windows.Forms.Padding(3, 2, 3, 2);
            this.Name = "ShowSettingsForm";
            this.Text = "ShowSettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowSettingsForm_FormClosing);
            this.Load += new System.EventHandler(this.ShowSettingsForm_Load);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion
        private System.Windows.Forms.Label lblCleanTitle;
        private System.Windows.Forms.TextBox txtCleanTitle;
        private System.Windows.Forms.ComboBox ddlSite;
        private System.Windows.Forms.Label lblSite;
    }
}