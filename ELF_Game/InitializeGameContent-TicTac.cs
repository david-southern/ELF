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
    public partial class TicTacGoldForm : Form
    {
        private void InitializeGameContent()
        {
            if (GameID == 0)
            {
                this.Text = "Tic-Tac-Gold Consonant Auditory 1";

                DrawDeck = new List<PhonicsCard>();

                DrawDeck.Add(new PhonicsCard("m",
                    Resources.INITIAL_M_MAN, Resources.INITIAL_M_MAN_SOUND,
                    Resources.INITIAL_M_MONKEY, Resources.INITIAL_M_MONKEY_SOUND,
                    Resources.INITIAL_M_MOON, Resources.INITIAL_M_MOON_SOUND,
                    Resources.INITIAL_M_MOUSE, Resources.INITIAL_M_MOUSE_SOUND));

                DrawDeck.Add(new PhonicsCard("d",
                    Resources.INITIAL_D_DESK, Resources.INITIAL_D_DESK_SOUND,
                    Resources.INITIAL_D_DICE, Resources.INITIAL_D_DICE_SOUND,
                    Resources.INITIAL_D_DOG, Resources.INITIAL_D_DOG_SOUND,
                    Resources.INITIAL_D_DOOR, Resources.INITIAL_D_DOOR_SOUND,
                    Resources.INITIAL_D_DUCK, Resources.INITIAL_D_DUCK_SOUND));

                DrawDeck.Add(new PhonicsCard("f",
                    Resources.INITIAL_F_FAN, Resources.INITIAL_F_FAN_SOUND,
                    Resources.INITIAL_F_FARMER, Resources.INITIAL_F_FARMER_SOUND,
                    Resources.INITIAL_F_FISH, Resources.INITIAL_F_FISH_SOUND,
                    Resources.INITIAL_F_FORK, Resources.INITIAL_F_FORK_SOUND,
                    Resources.INITIAL_F_FOX, Resources.INITIAL_F_FOX_SOUND));

                DrawDeck.Add(new PhonicsCard("g",
                    Resources.INITIAL_G_GAME, Resources.INITIAL_G_GAME_SOUND,
                    Resources.INITIAL_G_GATE, Resources.INITIAL_G_GATE_SOUND,
                    Resources.INITIAL_G_GIRL, Resources.INITIAL_G_GIRL_SOUND,
                    Resources.INITIAL_G_GOAT, Resources.INITIAL_G_GOAT_SOUND,
                    Resources.INITIAL_G_GOOSE, Resources.INITIAL_G_GOOSE_SOUND));

                DrawDeck.Add(new PhonicsCard("s",
                    Resources.INITIAL_S_SAW, Resources.INITIAL_S_SAW_SOUND,
                    Resources.INITIAL_S_SEAL, Resources.INITIAL_S_SEAL_SOuND,
                    Resources.INITIAL_S_SINK, Resources.INITIAL_S_SINK_SOUND,
                    Resources.INITIAL_S_SUIT, Resources.INITIAL_S_SUIT_SOUND,
                    Resources.INITIAL_S_SUN, Resources.INITIAL_S_SUN_SOUND));

                DrawDeck.Add(new PhonicsCard("r",
                    Resources.INITIAL_R_RABBIT, Resources.INITIAL_R_RABBIT_SOUND,
                    Resources.INITIAL_R_RAINBOW, Resources.INITIAL_R_RAINBOW_SOUND,
                    Resources.INITIAL_R_RING, Resources.INITIAL_R_RING_SOUND,
                    Resources.INITIAL_R_ROBOT, Resources.INITIAL_R_ROBOT_SOUND,
                    Resources.INITIAL_R_ROOSTER, Resources.INITIAL_R_ROOSTER_SOUND));

                DrawDeck.Add(new PhonicsCard("w",
                    Resources.INITIAL_W_WAGON, Resources.INITIAL_W_WAGON_SOUND,
                    Resources.INITIAL_W_WATCH, Resources.INITIAL_W_WATCH_SOUND,
                    Resources.INITIAL_W_WATERMELON, Resources.INITIAL_W_WATERMELON_SOUND,
                    Resources.INITIAL_W_WEB, Resources.INITIAL_W_WEB_SOUND,
                    Resources.INITIAL_W_WINDOW, Resources.INITIAL_W_WINDOW_SOUND));

                DrawDeck.Add(new PhonicsCard("c",
                    Resources.INITIAL_C_CAR, Resources.INITIAL_C_CAR_SOUND,
                    Resources.INITIAL_C_COAT, Resources.INITIAL_C_COAT_SOUND,
                    Resources.INITIAL_C_CORN, Resources.INITIAL_C_CORN_SOUND,
                    Resources.INITIAl_C_COW, Resources.INITIAL_C_COW_SOUND,
                    Resources.INITIAL_C_CUP, Resources.INITIAL_C_CUP_SOUND));

                DrawDeck.Add(new PhonicsCard("j",
                    Resources.INITIAL_J_JACKET, Resources.INITIAL_J_JACKET_SOUND,
                    Resources.INITIAL_J_JAIL, Resources.INITIAL_J_JAIL_SOUND,
                    Resources.INITIAL_J_JEEP, Resources.INITIAL_J_JEEP_SOUND,
                    Resources.INITIAL_J_JET, Resources.INITIAL_J_JET_SOUND,
                    Resources.INITIAL_J_JUGGLER, Resources.INITIAL_J_JUGGLER_SOUND));

                DrawDeck.Add(new PhonicsCard("y",
                    Resources.INITIAL_Y_YARN, Resources.INITIAL_Y_YARN_SOUND,
                    Resources.INITIAL_Y_YAWN, Resources.INITIAl_Y_YAWN_SOUND,
                    Resources.INITIAL_Y_YOLK, Resources.INITIAL_Y_YOLK_SOUND,
                    Resources.INITIAL_Y_YOYO, Resources.INITIAL_Y_YOYO_SOUND));

                DrawDeck.Add(new PhonicsCard("th",
                    Resources.INITIAL_TH_THERMOMETER, Resources.INITIAL_TH_THERMOMETER_SOUND,
                    Resources.INITIAL_TH_THIMBLE, Resources.INITIAL_TH_THIMBLE_SOUND,
                    Resources.INITIAL_TH_THORN, Resources.INITIAL_TH_THORN_SOUND,
                    Resources.INITIAL_TH_THUMB, Resources.INITIAL_TH_THUMB_SOUND));

                DrawDeck.Add(new PhonicsCard("ch",
                    Resources.INITIAL_CH_CHAIN, Resources.INITIAL_CH_CHAIN_SOUND,
                    Resources.INITIAL_CH_CHAIR, Resources.INITIAL_CH_CHAIR_SOUND,
                    Resources.INITIAL_CH_CHERRY, Resources.INITIAL_CH_CHERRY_SOUND,
                    Resources.INITIAL_CH_CHICKEN, Resources.INITIAL_CH_CHICKEN_SOUND,
                    Resources.INITIAL_CH_CHURCH, Resources.INITIAL_CH_CHURCH_SOUND));
            }

            if (GameID == 1)
            {
                this.Text = "Tic-Tac-Gold Consonant Auditory 2";

                DrawDeck = new List<PhonicsCard>();

                DrawDeck.Add(new PhonicsCard("b",
                    Resources.INITIAL_B_BED, Resources.INITIAL_B_BED_SOUND,
                    Resources.INITIAL_B_BEE, Resources.INITIAL_B_BEE_SOUND,
                    Resources.INITIAL_B_BIKE, Resources.INITIAL_B_BIKE_SOUND,
                    Resources.INITIAL_B_BOAT, Resources.INITIAL_B_BOAT_SOUND,
                    Resources.INITIAL_B_BUG, Resources.INITIAL_B_BUG_SOUND));

                DrawDeck.Add(new PhonicsCard("t",
                    Resources.INITIAL_T_TABLE, Resources.INITIAL_T_TABLE_SOUND,
                    Resources.INITIAL_T_TEETH, Resources.INITIAL_T_TEETH_SOUND,
                    Resources.INITIAL_T_TELEVISION, Resources.INITIAL_T_TELEVISION_SOUND,
                    Resources.INITIAL_T_TENT, Resources.INITIAL_T_TENT_SOUND,
                    Resources.INITIAL_T_TIGER, Resources.INITIAL_T_TIGER_SOUND,
                    Resources.INITIAL_T_TOYS, Resources.INITIAL_T_TOYS_SOUND,
                    Resources.INITIAL_T_TUB, Resources.INITIAL_T_TUB_SOUND,
                    Resources.INITIAL_T_TURKEY, Resources.INITIAL_T_TURKEY_SOUND,
                    Resources.INITIAL_T_TURTLE, Resources.INITIAL_T_TURTLE_SOUND));

                DrawDeck.Add(new PhonicsCard("v",
                    Resources.INITIAL_V_VAN, Resources.INITIAl_V_VAN_SOUND,
                    Resources.INITIAL_V_VASE, Resources.INITIAL_V_VASE_SOUND,
                    Resources.INITIAL_V_VEGETABLES, Resources.INITIAL_V_VEGETABLES_SOUND,
                    Resources.INITIAL_V_VEST, Resources.INITIAL_V_VEST_SOUND,
                    Resources.INITIAL_V_VIOLIN, Resources.INITIAL_V_VIOLIN_SOUND));

                DrawDeck.Add(new PhonicsCard("k",
                    Resources.INITIAl_K_KANGAROO, Resources.INITIAL_K_KANGAROO_SOUND,
                    Resources.INITIAL_K_KEY, Resources.INITIAL_K_KEY_SOUND,
                    Resources.INITIAL_K_KING, Resources.INITIAL_K_KING_SOUND,
                    Resources.INITIAL_K_KITE, Resources.INITIAL_K_KITE_SOUND,
                    Resources.INITIAL_K_KITTEN, Resources.INITIAL_K_KITTEN_SOUND));

                DrawDeck.Add(new PhonicsCard("l",
                    Resources.INITIAL_L_LAMB, Resources.INITIAL_L_LAMB_SOUND,
                    Resources.INITIAL_L_LAMP, Resources.INITIAL_L_LAMP_SOUND,
                    Resources.INITIAL_L_LEAF, Resources.INITIAL_L_LEAF_SOUND,
                    Resources.INITIAL_L_LEMON, Resources.INITIAL_L_LEMON_SOUND,
                    Resources.INITIAL_L_LION, Resources.INITIAL_L_LION_SOUND));

                DrawDeck.Add(new PhonicsCard("n",
                    Resources.INITIAL_N_NAIL, Resources.INITIAL_N_NAIL_SOUND,
                    Resources.INITIAL_N_NEST, Resources.INITIAL_N_NEST_SOUND,
                    Resources.INITIAL_N_NET, Resources.INITIAL_N_NET_SOUND,
                    Resources.INITIAL_N_NURSE, Resources.INITIAL_N_NURSE_SOUND,
                    Resources.INITIAL_N_NUT, Resources.INITIAL_N_NUT_SOUND));

                DrawDeck.Add(new PhonicsCard("h",
                    Resources.INITIAL_H_HAT, Resources.INITIAL_H_HAT_SOUND,
                    Resources.INITIAL_H_HEN, Resources.INITIAL_H_HEN_SOUND,
                    Resources.INITIAL_H_HIPPO, Resources.INITIAL_H_HIPPO_SOUND,
                    Resources.INITIAL_H_HOOK, Resources.INITIAL_H_HOOK_SOUND,
                    Resources.INITIAL_H_HORSE, Resources.INITIAL_H_HORSE_SOUND));

                DrawDeck.Add(new PhonicsCard("p",
                    Resources.INITIAL_P_PANDA, Resources.INITIAL_P_PANDA_SOUND,
                    Resources.INITIAL_P_PEAR, Resources.INITIAL_P_PEAR_SOUND,
                    Resources.INITIAL_P_PIE, Resources.INITIAL_P_PIE_SOUND,
                    Resources.INITIAL_P_PIG, Resources.INITIAL_P_PIG_SOUND,
                    Resources.INITIAL_P_PINEAPPLE, Resources.INITIAL_P_PINEAPPLE_SOUND));

                DrawDeck.Add(new PhonicsCard("z",
                    Resources.INITIAL_Z_ZEBRA, Resources.INITIAL_Z_ZEBRA_SOUND,
                    Resources.INITIAL_Z_ZERO, Resources.INITIAL_Z_ZERO_SOUND,
                    Resources.INITIAL_Z_ZIPPER, Resources.INITIAl_Z_ZIPPER_SOUND,
                    Resources.INITIAL_Z_ZOO, Resources.INITIAL_Z_ZOO_SOUND));

                DrawDeck.Add(new PhonicsCard("sh",
                    Resources.INITIAL_SH_SHARK, Resources.INITIAL_SH_SHARK_SOUND,
                    Resources.INITIAL_SH_SHEEP, Resources.INITIAL_SH_SHEEP_SOUND,
                    Resources.INITIAL_SH_SHELL, Resources.INITIAL_SH_SHELL_SOUND,
                    Resources.INITIAL_SH_SHIP, Resources.INITIAL_SH_SHIP_SOUND,
                    Resources.INITIAL_SH_SHOE, Resources.INITIAL_SH_SHOE_SOUND));

                DrawDeck.Add(new PhonicsCard("wh",
                    Resources.INITIAL_WH_WHALE, Resources.INITIAL_WH_WHALE_SOUND,
                    Resources.INITIAL_WH_WHEEL, Resources.INITIAL_WH_WHEEL_SOUND,
                    Resources.INITIAL_WH_WHIP, Resources.INITIAL_WH_WHIP_SOUND,
                    Resources.INITIAL_WH_WHISKERS, Resources.INITIAL_WH_WHISKERS_SOUND,
                    Resources.INITIAL_WH_WHISTLE, Resources.INITIAL_WH_WHISTLE_SOUND));
            }

            if (GameID == 2)
            {
                this.Text = "Tic-Tac-Gold Consonant Phonics 1";

                DrawDeck = new List<PhonicsCard>();

                DrawDeck.Add(new PhonicsCard("m", true,
                    Resources.INITIAL_M_LETTER, Resources.INITIAL_M_LETTER_SOUND,
                    Resources.INITIAL_M_MAN, Resources.INITIAL_M_MAN_SOUND,
                    Resources.INITIAL_M_MONKEY, Resources.INITIAL_M_MONKEY_SOUND,
                    Resources.INITIAL_M_MOON, Resources.INITIAL_M_MOON_SOUND,
                    Resources.INITIAL_M_MOUSE, Resources.INITIAL_M_MOUSE_SOUND));

                DrawDeck.Add(new PhonicsCard("d", true,
                    Resources.INITIAL_D_LETTER, Resources.INITIAL_D_LETTER_SOUND,
                    Resources.INITIAL_D_DESK, Resources.INITIAL_D_DESK_SOUND,
                    Resources.INITIAL_D_DICE, Resources.INITIAL_D_DICE_SOUND,
                    Resources.INITIAL_D_DOG, Resources.INITIAL_D_DOG_SOUND,
                    Resources.INITIAL_D_DOOR, Resources.INITIAL_D_DOOR_SOUND,
                    Resources.INITIAL_D_DUCK, Resources.INITIAL_D_DUCK_SOUND));

                DrawDeck.Add(new PhonicsCard("f", true,
                    Resources.INITIAL_F_LETTER, Resources.INITIAL_F_LETTER_SOUND,
                    Resources.INITIAL_F_FAN, Resources.INITIAL_F_FAN_SOUND,
                    Resources.INITIAL_F_FARMER, Resources.INITIAL_F_FARMER_SOUND,
                    Resources.INITIAL_F_FISH, Resources.INITIAL_F_FISH_SOUND,
                    Resources.INITIAL_F_FORK, Resources.INITIAL_F_FORK_SOUND,
                    Resources.INITIAL_F_FOX, Resources.INITIAL_F_FOX_SOUND));

                DrawDeck.Add(new PhonicsCard("g", true,
                    Resources.INITIAL_G_LETTER, Resources.INITIAL_G_LETTER_SOUND,
                    Resources.INITIAL_G_GAME, Resources.INITIAL_G_GAME_SOUND,
                    Resources.INITIAL_G_GATE, Resources.INITIAL_G_GATE_SOUND,
                    Resources.INITIAL_G_GIRL, Resources.INITIAL_G_GIRL_SOUND,
                    Resources.INITIAL_G_GOAT, Resources.INITIAL_G_GOAT_SOUND,
                    Resources.INITIAL_G_GOOSE, Resources.INITIAL_G_GOOSE_SOUND));

                DrawDeck.Add(new PhonicsCard("s", true,
                    Resources.INITIAL_S_LETTER, Resources.INITIAL_S_LETTER_SOUND,
                    Resources.INITIAL_S_SAW, Resources.INITIAL_S_SAW_SOUND,
                    Resources.INITIAL_S_SEAL, Resources.INITIAL_S_SEAL_SOuND,
                    Resources.INITIAL_S_SINK, Resources.INITIAL_S_SINK_SOUND,
                    Resources.INITIAL_S_SUIT, Resources.INITIAL_S_SUIT_SOUND,
                    Resources.INITIAL_S_SUN, Resources.INITIAL_S_SUN_SOUND));

                DrawDeck.Add(new PhonicsCard("r", true,
                    Resources.INITIAL_R_LETTER, Resources.INITIAL_R_LETTER_SOUND,
                    Resources.INITIAL_R_RABBIT, Resources.INITIAL_R_RABBIT_SOUND,
                    Resources.INITIAL_R_RAINBOW, Resources.INITIAL_R_RAINBOW_SOUND,
                    Resources.INITIAL_R_RING, Resources.INITIAL_R_RING_SOUND,
                    Resources.INITIAL_R_ROBOT, Resources.INITIAL_R_ROBOT_SOUND,
                    Resources.INITIAL_R_ROOSTER, Resources.INITIAL_R_ROOSTER_SOUND));

                DrawDeck.Add(new PhonicsCard("w", true,
                    Resources.INITIAL_W_LETTER, Resources.INITIAL_W_LETTER_SOUND,
                    Resources.INITIAL_W_WAGON, Resources.INITIAL_W_WAGON_SOUND,
                    Resources.INITIAL_W_WATCH, Resources.INITIAL_W_WATCH_SOUND,
                    Resources.INITIAL_W_WATERMELON, Resources.INITIAL_W_WATERMELON_SOUND,
                    Resources.INITIAL_W_WEB, Resources.INITIAL_W_WEB_SOUND,
                    Resources.INITIAL_W_WINDOW, Resources.INITIAL_W_WINDOW_SOUND));

                DrawDeck.Add(new PhonicsCard("c", true,
                    Resources.INITIAL_C_LETTER, Resources.INITIAL_C_LETTER_SOUND,
                    Resources.INITIAL_C_CAR, Resources.INITIAL_C_CAR_SOUND,
                    Resources.INITIAL_C_COAT, Resources.INITIAL_C_COAT_SOUND,
                    Resources.INITIAL_C_CORN, Resources.INITIAL_C_CORN_SOUND,
                    Resources.INITIAl_C_COW, Resources.INITIAL_C_COW_SOUND,
                    Resources.INITIAL_C_CUP, Resources.INITIAL_C_CUP_SOUND));

                DrawDeck.Add(new PhonicsCard("j", true,
                    Resources.INITIAL_J_LETTER, Resources.INITIAL_J_LETTER_SOUND,
                    Resources.INITIAL_J_JACKET, Resources.INITIAL_J_JACKET_SOUND,
                    Resources.INITIAL_J_JAIL, Resources.INITIAL_J_JAIL_SOUND,
                    Resources.INITIAL_J_JEEP, Resources.INITIAL_J_JEEP_SOUND,
                    Resources.INITIAL_J_JET, Resources.INITIAL_J_JET_SOUND,
                    Resources.INITIAL_J_JUGGLER, Resources.INITIAL_J_JUGGLER_SOUND));

                DrawDeck.Add(new PhonicsCard("y", true,
                    Resources.INITIAL_Y_LETTER, Resources.INITIAL_Y_LETTER_SOUND,
                    Resources.INITIAL_Y_YARN, Resources.INITIAL_Y_YARN_SOUND,
                    Resources.INITIAL_Y_YAWN, Resources.INITIAl_Y_YAWN_SOUND,
                    Resources.INITIAL_Y_YOLK, Resources.INITIAL_Y_YOLK_SOUND,
                    Resources.INITIAL_Y_YOYO, Resources.INITIAL_Y_YOYO_SOUND));

                DrawDeck.Add(new PhonicsCard("th", true,
                    Resources.INITIAL_TH_LETTERS, Resources.INITIAL_TH_LETTERS_SOUND,
                    Resources.INITIAL_TH_THERMOMETER, Resources.INITIAL_TH_THERMOMETER_SOUND,
                    Resources.INITIAL_TH_THIMBLE, Resources.INITIAL_TH_THIMBLE_SOUND,
                    Resources.INITIAL_TH_THORN, Resources.INITIAL_TH_THORN_SOUND,
                    Resources.INITIAL_TH_THUMB, Resources.INITIAL_TH_THUMB_SOUND));

                DrawDeck.Add(new PhonicsCard("ch", true,
                    Resources.INITIAL_CH_LETTERS, Resources.INITIAL_CH_LETTERS_SOUND,
                    Resources.INITIAL_CH_CHAIN, Resources.INITIAL_CH_CHAIN_SOUND,
                    Resources.INITIAL_CH_CHAIR, Resources.INITIAL_CH_CHAIR_SOUND,
                    Resources.INITIAL_CH_CHERRY, Resources.INITIAL_CH_CHERRY_SOUND,
                    Resources.INITIAL_CH_CHICKEN, Resources.INITIAL_CH_CHICKEN_SOUND,
                    Resources.INITIAL_CH_CHURCH, Resources.INITIAL_CH_CHURCH_SOUND));
            }

            if (GameID == 3)
            {
                this.Text = "Tic-Tac-Gold Consonant Phonics 2";

                DrawDeck = new List<PhonicsCard>();

                DrawDeck.Add(new PhonicsCard("b", true,
                    Resources.INITIAL_B_LETTER, Resources.INITIAL_B_LETTER_SOUND,
                    Resources.INITIAL_B_BED, Resources.INITIAL_B_BED_SOUND,
                    Resources.INITIAL_B_BEE, Resources.INITIAL_B_BEE_SOUND,
                    Resources.INITIAL_B_BIKE, Resources.INITIAL_B_BIKE_SOUND,
                    Resources.INITIAL_B_BOAT, Resources.INITIAL_B_BOAT_SOUND,
                    Resources.INITIAL_B_BUG, Resources.INITIAL_B_BUG_SOUND));

                DrawDeck.Add(new PhonicsCard("t", true,
                    Resources.INITIAL_T_LETTER, Resources.INITIAL_T_LETTER_SOUND,
                    Resources.INITIAL_T_TABLE, Resources.INITIAL_T_TABLE_SOUND,
                    Resources.INITIAL_T_TEETH, Resources.INITIAL_T_TEETH_SOUND,
                    Resources.INITIAL_T_TELEVISION, Resources.INITIAL_T_TELEVISION_SOUND,
                    Resources.INITIAL_T_TENT, Resources.INITIAL_T_TENT_SOUND,
                    Resources.INITIAL_T_TIGER, Resources.INITIAL_T_TIGER_SOUND,
                    Resources.INITIAL_T_TOYS, Resources.INITIAL_T_TOYS_SOUND,
                    Resources.INITIAL_T_TUB, Resources.INITIAL_T_TUB_SOUND,
                    Resources.INITIAL_T_TURKEY, Resources.INITIAL_T_TURKEY_SOUND,
                    Resources.INITIAL_T_TURTLE, Resources.INITIAL_T_TURTLE_SOUND));

                DrawDeck.Add(new PhonicsCard("v", true,
                    Resources.INITIAL_V_LETTER, Resources.INITIAL_V_LETTER_SOUND,
                    Resources.INITIAL_V_VAN, Resources.INITIAl_V_VAN_SOUND,
                    Resources.INITIAL_V_VASE, Resources.INITIAL_V_VASE_SOUND,
                    Resources.INITIAL_V_VEGETABLES, Resources.INITIAL_V_VEGETABLES_SOUND,
                    Resources.INITIAL_V_VEST, Resources.INITIAL_V_VEST_SOUND,
                    Resources.INITIAL_V_VIOLIN, Resources.INITIAL_V_VIOLIN_SOUND));

                DrawDeck.Add(new PhonicsCard("k", true,
                    Resources.INITIAL_K_LETTER, Resources.INITIAL_K_LETTER_SOUND,
                    Resources.INITIAl_K_KANGAROO, Resources.INITIAL_K_KANGAROO_SOUND,
                    Resources.INITIAL_K_KEY, Resources.INITIAL_K_KEY_SOUND,
                    Resources.INITIAL_K_KING, Resources.INITIAL_K_KING_SOUND,
                    Resources.INITIAL_K_KITE, Resources.INITIAL_K_KITE_SOUND,
                    Resources.INITIAL_K_KITTEN, Resources.INITIAL_K_KITTEN_SOUND));

                DrawDeck.Add(new PhonicsCard("l", true,
                    Resources.INITIAL_L_LETTER, Resources.INITIAL_L_LETTER_SOUND,
                    Resources.INITIAL_L_LAMB, Resources.INITIAL_L_LAMB_SOUND,
                    Resources.INITIAL_L_LAMP, Resources.INITIAL_L_LAMP_SOUND,
                    Resources.INITIAL_L_LEAF, Resources.INITIAL_L_LEAF_SOUND,
                    Resources.INITIAL_L_LEMON, Resources.INITIAL_L_LEMON_SOUND,
                    Resources.INITIAL_L_LION, Resources.INITIAL_L_LION_SOUND));

                DrawDeck.Add(new PhonicsCard("n", true,
                    Resources.INITIAL_N_LETTER, Resources.INITIAL_N_LETTER_SOUND,
                    Resources.INITIAL_N_NAIL, Resources.INITIAL_N_NAIL_SOUND,
                    Resources.INITIAL_N_NEST, Resources.INITIAL_N_NEST_SOUND,
                    Resources.INITIAL_N_NET, Resources.INITIAL_N_NET_SOUND,
                    Resources.INITIAL_N_NURSE, Resources.INITIAL_N_NURSE_SOUND,
                    Resources.INITIAL_N_NUT, Resources.INITIAL_N_NUT_SOUND));

                DrawDeck.Add(new PhonicsCard("h", true,
                    Resources.INITIAL_H_LETTER, Resources.INITIAL_H_LETTER_SOUND,
                    Resources.INITIAL_H_HAT, Resources.INITIAL_H_HAT_SOUND,
                    Resources.INITIAL_H_HEN, Resources.INITIAL_H_HEN_SOUND,
                    Resources.INITIAL_H_HIPPO, Resources.INITIAL_H_HIPPO_SOUND,
                    Resources.INITIAL_H_HOOK, Resources.INITIAL_H_HOOK_SOUND,
                    Resources.INITIAL_H_HORSE, Resources.INITIAL_H_HORSE_SOUND));

                DrawDeck.Add(new PhonicsCard("p", true,
                    Resources.INITIAL_P_LETTER, Resources.INITIAL_P_LETTER_SOUND,
                    Resources.INITIAL_P_PANDA, Resources.INITIAL_P_PANDA_SOUND,
                    Resources.INITIAL_P_PEAR, Resources.INITIAL_P_PEAR_SOUND,
                    Resources.INITIAL_P_PIE, Resources.INITIAL_P_PIE_SOUND,
                    Resources.INITIAL_P_PIG, Resources.INITIAL_P_PIG_SOUND,
                    Resources.INITIAL_P_PINEAPPLE, Resources.INITIAL_P_PINEAPPLE_SOUND));

                DrawDeck.Add(new PhonicsCard("z", true,
                    Resources.INITIAL_Z_LETTER, Resources.INITIAL_Z_LETTER_SOUND,
                    Resources.INITIAL_Z_ZEBRA, Resources.INITIAL_Z_ZEBRA_SOUND,
                    Resources.INITIAL_Z_ZERO, Resources.INITIAL_Z_ZERO_SOUND,
                    Resources.INITIAL_Z_ZIPPER, Resources.INITIAl_Z_ZIPPER_SOUND,
                    Resources.INITIAL_Z_ZOO, Resources.INITIAL_Z_ZOO_SOUND));

                DrawDeck.Add(new PhonicsCard("sh", true,
                    Resources.INITIAl_SH_LETTERS, Resources.INITIAL_SH_LETTERS_SOUND,
                    Resources.INITIAL_SH_SHARK, Resources.INITIAL_SH_SHARK_SOUND,
                    Resources.INITIAL_SH_SHEEP, Resources.INITIAL_SH_SHEEP_SOUND,
                    Resources.INITIAL_SH_SHELL, Resources.INITIAL_SH_SHELL_SOUND,
                    Resources.INITIAL_SH_SHIP, Resources.INITIAL_SH_SHIP_SOUND,
                    Resources.INITIAL_SH_SHOE, Resources.INITIAL_SH_SHOE_SOUND));

                DrawDeck.Add(new PhonicsCard("wh", true,
                    Resources.INITIAL_WH_LETTERS, Resources.INITIAL_WH_LETTERS_SOUND,
                    Resources.INITIAL_WH_WHALE, Resources.INITIAL_WH_WHALE_SOUND,
                    Resources.INITIAL_WH_WHEEL, Resources.INITIAL_WH_WHEEL_SOUND,
                    Resources.INITIAL_WH_WHIP, Resources.INITIAL_WH_WHIP_SOUND,
                    Resources.INITIAL_WH_WHISKERS, Resources.INITIAL_WH_WHISKERS_SOUND,
                    Resources.INITIAL_WH_WHISTLE, Resources.INITIAL_WH_WHISTLE_SOUND));
            }

            if (GameID == 4)
            {
                this.Text = "Tic-Tac-Gold Vowels Auditory 1";

                DrawDeck = new List<PhonicsCard>();

                DrawDeck.Add(new PhonicsCard("longa",
                    Resources.LONG_A_APE, Resources.LONG_A_APE_SOUND,
                    Resources.LONG_A_CAKE, Resources.LONG_A_CAKE_SOUND,
                    Resources.LONG_A_TRAIN, Resources.LONG_A_TRAIN_SOUND,
                    Resources.LONG_A_WHALE, Resources.LONG_A_WHALE_SOUND));

                DrawDeck.Add(new PhonicsCard("longe",
                    Resources.LONG_E_BEE, Resources.LONG_E_BEE_SOUND,
                    Resources.LONG_E_JEEP, Resources.LONG_E_JEEP_SOUND,
                    Resources.LONG_E_LEAF, Resources.LONG_E_LEAF_SOUND,
                    Resources.LONG_E_SEAL, Resources.LONG_E_SEAL_SOUND));

                DrawDeck.Add(new PhonicsCard("longi",
                    Resources.LONG_I_BIKE, Resources.LONG_I_BIKE_SOUND,
                    Resources.LONG_I_KITE, Resources.LONG_I_KITE_SOUND,
                    Resources.LONG_I_PIE, Resources.LONG_I_PIE_SOUND,
                    Resources.LONG_I_TIE, Resources.LONG_I_TIE_SOUND));

                DrawDeck.Add(new PhonicsCard("longo",
                    Resources.LONG_O_BOAT, Resources.LONG_O_BOAT_SOUND,
                    Resources.LONG_O_NOSE, Resources.LONG_O_NOSE_SOUND,
                    Resources.LONG_O_ROSE, Resources.LONG_O_ROSE_SOUND,
                    Resources.LONG_O_SNOW, Resources.LONG_O_SNOW_SOUND));

                DrawDeck.Add(new PhonicsCard("longu",
                    Resources.LONG_U_CUBE, Resources.LONG_U_CUBE_SOUND,
                    Resources.LONG_U_MULE, Resources.LONG_U_MULE_SOUND,
                    Resources.LONG_U_UNICORN, Resources.LONG_U_UNICORN_SOUND));

                DrawDeck.Add(new PhonicsCard("shorta",
                    Resources.SHORT_A_ANT, Resources.SHORT_A_ANT_SOUND,
                    Resources.SHORT_A_CAT, Resources.SHORT_A_CAT_SOUND,
                    Resources.SHORT_A_HAT, Resources.SHORT_A_HAT_SOUND,
                    Resources.SHORT_A_LAMP, Resources.SHORT_A_LAMP_SOUND,
                    Resources.SHORT_A_MAN, Resources.SHORT_A_MAN_SOUND));

                DrawDeck.Add(new PhonicsCard("shorte",
                    Resources.SHORT_E_BED, Resources.SHORT_E_BED_SOUND,
                    Resources.SHORT_E_JET, Resources.SHORT_E_JET_SOUND,
                    Resources.SHORT_E_NEST, Resources.SHORT_E_NEST_SOUND,
                    Resources.SHORT_E_TENT, Resources.SHORT_E_TENT_SOUND,
                    Resources.SHORT_E_WEB, Resources.SHORT_E_WEB_SOUND));

                DrawDeck.Add(new PhonicsCard("shorti",
                    Resources.SHORT_I_FISH, Resources.SHORT_I_FISH_SOUND,
                    Resources.SHORT_I_PIG, Resources.SHORT_I_PIG_SOUND,
                    Resources.SHORT_I_SHIP, Resources.SHORT_I_SHIP_SOUND,
                    Resources.SHORT_I_SINK, Resources.SHORT_I_SINK_SOUND,
                    Resources.SHORT_I_WITCH, Resources.SHORT_I_WITCH_SOUND));

                DrawDeck.Add(new PhonicsCard("shorto",
                    Resources.SHORT_O_CLOCK, Resources.SHORT_O_CLOCK_SOUND,
                    Resources.SHORT_O_DOG, Resources.SHORT_O_DOG_SOUND,
                    Resources.SHORT_O_FOX, Resources.SHORT_O_FOX_SOUND,
                    Resources.SHORT_O_MOP, Resources.SHORT_O_MOP_SOUND));

                DrawDeck.Add(new PhonicsCard("shortu",
                    Resources.SHORT_U_BUG, Resources.SHORT_U_BUG_SOUND,
                    Resources.SHORT_U_CUP, Resources.SHORT_U_CUP_SOUND,
                    Resources.SHORT_U_DUCK, Resources.SHORT_U_DUCK_SOUND,
                    Resources.SHORT_U_NUT, Resources.SHORT_U_NUT_SOUND,
                    Resources.SHORT_U_SUN, Resources.SHORT_U_SUN_SOUND));
            }

            if (GameID == 5)
            {
                this.Text = "Tic-Tac-Gold Vowels Auditory 2";

                DrawDeck = new List<PhonicsCard>();

                // Pick two random long cards and two random short cards
                PhonicsCard long1, long2, short1, short2;

                DrawDeck.Add(new PhonicsCard("longa",
                    Resources.LONG_A_APE, Resources.LONG_A_APE_SOUND,
                    Resources.LONG_A_CAKE, Resources.LONG_A_CAKE_SOUND,
                    Resources.LONG_A_TRAIN, Resources.LONG_A_TRAIN_SOUND,
                    Resources.LONG_A_WHALE, Resources.LONG_A_WHALE_SOUND));

                DrawDeck.Add(new PhonicsCard("longe",
                    Resources.LONG_E_BEE, Resources.LONG_E_BEE_SOUND,
                    Resources.LONG_E_JEEP, Resources.LONG_E_JEEP_SOUND,
                    Resources.LONG_E_LEAF, Resources.LONG_E_LEAF_SOUND,
                    Resources.LONG_E_SEAL, Resources.LONG_E_SEAL_SOUND));

                DrawDeck.Add(new PhonicsCard("longi",
                    Resources.LONG_I_BIKE, Resources.LONG_I_BIKE_SOUND,
                    Resources.LONG_I_KITE, Resources.LONG_I_KITE_SOUND,
                    Resources.LONG_I_PIE, Resources.LONG_I_PIE_SOUND,
                    Resources.LONG_I_TIE, Resources.LONG_I_TIE_SOUND));

                DrawDeck.Add(new PhonicsCard("longo",
                    Resources.LONG_O_BOAT, Resources.LONG_O_BOAT_SOUND,
                    Resources.LONG_O_NOSE, Resources.LONG_O_NOSE_SOUND,
                    Resources.LONG_O_ROSE, Resources.LONG_O_ROSE_SOUND,
                    Resources.LONG_O_SNOW, Resources.LONG_O_SNOW_SOUND));

                DrawDeck.Add(new PhonicsCard("longu",
                    Resources.LONG_U_CUBE, Resources.LONG_U_CUBE_SOUND,
                    Resources.LONG_U_MULE, Resources.LONG_U_MULE_SOUND,
                    Resources.LONG_U_UNICORN, Resources.LONG_U_UNICORN_SOUND));

                Utils.Shuffle<PhonicsCard>(DrawDeck);
                long1 = DrawDeck[0];
                long2 = DrawDeck[1];

                DrawDeck.Clear();

                DrawDeck.Add(new PhonicsCard("shorta",
                    Resources.SHORT_A_ANT, Resources.SHORT_A_ANT_SOUND,
                    Resources.SHORT_A_CAT, Resources.SHORT_A_CAT_SOUND,
                    Resources.SHORT_A_HAT, Resources.SHORT_A_HAT_SOUND,
                    Resources.SHORT_A_LAMP, Resources.SHORT_A_LAMP_SOUND,
                    Resources.SHORT_A_MAN, Resources.SHORT_A_MAN_SOUND));

                DrawDeck.Add(new PhonicsCard("shorte",
                    Resources.SHORT_E_BED, Resources.SHORT_E_BED_SOUND,
                    Resources.SHORT_E_JET, Resources.SHORT_E_JET_SOUND,
                    Resources.SHORT_E_NEST, Resources.SHORT_E_NEST_SOUND,
                    Resources.SHORT_E_TENT, Resources.SHORT_E_TENT_SOUND,
                    Resources.SHORT_E_WEB, Resources.SHORT_E_WEB_SOUND));

                DrawDeck.Add(new PhonicsCard("shorti",
                    Resources.SHORT_I_FISH, Resources.SHORT_I_FISH_SOUND,
                    Resources.SHORT_I_PIG, Resources.SHORT_I_PIG_SOUND,
                    Resources.SHORT_I_SHIP, Resources.SHORT_I_SHIP_SOUND,
                    Resources.SHORT_I_SINK, Resources.SHORT_I_SINK_SOUND,
                    Resources.SHORT_I_WITCH, Resources.SHORT_I_WITCH_SOUND));

                DrawDeck.Add(new PhonicsCard("shorto",
                    Resources.SHORT_O_CLOCK, Resources.SHORT_O_CLOCK_SOUND,
                    Resources.SHORT_O_DOG, Resources.SHORT_O_DOG_SOUND,
                    Resources.SHORT_O_FOX, Resources.SHORT_O_FOX_SOUND,
                    Resources.SHORT_O_MOP, Resources.SHORT_O_MOP_SOUND));

                DrawDeck.Add(new PhonicsCard("shortu",
                    Resources.SHORT_U_BUG, Resources.SHORT_U_BUG_SOUND,
                    Resources.SHORT_U_CUP, Resources.SHORT_U_CUP_SOUND,
                    Resources.SHORT_U_DUCK, Resources.SHORT_U_DUCK_SOUND,
                    Resources.SHORT_U_NUT, Resources.SHORT_U_NUT_SOUND,
                    Resources.SHORT_U_SUN, Resources.SHORT_U_SUN_SOUND));

                Utils.Shuffle<PhonicsCard>(DrawDeck);
                short1 = DrawDeck[0];
                short2 = DrawDeck[1];

                DrawDeck.Clear();

                DrawDeck.Add(new PhonicsCard("varieda",
                    Resources.VARIED_AR_BARN, Resources.VARIED_AR_BARN_SOUND,
                    Resources.VARIED_AR_CAR, Resources.VARIED_AR_CAR_SOUND,
                    Resources.VARIED_AR_DART, Resources.VARIED_AR_DART_SOUND,
                    Resources.VARIED_AR_SHARK, Resources.VARIED_AR_SHARK_SOUND,
                    Resources.VARIED_AR_STAR, Resources.VARIED_AR_STAR_SOUND));

                DrawDeck.Add(new PhonicsCard("variede",
                    Resources.VARIED_ER_BIRD, Resources.VARIED_ER_BIRD_SOUND,
                    Resources.VARIED_ER_CHURCH, Resources.VARIED_ER_CHURCH_SOUND,
                    Resources.VARIED_ER_GIRL, Resources.VARIED_ER_GIRL_SOUND,
                    Resources.VARIED_ER_NURSE, Resources.VARIED_ER_NURSE_SOUND));

                DrawDeck.Add(new PhonicsCard("variedi",
                    Resources.VARIED_OO_BOOT, Resources.VARIED_OO_BOOT_SOUND,
                    Resources.VARIED_OO_MOON, Resources.VARIED_OO_MOON_SOUND,
                    Resources.VARIED_OO_SCOOTER, Resources.VARIED_OO_SCOOTER_SOUND,
                    Resources.VARIED_OO_SCREW, Resources.VARIED_OO_SCREW_SOUND,
                    Resources.VARIED_OO_ZOO, Resources.VARIED_OO_ZOO_SOUND));

                DrawDeck.Add(new PhonicsCard("variedo",
                    Resources.VARIED_OR_CORN, Resources.VARIED_OR_CORN_SOUND,
                    Resources.VARIED_OR_DOOR, Resources.VARIED_OR_DOOR_SOUND,
                    Resources.VARIED_OR_FORK, Resources.VARIED_OR_FORK_SOUND,
                    Resources.VARIED_OR_HORN, Resources.VARIED_OR_HORN_SOUND,
                    Resources.VARIED_OR_HORSE, Resources.VARIED_OR_HORSE_SOUND));

                DrawDeck.Add(new PhonicsCard("variedu",
                    Resources.VARIED_OW_CLOWN, Resources.VARIED_OW_CLOWN_SOUND,
                    Resources.VARIED_OW_COW, Resources.VARIED_OW_COW_SOUND,
                    Resources.VARIED_OW_HOUSE, Resources.VARIED_OW_HOUSE_SOUND,
                    Resources.VARIED_OW_MOUSE, Resources.VARIED_OW_MOUSE_SOUND,
                    Resources.VARIED_OW_OWL, Resources.VARIED_OW_OWL_SOUND));

                DrawDeck.Add(long1);
                DrawDeck.Add(long2);
                DrawDeck.Add(short1);
                DrawDeck.Add(short2);
            }

            if (GameID == 6)
            {
                this.Text = "Tic-Tac-Gold Vowels Phonics 1";

                DrawDeck = new List<PhonicsCard>();

                DrawDeck.Add(new PhonicsCard("longa", true,
                    Resources.LONG_A_LETTER, Resources.LONG_A_SOUND,
                    Resources.LONG_A_APE, Resources.LONG_A_APE_SOUND,
                    Resources.LONG_A_CAKE, Resources.LONG_A_CAKE_SOUND,
                    Resources.LONG_A_TRAIN, Resources.LONG_A_TRAIN_SOUND,
                    Resources.LONG_A_WHALE, Resources.LONG_A_WHALE_SOUND));

                DrawDeck.Add(new PhonicsCard("longe", true,
                    Resources.LONG_E_LETTER, Resources.LONG_E_SOUND,
                    Resources.LONG_E_BEE, Resources.LONG_E_BEE_SOUND,
                    Resources.LONG_E_JEEP, Resources.LONG_E_JEEP_SOUND,
                    Resources.LONG_E_LEAF, Resources.LONG_E_LEAF_SOUND,
                    Resources.LONG_E_SEAL, Resources.LONG_E_SEAL_SOUND));

                DrawDeck.Add(new PhonicsCard("longi", true,
                    Resources.LONG_I_LETTER, Resources.LONG_I_SOUND,
                    Resources.LONG_I_BIKE, Resources.LONG_I_BIKE_SOUND,
                    Resources.LONG_I_KITE, Resources.LONG_I_KITE_SOUND,
                    Resources.LONG_I_PIE, Resources.LONG_I_PIE_SOUND,
                    Resources.LONG_I_TIE, Resources.LONG_I_TIE_SOUND));

                DrawDeck.Add(new PhonicsCard("longo", true,
                    Resources.LONG_O_LETTER, Resources.LONG_O_SOUND,
                    Resources.LONG_O_BOAT, Resources.LONG_O_BOAT_SOUND,
                    Resources.LONG_O_NOSE, Resources.LONG_O_NOSE_SOUND,
                    Resources.LONG_O_ROSE, Resources.LONG_O_ROSE_SOUND,
                    Resources.LONG_O_SNOW, Resources.LONG_O_SNOW_SOUND));

                DrawDeck.Add(new PhonicsCard("longu", true,
                    Resources.LONG_U_LETTER, Resources.LONG_U_SOUND,
                    Resources.LONG_U_CUBE, Resources.LONG_U_CUBE_SOUND,
                    Resources.LONG_U_MULE, Resources.LONG_U_MULE_SOUND,
                    Resources.LONG_U_UNICORN, Resources.LONG_U_UNICORN_SOUND));

                DrawDeck.Add(new PhonicsCard("shorta", true,
                    Resources.SHORT_A_LETTER, Resources.SHORT_A_LETTER_SOUND,
                    Resources.SHORT_A_ANT, Resources.SHORT_A_ANT_SOUND,
                    Resources.SHORT_A_CAT, Resources.SHORT_A_CAT_SOUND,
                    Resources.SHORT_A_HAT, Resources.SHORT_A_HAT_SOUND,
                    Resources.SHORT_A_LAMP, Resources.SHORT_A_LAMP_SOUND,
                    Resources.SHORT_A_MAN, Resources.SHORT_A_MAN_SOUND));

                DrawDeck.Add(new PhonicsCard("shorte", true,
                    Resources.SHORT_E_LETTER, Resources.SHORT_E_LETTER_SOUND,
                    Resources.SHORT_E_BED, Resources.SHORT_E_BED_SOUND,
                    Resources.SHORT_E_JET, Resources.SHORT_E_JET_SOUND,
                    Resources.SHORT_E_NEST, Resources.SHORT_E_NEST_SOUND,
                    Resources.SHORT_E_TENT, Resources.SHORT_E_TENT_SOUND,
                    Resources.SHORT_E_WEB, Resources.SHORT_E_WEB_SOUND));

                DrawDeck.Add(new PhonicsCard("shorti", true,
                    Resources.SHORT_I_LETTER, Resources.SHORT_I_LETTER_SOUND,
                    Resources.SHORT_I_FISH, Resources.SHORT_I_FISH_SOUND,
                    Resources.SHORT_I_PIG, Resources.SHORT_I_PIG_SOUND,
                    Resources.SHORT_I_SHIP, Resources.SHORT_I_SHIP_SOUND,
                    Resources.SHORT_I_SINK, Resources.SHORT_I_SINK_SOUND,
                    Resources.SHORT_I_WITCH, Resources.SHORT_I_WITCH_SOUND));

                DrawDeck.Add(new PhonicsCard("shorto", true,
                    Resources.SHORT_O_LETTER, Resources.SHORT_O_LETTER_SOUND,
                    Resources.SHORT_O_CLOCK, Resources.SHORT_O_CLOCK_SOUND,
                    Resources.SHORT_O_DOG, Resources.SHORT_O_DOG_SOUND,
                    Resources.SHORT_O_FOX, Resources.SHORT_O_FOX_SOUND,
                    Resources.SHORT_O_MOP, Resources.SHORT_O_MOP_SOUND));

                DrawDeck.Add(new PhonicsCard("shortu", true,
                    Resources.SHORT_U_LETTER, Resources.SHORT_U_LETTER_SOUND,
                    Resources.SHORT_U_BUG, Resources.SHORT_U_BUG_SOUND,
                    Resources.SHORT_U_CUP, Resources.SHORT_U_CUP_SOUND,
                    Resources.SHORT_U_DUCK, Resources.SHORT_U_DUCK_SOUND,
                    Resources.SHORT_U_NUT, Resources.SHORT_U_NUT_SOUND,
                    Resources.SHORT_U_SUN, Resources.SHORT_U_SUN_SOUND));
            }

            if (GameID == 7)
            {
                this.Text = "Tic-Tac-Gold Vowels Phonics 2";

                DrawDeck = new List<PhonicsCard>();

                // Pick two random long cards and two random short cards
                PhonicsCard long1, long2, short1, short2;

                DrawDeck.Add(new PhonicsCard("longa", true,
                    Resources.LONG_A_LETTER, Resources.LONG_A_SOUND,
                    Resources.LONG_A_APE, Resources.LONG_A_APE_SOUND,
                    Resources.LONG_A_CAKE, Resources.LONG_A_CAKE_SOUND,
                    Resources.LONG_A_TRAIN, Resources.LONG_A_TRAIN_SOUND,
                    Resources.LONG_A_WHALE, Resources.LONG_A_WHALE_SOUND));

                DrawDeck.Add(new PhonicsCard("longe", true,
                    Resources.LONG_E_LETTER, Resources.LONG_E_SOUND,
                    Resources.LONG_E_BEE, Resources.LONG_E_BEE_SOUND,
                    Resources.LONG_E_JEEP, Resources.LONG_E_JEEP_SOUND,
                    Resources.LONG_E_LEAF, Resources.LONG_E_LEAF_SOUND,
                    Resources.LONG_E_SEAL, Resources.LONG_E_SEAL_SOUND));

                DrawDeck.Add(new PhonicsCard("longi", true,
                    Resources.LONG_I_LETTER, Resources.LONG_I_SOUND,
                    Resources.LONG_I_BIKE, Resources.LONG_I_BIKE_SOUND,
                    Resources.LONG_I_KITE, Resources.LONG_I_KITE_SOUND,
                    Resources.LONG_I_PIE, Resources.LONG_I_PIE_SOUND,
                    Resources.LONG_I_TIE, Resources.LONG_I_TIE_SOUND));

                DrawDeck.Add(new PhonicsCard("longo", true,
                    Resources.LONG_O_LETTER, Resources.LONG_O_SOUND,
                    Resources.LONG_O_BOAT, Resources.LONG_O_BOAT_SOUND,
                    Resources.LONG_O_NOSE, Resources.LONG_O_NOSE_SOUND,
                    Resources.LONG_O_ROSE, Resources.LONG_O_ROSE_SOUND,
                    Resources.LONG_O_SNOW, Resources.LONG_O_SNOW_SOUND));

                DrawDeck.Add(new PhonicsCard("longu", true,
                    Resources.LONG_U_LETTER, Resources.LONG_U_SOUND,
                    Resources.LONG_U_CUBE, Resources.LONG_U_CUBE_SOUND,
                    Resources.LONG_U_MULE, Resources.LONG_U_MULE_SOUND,
                    Resources.LONG_U_UNICORN, Resources.LONG_U_UNICORN_SOUND));

                Utils.Shuffle<PhonicsCard>(DrawDeck);
                long1 = DrawDeck[0];
                long2 = DrawDeck[1];

                DrawDeck.Clear();

                DrawDeck.Add(new PhonicsCard("shorta", true,
                    Resources.SHORT_A_LETTER, Resources.SHORT_A_LETTER_SOUND,
                    Resources.SHORT_A_ANT, Resources.SHORT_A_ANT_SOUND,
                    Resources.SHORT_A_CAT, Resources.SHORT_A_CAT_SOUND,
                    Resources.SHORT_A_HAT, Resources.SHORT_A_HAT_SOUND,
                    Resources.SHORT_A_LAMP, Resources.SHORT_A_LAMP_SOUND,
                    Resources.SHORT_A_MAN, Resources.SHORT_A_MAN_SOUND));

                DrawDeck.Add(new PhonicsCard("shorte", true,
                    Resources.SHORT_E_LETTER, Resources.SHORT_E_LETTER_SOUND,
                    Resources.SHORT_E_BED, Resources.SHORT_E_BED_SOUND,
                    Resources.SHORT_E_JET, Resources.SHORT_E_JET_SOUND,
                    Resources.SHORT_E_NEST, Resources.SHORT_E_NEST_SOUND,
                    Resources.SHORT_E_TENT, Resources.SHORT_E_TENT_SOUND,
                    Resources.SHORT_E_WEB, Resources.SHORT_E_WEB_SOUND));

                DrawDeck.Add(new PhonicsCard("shorti", true,
                    Resources.SHORT_I_LETTER, Resources.SHORT_I_LETTER_SOUND,
                    Resources.SHORT_I_FISH, Resources.SHORT_I_FISH_SOUND,
                    Resources.SHORT_I_PIG, Resources.SHORT_I_PIG_SOUND,
                    Resources.SHORT_I_SHIP, Resources.SHORT_I_SHIP_SOUND,
                    Resources.SHORT_I_SINK, Resources.SHORT_I_SINK_SOUND,
                    Resources.SHORT_I_WITCH, Resources.SHORT_I_WITCH_SOUND));

                DrawDeck.Add(new PhonicsCard("shorto", true,
                    Resources.SHORT_O_LETTER, Resources.SHORT_O_LETTER_SOUND,
                    Resources.SHORT_O_CLOCK, Resources.SHORT_O_CLOCK_SOUND,
                    Resources.SHORT_O_DOG, Resources.SHORT_O_DOG_SOUND,
                    Resources.SHORT_O_FOX, Resources.SHORT_O_FOX_SOUND,
                    Resources.SHORT_O_MOP, Resources.SHORT_O_MOP_SOUND));

                DrawDeck.Add(new PhonicsCard("shortu", true,
                    Resources.SHORT_U_LETTER, Resources.SHORT_U_LETTER_SOUND,
                    Resources.SHORT_U_BUG, Resources.SHORT_U_BUG_SOUND,
                    Resources.SHORT_U_CUP, Resources.SHORT_U_CUP_SOUND,
                    Resources.SHORT_U_DUCK, Resources.SHORT_U_DUCK_SOUND,
                    Resources.SHORT_U_NUT, Resources.SHORT_U_NUT_SOUND,
                    Resources.SHORT_U_SUN, Resources.SHORT_U_SUN_SOUND));

                Utils.Shuffle<PhonicsCard>(DrawDeck);
                short1 = DrawDeck[0];
                short2 = DrawDeck[1];

                DrawDeck.Clear();

                DrawDeck.Add(new PhonicsCard("variedar", true,
                    Resources.VARIED_AR_LETTERS, Resources.VARIED_AR_LETTERS_SOUND,
                    Resources.VARIED_AR_BARN, Resources.VARIED_AR_BARN_SOUND,
                    Resources.VARIED_AR_CAR, Resources.VARIED_AR_CAR_SOUND,
                    Resources.VARIED_AR_DART, Resources.VARIED_AR_DART_SOUND,
                    Resources.VARIED_AR_SHARK, Resources.VARIED_AR_SHARK_SOUND,
                    Resources.VARIED_AR_STAR, Resources.VARIED_AR_STAR_SOUND));

                DrawDeck.Add(new PhonicsCard("varieder", true,
                    Resources.VARIED_ER_LETTERS, Resources.VARIED_ER_LETTERS_SOUND,
                    Resources.VARIED_ER_BIRD, Resources.VARIED_ER_BIRD_SOUND,
                    Resources.VARIED_ER_CHURCH, Resources.VARIED_ER_CHURCH_SOUND,
                    Resources.VARIED_ER_GIRL, Resources.VARIED_ER_GIRL_SOUND,
                    Resources.VARIED_ER_NURSE, Resources.VARIED_ER_NURSE_SOUND));

                DrawDeck.Add(new PhonicsCard("variedoo", true,
                    Resources.VARIED_OO_LETTERS, Resources.VARIED_OO_LETTERS_SOUND,
                    Resources.VARIED_OO_BOOT, Resources.VARIED_OO_BOOT_SOUND,
                    Resources.VARIED_OO_MOON, Resources.VARIED_OO_MOON_SOUND,
                    Resources.VARIED_OO_SCOOTER, Resources.VARIED_OO_SCOOTER_SOUND,
                    Resources.VARIED_OO_SCREW, Resources.VARIED_OO_SCREW_SOUND,
                    Resources.VARIED_OO_ZOO, Resources.VARIED_OO_ZOO_SOUND));

                DrawDeck.Add(new PhonicsCard("variedor", true,
                    Resources.VARIED_OR_LETTERS, Resources.VARIED_OR_LETTERS_SOUND,
                    Resources.VARIED_OR_CORN, Resources.VARIED_OR_CORN_SOUND,
                    Resources.VARIED_OR_DOOR, Resources.VARIED_OR_DOOR_SOUND,
                    Resources.VARIED_OR_FORK, Resources.VARIED_OR_FORK_SOUND,
                    Resources.VARIED_OR_HORN, Resources.VARIED_OR_HORN_SOUND,
                    Resources.VARIED_OR_HORSE, Resources.VARIED_OR_HORSE_SOUND));

                DrawDeck.Add(new PhonicsCard("variedow", true,
                    Resources.VARIED_OW_LETTERS, Resources.VARIED_OW_LETTERS_SOUND,
                    Resources.VARIED_OW_CLOWN, Resources.VARIED_OW_CLOWN_SOUND,
                    Resources.VARIED_OW_COW, Resources.VARIED_OW_COW_SOUND,
                    Resources.VARIED_OW_HOUSE, Resources.VARIED_OW_HOUSE_SOUND,
                    Resources.VARIED_OW_MOUSE, Resources.VARIED_OW_MOUSE_SOUND,
                    Resources.VARIED_OW_OWL, Resources.VARIED_OW_OWL_SOUND));

                DrawDeck.Add(long1);
                DrawDeck.Add(long2);
                DrawDeck.Add(short1);
                DrawDeck.Add(short2);
            }

            if (GameID == 8)
            {
                this.Text = "Tic-Tac-Gold Letter Recognition Lower to Lower";

                DrawDeck = new List<PhonicsCard>();

                DrawDeck.Add(new PhonicsCard("m", true,
                    Resources.INITIAL_M_LETTER, Resources.INITIAL_M_LETTER_SOUND,
                    Resources.INITIAL_M_LETTER, Resources.INITIAL_M_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("d", true,
                    Resources.INITIAL_D_LETTER, Resources.INITIAL_D_LETTER_SOUND,
                    Resources.INITIAL_D_LETTER, Resources.INITIAL_D_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("f", true,
                    Resources.INITIAL_F_LETTER, Resources.INITIAL_F_LETTER_SOUND,
                    Resources.INITIAL_F_LETTER, Resources.INITIAL_F_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("g", true,
                    Resources.INITIAL_G_LETTER, Resources.INITIAL_G_LETTER_SOUND,
                    Resources.INITIAL_G_LETTER, Resources.INITIAL_G_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("s", true,
                    Resources.INITIAL_S_LETTER, Resources.INITIAL_S_LETTER_SOUND,
                    Resources.INITIAL_S_LETTER, Resources.INITIAL_S_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("r", true,
                    Resources.INITIAL_R_LETTER, Resources.INITIAL_R_LETTER_SOUND,
                    Resources.INITIAL_R_LETTER, Resources.INITIAL_R_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("w", true,
                    Resources.INITIAL_W_LETTER, Resources.INITIAL_W_LETTER_SOUND,
                    Resources.INITIAL_W_LETTER, Resources.INITIAL_W_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("c", true,
                    Resources.INITIAL_C_LETTER, Resources.INITIAL_C_LETTER_SOUND,
                    Resources.INITIAL_C_LETTER, Resources.INITIAL_C_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("j", true,
                    Resources.INITIAL_J_LETTER, Resources.INITIAL_J_LETTER_SOUND,
                    Resources.INITIAL_J_LETTER, Resources.INITIAL_J_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("y", true,
                    Resources.INITIAL_Y_LETTER, Resources.INITIAL_Y_LETTER_SOUND,
                    Resources.INITIAL_Y_LETTER, Resources.INITIAL_Y_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("b", true,
                    Resources.INITIAL_B_LETTER, Resources.INITIAL_B_LETTER_SOUND,
                    Resources.INITIAL_B_LETTER, Resources.INITIAL_B_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("t", true,
                    Resources.INITIAL_T_LETTER, Resources.INITIAL_T_LETTER_SOUND,
                    Resources.INITIAL_T_LETTER, Resources.INITIAL_T_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("v", true,
                    Resources.INITIAL_V_LETTER, Resources.INITIAL_V_LETTER_SOUND,
                    Resources.INITIAL_V_LETTER, Resources.INITIAL_V_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("k", true,
                    Resources.INITIAL_K_LETTER, Resources.INITIAL_K_LETTER_SOUND,
                    Resources.INITIAL_K_LETTER, Resources.INITIAL_K_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("l", true,
                    Resources.INITIAL_L_LETTER, Resources.INITIAL_L_LETTER_SOUND,
                    Resources.INITIAL_L_LETTER, Resources.INITIAL_L_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("n", true,
                    Resources.INITIAL_N_LETTER, Resources.INITIAL_N_LETTER_SOUND,
                    Resources.INITIAL_N_LETTER, Resources.INITIAL_N_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("h", true,
                    Resources.INITIAL_H_LETTER, Resources.INITIAL_H_LETTER_SOUND,
                    Resources.INITIAL_H_LETTER, Resources.INITIAL_H_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("p", true,
                    Resources.INITIAL_P_LETTER, Resources.INITIAL_P_LETTER_SOUND,
                    Resources.INITIAL_P_LETTER, Resources.INITIAL_P_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("z", true,
                    Resources.INITIAL_Z_LETTER, Resources.INITIAL_Z_LETTER_SOUND,
                    Resources.INITIAL_Z_LETTER, Resources.INITIAL_Z_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("a", true,
                    Resources.UNMARKED_LOWER_A, Resources.SHORT_A_LETTER_SOUND,
                    Resources.UNMARKED_LOWER_A, Resources.SHORT_A_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("e", true,
                    Resources.UNMARKED_LOWER_E, Resources.SHORT_E_LETTER_SOUND,
                    Resources.UNMARKED_LOWER_E, Resources.SHORT_E_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("i", true,
                    Resources.UNMARKED_LOWER_I, Resources.SHORT_I_LETTER_SOUND,
                    Resources.UNMARKED_LOWER_I, Resources.SHORT_I_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("o", true,
                    Resources.UNMARKED_LOWER_O, Resources.SHORT_O_LETTER_SOUND,
                    Resources.UNMARKED_LOWER_O, Resources.SHORT_O_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("u", true,
                    Resources.UNMARKED_LOWER_U, Resources.SHORT_U_LETTER_SOUND,
                    Resources.UNMARKED_LOWER_U, Resources.SHORT_U_LETTER_SOUND
                    ));
            }

            if (GameID == 9)
            {
                this.Text = "Tic-Tac-Gold Letter Recognition Lower to Upper";

                DrawDeck = new List<PhonicsCard>();

                DrawDeck.Add(new PhonicsCard("m", true,
                    Resources.INITIAL_M_LETTER, Resources.INITIAL_M_LETTER_SOUND,
                    Resources.UPPER_M, Resources.INITIAL_M_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("d", true,
                    Resources.INITIAL_D_LETTER, Resources.INITIAL_D_LETTER_SOUND,
                    Resources.UPPER_D, Resources.INITIAL_D_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("f", true,
                    Resources.INITIAL_F_LETTER, Resources.INITIAL_F_LETTER_SOUND,
                    Resources.UPPER_F, Resources.INITIAL_F_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("g", true,
                    Resources.INITIAL_G_LETTER, Resources.INITIAL_G_LETTER_SOUND,
                    Resources.UPPER_G, Resources.INITIAL_G_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("s", true,
                    Resources.INITIAL_S_LETTER, Resources.INITIAL_S_LETTER_SOUND,
                    Resources.UPPER_S, Resources.INITIAL_S_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("r", true,
                    Resources.INITIAL_R_LETTER, Resources.INITIAL_R_LETTER_SOUND,
                    Resources.UPPER_R, Resources.INITIAL_R_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("w", true,
                    Resources.INITIAL_W_LETTER, Resources.INITIAL_W_LETTER_SOUND,
                    Resources.UPPER_W, Resources.INITIAL_W_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("c", true,
                    Resources.INITIAL_C_LETTER, Resources.INITIAL_C_LETTER_SOUND,
                    Resources.UPPER_C, Resources.INITIAL_C_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("j", true,
                    Resources.INITIAL_J_LETTER, Resources.INITIAL_J_LETTER_SOUND,
                    Resources.UPPER_J, Resources.INITIAL_J_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("y", true,
                    Resources.INITIAL_Y_LETTER, Resources.INITIAL_Y_LETTER_SOUND,
                    Resources.UPPER_Y, Resources.INITIAL_Y_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("b", true,
                    Resources.INITIAL_B_LETTER, Resources.INITIAL_B_LETTER_SOUND,
                    Resources.UPPER_B, Resources.INITIAL_B_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("t", true,
                    Resources.INITIAL_T_LETTER, Resources.INITIAL_T_LETTER_SOUND,
                    Resources.UPPER_T, Resources.INITIAL_T_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("v", true,
                    Resources.INITIAL_V_LETTER, Resources.INITIAL_V_LETTER_SOUND,
                    Resources.UPPER_V, Resources.INITIAL_V_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("k", true,
                    Resources.INITIAL_K_LETTER, Resources.INITIAL_K_LETTER_SOUND,
                    Resources.UPPER_K, Resources.INITIAL_K_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("l", true,
                    Resources.INITIAL_L_LETTER, Resources.INITIAL_L_LETTER_SOUND,
                    Resources.UPPER_L, Resources.INITIAL_L_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("n", true,
                    Resources.INITIAL_N_LETTER, Resources.INITIAL_N_LETTER_SOUND,
                    Resources.UPPER_N, Resources.INITIAL_N_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("h", true,
                    Resources.INITIAL_H_LETTER, Resources.INITIAL_H_LETTER_SOUND,
                    Resources.UPPER_H, Resources.INITIAL_H_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("p", true,
                    Resources.INITIAL_P_LETTER, Resources.INITIAL_P_LETTER_SOUND,
                    Resources.UPPER_P, Resources.INITIAL_P_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("z", true,
                    Resources.INITIAL_Z_LETTER, Resources.INITIAL_Z_LETTER_SOUND,
                    Resources.UPPER_Z, Resources.INITIAL_Z_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("a", true,
                    Resources.UNMARKED_LOWER_A, Resources.SHORT_A_LETTER_SOUND,
                    Resources.UPPER_A, Resources.SHORT_A_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("e", true,
                    Resources.UNMARKED_LOWER_E, Resources.SHORT_E_LETTER_SOUND,
                    Resources.UPPER_E, Resources.SHORT_E_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("i", true,
                    Resources.UNMARKED_LOWER_I, Resources.SHORT_I_LETTER_SOUND,
                    Resources.UPPER_I, Resources.SHORT_I_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("o", true,
                    Resources.UNMARKED_LOWER_O, Resources.SHORT_O_LETTER_SOUND,
                    Resources.UPPER_O, Resources.SHORT_O_LETTER_SOUND
                    ));

                DrawDeck.Add(new PhonicsCard("u", true,
                    Resources.UNMARKED_LOWER_U, Resources.SHORT_U_LETTER_SOUND,
                    Resources.UPPER_U, Resources.SHORT_U_LETTER_SOUND
                    ));
            }

        }
    }
}
