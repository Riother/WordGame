using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Localization.GameSetup
{
	/// <summary>
	/// Localization manager of call letters panel
	/// </summary>
	[Serializable]
	public class CallLettersUserControlLocalization : PanelLocalization
	{

		/// <summary>
		/// Maximum number of call letters
		/// </summary>
		public string MaxCallLetters
		{
			get { return _maxCallLetters; }
			set
			{
				_maxCallLetters = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("MaxCallLetters"));
			}
		}

		/// <summary>
		/// Minimum number of maximum call letters
		/// </summary>
		public string MinCallLetters
		{
			get { return _minCallLetters; }
			set
			{
				_minCallLetters = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("MinCallLetters"));
			}
		}

		public CallLettersUserControlLocalization()
		{
			_title = "Call Letters";
			_instructions = "Set up the number of minimum and maximum call letters.";
			_maxCallLetters = "Max Call Letters:";
			_minCallLetters = "Min Call Letters:";
		}
		private string _maxCallLetters;
		private string _minCallLetters;
	}
}
