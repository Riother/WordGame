using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Common.GameSetup
{
    /// <summary>
    /// Model to hold file paths to word files
    /// </summary>
	[Serializable]
	public class WordListsModel : INotifyPropertyChanged, INotifyValidationRequired
	{
        /// <summary>
        /// Event manager
        /// </summary>
		[field:NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;
		[field: NonSerialized]
		public event ValidationRequested ValidationRequest;

		public void SetValidator(ValidationRequested validator)
		{
			ValidationRequest += validator;
		}

		private string _bannedWords;
        private string _validWords;

        /// <summary>
        /// The path to the Banned Words file
        /// </summary>
		public string BannedWords
		{
			get { return _bannedWords; }
			set
			{
				_bannedWords = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("BannedWords"));
				}
				if (ValidationRequest != null) ValidationRequest();
			}
		}

        /// <summary>
        /// The path to the Valid Words file
        /// </summary>
		public string ValidWords
		{
			get { return _validWords; }
			set
			{
				_validWords = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ValidWords"));
				}
				if (ValidationRequest != null) ValidationRequest();
			}
		}
	}
}
