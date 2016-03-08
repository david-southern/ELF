using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using ELF_Resources.Properties;
using System.Threading;
using System.IO;

namespace ELF
{
    public partial class WordBuilderForm : Form
    {
        private List<string> StartingSounds;
        private string[] all_starting_sounds = new string[]
        { 
            "b", "bl", "br", "c", "ch", "cl", "cr", "d", "dr",
            "f", "fl", "g", "gr", "h", "j", "l", "m", "n",
            "p", "pl", "pr", "r", "s", "sc", "sh", "sl", "sn",
            "sp", "st", "str", "sw", "t", "th", "tr", "v", "w", "wh"

        };

        private string[] beginner_starting_sounds = new string[]
        {
            "b", "c", "ch", "d", "f", "g", "h", "j", "l", "m", "n",
            "r", "s", "sh", "t", "th", "v", "w", "w", "wh"
        };

        List<string> EndingSounds;
        string[] all_ending_sounds = new string[]
        {
            "ab", "ack", "ad", "ag", "ail", "ain", "ake", "ale",
            "all", "am", "ame", "an", "ank", "ap", "ar", "are",
            "at", "ate", "ave", "aw", "ay", "eak", "ear", "eat",
            "ed", "ee", "eed", "eep", "eer", "eet", "ell", "en",
            "end", "ent", "est", "et", "ew", "ice", "ick", "id",
            "ide", "ig", "ight", "ike", "ile", "ill", "im", "in",
            "ine", "ing", "ink", "ip", "it", "ite", "ive", "ob",
            "ock", "og", "one", "ood", "ook", "oop", "oot", "op",
            "ope", "ore", "ose", "ot", "out", "ow", "ub", "uck",
            "ue", "uff", "ug", "ump", "un", "ust", "ut"
        };

        private string[] beginner_ending_sounds = new string[]
        {
            "ack", "ad", "ag", "ail", "ake", "all", "ame", "an",
            "ap", "ar", "are", "at", "ate", "ave", "aw", "ear",
            "eat", "ed", "eed", "ell", "en", "end", "ent", "est",
            "et", "ice", "ick", "ide", "ig", "ight", "ike", "ill",
            "in", "ine", "ing", "ink", "ip", "it", "ob", "ock",
            "og", "one", "ook", "oot", "op", "ope", "ose", "ot",
            "ow", "ub", "ug", "ump", "un", "ut"
        };

        private string[] generate_starting_sounds;
        private string[] generate_ending_sounds;

        Dictionary<string, bool> IgnoredWords = new Dictionary<string, bool>();
        string[] ignored_words = new string[]
        {
            "bock", "dow", "gop", "jan", "jew", "jim", "jill", "lat",
            "mag", "med", "mell", "mig", "min", "ming", "mit", "pob", "sam",
            "san", "tay", "tim", "veep", "vide", "vip", "cree", "blent", "brab",
            "drear", "grot", "prot", "scag", "scop", "spain", "stan", "stat",
            "stook", "swed", "trop", "vail", "tue", "bing", "dan", "fide",
            "jake", "jose", "tope", "jun", "wight"
        };

        Dictionary<string, bool> AllowedWords = new Dictionary<string, bool>();
        string[] allowed_words = new string[]
        {
            "cock", "muff"
        };

        private int TEXT_IMAGE_WIDTH;
        private int TEXT_IMAGE_HEIGHT;

        private int BOARD_FONT_SIZE = 54;
        private int CARD_FONT_SIZE = 72;

        private int GENIUS_STARS_NEEDED = 8;

        private bool GameOver = false;
        private int TotalScore = 0;
        private bool FreezeBoard = false;

        List<string> DrawDeck;
        int DrawIndex;
        int GeniusStarCount;
        int GeniusGoalCount;
        int TotalWords;
        int WordErrors;

        int BASE_CARD_TIMER_SECONDS;
        DateTime card_start_time;

        Dictionary<string, bool> FoundWords = new Dictionary<string, bool>();

        string[] Board = new string[9];
        PictureBox[] BoardControls = new PictureBox[9];
        PictureBox[] GeniusStars = new PictureBox[10];

        Font board_font = null;
        Font card_font = null;

        string HighScorePrefix = "GWB";

        public WordBuilderForm(int difficulty)
        {
            HighScorePrefix += difficulty.ToString();

            if (difficulty == 0)
            {
                BASE_CARD_TIMER_SECONDS = int.MaxValue;
            }
            else
            {
                BASE_CARD_TIMER_SECONDS = 26 - difficulty * 6;
            }

            InitializeComponent();

            if (difficulty == 0)
            {
                StartingSounds = new List<string>(beginner_starting_sounds);
                EndingSounds = new List<string>(beginner_ending_sounds);
            }
            else
            {
                StartingSounds = new List<string>(all_starting_sounds);
                EndingSounds = new List<string>(all_ending_sounds);
            }

            StartingSounds.Sort();
            EndingSounds.Sort();

            generate_starting_sounds = StartingSounds.ToArray();
            generate_ending_sounds = EndingSounds.ToArray();

            foreach (string init_sound in StartingSounds)
            {
                StartingList.Items.Add(init_sound);
            }
            StartingList.Visible = false;

            foreach (string end_sound in EndingSounds)
            {
                EndingList.Items.Add(end_sound);
            }
            EndingList.Visible = false;
            button1.Visible = false;
            label1.Visible = false;
            label2.Visible = false;

            BoardControls[0] = Board_0_0;
            BoardControls[1] = Board_1_0;
            BoardControls[2] = Board_2_0;
            BoardControls[3] = Board_0_1;
            BoardControls[4] = Board_1_1;
            BoardControls[5] = Board_2_1;
            BoardControls[6] = Board_0_2;
            BoardControls[7] = Board_1_2;
            BoardControls[8] = Board_2_2;

            GeniusStars[0] = Genius0;
            GeniusStars[1] = Genius1;
            GeniusStars[2] = Genius2;
            GeniusStars[3] = Genius3;
            GeniusStars[4] = Genius4;
            GeniusStars[5] = Genius5;
            GeniusStars[6] = Genius6;
            GeniusStars[7] = Genius7;
            GeniusStars[8] = Genius8;
            GeniusStars[9] = Genius9;

            TEXT_IMAGE_WIDTH = Board_0_0.Width;
            TEXT_IMAGE_HEIGHT = Board_0_0.Height;

            foreach (string ignored_word in ignored_words)
            {
                IgnoredWords.Add(ignored_word, true);
            }

            foreach (string allowed_word in allowed_words)
            {
                AllowedWords.Add(allowed_word, true);
            }

            InitializeGame();
        }

        public void InitializeGame()
        {
            Utils.Shuffle<string>(EndingSounds);

            if (EndingList.Visible)
            {
                List<string> selected_sounds = new List<string>();
                foreach (object selected_item in EndingList.SelectedItems)
                {
                    selected_sounds.Add((string)selected_item);
                }

                EndingSounds.Sort(delegate(string s1, string s2)
                {
                    bool s1_selected = selected_sounds.Contains(s1);
                    bool s2_selected = selected_sounds.Contains(s2);

                    if (s1_selected == s2_selected)
                    {
                        return 0;
                    }

                    return s1_selected ? -1 : 1;
                });
            }

            BuildBoard();

            List<string> valid_word_starts;

            int valid_threshold = 3;

            if (StartingList.Visible)
            {
                valid_word_starts = new List<string>(StartingSounds);

                List<string> selected_sounds = new List<string>();
                foreach (object selected_item in StartingList.SelectedItems)
                {
                    selected_sounds.Add((string)selected_item);
                }

                valid_word_starts.Sort(delegate(string s1, string s2)
                {
                    bool s1_selected = selected_sounds.Contains(s1);
                    bool s2_selected = selected_sounds.Contains(s2);

                    if (s1_selected == s2_selected)
                    {
                        return 0;
                    }

                    return s1_selected ? -1 : 1;
                });
            }
            else
            {
                do
                {
                    valid_word_starts = new List<string>(StartingSounds);

                    valid_word_starts.RemoveAll(delegate(string check_word_start)
                    {
                        int valid_word_count = 0;
                        for (int ending_index = 0; ending_index < 9; ending_index++)
                        {
                            string check_word = check_word_start + Board[ending_index];
                            if (IsValidWord(check_word))
                            {
                                valid_word_count++;
                            }
                        }
                        return valid_word_count < valid_threshold;
                    });

                    valid_threshold--;
                } while (valid_word_starts.Count < 10);

                Utils.Shuffle<string>(valid_word_starts);
            }

            DrawDeck = valid_word_starts.GetRange(0, 10);
            DrawIndex = -1;
            TotalScore = 0;
            GameOver = false;
            FreezeBoard = false;
            TimeLabel.Text = "";

            if (flash_timer != null && flash_timer.Enabled)
            {
                flash_timer.Stop();
                flash_picture.Visible = true;
            }

            if (card_timer != null && card_timer.Enabled)
            {
                card_timer.Stop();
            }

            CardsLeft.Text = (DrawDeck.Count - (DrawIndex + 1)).ToString();
            CurrentCard.Image = null;

            for (int star_index = 0; star_index < 10; star_index++)
            {
                GeniusStars[star_index].Image = Resources.GENIUS_STAR_GREY;
            }
            GeniusStarCount = 0;
            GeniusGoalCount = 0;
            TotalWords = -1;
            DrawCardButton.Text = "Draw a Card";
            GeniusAward.Visible = false;
        }

        private void BuildBoard()
        {
            for (int board_index = 0; board_index < 9; board_index++)
            {
                Board[board_index] = EndingSounds[board_index];
                BoardControls[board_index].Image = BuildWordImage(Board[board_index], BOARD_FONT_SIZE);
            }
        }

        private Bitmap BuildWordImage(string word, int font_size)
        {
            Bitmap image_map = new Bitmap(TEXT_IMAGE_WIDTH, TEXT_IMAGE_HEIGHT);

            Graphics g = Graphics.FromImage(image_map);

            if (board_font == null)
            {
                board_font = new Font("Film Cryptic", BOARD_FONT_SIZE);
                card_font = new Font("Film Cryptic", CARD_FONT_SIZE);
            }

            g.TextRenderingHint = System.Drawing.Text.TextRenderingHint.AntiAliasGridFit;

            Font render_font = font_size == BOARD_FONT_SIZE ? board_font : card_font;

            SizeF text_size = g.MeasureString(word, render_font);

            g.DrawString(word, render_font, Brushes.White,
                (TEXT_IMAGE_WIDTH - text_size.Width) / 2, (TEXT_IMAGE_HEIGHT - text_size.Height) / 2);

            g.Dispose();

            return image_map;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            NotesDialog note = new NotesDialog();

            string words_results = "";
            List<string> all_words = new List<string>();
            List<string> censored_words = new List<string>();
            List<string> invalid_words = new List<string>();
            List<string> valid_words = new List<string>();

            foreach (string starter in generate_starting_sounds)
            {
                foreach (string ender in generate_ending_sounds)
                {
                    string word = starter + ender;

                    all_words.Add(word);

                    if (Utils.IsCensoredWord(word) && !AllowedWords.ContainsKey(word))
                    {
                        censored_words.Add(word);
                    }
                    else if (IsValidWord(word))
                    {
                        valid_words.Add(word);
                    }
                    else
                    {
                        invalid_words.Add(word);
                    }

                }
            }

            words_results = String.Format(
                "Full join of all startings and endings results in:\r\n"
                + "    {0} Total Words\r\n"
                + "    {1} Words recognized by our dictionary\r\n"
                + "    {2} Words not recognized by our dictionary\r\n"
                + "    {3} words deemed to be offensive\r\n\r\nDictionary Words:",
                all_words.Count, valid_words.Count, invalid_words.Count, censored_words.Count);

            bool newline = true;
            int word_count = 0;

            foreach (string this_word in valid_words)
            {
                if (newline)
                {
                    words_results += "\r\n";
                    newline = false;
                    word_count = 0;
                }
                else
                {
                    words_results += ", ";
                }

                words_results += this_word;

                word_count++;

                if (word_count > 9)
                {
                    newline = true;
                }
            }

            words_results += "\r\n\r\nNon-Dictionary Words:";

            newline = true;
            word_count = 0;

            foreach (string this_word in invalid_words)
            {
                if (newline)
                {
                    words_results += "\r\n";
                    newline = false;
                    word_count = 0;
                }
                else
                {
                    words_results += ", ";
                }

                words_results += this_word;

                word_count++;

                if (word_count > 9)
                {
                    newline = true;
                }
            }

            words_results += "\r\n\r\nCensored Words:";

            newline = true;
            word_count = 0;

            foreach (string this_word in censored_words)
            {
                if (newline)
                {
                    words_results += "\r\n";
                    newline = false;
                    word_count = 0;
                }
                else
                {
                    words_results += ", ";
                }

                words_results += this_word;

                word_count++;

                if (word_count > 9)
                {
                    newline = true;
                }
            }

            note.NotesText.Text = words_results;

            note.ShowDialog();
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void RenderScore()
        {
            string score_text = String.Format("{0,3}", TotalScore);
            ScoreHundreds.Text = score_text.Substring(0, 1);
            ScoreTens.Text = score_text.Substring(1, 1);
            ScoreOnes.Text = score_text.Substring(2, 1);
        }

        private DateTime last_draw_click = DateTime.MinValue;

        private void DrawCard_Click(object sender, EventArgs e)
        {
            if (GameOver)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            TimeSpan time_since_last_click = DateTime.Now - last_draw_click;

            if (time_since_last_click.TotalSeconds < 1)
            {
                return;
            }

            last_draw_click = DateTime.Now;

            if (flash_timer != null && flash_timer.Enabled)
            {
                return;
            }

            if (card_timer == null)
            {
                card_timer = new System.Windows.Forms.Timer();
                card_timer.Interval = 500;
                card_timer.Tick += new EventHandler(UpdateCardTimer);
            }

            card_timer.Stop();

            FreezeBoard = false;

            if (TotalWords != -1)
            {
                if (FoundWords.Count > 0 && 
                    (GameOptions.GWB_RequireAllWordsFound || FoundWords.Count == TotalWords) && WordErrors == 0)
                {
                    GeniusStars[GeniusStarCount++].Image = Resources.GENIUS_STAR;
                    GeniusGoalCount++;

                    if (GameOptions.GWB_RequireAllWordsFound)
                    {
                        TotalScore += FoundWords.Count * 5;

                        if (FoundWords.Count == TotalWords)
                        {
                            TotalScore += 20;
                        }

                        if (FoundWords.Count < TotalWords)
                        {
                            for (int board_index = 0; board_index < 9; board_index++)
                            {
                                string check_word = DrawDeck[DrawIndex] + Board[board_index];

                                if (IsValidWord(check_word) && !FoundWords.ContainsKey(check_word))
                                {
                                    if (MissedWords.Text.Length > 0)
                                    {
                                        MissedWords.Text += ", ";
                                    }

                                    MissedWords.Text += check_word;
                                }
                            }
                        }
                    }
                    else
                    {
                        TotalScore += 50;
                    }
                    RenderScore();

                    if (GeniusGoalCount == GENIUS_STARS_NEEDED)
                    {
                        GeniusAward.Visible = true;
                        new SoundPlayer(Resources.APPLAUSE_SHORT).Play();
                        // Flash(GeniusAward);
                        GeniusAward.Visible = true;
                        TotalScore += 100;
                        RenderScore();
                    }
                    else
                    {
                        new SoundPlayer(Resources.CHING).Play();
                        Flash(GeniusStars[GeniusStarCount - 1]);
                    }
                }
                else
                {
                    GeniusStars[GeniusStarCount++].Image = Resources.GENIUS_STAR_BLACK;
                    Flash(GeniusStars[GeniusStarCount - 1]);
                    new SoundPlayer(Resources.DULL_GONG).Play();

                    if (FoundWords.Count < TotalWords)
                    {
                        for (int board_index = 0; board_index < 9; board_index++)
                        {
                            string check_word = DrawDeck[DrawIndex] + Board[board_index];

                            if (IsValidWord(check_word) && !FoundWords.ContainsKey(check_word))
                            {
                                if (MissedWords.Text.Length > 0)
                                {
                                    MissedWords.Text += ", ";
                                }

                                MissedWords.Text += check_word;

                                if (GameOptions.GWB_RequireAllWordsFound)
                                {
                                    TotalScore -= 5;
                                }
                            }
                        }

                        if (TotalScore < 0)
                        {
                            TotalScore = 0;
                        }

                    }
                }

                if (DrawIndex >= DrawDeck.Count - 1)
                {
                    GameOver = true;

                    HighScoreTable highScoreTable = new HighScoreTable(HighScorePrefix);
                    highScoreTable.Load();
                    int score_index = highScoreTable.GetIndexOfScore(TotalScore);

                    if (score_index != -1)
                    {
                        GetName name_form = new GetName();
                        name_form.ShowDialog();

                        highScoreTable.Update(name_form.PlayerName.Text, TotalScore);
                        highScoreTable.Save();
                    }

                    return;
                }

                DrawCardButton.Text = "Draw a Card";
                TotalWords = -1;
                FreezeBoard = true;
                return;
            }

            if (GameOver || DrawIndex >= DrawDeck.Count - 1)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            DrawIndex++;
            CurrentCard.Image = BuildWordImage(DrawDeck[DrawIndex], CARD_FONT_SIZE);

            CardsLeft.Text = (DrawDeck.Count - (DrawIndex + 1)).ToString();

            ResultsText.Text = "";
            MissedWords.Text = "";

            FoundWords.Clear();
            WordErrors = 0;

            TotalWords = 0;

            for (int board_index = 0; board_index < 9; board_index++)
            {
                string check_word = DrawDeck[DrawIndex] + Board[board_index];

                if (IsValidWord(check_word))
                {
                    if (StartingList.Visible)
                    {
                        MissedWords.Text += check_word + " ";
                    }

                    TotalWords++;
                }
            }

            DrawCardButton.Text = "Check Results";

            card_start_time = DateTime.Now;

            if (BASE_CARD_TIMER_SECONDS == int.MaxValue)
            {
                TimeLabel.Text = "Unlimited";
            }
            else
            {
                TimeLabel.Text = String.Format("{0} seconds", BASE_CARD_TIMER_SECONDS);
                card_timer.Start();
            }
            TimeLabel.ForeColor = Color.White;
        }

        System.Windows.Forms.Timer card_timer = null;

        private void UpdateCardTimer(object sender, EventArgs e)
        {
            TimeSpan elapsed_time = DateTime.Now - card_start_time;

            int time_remaining = BASE_CARD_TIMER_SECONDS - (int)Math.Round(elapsed_time.TotalSeconds);

            if (time_remaining < 0)
            {
                DrawCard_Click(null, null);
                return;
            }

            TimeLabel.Text = String.Format("{0} second{1}", time_remaining, time_remaining != 1 ? "s" : "");
            if (time_remaining < 5)
            {
                TimeLabel.ForeColor = Color.Red;
            }
        }

        private static readonly int FLASH_MILLIS = 100;
        private static readonly int FLASH_TIMES = 10;
        System.Windows.Forms.Timer flash_timer = null;
        int flash_count;
        PictureBox flash_picture;

        private void UpdateFlash(object sender, EventArgs e)
        {
            flash_picture.Visible = !flash_picture.Visible;
            flash_count++;

            if (flash_count == FLASH_TIMES)
            {
                flash_timer.Stop();
                flash_picture.Visible = true;
            }
        }

        private void Flash(PictureBox pictureBox)
        {
            flash_picture = pictureBox;
            flash_count = 0;

            if (flash_timer == null)
            {
                flash_timer = new System.Windows.Forms.Timer();
                flash_timer.Tick += new EventHandler(UpdateFlash);
                flash_timer.Interval = FLASH_MILLIS;
            }
            flash_timer.Start();
        }

        private bool IsValidWord(string check_word)
        {
            return Utils.CheckDictionaryWord(check_word)
                && (!Utils.IsCensoredWord(check_word) || AllowedWords.ContainsKey(check_word))
                && !IsIgnoredWord(check_word);
        }

        private bool IsIgnoredWord(string check_word)
        {
            return IgnoredWords.ContainsKey(check_word);
        }

        private void Board_Click(object sender, EventArgs e)
        {
            PictureBox board_button = sender as PictureBox;

            if (board_button == null)
            {
                return;
            }

            if (!board_button.Name.StartsWith("Board"))
            {
                return;
            }

            if (DrawIndex < 0 || GameOver)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            if (FreezeBoard)
            {
                new SoundPlayer(Resources.DULL_GONG).Play();
                return;
            }

            int board_index = Int32.Parse(board_button.Tag.ToString());

            if (board_index < 0 || board_index > 8)
            {
                return;
            }

            string check_word = DrawDeck[DrawIndex] + Board[board_index];

            bool valid_word = IsValidWord(check_word);

            if (valid_word)
            {
                if (FoundWords.ContainsKey(check_word))
                {
                    new SoundPlayer(Resources.DONG).Play();
                    return;
                }

                TotalScore += 10;

                if (ResultsText.Text.Length > 0)
                {
                    ResultsText.Text += ", ";
                }

                ResultsText.Text += check_word;

                FoundWords[check_word] = true;
            }
            else
            {
                TotalScore -= 5;
                WordErrors++;

                if (TotalScore < 0)
                {
                    TotalScore = 0;
                }
            }

            CheckForFinishedCard();

            RenderScore();

            if (!GameOver)
            {
                if (valid_word)
                {
                    new SoundPlayer(Resources.DING).Play();
                }
                else
                {
                    new SoundPlayer(Resources.BUZZ).Play();
                }
            }
        }

        private void CheckForFinishedCard()
        {
        }

        private string keypresses = "";

        private void TestMode_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (e.KeyChar == (char)Keys.Tab)
            {
                if (keypresses == "test")
                {
                    StartingList.Visible = true;
                    EndingList.Visible = true;
                    button1.Visible = true;
                    label1.Visible = true;
                    label2.Visible = true;

                    // GENIUS_STARS_NEEDED = 1;
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

        private void ExitButton_Click(object sender, EventArgs e)
        {
            this.Close();
        }
    }
}
