using IGT.WordGameGenerator.Common.PrizeLevel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Common.Division
{
	/// <summary>
	/// Data containing the divisions information of the current project
	/// </summary>
	[Serializable]
	public class DivisionsModel : INotifyPropertyChanged, INotifyValidationRequired
	{
		/// <summary>
		/// Event manager for changed properties
		/// </summary>
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;
		[field: NonSerialized]
		public event ValidationRequested ValidationRequest;

		public DivisionsModel()
		{
			_divisionList = new List<DivisionModel>();
		}
		public void SetValidator(ValidationRequested validator)
		{
			ValidationRequest += validator;
			if (ValidationRequest == null) return;
			foreach (var i in _divisionList)
			{
				i.SetValidator(validator);
			}
		}

		public int NumPermutations
		{
			get { return _numPermutations; }
			set
			{
				_numPermutations = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("NumPermutations"));
				}
				if (ValidationRequest != null) ValidationRequest();
			}
		}
		/// <summary>
		/// The maximum number of divisions a project can have
		/// </summary>
		public int MaxDivisions { get { return 10; } }
		/// <summary>
		/// The current number of Divisions
		/// </summary>
		public int NumberOfDivisions { get { return _divisionList == null ? 0 : _divisionList.Count; } }

		/// <summary>
		/// Adds a division to the model
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool AddDivision(DivisionModel model)
		{
			if ((_divisionList ?? (_divisionList = new List<DivisionModel>())).Count < MaxDivisions)
			{
				if (ValidationRequest != null)
				{
					foreach(var i in ValidationRequest.GetInvocationList())
					{
						model.SetValidator(i as ValidationRequested);
					}
				}
				_divisionList.Add(model);
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("DivisionList"));
					PropertyChanged(this, new PropertyChangedEventArgs("NumberOfDivisions"));
				}
				if (ValidationRequest != null) ValidationRequest();
				return true;
			}
			else return false;
		}

		/// <summary>
		/// Removes division from model
		/// </summary>
		/// <param name="model"></param>
		/// <returns></returns>
		public bool RemoveDivision(DivisionModel model)
		{
			bool toReturn = _divisionList == null ? false : _divisionList.Remove(model);
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("DivisionList"));
				PropertyChanged(this, new PropertyChangedEventArgs("NumberOfDivisions"));
			}
            if (ValidationRequest != null)
            {
                ValidationRequest();
            }
			return toReturn;
		}

		/// <summary>
		/// List of divisions
		/// </summary>
		public IList<DivisionModel> DivisionList
		{
			get
			{
				if (_divisionList == null)
				{
					return null;
				}
				return _divisionList.AsReadOnly();
			}
		}

		/// <summary>
		/// Holder for PrizeLevel references
		/// </summary>
		public IEnumerable<PrizeLevelModel> PrizeLevels
		{
			get
			{
				return _prizeLevels;
			}
			set
			{
				_prizeLevels = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevels"));
					if (_divisionList != null)
					{
						foreach (DivisionModel i in _divisionList)
						{
							i.PrizeLevels = _prizeLevels;
							i.PrizeLevel = _prizeLevels.ElementAtOrDefault(i.LastPrizeLevelIndex);
						}
					}
				}
			}
		}
		private List<DivisionModel> _divisionList;
		private IEnumerable<PrizeLevelModel> _prizeLevels;
		private int _numPermutations = 10;
	}
}
