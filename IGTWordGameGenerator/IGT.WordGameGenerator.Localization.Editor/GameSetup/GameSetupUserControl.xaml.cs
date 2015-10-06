using IGT.WordGameGenerator.Localization.GameSetup;
using System.Windows.Controls;

namespace IGT.WordGameGenerator.Localization.Editor.GameSetup
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
			SetLocalization(new GameSetupUserControlLocalization());
		}

		/// <summary>
		/// Sets the data context for the UserControl panel
		/// </summary>
		/// <param name="model">The data context</param>
		public void SetLocalization(GameSetupUserControlLocalization localization)
		{
			_localization = localization ?? new GameSetupUserControlLocalization();
			DataBindLocale();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
			CallLetters.SetLocalization(_localization.CallLetters);
			WordLists.SetLocalization(_localization.WordLists);
		}

		private GameSetupUserControlLocalization _localization;
	}
}
