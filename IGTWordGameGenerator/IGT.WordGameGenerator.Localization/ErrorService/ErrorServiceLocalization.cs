using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Localization.ErrorService
{
	[Serializable]
	public class ErrorServiceLocalization : INotifyPropertyChanged
	{
		[field:NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;
		public string[] ErrorMessages
		{
			get { return _errorMessages; }
			set
			{
				_errorMessages = value;

				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ErrorMessages"));
				}
			}
		}

		public string[] WarningMessages
		{
			get { return _warningMessages; }
			set
			{
				_warningMessages = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("WarningMessages"));
				}
			}
		}

		public ErrorServiceLocalization()
		{
			ErrorMessages = new string[]
			{
			"The Valid Words file does not exist!",
			"The Banned Words file does not exist!",
			"The word {0} exceeds the maximum word length!",
			"The number of displayed words needs to be less than or equal to the number of prize levels",
			"The minimum number of call letters must be greater than or equal the smallest unique letter count in Prize Levels",
			"The maximum number of call letters must be less than the total sum of Prize Levels unique letter count",
			"Prize Levels {0} and {1} have the same unique letter count"
			};

			WarningMessages = new string[]
			{
			"The Divisions {0} and {1} are duplicates",
			"The PrizeLevels {0} and {1} are duplicates"
			};
		}

		private string[] _errorMessages;
		private string[] _warningMessages;
	}
}