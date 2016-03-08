using System;
using System.Collections.Generic;
using System.Text;

namespace ELF
{
    class CalculateScores
    {
        public enum ScoreCategories
        {
            Letters = 0,
            TwoPair,
            ThreeKind, 
            FullHouse,
            FourKind,
            FourDifferent,
            FiveDifferent,
            VowelHowl
        }

        public static readonly int[] SCORE_VALUES = new int[]
        { 10, 20, 30, 40, 50, 50, 60, 70 };

        public CalculateScores()
        {
        }

        private bool CheckContainsOnly(int which_sound, int[] sound_counts)
        {
            return CheckContainsOnly(new int[] { which_sound }, sound_counts);
        }

        private bool CheckContainsOnly(int[] valid_sounds, int[] sound_counts)
        {
            List<int> valid_list = new List<int>(valid_sounds);

            for (int sound_index = 0; sound_index < sound_counts.Length; sound_index++)
            {
                if (!valid_list.Contains(sound_index) && sound_counts[sound_index] > 0)
                {
                    return false;
                }
            }

            return true;
        }

        public int firstValue(int[] sound_counts)
        {
            if (!CheckContainsOnly(0, sound_counts))
            {
                return 0;
            }
            return sound_counts[0] * SCORE_VALUES[(int)ScoreCategories.Letters];
        }

        public int secondValue(int[] sound_counts)
        {
            if (!CheckContainsOnly(1, sound_counts))
            {
                return 0;
            }
            return sound_counts[1] * SCORE_VALUES[(int)ScoreCategories.Letters];
        }

        public int thirdValue(int[] sound_counts)
        {
            if (!CheckContainsOnly(2, sound_counts))
            {
                return 0;
            }
            return sound_counts[2] * SCORE_VALUES[(int)ScoreCategories.Letters];
        }

        public int fourthValue(int[] sound_counts)
        {
            if (!CheckContainsOnly(3, sound_counts))
            {
                return 0;
            }
            return sound_counts[3] * SCORE_VALUES[(int)ScoreCategories.Letters];
        }

        public int fifthValue(int[] sound_counts)
        {
            if (!CheckContainsOnly(4, sound_counts))
            {
                return 0;
            }
            return sound_counts[4] * SCORE_VALUES[(int)ScoreCategories.Letters];
        }

        public int fullHouse(int[] sound_counts)
        {
            int three_sound = -1;
            int two_sound = -1;

            for (int i = 0; i < sound_counts.Length; i++)
            {
                if (sound_counts[i] == 2)
                {
                    two_sound = i;
                }
                if (sound_counts[i] == 3)
                {
                    three_sound = i;
                }

            }

            if (three_sound == -1 || two_sound == -1)
            {
                return 0;
            }

            return SCORE_VALUES[(int)ScoreCategories.FullHouse];
        }

        public int twoPair(int[] sound_counts)
        {
            int first_pair = -1;
            int second_pair = -1;

            for (int i = 0; i < sound_counts.Length; i++)
            {
                // Four of a kind can be scored as two pair if you want to
                if (sound_counts[i] == 4)
                {
                    if (CheckContainsOnly(i, sound_counts))
                    {
                        return SCORE_VALUES[(int)ScoreCategories.TwoPair];
                    }
                    else
                    {
                        return 0;
                    }
                }

                if (sound_counts[i] == 2)
                {
                    if (first_pair == -1)
                    {
                        first_pair = i;
                    }
                    else
                    {
                        second_pair = i;
                    }
                }

            }

            if (first_pair != -1 && second_pair != -1 && CheckContainsOnly(new int[] { first_pair, second_pair }, sound_counts))
            {
                return SCORE_VALUES[(int)ScoreCategories.TwoPair];
            }

            return 0;
        }

        public int fourDifferent(int[] sound_counts)
        {
            List<int> single_sounds = new List<int>();

            for (int i = 0; i < sound_counts.Length; i++)
            {
                if (sound_counts[i] == 1)
                {
                    single_sounds.Add(i);
                }
            }

            if (single_sounds.Count == 4)
            {
                return SCORE_VALUES[(int)ScoreCategories.FourDifferent];
            }

            return 0;
        }

        public int fiveDifferent(int[] sound_counts)
        {
            List<int> single_sounds = new List<int>();

            for (int i = 0; i < sound_counts.Length; i++)
            {
                if (sound_counts[i] == 1)
                {
                    single_sounds.Add(i);
                }
            }

            if (single_sounds.Count == 5)
            {
                return SCORE_VALUES[(int)ScoreCategories.FiveDifferent];
            }

            return 0;
        }

        public int vowelHowl(int[] sound_counts)
        {
            for (int i = 0; i < sound_counts.Length; i++)
            {
                if (sound_counts[i] == 5)
                {
                    return SCORE_VALUES[(int)ScoreCategories.VowelHowl];
                }
            }

            return 0;
        }

        public int threeOfKind(int[] sound_counts)
        {
            int three_sounds = -1;

            for (int i = 0; i < sound_counts.Length; i++)
            {
                if (sound_counts[i] == 3)
                {
                    three_sounds = i;
                }
            }

            if (three_sounds != -1 && CheckContainsOnly(three_sounds, sound_counts))
            {
                return SCORE_VALUES[(int)ScoreCategories.ThreeKind];
            }

            return 0;
        }

        public int fourOfKind(int[] sound_counts)
        {
            int four_sounds = -1;

            for (int i = 0; i < sound_counts.Length; i++)
            {
                if (sound_counts[i] == 4)
                {
                    four_sounds = i;
                }
            }

            if (four_sounds != -1 && CheckContainsOnly(four_sounds, sound_counts))
            {
                return SCORE_VALUES[(int)ScoreCategories.FourKind];
            }

            return 0;
        }
    }
}
