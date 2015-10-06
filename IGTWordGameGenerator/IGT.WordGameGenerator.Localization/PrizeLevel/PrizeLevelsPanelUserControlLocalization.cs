using System;
using System.ComponentModel;

namespace IGT.WordGameGenerator.Localization.PrizeLevel
{
	/// <summary>
	/// Localization for prize levels panel
	/// </summary>
	[Serializable]
	public class PrizeLevelsPanelUserControlLocalization : PanelLocalization
	{
		/// <summary>
		/// The add button text
		/// </summary>
		public string AddButton
		{
			get { return _addButton; }
			set
			{
				_addButton = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("AddButton"));
			}
		}

		/// <summary>
		/// The number of prize levels label
		/// </summary>
		public string NumberOfPrizeLevels
		{
			get { return _numberOfPrizeLevels; }
			set
			{
				_numberOfPrizeLevels = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("NumberOfPrizeLevels"));
			}
		}

		public PrizeLevelsPanelUserControlLocalization()
		{
			_title = "Prize Levels";
			_instructions = "The list of prize levels.";
			_addButton = "Add Prize Level";
			_numberOfPrizeLevels = "Number of Prize Levels:";
			_chooseUniqueLetters = "Choose # of Unique Letters";
			_randomize = "Randomize";
            _prizeLevel = new PrizeLevelUserControlLocalization();
		}
		public PrizeLevelUserControlLocalization PrizeLevel
		{
			get { return _prizeLevel; }
			set
			{
				_prizeLevel = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("PrizeLevel"));
			}
		}
		public string ChooseUniqueLetters
		{
			get { return _chooseUniqueLetters; }
			set
			{
				_chooseUniqueLetters = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("ChooseUniqueLetters"));
			}
		}

		public string Randomize
		{
			get { return _randomize; }
			set
			{
				_randomize = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("Randomize"));
			}
		}
		private string _addButton;
		private string _numberOfPrizeLevels;
		private string _chooseUniqueLetters;
		private string _randomize;
		private PrizeLevelUserControlLocalization _prizeLevel;
	}
}
