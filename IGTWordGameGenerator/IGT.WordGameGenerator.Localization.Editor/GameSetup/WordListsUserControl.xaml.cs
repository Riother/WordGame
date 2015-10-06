using System.Windows;
using System.Windows.Controls;
using Microsoft.Win32;
using IGT.WordGameGenerator.Localization.GameSetup;

namespace IGT.WordGameGenerator.Localization.Editor.GameSetup
{
	/// <summary>
	/// Interaction logic for WordListsUserControl.xaml
	/// </summary>
	public partial class WordListsUserControl : UserControl
	{
		/// <summary>
		/// Constructor for WordList GUI object
		/// </summary>
		public WordListsUserControl()
		{
			InitializeComponent();
			SetLocalization(new WordListsUserControlLocalization());
		}

		/// <summary>
		/// Sets the model for the GUI object
		/// </summary>
		/// <param name="model"></param>
		public void SetLocalization(WordListsUserControlLocalization localization)
		{
			_localization = localization ?? new WordListsUserControlLocalization();
			DataBindLocale();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
		}
		private WordListsUserControlLocalization _localization;
	}
}
