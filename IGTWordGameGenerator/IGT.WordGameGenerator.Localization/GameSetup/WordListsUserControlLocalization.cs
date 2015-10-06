using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Localization.GameSetup
{
	[Serializable]
	public class WordListsUserControlLocalization : PanelLocalization
	{
		public string ValidWords
		{
			get { return _validWords; }
			set
			{
				_validWords = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("ValidWords"));
			}
		}
		public string BannedWords
		{
			get { return _bannedWords; }
			set
			{
				_bannedWords = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("BannedWords"));
			}
		}
		public string Browse
		{
			get { return _browse; }
			set
			{
				_browse = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("Browse"));
			}
		}
		public WordListsUserControlLocalization()
		{
			_title = "Word Lists";
			_instructions = "The file locations for the list of words.";
			_validWords = "Valid Words List:";
			_bannedWords = "Banned Words List:";
			_browse = "Browse…";
		}
		private string _validWords;
		private string _bannedWords;
		private string _browse;
	}
}
