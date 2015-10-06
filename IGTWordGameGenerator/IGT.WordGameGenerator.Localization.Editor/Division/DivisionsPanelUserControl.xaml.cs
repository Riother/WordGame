using IGT.WordGameGenerator.Localization.Division;
using System.Windows.Controls;

namespace IGT.WordGameGenerator.Localization.Editor.Division
{
	/// <summary>
	/// Interaction logic for DivisionsPanelUserControl.xaml
	/// </summary>
	public partial class DivisionsPanelUserControl : UserControl
	{
		/// <summary>
		/// Constructor for Divisions Panel GUI element
		/// </summary>
		public DivisionsPanelUserControl()
		{
			InitializeComponent();
			SetLocalization(new DivisionsPanelUserControlLocalization());
		}

		/// <summary>
		/// Sets model of the Divisions panel
		/// </summary>
		/// <param name="model">The model instance</param>
		public void SetLocalization(DivisionsPanelUserControlLocalization localization)
		{
			_localization = localization ?? new DivisionsPanelUserControlLocalization();
			DataBindLocale();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
			foreach(var i in DivisionList.Children)
			{
				if (i is DivisionUserControl)
				{
					(i as DivisionUserControl).SetLocalization(_localization.Division);
				}
			}
		}

		private DivisionsPanelUserControlLocalization _localization;
	}
}
