namespace TV_Rename_Missing_XML_Parser
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
            this.txtIMDB_ID = new System.Windows.Forms.TextBox();
            this.label1 = new System.Windows.Forms.Label();
            this.rbRARBG = new System.Windows.Forms.RadioButton();
            this.rbTorrentz2 = new System.Windows.Forms.RadioButton();
            this.SuspendLayout();
            // 
            // txtIMDB_ID
            // 
            this.txtIMDB_ID.Location = new System.Drawing.Point(77, 28);
            this.txtIMDB_ID.Name = "txtIMDB_ID";
            this.txtIMDB_ID.Size = new System.Drawing.Size(151, 22);
            this.txtIMDB_ID.TabIndex = 0;
            this.txtIMDB_ID.TextChanged += new System.EventHandler(this.txtIMDB_ID_TextChanged);
            this.txtIMDB_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIMDB_ID_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(13, 31);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(58, 17);
            this.label1.TabIndex = 1;
            this.label1.Text = "IMDB ID";
            // 
            // rbRARBG
            // 
            this.rbRARBG.AutoSize = true;
            this.rbRARBG.Location = new System.Drawing.Point(16, 67);
            this.rbRARBG.Name = "rbRARBG";
            this.rbRARBG.Size = new System.Drawing.Size(78, 21);
            this.rbRARBG.TabIndex = 2;
            this.rbRARBG.TabStop = true;
            this.rbRARBG.Text = "RARBG";
            this.rbRARBG.UseVisualStyleBackColor = true;
            this.rbRARBG.CheckedChanged += new System.EventHandler(this.rbRARBG_CheckedChanged);
            // 
            // rbTorrentz2
            // 
            this.rbTorrentz2.AutoSize = true;
            this.rbTorrentz2.Location = new System.Drawing.Point(142, 67);
            this.rbTorrentz2.Name = "rbTorrentz2";
            this.rbTorrentz2.Size = new System.Drawing.Size(86, 21);
            this.rbTorrentz2.TabIndex = 3;
            this.rbTorrentz2.TabStop = true;
            this.rbTorrentz2.Text = "torrentz2";
            this.rbTorrentz2.UseVisualStyleBackColor = true;
            this.rbTorrentz2.CheckedChanged += new System.EventHandler(this.rbTorrentz2_CheckedChanged);
            // 
            // ShowSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(8F, 16F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(246, 108);
            this.Controls.Add(this.rbTorrentz2);
            this.Controls.Add(this.rbRARBG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIMDB_ID);
            this.Name = "ShowSettingsForm";
            this.Text = "ShowSettingsForm";
            this.FormClosing += new System.Windows.Forms.FormClosingEventHandler(this.ShowSettingsForm_FormClosing);
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.TextBox txtIMDB_ID;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.RadioButton rbRARBG;
        private System.Windows.Forms.RadioButton rbTorrentz2;
    }
}