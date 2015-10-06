using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Common.GameSetup
{
	/// <summary>
	/// The data of the CallLetters
	/// </summary>
	[Serializable]
	public class CallLettersModel : INotifyPropertyChanged, INotifyValidationRequired
	{
		/// <summary>
		/// Event manager
		/// </summary>
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;
		[field: NonSerialized]
		public event ValidationRequested ValidationRequest;

		public void SetValidator(ValidationRequested validator)
		{
			ValidationRequest += validator;
		}

		/// <summary>
		/// The maximum number of call letters the player will be able to pick
		/// </summary>
		public int MaxCallLetters
		{
			get
			{
				return _maxCallLetters;
			}
			set
			{
				_maxCallLetters = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("MaxCallLetters"));
				}
				if (ValidationRequest != null) ValidationRequest();
			}
		}

		/// <summary>
		/// The minimum number of call letters the player will be able to pick before being able to get a strike
		/// </summary>
		public int MinCallLetters
		{
			get
			{
				return _minCallLetters;
			}
			set
			{
				_minCallLetters = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("MinCallLetters"));
				}
				if (ValidationRequest != null) ValidationRequest();
			}
		}

		/// <summary>
		/// Minium number for max call letters.
		/// </summary>
		public int MinMaxCallLetters
		{
			get
			{
				return _minMaxCallLetters;
			}
			set
			{
				_minMaxCallLetters = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MinMaxCallLetters"));
			}
		}

		/// <summary>
		/// Maximum number for max call letters
		/// </summary>
		public int MaxMaxCallLetters
		{
			get
			{
				return _maxMaxCallLetters;
			}
			set
			{
				_maxMaxCallLetters = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MaxMaxCallLetters"));
			}
		}

		/// <summary>
		/// Minimum number for min call letters
		/// </summary>
		public int MinMinCallLetters
		{
			get
			{
				return _minMinCallLetters;
			}
			set
			{
				_minMinCallLetters = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MinMinCallLetters"));
			}
		}

		/// <summary>
		/// Maximum number for min call letters
		/// </summary>
		public int MaxMinCallLetters
		{
			get
			{
				return _maxMinCallLetters;
			}
			set
			{
				_maxMinCallLetters = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MaxMinCallLetters"));
			}
		}

		public int NumCallLettersPerRowInHistory
		{
			get
			{
				return _numCallLettersPerRowInHistory;
			}
			set
			{
				_numCallLettersPerRowInHistory = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("NumCallLettersPerRowInHistory"));
			}
		}

		private int _maxCallLetters = 1;
		private int _minCallLetters = 1;
		private int _minMaxCallLetters = 1;
		private int _maxMaxCallLetters = 10;
		private int _minMinCallLetters = 1;
		private int _maxMinCallLetters = 10;
		private int _numCallLettersPerRowInHistory = 1;
	}
}
