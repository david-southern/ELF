namespace ELF
{
    partial class MainMenu
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(MainMenu));
            this.HighScores = new System.Windows.Forms.Button();
            this.MainPanel = new System.Windows.Forms.Panel();
            this.label4 = new System.Windows.Forms.Label();
            this.label3 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.label1 = new System.Windows.Forms.Label();
            this.linkLabel1 = new System.Windows.Forms.LinkLabel();
            this.AboutELF = new System.Windows.Forms.Button();
            this.Rules = new System.Windows.Forms.Button();
            this.VH_Pic_Button = new System.Windows.Forms.PictureBox();
            this.GWB_Pic_Button = new System.Windows.Forms.PictureBox();
            this.SP_Pic_Button = new System.Windows.Forms.PictureBox();
            this.TTG_Pic_Button = new System.Windows.Forms.PictureBox();
            this.MainPanel.SuspendLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VH_Pic_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GWB_Pic_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_Pic_Button)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.TTG_Pic_Button)).BeginInit();
            this.SuspendLayout();
            // 
            // HighScores
            // 
            this.HighScores.Location = new System.Drawing.Point(830, 577);
            this.HighScores.Name = "HighScores";
            this.HighScores.Size = new System.Drawing.Size(92, 29);
            this.HighScores.TabIndex = 13;
            this.HighScores.Text = "High Scores";
            this.HighScores.UseVisualStyleBackColor = true;
            this.HighScores.Click += new System.EventHandler(this.HighScores_Click);
            // 
            // MainPanel
            // 
            this.MainPanel.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("MainPanel.BackgroundImage")));
            this.MainPanel.Controls.Add(this.label4);
            this.MainPanel.Controls.Add(this.label3);
            this.MainPanel.Controls.Add(this.label2);
            this.MainPanel.Controls.Add(this.label1);
            this.MainPanel.Controls.Add(this.linkLabel1);
            this.MainPanel.Controls.Add(this.AboutELF);
            this.MainPanel.Controls.Add(this.Rules);
            this.MainPanel.Controls.Add(this.VH_Pic_Button);
            this.MainPanel.Controls.Add(this.GWB_Pic_Button);
            this.MainPanel.Controls.Add(this.SP_Pic_Button);
            this.MainPanel.Controls.Add(this.TTG_Pic_Button);
            this.MainPanel.Controls.Add(this.HighScores);
            this.MainPanel.Dock = System.Windows.Forms.DockStyle.Fill;
            this.MainPanel.Location = new System.Drawing.Point(0, 0);
            this.MainPanel.Name = "MainPanel";
            this.MainPanel.Size = new System.Drawing.Size(934, 701);
            this.MainPanel.TabIndex = 30;
            // 
            // label4
            // 
            this.label4.AutoSize = true;
            this.label4.BackColor = System.Drawing.Color.White;
            this.label4.Font = new System.Drawing.Font("Arial", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label4.Location = new System.Drawing.Point(190, 65);
            this.label4.Name = "label4";
            this.label4.Size = new System.Drawing.Size(555, 22);
            this.label4.TabIndex = 39;
            this.label4.Text = "brought to you by English Language Fundamentals - ELF © 2009";
            // 
            // label3
            // 
            this.label3.AutoSize = true;
            this.label3.BackColor = System.Drawing.Color.White;
            this.label3.Font = new System.Drawing.Font("Cooper Blk BT", 33.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label3.Location = new System.Drawing.Point(218, 14);
            this.label3.Name = "label3";
            this.label3.Size = new System.Drawing.Size(499, 55);
            this.label3.TabIndex = 38;
            this.label3.Text = "ELF Phonics Games";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.BackColor = System.Drawing.Color.White;
            this.label2.Font = new System.Drawing.Font("Cooper Blk BT", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label2.Location = new System.Drawing.Point(430, 96);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(386, 24);
            this.label2.TabIndex = 37;
            this.label2.Text = "for more spelling and writing fun!";
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.BackColor = System.Drawing.Color.White;
            this.label1.Font = new System.Drawing.Font("Cooper Blk BT", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.label1.Location = new System.Drawing.Point(118, 96);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(121, 24);
            this.label1.TabIndex = 36;
            this.label1.Text = "Visit us at";
            // 
            // linkLabel1
            // 
            this.linkLabel1.AutoSize = true;
            this.linkLabel1.BackColor = System.Drawing.Color.White;
            this.linkLabel1.Font = new System.Drawing.Font("Cooper Blk BT", 15F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.linkLabel1.Location = new System.Drawing.Point(235, 96);
            this.linkLabel1.Name = "linkLabel1";
            this.linkLabel1.Size = new System.Drawing.Size(200, 24);
            this.linkLabel1.TabIndex = 35;
            this.linkLabel1.TabStop = true;
            this.linkLabel1.Text = "www.e-l-fun.com";
            this.linkLabel1.LinkClicked += new System.Windows.Forms.LinkLabelLinkClickedEventHandler(this.linkLabel1_LinkClicked);
            // 
            // AboutELF
            // 
            this.AboutELF.Location = new System.Drawing.Point(830, 660);
            this.AboutELF.Name = "AboutELF";
            this.AboutELF.Size = new System.Drawing.Size(92, 29);
            this.AboutELF.TabIndex = 34;
            this.AboutELF.Text = "About ELF";
            this.AboutELF.UseVisualStyleBackColor = true;
            this.AboutELF.Click += new System.EventHandler(this.AboutELF_Click);
            // 
            // Rules
            // 
            this.Rules.Location = new System.Drawing.Point(830, 612);
            this.Rules.Name = "Rules";
            this.Rules.Size = new System.Drawing.Size(92, 42);
            this.Rules.TabIndex = 33;
            this.Rules.Text = "Game Instructions";
            this.Rules.UseVisualStyleBackColor = true;
            this.Rules.Click += new System.EventHandler(this.Rules_Click);
            // 
            // VH_Pic_Button
            // 
            this.VH_Pic_Button.BackColor = System.Drawing.Color.Transparent;
            this.VH_Pic_Button.Location = new System.Drawing.Point(486, 264);
            this.VH_Pic_Button.Name = "VH_Pic_Button";
            this.VH_Pic_Button.Size = new System.Drawing.Size(344, 108);
            this.VH_Pic_Button.TabIndex = 32;
            this.VH_Pic_Button.TabStop = false;
            this.VH_Pic_Button.Tag = "0";
            this.VH_Pic_Button.Click += new System.EventHandler(this.GameSelectionClick);
            // 
            // GWB_Pic_Button
            // 
            this.GWB_Pic_Button.BackColor = System.Drawing.Color.Transparent;
            this.GWB_Pic_Button.Location = new System.Drawing.Point(107, 264);
            this.GWB_Pic_Button.Name = "GWB_Pic_Button";
            this.GWB_Pic_Button.Size = new System.Drawing.Size(344, 108);
            this.GWB_Pic_Button.TabIndex = 31;
            this.GWB_Pic_Button.TabStop = false;
            this.GWB_Pic_Button.Tag = "3";
            this.GWB_Pic_Button.Click += new System.EventHandler(this.GameSelectionClick);
            // 
            // SP_Pic_Button
            // 
            this.SP_Pic_Button.BackColor = System.Drawing.Color.Transparent;
            this.SP_Pic_Button.Location = new System.Drawing.Point(489, 140);
            this.SP_Pic_Button.Name = "SP_Pic_Button";
            this.SP_Pic_Button.Size = new System.Drawing.Size(344, 108);
            this.SP_Pic_Button.TabIndex = 30;
            this.SP_Pic_Button.TabStop = false;
            this.SP_Pic_Button.Tag = "1";
            this.SP_Pic_Button.Click += new System.EventHandler(this.GameSelectionClick);
            // 
            // TTG_Pic_Button
            // 
            this.TTG_Pic_Button.BackColor = System.Drawing.Color.Transparent;
            this.TTG_Pic_Button.Location = new System.Drawing.Point(107, 140);
            this.TTG_Pic_Button.Name = "TTG_Pic_Button";
            this.TTG_Pic_Button.Size = new System.Drawing.Size(344, 108);
            this.TTG_Pic_Button.TabIndex = 0;
            this.TTG_Pic_Button.TabStop = false;
            this.TTG_Pic_Button.Tag = "2";
            this.TTG_Pic_Button.Click += new System.EventHandler(this.GameSelectionClick);
            // 
            // MainMenu
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.ClientSize = new System.Drawing.Size(934, 701);
            this.Controls.Add(this.MainPanel);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "MainMenu";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "English Language Fundamentals";
            this.MainPanel.ResumeLayout(false);
            this.MainPanel.PerformLayout();
            ((System.ComponentModel.ISupportInitialize)(this.VH_Pic_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GWB_Pic_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.SP_Pic_Button)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.TTG_Pic_Button)).EndInit();
            this.ResumeLayout(false);

        }

        #endregion

        private System.Windows.Forms.Button HighScores;
        private System.Windows.Forms.Panel MainPanel;
        private System.Windows.Forms.PictureBox TTG_Pic_Button;
        private System.Windows.Forms.PictureBox VH_Pic_Button;
        private System.Windows.Forms.PictureBox GWB_Pic_Button;
        private System.Windows.Forms.PictureBox SP_Pic_Button;
        private System.Windows.Forms.Button AboutELF;
        private System.Windows.Forms.Button Rules;
        private System.Windows.Forms.LinkLabel linkLabel1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label4;
        private System.Windows.Forms.Label label3;
    }
}
