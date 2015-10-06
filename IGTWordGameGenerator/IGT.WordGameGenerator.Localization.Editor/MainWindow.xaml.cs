using System.ComponentModel;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using System.Windows.Controls;
using IGT.WordGameGenerator.Services.Localization;

namespace IGT.WordGameGenerator.Localization.Editor
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window
	{
		private MainWindowLocalization _localization;

		/// <summary>
		/// ErrorService MainWindow ID
		/// </summary>
		public int? ErrorServiceId { get; set; }

		/// <summary>
		/// Initialize main window
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			SetLocalization(new MainWindowLocalization());
		}

		private void SetLocalization(MainWindowLocalization localization)
		{
			_localization = localization ?? new MainWindowLocalization();
			DataBindLocale();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
			PrizeLevels.SetLocalization(_localization.PrizeLevelsPanel);
			GameSetup.SetLocalization(_localization.GameSetupPanel);
			Divisions.SetLocalization(_localization.DivisionPanel);
			SetErrorText();
        }

		private void SetErrorText()
		{
			Errors.Children.Clear();
			for(int i = 0; i < _localization.ErrorService.ErrorMessages.Length;++i)
			{
				var textbox = new ErrorTextBox();
				textbox.IsWarning = false;
				textbox.Index = i;
				textbox.Text = _localization.ErrorService.ErrorMessages[i];
				textbox.TextChanged += ErrorTextChanged;
				Errors.Children.Add(textbox);
			}

			Warnings.Children.Clear();
			for (int i = 0; i < _localization.ErrorService.WarningMessages.Length; ++i)
			{
				var textbox = new ErrorTextBox();
				textbox.IsWarning = true;
				textbox.Index = i;
				textbox.Text = _localization.ErrorService.WarningMessages[i];
				textbox.TextChanged += ErrorTextChanged;
				Warnings.Children.Add(textbox);
			}
		}

		private void ErrorTextChanged(object sender, TextChangedEventArgs e)
		{
			if (!(sender is ErrorTextBox)) return;
			var Errortext = sender as ErrorTextBox;
			if(Errortext.IsWarning)
			{
				_localization.ErrorService.WarningMessages[Errortext.Index] = Errortext.Text;
			}
			else
			{
				_localization.ErrorService.ErrorMessages[Errortext.Index] = Errortext.Text;
			}
		}

		private void TitleTextChanged(object sender, TextChangedEventArgs e)
		{
			if (_localization == null) return;
			if (sender == Title) _localization.Title = Title.Text;
		}

		private void NewClick(object sender, RoutedEventArgs e)
		{
			SetLocalization(new MainWindowLocalization());
		}

		private void OpenClick(object sender, RoutedEventArgs e)
		{
			var dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.FileName = "English"; // Default file name
			dlg.DefaultExt = ".wgglang"; // Default file extension
			dlg.Filter = "Word Game Generator Localization (.wgglang) | *.wgglang"; // Filter files by extension
			var success = dlg.ShowDialog();
			if (success.HasValue && success.Value)
			{
				SetLocalization(LocalizationHelpers.LoadLocalization(dlg.FileName));
			}
			
		}

		private void SaveClick(object sender, RoutedEventArgs e)
		{
			var dlg = new Microsoft.Win32.SaveFileDialog();
			dlg.FileName = "English"; // Default file name
			dlg.DefaultExt = ".wgglang"; // Default file extension
			dlg.Filter = "Word Game Generator Localization (.wgglang) | *.wgglang"; // Filter files by extension
			var success = dlg.ShowDialog();
			if (success.HasValue && success.Value) LocalizationHelpers.SaveLocalization(dlg.FileName, _localization);
		}
	}
}
