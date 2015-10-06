using IGT.WordGameGenerator.Localization.GameSetup;
using System.Windows.Controls;

namespace IGT.WordGameGenerator.Localization.Editor.GameSetup
{
	/// <summary>
	/// Interaction logic for CallLettersUserControl.xaml
	/// </summary>
	public partial class CallLettersUserControl : UserControl
	{
        /// <summary>
        /// Constructor for Call Letter GUI object
        /// </summary>
		public CallLettersUserControl()
		{
			InitializeComponent();
			SetLocalization(new CallLettersUserControlLocalization());
		}

        /// <summary>
        /// Sets the data context for the CallLetters GUI element
        /// </summary>
        /// <param name="model">The DataContext</param>
		public void SetLocalization(CallLettersUserControlLocalization localization)
		{
			_localization = localization ?? new CallLettersUserControlLocalization();
			DataBindLocale();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
		}

		private CallLettersUserControlLocalization _localization;
	}
}
