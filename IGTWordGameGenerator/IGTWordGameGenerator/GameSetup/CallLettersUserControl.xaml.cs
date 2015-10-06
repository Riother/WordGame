using IGT.WordGameGenerator.Common;
using IGT.WordGameGenerator.Common.GameSetup;
using IGT.WordGameGenerator.Common.PrizeLevel;
using IGT.WordGameGenerator.Localization.GameSetup;
using System;
using System.Collections.Generic;
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

namespace IGTWordGameGenerator.GameSetup
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
			SetModel(new CallLettersModel());
			SetLocalization(new CallLettersUserControlLocalization());
		}

        /// <summary>
        /// Sets the data context for the CallLetters GUI element
        /// </summary>
        /// <param name="model">The DataContext</param>
		public void SetModel(CallLettersModel model)
		{
			_model = model;
			DataBind();
		}

		private void DataBind()
		{
			MaxCallLetters.DataContext = _model;
			MinCallLetters.DataContext = _model;
			NumCallLettersPerRowInHistory.DataContext = _model;
		}

        /// <summary>
        /// Sets localization settings for the UI element
        /// </summary>
        /// <param name="localization">The localization object, representing a language</param>
		public void SetLocalization(CallLettersUserControlLocalization localization)
		{
			_localization = localization ?? new CallLettersUserControlLocalization();
			DataBindLocale();
			DataBind();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
		}

		private CallLettersModel _model;
		private CallLettersUserControlLocalization _localization;
	}
}
