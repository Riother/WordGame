using IGT.WordGameGenerator.Localization.PrizeLevel;
using System.Windows.Controls;

namespace IGT.WordGameGenerator.Localization.Editor.PrizeLevel
{
	/// <summary>
	/// Interaction logic for PrizeLevelsPanelUserControl.xaml
	/// </summary>
	public partial class PrizeLevelsPanelUserControl : UserControl
	{
		/// <summary>
		/// Create a new prize levels panel user control
		/// </summary>
		public PrizeLevelsPanelUserControl()
		{
			InitializeComponent();
			SetLocalization(new PrizeLevelsPanelUserControlLocalization());
		}

		public void SetLocalization(PrizeLevelsPanelUserControlLocalization localization)
		{
			_localization = localization ?? new PrizeLevelsPanelUserControlLocalization();
			DataBindLocale();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
			foreach(var i in PrizeLevelList.Children)
			{
				if(i is PrizeLevelUserControl)
				{
					(i as PrizeLevelUserControl).SetLocalization(_localization.PrizeLevel);
				}
			}
		}

		private PrizeLevelsPanelUserControlLocalization _localization;
	}
}
