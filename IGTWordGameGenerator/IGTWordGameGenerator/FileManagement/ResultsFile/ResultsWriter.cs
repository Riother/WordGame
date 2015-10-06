using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using IGT.WordGameGenerator.Common.Division;
using IGT.WordGameGenerator.Common.PrizeLevel;
using IGT.WordGameGenerator.Common;
using IGT.WordGameGenerator.Common.GameSetup;
using IGT.WordGameGenerator.Services.DebugTools;
using IGT.WordGameGenerator.Services.ListTools;
using IGT.WordGameGenerator.Services.PlayGeneration;
using IGT.WordGameGenerator.Services.WordManagement;
using IGT.WordGameGenerator.Services.WordManagement.WordListManager;

namespace IGTWordGameGenerator.FileManagement
{
    /// <summary>
    /// Writes gameplay results to designated file path
    /// </summary>
    public class ResultsWriter
    {
        /// <summary>
        /// Creates new file and writes generated gameplay into it
        /// </summary>
        /// <param name="targetFilePath">Path to the file being written to</param>
        /// <param name="validPath">Valid words list file path</param>
        /// <param name="bannedPath">Banned words list file path</param>
        /// <param name="allModel">Project configuration</param>
        public void CreateGeneratedFile(string targetFilePath, string validPath, string bannedPath, AllModelsModel allModel)
        {
            List<PrizeLevelModel> prizeLevels = new List<PrizeLevelModel>(allModel.PrizeLevelsModel.PrizeLevelList);
            List<DivisionModel> divisions = new List<DivisionModel>(allModel.DivisionsModel.DivisionList);
            GameSetupModel gameModel = allModel.GameSetupModel;

            PlayGenerator generator = new PlayGenerator(validPath, bannedPath);
            StringBuilder gameplay = new StringBuilder();

            int numPermutations = allModel.DivisionsModel.NumPermutations;
            List<int> complexities = new List<int>();

            LogWriter.GetInstance().WriteLog("Generating complexities");

            // sets complexities for entire generation
            if(!allModel.PrizeLevelsModel.Randomize)
            {
                foreach (PrizeLevelModel currentPrizeLevel in prizeLevels)
                {
                    complexities.Add(currentPrizeLevel.UniqueLetterCount);
                }
                LogWriter.GetInstance().WriteLog("Finished non-random complexities");
            }

            for (int i = 0; i < numPermutations; i++)
            {
                // creates new complexities list for each permutation
                if(allModel.PrizeLevelsModel.Randomize)
                {
                    LogWriter.GetInstance().WriteLog("Generating random complexities");
                    complexities.Clear();
                    WordListManager lists = new WordListManager(validPath, bannedPath, 1, 30);
                    List<int> potentialComplexities = new List<int>();
                    for (int complexityIndex = lists.GetLeastComplexCount(); complexityIndex < lists.GetMostComplexCount(); complexityIndex++)
                    {
                        if (lists.HasComplexity(complexityIndex))
                        {
                            potentialComplexities.Add(complexityIndex);
                        }
                    }
                    potentialComplexities.Shuffle();

                    foreach (PrizeLevelModel currentPrizeLevel in prizeLevels)
                    {
                        int seed = (int)((DateTime.Now.Ticks / TimeSpan.TicksPerMillisecond) % Int32.MaxValue);
                        Random rand = new Random(seed);
                        int nextComplexityIndex = rand.Next(0, potentialComplexities.Count);
                        int nextComplexityValue = potentialComplexities[nextComplexityIndex];
                        potentialComplexities.Remove(nextComplexityValue);

                        complexities.Add(nextComplexityValue);
                    }
                    LogWriter.GetInstance().WriteLog("Finished randomized complexities");
                }

                LogWriter.GetInstance().WriteLog("Creating WordManager");
                WordManager manager = new WordManager(prizeLevels.Count, complexities, validPath, bannedPath);
                LogWriter.GetInstance().WriteLog("Finished creating WordManager");
                foreach (DivisionModel currentDivision in divisions)
                {
                    LogWriter.GetInstance().WriteLog("Generating gameplay");
                    string winPermutation = generator.GenerateGameplay(prizeLevels, currentDivision, gameModel, complexities, prizeLevels.IndexOf(currentDivision.PrizeLevel), manager, allModel.PrizeLevelsModel.Randomize);
                    gameplay.Append(winPermutation);
                    LogWriter.GetInstance().WriteLog("Finished generating gameplay");
                }
                // lose division
                DivisionModel loss = new DivisionModel(allModel.DivisionsModel);
                loss.Multiplier = 0;
                gameplay.Append(generator.GenerateGameplay(prizeLevels, loss, gameModel, complexities, -1, manager, allModel.PrizeLevelsModel.Randomize));
            }

            File.WriteAllText(targetFilePath, gameplay.ToString());
        }
    }
}
