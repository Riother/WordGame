using IGT.WordGameGenerator.Common;
using IGT.WordGameGenerator.Common.Division;
using IGT.WordGameGenerator.Common.PrizeLevel;
using IGT.WordGameGenerator.Localization;
using IGTWordGameGenerator.FileManagement;
using System.ComponentModel;
using System.Windows;
using System.IO;
using System.Collections.Generic;
using IGTWordGameGenerator.Services.PlayGeneration;
using IGT.WordGameGenerator.Services.ErrorService;
using System.Windows.Controls;
using System.Reflection;
using System;
using IGT.WordGameGenerator.Services.Localization;
using System.Text.RegularExpressions;

namespace IGTWordGameGenerator
{
	/// <summary>
	/// Interaction logic for MainWindow.xaml
	/// </summary>
	public partial class MainWindow : Window, INotifyPropertyChanged
	{
		private AllModelsModel _model;
		private MainWindowLocalization _localization;
		private IList<MenuItem> _languages;

        /// <summary>
        /// Event handler
        /// </summary>
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// ErrorService MainWindow ID
		/// </summary>
		public int? ErrorServiceId { get; set; }

		/// <summary>
		/// Window for displaying saving animation
		/// </summary>
		public ProcessingWindow processWindow { get; private set; }

		/// <summary>
		/// Initialize main window
		/// </summary>
		public MainWindow()
		{
			InitializeComponent();
			GameSetup.CreateButton.Click += CreateButton_Click;
			SetModel(new AllModelsModel());
			SetLocalization(new MainWindowLocalization());
			var prize = new PrizeLevelModel(_model.PrizeLevelsModel);
			prize.Value = 100;
			_model.PrizeLevelsModel.AddPrizeLevel(prize);
			_model.PrizeLevelsModel.AddPrizeLevel(new PrizeLevelModel(_model.PrizeLevelsModel));
			var div = new DivisionModel(_model.DivisionsModel);
			div.PrizeLevel = prize;
			_model.DivisionsModel.AddDivision(div);
		}
		private void Window_Initialized(object sender, EventArgs e)
		{
			FindLanguages();
		}

		private void FindLanguages()
		{
			var directory = new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).DirectoryName + "/lang/";
			if (Directory.Exists(directory))
			{
				Language.IsEnabled = true;
			}
			else
			{
				Language.IsEnabled = false;
				return;
			}
			_languages = new List<MenuItem>();
			var english = new MenuItem();
			english.Header = "English";
			english.IsCheckable = true;
			english.Checked += LanguageChanged;
			_languages.Add(english);
			foreach (var i in Directory.GetFiles(directory))
			{
				if(!Regex.IsMatch(i, ".*\\.wgglang")) continue;
				var lang = new MenuItem();
				lang.Header = Regex.Replace(new FileInfo(i).Name, "\\.wgglang", "");
				lang.IsCheckable = true;
				lang.Checked += LanguageChanged;
				_languages.Add(lang);
			}

			if(PropertyChanged != null) PropertyChanged(this, new PropertyChangedEventArgs("Languages"));
        }

        /// <summary>
        /// Languages that will appear in the menu option
        /// </summary>
		public IEnumerable<MenuItem> Languages { get { return _languages; } }

		private void LanguageChanged(object sender, RoutedEventArgs e)
		{
			if (sender is MenuItem)
			{
				if ((sender as MenuItem).Header.ToString() == "English")
				{
					SetLocalization(new MainWindowLocalization());
				}
				else SetLocalization(LocalizationHelpers.LoadLocalization(new FileInfo(new Uri(Assembly.GetExecutingAssembly().CodeBase).LocalPath).DirectoryName + "/lang/" + (sender as MenuItem).Header + ".wgglang") );
				foreach(var i in _languages)
				{
					if (i == sender) continue;
					i.IsChecked = false;
				}
			}
		}
		private void SetModel(AllModelsModel model)
		{
			if (_model != null)
			{
				_model.PrizeLevelsModel.PropertyChanged -= PrizeLevelListChanged;
			}
			_model = model ?? new AllModelsModel();
			DataBind();
		}

		private void SetLocalization(MainWindowLocalization localization)
		{
			_localization = localization ?? new MainWindowLocalization();
			DataBindLocale();
			DataBind();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
			Language.DataContext = _localization;
			Language.DataContext = this;
			PrizeLevels.SetLocalization(_localization.PrizeLevelsPanel);
			GameSetup.SetLocalization(_localization.GameSetupPanel);
			Divisions.SetLocalization(_localization.DivisionPanel);
			ErrorService.Instance.SetLocalization(_localization.ErrorService);
		}

		private void DataBind()
		{
			PrizeLevels.SetModel(_model.PrizeLevelsModel);
			PrizeLevels.SetValidator(VerifyAllModel);
			GameSetup.SetModel(_model.GameSetupModel);
			Divisions.SetModel(_model.DivisionsModel);
			if (_model != null)
			{
				_model.PrizeLevelsModel.PropertyChanged -= PrizeLevelListChanged;
				_model.PrizeLevelsModel.PropertyChanged += PrizeLevelListChanged;
				_model.DivisionsModel.SetValidator(VerifyAllModel);
				_model.PrizeLevelsModel.SetValidator(VerifyAllModel);
				_model.GameSetupModel.SetValidator(VerifyAllModel);
			}
			PrizeLevelListChanged(this, new PropertyChangedEventArgs("PrizeLevelList2"));
			ErrorTextBlock.DataContext = ErrorService.Instance;
			WarningTextBlock.DataContext = ErrorService.Instance;
		}

		private void PrizeLevelListChanged(object sender, PropertyChangedEventArgs e)
		{
			if (!e.PropertyName.Equals("PrizeLevelList2")) return;
			var prizeLevelCombo = _model.PrizeLevelsModel.PrizeLevelList;
			_model.DivisionsModel.PrizeLevels = prizeLevelCombo;
		}

		private void ErrorTextBlock_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
		{
			AdjustBorderVisibility();
			AdjustCreateButtonEnabled();
		}

		private void WarningTextBlock_TargetUpdated(object sender, System.Windows.Data.DataTransferEventArgs e)
		{
			AdjustBorderVisibility();
		}

		/// <summary>
		/// Ensures that the Create Button can only be pressed when there are no errors
		/// </summary>
		public void AdjustCreateButtonEnabled()
		{
			GameSetup.CreateButton.IsEnabled = string.IsNullOrEmpty(ErrorService.Instance.ErrorText);
		}

		private void AdjustBorderVisibility()
		{
			if (ErrorService.Instance.HasErrors() || ErrorService.Instance.HasWarnings())
			{
				ErrorBoxBorder.Visibility = Visibility.Visible;
				// hides error box if no errors
				if (!ErrorService.Instance.HasErrors())
				{
					ErrorHeader.Visibility = Visibility.Collapsed;
					ErrorTextBlock.Visibility = Visibility.Collapsed;
					ErrorColumn.Width = new GridLength(0);
				}
				else
				{
					ErrorHeader.Visibility = Visibility.Visible;
					ErrorTextBlock.Visibility = Visibility.Visible;
					ErrorColumn.Width = new GridLength(1, GridUnitType.Star);
				}
				// hides warning box if no warnings
				if (!ErrorService.Instance.HasWarnings())
				{
					WarningHeader.Visibility = Visibility.Collapsed;
					WarningTextBlock.Visibility = Visibility.Collapsed;
					WarningColumn.Width = new GridLength(0);
				}
				else
				{
					WarningHeader.Visibility = Visibility.Visible;
					WarningTextBlock.Visibility = Visibility.Visible;
					WarningColumn.Width = new GridLength(1, GridUnitType.Star);
				}
				ErrorRow.Height = new GridLength(200);
			}
			else
			{
				ErrorBoxBorder.Visibility = Visibility.Hidden;
				ErrorRow.Height = new GridLength(0);
				ErrorColumn.Width = new GridLength(0);
				WarningColumn.Width = new GridLength(0);
			}
		}

		private void New_Click(object sender, RoutedEventArgs e)
		{
			_model = new AllModelsModel();
			SetModel(_model);
			var prize = new PrizeLevelModel(_model.PrizeLevelsModel);
			prize.Value = 100;
			_model.PrizeLevelsModel.AddPrizeLevel(prize);
			_model.PrizeLevelsModel.AddPrizeLevel(new PrizeLevelModel(_model.PrizeLevelsModel));
			var div = new DivisionModel(_model.DivisionsModel);
			div.PrizeLevel = prize;
			_model.DivisionsModel.AddDivision(div);
		}

		private void Open_Click(object sender, RoutedEventArgs e)
		{
			var reader = new ProjectReader();
			var dlg = new Microsoft.Win32.OpenFileDialog();
			dlg.FileName = "Word-Game-Project"; // Default file name
			dlg.DefaultExt = ".wggproj"; // Default file extension
			dlg.Filter = "Word Game Generator Project (.wggproj) | *.wggproj"; // Filter files by extension
			var success = dlg.ShowDialog();
			if (success.HasValue && success.Value)
			{
				ErrorService.Instance.ClearErrors();
				ErrorService.Instance.ClearWarnings();
				SetModel(reader.OpenProject(dlg.FileName));
				VerifyAllModel();
			}
		}

		private void CreateButton_Click(object sender, RoutedEventArgs e)
		{
			processWindow = new ProcessingWindow();
			processWindow.SetModel(_model);
			processWindow.ShowDialog();
		}

		private void Save_Click(object sender, RoutedEventArgs e)
		{
			var writer = new ProjectWriter(_model);
			var dlg = new Microsoft.Win32.SaveFileDialog();
			dlg.FileName = "Word-Game-Project"; // Default file name
			dlg.DefaultExt = ".wggproj"; // Default file extension
			dlg.Filter = "Word Game Generator Project (.wggproj) | *.wggproj"; // Filter files by extension
			var success = dlg.ShowDialog();
			if (success.HasValue && success.Value) writer.SaveProject(dlg.FileName);
		}

		/// <summary>
		/// Verifies all the sub-models of the project
		/// </summary>
		public void VerifyAllModel()
		{
			VerifyGameSetup();
			VerifyPrizeLevels();
			VerifyDivisions();
		}

		private void VerifyGameSetup()
		{
			if (!File.Exists(_model.GameSetupModel.WordListsModel.ValidWords))
			{
				ErrorServiceId = ErrorService.Instance.ReportError(0, new List<string>() { "Valid Words" }, ErrorServiceId);
			}
			else
			{
				ErrorService.Instance.ResolveError(0, ErrorServiceId);
			}

			if (!File.Exists(_model.GameSetupModel.WordListsModel.BannedWords))
			{
				ErrorServiceId = ErrorService.Instance.ReportError(1, new List<string>() { "Banned Words" }, ErrorServiceId);
			}
			else
			{
				ErrorService.Instance.ResolveError(1, ErrorServiceId);
			}
		}

		private void VerifyPrizeLevels()
		{
			int minCall = _model.GameSetupModel.CallLettersModel.MinCallLetters;
			int maxCall = _model.GameSetupModel.CallLettersModel.MaxCallLetters;

			if (_model.PrizeLevelsModel.PrizeLevelList != null)
			{
				int fewestUniqueLetterCount = int.MaxValue;
				int sumOfPrizeLevels = 0;
				bool allUnique = true;
				string failedId1 = "";
				string failedId2 = "";

				foreach (PrizeLevelModel pl in _model.PrizeLevelsModel.PrizeLevelList)
				{
					if (pl.UniqueLetterCount < fewestUniqueLetterCount)
					{
						fewestUniqueLetterCount = pl.UniqueLetterCount;
					}
					sumOfPrizeLevels += pl.UniqueLetterCount;

					if (allUnique)
					{
						for (int i = _model.PrizeLevelsModel.PrizeLevelList.IndexOf(pl) + 1; i < _model.PrizeLevelsModel.NumberOfPrizeLevels; i++)
						{
							if (_model.PrizeLevelsModel.PrizeLevelList[i].UniqueLetterCount == pl.UniqueLetterCount)
							{
								allUnique = false;
								failedId1 = ((char)(pl.Index + 'A')).ToString();
								failedId2 = ((char)(_model.PrizeLevelsModel.PrizeLevelList[i].Index + 'A')).ToString();
							}
						}
					}
				}
				allUnique = allUnique || _model.PrizeLevelsModel.Randomize;

				// min call letters check
				if (fewestUniqueLetterCount < minCall)
				{
					ErrorServiceId = ErrorService.Instance.ReportError(4, new List<string> { }, ErrorServiceId);
				}
				else
				{
					ErrorService.Instance.ResolveError(4, ErrorServiceId);
				}

				// max call letters check
				if (sumOfPrizeLevels < maxCall)
				{
					ErrorServiceId = ErrorService.Instance.ReportError(5, new List<string> { }, ErrorServiceId);
				}
				else
				{
					ErrorService.Instance.ResolveError(5, ErrorServiceId);
				}

				// all unique prize levels
				if (allUnique)
				{
					ErrorService.Instance.ResolveError(6, ErrorServiceId);
				}
				else
				{
					ErrorServiceId = ErrorService.Instance.ReportError(6, new List<string>() { failedId1, failedId2 }, ErrorServiceId);
				}
			}
		}

		private void VerifyDivisions()
		{
			ErrorService.Instance.ResolveWarning(0, ErrorServiceId);
			if (_model.DivisionsModel.DivisionList != null)
			{
				bool allUnique = true;
				string failedDiv1 = "";
				string failedDiv2 = "";
				List<DivisionModel> uniqueElements = new List<DivisionModel>();
				int counter = 0;
				foreach (DivisionModel currentDiv in _model.DivisionsModel.DivisionList)
				{
					if (uniqueElements.Contains(currentDiv))
					{
						allUnique = false;
						failedDiv1 = (_model.DivisionsModel.DivisionList.IndexOf(currentDiv) + 1).ToString();
						failedDiv2 = (counter + 1).ToString();
					}
					else
					{
						uniqueElements.Add(currentDiv);
						counter++;
					}
				}

				if (allUnique)
				{
					ErrorService.Instance.ResolveWarning(0, ErrorServiceId);
				}
				else
				{
					ErrorService.Instance.ReportWarning(0, new List<string>() { failedDiv1, failedDiv2 }, ErrorServiceId);
				}
			}
		}


	}
}
