using IGT.WordGameGenerator.Localization.PrizeLevel;
using System.Windows;
using System.Windows.Controls;

namespace IGT.WordGameGenerator.Localization.Editor.PrizeLevel
{
	/// <summary>
	/// Interaction logic for PrizeLevelUserControl.xaml
	/// </summary>
	public partial class PrizeLevelUserControl : UserControl
	{
		/// <summary>
		/// Create a new prize level user control
		/// </summary>
		/// <param name="model">The model of the PrizeLevel panel</param>
		public PrizeLevelUserControl()
		{
			InitializeComponent();
			SetLocalization(new PrizeLevelUserControlLocalization());
		}

		public void SetLocalization(PrizeLevelUserControlLocalization localization)
		{
			_localization = localization ?? new PrizeLevelUserControlLocalization();
			DataBindLocale();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
		}
		private PrizeLevelUserControlLocalization _localization;
	}
}
