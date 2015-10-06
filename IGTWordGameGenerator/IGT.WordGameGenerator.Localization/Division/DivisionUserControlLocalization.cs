using System;
using System.ComponentModel;

namespace IGT.WordGameGenerator.Localization.Division
{
	/// <summary>
	/// Localization manager of individual Division GUI object
	/// </summary>
	[Serializable]
	public class DivisionUserControlLocalization : INotifyPropertyChanged
	{
		/// <summary>
		/// Event manager for GUI object
		/// </summary>
		[field:NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

		/// <summary>
		/// PrizeLevel of the current division
		/// </summary>
		public string PrizeLevel
		{
			get
			{
				return _prizeLevel;
			}
			set
			{
				_prizeLevel = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevel"));
				}
			}
		}

		/// <summary>
		/// Multiplier amount of the current division
		/// </summary>
		public string Multiplier
		{
			get
			{
				return _multiplier;
			}
			set
			{
				_multiplier = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Multiplier"));
				}
			}
		}

		/// <summary>
		/// Total value of the division
		/// </summary>
		public string TotalValue
		{
			get
			{
				return _totalValue;
			}
			set
			{
				_totalValue = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("TotalValue"));
				}
			}
		}

		public string CloseButton
		{
			get { return _closeButton; }
			set
			{
				_closeButton = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("CloseButton"));
				}
			}
		}
		private string _prizeLevel = "Prize Level:";
		private string _multiplier = "Multiplier:";
		private string _totalValue = "Total Value: $";
		private string _closeButton = "Delete";
	}
}
