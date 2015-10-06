using IGT.WordGameGenerator.Common;
using IGT.WordGameGenerator.Common.GameSetup;
using IGT.WordGameGenerator.Localization.GameSetup;
using IGTWordGameGenerator.Services.PlayGeneration;
using System.Windows;
using System.Windows.Controls;

namespace IGTWordGameGenerator.GameSetup
{
	/// <summary>
	/// Interaction logic for GameSetupUserControl.xaml
	/// </summary>
	public partial class GameSetupUserControl : UserControl
	{
		/// <summary>
		/// Constructor for GameSetup Panel GUI object
		/// </summary>
		public GameSetupUserControl()
		{
			InitializeComponent();
			SetModel(new GameSetupModel());
			SetLocalization(new GameSetupUserControlLocalization());
		}

		/// <summary>
		/// Sets the data context for the UserControl panel
		/// </summary>
		/// <param name="model">The data context</param>
		public void SetModel(GameSetupModel model)
		{
			_model = model ?? new GameSetupModel();
			DataBind();
		}

		private void DataBind()
		{
			WordLists.SetModel(_model.WordListsModel);
			CallLetters.SetModel(_model.CallLettersModel);
		}

        /// <summary>
        /// Sets localization settings for the UI element
        /// </summary>
        /// <param name="localization">The localization object, representing a language</param>
		public void SetLocalization(GameSetupUserControlLocalization localization)
		{
			_localization = localization ?? new GameSetupUserControlLocalization();
			DataBindLocale();
			DataBind();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
			CallLetters.SetLocalization(_localization.CallLetters);
			WordLists.SetLocalization(_localization.WordLists);
		}

		private GameSetupModel _model;
		private GameSetupUserControlLocalization _localization;
	}
}
