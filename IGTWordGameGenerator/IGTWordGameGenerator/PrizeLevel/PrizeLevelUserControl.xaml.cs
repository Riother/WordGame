using IGT.WordGameGenerator.Common;
using IGT.WordGameGenerator.Common.PrizeLevel;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Collections.Generic;
using IGT.WordGameGenerator.Localization.PrizeLevel;

namespace IGTWordGameGenerator.PrizeLevel
{
	/// <summary>
	/// Interaction logic for PrizeLevelUserControl.xaml
	/// </summary>
	public partial class PrizeLevelUserControl : UserControl, INotifyValidationRequired
	{
		/// <summary>
		/// Create a new prize level user control
		/// </summary>
		/// <param name="model">The model of the PrizeLevel panel</param>
		public PrizeLevelUserControl(PrizeLevelModel model = null)
		{
			InitializeComponent();
			SetModel(model);
			SetLocalization(new PrizeLevelUserControlLocalization());
		}

        /// <summary>
        /// Sets model of prize level UI element
        /// </summary>
        /// <param name="model">Model to bind to</param>
		public void SetModel(PrizeLevelModel model)
		{
			_model = model ?? new PrizeLevelModel();
			DataBind();
		}

		private void DataBind()
		{
			Value.DataContext = _model;
			UniqueLetterCount.DataContext = _model;
			PrizeLevelLabel.DataContext = _model;
		}

        /// <summary>
        /// Sets localization of PrizeLevel
        /// </summary>
        /// <param name="localization">The localization object, representing a language to display</param>
		public void SetLocalization(PrizeLevelUserControlLocalization localization)
		{
			_localization = localization ?? new PrizeLevelUserControlLocalization();
			DataBindLocale();
			DataBind();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
		}

		/// <summary>
		/// Set ErrorService delegate
		/// </summary>
		/// <param name="validator"></param>
		public void SetValidator(ValidationRequested validator)
		{
			ValidationRequest += validator;
		}

		private void CloseButton_Click(object sender, RoutedEventArgs e)
		{
            _model.Delete();
		}

		private PrizeLevelModel _model;
		private PrizeLevelUserControlLocalization _localization;

        /// <summary>
        /// Delegate for ErrorService
        /// </summary>
		public event ValidationRequested ValidationRequest;

		private void UniqueLetterCount_TextChanged(object sender, TextChangedEventArgs e)
        {
			if (_model == null) return;
			int tryparse;
			if(sender is TextBox && int.TryParse((sender as TextBox).Text, out tryparse))
			{
				_model.UniqueLetterCount = tryparse;
				if(ValidationRequest != null) ValidationRequest();
			}
        }
	}
}
