using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using ELF_Resources.Properties;

namespace ELF
{
    public partial class MainMenu : Form
    {
        public MainMenu()
        {
            // Get license state cached so the delay doesn't affect starting games
            ProjectUtils.Utils.IsValidLicense();

            InitializeComponent();
        }

        private void ParentNotes_Click(object sender, EventArgs e)
        {
            NotesDialog notes = new NotesDialog();

            notes.Text = "Notes for Parents & Teachers";
            notes.NotesText.Rtf = Resources.TEACHER_INSTRUCTIONS;
            notes.ShowDialog();
        }

        private void HighScores_Click(object sender, EventArgs e)
        {
            HighScores score_form = new HighScores();
            score_form.ShowDialog();
        }

        private void GameSelectionClick(object sender, EventArgs e)
        {
            int GameID = Int32.Parse((sender as PictureBox).Tag.ToString());

            MainMenuGameOptions gameForm = new MainMenuGameOptions((GameClassEnum)GameID);
            gameForm.ShowDialog();
        }

        private void AboutELF_Click(object sender, EventArgs e)
        {
            NotesDialog notes = new NotesDialog();

            notes.Text = "About ELF";
            notes.NotesText.Text =
                "ELF Phonics Games Version " + ProjectUtils.Utils.GetProgramVersionString() + "\r\n\r\n"
                + "ELF Phonics Games are ©2009 by English Language Fundamentals, LLC\r\n\r\n"
                + "For more information, please visit http://www.e-l-fun.com.";
            notes.ShowDialog();
        }

        private void Rules_Click(object sender, EventArgs e)
        {
            NotesDialog notes = new NotesDialog();

            notes.Text = "Game Rules and Instructions";
            notes.NotesText.Rtf = Resources.GAME_INSTRUCTIONS;
            notes.ShowDialog();
        }

        private void linkLabel1_LinkClicked(object sender, LinkLabelLinkClickedEventArgs e)
        {
            string url = (string)linkLabel1.Text;
            System.Diagnostics.Process.Start(url);
        }
    }
}
