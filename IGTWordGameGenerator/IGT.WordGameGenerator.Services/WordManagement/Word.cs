using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Services.WordManagement
{
    /// <summary>
    /// Represents a word that collects letters
    /// </summary>
    public class Word
    {
        private string stringRepresentation;
        private char[] letters;
        private int wordLength;
        private int wordComplexity; // Number of unique letters in the Word

        /// <summary>
        /// Constructs a char[] of the neededLength and fills in the letters with the string, and fills the rest of array with ' '.
        /// </summary>
        /// <param name="word">The word that is represented by the new Word object</param>
        /// <param name="neededLength">The length that the char[] needs to be to fit with the other words</param>
        public Word(string word, int neededLength)
        {
            stringRepresentation = word;
            wordComplexity = 0;
            wordLength = word.Length;
            letters = new char[neededLength];
            for (int i = 0; i < neededLength; i++)
            {
                if (i < word.Length)
                {
                    if (!letters.Contains(word.ToCharArray()[i]))
                    {
                        wordComplexity++;
                    }
                    letters[i] = word.ToCharArray()[i];
                }
                else
                {
                    letters[i] = ' ';
                }
            }
        }

        /// <summary>
        /// Retrieve the character at the given index. Returns null if the index goes beyond the length of the actual word
        /// </summary>
        /// <param name="index">Index of desired character</param>
        /// <returns>Returns character if index is within wordLength, else returns null</returns>
        public char? GetLetterAt(int index)
        {
            return index < wordLength ? new char?(letters[index]) : null;
        }

        /// <summary>
        /// Retrieves the length of the word and any ' ' chars
        /// </summary>
        /// <returns>The length of the word and any ' ' chars</returns>
        public int GetTotalLength()
        {
            return letters.Length;
        }

        /// <summary>
        /// Retrieve the number of unique letters in the Word
        /// </summary>
        /// <returns>Number of unique letters in letters array</returns>
        public int GetComplexity()
        {
            return wordComplexity;
        }

        /// <summary>
        /// Determines if the Word contains the provided string
        /// </summary>
        /// <param name="word">The word that is being checked against</param>
        /// <returns>True if Word contains the provided string</returns>
        public bool Contains(string word)
        {
            return stringRepresentation.Contains(word);
        }

        public bool SameLettersAs(Word word)
        {
            string otherWord = word.ToString();
            string thisWord = ToString();

            if (word.GetComplexity() == wordComplexity)
            {
                foreach (char currentLetter in thisWord)
                {
                    if (otherWord.Contains(currentLetter))
                    {
                        otherWord.Remove(otherWord.IndexOf(currentLetter), 1);
                    }
                }
                if(string.IsNullOrWhiteSpace(otherWord))
                {
                    return true;
                }
            }

            return false;
        }

        /// <summary>
        /// Compares the Word object with provided object
        /// </summary>
        /// <param name="other">The object to compare against</param>
        /// <returns>Returns true if the hash of the source word matches the hash of the given word</returns>
        public override bool Equals(Object other)
        {
            if (other is Word)
            {
                return GetHashCode() == (other as Word).GetHashCode();
            }
            return false;
        }

        /// <summary>
        /// Retrieves the hashcode of the Word
        /// </summary>
        /// <returns>Returns sum of characters' hashes in letters array</returns>
        public override int GetHashCode()
        {
            int ret = 0;
            foreach (char c in letters)
            {
                ret += c.GetHashCode();
            }
            return ret;
        }

        /// <summary>
        /// String representation of the Word
        /// </summary>
        /// <returns>The word as a string</returns>
        public override string ToString()
        {
            return stringRepresentation;
        }
    }
}
