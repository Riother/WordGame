using System;
using System.ComponentModel;

namespace IGT.WordGameGenerator.Localization.PrizeLevel
{
    /// <summary>
    /// Handles foreign languages support
    /// </summary>
	[Serializable]
	public class PrizeLevelUserControlLocalization : INotifyPropertyChanged
	{
        /// <summary>
        /// Event manager
        /// </summary>
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		private string _value = "Value: $";
        /// <summary>
        /// The display text of the prize level value
        /// </summary>
		public string Value
		{
			get { return _value; }
			set
			{
				_value = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("Value"));
			}
		}

		private string _uniqueLetterCount = "Unique Letter Count: ";
        /// <summary>
        /// The display text of the number of unique letters
        /// </summary>
		public string UniqueLetterCount
		{
			get { return _uniqueLetterCount; }
			set
			{
				_uniqueLetterCount = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("UniqueLetterCount"));
			}
		}

		private string _closeButton = "Delete";
        /// <summary>
        /// The display text of the delete button of the prize level
        /// </summary>
		public string CloseButton
		{
			get { return _closeButton; }
			set
			{
				_closeButton = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("CloseButton"));
			}
		}
	}
}
