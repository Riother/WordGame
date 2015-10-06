using IGT.WordGameGenerator.Common;
using IGT.WordGameGenerator.Common.Division;
using IGT.WordGameGenerator.Localization.Division;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IGTWordGameGenerator.Division
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
			SetModel(new DivisionsModel());
			SetLocalization(new DivisionsPanelUserControlLocalization());
		}

        /// <summary>
        /// Sets model of the Divisions panel
        /// </summary>
        /// <param name="model">The model instance</param>
		public void SetModel(DivisionsModel model)
		{
			if(_model != null) _model.PropertyChanged -= DivisionsListChanged;
			_model = model ?? new DivisionsModel();
			DataBind();
		}

		private void DataBind()
		{
			NumberOfDivisions.DataContext = _model;
			LosePermutations.DataContext = _model;
			_model.PropertyChanged -= DivisionsListChanged;
			_model.PropertyChanged += DivisionsListChanged;
			DivisionsListChanged(this, new PropertyChangedEventArgs("DivisionList"));
		}

        /// <summary>
        /// Sets localization settings for the UI element
        /// </summary>
        /// <param name="localization">The localization object, representing a language</param>
		public void SetLocalization(DivisionsPanelUserControlLocalization localization)
		{
			_localization = localization ?? new DivisionsPanelUserControlLocalization();
			DataBindLocale();
			DataBind();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
			foreach (var i in DivisionList.Children)
			{
				if (i is DivisionUserControl)
				{
					(i as DivisionUserControl).SetLocalization(_localization.Division);
				}
			}
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			var division = new DivisionModel(_model);
			_model.AddDivision(division);
		}

		private void DivisionsListChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("DivisionList"))
			{
				DivisionList.Children.Clear();
				for (int i = 0; i < _model.NumberOfDivisions; ++i)
				{
					var divisionUC = new DivisionUserControl(_model.DivisionList[i], i);
					divisionUC.SetLocalization(_localization.Division);
					divisionUC.GotFocus += FocusDivision;
					divisionUC.LostFocus += LostFocusDivision;
                    DivisionList.Children.Add(divisionUC);
                    divisionUC.DeleteButton.IsEnabled = true;
				}
                if(DivisionList.Children.Count == 1)
                {
                    (DivisionList.Children[0] as DivisionUserControl).DeleteButton.IsEnabled = false;
                }
			}
		}

		private void FocusDivision(	object sender, RoutedEventArgs e)
		{
			if (!(sender is DivisionUserControl)) return;
			var realSender = sender as DivisionUserControl;
			realSender.PrizeLevelGrid.Background = Brushes.Yellow;
			realSender.PropertiesGrid.Background = Brushes.Yellow;
		}

		private void LostFocusDivision(object sender, RoutedEventArgs e)
		{
			if (!(sender is DivisionUserControl)) return;
			var realSender = sender as DivisionUserControl;
			realSender.PrizeLevelGrid.Background = new SolidColorBrush(new Color { R = 173, G = 173, B = 173, A = 255 });
			realSender.PropertiesGrid.Background = new SolidColorBrush(new Color { R = 173, G = 173, B = 173, A = 255 });
		}

		private DivisionsModel _model;
		private DivisionsPanelUserControlLocalization _localization;
	}
}
