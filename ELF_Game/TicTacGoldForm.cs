using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.IO;
using ELF_Resources.Properties;

namespace ELF
{
    public partial class TicTacGoldForm : Form
    {
        private static readonly int TOTAL_GAMES_PER_MATCH = 3;

        private string[] HighScorePrefix = 
        {
            "TTG",
            "TTG",
            "TTG",
            "TTG",
            "TTG",
            "TTG",
            "TTG",
            "TTG",
            "TTG_LI",
            "TTG_LI"
        };

        private int GameID;
        private bool GameOver = false;
        private int TotalScore = 0;
        private int BonusScore = 0;
        public bool PlayBlackout = false;

        private int MatchGameCount;
        private int MatchScore;

        List<PhonicsCard> DrawDeck;
        int DrawIndex;

        private enum BoardStateType
        {
            Empty,
            Right,
            Wrong
        }

        PhonicsCard[] Board = new PhonicsCard[9];
        BoardStateType[] BoardState = new BoardStateType[9];
        PictureBox[] BoardControls = new PictureBox[9];

        public TicTacGoldForm(int game_id)
        {
            GameID = game_id;
            InitializeComponent();

            BoardControls[0] = Board_0_0;
            BoardControls[1] = Board_1_0;
            BoardControls[2] = Board_2_0;
            BoardControls[3] = Board_0_1;
            BoardControls[4] = Board_1_1;
            BoardControls[5] = Board_2_1;
            BoardControls[6] = Board_0_2;
            BoardControls[7] = Board_1_2;
            BoardControls[8] = Board_2_2;

            GameOver = false;

            InitializeMatch();
        }

        private void InitializeMatch()
        {
            if (!GameOver || MatchGameCount != TOTAL_GAMES_PER_MATCH)
            {
                if (MatchScore + TotalScore != 0)
                {
                    if (MessageBox.Show(
                        "Are you sure you want to start a new match?  You will lose your current progress!",
                        "Start New Match?", MessageBoxButtons.YesNo) != DialogResult.Yes)
                    {
                        return;
                    }
                }
            }

            MatchGameCount = 0;
            MatchScore = 0;
            TotalScore = 0;
            BonusScore = 10;

            InitializeGame();
        }

        private void InitializeGame()
        {
            InitializeGameContent();

            GameOver = false;

            Utils.Shuffle<PhonicsCard>(DrawDeck);

            for (int board_index = 0; board_index < 9; board_index++)
            {
                Board[board_index] = DrawDeck[board_index];
                BoardState[board_index] = BoardStateType.Empty;
                BoardControls[board_index].BackgroundImage = Board[board_index].BoardImage;
                BoardControls[board_index].Image = null;
            }

            // Make sure that there are no cards in the draw deck that do
            // not appear on the board
            while (DrawDeck.Count > 9)
            {
                DrawDeck.RemoveAt(9);
            }

            Utils.Shuffle<PhonicsCard>(DrawDeck);

            DrawIndex = -1;

            CardsLeft.Text = (DrawDeck.Count - (DrawIndex + 1)).ToString();

            TotalScore = 0;
            BonusScore = 10;
            MatchGameCount++;
            StartNewGame.Enabled = false;

            RenderScore();

            CurrentCard.Image = null;
        }

        private void RenderScore()
        {
            string score_text = String.Format("{0,2}", TotalScore);
            ScoreTens.Text = score_text.Substring(0, 1);
            ScoreOnes.Text = score_text.Substring(1, 1);

            score_text = String.Format("{0,2}", MatchScore);
            MatchScoreTens.Text = score_text.Substring(0, 1);
            MatchScoreOnes.Text = score_text.Substring(1, 1);

            MatchGame.Text = MatchGameCount.ToString();
        }

        private void PlayBoardSound(object sender, EventArgs e)
        {
            PictureBox sound_button = sender as PictureBox;

            if (sound_button == null)
            {
                return;
            }

            if (!sound_button.Name.StartsWith("Sound"))
            {
                return;
            }

            int board_index = Int32.Parse(sound_button.Tag.ToString());

            if (board_index < 0 || board_index > 8)
            {
                return;
            }

            Board[board_index].BoardSound.Play();
        }

        private void CardSound_Click(object sender, EventArgs e)
        {
            if (DrawIndex < 0)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            DrawDeck[DrawIndex].DeckSound.Play();
        }

        private void DrawCard_Click(object sender, EventArgs e)
        {
            if (GameOver || DrawIndex >= DrawDeck.Count - 1)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            DrawIndex++;
            CurrentCard.Image = DrawDeck[DrawIndex].DeckImage;

            CardsLeft.Text = (DrawDeck.Count - (DrawIndex + 1)).ToString();

            if (DrawIndex >= DrawDeck.Count - 1 && MatchGameCount < TOTAL_GAMES_PER_MATCH)
            {
                StartNewGame.Enabled = true;
            }
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

            if (DrawIndex < 0 || GameOver || board_button.Image != null)
            {
                new SoundPlayer(Resources.BUZZ).Play();
                return;
            }

            int board_index = Int32.Parse(board_button.Tag.ToString());

            if (board_index < 0 || board_index > 8)
            {
                return;
            }

            BoardStateType result = BoardStateType.Empty;

            if (Board[board_index].Phoneme == DrawDeck[DrawIndex].Phoneme)
            {
                board_button.Image = Resources.GREEN_CHECK;
                BoardState[board_index] = BoardStateType.Right;
                TotalScore += 2;
                result = BoardStateType.Right;
            }
            else
            {
                if (GameOptions.TTG_CancelSquareOnMisClick)
                {
                    board_button.Image = Resources.RED_CROSS;
                }

                TotalScore -= 1;
                BonusScore -= 2;

                result = BoardStateType.Wrong;

                if (TotalScore < 0)
                {
                    TotalScore = 0;
                }
            }

            CheckForWin();

            if (!GameOver)
            {
                // Don't play the board click sound till after we've checked for a win.  If they did
                // win then CheckForWin will play the applause, and we don't want to play the DING/BUZZ
                // in that case
                if (result == BoardStateType.Right)
                {
                    new SoundPlayer(Resources.DING).Play();
                }
                else
                {
                    new SoundPlayer(Resources.BUZZ).Play();
                }


                if (GameOptions.TTG_CancelSquareOnMisClick)
                {
                    GameOver = true;
                    for (board_index = 0; board_index < 9; board_index++)
                    {
                        if (BoardControls[board_index].Image == null)
                        {
                            GameOver = false;
                            break;
                        }
                    }
                }
                else
                {
                    int cardsLeft = (DrawDeck.Count - (DrawIndex + 1));
                    GameOver = cardsLeft <= 0 && result == BoardStateType.Right;
                }
            }

            RenderScore();

            if (GameOver)
            {
                MatchScore += TotalScore;

                RenderScore();

                if (MatchGameCount == TOTAL_GAMES_PER_MATCH)
                {
                    HighScoreTable highScoreTable = new HighScoreTable(HighScorePrefix[GameID]);
                    highScoreTable.Load();
                    int score_index = highScoreTable.GetIndexOfScore(MatchScore);

                    if (score_index != -1)
                    {
                        GetName name_form = new GetName();
                        name_form.ShowDialog();

                        highScoreTable.Update(name_form.PlayerName.Text, MatchScore);
                        highScoreTable.Save();
                    }

                }
                else
                {
                    StartNewGame.Enabled = true;
                }

            }
        }

        private void CheckForWin()
        {
            if (PlayBlackout)
            {
                bool game_won = true;

                for (int board_index = 0; board_index < 9; board_index++)
                {
                    if (BoardState[board_index] != BoardStateType.Right)
                    {
                        game_won = false;
                        break;
                    }
                }

                if (game_won)
                {
                    GameOver = true;

                    if (BonusScore > 0)
                    {
                        TotalScore += BonusScore;
                    }
                    new SoundPlayer(Resources.APPLAUSE_SHORT).Play();
                }

            }
            else
            {
                if (
                    (BoardState[0] == BoardStateType.Right
                    && BoardState[3] == BoardStateType.Right
                    && BoardState[6] == BoardStateType.Right)
                    ||
                    (BoardState[1] == BoardStateType.Right
                    && BoardState[4] == BoardStateType.Right
                    && BoardState[7] == BoardStateType.Right)
                    ||
                    (BoardState[2] == BoardStateType.Right
                    && BoardState[5] == BoardStateType.Right
                    && BoardState[8] == BoardStateType.Right)
                    ||
                    (BoardState[0] == BoardStateType.Right
                    && BoardState[1] == BoardStateType.Right
                    && BoardState[2] == BoardStateType.Right)
                    ||
                    (BoardState[3] == BoardStateType.Right
                    && BoardState[4] == BoardStateType.Right
                    && BoardState[5] == BoardStateType.Right)
                    ||
                    (BoardState[6] == BoardStateType.Right
                    && BoardState[7] == BoardStateType.Right
                    && BoardState[8] == BoardStateType.Right)
                    ||
                    (BoardState[0] == BoardStateType.Right
                    && BoardState[4] == BoardStateType.Right
                    && BoardState[8] == BoardStateType.Right)
                    ||
                    (BoardState[2] == BoardStateType.Right
                    && BoardState[4] == BoardStateType.Right
                    && BoardState[6] == BoardStateType.Right)
                )
                {
                    if (BonusScore > 0)
                    {
                        TotalScore += BonusScore;
                    }
                    GameOver = true;
                    new SoundPlayer(Resources.APPLAUSE_SHORT).Play();
                }
            }
        }

        private void NewGame_Click(object sender, EventArgs e)
        {
            InitializeGame();
        }

        private void NewMatch_Click(object sender, EventArgs e)
        {
            InitializeMatch();
        }

        private void ExitButton_Click(object sender, EventArgs e)
        {
            Close();
        }
    }

    public class PhonicsCard
    {
        private string phoneme;
        private List<SoundPlayer> sounds;
        private List<Bitmap> images;

        private List<int> use_order;

        public string Phoneme { get { return phoneme; } }
        public Bitmap DeckImage { get { return images[use_order[0]]; } }
        public SoundPlayer DeckSound { get { return sounds[use_order[0]]; } }
        public Bitmap BoardImage { get { return images[use_order[1]]; } }
        public SoundPlayer BoardSound { get { return sounds[use_order[1]]; } }

        public PhonicsCard(string _phoneme, params object[] resources)
        {
            LoadResources(_phoneme, resources);

            use_order = new List<int>();

            for (int use_index = 0; use_index < images.Count; use_index++)
            {
                use_order.Add(use_index);
            }

            Utils.Shuffle<int>(use_order);
        }

        public PhonicsCard(string _phoneme, bool first_card_for_deck, params object[] resources)
        {
            LoadResources(_phoneme, resources);

            use_order = new List<int>();

            for (int use_index = 1; use_index < images.Count; use_index++)
            {
                use_order.Add(use_index);
            }

            if (!first_card_for_deck)
            {
                use_order.Add(0);
            }

            Utils.Shuffle<int>(use_order);

            if (first_card_for_deck)
            {
                use_order.Insert(0, 0);
            }
        }

        private void LoadResources(string _phoneme, object[] resources)
        {
            if (resources.Length % 2 != 0)
            {
                throw new ArgumentException("All phonics card constructors must supply a collection of image/sound pairs");
            }

            if (resources.Length < 4)
            {
                throw new ArgumentException("All phonice card constructors must supply at least two image/sound pairs");
            }

            phoneme = _phoneme;
            images = new List<Bitmap>();
            sounds = new List<SoundPlayer>();

            for (int res_index = 0; res_index < resources.Length / 2; res_index++)
            {
                Bitmap image = resources[res_index * 2] as Bitmap;
                Stream sound = resources[res_index * 2 + 1] as Stream;

                if (image == null)
                {
                    throw new ArgumentException(String.Format("Phonics card constructor expected an Image at resource position {0}", res_index * 2));
                }

                if (sound == null)
                {
                    throw new ArgumentException(String.Format("Phonics card constructor expected a Stream at resource position {0}", res_index * 2 + 1));
                }

                images.Add(image);
                sounds.Add(new SoundPlayer(sound));
            }
        }

        public override string ToString()
        {
            return String.Format("{0}: {1}, {2}", phoneme, use_order[0], use_order[1]);
        }

    }

}
