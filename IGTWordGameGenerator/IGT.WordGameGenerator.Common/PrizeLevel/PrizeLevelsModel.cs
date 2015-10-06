using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace IGT.WordGameGenerator.Common.PrizeLevel
{
	/// <summary>
	/// The prize levels model containing the list of prize levels
	/// </summary>
	[Serializable]
	public class PrizeLevelsModel : INotifyPropertyChanged, INotifyValidationRequired
	{
		/// <summary>
		/// The property changed event
		/// </summary>
		[field: NonSerialized()]
		public event PropertyChangedEventHandler PropertyChanged;
		[field: NonSerialized()]
		public event ValidationRequested ValidationRequest;
		private bool _randomize;

		public PrizeLevelsModel()
		{
			_prizeLevelList = new List<PrizeLevelModel>();
		}
		public void SetValidator(ValidationRequested validator)
		{
			ValidationRequest += validator;
			if (ValidationRequest == null) return;
			foreach(var i in _prizeLevelList)
			{
				i.SetValidator(validator);
			}
		}

		/// <summary>
		/// The maximum prize levels
		/// </summary>
		public int MaxPrizeLevels { get { return 10; } }

		/// <summary>
		/// The current number of prize levels
		/// </summary>
		public int NumberOfPrizeLevels { get { return _prizeLevelList == null ? 0 : _prizeLevelList.Count; } }

		/// <summary>
		/// Add a new prize level
		/// </summary>
		/// <param name="model">The prize level model</param>
		/// <returns></returns>
		public bool AddPrizeLevel(PrizeLevelModel model)
		{
			if ((_prizeLevelList ?? (_prizeLevelList = new List<PrizeLevelModel>())).Count < MaxPrizeLevels)
			{
                model.Index = _prizeLevelList.Count;
				if (ValidationRequest != null)
				{
					foreach (var i in ValidationRequest.GetInvocationList())
					{
						model.SetValidator(i as ValidationRequested);
					}
				}
				_prizeLevelList.Add(model);
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("NumberOfPrizeLevels"));
					PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevelList"));
					PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevelList2"));
				}
				return true;
			}
			else return false;
		}

		/// <summary>
		/// Remove a prize level
		/// </summary>
		/// <param name="model">The prize level to remove</param>
		/// <returns></returns>
		public bool RemovePrizeLevel(PrizeLevelModel model)
		{
			bool toReturn = _prizeLevelList == null ? false : _prizeLevelList.Remove(model);
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("NumberOfPrizeLevels"));
				PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevelList"));
				PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevelList2"));
			}
			return toReturn;
		}

		/// <summary>
		/// Gets index of PrizeLevelModel instance
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public int IndexOfPrizeLevel(PrizeLevelModel model)
		{
			return _prizeLevelList == null ? -1 : _prizeLevelList.IndexOf(model);
		}

		/// <summary>
		/// Reorders the list
		/// </summary>
		public void Reorder()
		{
			if (_prizeLevelList != null) _prizeLevelList.Sort();
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevelList"));
				PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevelList2"));
			}
		}

		public bool Randomize
		{
			get { return _randomize; }
			set
			{
				_randomize = value;
                ValidationRequest();
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ChooseUniqueLetters"));
					PropertyChanged(this, new PropertyChangedEventArgs("Randomize"));
				}
			}
		}

		public bool ChooseUniqueLetters
		{
			get { return !_randomize; }
			set
			{
				_randomize = !value;
                ValidationRequest();
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("ChooseUniqueLetters"));
					PropertyChanged(this, new PropertyChangedEventArgs("Randomize"));
				}
			}
		}

		/// <summary>
		/// The list of prize levels in the prize level panel
		/// </summary>
		public IList<PrizeLevelModel> PrizeLevelList { get { return _prizeLevelList == null ? null : _prizeLevelList.AsReadOnly(); } }
		private List<PrizeLevelModel> _prizeLevelList;
	}
}