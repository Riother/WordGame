using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Services.WordManagement
{
    /// <summary>
    /// Class that contains functions to determine if banned words are contained
    /// </summary>
    public class BannedWordChecker
    {
        List<string> bannedWords;

        /// <summary>
        /// Constructs BannedWordChecker with a list of banned words to check against
        /// </summary>
        /// <param name="bannedWords">The list of banned words</param>
        public BannedWordChecker(List<string> bannedWords)
        {
            this.bannedWords = bannedWords;
        }

        /// <summary>
        /// Determines if any words contains a banned word horizontally
        /// </summary>
        /// <param name="words">The words that need to be verified</param>
        /// <returns>Index of word that needs to be swapped, -1 when no banned words</returns>
        public int FindHorizontalBannedWord(List<Word> words)
        {
            foreach (Word current in words)
            {
                foreach (string banned in bannedWords)
                {
                    if (current.Contains(banned))
                    {
                        return words.IndexOf(current);
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Determines if the words have a banned word vertically
        /// </summary>
        /// <param name="words">The words that need to be verified</param>
        /// <returns>Index of word that needs to be swapped, -1 when no banned words</returns>
        public int FindVerticalBannedWord(List<Word> words)
        {
            int wordLength = words[0].GetTotalLength();
            List<char[]> vertWord = new List<char[]>();

            // travels horizontally across the word characters
            for (int i = 0; i < wordLength; i++)
            {
                vertWord.Add(new char[words.Count]);
                for (int j = 0; j < words.Count; j++)
                {
                    char? currentLetter = words[j].GetLetterAt(i);
                    vertWord[i][j] = currentLetter != null ? currentLetter.Value : ' ';
                }

                string topDownString = "";
                string bottomUpString = "";
                for (int charIndex = 0; charIndex < vertWord[i].Length; charIndex++)
                {
                    topDownString += vertWord[i][charIndex];
                    bottomUpString += vertWord[i][(vertWord[i].Length - charIndex) - 1];
                }
                foreach (string bannedWord in bannedWords)
                {
                    if (topDownString.Contains(bannedWord))
                    {
                        return topDownString.IndexOf(bannedWord);
                    }
                    else if (bottomUpString.Contains(bannedWord))
                    {
                        return (words.Count - bottomUpString.IndexOf(bannedWord)) - 1;
                    }
                }
            }
            return -1;
        }

        /// <summary>
        /// Determines if the words have a banned word diagonally
        /// </summary>
        /// <param name="words">The words that need to be verified</param>
        /// <returns>Index of word that needs to be swapped, -1 when no banned words</returns>
        public int FindDiagonalBannedWord(List<Word> words)
        {
            // check the words
            for (int y = 0; y < words.Count; y++)
            {
                for (int x = 0; x < words[0].GetTotalLength(); x++)
                {
                    int currentX = x, currentY = y;

                    // up-left
                    string upleftWord = "";
                    while (currentX > 0 && currentY > 0)
                    {
                        upleftWord += words[currentY].GetLetterAt(currentX);
                        // shift locations
                        currentX--;
                        currentY--;
                    }
                    // resets back to current space
                    currentX = x; 
                    currentY = y; 

                    // up-right
                    string uprightWord = "";
                    while (currentX < words[0].GetTotalLength() && currentY > 0)
                    {
                        uprightWord += words[currentY].GetLetterAt(currentX);
                        // shift locations
                        currentX++;
                        currentY--;
                    }
                    // resets back to current space
                    currentX = x;
                    currentY = y; 

                    // down-left
                    string downleftWord = "";
                    while (currentX > 0 && currentY < words.Count)
                    {
                        downleftWord += words[currentY].GetLetterAt(currentX);
                        // shift location
                        currentX--;
                        currentY++;
                    }
                    // resets back to current space
                    currentX = x;
                    currentY = y; 

                    // down-right
                    string downrightWord = "";
                    while (currentX < words[0].GetTotalLength() && currentY < words.Count)
                    {
                        downrightWord += words[currentY].GetLetterAt(currentX);
                        //shift location
                        currentX++;
                        currentY++;
                    }

                    // verify no banned words
                    foreach(string bannedWord in bannedWords)
                    {
                        if(upleftWord.Contains(bannedWord) || 
                            uprightWord.Contains(bannedWord) ||
                            downleftWord.Contains(bannedWord) ||
                            downrightWord.Contains(bannedWord))
                        {
                            return y; // index of word that needs to be replaced
                        }
                    }
                }
            }
            return -1;
        }

        private int HashLetterLocation(int x, int y)
        {
            int hash = 17;
            hash = ((hash + x) << 5) - (hash + x);
            hash = ((hash + y) << 5) - (hash + y);
            return hash;
        }
    }
}
