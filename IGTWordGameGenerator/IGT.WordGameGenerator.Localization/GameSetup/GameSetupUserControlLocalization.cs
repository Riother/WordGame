using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Localization.GameSetup
{
	/// <summary>
	/// Game set-up panel localization manager
	/// </summary>
	[Serializable]
	public class GameSetupUserControlLocalization : PanelLocalization
	{

		public string Generate
		{
			get { return _generate; }
			set
			{
				_generate = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("Generate"));
			}
		}
		public GameSetupUserControlLocalization()
		{
			_title = "Game Setup";
			_instructions = "Setup game properties";
			_generate = "Generate";
			_callLetters = new CallLettersUserControlLocalization();
			_wordLists = new WordListsUserControlLocalization();
		}

		public CallLettersUserControlLocalization CallLetters
		{
			get { return _callLetters; }
			set
			{
				_callLetters = value ?? new CallLettersUserControlLocalization();
				SendPropertyChanged(this, new PropertyChangedEventArgs("CallLetters"));
            }
		}

		public WordListsUserControlLocalization WordLists
		{
			get { return _wordLists; }
			set
			{
				_wordLists = value ?? new WordListsUserControlLocalization();
				SendPropertyChanged(this, new PropertyChangedEventArgs("WordLists"));
			}
		}

		private string _generate;
		private CallLettersUserControlLocalization _callLetters;
		private WordListsUserControlLocalization _wordLists;
	}
}
