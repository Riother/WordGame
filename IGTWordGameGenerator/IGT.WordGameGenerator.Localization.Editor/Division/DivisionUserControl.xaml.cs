using IGT.WordGameGenerator.Localization.Division;
using System.ComponentModel;
using System.Windows.Controls;

namespace IGT.WordGameGenerator.Localization.Editor.Division
{
	/// <summary>
	/// Interaction logic for DivisionUserControl.xaml
	/// </summary>
	public partial class DivisionUserControl : UserControl, INotifyPropertyChanged
	{
		/// <summary>
		/// Event manager for Division Panel
		/// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// Constructor of Division GUI object
		/// </summary>
		public DivisionUserControl()
		{
			InitializeComponent();
			SetLocalization(new DivisionUserControlLocalization());
		}

		public void SetLocalization(DivisionUserControlLocalization localization)
		{
			_localization = localization ?? new DivisionUserControlLocalization();
			DataBindLocale();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
		}

		private DivisionUserControlLocalization _localization;
	}
}
