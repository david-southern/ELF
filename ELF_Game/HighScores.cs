using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Text;
using System.Windows.Forms;
using System.IO;

namespace ELF
{
    public partial class HighScores : Form
    {
        public HighScores()
        {
            InitializeComponent();
        }

        private void ScoreTabs_TabIndexChanged(object sender, EventArgs e)
        {
            HighScoreTable highScoreTable = new HighScoreTable(ScoreTabs.SelectedTab.Name);
            highScoreTable.Load();
            highScoreTable.Populate(ScoreList);
        }

        private void HighScores_Load(object sender, EventArgs e)
        {
            ScoreTabs_TabIndexChanged(null, null);
        }

        private void ScoreList_SelectedIndexChanged(object sender, EventArgs e)
        {

        }
    }

    public class HighScoreEntry : IComparable
    {
        public string name;
        public int score;

        public HighScoreEntry(string name, int score)
        {
            this.name = name;
            this.score = score;
        }

        public int CompareTo(object obj)
        {
            if (!(obj is HighScoreEntry))
            {
                return -1;
            }

            int score_result = score.CompareTo((obj as HighScoreEntry).score);

            if (score_result != 0)
            {
                // Reverse this to sort descending
                return -score_result;
            }

            return name.CompareTo((obj as HighScoreEntry).name);
        }

        public override string ToString()
        {
            return name + " - " + score.ToString();
        }
    }


    public class HighScoreTable
    {
        private List<HighScoreEntry> scores;
        private bool isLoaded;
        private string path;

        public HighScoreTable(string filePrefix)
        {
            path = Environment.GetFolderPath(Environment.SpecialFolder.ApplicationData) + "\\ELF";

            if (!Directory.Exists(path))
            {
                Directory.CreateDirectory(path);
            }

            path += "\\" + filePrefix + "_scores.bin";

            scores = new List<HighScoreEntry>();
            isLoaded = false;
        }

        public void Load()
        {
            if (File.Exists(path))
            {
                scores = new List<HighScoreEntry>();

                string scores_string = null;

                using (StreamReader textStream = new StreamReader(path, Encoding.UTF8))
                {
                    scores_string = textStream.ReadToEnd();
                    textStream.Close();
                }

                scores_string = Utils.SimpleDecrpyt(scores_string);

                string[] entries = scores_string.Split("~".ToCharArray());

                foreach (string entry in entries)
                {
                    string[] scoreParts = entry.Split('^');

                    if (scoreParts.Length != 2)
                    {
                        continue;
                    }
                    else
                    {
                        scores.Add(new HighScoreEntry(scoreParts[0], Int32.Parse(scoreParts[1])));
                    }
                }

                scores.Sort();
            }
            else
            {
                for (int index = 0; index < 10; index++)
                {
                    scores.Add(new HighScoreEntry("Nobody", 0));
                }
                Save();
            }
            isLoaded = true;
        }

        public void Save()
        {
            if (File.Exists(path))
            {
                File.Delete(path);
            }

            string scores_string = "";

            if (scores.Count > 10)
            {
                scores.RemoveRange(10, scores.Count - 10);
            }

            foreach (HighScoreEntry this_score in scores)
            {
                scores_string += String.Format("{0}^{1}~", this_score.name, this_score.score);
            }

            scores_string = Utils.SimpleEncrypt(scores_string);

            using (StreamWriter textStream = new StreamWriter(path, false, Encoding.UTF8))
            {
                textStream.Write(scores_string);
                textStream.Close();
            }
        }

        public int GetIndexOfScore(int score)
        {
            if (!isLoaded) Load();

            for (int index = 0; index < scores.Count; index++)
            {
                if (score > scores[index].score)
                {
                    return index;
                }
            }

            return -1;
        }

        public void Update(string name, int score)
        {
            if (!isLoaded) Load();

            name = Utils.CensorText(name);

            int index = GetIndexOfScore(score);

            if (index > -1)
            {
                scores.Insert(index, new HighScoreEntry(name, score));
                Save();
            }
        }

        public void Populate(ListView listView)
        {
            if (!isLoaded) Load();

            listView.Items.Clear();

            foreach (HighScoreEntry this_entry in scores)
            {
                listView.Items.Add(new ListViewItem(new string[] { this_entry.name, this_entry.score.ToString() }));
            }
        }
    }
}
