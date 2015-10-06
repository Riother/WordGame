using IGT.WordGameGenerator.Common.Division;
using IGT.WordGameGenerator.Common.GameSetup;
using IGT.WordGameGenerator.Common.PrizeLevel;
using System;

namespace IGT.WordGameGenerator.Common
{
	/// <summary>
	/// Model for the project settings of the client
	/// </summary>
	[Serializable]
	public class AllModelsModel
	{
		/// <summary>
		/// Model for the GameSetup panel
		/// </summary>
		public GameSetupModel GameSetupModel { get; set; }

        /// <summary>
        /// Model for the PrizeLevels panel
        /// </summary>
		public PrizeLevelsModel PrizeLevelsModel { get; set; }

        /// <summary>
        /// Model for the Divisions panel
        /// </summary>
		public DivisionsModel DivisionsModel { get; set; }

        /// <summary>
        /// Constructor that creates the model data for the client project
        /// </summary>
		public AllModelsModel()
		{
			GameSetupModel = new GameSetupModel();
			PrizeLevelsModel = new PrizeLevelsModel();
			DivisionsModel = new DivisionsModel();
		}

		/// <summary>
		/// Constructs a ProjectData object that holds the references to the models of the project
		/// </summary>
		/// <param name="plModel"></param>
		/// <param name="gsuModel"></param>
		/// <param name="divModel"></param>
		public AllModelsModel(PrizeLevelsModel plModel, GameSetupModel gsuModel, DivisionsModel divModel)
		{
			PrizeLevelsModel = plModel;
			GameSetupModel = gsuModel;
			DivisionsModel = divModel;
		} 
    }
}