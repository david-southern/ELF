namespace ELF
{
    partial class WordBuilderForm
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
            System.ComponentModel.ComponentResourceManager resources = new System.ComponentModel.ComponentResourceManager(typeof(WordBuilderForm));
            this.button1 = new System.Windows.Forms.Button();
            this.CardsLeft = new System.Windows.Forms.Label();
            this.ScoreOnes = new System.Windows.Forms.Label();
            this.ScoreTens = new System.Windows.Forms.Label();
            this.CurrentCard = new System.Windows.Forms.PictureBox();
            this.Board_1_1 = new System.Windows.Forms.PictureBox();
            this.Board_1_0 = new System.Windows.Forms.PictureBox();
            this.Board_2_0 = new System.Windows.Forms.PictureBox();
            this.Board_2_2 = new System.Windows.Forms.PictureBox();
            this.Board_1_2 = new System.Windows.Forms.PictureBox();
            this.Board_0_2 = new System.Windows.Forms.PictureBox();
            this.Board_2_1 = new System.Windows.Forms.PictureBox();
            this.Board_0_1 = new System.Windows.Forms.PictureBox();
            this.Board_0_0 = new System.Windows.Forms.PictureBox();
            this.ResultsText = new System.Windows.Forms.TextBox();
            this.ScoreHundreds = new System.Windows.Forms.Label();
            this.Genius0 = new System.Windows.Forms.PictureBox();
            this.Genius2 = new System.Windows.Forms.PictureBox();
            this.Genius4 = new System.Windows.Forms.PictureBox();
            this.Genius6 = new System.Windows.Forms.PictureBox();
            this.Genius8 = new System.Windows.Forms.PictureBox();
            this.Genius1 = new System.Windows.Forms.PictureBox();
            this.Genius3 = new System.Windows.Forms.PictureBox();
            this.Genius5 = new System.Windows.Forms.PictureBox();
            this.Genius7 = new System.Windows.Forms.PictureBox();
            this.Genius9 = new System.Windows.Forms.PictureBox();
            this.GeniusAward = new System.Windows.Forms.PictureBox();
            this.MissedWords = new System.Windows.Forms.TextBox();
            this.StartingList = new System.Windows.Forms.ListBox();
            this.EndingList = new System.Windows.Forms.ListBox();
            this.label1 = new System.Windows.Forms.Label();
            this.label2 = new System.Windows.Forms.Label();
            this.TimeLabel = new System.Windows.Forms.Label();
            this.DrawCardButton = new System.Windows.Forms.Button();
            this.NewGame = new System.Windows.Forms.Button();
            this.ExitButton = new System.Windows.Forms.PictureBox();
            ((System.ComponentModel.ISupportInitialize)(this.CurrentCard)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_1_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_1_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_2_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_2_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_1_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_0_2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_2_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_0_1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_0_0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius0)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius2)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius4)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius6)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius8)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius1)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius3)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius5)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius7)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius9)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.GeniusAward)).BeginInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitButton)).BeginInit();
            this.SuspendLayout();
            // 
            // button1
            // 
            this.button1.Font = new System.Drawing.Font("Comic Sans MS", 8.25F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.button1.Location = new System.Drawing.Point(12, 12);
            this.button1.Name = "button1";
            this.button1.Size = new System.Drawing.Size(152, 23);
            this.button1.TabIndex = 0;
            this.button1.TabStop = false;
            this.button1.Text = "GenerateWordList";
            this.button1.UseVisualStyleBackColor = true;
            this.button1.Click += new System.EventHandler(this.button1_Click);
            // 
            // CardsLeft
            // 
            this.CardsLeft.BackColor = System.Drawing.Color.Transparent;
            this.CardsLeft.Font = new System.Drawing.Font("Film Cryptic", 15F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.CardsLeft.ForeColor = System.Drawing.Color.White;
            this.CardsLeft.Location = new System.Drawing.Point(187, 341);
            this.CardsLeft.Name = "CardsLeft";
            this.CardsLeft.Size = new System.Drawing.Size(38, 21);
            this.CardsLeft.TabIndex = 40;
            this.CardsLeft.Text = "14";
            // 
            // ScoreOnes
            // 
            this.ScoreOnes.BackColor = System.Drawing.Color.Transparent;
            this.ScoreOnes.Font = new System.Drawing.Font("Film Cryptic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreOnes.ForeColor = System.Drawing.Color.White;
            this.ScoreOnes.Location = new System.Drawing.Point(179, 379);
            this.ScoreOnes.Name = "ScoreOnes";
            this.ScoreOnes.Size = new System.Drawing.Size(28, 30);
            this.ScoreOnes.TabIndex = 38;
            this.ScoreOnes.Text = "0";
            // 
            // ScoreTens
            // 
            this.ScoreTens.BackColor = System.Drawing.Color.Transparent;
            this.ScoreTens.Font = new System.Drawing.Font("Film Cryptic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreTens.ForeColor = System.Drawing.Color.White;
            this.ScoreTens.Location = new System.Drawing.Point(157, 379);
            this.ScoreTens.Name = "ScoreTens";
            this.ScoreTens.Size = new System.Drawing.Size(28, 30);
            this.ScoreTens.TabIndex = 37;
            this.ScoreTens.Text = "0";
            // 
            // CurrentCard
            // 
            this.CurrentCard.BackColor = System.Drawing.Color.Transparent;
            this.CurrentCard.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.CurrentCard.Location = new System.Drawing.Point(56, 167);
            this.CurrentCard.Name = "CurrentCard";
            this.CurrentCard.Size = new System.Drawing.Size(150, 100);
            this.CurrentCard.TabIndex = 36;
            this.CurrentCard.TabStop = false;
            this.CurrentCard.Click += new System.EventHandler(this.DrawCard_Click);
            // 
            // Board_1_1
            // 
            this.Board_1_1.BackColor = System.Drawing.Color.Transparent;
            this.Board_1_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Board_1_1.Location = new System.Drawing.Point(474, 222);
            this.Board_1_1.Name = "Board_1_1";
            this.Board_1_1.Size = new System.Drawing.Size(150, 100);
            this.Board_1_1.TabIndex = 35;
            this.Board_1_1.TabStop = false;
            this.Board_1_1.Tag = "4";
            this.Board_1_1.Click += new System.EventHandler(this.Board_Click);
            // 
            // Board_1_0
            // 
            this.Board_1_0.BackColor = System.Drawing.Color.Transparent;
            this.Board_1_0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Board_1_0.Location = new System.Drawing.Point(472, 100);
            this.Board_1_0.Name = "Board_1_0";
            this.Board_1_0.Size = new System.Drawing.Size(150, 100);
            this.Board_1_0.TabIndex = 34;
            this.Board_1_0.TabStop = false;
            this.Board_1_0.Tag = "1";
            this.Board_1_0.Click += new System.EventHandler(this.Board_Click);
            // 
            // Board_2_0
            // 
            this.Board_2_0.BackColor = System.Drawing.Color.Transparent;
            this.Board_2_0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Board_2_0.Location = new System.Drawing.Point(678, 100);
            this.Board_2_0.Name = "Board_2_0";
            this.Board_2_0.Size = new System.Drawing.Size(150, 100);
            this.Board_2_0.TabIndex = 33;
            this.Board_2_0.TabStop = false;
            this.Board_2_0.Tag = "2";
            this.Board_2_0.Click += new System.EventHandler(this.Board_Click);
            // 
            // Board_2_2
            // 
            this.Board_2_2.BackColor = System.Drawing.Color.Transparent;
            this.Board_2_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Board_2_2.Location = new System.Drawing.Point(678, 344);
            this.Board_2_2.Name = "Board_2_2";
            this.Board_2_2.Size = new System.Drawing.Size(150, 100);
            this.Board_2_2.TabIndex = 32;
            this.Board_2_2.TabStop = false;
            this.Board_2_2.Tag = "8";
            this.Board_2_2.Click += new System.EventHandler(this.Board_Click);
            // 
            // Board_1_2
            // 
            this.Board_1_2.BackColor = System.Drawing.Color.Transparent;
            this.Board_1_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Board_1_2.Location = new System.Drawing.Point(474, 344);
            this.Board_1_2.Name = "Board_1_2";
            this.Board_1_2.Size = new System.Drawing.Size(150, 100);
            this.Board_1_2.TabIndex = 31;
            this.Board_1_2.TabStop = false;
            this.Board_1_2.Tag = "7";
            this.Board_1_2.Click += new System.EventHandler(this.Board_Click);
            // 
            // Board_0_2
            // 
            this.Board_0_2.BackColor = System.Drawing.Color.Transparent;
            this.Board_0_2.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Board_0_2.Location = new System.Drawing.Point(266, 344);
            this.Board_0_2.Name = "Board_0_2";
            this.Board_0_2.Size = new System.Drawing.Size(150, 100);
            this.Board_0_2.TabIndex = 30;
            this.Board_0_2.TabStop = false;
            this.Board_0_2.Tag = "6";
            this.Board_0_2.Click += new System.EventHandler(this.Board_Click);
            // 
            // Board_2_1
            // 
            this.Board_2_1.BackColor = System.Drawing.Color.Transparent;
            this.Board_2_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Board_2_1.Location = new System.Drawing.Point(678, 222);
            this.Board_2_1.Name = "Board_2_1";
            this.Board_2_1.Size = new System.Drawing.Size(150, 100);
            this.Board_2_1.TabIndex = 29;
            this.Board_2_1.TabStop = false;
            this.Board_2_1.Tag = "5";
            this.Board_2_1.Click += new System.EventHandler(this.Board_Click);
            // 
            // Board_0_1
            // 
            this.Board_0_1.BackColor = System.Drawing.Color.Transparent;
            this.Board_0_1.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Board_0_1.Location = new System.Drawing.Point(266, 222);
            this.Board_0_1.Name = "Board_0_1";
            this.Board_0_1.Size = new System.Drawing.Size(150, 100);
            this.Board_0_1.TabIndex = 28;
            this.Board_0_1.TabStop = false;
            this.Board_0_1.Tag = "3";
            this.Board_0_1.Click += new System.EventHandler(this.Board_Click);
            // 
            // Board_0_0
            // 
            this.Board_0_0.BackColor = System.Drawing.Color.Transparent;
            this.Board_0_0.BackgroundImageLayout = System.Windows.Forms.ImageLayout.Center;
            this.Board_0_0.Location = new System.Drawing.Point(266, 100);
            this.Board_0_0.Name = "Board_0_0";
            this.Board_0_0.Size = new System.Drawing.Size(150, 100);
            this.Board_0_0.TabIndex = 27;
            this.Board_0_0.TabStop = false;
            this.Board_0_0.Tag = "0";
            this.Board_0_0.Click += new System.EventHandler(this.Board_Click);
            // 
            // ResultsText
            // 
            this.ResultsText.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(97)))), ((int)(((byte)(37)))));
            this.ResultsText.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.ResultsText.Font = new System.Drawing.Font("Film Cryptic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ResultsText.ForeColor = System.Drawing.Color.White;
            this.ResultsText.Location = new System.Drawing.Point(69, 531);
            this.ResultsText.Multiline = true;
            this.ResultsText.Name = "ResultsText";
            this.ResultsText.Size = new System.Drawing.Size(296, 57);
            this.ResultsText.TabIndex = 42;
            this.ResultsText.TabStop = false;
            // 
            // ScoreHundreds
            // 
            this.ScoreHundreds.BackColor = System.Drawing.Color.Transparent;
            this.ScoreHundreds.Font = new System.Drawing.Font("Film Cryptic", 21.75F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.ScoreHundreds.ForeColor = System.Drawing.Color.White;
            this.ScoreHundreds.Location = new System.Drawing.Point(135, 379);
            this.ScoreHundreds.Name = "ScoreHundreds";
            this.ScoreHundreds.Size = new System.Drawing.Size(28, 30);
            this.ScoreHundreds.TabIndex = 43;
            this.ScoreHundreds.Text = "0";
            // 
            // Genius0
            // 
            this.Genius0.BackColor = System.Drawing.Color.Transparent;
            this.Genius0.Image = ((System.Drawing.Image)(resources.GetObject("Genius0.Image")));
            this.Genius0.Location = new System.Drawing.Point(875, 108);
            this.Genius0.Name = "Genius0";
            this.Genius0.Size = new System.Drawing.Size(45, 45);
            this.Genius0.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius0.TabIndex = 44;
            this.Genius0.TabStop = false;
            // 
            // Genius2
            // 
            this.Genius2.BackColor = System.Drawing.Color.Transparent;
            this.Genius2.Image = ((System.Drawing.Image)(resources.GetObject("Genius2.Image")));
            this.Genius2.Location = new System.Drawing.Point(875, 158);
            this.Genius2.Name = "Genius2";
            this.Genius2.Size = new System.Drawing.Size(45, 45);
            this.Genius2.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius2.TabIndex = 45;
            this.Genius2.TabStop = false;
            // 
            // Genius4
            // 
            this.Genius4.BackColor = System.Drawing.Color.Transparent;
            this.Genius4.Image = ((System.Drawing.Image)(resources.GetObject("Genius4.Image")));
            this.Genius4.Location = new System.Drawing.Point(875, 208);
            this.Genius4.Name = "Genius4";
            this.Genius4.Size = new System.Drawing.Size(45, 45);
            this.Genius4.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius4.TabIndex = 46;
            this.Genius4.TabStop = false;
            // 
            // Genius6
            // 
            this.Genius6.BackColor = System.Drawing.Color.Transparent;
            this.Genius6.Image = ((System.Drawing.Image)(resources.GetObject("Genius6.Image")));
            this.Genius6.Location = new System.Drawing.Point(875, 258);
            this.Genius6.Name = "Genius6";
            this.Genius6.Size = new System.Drawing.Size(45, 45);
            this.Genius6.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius6.TabIndex = 47;
            this.Genius6.TabStop = false;
            // 
            // Genius8
            // 
            this.Genius8.BackColor = System.Drawing.Color.Transparent;
            this.Genius8.Image = ((System.Drawing.Image)(resources.GetObject("Genius8.Image")));
            this.Genius8.Location = new System.Drawing.Point(875, 308);
            this.Genius8.Name = "Genius8";
            this.Genius8.Size = new System.Drawing.Size(45, 45);
            this.Genius8.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius8.TabIndex = 48;
            this.Genius8.TabStop = false;
            // 
            // Genius1
            // 
            this.Genius1.BackColor = System.Drawing.Color.Transparent;
            this.Genius1.Image = ((System.Drawing.Image)(resources.GetObject("Genius1.Image")));
            this.Genius1.Location = new System.Drawing.Point(924, 108);
            this.Genius1.Name = "Genius1";
            this.Genius1.Size = new System.Drawing.Size(45, 45);
            this.Genius1.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius1.TabIndex = 49;
            this.Genius1.TabStop = false;
            // 
            // Genius3
            // 
            this.Genius3.BackColor = System.Drawing.Color.Transparent;
            this.Genius3.Image = ((System.Drawing.Image)(resources.GetObject("Genius3.Image")));
            this.Genius3.Location = new System.Drawing.Point(924, 158);
            this.Genius3.Name = "Genius3";
            this.Genius3.Size = new System.Drawing.Size(45, 45);
            this.Genius3.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius3.TabIndex = 50;
            this.Genius3.TabStop = false;
            // 
            // Genius5
            // 
            this.Genius5.BackColor = System.Drawing.Color.Transparent;
            this.Genius5.Image = ((System.Drawing.Image)(resources.GetObject("Genius5.Image")));
            this.Genius5.Location = new System.Drawing.Point(924, 208);
            this.Genius5.Name = "Genius5";
            this.Genius5.Size = new System.Drawing.Size(45, 45);
            this.Genius5.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius5.TabIndex = 51;
            this.Genius5.TabStop = false;
            // 
            // Genius7
            // 
            this.Genius7.BackColor = System.Drawing.Color.Transparent;
            this.Genius7.Image = ((System.Drawing.Image)(resources.GetObject("Genius7.Image")));
            this.Genius7.Location = new System.Drawing.Point(924, 258);
            this.Genius7.Name = "Genius7";
            this.Genius7.Size = new System.Drawing.Size(45, 45);
            this.Genius7.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius7.TabIndex = 52;
            this.Genius7.TabStop = false;
            // 
            // Genius9
            // 
            this.Genius9.BackColor = System.Drawing.Color.Transparent;
            this.Genius9.Image = ((System.Drawing.Image)(resources.GetObject("Genius9.Image")));
            this.Genius9.Location = new System.Drawing.Point(924, 308);
            this.Genius9.Name = "Genius9";
            this.Genius9.Size = new System.Drawing.Size(45, 45);
            this.Genius9.SizeMode = System.Windows.Forms.PictureBoxSizeMode.Zoom;
            this.Genius9.TabIndex = 53;
            this.Genius9.TabStop = false;
            // 
            // GeniusAward
            // 
            this.GeniusAward.BackColor = System.Drawing.Color.Transparent;
            this.GeniusAward.Image = ((System.Drawing.Image)(resources.GetObject("GeniusAward.Image")));
            this.GeniusAward.Location = new System.Drawing.Point(717, 38);
            this.GeniusAward.Name = "GeniusAward";
            this.GeniusAward.Size = new System.Drawing.Size(119, 67);
            this.GeniusAward.SizeMode = System.Windows.Forms.PictureBoxSizeMode.CenterImage;
            this.GeniusAward.TabIndex = 54;
            this.GeniusAward.TabStop = false;
            this.GeniusAward.Visible = false;
            // 
            // MissedWords
            // 
            this.MissedWords.BackColor = System.Drawing.Color.FromArgb(((int)(((byte)(68)))), ((int)(((byte)(97)))), ((int)(((byte)(37)))));
            this.MissedWords.BorderStyle = System.Windows.Forms.BorderStyle.None;
            this.MissedWords.Font = new System.Drawing.Font("Film Cryptic", 18F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.MissedWords.ForeColor = System.Drawing.Color.White;
            this.MissedWords.Location = new System.Drawing.Point(495, 531);
            this.MissedWords.Multiline = true;
            this.MissedWords.Name = "MissedWords";
            this.MissedWords.Size = new System.Drawing.Size(279, 57);
            this.MissedWords.TabIndex = 55;
            this.MissedWords.TabStop = false;
            // 
            // StartingList
            // 
            this.StartingList.FormattingEnabled = true;
            this.StartingList.Location = new System.Drawing.Point(1, 54);
            this.StartingList.Name = "StartingList";
            this.StartingList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.StartingList.Size = new System.Drawing.Size(53, 238);
            this.StartingList.TabIndex = 56;
            // 
            // EndingList
            // 
            this.EndingList.FormattingEnabled = true;
            this.EndingList.Location = new System.Drawing.Point(1, 324);
            this.EndingList.Name = "EndingList";
            this.EndingList.SelectionMode = System.Windows.Forms.SelectionMode.MultiExtended;
            this.EndingList.Size = new System.Drawing.Size(53, 381);
            this.EndingList.TabIndex = 57;
            // 
            // label1
            // 
            this.label1.AutoSize = true;
            this.label1.Location = new System.Drawing.Point(9, 308);
            this.label1.Name = "label1";
            this.label1.Size = new System.Drawing.Size(45, 13);
            this.label1.TabIndex = 58;
            this.label1.Text = "Endings";
            // 
            // label2
            // 
            this.label2.AutoSize = true;
            this.label2.Location = new System.Drawing.Point(9, 38);
            this.label2.Name = "label2";
            this.label2.Size = new System.Drawing.Size(48, 13);
            this.label2.TabIndex = 59;
            this.label2.Text = "Startings";
            // 
            // TimeLabel
            // 
            this.TimeLabel.BackColor = System.Drawing.Color.Transparent;
            this.TimeLabel.Font = new System.Drawing.Font("Film Cryptic", 21.75F, System.Drawing.FontStyle.Regular, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.TimeLabel.ForeColor = System.Drawing.Color.White;
            this.TimeLabel.Location = new System.Drawing.Point(60, 71);
            this.TimeLabel.Name = "TimeLabel";
            this.TimeLabel.Size = new System.Drawing.Size(146, 30);
            this.TimeLabel.TabIndex = 60;
            this.TimeLabel.Text = "17 seconds";
            this.TimeLabel.TextAlign = System.Drawing.ContentAlignment.MiddleCenter;
            // 
            // DrawCardButton
            // 
            this.DrawCardButton.Font = new System.Drawing.Font("Film Cryptic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.DrawCardButton.Location = new System.Drawing.Point(45, 302);
            this.DrawCardButton.Name = "DrawCardButton";
            this.DrawCardButton.Size = new System.Drawing.Size(172, 29);
            this.DrawCardButton.TabIndex = 61;
            this.DrawCardButton.TabStop = false;
            this.DrawCardButton.Text = "Draw a Card";
            this.DrawCardButton.UseVisualStyleBackColor = true;
            this.DrawCardButton.Click += new System.EventHandler(this.DrawCard_Click);
            // 
            // NewGame
            // 
            this.NewGame.Font = new System.Drawing.Font("Film Cryptic", 14.25F, System.Drawing.FontStyle.Bold, System.Drawing.GraphicsUnit.Point, ((byte)(0)));
            this.NewGame.Location = new System.Drawing.Point(45, 415);
            this.NewGame.Name = "NewGame";
            this.NewGame.Size = new System.Drawing.Size(172, 29);
            this.NewGame.TabIndex = 62;
            this.NewGame.TabStop = false;
            this.NewGame.Text = "Start New Game";
            this.NewGame.UseVisualStyleBackColor = true;
            this.NewGame.Click += new System.EventHandler(this.NewGame_Click);
            // 
            // ExitButton
            // 
            this.ExitButton.BackColor = System.Drawing.Color.Transparent;
            this.ExitButton.Location = new System.Drawing.Point(871, 9);
            this.ExitButton.Name = "ExitButton";
            this.ExitButton.Size = new System.Drawing.Size(103, 24);
            this.ExitButton.TabIndex = 103;
            this.ExitButton.TabStop = false;
            this.ExitButton.Click += new System.EventHandler(this.ExitButton_Click);
            // 
            // WordBuilderForm
            // 
            this.AutoScaleDimensions = new System.Drawing.SizeF(6F, 13F);
            this.AutoScaleMode = System.Windows.Forms.AutoScaleMode.Font;
            this.BackColor = System.Drawing.SystemColors.Control;
            this.BackgroundImage = ((System.Drawing.Image)(resources.GetObject("$this.BackgroundImage")));
            this.BackgroundImageLayout = System.Windows.Forms.ImageLayout.None;
            this.ClientSize = new System.Drawing.Size(980, 700);
            this.Controls.Add(this.ExitButton);
            this.Controls.Add(this.NewGame);
            this.Controls.Add(this.GeniusAward);
            this.Controls.Add(this.DrawCardButton);
            this.Controls.Add(this.TimeLabel);
            this.Controls.Add(this.label2);
            this.Controls.Add(this.label1);
            this.Controls.Add(this.EndingList);
            this.Controls.Add(this.StartingList);
            this.Controls.Add(this.MissedWords);
            this.Controls.Add(this.Genius9);
            this.Controls.Add(this.Genius7);
            this.Controls.Add(this.Genius5);
            this.Controls.Add(this.Genius3);
            this.Controls.Add(this.Genius1);
            this.Controls.Add(this.Genius8);
            this.Controls.Add(this.Genius6);
            this.Controls.Add(this.Genius4);
            this.Controls.Add(this.Genius2);
            this.Controls.Add(this.Genius0);
            this.Controls.Add(this.ScoreHundreds);
            this.Controls.Add(this.ResultsText);
            this.Controls.Add(this.CardsLeft);
            this.Controls.Add(this.ScoreOnes);
            this.Controls.Add(this.ScoreTens);
            this.Controls.Add(this.CurrentCard);
            this.Controls.Add(this.Board_1_1);
            this.Controls.Add(this.Board_1_0);
            this.Controls.Add(this.Board_2_0);
            this.Controls.Add(this.Board_2_2);
            this.Controls.Add(this.Board_1_2);
            this.Controls.Add(this.Board_0_2);
            this.Controls.Add(this.Board_2_1);
            this.Controls.Add(this.Board_0_1);
            this.Controls.Add(this.Board_0_0);
            this.Controls.Add(this.button1);
            this.FormBorderStyle = System.Windows.Forms.FormBorderStyle.Fixed3D;
            this.KeyPreview = true;
            this.MaximizeBox = false;
            this.MinimizeBox = false;
            this.Name = "WordBuilderForm";
            this.SizeGripStyle = System.Windows.Forms.SizeGripStyle.Hide;
            this.StartPosition = System.Windows.Forms.FormStartPosition.CenterScreen;
            this.Text = "Genius Word Builder";
            this.KeyPress += new System.Windows.Forms.KeyPressEventHandler(this.TestMode_KeyPress);
            ((System.ComponentModel.ISupportInitialize)(this.CurrentCard)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_1_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_1_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_2_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_2_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_1_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_0_2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_2_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_0_1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Board_0_0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius0)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius2)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius4)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius6)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius8)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius1)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius3)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius5)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius7)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.Genius9)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.GeniusAward)).EndInit();
            ((System.ComponentModel.ISupportInitialize)(this.ExitButton)).EndInit();
            this.ResumeLayout(false);
            this.PerformLayout();

        }

        #endregion

        private System.Windows.Forms.Button button1;
        private System.Windows.Forms.Label CardsLeft;
        private System.Windows.Forms.Label ScoreOnes;
        private System.Windows.Forms.Label ScoreTens;
        private System.Windows.Forms.PictureBox CurrentCard;
        private System.Windows.Forms.PictureBox Board_1_1;
        private System.Windows.Forms.PictureBox Board_1_0;
        private System.Windows.Forms.PictureBox Board_2_0;
        private System.Windows.Forms.PictureBox Board_2_2;
        private System.Windows.Forms.PictureBox Board_1_2;
        private System.Windows.Forms.PictureBox Board_0_2;
        private System.Windows.Forms.PictureBox Board_2_1;
        private System.Windows.Forms.PictureBox Board_0_1;
        private System.Windows.Forms.PictureBox Board_0_0;
        private System.Windows.Forms.TextBox ResultsText;
        private System.Windows.Forms.Label ScoreHundreds;
        private System.Windows.Forms.PictureBox Genius0;
        private System.Windows.Forms.PictureBox Genius2;
        private System.Windows.Forms.PictureBox Genius4;
        private System.Windows.Forms.PictureBox Genius6;
        private System.Windows.Forms.PictureBox Genius8;
        private System.Windows.Forms.PictureBox Genius1;
        private System.Windows.Forms.PictureBox Genius3;
        private System.Windows.Forms.PictureBox Genius5;
        private System.Windows.Forms.PictureBox Genius7;
        private System.Windows.Forms.PictureBox Genius9;
        private System.Windows.Forms.PictureBox GeniusAward;
        private System.Windows.Forms.TextBox MissedWords;
        private System.Windows.Forms.ListBox StartingList;
        private System.Windows.Forms.ListBox EndingList;
        private System.Windows.Forms.Label label1;
        private System.Windows.Forms.Label label2;
        private System.Windows.Forms.Label TimeLabel;
        private System.Windows.Forms.Button DrawCardButton;
        private System.Windows.Forms.Button NewGame;
        private System.Windows.Forms.PictureBox ExitButton;
    }
}
