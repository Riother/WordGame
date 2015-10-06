using IGT.WordGameGenerator.Common.Division;
using IGT.WordGameGenerator.Localization.Division;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace IGTWordGameGenerator.Division
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
		/// Current count of divisions
		/// </summary>
		public int Division
		{
			get { return _division; }
			set
			{
				_division = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Division"));
			}
		}

		/// <summary>
		/// Constructor of Division GUI object
		/// </summary>
		public DivisionUserControl(DivisionModel model = null, int division = 0)
		{
			InitializeComponent();
			SetModel(model);
			SetLocalization(new DivisionUserControlLocalization());
			Division = division + 1;
		}

		/// <summary>
		/// Sets the DataContext for the Division GUI Element
		/// </summary>
		/// <param name="model"></param>
		public void SetModel(DivisionModel model)
		{
			//if (_model != null) _model.PropertyChanged -= UpdateValue;
			_model = model;
			DataBind();
		}

		private void DataBind()
		{
			DivisionLabel.DataContext = this;
			PrizeLevel.DataContext = _model;
			Value.DataContext = _model;
			x1.IsChecked = _model == null ? true : _model.Multiplier == 1;
			x2.IsChecked = _model == null ? false : _model.Multiplier == 2;
			x3.IsChecked = _model == null ? false : _model.Multiplier == 3;
			//if (_model != null) _model.PropertyChanged += UpdateValue;
		}

        /// <summary>
        /// Sets localization settings for the UI element
        /// </summary>
        /// <param name="localization">The localization object, representing a language</param>
		public void SetLocalization(DivisionUserControlLocalization localization)
		{
			_localization = localization ?? new DivisionUserControlLocalization();
			DataBindLocale();
			DataBind();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
		}
		private void Multiplier_Checked(object sender, RoutedEventArgs e)
		{
			if (sender == x1)
			{
				_model.Multiplier = 1;
			}
			else if (sender == x2)
			{
				_model.Multiplier = 2;
			}
			else if (sender == x3)
			{
				_model.Multiplier = 3;
			}
		}

		private void Close_Click(object sender, RoutedEventArgs e)
		{
            if (_model != null)
            {
                _model.Delete();
            }
		}

		private DivisionModel _model;
		private DivisionUserControlLocalization _localization;
		private int _division;
	}
}
