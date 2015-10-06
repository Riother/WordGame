using IGT.WordGameGenerator.Services.DebugTools;
using IGT.WordGameGenerator.Services.ListTools;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Services.WordManagement
{
    /// <summary>
    /// Manages winning words, removing banned words from board state
    /// </summary>
    public class WordManager
    {
        private WordListManager.WordListManager listManager;
        private BannedWordChecker checker;
        private Dictionary<int, Word> words;
        private int wordMaxLength;

        /// <summary>
        /// Takes in the winning words, does not verify absence of banned words
        /// </summary>
        /// <param name="numWords">The number of words that will be in the set</param>
        /// <param name="complexities">List of unique letter count in order of division</param>
        /// <param name="validWordsFilePath">Path to valid words text file</param>
        /// <param name="bannedWordsFilePath">Path to banned words text file</param>
        public WordManager(int numWords, List<int> complexities, string validWordsFilePath, string bannedWordsFilePath)
        {
            LogWriter.GetInstance().WriteLog("Creating list manager");
            listManager = new WordListManager.WordListManager(validWordsFilePath, bannedWordsFilePath, complexities.Min(), complexities.Max());
            LogWriter.GetInstance().WriteLog("Finished creating list manager");
            checker = new BannedWordChecker(listManager.GetBannedWords());

            LogWriter.GetInstance().WriteLog("Selecting winning words");
            List<string> winningWords = new List<string>();
            for (int i = 0; i < numWords; i++)
            {
                Word toAdd = new Word("", 100);
                string newWord = "";
                do
                {
                    newWord = listManager.GetRandomValidWord(complexities[i]);
                    toAdd = new Word(newWord, 100);
                }
                while(winningWords.Contains(newWord) || toAdd.GetComplexity() != complexities[i] || HasMultipleWin(winningWords, toAdd));
                winningWords.Add(newWord);
            }
            LogWriter.GetInstance().WriteLog("Finished choosing winning words");

            wordMaxLength = 0;
            foreach (string current in winningWords)
            {
                if (current.Length > wordMaxLength)
                {
                    wordMaxLength = current.Length;
                }
            }

            // add words to list, with ' ' filling for the empty space
            words = new Dictionary<int, Word>();
            for (int i = 0; i < winningWords.Count; i++)
            {
                words.Add(complexities[i], new Word(winningWords[i], wordMaxLength));
            }
        }

        /// <summary>
        /// Returns max length of any word that is placed in the array
        /// </summary>
        /// <returns></returns>
        public int GetLongestLength()
        {
            return wordMaxLength;
        }

        /// <summary>
        /// Retrieves Word at the given index
        /// </summary>
        /// <param name="index">Index of desired Word</param>
        /// <returns>The Word at the given index</returns>
        public Word GetWordOfComplexity(int index)
        {
            return words[index];
        }

        /// <summary>
        /// Returns the list of words in the game set
        /// </summary>
        /// <returns>List of words that are valid, and contain no banned words</returns>
        public List<int> GetAllComplexities()
        {
            return words.Keys.ToList<int>();
        }

        /// <summary>
        /// Get list of words in the current set
        /// </summary>
        /// <returns></returns>
        public List<Word> GetWordSet()
        {
            return new List<Word>(words.Values);
        }

        /// <summary>
        /// Goes through all of the words until there are no longer any banned words on the board
        /// </summary>
        public void HandleBannedWords()
        {
            LogWriter.GetInstance().WriteLog("Handling banned words");
            int horizontalBan, verticalBan, diagonalBan;
            do
            {
                horizontalBan = checker.FindHorizontalBannedWord(words.Values.ToList<Word>());
                verticalBan = checker.FindVerticalBannedWord(words.Values.ToList<Word>());
                diagonalBan = checker.FindDiagonalBannedWord(words.Values.ToList<Word>());

                if (horizontalBan != -1)
                {
                    ReplaceWordOfComplexity(words.Values.ToList<Word>()[horizontalBan].GetComplexity());
                }
                else if (verticalBan != -1)
                {
                    ReplaceWordOfComplexity(words.Values.ToList<Word>()[verticalBan].GetComplexity());
                }
                else if (diagonalBan != -1)
                {
                    ReplaceWordOfComplexity(words.Values.ToList<Word>()[diagonalBan].GetComplexity());
                }
            }
            while (horizontalBan != -1 || verticalBan != -1 || diagonalBan != -1);
            LogWriter.GetInstance().WriteLog("Finishing handling banned words");
        }

        /// <summary>
        /// Returns a list of characters that enforce a near-win experience of a random word
        /// </summary>
        /// <param name="numCallLetters">The number of letters that need to drawn before the strike is called</param>
        /// <returns>The list of chars that won't allow a player to win a game</returns>
        public List<char> GetLossSet(int numCallLetters, int numberCallLettersPerRow)
        {
            LogWriter.GetInstance().WriteLog("Determining loss character set");

            int seed = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % Int32.MaxValue);
            Random rand = new Random(seed);
            int uniqueLetters = words.Keys.ElementAt(rand.Next(0, words.Keys.Count));
            Word nearWinWord = words[uniqueLetters];

            LogWriter.GetInstance().WriteLog("Determining initial character set");
            // Gets unique characters from near-win word
            List<char> remaining = new List<char>(nearWinWord.ToString());
            for(int i = 0; i < remaining.Count; i++)
            {
                int index = i + 1;
                for (int j = index; j < remaining.Count; j++)
                {
                    if (remaining[i] == remaining[j])
                    {
                        remaining.RemoveAt(j);
                    }
                }
            }
            LogWriter.GetInstance().WriteLog("Finished determining initial character set");

            int indexToRemove = rand.Next(0, remaining.Count);
            char removed = remaining[indexToRemove];
            remaining.RemoveAt(indexToRemove); // prevents winning

            LogWriter.GetInstance().WriteLog("Fill in remaining call letter set");
            // If the word doesn't have enough letters for the number of call letters designated, put in filler characters
            while (remaining.Count < numCallLetters)
            {
                char toAdd = GetLetter();
                if (!remaining.Contains(toAdd) && !EnablesWin(toAdd, nearWinWord, remaining) && toAdd != removed)
                {
                    remaining.Add(toAdd);
                }
            }
            LogWriter.GetInstance().WriteLog("Finished filling in remaining call letter set");
            remaining.Shuffle<char>();
            RemoveCallHistoryBannedWords(remaining, numberCallLettersPerRow);

            return remaining;
        }

        /// <summary>
        /// Returns a list of characters that have the player win with the designated prize level
        /// </summary>
        /// <param name="uniqueLetterCount">The complexity of the winning prize level</param>
        /// <param name="numCallLetters">The number of letters that need to be drawn before the winning letter is called</param>
        /// <returns>The list of chars that allow the player to win a game</returns>
        public List<char> GetWinSet(int uniqueLetterCount, int numCallLetters, int numberCallLettersPerRow)
        {
            Word winningWord = words[uniqueLetterCount];

            LogWriter.GetInstance().WriteLog("Determining win character set");
            LogWriter.GetInstance().WriteLog("Creating initial character set");

            // Gets unique letters of winning word
            List<char> letterSet = new List<char>(winningWord.ToString());
            for (int i = 0; i < letterSet.Count; i++)
            {
                int index = i + 1;
                for(int j = index; j < letterSet.Count; j++)
                {
                    if(letterSet[i] == letterSet[j])
                    {
                        letterSet.RemoveAt(j);
                    }
                }
            }
            LogWriter.GetInstance().WriteLog("Finished creating initial character set");

            // Shuffles and removes valid letter from letter set, to verify that the last call letter wins the word
            letterSet.Shuffle<char>();
            char finalLetter = letterSet.Last<char>();
            letterSet.RemoveAt(letterSet.Count - 1);

            LogWriter.GetInstance().WriteLog("Filling in remaining character spots");
            // verifies that letter set is equal in length to the number of call letters
            while(letterSet.Count < numCallLetters - 1)
            {
                char toAdd = GetLetter();
                if (!letterSet.Contains(toAdd) && !EnablesWin(toAdd, winningWord, letterSet) && toAdd != finalLetter)
                {
                    letterSet.Add(toAdd);
                }
            }
            LogWriter.GetInstance().WriteLog("Finished filling in remaining character spots");

            letterSet.Shuffle<char>();
            LogWriter.GetInstance().WriteLog("Checking call letter history for banned words");
            RemoveCallHistoryBannedWords(letterSet, numberCallLettersPerRow);
            LogWriter.GetInstance().WriteLog("Finished checking call letter history for banned words");
            letterSet.Add(finalLetter);

            return letterSet;
        }

        private void ReplaceWordOfComplexity(int complexity)
        {
            words[complexity] = new Word(listManager.GetRandomValidWord(complexity, words[complexity].ToString()), wordMaxLength);
        }

        private bool HasMultipleWin(List<string> currentWords, Word newWord)
        {
            foreach (string curWord in currentWords)
            {
                if (newWord.SameLettersAs(new Word(curWord,newWord.GetTotalLength())))
                {
                    return true;
                }
            }
            return false;
        }

        private bool EnablesWin(char character, Word winningWord, List<char> remaining)
        {
            foreach (Word currentWord in words.Values)
            {
                if(!currentWord.Equals(winningWord))
                {
                    string wordRep = currentWord.ToString();
                    for (int i = 0; i < wordRep.Length; i++)
                    {
                        int index = i + 1;
                        for (int j = index; j < wordRep.Length; j++)
                        {
                            if (wordRep[i] == wordRep[j])
                            {
                                wordRep = wordRep.Remove(j, 1);
                            }
                        }
                    }

                    while (wordRep.Contains(character))
                    {
                        wordRep = wordRep.Remove(wordRep.IndexOf(character), 1);
                    }
                    foreach (char toRemove in remaining)
                    {
                        while (wordRep.Contains(toRemove))
                        {
                            wordRep = wordRep.Remove(wordRep.IndexOf(toRemove), 1);
                        }
                    }

                    if (wordRep.Length == 0)
                    {
                        return true;
                    }
                }
            }
            return false;
        }

        private char GetLetter()
        {
            int seed = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % Int32.MaxValue);
            Random rand = new Random(seed);
            int num = rand.Next(0, 26); // Zero to 25
            char let = (char)('A' + num);
            return let;
        }

        

        public void RemoveCallHistoryBannedWords(List<char> characters, int numberCallLettersPerRow)
        {
            int numRows = characters.Count / numberCallLettersPerRow;
            int numCharactersInLastRow = characters.Count % numberCallLettersPerRow;
            List<string> callLetterRows = new List<string>();
            
            for(int i = 0; i < numRows; i++)
            {
                string rowString = "";
                for(int j = 0; j < numberCallLettersPerRow; j++)
                {
                    rowString += characters[j + (i * numberCallLettersPerRow)];
                }
                callLetterRows.Add(rowString);
            }

            if (numCharactersInLastRow > 0)
            {
                string remaining = "";
                for (int i = numRows * numberCallLettersPerRow; i < characters.Count; i++)
                {
                    remaining += characters[i];
                }
                callLetterRows.Add(remaining);
            }

            List<Word> characterWords = new List<Word>();
            foreach(string characterString in callLetterRows)
            {
                characterWords.Add(new Word(characterString, numberCallLettersPerRow));
            }

            int horizontalBan, verticalBan, diagonalBan;
            do
            {
                horizontalBan = checker.FindHorizontalBannedWord(characterWords);
                verticalBan = checker.FindVerticalBannedWord(characterWords);
                diagonalBan = checker.FindDiagonalBannedWord(characterWords);

                int seed = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % Int32.MaxValue);
                Random rand = new Random(seed);

                int index = 0;
                int swapPlace = index + 1;
                if(horizontalBan != -1)
                {
                    // chooses random character in row to swap with its next door neighbor
                    index = horizontalBan * numberCallLettersPerRow;
                    index = rand.Next(index, (index + numberCallLettersPerRow)-1);

                    // if chosen character is last charater in history, swap with first character in history
                    swapPlace = index + 1 < characters.Count ? index + 1 : 0;

                    // switch the two characters
                    char temp = characters[index];
                    characters[index] = characters[swapPlace];
                    characters[swapPlace] = temp;
                }
                else if(verticalBan != -1)
                {
                    // chooses random character in row to swap with its next door neighbor
                    index = verticalBan * numberCallLettersPerRow;
                    index = rand.Next(index, index + numberCallLettersPerRow);

                    // if chosen character is last charater in history, swap with first character in history
                    swapPlace = index + 1 < characters.Count ? index + 1 : 0;

                    // switch the two characters
                    char temp = characters[index];
                    characters[index] = characters[swapPlace];
                    characters[swapPlace] = temp;
                }
                else if(diagonalBan != -1)
                {
                    // chooses random character in row to swap with its next door neighbor
                    index = diagonalBan * numberCallLettersPerRow;
                    index = rand.Next(index, index + numberCallLettersPerRow);

                    // if chosen character is last charater in history, swap with first character in history
                    swapPlace = index + 1 < characters.Count ? index + 1 : 0;

                    // switch the two characters
                    char temp = characters[index];
                    characters[index] = characters[swapPlace];
                    characters[swapPlace] = temp;
                }

                // update row words
                List<Word> tempWords = new List<Word>();
                for (int i = 0; i < numRows; i++)
                {
                    string rowString = "";
                    for (int j = 0; j < numberCallLettersPerRow; j++)
                    {
                        rowString += characters[j + (i * numberCallLettersPerRow)];
                    }
                    tempWords.Add(new Word(rowString, characterWords[i].GetTotalLength()));
                }

                if (numCharactersInLastRow > 0)
                {
                    string remaining = "";
                    for (int i = numRows * numberCallLettersPerRow; i < characters.Count; i++)
                    {
                        remaining += characters[i];
                    }
                    tempWords.Add(new Word(remaining, characterWords.Last().GetTotalLength()));
                }

                characterWords = tempWords;
            }
            while (horizontalBan != -1 || verticalBan != -1 || diagonalBan != -1);
        }
    }
}
