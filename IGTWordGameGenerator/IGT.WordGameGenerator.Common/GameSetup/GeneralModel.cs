using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Common.GameSetup
{
    /// <summary>
    /// Model containing the general settings of the project
    /// </summary>
	[Serializable]
	public class GeneralModel : INotifyPropertyChanged
	{
        /// <summary>
        /// Event manager for general model
        /// </summary>
		[field:NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;

        /// <summary>
        /// The number of displayed words in the generated games
        /// </summary>
		public int NumDisplayedWords
		{
			get { return _numDisplayedWords; }
			set
			{
				_numDisplayedWords = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("NumDisplayedWords"));
			}
		}
        
        /// <summary>
        /// The minimum number of displayed words
        /// </summary>
		public int MinNumDisplayedWords
		{
			get { return _minNumDisplayedWords; }
			set
			{
				_minNumDisplayedWords = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MinNumDisplayedWords"));
			}
		}

        /// <summary>
        /// The maximimum number of displayed words
        /// </summary>
		public int MaxNumDisplayedWords
		{
			get
			{
				return _maxNumDisplayedWords;
			}
			set
			{
				_maxNumDisplayedWords = value;
				if (PropertyChanged != null)
					PropertyChanged(this, new PropertyChangedEventArgs("MaxNumDisplayedWords"));
			}
		}

		private int _numDisplayedWords = 5;
		private int _minNumDisplayedWords = 3;
		private int _maxNumDisplayedWords = 8;
	}
}
