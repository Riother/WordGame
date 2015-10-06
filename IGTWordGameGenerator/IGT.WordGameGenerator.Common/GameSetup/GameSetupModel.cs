using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Common.GameSetup
{
    /// <summary>
    /// Data storage of Game Setup fields
    /// </summary>
	[Serializable]
	public class GameSetupModel: INotifyValidationRequired
	{
		[field: NonSerialized]
		public event ValidationRequested ValidationRequest;
		/// <summary>
		/// The model that contains the file paths to the desired word lists
		/// </summary>
		public WordListsModel WordListsModel { get; set; }

		/// <summary>
		///	Data manager for the Call Letters
		/// </summary>
		public CallLettersModel CallLettersModel { get; set; }

        /// <summary>
        /// The model for the General Panel
        /// </summary>
		//public GeneralModel GeneralModel { get; set; }

        /// <summary>
        /// Constructor for Game Setup data fields
        /// </summary>
		public GameSetupModel()
		{
			WordListsModel = new WordListsModel();
			CallLettersModel = new CallLettersModel();
			//GeneralModel = new GeneralModel();
		}
		public void SetValidator(ValidationRequested validator)
		{
			ValidationRequest += validator;
			WordListsModel.SetValidator(validator);
			CallLettersModel.SetValidator(validator);
		}

		private CallLettersModel _callLettersModel;
	}
}
