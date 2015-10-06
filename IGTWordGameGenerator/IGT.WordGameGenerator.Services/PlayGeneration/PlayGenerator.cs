using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using IGT.WordGameGenerator.Common.Division;
using IGT.WordGameGenerator.Services.WordManagement;
using IGT.WordGameGenerator.Common.PrizeLevel;
using IGT.WordGameGenerator.Common.GameSetup;
using IGT.WordGameGenerator.Services.DebugTools;

namespace IGT.WordGameGenerator.Services.PlayGeneration
{
    /// <summary>
    /// Generates contents of gameplay file
    /// </summary>
    public class PlayGenerator
    {
        private string _validPath;
        private string _bannedPath;

        /// <summary>
        /// Constructs generator object that has valid and banned word file paths
        /// </summary>
        /// <param name="validWordsFile">File path to valid words file</param>
        /// <param name="bannedWordsFile">File path to banned words file</param>
        public PlayGenerator(string validWordsFile, string bannedWordsFile)
        {
            _validPath = validWordsFile;
            _bannedPath = bannedWordsFile;
        }

        /// <summary>
        /// Returns generated gameplay
        /// </summary>
        /// <param name="prizeLevels">The prize levels of the current project configuration</param>
        /// <param name="currentDivision">The division currently being generated</param>
        /// <param name="gameSetup">The game configurations</param>
        /// <returns>String representation of gameplay</returns>
        public string GenerateGameplay(List<PrizeLevelModel> prizeLevels, DivisionModel currentDivision, GameSetupModel gameSetup, List<int> complexities, int prizeLevelIndex, WordManager manager, bool isRandomized)
        {
            StringBuilder gameplay = new StringBuilder();
            List<Word> wordList = manager.GetWordSet();

            LogWriter.GetInstance().WriteLog("Creating words list");
            foreach (Word currentWord in wordList)
            {
                gameplay.Append(currentWord.ToString());
                gameplay.Append(":").Append((char)('A' + complexities.IndexOf(currentWord.GetComplexity())));
                if(wordList.IndexOf(currentWord) < wordList.Count - 1)
                {
                    gameplay.Append(",");
                }
            }
            gameplay.Append("|");
            LogWriter.GetInstance().WriteLog("Finished creating words list");

            // letter logic goes here
            List<char> callLetters = new List<char>();
            char terminator = ' ';

            LogWriter.GetInstance().WriteLog("Determining min/max call letters");
            int minimumNumLetters = -1;
            int maximumNumLetters = -1;
            if(isRandomized)
            {
                int minValue = Int32.MaxValue;
                int maxCallLetterValue = 0;
                foreach(int complexity in complexities)
                {
                    maxCallLetterValue += complexity;
                    if(complexity < minValue)
                    {
                        minValue = complexity;
                    }
                }

                minimumNumLetters = minValue;
                maximumNumLetters = maxCallLetterValue < 10 ? maxCallLetterValue : 10;
            }
            else
            {
               minimumNumLetters = gameSetup.CallLettersModel.MinCallLetters;
               maximumNumLetters = gameSetup.CallLettersModel.MaxCallLetters;
            }
            LogWriter.GetInstance().WriteLog("Selected min/max call letters");
            
            int seed = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % Int32.MaxValue);
            Random rand = new Random(seed);
            int numCallLetters = rand.Next(minimumNumLetters, maximumNumLetters + 1);

            if (currentDivision.Multiplier == 0)
            {
                LogWriter.GetInstance().WriteLog("Generating loss set");
                callLetters = manager.GetLossSet(numCallLetters, gameSetup.CallLettersModel.NumCallLettersPerRowInHistory);
                terminator = '*';
                LogWriter.GetInstance().WriteLog("Finished creating loss set");
            }
            else
            {
                LogWriter.GetInstance().WriteLog("Generating win set");
                callLetters = manager.GetWinSet(complexities[prizeLevelIndex], numCallLetters, gameSetup.CallLettersModel.NumCallLettersPerRowInHistory);
                terminator = currentDivision.Multiplier.ToString().ToCharArray()[0];
                LogWriter.GetInstance().WriteLog("Finished creating win set");                
            }

            foreach (char currentLetter in callLetters)
            {
                gameplay.Append(currentLetter).Append(',');
            }
            gameplay.Append(terminator).Append("\n");

            return gameplay.ToString();
        }
    }
}
