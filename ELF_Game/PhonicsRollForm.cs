using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ELF_Resources.Properties;
using System.Media;
using IntelliLock.Licensing;

namespace ELF
{
    public partial class PhonicsRollForm : Form
    {
        private static readonly int MAX_ROLLS = 5;

        private static readonly int SCORE_CAT1 = 0;
        private static readonly int SCORE_CAT2 = 1;
        private static readonly int SCORE_CAT3 = 2;
        private static readonly int SCORE_CAT4 = 3;
        private static readonly int SCORE_CAT5 = 4;

        private int GameID { get; set; }

        private string[] GameNames = 
        {
            "Vowel Howl - Long",
            "Vowel Howl - Short",
            "Vowel Howl - Varied",
            "Super Sonic Phonics",
            "Super Sonic Phonics",
            "Super Sonic Phonics",
            "Super Sonic Phonics",
            "Super Sonic Phonics"
        };

        private string[] HighScorePrefix = 
        {
            "VH",
            "VH",
            "VH",
            "SP",
            "SP",
            "SP",
            "SP",
            "SP"
        };

        private Image[] GameBackgrounds = new Image[8];

        private Dice[] TheDice = new Dice[5];
        PictureBox[] DicePicture = new PictureBox[5];
        PictureBox[,] ScorePicture = new PictureBox[5, 5];
        Button[] SpeakButtons = new Button[5];
        private int[] SelectedValue = new int[5];

        private Image[] SCORE_UP = new Image[5];
        private Image[] SCORE_SCORED = new Image[5];
        private Image[] SCORE_BURNED = new Image[5];

        int RollCount;
        int TotalScore;

        private Random rand = new Random();

        public PhonicsRollForm(int which_game)
        {
            GameID = which_game;

            InitializeBaseResources();

            SetupNewGame();
        }

        private void SetupNewGame()
        {
            this.Text = GameNames[GameID];

            InitializeGame();
            ResetForNewGame();
            ResetDice();
        }

        private bool allowScore = false;

        private void ResetForNewGame()
        {
            TotalScore = 0;
            RenderScore();

            RollCount = MAX_ROLLS;
            ResetDice();

            Instructions.Text = "Roll the dice to begin the game!";
            allowScore = false;
        }

        private void ResetDice()
        {
            for (int diceIndex = 0; diceIndex < 5; diceIndex++)
            {
                DicePicture[diceIndex].Image = null;
                SpeakButtons[diceIndex].Visible = false;
                SelectedValue[diceIndex] = -1;

                for (int scoreIndex = 0; scoreIndex < 5; scoreIndex++)
                {
                    ScorePicture[diceIndex, scoreIndex].Image = SCORE_UP[scoreIndex];
                }
            }

            RollNumber.Text = RollCount.ToString();
            Invalidate();
        }

        private const int MAX_ROLL_ITERATION = 10;
        private const int MAX_ROLL_SLEEP = 80;
        System.Windows.Forms.Timer rollProgressTimer = null;
        private int rollProgressCounter;

        private string RollState = "Roll";

        private void RollButton_Click(object sender, EventArgs e)
        {
            if (RollState == "New Game")
            {
                ResetForNewGame();
                RollState = "Roll";
                return;
            }

            if (rollProgressTimer.Enabled)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            ResetDice();

            new SoundPlayer(Resources.DICE_ROLL).Play();

            rollProgressCounter = 0;
            rollProgressTimer.Start();
        }

        private void CheckHighScores()
        {
            HighScoreTable highScoreTable = new HighScoreTable(HighScorePrefix[GameID]);
            highScoreTable.Load();
            int score_index = highScoreTable.GetIndexOfScore(TotalScore);

            if (score_index != -1)
            {
                GetName name_form = new GetName();
                name_form.ShowDialog();

                highScoreTable.Update(name_form.PlayerName.Text, TotalScore);
                highScoreTable.Save();
            }
        }

        private void RenderScore()
        {
            //Required for formatting correctly
            string newScoreText = String.Format("{0,3}", TotalScore);
            ScoreHundreds.Text = newScoreText.Substring(0, 1);
            ScoreTens.Text = newScoreText.Substring(1, 1);
            ScoreOnes.Text = newScoreText.Substring(2, 1);
        }

        void HandleRoll(object sender, EventArgs e)
        {
            for (int diceIndex = 0; diceIndex < 5; diceIndex++)
            {
                TheDice[diceIndex].Roll(rand.Next(0, 6));
                DicePicture[diceIndex].Image = TheDice[diceIndex].currentFace.Pic;
            }

            if (rollProgressCounter++ < MAX_ROLL_ITERATION)
            {
                return;
            }

            rollProgressTimer.Stop();

            RollCount--;

            RollNumber.Text = RollCount.ToString();

            foreach (Button visButton in SpeakButtons)
            {
                visButton.Visible = true;
            }

            RollButton.Enabled = false;
            Instructions.Text = "Click the letter buttons that match each picture.";
            allowScore = true;
        }

        private void Score_Click(object sender, EventArgs e)
        {
            if (!allowScore)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            string controlName = null;

            if (sender is PictureBox)
            {
                controlName = ((PictureBox)sender).Name;
            }

            if (controlName == null || !controlName.StartsWith("Score"))
            {
                return;
            }

            int diceIndex = Int32.Parse(controlName.Substring(controlName.Length - 3, 1));
            int scoreValue = Int32.Parse(controlName.Substring(controlName.Length - 1, 1));

            if (RollState == "Roll")
            {
                int correctValue = TheDice[diceIndex].Value;

                if (SelectedValue[diceIndex] == correctValue)
                {
                    new SoundPlayer(Resources.DING).Play();
                    return;
                }

                if (correctValue == scoreValue)
                {
                    new SoundPlayer(Resources.DING).Play();
                    ScorePicture[diceIndex, correctValue].Image = SCORE_SCORED[correctValue];
                    SelectedValue[diceIndex] = correctValue;
                    TotalScore += 10;
                }
                else
                {
                    new SoundPlayer(Resources.BUZZ).Play();
                    if (ScorePicture[diceIndex, scoreValue].Image != SCORE_BURNED[scoreValue])
                    {
                        ScorePicture[diceIndex, scoreValue].Image = SCORE_BURNED[scoreValue];
                        TotalScore -= 5;
                        if (TotalScore < 0)
                        {
                            TotalScore = 0;
                        }
                    }
                }

                int goodScores = 0;

                for (int checkScore = 0; checkScore < 5; checkScore++)
                {
                    if (SelectedValue[checkScore] == TheDice[checkScore].Value)
                    {
                        goodScores++;
                    }
                }

                if (goodScores == 5)
                {
                    Instructions.Text = "Click the Roll button to roll the dice.";
                    RollButton.Enabled = true;

                    if (RollCount == 0)
                    {
                        RollState = "New Game";
                        Instructions.Text = "Click 'Roll' to start a new game.";
                        CheckHighScores();
                    }
                }
                else
                {
                    Instructions.Text = "Please score all dice in order to continue.";
                }

                RenderScore();
            }

        }

        private void Speak_Click(object sender, EventArgs e)
        {
            if (RollCount == MAX_ROLLS)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            string nameOfButton = null;

            if (sender is Button)
            {
                nameOfButton = ((Button)sender).Name;
            }

            if (nameOfButton == null || !nameOfButton.StartsWith("Speak"))
            {
                return;
            }

            int button_index = Int32.Parse(nameOfButton.Substring(nameOfButton.Length - 1, 1));

            if (TheDice[button_index] != null)
            {
                TheDice[button_index].PlaySound();
            }
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
