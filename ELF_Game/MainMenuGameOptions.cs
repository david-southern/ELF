using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using IntelliLock.Licensing;

namespace ELF
{
    public enum GameClassEnum
    {
        VH = 0,
        SP = 1,
        TTG = 2,
        GWB = 3
    }

    public partial class MainMenuGameOptions : Form
    {
        private List<Panel> GamePanels = new List<Panel>();

        public MainMenuGameOptions(GameClassEnum whichGame)
        {
            InitializeComponent();

            GamePanels.Add(VH_Panel);
            GamePanels.Add(SP_Panel);
            GamePanels.Add(TTG_Panel);
            GamePanels.Add(GWB_Panel);

            foreach (Panel hidePanel in GamePanels)
            {
                hidePanel.Visible = false;
            }

            Panel whichPanel = GamePanels[(int)whichGame];

            whichPanel.Visible = true;
            whichPanel.BringToFront();
            whichPanel.Dock = DockStyle.Fill;
        }

        private void StartPhonicsRoll(object sender, EventArgs e)
        {
            int GameID = Int32.Parse((sender as PictureBox).Tag.ToString());

            if (GameID != 0 && GameID != 3 && !ProjectUtils.Utils.IsValidLicense())
            {
                LicenseNeeded lic_form = new LicenseNeeded();
                lic_form.ShowDialog();
                return;
            }

            PhonicsRollForm pr = new PhonicsRollForm(GameID);
            pr.ShowDialog();
            this.Close();
        }

        private void StartGWB(object sender, EventArgs e)
        {
            int GameID = Int32.Parse((sender as PictureBox).Tag.ToString());

            if (GameID != 0 && GameID != 3 && !ProjectUtils.Utils.IsValidLicense())
            {
                LicenseNeeded lic_form = new LicenseNeeded();
                lic_form.ShowDialog();
                return;
            }

            WordBuilderForm wb = new WordBuilderForm(GameID);
            wb.ShowDialog();
            this.Close();
        }

        private void StartRandomTTG(List<int> gameIDs)
        {
            Random myRandom = new Random();

            int whichGame = myRandom.Next(gameIDs.Count);

            TicTacGoldForm ttg = new TicTacGoldForm(gameIDs[whichGame]);

            ttg.PlayBlackout = TTG_GameType_Blackout.Checked;

            ttg.ShowDialog();
            this.Close();
        }

        private void StartTTGAuditoryConsonants(object sender, EventArgs e)
        {
            List<int> GameIDs = new List<int>();
            GameIDs.Add(2);

            if (ProjectUtils.Utils.IsValidLicense())
            {
                GameIDs.Add(3);
            }

            StartRandomTTG(GameIDs);
        }

        private void StartTTGAuditoryVowels(object sender, EventArgs e)
        {
            if (!ProjectUtils.Utils.IsValidLicense())
            {
                LicenseNeeded lic_form = new LicenseNeeded();
                lic_form.ShowDialog();
                return;
            }

            List<int> GameIDs = new List<int>();
            GameIDs.Add(6);
            GameIDs.Add(7);
            StartRandomTTG(GameIDs);
        }

        private void StartTTGPhonicsConsonants(object sender, EventArgs e)
        {
            if (!ProjectUtils.Utils.IsValidLicense())
            {
                LicenseNeeded lic_form = new LicenseNeeded();
                lic_form.ShowDialog();
                return;
            }

            List<int> GameIDs = new List<int>();
            GameIDs.Add(0);
            GameIDs.Add(1);
            StartRandomTTG(GameIDs);
        }

        private void StartTTGPhonicsVowels(object sender, EventArgs e)
        {
            if (!ProjectUtils.Utils.IsValidLicense())
            {
                LicenseNeeded lic_form = new LicenseNeeded();
                lic_form.ShowDialog();
                return;
            }

            List<int> GameIDs = new List<int>();
            GameIDs.Add(4);
            GameIDs.Add(5);
            StartRandomTTG(GameIDs);
        }

        private void StartTTGLetterIDLower(object sender, EventArgs e)
        {
            if (!ProjectUtils.Utils.IsValidLicense())
            {
                LicenseNeeded lic_form = new LicenseNeeded();
                lic_form.ShowDialog();
                return;
            }

            List<int> GameIDs = new List<int>();
            GameIDs.Add(8);
            StartRandomTTG(GameIDs);
        }

        private void StartTTGLetterIDUpper(object sender, EventArgs e)
        {
            if (!ProjectUtils.Utils.IsValidLicense())
            {
                LicenseNeeded lic_form = new LicenseNeeded();
                lic_form.ShowDialog();
                return;
            }

            List<int> GameIDs = new List<int>();
            GameIDs.Add(9);
            StartRandomTTG(GameIDs);
        }
    }
}
