using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using System.Threading;

using IntelliLock.Licensing;

using ELF_Resources.Properties;


namespace ELF
{
    public partial class VowelHowlForm : Form
    {
        private int FONT_SIZE_SMALL_INSTRUCTIONS = 12;
        private int FONT_SIZE_LARGE_INSTRUCTIONS = 14;

        //Private info
        private int[] CategoryScores = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
        private int GameID { get; set; }
        private string[] GameNames = 
        {
            "Vowel Howl - Long",
            "Vowel Howl - Short",
            "Vowel Howl - Varied",
            "Super Sonic Phonics 1",
            "Super Sonic Phonics 2",
            "Super Sonic Phonics 3",
            "Super Sonic Phonics 4",
            "Super Sonic Phonics 5"
        };

        private string[] HighScorePrefix = 
        {
            "LVH",
            "SVH",
            "VVH",
            "SP1",
            "SP2",
            "SP3",
            "SP4",
            "SP5"
        };

        private Image[] GameBackgrounds = new Image[8];

        private int[] TheDiceValue = new int[5];
        private int scored_dice = 0;

        private Dice[] TheDice = new Dice[5];
        private Random rand = new Random();

        private CalculateScores scoring = new CalculateScores();


        private int RollCount = 0;

        private bool gameOver = false;
        private bool TopBonus = false;

        private int TopScore = 0;
        private int TotalScore = 0;

        delegate int GetScore(int[] diceValueArray);

        private int SCORE_CAT1 = 0;
        private int SCORE_CAT2 = 1;
        private int SCORE_CAT3 = 2;
        private int SCORE_CAT4 = 3;
        private int SCORE_CAT5 = 4;
        private int SCORE_2P = 5;
        private int SCORE_3K = 6;
        private int SCORE_FH = 7;
        private int SCORE_4K = 8;
        private int SCORE_4D = 9;
        private int SCORE_5D = 10;
        private int SCORE_VH = 11;

        private Image[] SCORE_UP = new Image[12];
        private Image[] SCORE_SCORED = new Image[12];
        private Image[] SCORE_BURNED = new Image[12];

        GetScore[] SCORE_FUNCS = new GetScore[12];

        PictureBox[] HoldPicture = new PictureBox[5];
        PictureBox[] DicePicture = new PictureBox[5];

        private int test_index = -1;

        private object roll_lock = new object();
        private int roll_in_progress = -1;
        private int roll_in_progress_target = -1;

        private const int MAX_ROLL_ITERATION = 10;
        private const int MAX_ROLL_SLEEP = 80;
        System.Windows.Forms.Timer check_finished_roll = null;

        public VowelHowlForm(int which_game)
        {
            GameID = which_game;

            InitializeBaseResources();

            TwoPairPoints.Text = CalculateScores.SCORE_VALUES[(int)CalculateScores.ScoreCategories.TwoPair].ToString();
            ThreeKindPoints.Text = CalculateScores.SCORE_VALUES[(int)CalculateScores.ScoreCategories.ThreeKind].ToString();
            FullHoursPoints.Text = CalculateScores.SCORE_VALUES[(int)CalculateScores.ScoreCategories.FullHouse].ToString();
            FourKindPoints.Text = CalculateScores.SCORE_VALUES[(int)CalculateScores.ScoreCategories.FourKind].ToString();
            FourDifferentPoints.Text = CalculateScores.SCORE_VALUES[(int)CalculateScores.ScoreCategories.FourDifferent].ToString();
            FiveDifferentPoints.Text = CalculateScores.SCORE_VALUES[(int)CalculateScores.ScoreCategories.FiveDifferent].ToString();
            VowelHowlPoints.Text = CalculateScores.SCORE_VALUES[(int)CalculateScores.ScoreCategories.VowelHowl].ToString();

            SetupNewGame();
        }

        private void StartGame(object sender, EventArgs e)
        {
            if (!(sender is Button))
            {
                return;
            }

            GameID = Int32.Parse((sender as Button).Tag.ToString());

            if (GameID > 0 && !ProjectUtils.Utils.IsValidLicense())
            {
                LicenseNeeded lic_form = new LicenseNeeded();
                lic_form.ShowDialog();
                return;
            }

            SetupNewGame();
        }

        private void SetupNewGame()
        {
            this.Text = GameNames[GameID];

            InitializeGame();
            ResetForNewGame();
            ResetDice();
        }

        private void ResetForRoll()
        {
            TopBonus = false;
            TopScore = 0;
            RollCount = 0;
            int[] temp = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            CategoryScores = temp;
            ResetDiceValues();
        }

        private void ResetDiceValues()
        {
            TheDiceValue[0] = 0;
            TheDiceValue[1] = 0;
            TheDiceValue[2] = 0;
            TheDiceValue[3] = 0;
            TheDiceValue[4] = 0;
            scored_dice = 0;
        }

        private void ResetForNewGame()
        {
            Score1.Image = SCORE_UP[SCORE_CAT1];
            Score2.Image = SCORE_UP[SCORE_CAT2];
            Score3.Image = SCORE_UP[SCORE_CAT3];
            Score4.Image = SCORE_UP[SCORE_CAT4];
            Score5.Image = SCORE_UP[SCORE_CAT5];
            Score6.Image = SCORE_UP[SCORE_2P];
            Score7.Image = SCORE_UP[SCORE_3K];
            Score8.Image = SCORE_UP[SCORE_FH];
            Score9.Image = SCORE_UP[SCORE_4K];
            Score10.Image = SCORE_UP[SCORE_4D];
            Score11.Image = SCORE_UP[SCORE_5D];
            Score12.Image = SCORE_UP[SCORE_VH];

            TopBonus = false;
            TopScore = 0;
            RollCount = 0;
            TotalScore = 0;
            ScoreHundreds.Text = "";
            ScoreTens.Text = "";
            ScoreOnes.Text = "0";

            TopScoreHundreds.Text = "";
            TopScoreTens.Text = "";
            TopScoreOnes.Text = "0";

            BonusScoreTens.Text = "";
            BonusScoreOnes.Text = "";

            m_rollNumberLbl.Text = TotalScore + "";

            gameOver = false;
            int[] temp = { -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1, -1 };
            CategoryScores = temp;
            ResetDiceValues();
        }

        private void ResetDice()
        {
            foreach (Dice d in TheDice)
            {
                d.Hold = false;
            }

            foreach (PictureBox clear_box in DicePicture)
            {
                clear_box.Image = null;
            }

            Speak1.Visible = false;
            Speak2.Visible = false;
            Speak3.Visible = false;
            Speak4.Visible = false;
            Speak5.Visible = false;

            foreach (PictureBox clear_box in HoldPicture)
            {
                clear_box.Visible = false;
            }

            RollCount = 0;
            m_rollNumberLbl.Text = RollCount + "";
            Invalidate();
        }

        private void checkTopBonus()
        {
            if (!TopBonus && TopScore >= 150)
            {
                TotalScore += 25;
                TopBonus = true;
            }
        }

        private bool checkCategoriesFull()
        {
            bool allFilled = false;
            int count = 0;

            foreach (int i in CategoryScores)
            {
                if (i > -1)
                    count++;
            }
            if (count == 12)
                allFilled = true;
            return allFilled;
        }

        private void Speak_Click(object sender, EventArgs e)
        {
            if (RollCount == 0)
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

            int button_index = Int32.Parse(nameOfButton.Substring(nameOfButton.Length - 1, 1)) - 1;

            if (TheDice[button_index] != null)
            {
                TheDice[button_index].PlaySound();
            }
        }

        private void score_MouseEnter(object sender, EventArgs e)
        {
            if (!(sender is PictureBox))
            {
                return;
            }

            PictureBox clicked_pic = sender as PictureBox;
            string senderName = clicked_pic.Name;

            if (!senderName.StartsWith("Score"))
            {
                return;
            }

            if (gameOver)
            {
                if (GameID > 2)
                {
                    m_instructionsLbl.Font = new Font(m_instructionsLbl.Font.FontFamily, FONT_SIZE_SMALL_INSTRUCTIONS);
                }
                m_instructionsLbl.Text = "The current game is over. Start a new game.";
                return;
            }

            if (!(sender is PictureBox))
            {
                return;
            }

            int score_cat = Int32.Parse(clicked_pic.Tag.ToString());

            if (score_cat < 0 || score_cat > SCORE_VH)
            {
                return;
            }

            if (CategoryScores[score_cat] != -1)
            {
                if (GameID > 2)
                {
                    m_instructionsLbl.Font = new Font(m_instructionsLbl.Font.FontFamily, FONT_SIZE_SMALL_INSTRUCTIONS);
                }
                m_instructionsLbl.Text = String.Format("You have scored {0} points in this category.", CategoryScores[score_cat]);
                return;
            }

            if (RollCount == 0)
            {
                m_instructionsLbl.Text = "Roll to start a new round.";
                return;
            }

            if (GameOptions.ShowPossibleScores)
            {
                int clickScore = SCORE_FUNCS[score_cat](TheDiceValue);
                m_instructionsLbl.Font = new Font(m_instructionsLbl.Font.FontFamily, FONT_SIZE_SMALL_INSTRUCTIONS);
                m_instructionsLbl.Text = "The selected dice are worth " + clickScore + " points in this category.";
            }
            else
            {
                m_instructionsLbl.Text = "Score the selected dice in this category.";
            }
        }

        private void score_MouseLeave(object sender, EventArgs e)
        {
            m_instructionsLbl.Font = new Font(m_instructionsLbl.Font.FontFamily, FONT_SIZE_LARGE_INSTRUCTIONS);
            m_instructionsLbl.Text = "";
        }
        
        private void score_MouseUp(object sender, MouseEventArgs e)
        {
            if (!(sender is PictureBox))
            {
                return;
            }

            PictureBox clicked_pic = sender as PictureBox;
            string senderName = clicked_pic.Name;

            if (!senderName.StartsWith("Score"))
            {
                return;
            }

            int score_cat = Int32.Parse(clicked_pic.Tag.ToString());

            if (score_cat < 0 || score_cat > SCORE_VH)
            {
                return;
            }

            if (RollCount == 0)
            {
                m_instructionsLbl.Text = "Roll to start a new round.";
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            if (CategoryScores[score_cat] != -1)
            {
                m_instructionsLbl.Text = "You may only score each category once.";
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            if (scored_dice == 0)
            {
                m_instructionsLbl.Text = "You must select at least one dice to score.";
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            int clickScore = SCORE_FUNCS[score_cat](TheDiceValue);
            CategoryScores[score_cat] = clickScore;

            if (clickScore == 70 && score_cat == SCORE_VH)
            {
                new SoundPlayer(Resources.CELEBRATE_HOWL).Play();
            }

            if (clickScore > 0)
            {
                clicked_pic.Image = SCORE_SCORED[score_cat];
            }
            else
            {
                clicked_pic.Image = SCORE_BURNED[score_cat];
            }

            if (score_cat >= SCORE_CAT1 && score_cat <= SCORE_CAT5)
            {
                TopScore += clickScore;
            }

            TotalScore += clickScore;
            checkTopBonus();

            //Required for formatting correctly
            string newScoreText = String.Format("{0,3}", TotalScore);
            ScoreHundreds.Text = newScoreText.Substring(0, 1);
            ScoreTens.Text = newScoreText.Substring(1, 1);
            ScoreOnes.Text = newScoreText.Substring(2, 1);

            newScoreText = String.Format("{0,3}", TopScore);
            TopScoreHundreds.Text = newScoreText.Substring(0, 1);
            TopScoreTens.Text = newScoreText.Substring(1, 1);
            TopScoreOnes.Text = newScoreText.Substring(2, 1);

            if (TopBonus)
            {
                BonusScoreTens.Text = "2";
                BonusScoreOnes.Text = "5";
            }

            ResetDice();

            gameOver = checkCategoriesFull();
            if (gameOver)
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

                if (GameID > 2)
                {
                    m_instructionsLbl.Font = new Font(m_instructionsLbl.Font.FontFamily, FONT_SIZE_SMALL_INSTRUCTIONS);
                }
                m_instructionsLbl.Text = "The current game is over. Start a new game.";
                ResetForRoll();
            }

            Invalidate();
            RollCount = 0;
        }

        private void Hold_MouseEnter(object sender, EventArgs e)
        {
            if (RollCount >= 3)
            {
                m_instructionsLbl.Text = "Click here to remove this die for scoring.";
            }
            else
            {
                m_instructionsLbl.Text = "Click here to allow this dice to roll.";
            }
        }

        private void Hold_MouseLeave(object sender, EventArgs e)
        {
            m_instructionsLbl.Text = "";
        }

        private void Hold_MouseUp(object sender, MouseEventArgs e)
        {
            if (RollCount == 0)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            if (!(sender is PictureBox))
            {
                return;
            }

            PictureBox clicked_pic = sender as PictureBox;

            string pic_name = clicked_pic.Name;

            if (!pic_name.StartsWith("Hold"))
            {
                return;
            }

            int which_dice = Int32.Parse(clicked_pic.Tag.ToString());

            if (which_dice < 0 || which_dice > 4)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                TheDice[which_dice].Hold = !TheDice[which_dice].Hold;
                HoldPicture[which_dice].BackgroundImage = DicePicture[which_dice].Image;
                HoldPicture[which_dice].Visible = TheDice[which_dice].Hold;
            }

            CheckScoredDice();
        }

        private void Dice_MouseEnter(object sender, EventArgs e)
        {
            if (gameOver)
            {
                if (GameID > 2)
                {
                    m_instructionsLbl.Font = new Font(m_instructionsLbl.Font.FontFamily, FONT_SIZE_SMALL_INSTRUCTIONS);
                }
                m_instructionsLbl.Text = "The current game is over. Start a new game.";
                return;
            }

            if (!(sender is PictureBox))
            {
                return;
            }

            PictureBox clicked_pic = sender as PictureBox;

            if (!clicked_pic.Name.StartsWith("Dice"))
            {
                return;
            }

            if (RollCount == 0)
            {
                m_instructionsLbl.Text = "Roll to start a new round.";
                return;
            }

            if (RollCount >= 3)
            {
                m_instructionsLbl.Text = "Click here to select this die for scoring.";
                return;
            }

            m_instructionsLbl.Text = "Click to hold this dice on the next roll.";
        }

        private void Dice_MouseLeave(object sender, EventArgs e)
        {
            m_instructionsLbl.Font = new Font(m_instructionsLbl.Font.FontFamily, FONT_SIZE_LARGE_INSTRUCTIONS);
            m_instructionsLbl.Text = "";
        }

        private void Dice_MouseUp(object sender, MouseEventArgs e)
        {
            if (gameOver)
            {
                m_instructionsLbl.Text = "The current game is over. Start a new game.";
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            if (!(sender is PictureBox))
            {
                return;
            }

            PictureBox clicked_pic = sender as PictureBox;

            if (!clicked_pic.Name.StartsWith("Dice"))
            {
                return;
            }

            if (RollCount == 0)
            {
                m_instructionsLbl.Text = "Roll to start a new round.";
                return;
            }

            m_instructionsLbl.Text = "";

            int which_dice = Int32.Parse(clicked_pic.Tag.ToString());

            if (which_dice < 0 || which_dice > 4)
            {
                return;
            }

            if (e.Button == MouseButtons.Left)
            {
                TheDice[which_dice].Hold = !TheDice[which_dice].Hold;
                HoldPicture[which_dice].BackgroundImage = DicePicture[which_dice].Image;
                HoldPicture[which_dice].Visible = TheDice[which_dice].Hold;
            }

            CheckScoredDice();
        }

        private void Roll_MouseEnter(object sender, EventArgs e)
        {
            if (gameOver)
            {
                m_instructionsLbl.Font = new Font(m_instructionsLbl.Font.FontFamily, FONT_SIZE_SMALL_INSTRUCTIONS);
                m_instructionsLbl.Text = "The current game is over. Click here to start a new game.";
            }
            else
            {
                if (RollCount >= 3)
                {
                    m_instructionsLbl.Text = "Please score this roll.";
                }
                else
                {
                    m_instructionsLbl.Text = "Click to roll the dice.";
                }
            }
        }

        private void Roll_MouseLeave(object sender, EventArgs e)
        {
            m_instructionsLbl.Font = new Font(m_instructionsLbl.Font.FontFamily, FONT_SIZE_LARGE_INSTRUCTIONS);
            m_instructionsLbl.Text = "";
        }

        private void DiceRoller(object dice_index_obj)
        {
            int dice_index = (int)dice_index_obj;

            int num_rolls = rand.Next(MAX_ROLL_ITERATION / 2, MAX_ROLL_ITERATION);

            for (int roll_iter = 0; roll_iter < num_rolls; roll_iter++)
            {
                if (test_index == -1)
                {
                    TheDice[dice_index].Roll(rand.Next(0, 6));
                }
                else
                {
                    TheDice[dice_index].Roll(test_index);
                }

                DicePicture[dice_index].Invoke(new EventHandler(ThreadedImageChanged), null);
                Thread.Sleep(rand.Next(MAX_ROLL_SLEEP / 4, MAX_ROLL_SLEEP));
            }

            lock (roll_lock)
            {
                roll_in_progress++;
            }
        }

        private void ThreadedImageChanged(object sender, EventArgs e)
        {
            if (!(sender is PictureBox))
            {
                return;
            }

            PictureBox change_pic = sender as PictureBox;
            int dice_index = Int32.Parse(change_pic.Tag.ToString());
            change_pic.Image = TheDice[dice_index].currentFace.Pic;
        }

        private void Roll_MouseUp(object sender, MouseEventArgs e)
        {
            lock (roll_lock)
            {
                if (gameOver)
                {
                    SetupNewGame();
                }

                if (RollCount >= 3 || roll_in_progress != -1)
                {
                    if (gameOver)
                    {
                        m_instructionsLbl.Text = "The current game is over. Start a new game.";
                    }

                    if (RollCount >= 3)
                    {
                        m_instructionsLbl.Text = "Please score this roll.";
                    }

                    new SoundPlayer(Resources.BUZZ).Play();
                    return;
                }
                roll_in_progress = 0;
            }

            int dice_index;

            roll_in_progress_target = 0;

            for (dice_index = 0; dice_index < 5; dice_index++)
            {
                if (!TheDice[dice_index].Hold)
                {
                    Thread dice_thread = new Thread(new ParameterizedThreadStart(DiceRoller));
                    dice_thread.Start(dice_index);
                    roll_in_progress_target++;
                }
            }

            new SoundPlayer(Resources.DICE_ROLL).Play();
            check_finished_roll.Start();

            Speak1.Visible = true;
            Speak2.Visible = true;
            Speak3.Visible = true;
            Speak4.Visible = true;
            Speak5.Visible = true;
        }

        private void FinishRoll(object sender, EventArgs e)
        {
            lock (roll_lock)
            {
                if (roll_in_progress < roll_in_progress_target)
                {
                    return;
                }
            }

            check_finished_roll.Stop();

            CheckScoredDice();

            RollCount++;

            m_rollNumberLbl.Text = RollCount + "";

            if (RollCount == 3)
            {
                m_instructionsLbl.Text = "Please score this roll.";
            }

            if (test_index > -1)
            {
                test_index++;
                if (test_index > 5)
                {
                    test_index = 0;
                }
                button8.Text = String.Format("Test: {0}", test_index);
            }

            lock (roll_lock)
            {
                roll_in_progress = -1;
            }
        }

        private void CheckScoredDice()
        {
            ResetDiceValues();

            for (int dice_index = 0; dice_index < 5; dice_index++)
            {
                if (TheDice[dice_index].Hold)
                {
                    TheDiceValue[TheDice[dice_index].Value]++;
                    scored_dice++;
                }
            }
        }

        private void MenuPictureBox_Click(object sender, EventArgs e)
        {
            this.Close();
        }

        private void button8_Click(object sender, EventArgs e)
        {
            test_index++;
            if (test_index > 5)
            {
                test_index = 0;
            }

            button8.Text = String.Format("Test: {0}", test_index);
        }

        private string keypresses = "";

        private void VowelHowlForm_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (keypresses == "test")
                {
                    button1.Visible = true;
                    button2.Visible = true;
                    button3.Visible = true;
                    button4.Visible = true;
                    button5.Visible = true;
                    button6.Visible = true;
                    button7.Visible = true;
                    button8.Visible = true;
                    button9.Visible = true;
                }

                keypresses = "";

                e.Handled = true;
            }

            if (char.IsLetter(e.KeyChar))
            {
                keypresses += e.KeyChar;
                e.Handled = true;
            }
        }

        private void VowelHowlForm_Paint(object sender, PaintEventArgs e)
        {
            if (TopBonus)
            {
                Graphics g = e.Graphics;
                SolidBrush bonus_brush = new SolidBrush(Color.FromArgb(48, Color.Red));

                g.FillRectangle(bonus_brush, 738, 484, 19, 20);
                g.FillRectangle(bonus_brush, 760, 484, 19, 20);
            }

        }

        private void VowelHowlForm_Load(object sender, EventArgs e)
        {

        }
    }
}
