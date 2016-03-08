namespace ELF
{
    partial class HighScores
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
            this.ScoreTabs = new System.Windows.Forms.TabControl();
            this.VH = new System.Windows.Forms.TabPage();
            this.SP = new System.Windows.Forms.TabPage();
            this.TTG = new System.Windows.Forms.TabPage();
            this.TTG_LI = new System.Windows.Forms.TabPage();
            this.GWB0 = new System.Windows.Forms.TabPage();
            this.GWB1 = new System.Windows.Forms.TabPage();
            this.GWB2 = new System.Windows.Forms.TabPage();
            this.ScoreList = new System.Windows.Forms.ListView();
            this.columnHeader1 = new System.Windows.Forms.ColumnHeader();
            this.columnHeader2 = new System.Windows.Forms.ColumnHeader();
            this.ScoreTabs.SuspendLayout();
            this.SuspendLayout();
            // 
            // ScoreTabs
            // 
            this.ScoreTabs.Anchor = ((System.Windows.Forms.AnchorStyles)(((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScoreTabs.Controls.Add(this.VH);
            this.ScoreTabs.Controls.Add(this.SP);
            this.ScoreTabs.Controls.Add(this.TTG);
            this.ScoreTabs.Controls.Add(this.TTG_LI);
            this.ScoreTabs.Controls.Add(this.GWB0);
            this.ScoreTabs.Controls.Add(this.GWB1);
            this.ScoreTabs.Controls.Add(this.GWB2);
            this.ScoreTabs.HotTrack = true;
            this.ScoreTabs.Location = new System.Drawing.Point(12, 12);
            this.ScoreTabs.Name = "ScoreTabs";
            this.ScoreTabs.SelectedIndex = 0;
            this.ScoreTabs.Size = new System.Drawing.Size(491, 26);
            this.ScoreTabs.TabIndex = 2;
            this.ScoreTabs.TabIndexChanged += new System.EventHandler(this.ScoreTabs_TabIndexChanged);
            this.ScoreTabs.SelectedIndexChanged += new System.EventHandler(this.ScoreTabs_TabIndexChanged);
            // 
            // VH
            // 
            this.VH.Location = new System.Drawing.Point(4, 22);
            this.VH.Name = "VH";
            this.VH.Padding = new System.Windows.Forms.Padding(3);
            this.VH.Size = new System.Drawing.Size(483, 0);
            this.VH.TabIndex = 0;
            this.VH.Text = "Vowel Howl";
            this.VH.UseVisualStyleBackColor = true;
            // 
            // SP
            // 
            this.SP.Location = new System.Drawing.Point(4, 22);
            this.SP.Name = "SP";
            this.SP.Size = new System.Drawing.Size(389, 0);
            this.SP.TabIndex = 3;
            this.SP.Text = "Super Sonic Phonics";
            this.SP.UseVisualStyleBackColor = true;
            // 
            // TTG
            // 
            this.TTG.Location = new System.Drawing.Point(4, 22);
            this.TTG.Name = "TTG";
            this.TTG.Size = new System.Drawing.Size(389, 0);
            this.TTG.TabIndex = 7;
            this.TTG.Text = "Tic-Tac-Gold";
            this.TTG.UseVisualStyleBackColor = true;
            // 
            // TTG_LI
            // 
            this.TTG_LI.Location = new System.Drawing.Point(4, 22);
            this.TTG_LI.Name = "TTG_LI";
            this.TTG_LI.Size = new System.Drawing.Size(389, 0);
            this.TTG_LI.TabIndex = 9;
            this.TTG_LI.Text = "Letter Identification";
            this.TTG_LI.UseVisualStyleBackColor = true;
            // 
            // GWB0
            // 
            this.GWB0.Location = new System.Drawing.Point(4, 22);
            this.GWB0.Name = "GWB0";
            this.GWB0.Size = new System.Drawing.Size(389, 0);
            this.GWB0.TabIndex = 8;
            this.GWB0.Text = "Genius Word Builder - Easy";
            this.GWB0.UseVisualStyleBackColor = true;
            // 
            // GWB1
            // 
            this.GWB1.Location = new System.Drawing.Point(4, 22);
            this.GWB1.Name = "GWB1";
            this.GWB1.Size = new System.Drawing.Size(389, 0);
            this.GWB1.TabIndex = 10;
            this.GWB1.Text = "Genius World Builder - Medium";
            this.GWB1.UseVisualStyleBackColor = true;
            // 
            // GWB2
            // 
            this.GWB2.Location = new System.Drawing.Point(4, 22);
            this.GWB2.Name = "GWB2";
            this.GWB2.Size = new System.Drawing.Size(389, 0);
            this.GWB2.TabIndex = 11;
            this.GWB2.Text = "Genius World Builder - Hard";
            this.GWB2.UseVisualStyleBackColor = true;
            // 
            // ScoreList
            // 
            this.ScoreList.Anchor = ((System.Windows.Forms.AnchorStyles)((((System.Windows.Forms.AnchorStyles.Top | System.Windows.Forms.AnchorStyles.Bottom)
                        | System.Windows.Forms.AnchorStyles.Left)
                        | System.Windows.Forms.AnchorStyles.Right)));
            this.ScoreList.Columns.AddRange(new System.Windows.Forms.ColumnHeader[] {
            this.columnHeader1,
            this.columnHeader2});
            this.ScoreList.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreList.GridLines = true;
            this.ScoreList.Location = new System.Drawing.Point(12, 38);
            this.ScoreList.Name = "ScoreList";
            this.ScoreList.Scrollable = false;
            this.ScoreList.Size = new System.Drawing.Size(491, 289);
            this.ScoreList.TabIndex = 3;
            this.ScoreList.UseCompatibleStateImageBehavior = false;
            this.ScoreList.View = System.Windows.Forms.View.Details;
            this.ScoreList.SelectedIndexChanged += new System.EventHandler(this.ScoreList_SelectedIndexChanged);
            // 
            // columnHeader1
            // 
            this.columnHeader1.Text = "Name";
            this.columnHeader1.Width = 328;
            // 
            // columnHeader2
            // 
            this.columnHeader2.Text = "Score";
            this.columnHeader2.Width = 66;
            // 
            // HighScores
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(515, 341);
            this.Controls.Add(this.ScoreList);
            this.Controls.Add(this.ScoreTabs);
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "HighScores";
            this.ShowIcon = false;
            this.ShowInTaskbar = false;
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterParent;
            this.Text = "High Scores";
            this.Load += new System.EventHandler(this.HighScores_Load);
            this.ScoreTabs.ResumeLayout(false);
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.TabControl ScoreTabs;
        private System.Windows.Forms.TabPage VH;
        private System.Windows.Forms.TabPage SP;
        private System.Windows.Forms.TabPage TTG;
        private System.Windows.Forms.ListView ScoreList;
        private System.Windows.Forms.ColumnHeader columnHeader1;
        private System.Windows.Forms.ColumnHeader columnHeader2;
        private System.Windows.Forms.TabPage GWB0;
        private System.Windows.Forms.TabPage TTG_LI;
        private System.Windows.Forms.TabPage GWB1;
        private System.Windows.Forms.TabPage GWB2;


    }
}