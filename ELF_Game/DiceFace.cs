using System;
using System.Collections.Generic;
using System.Text;
using System.Windows.Forms;
using System.Media;
using System.Drawing;
using System.IO;

namespace ELF
{
    class DiceFace
    {
        public string Value { get; set; }
        public Bitmap Pic { get; set; }
        public bool BucketHasSound { get; set; }
        public SoundPlayer SoundFile { get; set; }

        /// <summary>
        /// Creates a single face value on our playable dice.
        /// </summary>
        /// <param name="v">Value to be assigned.</param>
        /// <param name="p">Path to the picture.</param>
        /// <param name="soundBool">Does a sound exist for this face</param>
        /// <param name="sound">The sound file itself.</param>
        public DiceFace(string base_letter, Image image, Stream soundStream)
        {
            Value = base_letter;
            Pic = new Bitmap(image);
            SoundFile = new SoundPlayer(soundStream);
        }

    }
}
