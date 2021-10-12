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
            this.SuspendLayout();
            // 
            // txtIMDB_ID
            // 
            this.txtIMDB_ID.Location = new System.Drawing.Point(58, 23);
            this.txtIMDB_ID.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.txtIMDB_ID.Name = "txtIMDB_ID";
            this.txtIMDB_ID.Size = new System.Drawing.Size(114, 20);
            this.txtIMDB_ID.TabIndex = 0;
            this.txtIMDB_ID.TextChanged += new System.EventHandler(this.txtIMDB_ID_TextChanged);
            this.txtIMDB_ID.KeyDown += new System.Windows.Forms.KeyEventHandler(this.txtIMDB_ID_KeyDown);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(10, 25);
            this.label1.Margin = new System.Windows.Forms.Padding(2, 0, 2, 0);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(48, 13);
            this.label1.TabIndex = 1;
            this.label1.Text = "IMDB ID";
            // 
            // rbRARBG
            // 
            this.rbRARBG.AutoSize = true;
            this.rbRARBG.Location = new System.Drawing.Point(12, 54);
            this.rbRARBG.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
            this.rbRARBG.Name = "rbRARBG";
            this.rbRARBG.Size = new System.Drawing.Size(63, 17);
            this.rbRARBG.TabIndex = 2;
            this.rbRARBG.TabStop = true;
            this.rbRARBG.Text = "RARBG";
            this.rbRARBG.UseVisualStyleBackColor = true;
            this.rbRARBG.CheckedChanged += new System.EventHandler(this.rbRARBG_CheckedChanged);
            // 
            // ShowSettingsForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(184, 88);
            this.Controls.Add(this.rbRARBG);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.txtIMDB_ID);
            this.Margin = new System.Windows.Forms.Padding(2, 2, 2, 2);
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
    }
}