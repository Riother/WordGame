using IGT.WordGameGenerator.Common;
using IGT.WordGameGenerator.Common.PrizeLevel;
using IGT.WordGameGenerator.Localization.PrizeLevel;
using System.Collections.Generic;
using System.ComponentModel;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Media;

namespace IGTWordGameGenerator.PrizeLevel
{
	/// <summary>
	/// Interaction logic for PrizeLevelsPanelUserControl.xaml
	/// </summary>
	public partial class PrizeLevelsPanelUserControl : UserControl, INotifyValidationRequired
	{
        /// <summary>
        /// ErrorService delegate
        /// </summary>
		public event ValidationRequested ValidationRequest;

		/// <summary>
		/// Create a new prize levels panel user control
		/// </summary>
		public PrizeLevelsPanelUserControl()
		{
			InitializeComponent();
			SetModel(new PrizeLevelsModel());
			SetLocalization(new PrizeLevelsPanelUserControlLocalization());
		}

        /// <summary>
        /// Set delegate for ErrorService validation
        /// </summary>
        /// <param name="validator"></param>
		public void SetValidator(ValidationRequested validator)
		{
			ValidationRequest += validator;
			if (ValidationRequest != null)
			{
				foreach (var i in PrizeLevelList.Children)
				{
					var prizeLevelUC = i as PrizeLevelUserControl;
					if(prizeLevelUC != null)
					{
						prizeLevelUC.SetValidator(validator);
					}
				}
			}
		}

        /// <summary>
        /// Sets localization settings for the UI element
        /// </summary>
        /// <param name="localization">The localization object, representing a language</param>
		public void SetLocalization(PrizeLevelsPanelUserControlLocalization localization)
		{
			_localization = localization ?? new PrizeLevelsPanelUserControlLocalization();
			DataBindLocale();
			DataBind();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
			foreach (var i in PrizeLevelList.Children)
			{
				if (i is PrizeLevelUserControl)
				{
					(i as PrizeLevelUserControl).SetLocalization(_localization.PrizeLevel);
				}
			}
		}

		/// <summary>
		/// Sets DataContext for PrizeLevels Panel
		/// </summary>
		/// <param name="model">The model with the data for the panel</param>
		public void SetModel(PrizeLevelsModel model)
		{
			if (_model != null) _model.PropertyChanged -= PrizeLevelListChanged;
			_model = model ?? new PrizeLevelsModel();
			DataBind();
		}

		private void DataBind()
		{
			NumberOfPrizeLevels.DataContext = _model;
			ChooseUniqueLetters.DataContext = _model;
			Randomize.DataContext = _model;
			_model.PropertyChanged -= PrizeLevelListChanged;
			_model.PropertyChanged += PrizeLevelListChanged;
			PrizeLevelListChanged(this, new PropertyChangedEventArgs("PrizeLevelList"));
		}

		private void AddButton_Click(object sender, RoutedEventArgs e)
		{
			_model.AddPrizeLevel(new PrizeLevelModel(_model));
		}

		private void PrizeLevelListChanged(object sender, PropertyChangedEventArgs e)
		{
			if (e.PropertyName.Equals("PrizeLevelList"))
			{
				PrizeLevelList.Children.Clear();
				for (int i = 0; i < _model.NumberOfPrizeLevels; ++i)
				{
                    var prizeLevelUC = new PrizeLevelUserControl(_model.PrizeLevelList[i]);
					prizeLevelUC.SetLocalization(_localization.PrizeLevel);
                    prizeLevelUC.GotFocus += FocusPrizeLevel;
                    prizeLevelUC.LostFocus += LostFocusPrizeLevel;
                    prizeLevelUC.CloseButton.IsEnabled = true;
					if(ValidationRequest != null)
					{
						foreach(var validator in ValidationRequest.GetInvocationList())
						{
							prizeLevelUC.SetValidator(validator as ValidationRequested);
						}
					}
					PrizeLevelList.Children.Add(prizeLevelUC);
				}
				if (ValidationRequest != null) ValidationRequest();

                if(PrizeLevelList.Children.Count == 2)
                {
                    foreach(UIElement currentPL in PrizeLevelList.Children)
                    {
                        (currentPL as PrizeLevelUserControl).CloseButton.IsEnabled = false;
                    }
                }
			}
		}

		private void FocusPrizeLevel(object sender, RoutedEventArgs e)
		{
			if (!(sender is PrizeLevelUserControl)) return;
			var realSender = sender as PrizeLevelUserControl;
			realSender.PanelGrid.Background = Brushes.Yellow;
		}

		private void LostFocusPrizeLevel(object sender, RoutedEventArgs e)
		{
			if (!(sender is PrizeLevelUserControl)) return;
			var realSender = sender as PrizeLevelUserControl;
			realSender.PanelGrid.Background = new SolidColorBrush(new Color { R = 173, G = 173, B = 173, A = 255 });
		}

		private PrizeLevelsPanelUserControlLocalization _localization;
		private PrizeLevelsModel _model;
	}
}
