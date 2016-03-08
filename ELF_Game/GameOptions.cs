using System;
using System.Collections.Generic;
using System.Text;

namespace ELF
{
    public static class GameOptions
    {
        /// <summary>
        /// In VH, whenever dice are selected, show the player which scoring buttons are valid.
        /// (i.e. would produce a non-zero score)
        /// </summary>
        public static bool UsePickPointers = false;

        /// <summary>
        /// In VowelHowl, show the player what the currently selected dice would score whenever
        /// a score button is hovered.  Turn this off to require the player to figure out whether
        /// the currently selected dice will score or not.
        /// </summary>
        public static bool ShowPossibleScores = true;

        /// <summary>
        /// If true then when the player incorrectly clicks on a TTG square, we fill that
        /// square with a red X so that the player cannot click on it again later.  This
        /// means that if the correct card comes up later then it cannot be correctly
        /// identified.
        /// </summary>
        public static bool TTG_CancelSquareOnMisClick = false;

        /// <summary>
        /// If true, the player is awarded a Genius Star as long as they made no errors
        /// on the current card.  In this case the value of the Genius Star bonus is equal
        /// to the number of words correctly found times 5.  If false then the player must
        /// find all words for the current card in order to get a Genius Star, in which case
        /// the value of the star is 50 points, but any words that were not found carry a five
        /// point penalty.
        /// </summary>
        public static bool GWB_RequireAllWordsFound = true;
    }
}
