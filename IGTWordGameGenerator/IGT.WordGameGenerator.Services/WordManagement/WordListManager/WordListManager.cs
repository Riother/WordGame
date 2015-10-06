using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Services.WordManagement.WordListManager
{
    /// <summary>
    /// Reads in files that contain word lists
    /// </summary>
    public class WordListManager
    {
        private int maxComplexity;
        private int minComplexity;
        private string acceptedWordsPath;
        private string bannedWordsPath;
        private List<string>[] acceptedWords;
        private List<string> bannedWords;

        /// <summary>
        /// Constructor for manager of word list files
        /// </summary>
        /// <param name="acceptedWordFilePath">The path to the file with the valid words</param>
        /// <param name="bannedWordFilePath">The path to the file with the banned words</param>
        /// <param name="maximumComplexity">Maximum number of unique letters</param>
        /// <param name="minimumComplexity">Minimum number of unique letters</param>
        public WordListManager(string acceptedWordFilePath, string bannedWordFilePath, int minimumComplexity, int maximumComplexity)
        {
            maxComplexity = maximumComplexity;
            minComplexity = minimumComplexity;
            acceptedWordsPath = acceptedWordFilePath;
            bannedWordsPath = bannedWordFilePath;
        }

        /// <summary>
        /// Returns whether or not the read-in words 
        /// </summary>
        /// <param name="complexity"></param>
        /// <returns></returns>
        public bool HasComplexity(int complexity)
        {
            if(acceptedWords == null)
            {
                acceptedWords = new List<string>[maxComplexity + 1];
                for (int i = 0; i < acceptedWords.Length; i++)
                {
                    acceptedWords[i] = new List<string>();
                }
                for (int i = minComplexity; i <= maxComplexity; i++)
                {
                    ReadInStream(acceptedWords[i], acceptedWordsPath, i);
                }
            }
            if(complexity < maxComplexity)
            {
                return acceptedWords[complexity].Count > 0;
            }
            return false;
        }

        /// <summary>
        /// Retrieves list of valid words
        /// </summary>
        /// <returns>Returns list of valid words</returns>
        public List<string>[] GetAcceptedWords()
        {
            if (acceptedWords == null)
            {
                acceptedWords = new List<string>[maxComplexity + 1];
                for (int i = 0; i < acceptedWords.Length; i++)
                {
                    acceptedWords[i] = new List<string>();
                }
                for (int i = minComplexity; i <= maxComplexity; i++)
                {
                    ReadInStream(acceptedWords[i], acceptedWordsPath, i);
                }
            }
            return acceptedWords;
        }

        /// <summary>
        /// Retrieves random valid word
        /// </summary>
        /// <returns>Random valid word</returns>
        public string GetRandomValidWord(int requiredComplexity, string toBeReplaced = null)
        {
            if (acceptedWords == null)
            {
                acceptedWords = new List<string>[maxComplexity + 1];
                for (int i = 0; i < acceptedWords.Length; i++)
                {
                    acceptedWords[i] = new List<string>();
                }
                for (int i = minComplexity; i <= maxComplexity; i++)
                {
                    ReadInStream(acceptedWords[i], acceptedWordsPath, i);
                }
            }
            int index = -1;
            if(toBeReplaced != null)
            {
                index = acceptedWords[requiredComplexity].IndexOf(toBeReplaced);
            }
            Word randomWord = new Word("", 100);
            do
            {
                if (index == -1)
                {
                    int seed = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % Int32.MaxValue);
                    Random rand = new Random(seed);
                    randomWord = new Word(acceptedWords[requiredComplexity][rand.Next(0,acceptedWords[requiredComplexity].Count - 1)], 100);
                }
                else
                {
                    randomWord = new Word(acceptedWords[requiredComplexity][(index + 1) % acceptedWords.Length], 100);
                }
            }
            while (randomWord.GetComplexity() < GetLeastComplexCount() || randomWord.GetComplexity() > GetMostComplexCount());

            return randomWord.ToString();
        }

        /// <summary>
        /// Retreives list of banned words
        /// </summary>
        /// <returns>Returns list of banned words</returns>
        public List<string> GetBannedWords()
        {
            if (bannedWords == null)
            {
                bannedWords = new List<string>();
                ReadInStream(bannedWords, bannedWordsPath);
            }
            return bannedWords;
        }

        /// <summary>
        /// Retrieves the count of the largest number of unique letters in a word
        /// </summary>
        /// <returns>Returns the count of the most unique letters in a word</returns>
        public int GetMostComplexCount()
        {
            return maxComplexity;
        }

        /// <summary>
        /// Retrieves the count of the least number of unique letters in a word
        /// </summary>
        /// <returns>Returns the count of the least unique letters in a word</returns>
        public int GetLeastComplexCount()
        {
            return minComplexity;
        }

        private void ReadInStream(List<string> wordsList, string filePath, int complexity = 0)
        {
            StreamReader stream = new StreamReader(File.Open(filePath, FileMode.Open));
            string current;
            while ((current = stream.ReadLine()) != null)
            {
                Word test = new Word(current, 100);
                if (test.GetComplexity() == complexity || complexity == 0)
                {
                    wordsList.Add(current);
                }
            }
            stream.Close();

            if (filePath.Equals(acceptedWordsPath))
            {
                Shuffle(wordsList);
            }
        }

        private void Shuffle<T>(IList<T> list)
        {
            int seed = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % Int32.MaxValue);
            Random rand = new Random(seed);
            int n = list.Count;
            while (n > 1)
            {
                n--;
                int k = rand.Next(n + 1);
                T value = list[k];
                list[k] = list[n];
                list[n] = value;
            }
        }
    }
}
