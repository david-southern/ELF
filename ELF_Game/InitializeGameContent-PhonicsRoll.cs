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
using ProjectUtils;


namespace ELF
{
    public partial class PhonicsRollForm : Form
    {
        private void InitializeBaseResources()
        {
            InitializeComponent();

            for (int diceIndex = 0; diceIndex < 5; diceIndex++)
            {
                string diceName = String.Format("Dice_{0}", diceIndex);

                DicePicture[diceIndex] = Controls[diceName] as PictureBox;

                if (DicePicture[diceIndex] == null)
                {
                    throw new ExceptionEx("Dice PictureBox {0} was not found", diceName);
                }

                for (int scoreIndex = 0; scoreIndex < 5; scoreIndex++)
                {
                    string scoreName = String.Format("Score_{0}_{1}", diceIndex, scoreIndex);
                    ScorePicture[diceIndex, scoreIndex] = Controls[scoreName] as PictureBox;

                    if (ScorePicture[diceIndex, scoreIndex] == null)
                    {
                        throw new ExceptionEx("Score PictureBox {0} was not found", scoreName);
                    }
                }

                string speakName = String.Format("Speak_{0}", diceIndex);
                SpeakButtons[diceIndex] = Controls[speakName] as Button;

                if (SpeakButtons[diceIndex] == null)
                {
                    throw new ExceptionEx("Speak Button {0} was not found", speakName);
                }
            }

            rollProgressTimer = new System.Windows.Forms.Timer();
            rollProgressTimer.Tick += new EventHandler(HandleRoll);
            rollProgressTimer.Interval = MAX_ROLL_SLEEP;

            GameBackgrounds[0] = Resources.Vowel_Roll_Background;
            GameBackgrounds[1] = Resources.Vowel_Roll_Background;
            GameBackgrounds[2] = Resources.Vowel_Roll_Background;
            GameBackgrounds[3] = Resources.Sonic_Roll_Background;
            GameBackgrounds[4] = Resources.Sonic_Roll_Background;
            GameBackgrounds[5] = Resources.Sonic_Roll_Background;
            GameBackgrounds[6] = Resources.Sonic_Roll_Background;
            GameBackgrounds[7] = Resources.Sonic_Roll_Background;
        }

        private void InitializeGame()
        {
            Dice.possible_values = new List<string>();

            this.BackgroundImage = GameBackgrounds[GameID];

            if (GameID <= 2)
            {
                Instructions.ForeColor = Color.Black;
                GameTitle.Visible = true;
            }
            else
            {
                Instructions.ForeColor = Color.White;
                GameTitle.Visible = false;
            }

            //initialize with game content
            if (GameID == 0)
            {
                GameTitle.Text = "Vowel Howl - Long";

                TheDice[0] = new Dice(
                    new DiceFace("a", Resources.LONG_A_LETTER, Resources.LONG_A_SOUND),
                    new DiceFace("e", Resources.LONG_E_LETTER, Resources.LONG_E_SOUND),
                    new DiceFace("i", Resources.LONG_I_LETTER, Resources.LONG_I_SOUND),
                    new DiceFace("o", Resources.LONG_O_LETTER, Resources.LONG_O_SOUND),
                    new DiceFace("u", Resources.LONG_U_LETTER, Resources.LONG_U_SOUND),
                    new DiceFace("u", Resources.LONG_U_LETTER, Resources.LONG_U_SOUND)
                );

                TheDice[1] = new Dice(
                    new DiceFace("a", Resources.LONG_A_COMBO_LETTERS, Resources.LONG_A_COMBO_LETTERS_SOUND),
                    new DiceFace("e", Resources.LONG_E_COMBO_LETTERS, Resources.LONG_E_COMBO_LETTERS_SOUND),
                    new DiceFace("i", Resources.LONG_I_COMBO_LETTERS, Resources.LONG_I_COMBO_LETTERS_SOUND),
                    new DiceFace("o", Resources.LONG_O_COMBO_LETTERS, Resources.LONG_O_COMBO_LETTERS_SOUND),
                    new DiceFace("u", Resources.LONG_U_COMBO_LETTERS, Resources.LONG_U_COMBO_LETTERS_SOUND),
                    new DiceFace("e", Resources.LONG_E_SEAL, Resources.LONG_E_SEAL_SOUND)
                );

                TheDice[2] = new Dice(
                    new DiceFace("a", Resources.LONG_A_APE, Resources.LONG_A_APE_SOUND),
                    new DiceFace("e", Resources.LONG_E_BEE, Resources.LONG_E_BEE_SOUND),
                    new DiceFace("i", Resources.LONG_I_PIE, Resources.LONG_I_PIE_SOUND),
                    new DiceFace("o", Resources.LONG_O_BOAT, Resources.LONG_O_BOAT_SOUND),
                    new DiceFace("u", Resources.LONG_U_CUBE, Resources.LONG_U_CUBE_SOUND),
                    new DiceFace("a", Resources.LONG_A_WHALE, Resources.LONG_A_WHALE_SOUND)
                );

                TheDice[3] = new Dice(
                    new DiceFace("a", Resources.LONG_A_TRAIN, Resources.LONG_A_TRAIN_SOUND),
                    new DiceFace("e", Resources.LONG_E_LEAF, Resources.LONG_E_LEAF_SOUND),
                    new DiceFace("i", Resources.LONG_I_KITE, Resources.LONG_I_KITE_SOUND),
                    new DiceFace("o", Resources.LONG_O_NOSE, Resources.LONG_O_NOSE_SOUND),
                    new DiceFace("u", Resources.LONG_U_MULE, Resources.LONG_U_MULE_SOUND),
                    new DiceFace("i", Resources.LONG_I_TIE, Resources.LONG_I_TIE_SOUND)
                );

                TheDice[4] = new Dice(
                    new DiceFace("a", Resources.LONG_A_CAKE, Resources.LONG_A_CAKE_SOUND),
                    new DiceFace("e", Resources.LONG_E_JEEP, Resources.LONG_E_JEEP_SOUND),
                    new DiceFace("i", Resources.LONG_I_BIKE, Resources.LONG_I_BIKE_SOUND),
                    new DiceFace("o", Resources.LONG_O_SNOW, Resources.LONG_O_SNOW_SOUND),
                    new DiceFace("u", Resources.LONG_U_UNICORN, Resources.LONG_U_UNICORN_SOUND),
                    new DiceFace("o", Resources.LONG_O_ROSE, Resources.LONG_O_ROSE_SOUND)
                );

                SCORE_UP[SCORE_CAT1] = Resources.SCORE_LONG_A_UP;
                SCORE_SCORED[SCORE_CAT1] = Resources.SCORE_LONG_A_SCORED;
                SCORE_BURNED[SCORE_CAT1] = Resources.SCORE_LONG_A_BURNED;

                SCORE_UP[SCORE_CAT2] = Resources.SCORE_LONG_E_UP;
                SCORE_SCORED[SCORE_CAT2] = Resources.SCORE_LONG_E_SCORED;
                SCORE_BURNED[SCORE_CAT2] = Resources.SCORE_LONG_E_BURNED;

                SCORE_UP[SCORE_CAT3] = Resources.SCORE_LONG_I_UP;
                SCORE_SCORED[SCORE_CAT3] = Resources.SCORE_LONG_I_SCORED;
                SCORE_BURNED[SCORE_CAT3] = Resources.SCORE_LONG_I_BURNED;

                SCORE_UP[SCORE_CAT4] = Resources.SCORE_LONG_O_UP;
                SCORE_SCORED[SCORE_CAT4] = Resources.SCORE_LONG_O_SCORED;
                SCORE_BURNED[SCORE_CAT4] = Resources.SCORE_LONG_O_BURNED;

                SCORE_UP[SCORE_CAT5] = Resources.SCORE_LONG_U_UP;
                SCORE_SCORED[SCORE_CAT5] = Resources.SCORE_LONG_U_SCORED;
                SCORE_BURNED[SCORE_CAT5] = Resources.SCORE_LONG_U_BURNED;
            }

            if (GameID == 1)
            {
                GameTitle.Text = "Vowel Howl - Short";

                TheDice[0] = new Dice(
                    new DiceFace("a", Resources.SHORT_A_LETTER, Resources.SHORT_A_LETTER_SOUND),
                    new DiceFace("e", Resources.SHORT_E_LETTER, Resources.SHORT_E_LETTER_SOUND),
                    new DiceFace("i", Resources.SHORT_I_LETTER, Resources.SHORT_I_LETTER_SOUND),
                    new DiceFace("o", Resources.SHORT_O_LETTER, Resources.SHORT_O_LETTER_SOUND),
                    new DiceFace("u", Resources.SHORT_U_LETTER, Resources.SHORT_U_LETTER_SOUND),
                    new DiceFace("o", Resources.SHORT_O_LETTER, Resources.SHORT_O_LETTER_SOUND)
                );

                TheDice[1] = new Dice(
                    new DiceFace("a", Resources.SHORT_A_CAT, Resources.SHORT_A_CAT_SOUND),
                    new DiceFace("e", Resources.SHORT_E_BED, Resources.SHORT_E_BED_SOUND),
                    new DiceFace("i", Resources.SHORT_I_FISH, Resources.SHORT_I_FISH_SOUND),
                    new DiceFace("o", Resources.SHORT_O_DOG, Resources.SHORT_O_DOG_SOUND),
                    new DiceFace("u", Resources.SHORT_U_CUP, Resources.SHORT_U_CUP_SOUND),
                    new DiceFace("a", Resources.SHORT_A_LAMP, Resources.SHORT_A_LAMP_SOUND)
                );

                TheDice[2] = new Dice(
                    new DiceFace("a", Resources.SHORT_A_MAN, Resources.SHORT_A_MAN_SOUND),
                    new DiceFace("e", Resources.SHORT_E_JET, Resources.SHORT_E_JET_SOUND),
                    new DiceFace("i", Resources.SHORT_I_SHIP, Resources.SHORT_I_SHIP_SOUND),
                    new DiceFace("o", Resources.SHORT_O_CLOCK, Resources.SHORT_O_CLOCK_SOUND),
                    new DiceFace("u", Resources.SHORT_U_NUT, Resources.SHORT_U_NUT_SOUND),
                    new DiceFace("e", Resources.SHORT_E_TENT, Resources.SHORT_E_TENT_SOUND)
                );

                TheDice[3] = new Dice(
                    new DiceFace("a", Resources.SHORT_A_ANT, Resources.SHORT_A_ANT_SOUND),
                    new DiceFace("e", Resources.SHORT_E_NEST, Resources.SHORT_E_NEST_SOUND),
                    new DiceFace("i", Resources.SHORT_I_PIG, Resources.SHORT_I_PIG_SOUND),
                    new DiceFace("o", Resources.SHORT_O_FOX, Resources.SHORT_O_FOX_SOUND),
                    new DiceFace("u", Resources.SHORT_U_DUCK, Resources.SHORT_U_DUCK_SOUND),
                    new DiceFace("u", Resources.SHORT_U_BUG, Resources.SHORT_U_BUG_SOUND)
                );

                TheDice[4] = new Dice(
                    new DiceFace("a", Resources.SHORT_A_HAT, Resources.SHORT_A_HAT_SOUND),
                    new DiceFace("e", Resources.SHORT_E_WEB, Resources.SHORT_E_WEB_SOUND),
                    new DiceFace("i", Resources.SHORT_I_WITCH, Resources.SHORT_I_WITCH_SOUND),
                    new DiceFace("o", Resources.SHORT_O_MOP, Resources.SHORT_O_MOP_SOUND),
                    new DiceFace("u", Resources.SHORT_U_SUN, Resources.SHORT_U_SUN_SOUND),
                    new DiceFace("i", Resources.SHORT_I_SINK, Resources.SHORT_I_SINK_SOUND)
                );

                SCORE_UP[SCORE_CAT1] = Resources.SCORE_LONG_A_UP;
                SCORE_SCORED[SCORE_CAT1] = Resources.SCORE_LONG_A_SCORED;
                SCORE_BURNED[SCORE_CAT1] = Resources.SCORE_LONG_A_BURNED;

                SCORE_UP[SCORE_CAT2] = Resources.SCORE_LONG_E_UP;
                SCORE_SCORED[SCORE_CAT2] = Resources.SCORE_LONG_E_SCORED;
                SCORE_BURNED[SCORE_CAT2] = Resources.SCORE_LONG_E_BURNED;

                SCORE_UP[SCORE_CAT3] = Resources.SCORE_LONG_I_UP;
                SCORE_SCORED[SCORE_CAT3] = Resources.SCORE_LONG_I_SCORED;
                SCORE_BURNED[SCORE_CAT3] = Resources.SCORE_LONG_I_BURNED;

                SCORE_UP[SCORE_CAT4] = Resources.SCORE_LONG_O_UP;
                SCORE_SCORED[SCORE_CAT4] = Resources.SCORE_LONG_O_SCORED;
                SCORE_BURNED[SCORE_CAT4] = Resources.SCORE_LONG_O_BURNED;

                SCORE_UP[SCORE_CAT5] = Resources.SCORE_LONG_U_UP;
                SCORE_SCORED[SCORE_CAT5] = Resources.SCORE_LONG_U_SCORED;
                SCORE_BURNED[SCORE_CAT5] = Resources.SCORE_LONG_U_BURNED;
            }

            if (GameID == 2)
            {
                GameTitle.Text = "Vowel Howl - Varied";

                TheDice[0] = new Dice(
                    new DiceFace("0ow", Resources.VARIED_OW_LETTERS, Resources.VARIED_OW_LETTERS_SOUND),
                    new DiceFace("1ar", Resources.VARIED_AR_LETTERS, Resources.VARIED_AR_LETTERS_SOUND),
                    new DiceFace("2or", Resources.VARIED_OR_LETTERS, Resources.VARIED_OR_LETTERS_SOUND),
                    new DiceFace("3er", Resources.VARIED_ER_LETTERS, Resources.VARIED_ER_LETTERS_SOUND),
                    new DiceFace("4oo", Resources.VARIED_OO_LETTERS, Resources.VARIED_OO_LETTERS_SOUND),
                    new DiceFace("3er", Resources.VARIED_ER_LETTERS, Resources.VARIED_ER_LETTERS_SOUND)
                );

                TheDice[1] = new Dice(
                    new DiceFace("0ow", Resources.VARIED_OW_HOUSE, Resources.VARIED_OW_HOUSE_SOUND),
                    new DiceFace("1ar", Resources.VARIED_AR_STAR, Resources.VARIED_AR_STAR_SOUND),
                    new DiceFace("2or", Resources.VARIED_OR_HORSE, Resources.VARIED_OR_HORSE_SOUND),
                    new DiceFace("3er", Resources.VARIED_ER_BIRD, Resources.VARIED_ER_BIRD_SOUND),
                    new DiceFace("4oo", Resources.VARIED_OO_MOON, Resources.VARIED_OO_MOON_SOUND),
                    new DiceFace("2or", Resources.VARIED_OR_HORN, Resources.VARIED_OR_HORN_SOUND)
                );

                TheDice[2] = new Dice(
                    new DiceFace("0ow", Resources.VARIED_OW_COW, Resources.VARIED_OW_COW_SOUND),
                    new DiceFace("1ar", Resources.VARIED_AR_CAR, Resources.VARIED_AR_CAR_SOUND),
                    new DiceFace("2or", Resources.VARIED_OR_CORN, Resources.VARIED_OR_CORN_SOUND),
                    new DiceFace("3er", Resources.VARIED_ER_CHURCH, Resources.VARIED_ER_CHURCH_SOUND),
                    new DiceFace("4oo", Resources.VARIED_OO_SCOOTER, Resources.VARIED_OO_SCOOTER_SOUND),
                    new DiceFace("0ow", Resources.VARIED_OW_MOUSE, Resources.VARIED_OW_MOUSE_SOUND)
                );

                TheDice[3] = new Dice(
                    new DiceFace("0ow", Resources.VARIED_OW_CLOWN, Resources.VARIED_OW_CLOWN_SOUND),
                    new DiceFace("1ar", Resources.VARIED_AR_DART, Resources.VARIED_AR_DART_SOUND),
                    new DiceFace("2or", Resources.VARIED_OR_DOOR, Resources.VARIED_OR_DOOR_SOUND),
                    new DiceFace("3er", Resources.VARIED_ER_NURSE, Resources.VARIED_ER_NURSE_SOUND),
                    new DiceFace("4oo", Resources.VARIED_OO_BOOT, Resources.VARIED_OO_BOOT_SOUND),
                    new DiceFace("4oo", Resources.VARIED_OO_ZOO, Resources.VARIED_OO_ZOO_SOUND)
                );

                TheDice[4] = new Dice(
                    new DiceFace("0ow", Resources.VARIED_OW_OWL, Resources.VARIED_OW_OWL_SOUND),
                    new DiceFace("1ar", Resources.VARIED_AR_BARN, Resources.VARIED_AR_BARN_SOUND),
                    new DiceFace("2or", Resources.VARIED_OR_FORK, Resources.VARIED_OR_FORK_SOUND),
                    new DiceFace("3er", Resources.VARIED_ER_GIRL, Resources.VARIED_ER_GIRL_SOUND),
                    new DiceFace("4oo", Resources.VARIED_OO_SCREW, Resources.VARIED_OO_SCREW_SOUND),
                    new DiceFace("1ar", Resources.VARIED_AR_SHARK, Resources.VARIED_AR_SHARK_SOUND)
                );

                SCORE_UP[SCORE_CAT1] = Resources.SCORE_VARIED_OW_UP;
                SCORE_SCORED[SCORE_CAT1] = Resources.SCORE_VARIED_OW_SCORED;
                SCORE_BURNED[SCORE_CAT1] = Resources.SCORE_VARIED_OW_BURNED;

                SCORE_UP[SCORE_CAT2] = Resources.SCORE_VARIED_AR_UP;
                SCORE_SCORED[SCORE_CAT2] = Resources.SCORE_VARIED_AR_SCORED;
                SCORE_BURNED[SCORE_CAT2] = Resources.SCORE_VARIED_AR_BURNED;

                SCORE_UP[SCORE_CAT3] = Resources.SCORE_VARIED_OR_UP;
                SCORE_SCORED[SCORE_CAT3] = Resources.SCORE_VARIED_OR_SCORED;
                SCORE_BURNED[SCORE_CAT3] = Resources.SCORE_VARIED_OR_BURNED;

                SCORE_UP[SCORE_CAT4] = Resources.SCORE_VARIED_ER_UP;
                SCORE_SCORED[SCORE_CAT4] = Resources.SCORE_VARIED_ER_SCORED;
                SCORE_BURNED[SCORE_CAT4] = Resources.SCORE_VARIED_ER_BURNED;

                SCORE_UP[SCORE_CAT5] = Resources.SCORE_VARIED_OO_UP;
                SCORE_SCORED[SCORE_CAT5] = Resources.SCORE_VARIED_OO_SCORED;
                SCORE_BURNED[SCORE_CAT5] = Resources.SCORE_VARIED_OO_BURNED;
            }

            if (GameID == 3)
            {
                TheDice[0] = new Dice(
                    new DiceFace("d", Resources.INITIAL_D_LETTER, Resources.INITIAL_D_LETTER_SOUND),
                    new DiceFace("f", Resources.INITIAL_F_LETTER, Resources.INITIAL_F_LETTER_SOUND),
                    new DiceFace("g", Resources.INITIAL_G_LETTER, Resources.INITIAL_G_LETTER_SOUND),
                    new DiceFace("m", Resources.INITIAL_M_LETTER, Resources.INITIAL_M_LETTER_SOUND),
                    new DiceFace("s", Resources.INITIAL_S_LETTER, Resources.INITIAL_S_LETTER_SOUND),
                    new DiceFace("m", Resources.INITIAL_M_LETTER, Resources.INITIAL_M_LETTER_SOUND)
                );

                TheDice[1] = new Dice(
                    new DiceFace("d", Resources.INITIAL_D_DESK, Resources.INITIAL_D_DESK_SOUND),
                    new DiceFace("f", Resources.INITIAL_F_FAN, Resources.INITIAL_F_FAN_SOUND),
                    new DiceFace("g", Resources.INITIAL_G_GATE, Resources.INITIAL_G_GATE_SOUND),
                    new DiceFace("m", Resources.INITIAL_M_MOUSE, Resources.INITIAL_M_MOUSE_SOUND),
                    new DiceFace("s", Resources.INITIAL_S_SAW, Resources.INITIAL_S_SAW_SOUND),
                    new DiceFace("d", Resources.INITIAL_D_DICE, Resources.INITIAL_D_DICE_SOUND)
                );

                TheDice[2] = new Dice(
                    new DiceFace("d", Resources.INITIAL_D_DOG, Resources.INITIAL_D_DOG_SOUND),
                    new DiceFace("f", Resources.INITIAL_F_FISH, Resources.INITIAL_F_FISH_SOUND),
                    new DiceFace("g", Resources.INITIAL_G_GOAT, Resources.INITIAL_G_GOAT_SOUND),
                    new DiceFace("m", Resources.INITIAL_M_MAN, Resources.INITIAL_M_MAN_SOUND),
                    new DiceFace("s", Resources.INITIAL_S_SUN, Resources.INITIAL_S_SUN_SOUND),
                    new DiceFace("f", Resources.INITIAL_F_FARMER, Resources.INITIAL_F_FARMER_SOUND)
                );

                TheDice[3] = new Dice(
                    new DiceFace("d", Resources.INITIAL_D_DUCK, Resources.INITIAL_D_DUCK_SOUND),
                    new DiceFace("f", Resources.INITIAL_F_FOX, Resources.INITIAL_F_FOX_SOUND),
                    new DiceFace("g", Resources.INITIAL_G_GIRL, Resources.INITIAL_G_GIRL_SOUND),
                    new DiceFace("m", Resources.INITIAL_M_MOON, Resources.INITIAL_M_MOON_SOUND),
                    new DiceFace("s", Resources.INITIAL_S_SEAL, Resources.INITIAL_S_SEAL_SOuND),
                    new DiceFace("g", Resources.INITIAL_G_GOOSE, Resources.INITIAL_G_GOOSE_SOUND)
                );

                TheDice[4] = new Dice(
                    new DiceFace("d", Resources.INITIAL_D_DOOR, Resources.INITIAL_D_DOOR_SOUND),
                    new DiceFace("f", Resources.INITIAL_F_FORK, Resources.INITIAL_F_FORK_SOUND),
                    new DiceFace("g", Resources.INITIAL_G_GAME, Resources.INITIAL_G_GAME_SOUND),
                    new DiceFace("m", Resources.INITIAL_M_MONKEY, Resources.INITIAL_M_MONKEY_SOUND),
                    new DiceFace("s", Resources.INITIAL_S_SINK, Resources.INITIAL_S_SINK_SOUND),
                    new DiceFace("s", Resources.INITIAL_S_SUIT, Resources.INITIAL_S_SUIT_SOUND)
                );

                SCORE_UP[SCORE_CAT1] = Resources.SCORE_INITIAL_D_UP;
                SCORE_SCORED[SCORE_CAT1] = Resources.SCORE_INITIAL_D_SCORED;
                SCORE_BURNED[SCORE_CAT1] = Resources.SCORE_INITIAL_D_BURNED;

                SCORE_UP[SCORE_CAT2] = Resources.SCORE_INITIAL_F_UP;
                SCORE_SCORED[SCORE_CAT2] = Resources.SCORE_INITIAL_F_SCORED;
                SCORE_BURNED[SCORE_CAT2] = Resources.SCORE_INITIAL_F_BURNED;

                SCORE_UP[SCORE_CAT3] = Resources.SCORE_INITIAL_G_UP;
                SCORE_SCORED[SCORE_CAT3] = Resources.SCORE_INITIAL_G_SCORED;
                SCORE_BURNED[SCORE_CAT3] = Resources.SCORE_INITIAL_G_BURNED;

                SCORE_UP[SCORE_CAT4] = Resources.SCORE_INITIAL_M_UP;
                SCORE_SCORED[SCORE_CAT4] = Resources.SCORE_INITIAL_M_SCORED;
                SCORE_BURNED[SCORE_CAT4] = Resources.SCORE_INITIAL_M_BURNED;

                SCORE_UP[SCORE_CAT5] = Resources.SCORE_INITIAL_S_UP;
                SCORE_SCORED[SCORE_CAT5] = Resources.SCORE_INITIAL_S_SCORED;
                SCORE_BURNED[SCORE_CAT5] = Resources.SCORE_INITIAL_S_BURNED;
            }

            if (GameID == 4)
            {
                TheDice[0] = new Dice(
                    new DiceFace("j", Resources.INITIAL_J_LETTER, Resources.INITIAL_J_LETTER_SOUND),
                    new DiceFace("k", Resources.INITIAL_K_LETTER, Resources.INITIAL_K_LETTER_SOUND),
                    new DiceFace("n", Resources.INITIAL_N_LETTER, Resources.INITIAL_N_LETTER_SOUND),
                    new DiceFace("p", Resources.INITIAL_P_LETTER, Resources.INITIAL_P_LETTER_SOUND),
                    new DiceFace("y", Resources.INITIAL_Y_LETTER, Resources.INITIAL_Y_LETTER_SOUND),
                    new DiceFace("y", Resources.INITIAL_Y_LETTER, Resources.INITIAL_Y_LETTER_SOUND)
                );

                TheDice[1] = new Dice(
                    new DiceFace("j", Resources.INITIAL_J_JAIL, Resources.INITIAL_J_JAIL_SOUND),
                    new DiceFace("k", Resources.INITIAL_K_KEY, Resources.INITIAL_K_KEY_SOUND),
                    new DiceFace("n", Resources.INITIAL_N_NURSE, Resources.INITIAL_N_NURSE_SOUND),
                    new DiceFace("p", Resources.INITIAL_P_PEAR, Resources.INITIAL_P_PEAR_SOUND),
                    new DiceFace("y", Resources.INITIAL_Y_YAWN, Resources.INITIAl_Y_YAWN_SOUND),
                    new DiceFace("p", Resources.INITIAL_P_PINEAPPLE, Resources.INITIAL_P_PINEAPPLE_SOUND)
                );

                TheDice[2] = new Dice(
                    new DiceFace("j", Resources.INITIAL_J_JEEP, Resources.INITIAL_J_JEEP_SOUND),
                    new DiceFace("k", Resources.INITIAL_K_KING, Resources.INITIAL_K_KING_SOUND),
                    new DiceFace("n", Resources.INITIAL_N_NEST, Resources.INITIAL_N_NEST_SOUND),
                    new DiceFace("p", Resources.INITIAL_P_PIG, Resources.INITIAL_P_PIG_SOUND),
                    new DiceFace("y", Resources.INITIAL_Y_YOYO, Resources.INITIAL_Y_YOYO_SOUND),
                    new DiceFace("n", Resources.INITIAL_N_NET, Resources.INITIAL_N_NET_SOUND)
                );

                TheDice[3] = new Dice(
                    new DiceFace("j", Resources.INITIAL_J_JET, Resources.INITIAL_J_JET_SOUND),
                    new DiceFace("k", Resources.INITIAL_K_KITE, Resources.INITIAL_K_KITE_SOUND),
                    new DiceFace("n", Resources.INITIAL_N_NAIL, Resources.INITIAL_N_NAIL_SOUND),
                    new DiceFace("p", Resources.INITIAL_P_PIE, Resources.INITIAL_P_PIE_SOUND),
                    new DiceFace("y", Resources.INITIAL_Y_YARN, Resources.INITIAL_Y_YARN_SOUND),
                    new DiceFace("k", Resources.INITIAL_K_KITTEN, Resources.INITIAL_K_KITTEN_SOUND)
                );

                TheDice[4] = new Dice(
                    new DiceFace("j", Resources.INITIAL_J_JACKET, Resources.INITIAL_J_JACKET_SOUND),
                    new DiceFace("k", Resources.INITIAl_K_KANGAROO, Resources.INITIAL_K_KANGAROO_SOUND),
                    new DiceFace("n", Resources.INITIAL_N_NUT, Resources.INITIAL_N_NUT_SOUND),
                    new DiceFace("p", Resources.INITIAL_P_PANDA, Resources.INITIAL_P_PANDA_SOUND),
                    new DiceFace("y", Resources.INITIAL_Y_YOLK, Resources.INITIAL_Y_YOLK_SOUND),
                    new DiceFace("j", Resources.INITIAL_J_JUGGLER, Resources.INITIAL_J_JUGGLER_SOUND)
                );

                SCORE_UP[SCORE_CAT1] = Resources.SCORE_INITIAL_J_UP;
                SCORE_SCORED[SCORE_CAT1] = Resources.SCORE_INITIAL_J_SCORED;
                SCORE_BURNED[SCORE_CAT1] = Resources.SCORE_INITIAL_J_BURNED;

                SCORE_UP[SCORE_CAT2] = Resources.SCORE_INITIAL_K_UP;
                SCORE_SCORED[SCORE_CAT2] = Resources.SCORE_INITIAL_K_SCORED;
                SCORE_BURNED[SCORE_CAT2] = Resources.SCORE_INITIAL_K_BURNED;

                SCORE_UP[SCORE_CAT3] = Resources.SCORE_INITIAL_N_UP;
                SCORE_SCORED[SCORE_CAT3] = Resources.SCORE_INITIAL_N_SCORED;
                SCORE_BURNED[SCORE_CAT3] = Resources.SCORE_INITIAL_N_BURNED;

                SCORE_UP[SCORE_CAT4] = Resources.SCORE_INITIAL_P_UP;
                SCORE_SCORED[SCORE_CAT4] = Resources.SCORE_INITIAL_P_SCORED;
                SCORE_BURNED[SCORE_CAT4] = Resources.SCORE_INITIAL_P_BURNED;

                SCORE_UP[SCORE_CAT5] = Resources.SCORE_INITIAL_Y_UP;
                SCORE_SCORED[SCORE_CAT5] = Resources.SCORE_INITIAL_Y_SCORED;
                SCORE_BURNED[SCORE_CAT5] = Resources.SCORE_INITIAL_Y_BURNED;
            }

            if (GameID == 5)
            {
                TheDice[0] = new Dice(
                    new DiceFace("b", Resources.INITIAL_B_LETTER, Resources.INITIAL_B_LETTER_SOUND),
                    new DiceFace("c", Resources.INITIAL_C_LETTER, Resources.INITIAL_C_LETTER_SOUND),
                    new DiceFace("h", Resources.INITIAL_H_LETTER, Resources.INITIAL_H_LETTER_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_LETTER, Resources.INITIAL_T_LETTER_SOUND),
                    new DiceFace("w", Resources.INITIAL_W_LETTER, Resources.INITIAL_W_LETTER_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_LETTER, Resources.INITIAL_T_LETTER_SOUND)
                );

                TheDice[1] = new Dice(
                    new DiceFace("b", Resources.INITIAL_B_BOAT, Resources.INITIAL_B_BOAT_SOUND),
                    new DiceFace("c", Resources.INITIAL_C_CORN, Resources.INITIAL_C_CORN_SOUND),
                    new DiceFace("h", Resources.INITIAL_H_HOOK, Resources.INITIAL_H_HOOK_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_TUB, Resources.INITIAL_T_TUB_SOUND),
                    new DiceFace("w", Resources.INITIAL_W_WEB, Resources.INITIAL_W_WEB_SOUND),
                    new DiceFace("b", Resources.INITIAL_B_BIKE, Resources.INITIAL_B_BIKE_SOUND)
                );

                TheDice[2] = new Dice(
                    new DiceFace("b", Resources.INITIAL_B_BEE, Resources.INITIAL_B_BEE_SOUND),
                    new DiceFace("c", Resources.INITIAl_C_COW, Resources.INITIAL_C_COW_SOUND),
                    new DiceFace("h", Resources.INITIAL_H_HORSE, Resources.INITIAL_H_HORSE_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_TIGER, Resources.INITIAL_T_TIGER_SOUND),
                    new DiceFace("w", Resources.INITIAL_W_WATCH, Resources.INITIAL_W_WATCH_SOUND),
                    new DiceFace("h", Resources.INITIAL_H_HIPPO, Resources.INITIAL_H_HIPPO_SOUND)
                );

                TheDice[3] = new Dice(
                    new DiceFace("b", Resources.INITIAL_B_BED, Resources.INITIAL_B_BED_SOUND),
                    new DiceFace("c", Resources.INITIAL_C_COAT, Resources.INITIAL_C_COAT_SOUND),
                    new DiceFace("h", Resources.INITIAL_H_HAT, Resources.INITIAL_H_HAT_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_TENT, Resources.INITIAL_T_TENT_SOUND),
                    new DiceFace("w", Resources.INITIAL_W_WATERMELON, Resources.INITIAL_W_WATERMELON_SOUND),
                    new DiceFace("w", Resources.INITIAL_W_WAGON, Resources.INITIAL_W_WAGON_SOUND)
                );

                TheDice[4] = new Dice(
                    new DiceFace("b", Resources.INITIAL_B_BUG, Resources.INITIAL_B_BUG_SOUND),
                    new DiceFace("c", Resources.INITIAL_C_CAR, Resources.INITIAL_C_CAR_SOUND),
                    new DiceFace("h", Resources.INITIAL_H_HEN, Resources.INITIAL_H_HEN_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_TURKEY, Resources.INITIAL_T_TURKEY_SOUND),
                    new DiceFace("w", Resources.INITIAL_W_WINDOW, Resources.INITIAL_W_WINDOW_SOUND),
                    new DiceFace("c", Resources.INITIAL_C_CUP, Resources.INITIAL_C_CUP_SOUND)
                );

                SCORE_UP[SCORE_CAT1] = Resources.SCORE_INITIAL_B_UP;
                SCORE_SCORED[SCORE_CAT1] = Resources.SCORE_INITIAL_B_SCORED;
                SCORE_BURNED[SCORE_CAT1] = Resources.SCORE_INITIAL_B_BURNED;

                SCORE_UP[SCORE_CAT2] = Resources.SCORE_INITIAL_C_UP;
                SCORE_SCORED[SCORE_CAT2] = Resources.SCORE_INITIAL_C_SCORED;
                SCORE_BURNED[SCORE_CAT2] = Resources.SCORE_INITIAL_C_BURNED;

                SCORE_UP[SCORE_CAT3] = Resources.SCORE_INITIAL_H_UP;
                SCORE_SCORED[SCORE_CAT3] = Resources.SCORE_INITIAL_H_SCORED;
                SCORE_BURNED[SCORE_CAT3] = Resources.SCORE_INITIAL_H_BURNED;

                SCORE_UP[SCORE_CAT4] = Resources.SCORE_INITIAL_T_UP;
                SCORE_SCORED[SCORE_CAT4] = Resources.SCORE_INITIAL_T_SCORED;
                SCORE_BURNED[SCORE_CAT4] = Resources.SCORE_INITIAL_T_BURNED;

                SCORE_UP[SCORE_CAT5] = Resources.SCORE_INITIAL_W_UP;
                SCORE_SCORED[SCORE_CAT5] = Resources.SCORE_INITIAL_W_SCORED;
                SCORE_BURNED[SCORE_CAT5] = Resources.SCORE_INITIAL_W_BURNED;
            }

            if (GameID == 6)
            {
                TheDice[0] = new Dice(
                    new DiceFace("l", Resources.INITIAL_L_LETTER, Resources.INITIAL_L_LETTER_SOUND),
                    new DiceFace("r", Resources.INITIAL_R_LETTER, Resources.INITIAL_R_LETTER_SOUND),
                    new DiceFace("v", Resources.INITIAL_V_LETTER, Resources.INITIAL_V_LETTER_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_LETTER, Resources.INITIAL_T_LETTER_SOUND),
                    new DiceFace("z", Resources.INITIAL_Z_LETTER, Resources.INITIAL_Z_LETTER_SOUND),
                    new DiceFace("z", Resources.INITIAL_Z_LETTER, Resources.INITIAL_Z_LETTER_SOUND)
                );

                TheDice[1] = new Dice(
                    new DiceFace("l", Resources.INITIAL_L_LEMON, Resources.INITIAL_L_LEMON_SOUND),
                    new DiceFace("r", Resources.INITIAL_R_RAINBOW, Resources.INITIAL_R_RAINBOW_SOUND),
                    new DiceFace("v", Resources.INITIAL_V_VEST, Resources.INITIAL_V_VEST_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_TURTLE, Resources.INITIAL_T_TURTLE_SOUND),
                    new DiceFace("z", Resources.INITIAL_Z_ZEBRA, Resources.INITIAL_Z_ZEBRA_SOUND),
                    new DiceFace("l", Resources.INITIAL_L_LAMB, Resources.INITIAL_L_LAMB_SOUND)
                );

                TheDice[2] = new Dice(
                    new DiceFace("l", Resources.INITIAL_L_LION, Resources.INITIAL_L_LION_SOUND),
                    new DiceFace("r", Resources.INITIAL_R_RING, Resources.INITIAL_R_RING_SOUND),
                    new DiceFace("v", Resources.INITIAL_V_VEGETABLES, Resources.INITIAL_V_VEGETABLES_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_TEETH, Resources.INITIAL_T_TEETH_SOUND),
                    new DiceFace("z", Resources.INITIAL_Z_ZOO, Resources.INITIAL_Z_ZOO_SOUND),
                    new DiceFace("v", Resources.INITIAL_V_VASE, Resources.INITIAL_V_VASE_SOUND)
                );

                TheDice[3] = new Dice(
                    new DiceFace("l", Resources.INITIAL_L_LAMP, Resources.INITIAL_L_LAMP_SOUND),
                    new DiceFace("r", Resources.INITIAL_R_RABBIT, Resources.INITIAL_R_RABBIT_SOUND),
                    new DiceFace("v", Resources.INITIAL_V_VAN, Resources.INITIAl_V_VAN_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_TABLE, Resources.INITIAL_T_TABLE_SOUND),
                    new DiceFace("z", Resources.INITIAL_Z_ZIPPER, Resources.INITIAl_Z_ZIPPER_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_TELEVISION, Resources.INITIAL_T_TELEVISION_SOUND)
                );

                TheDice[4] = new Dice(
                    new DiceFace("l", Resources.INITIAL_L_LEAF, Resources.INITIAL_L_LEAF_SOUND),
                    new DiceFace("r", Resources.INITIAL_R_ROBOT, Resources.INITIAL_R_ROBOT_SOUND),
                    new DiceFace("v", Resources.INITIAL_V_VIOLIN, Resources.INITIAL_V_VIOLIN_SOUND),
                    new DiceFace("t", Resources.INITIAL_T_TOYS, Resources.INITIAL_T_TOYS_SOUND),
                    new DiceFace("z", Resources.INITIAL_Z_ZERO, Resources.INITIAL_Z_ZERO_SOUND),
                    new DiceFace("r", Resources.INITIAL_R_ROOSTER, Resources.INITIAL_R_ROOSTER_SOUND)
                );

                SCORE_UP[SCORE_CAT1] = Resources.SCORE_INITIAL_L_UP;
                SCORE_SCORED[SCORE_CAT1] = Resources.SCORE_INITIAL_L_SCORED;
                SCORE_BURNED[SCORE_CAT1] = Resources.SCORE_INITIAL_L_BURNED;

                SCORE_UP[SCORE_CAT2] = Resources.SCORE_INITIAL_R_UP;
                SCORE_SCORED[SCORE_CAT2] = Resources.SCORE_INITIAL_R_SCORED;
                SCORE_BURNED[SCORE_CAT2] = Resources.SCORE_INITIAL_R_BURNED;

                SCORE_UP[SCORE_CAT3] = Resources.SCORE_INITIAL_T_UP;
                SCORE_SCORED[SCORE_CAT3] = Resources.SCORE_INITIAL_T_SCORED;
                SCORE_BURNED[SCORE_CAT3] = Resources.SCORE_INITIAL_T_BURNED;

                SCORE_UP[SCORE_CAT4] = Resources.SCORE_INITIAL_V_UP;
                SCORE_SCORED[SCORE_CAT4] = Resources.SCORE_INITIAL_V_SCORED;
                SCORE_BURNED[SCORE_CAT4] = Resources.SCORE_INITIAL_V_BURNED;

                SCORE_UP[SCORE_CAT5] = Resources.SCORE_INITIAL_Z_UP;
                SCORE_SCORED[SCORE_CAT5] = Resources.SCORE_INITIAL_Z_SCORED;
                SCORE_BURNED[SCORE_CAT5] = Resources.SCORE_INITIAL_Z_BURNED;
            }

            if (GameID == 7)
            {
                TheDice[0] = new Dice(
                    new DiceFace("ch", Resources.INITIAL_CH_LETTERS, Resources.INITIAL_CH_LETTERS_SOUND),
                    new DiceFace("qu", Resources.INITIAL_QU_LETTERS, Resources.INITIAL_QU_LETTERS_SOUND),
                    new DiceFace("sh", Resources.INITIAl_SH_LETTERS, Resources.INITIAL_SH_LETTERS_SOUND),
                    new DiceFace("th", Resources.INITIAL_TH_LETTERS, Resources.INITIAL_TH_LETTERS_SOUND),
                    new DiceFace("wh", Resources.INITIAL_WH_LETTERS, Resources.INITIAL_WH_LETTERS_SOUND),
                    new DiceFace("th", Resources.INITIAL_TH_LETTERS, Resources.INITIAL_TH_LETTERS_SOUND)
                );

                TheDice[1] = new Dice(
                    new DiceFace("ch", Resources.INITIAL_CH_CHICKEN, Resources.INITIAL_CH_CHICKEN_SOUND),
                    new DiceFace("qu", Resources.INITIAL_QU_QUILL, Resources.INITIAL_QU_QUILL_SOUND),
                    new DiceFace("sh", Resources.INITIAL_SH_SHELL, Resources.INITIAL_SH_SHELL_SOUND),
                    new DiceFace("th", Resources.INITIAL_TH_THUMB, Resources.INITIAL_TH_THUMB_SOUND),
                    new DiceFace("wh", Resources.INITIAL_WH_WHIP, Resources.INITIAL_WH_WHIP_SOUND),
                    new DiceFace("ch", Resources.INITIAL_CH_CHERRY, Resources.INITIAL_CH_CHERRY_SOUND)
                );

                TheDice[2] = new Dice(
                    new DiceFace("ch", Resources.INITIAL_CH_CHURCH, Resources.INITIAL_CH_CHURCH_SOUND),
                    new DiceFace("qu", Resources.INITIAL_QU_QUEEN, Resources.INITIAL_QU_QUEEN_SOUND),
                    new DiceFace("sh", Resources.INITIAL_SH_SHOE, Resources.INITIAL_SH_SHOE_SOUND),
                    new DiceFace("th", Resources.INITIAL_TH_THERMOMETER, Resources.INITIAL_TH_THERMOMETER_SOUND),
                    new DiceFace("wh", Resources.INITIAL_WH_WHISTLE, Resources.INITIAL_WH_WHISTLE_SOUND),
                    new DiceFace("sh", Resources.INITIAL_SH_SHARK, Resources.INITIAL_SH_SHARK_SOUND)
                );

                TheDice[3] = new Dice(
                    new DiceFace("ch", Resources.INITIAL_CH_CHAIR, Resources.INITIAL_CH_CHAIR_SOUND),
                    new DiceFace("qu", Resources.INITIAL_QU_QUILT, Resources.INITIAL_QU_QUILT_SOUND),
                    new DiceFace("sh", Resources.INITIAL_SH_SHEEP, Resources.INITIAL_SH_SHEEP_SOUND),
                    new DiceFace("th", Resources.INITIAL_TH_THIMBLE, Resources.INITIAL_TH_THIMBLE_SOUND),
                    new DiceFace("wh", Resources.INITIAL_WH_WHEEL, Resources.INITIAL_WH_WHEEL_SOUND),
                    new DiceFace("wh", Resources.INITIAL_WH_WHISKERS, Resources.INITIAL_WH_WHISKERS_SOUND)
                );

                TheDice[4] = new Dice(
                    new DiceFace("ch", Resources.INITIAL_CH_CHAIN, Resources.INITIAL_CH_CHAIN_SOUND),
                    new DiceFace("qu", Resources.INITIAL_QU_QUAIL, Resources.INITIAL_QU_QUAIL_SOUND),
                    new DiceFace("sh", Resources.INITIAL_SH_SHIP, Resources.INITIAL_SH_SHIP_SOUND),
                    new DiceFace("th", Resources.INITIAL_TH_THORN, Resources.INITIAL_TH_THORN_SOUND),
                    new DiceFace("wh", Resources.INITIAL_WH_WHALE, Resources.INITIAL_WH_WHALE_SOUND),
                    new DiceFace("qu", Resources.INITIAL_QU_QUESTION, Resources.INITIAL_QU_QUESTION_SOUND)
                );

                SCORE_UP[SCORE_CAT1] = Resources.SCORE_INITIAL_CH_UP;
                SCORE_SCORED[SCORE_CAT1] = Resources.SCORE_INITIAL_CH_SCORED;
                SCORE_BURNED[SCORE_CAT1] = Resources.SCORE_INITIAL_CH_BURNED;

                SCORE_UP[SCORE_CAT2] = Resources.SCORE_INITIAL_QU_UP;
                SCORE_SCORED[SCORE_CAT2] = Resources.SCORE_INITIAL_QU_SCORED;
                SCORE_BURNED[SCORE_CAT2] = Resources.SCORE_INITIAL_QU_BURNED;

                SCORE_UP[SCORE_CAT3] = Resources.SCORE_INITIAL_SH_UP;
                SCORE_SCORED[SCORE_CAT3] = Resources.SCORE_INITIAL_SH_SCORED;
                SCORE_BURNED[SCORE_CAT3] = Resources.SCORE_INITIAL_SH_BURNED;

                SCORE_UP[SCORE_CAT4] = Resources.SCORE_INITIAL_TH_UP;
                SCORE_SCORED[SCORE_CAT4] = Resources.SCORE_INITIAL_TH_SCORED;
                SCORE_BURNED[SCORE_CAT4] = Resources.SCORE_INITIAL_TH_BURNED;

                SCORE_UP[SCORE_CAT5] = Resources.SCORE_INITIAL_WH_UP;
                SCORE_SCORED[SCORE_CAT5] = Resources.SCORE_INITIAL_WH_SCORED;
                SCORE_BURNED[SCORE_CAT5] = Resources.SCORE_INITIAL_WH_BURNED;
            }
        }
    }
}
