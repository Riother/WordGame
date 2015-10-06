using System;
using System.Collections.Generic;
using System.IO;
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
using Microsoft.Win32;
using IGT.WordGameGenerator.Localization.GameSetup;
using IGT.WordGameGenerator.Common.GameSetup;
using IGT.WordGameGenerator.Common;

namespace IGTWordGameGenerator.GameSetup
{
	/// <summary>
	/// Interaction logic for WordListsUserControl.xaml
	/// </summary>
	public partial class WordListsUserControl : UserControl
	{
		private WordListsModel _model;
		/// <summary>
		/// Constructor for WordList GUI object
		/// </summary>
		public WordListsUserControl()
		{
			InitializeComponent();
			SetModel(new WordListsModel());
			SetLocalization(new WordListsUserControlLocalization());
		}

		/// <summary>
		/// Sets the model for the GUI object
		/// </summary>
		/// <param name="model"></param>
		public void SetModel(WordListsModel model)
		{
			_model = model ?? new WordListsModel();
			DataBind();
		}

		private void DataBind()
		{
			ValidWords.DataContext = _model;
			BannedWords.DataContext = _model;
		}

        /// <summary>
        /// Sets localization settings for the UI element
        /// </summary>
        /// <param name="localization">The localization object, representing a language</param>
		public void SetLocalization(WordListsUserControlLocalization localization)
		{
			_localization = localization ?? new WordListsUserControlLocalization();
			DataBindLocale();
			DataBind();
		}

		private void DataBindLocale()
		{
			DataContext = _localization;
		}

		private void Banned_Browse(object sender, RoutedEventArgs e)
		{
			var file = GetOpenFileName("Banned Words List");
			if (!string.IsNullOrEmpty(file))
			{
				_model.BannedWords = file;
			}
		}

		private void Word_Browse(object sender, RoutedEventArgs e)
		{
			var file = GetOpenFileName("Words List");
			if (!string.IsNullOrEmpty(file))
			{
				_model.ValidWords = file;
			}
		}

		private string GetOpenFileName(string title)
		{
			var openDialog = new OpenFileDialog();
			openDialog.Title = title;
			openDialog.FileName = "BannedList";
			openDialog.DefaultExt = ".txt";
			openDialog.Filter = "Text documents (.txt)|*.txt";
			bool? result = openDialog.ShowDialog();
			if (result.HasValue && result.Value)
			{
				return openDialog.FileName;
			}
			else return null;
		}
		private WordListsUserControlLocalization _localization;
	}
}
