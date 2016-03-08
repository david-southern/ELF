using System;
using System.Collections.Generic;
using System.Text;
using System.Media;

namespace ELF
{
    class Dice
    {
        public DiceFace[] allFacesPossible = new DiceFace[6];

        public int Value { get; set; }
        public bool Hold { get; set; }
        public int Position { get; set; }
        public SoundPlayer soundFile { get; set; }
        public DiceFace currentFace { get; set; }

        public static List<string> possible_values;

        public Dice(DiceFace f1, DiceFace f2, DiceFace f3, DiceFace f4, DiceFace f5, DiceFace f6)
        {
            Hold = false;

            currentFace = f1;

            allFacesPossible[0] = f1;
            allFacesPossible[1] = f2;
            allFacesPossible[2] = f3;
            allFacesPossible[3] = f4;
            allFacesPossible[4] = f5;
            allFacesPossible[5] = f6;

            if (!possible_values.Contains(f1.Value))
            {
                possible_values.Add(f1.Value);
            }

            if (!possible_values.Contains(f2.Value))
            {
                possible_values.Add(f2.Value);
            }

            if (!possible_values.Contains(f3.Value))
            {
                possible_values.Add(f3.Value);
            }

            if (!possible_values.Contains(f4.Value))
            {
                possible_values.Add(f4.Value);
            }

            if (!possible_values.Contains(f5.Value))
            {
                possible_values.Add(f5.Value);
            }

            if (!possible_values.Contains(f6.Value))
            {
                possible_values.Add(f6.Value);
            }

            possible_values.Sort();
        }

        public void Roll(int randomNumber)
        {
            Value = possible_values.IndexOf(allFacesPossible[randomNumber].Value);
            currentFace = allFacesPossible[randomNumber];
            soundFile = allFacesPossible[randomNumber].SoundFile;
        }

        internal void PlaySound()
        {
            if (soundFile != null)
            {
                soundFile.Play();
            }
        }
    }
}
