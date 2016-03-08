using System.IO;
using System.Windows.Forms;
namespace ELF
{
    partial class VowelHowlForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(VowelHowlForm));
            this.m_instructionsLbl = new System.Windows.Forms.Label();
            this.Roll = new System.Windows.Forms.PictureBox();
            this.Hold1 = new System.Windows.Forms.PictureBox();
            this.m_rollNumberLbl = new System.Windows.Forms.Label();
            this.ScoreHundreds = new System.Windows.Forms.Label();
            this.fileToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.newGameToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.vowelHowelToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.shortVowelHowlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.variedVowelHowlToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consonantRoll1ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consonantRoll2ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consonantRoll3ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.consonantRoll4ToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.optionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.instructionsToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.exitToolStripMenuItem = new System.Windows.Forms.ToolStripMenuItem();
            this.Speak1 = new System.Windows.Forms.Button();
            this.Speak2 = new System.Windows.Forms.Button();
            this.Speak3 = new System.Windows.Forms.Button();
            this.Speak4 = new System.Windows.Forms.Button();
            this.Speak5 = new System.Windows.Forms.Button();
            this.Score1 = new System.Windows.Forms.PictureBox();
            this.Score2 = new System.Windows.Forms.PictureBox();
            this.Score3 = new System.Windows.Forms.PictureBox();
            this.Score4 = new System.Windows.Forms.PictureBox();
            this.Score5 = new System.Windows.Forms.PictureBox();
            this.ScoreButton1 = new System.Windows.Forms.PictureBox();
            this.Score6 = new System.Windows.Forms.PictureBox();
            this.Score8 = new System.Windows.Forms.PictureBox();
            this.Score7 = new System.Windows.Forms.PictureBox();
            this.Score9 = new System.Windows.Forms.PictureBox();
            this.Score10 = new System.Windows.Forms.PictureBox();
            this.Score11 = new System.Windows.Forms.PictureBox();
            this.Score12 = new System.Windows.Forms.PictureBox();
            this.button1 = new System.Windows.Forms.Button();
            this.button2 = new System.Windows.Forms.Button();
            this.Dice1 = new System.Windows.Forms.PictureBox();
            this.Dice2 = new System.Windows.Forms.PictureBox();
            this.Dice3 = new System.Windows.Forms.PictureBox();
            this.Dice4 = new System.Windows.Forms.PictureBox();
            this.Dice5 = new System.Windows.Forms.PictureBox();
            this.ScoreTens = new System.Windows.Forms.Label();
            this.ScoreOnes = new System.Windows.Forms.Label();
            this.TopScoreOnes = new System.Windows.Forms.Label();
            this.TopScoreTens = new System.Windows.Forms.Label();
            this.TopScoreHundreds = new System.Windows.Forms.Label();
            this.BonusScoreOnes = new System.Windows.Forms.Label();
            this.BonusScoreTens = new System.Windows.Forms.Label();
            this.button9 = new System.Windows.Forms.Button();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.button3 = new System.Windows.Forms.Button();
            this.button4 = new System.Windows.Forms.Button();
            this.button5 = new System.Windows.Forms.Button();
            this.button6 = new System.Windows.Forms.Button();
            this.button7 = new System.Windows.Forms.Button();
            this.MenuPictureBox = new System.Windows.Forms.PictureBox();
            this.button8 = new System.Windows.Forms.Button();
            this.TwoPairPoints = new System.Windows.Forms.Label();
            this.FullHoursPoints = new System.Windows.Forms.Label();
            this.FourDifferentPoints = new System.Windows.Forms.Label();
            this.FiveDifferentPoints = new System.Windows.Forms.Label();
            this.VowelHowlPoints = new System.Windows.Forms.Label();
            this.ThreeKindPoints = new System.Windows.Forms.Label();
            this.FourKindPoints = new System.Windows.Forms.Label();
            this.Hold2 = new System.Windows.Forms.PictureBox();
            this.Hold3 = new System.Windows.Forms.PictureBox();
            this.Hold4 = new System.Windows.Forms.PictureBox();
            this.Hold5 = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.Roll)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScoreButton1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score10)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score11)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score12)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuPictureBox)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold5)).BeginInit();
            this.SuspendLayout();
            // 
            // m_instructionsLbl
            // 
            this.m_instructionsLbl.BackColor = System.Drawing.Color.Transparent;
            this.m_instructionsLbl.Font = new System.Drawing.Font("Century Gothic", 14F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_instructionsLbl.ForeColor = System.Drawing.Color.Black;
            this.m_instructionsLbl.Location = new System.Drawing.Point(211, 101);
            this.m_instructionsLbl.Name = "m_instructionsLbl";
            this.m_instructionsLbl.Size = new System.Drawing.Size(589, 35);
            this.m_instructionsLbl.TabIndex = 18;
            this.m_instructionsLbl.Text = "Roll the dice to begin the game!";
            this.m_instructionsLbl.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // Roll
            // 
            this.Roll.BackColor = System.Drawing.Color.Transparent;
            this.Roll.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Stretch;
            this.Roll.InitialImage = ((System.Drawing.Image)(resources.GetObject("Roll.InitialImage")));
            this.Roll.Location = new System.Drawing.Point(281, 591);
            this.Roll.Margin = new System.Windows.Forms.Padding(30);
            this.Roll.Name = "Roll";
            this.Roll.Size = new System.Drawing.Size(339, 59);
            this.Roll.TabIndex = 20;
            this.Roll.TabStop = false;
            this.Roll.MouseLeave += new System.EventHandler(this.Roll_MouseLeave);
            this.Roll.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Roll_MouseUp);
            this.Roll.MouseEnter += new System.EventHandler(this.Roll_MouseEnter);
            // 
            // Hold1
            // 
            this.Hold1.BackColor = System.Drawing.Color.Transparent;
            this.Hold1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Hold1.Image = global::ELF_Resources.Properties.Resources.HOLD;
            this.Hold1.Location = new System.Drawing.Point(141, 215);
            this.Hold1.Name = "Hold1";
            this.Hold1.Size = new System.Drawing.Size(165, 165);
            this.Hold1.TabIndex = 39;
            this.Hold1.TabStop = false;
            this.Hold1.Tag = "0";
            this.Hold1.Visible = false;
            this.Hold1.MouseLeave += new System.EventHandler(this.Hold_MouseLeave);
            this.Hold1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Hold_MouseUp);
            this.Hold1.MouseEnter += new System.EventHandler(this.Hold_MouseEnter);
            // 
            // m_rollNumberLbl
            // 
            this.m_rollNumberLbl.AutoSize = true;
            this.m_rollNumberLbl.BackColor = System.Drawing.Color.Transparent;
            this.m_rollNumberLbl.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.m_rollNumberLbl.Location = new System.Drawing.Point(650, 415);
            this.m_rollNumberLbl.Name = "m_rollNumberLbl";
            this.m_rollNumberLbl.Size = new System.Drawing.Size(24, 27);
            this.m_rollNumberLbl.TabIndex = 55;
            this.m_rollNumberLbl.Text = "0";
            // 
            // ScoreHundreds
            // 
            this.ScoreHundreds.AutoSize = true;
            this.ScoreHundreds.BackColor = System.Drawing.Color.Transparent;
            this.ScoreHundreds.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreHundreds.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ScoreHundreds.Location = new System.Drawing.Point(649, 526);
            this.ScoreHundreds.Name = "ScoreHundreds";
            this.ScoreHundreds.Size = new System.Drawing.Size(40, 45);
            this.ScoreHundreds.TabIndex = 57;
            this.ScoreHundreds.Text = "0";
            this.ScoreHundreds.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // fileToolStripMenuItem
            // 
            this.fileToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.newGameToolStripMenuItem,
            this.optionsToolStripMenuItem,
            this.instructionsToolStripMenuItem,
            this.exitToolStripMenuItem});
            this.fileToolStripMenuItem.Name = "fileToolStripMenuItem";
            this.fileToolStripMenuItem.Size = new System.Drawing.Size(50, 20);
            this.fileToolStripMenuItem.Text = "Menu";
            // 
            // newGameToolStripMenuItem
            // 
            this.newGameToolStripMenuItem.DropDownItems.AddRange(new System.Windows.Forms.ToolStripItem[] {
            this.vowelHowelToolStripMenuItem,
            this.shortVowelHowlToolStripMenuItem,
            this.variedVowelHowlToolStripMenuItem,
            this.consonantRoll1ToolStripMenuItem,
            this.consonantRoll2ToolStripMenuItem,
            this.consonantRoll3ToolStripMenuItem,
            this.consonantRoll4ToolStripMenuItem});
            this.newGameToolStripMenuItem.Name = "newGameToolStripMenuItem";
            this.newGameToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.newGameToolStripMenuItem.Text = "New Game...";
            // 
            // vowelHowelToolStripMenuItem
            // 
            this.vowelHowelToolStripMenuItem.Name = "vowelHowelToolStripMenuItem";
            this.vowelHowelToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.vowelHowelToolStripMenuItem.Text = "Long Vowel Howl";
            // 
            // shortVowelHowlToolStripMenuItem
            // 
            this.shortVowelHowlToolStripMenuItem.Name = "shortVowelHowlToolStripMenuItem";
            this.shortVowelHowlToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.shortVowelHowlToolStripMenuItem.Text = "Short Vowel Howl";
            // 
            // variedVowelHowlToolStripMenuItem
            // 
            this.variedVowelHowlToolStripMenuItem.Name = "variedVowelHowlToolStripMenuItem";
            this.variedVowelHowlToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.variedVowelHowlToolStripMenuItem.Text = "Varied Vowel Howl";
            // 
            // consonantRoll1ToolStripMenuItem
            // 
            this.consonantRoll1ToolStripMenuItem.Name = "consonantRoll1ToolStripMenuItem";
            this.consonantRoll1ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.consonantRoll1ToolStripMenuItem.Text = "Phonics Roll 1 ";
            // 
            // consonantRoll2ToolStripMenuItem
            // 
            this.consonantRoll2ToolStripMenuItem.Name = "consonantRoll2ToolStripMenuItem";
            this.consonantRoll2ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.consonantRoll2ToolStripMenuItem.Text = "Phonics Roll 2";
            // 
            // consonantRoll3ToolStripMenuItem
            // 
            this.consonantRoll3ToolStripMenuItem.Name = "consonantRoll3ToolStripMenuItem";
            this.consonantRoll3ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.consonantRoll3ToolStripMenuItem.Text = "Phonics Roll 3";
            // 
            // consonantRoll4ToolStripMenuItem
            // 
            this.consonantRoll4ToolStripMenuItem.Name = "consonantRoll4ToolStripMenuItem";
            this.consonantRoll4ToolStripMenuItem.Size = new System.Drawing.Size(173, 22);
            this.consonantRoll4ToolStripMenuItem.Text = "Phonics Roll 4";
            // 
            // optionsToolStripMenuItem
            // 
            this.optionsToolStripMenuItem.Name = "optionsToolStripMenuItem";
            this.optionsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.optionsToolStripMenuItem.Text = "Options";
            // 
            // instructionsToolStripMenuItem
            // 
            this.instructionsToolStripMenuItem.Name = "instructionsToolStripMenuItem";
            this.instructionsToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.instructionsToolStripMenuItem.Text = "Game Rules";
            // 
            // exitToolStripMenuItem
            // 
            this.exitToolStripMenuItem.Name = "exitToolStripMenuItem";
            this.exitToolStripMenuItem.Size = new System.Drawing.Size(141, 22);
            this.exitToolStripMenuItem.Text = "Exit";
            // 
            // Speak1
            // 
            this.Speak1.BackColor = System.Drawing.Color.White;
            this.Speak1.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Speak1.FlatAppearance.BorderSize = 0;
            this.Speak1.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Speak1.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.Speak1.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Speak1.ForeColor = System.Drawing.Color.Transparent;
            this.Speak1.Image = ((System.Drawing.Image)(resources.GetObject("Speak1.Image")));
            this.Speak1.Location = new System.Drawing.Point(261, 220);
            this.Speak1.Margin = new System.Windows.Forms.Padding(10);
            this.Speak1.Name = "Speak1";
            this.Speak1.Size = new System.Drawing.Size(38, 38);
            this.Speak1.TabIndex = 59;
            this.Speak1.TabStop = false;
            this.Speak1.UseMnemonic = false;
            this.Speak1.UseVisualStyleBackColor = false;
            this.Speak1.Click += new System.EventHandler(this.Speak_Click);
            // 
            // Speak2
            // 
            this.Speak2.BackColor = System.Drawing.Color.White;
            this.Speak2.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Speak2.FlatAppearance.BorderSize = 0;
            this.Speak2.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Speak2.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.Speak2.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Speak2.Image = ((System.Drawing.Image)(resources.GetObject("Speak2.Image")));
            this.Speak2.Location = new System.Drawing.Point(490, 220);
            this.Speak2.Margin = new System.Windows.Forms.Padding(10);
            this.Speak2.Name = "Speak2";
            this.Speak2.Size = new System.Drawing.Size(38, 38);
            this.Speak2.TabIndex = 60;
            this.Speak2.TabStop = false;
            this.Speak2.UseMnemonic = false;
            this.Speak2.UseVisualStyleBackColor = false;
            this.Speak2.Click += new System.EventHandler(this.Speak_Click);
            // 
            // Speak3
            // 
            this.Speak3.BackColor = System.Drawing.Color.White;
            this.Speak3.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Speak3.FlatAppearance.BorderSize = 0;
            this.Speak3.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Speak3.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.Speak3.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Speak3.Image = ((System.Drawing.Image)(resources.GetObject("Speak3.Image")));
            this.Speak3.Location = new System.Drawing.Point(718, 220);
            this.Speak3.Margin = new System.Windows.Forms.Padding(10);
            this.Speak3.Name = "Speak3";
            this.Speak3.Size = new System.Drawing.Size(38, 38);
            this.Speak3.TabIndex = 61;
            this.Speak3.TabStop = false;
            this.Speak3.UseMnemonic = false;
            this.Speak3.UseVisualStyleBackColor = false;
            this.Speak3.Click += new System.EventHandler(this.Speak_Click);
            // 
            // Speak4
            // 
            this.Speak4.BackColor = System.Drawing.Color.White;
            this.Speak4.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Speak4.FlatAppearance.BorderSize = 0;
            this.Speak4.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Speak4.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.Speak4.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Speak4.Image = ((System.Drawing.Image)(resources.GetObject("Speak4.Image")));
            this.Speak4.Location = new System.Drawing.Point(261, 414);
            this.Speak4.Margin = new System.Windows.Forms.Padding(10);
            this.Speak4.Name = "Speak4";
            this.Speak4.Size = new System.Drawing.Size(38, 38);
            this.Speak4.TabIndex = 62;
            this.Speak4.TabStop = false;
            this.Speak4.UseMnemonic = false;
            this.Speak4.UseVisualStyleBackColor = false;
            this.Speak4.Click += new System.EventHandler(this.Speak_Click);
            // 
            // Speak5
            // 
            this.Speak5.BackColor = System.Drawing.Color.White;
            this.Speak5.FlatAppearance.BorderColor = System.Drawing.Color.White;
            this.Speak5.FlatAppearance.BorderSize = 0;
            this.Speak5.FlatAppearance.MouseDownBackColor = System.Drawing.Color.White;
            this.Speak5.FlatAppearance.MouseOverBackColor = System.Drawing.Color.White;
            this.Speak5.FlatStyle = System.Windows.Forms.FlatStyle.Flat;
            this.Speak5.Image = ((System.Drawing.Image)(resources.GetObject("Speak5.Image")));
            this.Speak5.Location = new System.Drawing.Point(490, 415);
            this.Speak5.Margin = new System.Windows.Forms.Padding(10);
            this.Speak5.Name = "Speak5";
            this.Speak5.Size = new System.Drawing.Size(38, 38);
            this.Speak5.TabIndex = 63;
            this.Speak5.TabStop = false;
            this.Speak5.UseMnemonic = false;
            this.Speak5.UseVisualStyleBackColor = false;
            this.Speak5.Click += new System.EventHandler(this.Speak_Click);
            // 
            // Score1
            // 
            this.Score1.BackColor = System.Drawing.Color.Transparent;
            this.Score1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score1.Image = ((System.Drawing.Image)(resources.GetObject("Score1.Image")));
            this.Score1.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score1.InitialImage")));
            this.Score1.Location = new System.Drawing.Point(100, 143);
            this.Score1.Margin = new System.Windows.Forms.Padding(5);
            this.Score1.Name = "Score1";
            this.Score1.Size = new System.Drawing.Size(51, 49);
            this.Score1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score1.TabIndex = 64;
            this.Score1.TabStop = false;
            this.Score1.Tag = "0";
            this.Score1.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score1.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score2
            // 
            this.Score2.BackColor = System.Drawing.Color.Transparent;
            this.Score2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score2.Image = ((System.Drawing.Image)(resources.GetObject("Score2.Image")));
            this.Score2.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score2.InitialImage")));
            this.Score2.Location = new System.Drawing.Point(159, 143);
            this.Score2.Margin = new System.Windows.Forms.Padding(5);
            this.Score2.Name = "Score2";
            this.Score2.Size = new System.Drawing.Size(51, 49);
            this.Score2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score2.TabIndex = 65;
            this.Score2.TabStop = false;
            this.Score2.Tag = "1";
            this.Score2.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score2.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score3
            // 
            this.Score3.BackColor = System.Drawing.Color.Transparent;
            this.Score3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score3.Image = ((System.Drawing.Image)(resources.GetObject("Score3.Image")));
            this.Score3.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score3.InitialImage")));
            this.Score3.Location = new System.Drawing.Point(218, 143);
            this.Score3.Margin = new System.Windows.Forms.Padding(5);
            this.Score3.Name = "Score3";
            this.Score3.Size = new System.Drawing.Size(51, 49);
            this.Score3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score3.TabIndex = 66;
            this.Score3.TabStop = false;
            this.Score3.Tag = "2";
            this.Score3.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score3.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score4
            // 
            this.Score4.BackColor = System.Drawing.Color.Transparent;
            this.Score4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score4.Image = ((System.Drawing.Image)(resources.GetObject("Score4.Image")));
            this.Score4.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score4.InitialImage")));
            this.Score4.Location = new System.Drawing.Point(277, 143);
            this.Score4.Margin = new System.Windows.Forms.Padding(5);
            this.Score4.Name = "Score4";
            this.Score4.Size = new System.Drawing.Size(51, 49);
            this.Score4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score4.TabIndex = 67;
            this.Score4.TabStop = false;
            this.Score4.Tag = "3";
            this.Score4.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score4.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score5
            // 
            this.Score5.BackColor = System.Drawing.Color.Transparent;
            this.Score5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score5.Image = ((System.Drawing.Image)(resources.GetObject("Score5.Image")));
            this.Score5.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score5.InitialImage")));
            this.Score5.Location = new System.Drawing.Point(336, 143);
            this.Score5.Margin = new System.Windows.Forms.Padding(5);
            this.Score5.Name = "Score5";
            this.Score5.Size = new System.Drawing.Size(51, 49);
            this.Score5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score5.TabIndex = 68;
            this.Score5.TabStop = false;
            this.Score5.Tag = "4";
            this.Score5.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score5.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // ScoreButton1
            // 
            this.ScoreButton1.BackColor = System.Drawing.Color.Transparent;
            this.ScoreButton1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.ScoreButton1.Image = ((System.Drawing.Image)(resources.GetObject("ScoreButton1.Image")));
            this.ScoreButton1.InitialImage = ((System.Drawing.Image)(resources.GetObject("ScoreButton1.InitialImage")));
            this.ScoreButton1.Location = new System.Drawing.Point(119, 165);
            this.ScoreButton1.Margin = new System.Windows.Forms.Padding(5);
            this.ScoreButton1.Name = "ScoreButton1";
            this.ScoreButton1.Size = new System.Drawing.Size(51, 49);
            this.ScoreButton1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.ScoreButton1.TabIndex = 64;
            this.ScoreButton1.TabStop = false;
            // 
            // Score6
            // 
            this.Score6.BackColor = System.Drawing.Color.Transparent;
            this.Score6.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score6.Image = ((System.Drawing.Image)(resources.GetObject("Score6.Image")));
            this.Score6.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score6.InitialImage")));
            this.Score6.Location = new System.Drawing.Point(395, 143);
            this.Score6.Margin = new System.Windows.Forms.Padding(5);
            this.Score6.Name = "Score6";
            this.Score6.Size = new System.Drawing.Size(51, 49);
            this.Score6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score6.TabIndex = 69;
            this.Score6.TabStop = false;
            this.Score6.Tag = "5";
            this.Score6.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score6.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score6.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score8
            // 
            this.Score8.BackColor = System.Drawing.Color.Transparent;
            this.Score8.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score8.Image = ((System.Drawing.Image)(resources.GetObject("Score8.Image")));
            this.Score8.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score8.InitialImage")));
            this.Score8.Location = new System.Drawing.Point(572, 143);
            this.Score8.Margin = new System.Windows.Forms.Padding(5);
            this.Score8.Name = "Score8";
            this.Score8.Size = new System.Drawing.Size(51, 49);
            this.Score8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score8.TabIndex = 70;
            this.Score8.TabStop = false;
            this.Score8.Tag = "7";
            this.Score8.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score8.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score8.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score7
            // 
            this.Score7.BackColor = System.Drawing.Color.Transparent;
            this.Score7.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score7.Image = ((System.Drawing.Image)(resources.GetObject("Score7.Image")));
            this.Score7.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score7.InitialImage")));
            this.Score7.Location = new System.Drawing.Point(454, 143);
            this.Score7.Margin = new System.Windows.Forms.Padding(5);
            this.Score7.Name = "Score7";
            this.Score7.Size = new System.Drawing.Size(51, 49);
            this.Score7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score7.TabIndex = 71;
            this.Score7.TabStop = false;
            this.Score7.Tag = "6";
            this.Score7.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score7.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score7.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score9
            // 
            this.Score9.BackColor = System.Drawing.Color.Transparent;
            this.Score9.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score9.Image = ((System.Drawing.Image)(resources.GetObject("Score9.Image")));
            this.Score9.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score9.InitialImage")));
            this.Score9.Location = new System.Drawing.Point(513, 143);
            this.Score9.Margin = new System.Windows.Forms.Padding(5);
            this.Score9.Name = "Score9";
            this.Score9.Size = new System.Drawing.Size(51, 49);
            this.Score9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score9.TabIndex = 72;
            this.Score9.TabStop = false;
            this.Score9.Tag = "8";
            this.Score9.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score9.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score9.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score10
            // 
            this.Score10.BackColor = System.Drawing.Color.Transparent;
            this.Score10.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score10.Image = ((System.Drawing.Image)(resources.GetObject("Score10.Image")));
            this.Score10.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score10.InitialImage")));
            this.Score10.Location = new System.Drawing.Point(631, 143);
            this.Score10.Margin = new System.Windows.Forms.Padding(5);
            this.Score10.Name = "Score10";
            this.Score10.Size = new System.Drawing.Size(51, 49);
            this.Score10.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score10.TabIndex = 73;
            this.Score10.TabStop = false;
            this.Score10.Tag = "9";
            this.Score10.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score10.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score10.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score11
            // 
            this.Score11.BackColor = System.Drawing.Color.Transparent;
            this.Score11.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score11.Image = ((System.Drawing.Image)(resources.GetObject("Score11.Image")));
            this.Score11.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score11.InitialImage")));
            this.Score11.Location = new System.Drawing.Point(690, 143);
            this.Score11.Margin = new System.Windows.Forms.Padding(5);
            this.Score11.Name = "Score11";
            this.Score11.Size = new System.Drawing.Size(51, 49);
            this.Score11.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score11.TabIndex = 74;
            this.Score11.TabStop = false;
            this.Score11.Tag = "10";
            this.Score11.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score11.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score11.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // Score12
            // 
            this.Score12.BackColor = System.Drawing.Color.Transparent;
            this.Score12.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Score12.Image = ((System.Drawing.Image)(resources.GetObject("Score12.Image")));
            this.Score12.InitialImage = ((System.Drawing.Image)(resources.GetObject("Score12.InitialImage")));
            this.Score12.Location = new System.Drawing.Point(749, 143);
            this.Score12.Margin = new System.Windows.Forms.Padding(5);
            this.Score12.Name = "Score12";
            this.Score12.Size = new System.Drawing.Size(51, 49);
            this.Score12.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Score12.TabIndex = 75;
            this.Score12.TabStop = false;
            this.Score12.Tag = "11";
            this.Score12.MouseLeave += new System.EventHandler(this.score_MouseLeave);
            this.Score12.MouseUp += new System.Windows.Forms.MouseEventHandler(this.score_MouseUp);
            this.Score12.MouseEnter += new System.EventHandler(this.score_MouseEnter);
            // 
            // button1
            // 
            this.button1.Location = new System.Drawing.Point(12, 416);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(75, 23);
            this.button1.TabIndex = 76;
            this.button1.Tag = "0";
            this.button1.Text = "Long";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Visible = false;
            this.button1.Click += new System.EventHandler(this.StartGame);
            // 
            // button2
            // 
            this.button2.Location = new System.Drawing.Point(12, 445);
            this.button2.Name = "button2";
            this.button2.Size = new System.Drawing.Size(75, 23);
            this.button2.TabIndex = 77;
            this.button2.Tag = "1";
            this.button2.Text = "Short";
            this.button2.UseVisualStyleBackColor = true;
            this.button2.Visible = false;
            this.button2.Click += new System.EventHandler(this.StartGame);
            // 
            // Dice1
            // 
            this.Dice1.BackColor = System.Drawing.Color.Transparent;
            this.Dice1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Dice1.InitialImage = null;
            this.Dice1.Location = new System.Drawing.Point(141, 215);
            this.Dice1.Margin = new System.Windows.Forms.Padding(5);
            this.Dice1.Name = "Dice1";
            this.Dice1.Size = new System.Drawing.Size(165, 165);
            this.Dice1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Dice1.TabIndex = 78;
            this.Dice1.TabStop = false;
            this.Dice1.Tag = "0";
            this.Dice1.MouseLeave += new System.EventHandler(this.Dice_MouseLeave);
            this.Dice1.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dice_MouseUp);
            this.Dice1.MouseEnter += new System.EventHandler(this.Dice_MouseEnter);
            // 
            // Dice2
            // 
            this.Dice2.BackColor = System.Drawing.Color.Transparent;
            this.Dice2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Dice2.InitialImage = null;
            this.Dice2.Location = new System.Drawing.Point(370, 215);
            this.Dice2.Margin = new System.Windows.Forms.Padding(5);
            this.Dice2.Name = "Dice2";
            this.Dice2.Size = new System.Drawing.Size(165, 165);
            this.Dice2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Dice2.TabIndex = 79;
            this.Dice2.TabStop = false;
            this.Dice2.Tag = "1";
            this.Dice2.MouseLeave += new System.EventHandler(this.Dice_MouseLeave);
            this.Dice2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dice_MouseUp);
            this.Dice2.MouseEnter += new System.EventHandler(this.Dice_MouseEnter);
            // 
            // Dice3
            // 
            this.Dice3.BackColor = System.Drawing.Color.Transparent;
            this.Dice3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Dice3.InitialImage = null;
            this.Dice3.Location = new System.Drawing.Point(598, 215);
            this.Dice3.Margin = new System.Windows.Forms.Padding(5);
            this.Dice3.Name = "Dice3";
            this.Dice3.Size = new System.Drawing.Size(165, 165);
            this.Dice3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Dice3.TabIndex = 80;
            this.Dice3.TabStop = false;
            this.Dice3.Tag = "2";
            this.Dice3.MouseLeave += new System.EventHandler(this.Dice_MouseLeave);
            this.Dice3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dice_MouseUp);
            this.Dice3.MouseEnter += new System.EventHandler(this.Dice_MouseEnter);
            // 
            // Dice4
            // 
            this.Dice4.BackColor = System.Drawing.Color.Transparent;
            this.Dice4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Dice4.InitialImage = null;
            this.Dice4.Location = new System.Drawing.Point(141, 409);
            this.Dice4.Margin = new System.Windows.Forms.Padding(5);
            this.Dice4.Name = "Dice4";
            this.Dice4.Size = new System.Drawing.Size(165, 165);
            this.Dice4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Dice4.TabIndex = 81;
            this.Dice4.TabStop = false;
            this.Dice4.Tag = "3";
            this.Dice4.MouseLeave += new System.EventHandler(this.Dice_MouseLeave);
            this.Dice4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dice_MouseUp);
            this.Dice4.MouseEnter += new System.EventHandler(this.Dice_MouseEnter);
            // 
            // Dice5
            // 
            this.Dice5.BackColor = System.Drawing.Color.Transparent;
            this.Dice5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Dice5.InitialImage = null;
            this.Dice5.Location = new System.Drawing.Point(370, 409);
            this.Dice5.Margin = new System.Windows.Forms.Padding(5);
            this.Dice5.Name = "Dice5";
            this.Dice5.Size = new System.Drawing.Size(165, 165);
            this.Dice5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.Dice5.TabIndex = 82;
            this.Dice5.TabStop = false;
            this.Dice5.Tag = "4";
            this.Dice5.MouseLeave += new System.EventHandler(this.Dice_MouseLeave);
            this.Dice5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Dice_MouseUp);
            this.Dice5.MouseEnter += new System.EventHandler(this.Dice_MouseEnter);
            // 
            // ScoreTens
            // 
            this.ScoreTens.AutoSize = true;
            this.ScoreTens.BackColor = System.Drawing.Color.Transparent;
            this.ScoreTens.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreTens.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ScoreTens.Location = new System.Drawing.Point(683, 526);
            this.ScoreTens.Name = "ScoreTens";
            this.ScoreTens.Size = new System.Drawing.Size(40, 45);
            this.ScoreTens.TabIndex = 87;
            this.ScoreTens.Text = "0";
            this.ScoreTens.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // ScoreOnes
            // 
            this.ScoreOnes.AutoSize = true;
            this.ScoreOnes.BackColor = System.Drawing.Color.Transparent;
            this.ScoreOnes.Font = new System.Drawing.Font("Comic Sans MS", 24F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreOnes.ImageAlign = System.Drawing.ContentAlignment.TopLeft;
            this.ScoreOnes.Location = new System.Drawing.Point(717, 526);
            this.ScoreOnes.Name = "ScoreOnes";
            this.ScoreOnes.Size = new System.Drawing.Size(40, 45);
            this.ScoreOnes.TabIndex = 88;
            this.ScoreOnes.Text = "0";
            this.ScoreOnes.TextAlign = System.Drawing.ContentAlignment.MiddleRight;
            // 
            // TopScoreOnes
            // 
            this.TopScoreOnes.AutoSize = true;
            this.TopScoreOnes.BackColor = System.Drawing.Color.Transparent;
            this.TopScoreOnes.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopScoreOnes.Location = new System.Drawing.Point(758, 446);
            this.TopScoreOnes.Name = "TopScoreOnes";
            this.TopScoreOnes.Size = new System.Drawing.Size(24, 27);
            this.TopScoreOnes.TabIndex = 89;
            this.TopScoreOnes.Text = "0";
            // 
            // TopScoreTens
            // 
            this.TopScoreTens.AutoSize = true;
            this.TopScoreTens.BackColor = System.Drawing.Color.Transparent;
            this.TopScoreTens.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopScoreTens.Location = new System.Drawing.Point(736, 446);
            this.TopScoreTens.Name = "TopScoreTens";
            this.TopScoreTens.Size = new System.Drawing.Size(24, 27);
            this.TopScoreTens.TabIndex = 90;
            this.TopScoreTens.Text = "0";
            // 
            // TopScoreHundreds
            // 
            this.TopScoreHundreds.AutoSize = true;
            this.TopScoreHundreds.BackColor = System.Drawing.Color.Transparent;
            this.TopScoreHundreds.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TopScoreHundreds.Location = new System.Drawing.Point(714, 446);
            this.TopScoreHundreds.Name = "TopScoreHundreds";
            this.TopScoreHundreds.Size = new System.Drawing.Size(24, 27);
            this.TopScoreHundreds.TabIndex = 91;
            this.TopScoreHundreds.Text = "0";
            // 
            // BonusScoreOnes
            // 
            this.BonusScoreOnes.AutoSize = true;
            this.BonusScoreOnes.BackColor = System.Drawing.Color.Transparent;
            this.BonusScoreOnes.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BonusScoreOnes.Location = new System.Drawing.Point(758, 480);
            this.BonusScoreOnes.Name = "BonusScoreOnes";
            this.BonusScoreOnes.Size = new System.Drawing.Size(24, 27);
            this.BonusScoreOnes.TabIndex = 92;
            this.BonusScoreOnes.Text = "0";
            // 
            // BonusScoreTens
            // 
            this.BonusScoreTens.AutoSize = true;
            this.BonusScoreTens.BackColor = System.Drawing.Color.Transparent;
            this.BonusScoreTens.Font = new System.Drawing.Font("Comic Sans MS", 14F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.BonusScoreTens.Location = new System.Drawing.Point(737, 480);
            this.BonusScoreTens.Name = "BonusScoreTens";
            this.BonusScoreTens.Size = new System.Drawing.Size(24, 27);
            this.BonusScoreTens.TabIndex = 93;
            this.BonusScoreTens.Text = "0";
            // 
            // button9
            // 
            this.button9.Location = new System.Drawing.Point(12, 474);
            this.button9.Name = "button9";
            this.button9.Size = new System.Drawing.Size(75, 23);
            this.button9.TabIndex = 94;
            this.button9.Tag = "2";
            this.button9.Text = "Varied";
            this.button9.UseVisualStyleBackColor = true;
            this.button9.Visible = false;
            this.button9.Click += new System.EventHandler(this.StartGame);
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(12, 400);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(41, 13);
            this.label1.TabIndex = 107;
            this.label1.Text = "Vowels";
            this.label1.Visible = false;
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(12, 528);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(63, 13);
            this.label2.TabIndex = 108;
            this.label2.Text = "Consonants";
            this.label2.Visible = false;
            // 
            // button3
            // 
            this.button3.Location = new System.Drawing.Point(12, 544);
            this.button3.Name = "button3";
            this.button3.Size = new System.Drawing.Size(75, 23);
            this.button3.TabIndex = 109;
            this.button3.Tag = "3";
            this.button3.Text = "DFGMS";
            this.button3.UseVisualStyleBackColor = true;
            this.button3.Visible = false;
            this.button3.Click += new System.EventHandler(this.StartGame);
            // 
            // button4
            // 
            this.button4.Location = new System.Drawing.Point(12, 573);
            this.button4.Name = "button4";
            this.button4.Size = new System.Drawing.Size(75, 23);
            this.button4.TabIndex = 110;
            this.button4.Tag = "4";
            this.button4.Text = "JKNPY";
            this.button4.UseVisualStyleBackColor = true;
            this.button4.Visible = false;
            this.button4.Click += new System.EventHandler(this.StartGame);
            // 
            // button5
            // 
            this.button5.Location = new System.Drawing.Point(12, 602);
            this.button5.Name = "button5";
            this.button5.Size = new System.Drawing.Size(75, 23);
            this.button5.TabIndex = 111;
            this.button5.Tag = "5";
            this.button5.Text = "BCHTW";
            this.button5.UseVisualStyleBackColor = true;
            this.button5.Visible = false;
            this.button5.Click += new System.EventHandler(this.StartGame);
            // 
            // button6
            // 
            this.button6.Location = new System.Drawing.Point(12, 631);
            this.button6.Name = "button6";
            this.button6.Size = new System.Drawing.Size(75, 23);
            this.button6.TabIndex = 112;
            this.button6.Tag = "6";
            this.button6.Text = "LRTVZ";
            this.button6.UseVisualStyleBackColor = true;
            this.button6.Visible = false;
            this.button6.Click += new System.EventHandler(this.StartGame);
            // 
            // button7
            // 
            this.button7.Location = new System.Drawing.Point(12, 660);
            this.button7.Name = "button7";
            this.button7.Size = new System.Drawing.Size(75, 23);
            this.button7.TabIndex = 113;
            this.button7.Tag = "7";
            this.button7.Text = "Digraphs";
            this.button7.UseVisualStyleBackColor = true;
            this.button7.Visible = false;
            this.button7.Click += new System.EventHandler(this.StartGame);
            // 
            // MenuPictureBox
            // 
            this.MenuPictureBox.BackColor = System.Drawing.Color.Transparent;
            this.MenuPictureBox.Location = new System.Drawing.Point(674, 662);
            this.MenuPictureBox.Name = "MenuPictureBox";
            this.MenuPictureBox.Size = new System.Drawing.Size(143, 32);
            this.MenuPictureBox.TabIndex = 114;
            this.MenuPictureBox.TabStop = false;
            this.MenuPictureBox.Click += new System.EventHandler(this.MenuPictureBox_Click);
            // 
            // button8
            // 
            this.button8.Location = new System.Drawing.Point(12, 362);
            this.button8.Name = "button8";
            this.button8.Size = new System.Drawing.Size(75, 23);
            this.button8.TabIndex = 115;
            this.button8.Tag = "0";
            this.button8.Text = "Test Mode";
            this.button8.UseVisualStyleBackColor = true;
            this.button8.Visible = false;
            this.button8.Click += new System.EventHandler(this.button8_Click);
            // 
            // TwoPairPoints
            // 
            this.TwoPairPoints.AutoSize = true;
            this.TwoPairPoints.BackColor = System.Drawing.Color.Transparent;
            this.TwoPairPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TwoPairPoints.Location = new System.Drawing.Point(413, 192);
            this.TwoPairPoints.Name = "TwoPairPoints";
            this.TwoPairPoints.Size = new System.Drawing.Size(15, 12);
            this.TwoPairPoints.TabIndex = 116;
            this.TwoPairPoints.Text = "20";
            // 
            // FullHoursPoints
            // 
            this.FullHoursPoints.AutoSize = true;
            this.FullHoursPoints.BackColor = System.Drawing.Color.Transparent;
            this.FullHoursPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FullHoursPoints.Location = new System.Drawing.Point(590, 192);
            this.FullHoursPoints.Name = "FullHoursPoints";
            this.FullHoursPoints.Size = new System.Drawing.Size(15, 12);
            this.FullHoursPoints.TabIndex = 117;
            this.FullHoursPoints.Text = "30";
            // 
            // FourDifferentPoints
            // 
            this.FourDifferentPoints.AutoSize = true;
            this.FourDifferentPoints.BackColor = System.Drawing.Color.Transparent;
            this.FourDifferentPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FourDifferentPoints.Location = new System.Drawing.Point(649, 192);
            this.FourDifferentPoints.Name = "FourDifferentPoints";
            this.FourDifferentPoints.Size = new System.Drawing.Size(15, 12);
            this.FourDifferentPoints.TabIndex = 118;
            this.FourDifferentPoints.Text = "40";
            // 
            // FiveDifferentPoints
            // 
            this.FiveDifferentPoints.AutoSize = true;
            this.FiveDifferentPoints.BackColor = System.Drawing.Color.Transparent;
            this.FiveDifferentPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FiveDifferentPoints.Location = new System.Drawing.Point(708, 192);
            this.FiveDifferentPoints.Name = "FiveDifferentPoints";
            this.FiveDifferentPoints.Size = new System.Drawing.Size(15, 12);
            this.FiveDifferentPoints.TabIndex = 119;
            this.FiveDifferentPoints.Text = "50";
            // 
            // VowelHowlPoints
            // 
            this.VowelHowlPoints.AutoSize = true;
            this.VowelHowlPoints.BackColor = System.Drawing.Color.Transparent;
            this.VowelHowlPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.VowelHowlPoints.Location = new System.Drawing.Point(767, 192);
            this.VowelHowlPoints.Name = "VowelHowlPoints";
            this.VowelHowlPoints.Size = new System.Drawing.Size(15, 12);
            this.VowelHowlPoints.TabIndex = 120;
            this.VowelHowlPoints.Text = "70";
            // 
            // ThreeKindPoints
            // 
            this.ThreeKindPoints.AutoSize = true;
            this.ThreeKindPoints.BackColor = System.Drawing.Color.Transparent;
            this.ThreeKindPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ThreeKindPoints.Location = new System.Drawing.Point(472, 192);
            this.ThreeKindPoints.Name = "ThreeKindPoints";
            this.ThreeKindPoints.Size = new System.Drawing.Size(15, 12);
            this.ThreeKindPoints.TabIndex = 121;
            this.ThreeKindPoints.Text = "30";
            // 
            // FourKindPoints
            // 
            this.FourKindPoints.AutoSize = true;
            this.FourKindPoints.BackColor = System.Drawing.Color.Transparent;
            this.FourKindPoints.Font = new System.Drawing.Font("Microsoft Sans Serif", 6.5F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.FourKindPoints.Location = new System.Drawing.Point(531, 192);
            this.FourKindPoints.Name = "FourKindPoints";
            this.FourKindPoints.Size = new System.Drawing.Size(15, 12);
            this.FourKindPoints.TabIndex = 122;
            this.FourKindPoints.Text = "40";
            // 
            // Hold2
            // 
            this.Hold2.BackColor = System.Drawing.Color.Transparent;
            this.Hold2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Hold2.Image = global::ELF_Resources.Properties.Resources.HOLD;
            this.Hold2.Location = new System.Drawing.Point(370, 215);
            this.Hold2.Name = "Hold2";
            this.Hold2.Size = new System.Drawing.Size(165, 165);
            this.Hold2.TabIndex = 123;
            this.Hold2.TabStop = false;
            this.Hold2.Tag = "1";
            this.Hold2.Visible = false;
            this.Hold2.MouseLeave += new System.EventHandler(this.Hold_MouseLeave);
            this.Hold2.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Hold_MouseUp);
            this.Hold2.MouseEnter += new System.EventHandler(this.Hold_MouseEnter);
            // 
            // Hold3
            // 
            this.Hold3.BackColor = System.Drawing.Color.Transparent;
            this.Hold3.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Hold3.Image = global::ELF_Resources.Properties.Resources.HOLD;
            this.Hold3.Location = new System.Drawing.Point(598, 215);
            this.Hold3.Name = "Hold3";
            this.Hold3.Size = new System.Drawing.Size(165, 165);
            this.Hold3.TabIndex = 124;
            this.Hold3.TabStop = false;
            this.Hold3.Tag = "2";
            this.Hold3.Visible = false;
            this.Hold3.MouseLeave += new System.EventHandler(this.Hold_MouseLeave);
            this.Hold3.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Hold_MouseUp);
            this.Hold3.MouseEnter += new System.EventHandler(this.Hold_MouseEnter);
            // 
            // Hold4
            // 
            this.Hold4.BackColor = System.Drawing.Color.Transparent;
            this.Hold4.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Hold4.Image = global::ELF_Resources.Properties.Resources.HOLD;
            this.Hold4.Location = new System.Drawing.Point(141, 409);
            this.Hold4.Name = "Hold4";
            this.Hold4.Size = new System.Drawing.Size(165, 165);
            this.Hold4.TabIndex = 125;
            this.Hold4.TabStop = false;
            this.Hold4.Tag = "3";
            this.Hold4.Visible = false;
            this.Hold4.MouseLeave += new System.EventHandler(this.Hold_MouseLeave);
            this.Hold4.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Hold_MouseUp);
            this.Hold4.MouseEnter += new System.EventHandler(this.Hold_MouseEnter);
            // 
            // Hold5
            // 
            this.Hold5.BackColor = System.Drawing.Color.Transparent;
            this.Hold5.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Hold5.Image = global::ELF_Resources.Properties.Resources.HOLD;
            this.Hold5.Location = new System.Drawing.Point(370, 409);
            this.Hold5.Name = "Hold5";
            this.Hold5.Size = new System.Drawing.Size(165, 165);
            this.Hold5.TabIndex = 126;
            this.Hold5.TabStop = false;
            this.Hold5.Tag = "4";
            this.Hold5.Visible = false;
            this.Hold5.MouseLeave += new System.EventHandler(this.Hold_MouseLeave);
            this.Hold5.MouseUp += new System.Windows.Forms.MouseEventHandler(this.Hold_MouseUp);
            this.Hold5.MouseEnter += new System.EventHandler(this.Hold_MouseEnter);
            // 
            // VowelHowlForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.Color.White;
            this.BackgroundImage = global::ELF_Resources.Properties.Resources.VARIED_VOWEL_HOWL_BACKGROUND;
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(899, 699);
            this.Controls.Add(this.FourKindPoints);
            this.Controls.Add(this.ThreeKindPoints);
            this.Controls.Add(this.VowelHowlPoints);
            this.Controls.Add(this.FiveDifferentPoints);
            this.Controls.Add(this.FourDifferentPoints);
            this.Controls.Add(this.FullHoursPoints);
            this.Controls.Add(this.TwoPairPoints);
            this.Controls.Add(this.BonusScoreTens);
            this.Controls.Add(this.button8);
            this.Controls.Add(this.MenuPictureBox);
            this.Controls.Add(this.button7);
            this.Controls.Add(this.button6);
            this.Controls.Add(this.button5);
            this.Controls.Add(this.button4);
            this.Controls.Add(this.button3);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.button9);
            this.Controls.Add(this.BonusScoreOnes);
            this.Controls.Add(this.TopScoreHundreds);
            this.Controls.Add(this.TopScoreTens);
            this.Controls.Add(this.TopScoreOnes);
            this.Controls.Add(this.ScoreOnes);
            this.Controls.Add(this.ScoreTens);
            this.Controls.Add(this.Speak5);
            this.Controls.Add(this.Speak4);
            this.Controls.Add(this.Speak3);
            this.Controls.Add(this.button2);
            this.Controls.Add(this.button1);
            this.Controls.Add(this.Score12);
            this.Controls.Add(this.Score11);
            this.Controls.Add(this.Score10);
            this.Controls.Add(this.Score9);
            this.Controls.Add(this.Score7);
            this.Controls.Add(this.Score8);
            this.Controls.Add(this.Score6);
            this.Controls.Add(this.Score5);
            this.Controls.Add(this.Score4);
            this.Controls.Add(this.Score3);
            this.Controls.Add(this.Score2);
            this.Controls.Add(this.Score1);
            this.Controls.Add(this.Speak2);
            this.Controls.Add(this.Speak1);
            this.Controls.Add(this.ScoreHundreds);
            this.Controls.Add(this.m_rollNumberLbl);
            this.Controls.Add(this.Roll);
            this.Controls.Add(this.m_instructionsLbl);
            this.Controls.Add(this.Hold1);
            this.Controls.Add(this.Dice1);
            this.Controls.Add(this.Hold2);
            this.Controls.Add(this.Hold3);
            this.Controls.Add(this.Dice2);
            this.Controls.Add(this.Dice3);
            this.Controls.Add(this.Hold4);
            this.Controls.Add(this.Dice4);
            this.Controls.Add(this.Hold5);
            this.Controls.Add(this.Dice5);
            this.DoubleBuffered = true;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "VowelHowlForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Vowel Howl";
            this.Load += new System.EventHandler(this.VowelHowlForm_Load);
            this.Paint += new System.Windows.Forms.PaintEventHandler(this.VowelHowlForm_Paint);
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.VowelHowlForm_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.Roll)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ScoreButton1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score10)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score11)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Score12)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Dice5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.MenuPictureBox)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Hold5)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Label m_instructionsLbl;
        private System.Windows.Forms.PictureBox Roll;
        private System.Windows.Forms.PictureBox Hold1;
        private Label m_rollNumberLbl;
        private Label ScoreHundreds;
        private ToolStripMenuItem fileToolStripMenuItem;
        private ToolStripMenuItem newGameToolStripMenuItem;
        private ToolStripMenuItem exitToolStripMenuItem;
        private ToolStripMenuItem vowelHowelToolStripMenuItem;
        private ToolStripMenuItem optionsToolStripMenuItem;
        private ToolStripMenuItem instructionsToolStripMenuItem;
        private ToolStripMenuItem shortVowelHowlToolStripMenuItem;
        private ToolStripMenuItem variedVowelHowlToolStripMenuItem;
        private ToolStripMenuItem consonantRoll1ToolStripMenuItem;
        private ToolStripMenuItem consonantRoll2ToolStripMenuItem;
        private ToolStripMenuItem consonantRoll3ToolStripMenuItem;
        private ToolStripMenuItem consonantRoll4ToolStripMenuItem;
        private Button Speak1;
        private Button Speak2;
        private Button Speak3;
        private Button Speak4;
        private Button Speak5;
        private PictureBox Score1;
        private PictureBox Score2;
        private PictureBox Score3;
        private PictureBox Score4;
        private PictureBox Score5;
        private PictureBox ScoreButton1;
        private PictureBox Score6;
        private PictureBox Score8;
        private PictureBox Score7;
        private PictureBox Score9;
        private PictureBox Score10;
        private PictureBox Score11;
        private PictureBox Score12;
        private Button button1;
        private Button button2;
        private PictureBox Dice1;
        private PictureBox Dice2;
        private PictureBox Dice3;
        private PictureBox Dice4;
        private PictureBox Dice5;
        private Label ScoreTens;
        private Label ScoreOnes;
        private Label TopScoreOnes;
        private Label TopScoreTens;
        private Label TopScoreHundreds;
        private Label BonusScoreOnes;
        private Label BonusScoreTens;
        private Button button9;
        private Label label1;
        private Label label2;
        private Button button3;
        private Button button4;
        private Button button5;
        private Button button6;
        private Button button7;
        private PictureBox MenuPictureBox;
        private Button button8;
        private Label TwoPairPoints;
        private Label FullHoursPoints;
        private Label FourDifferentPoints;
        private Label FiveDifferentPoints;
        private Label VowelHowlPoints;
        private Label ThreeKindPoints;
        private Label FourKindPoints;
        private PictureBox Hold2;
        private PictureBox Hold3;
        private PictureBox Hold4;
        private PictureBox Hold5;

    }
}

