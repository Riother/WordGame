using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Runtime.Serialization;
using System.Runtime.Serialization.Formatters.Binary;
using System.IO;
using IGT.WordGameGenerator.Common.PrizeLevel;
using IGT.WordGameGenerator.Common.Division;
using IGT.WordGameGenerator.Common.GameSetup;
using IGT.WordGameGenerator.Common;

namespace IGTWordGameGenerator.FileManagement
{
    /// <summary>
    /// Writes project data to file
    /// </summary>
    public class ProjectWriter : ProjectDataManager
    {
        private AllModelsModel project;

        /// <summary>
        /// Constructs project writer with references to the project models
        /// </summary>
        /// <param name="plModel">PrizeLevels model</param>
        /// <param name="gsuModel">GameSetup model</param>
        /// <param name="divModel">Divisions model</param>
        public ProjectWriter(PrizeLevelsModel plModel, GameSetupModel gsuModel, DivisionsModel divModel)
        {
            project = new AllModelsModel(plModel, gsuModel, divModel);
        }

		/// <summary>
		/// Constructs project writer with references to the project models
		/// </summary>
		public ProjectWriter(AllModelsModel model)
		{
			project = model;
		}

		/// <summary>
		/// Saves project out to designated file
		/// </summary>
		/// <param name="filePath">The path to the file where the data will be written</param>
		public void SaveProject(string filePath)
        {
            if (!filePath.Contains(DEFAULT_FILE_EXTENSION))
            {
                filePath += DEFAULT_FILE_EXTENSION;
            }
            IFormatter formatter = new BinaryFormatter();
            Stream fileStream = new FileStream(filePath, FileMode.OpenOrCreate, FileAccess.Write, FileShare.None);
            formatter.Serialize(fileStream, project);
            fileStream.Close();
        }
    }
}
