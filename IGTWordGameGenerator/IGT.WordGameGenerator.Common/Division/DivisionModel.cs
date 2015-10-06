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
	/// Represents the Division paylevel
	/// </summary>
	[Serializable]
	public class DivisionModel : INotifyPropertyChanged, INotifyValidationRequired
	{
		/// <summary>
		/// Event manager
		/// </summary>
		[field: NonSerialized]
		public event PropertyChangedEventHandler PropertyChanged;
		[field: NonSerialized]
		public event ValidationRequested ValidationRequest;

		/// <summary>
		/// Constructor for Division paylevel
		/// </summary>
		/// <param name="parent"></param>
		public DivisionModel(DivisionsModel parent = null)
		{
			_parent = parent;
			LastPrizeLevelIndex = -1;
			PrizeLevel = PrizeLevels.First();
		}

		public void SetValidator(ValidationRequested validator)
		{
			ValidationRequest += validator;
		}

		/// <summary>
		/// Total value of the Division
		/// </summary>
		public double TotalValue { get { return PrizeLevel == null ? 0 : PrizeLevel.Value * Multiplier; } }

		/// <summary>
		/// The PrizeLevel of the division
		/// </summary>
		public PrizeLevelModel PrizeLevel
		{
			get
			{
				return _prizeLevel;
			}
			set
			{
				if (_prizeLevel != null) _prizeLevel.PropertyChanged -= UpdateValue;
				_prizeLevel = value;
				LastPrizeLevelIndex = _prizeLevel == null ? -1 : _prizeLevel.Index;
				if (_prizeLevel != null) _prizeLevel.PropertyChanged += UpdateValue;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevel"));
					PropertyChanged(this, new PropertyChangedEventArgs("TotalValue"));
				}
				if (ValidationRequest != null) ValidationRequest();
			}
		}

		/// <summary>
		/// The prize multiplier for the division
		/// </summary>
		public int Multiplier
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
					PropertyChanged(this, new PropertyChangedEventArgs("TotalValue"));
				}
				if (ValidationRequest != null) ValidationRequest();
			}
		}

		/// <summary>
		/// Signal a delete to the parent
		/// </summary>
		/// <returns></returns>
		public bool Delete()
		{
			return _parent == null ? false : _parent.RemoveDivision(this);
		}

		/// <summary>
		/// The PrizeLevels that are available for selection
		/// </summary>
		public IEnumerable<PrizeLevelModel> PrizeLevels
		{
			get
			{
				return _parent == null ? null : _parent.PrizeLevels;
			}
			set
			{
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevels"));
				}
			}
		}

		public int Permutations
		{
			get { return _permutations; }
			set
			{
				_permutations = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Permutations"));
				}
				if (ValidationRequest != null) ValidationRequest();
			}
		}

		/// <summary>
		/// Update value of Division
		/// </summary>
		/// <param name="sender">GUI element sending signal</param>
		/// <param name="e">Event manager object</param>
		public void UpdateValue(object sender, PropertyChangedEventArgs e)
		{
			if (PropertyChanged != null)
			{
				PropertyChanged(this, new PropertyChangedEventArgs("PrizeLevel"));
				PropertyChanged(this, new PropertyChangedEventArgs("TotalValue"));
			}
		}

		public override bool Equals(object obj)
		{
			if (obj as DivisionModel != null)
			{
				bool samePrizeLevel = PrizeLevel.Equals((obj as DivisionModel).PrizeLevel);
				bool sameMultiplier = Multiplier == (obj as DivisionModel).Multiplier;
				return samePrizeLevel && sameMultiplier;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}

		private DivisionsModel _parent;
		private PrizeLevelModel _prizeLevel;

		private int _multiplier = 1;
		private int _permutations = 1;
		public int LastPrizeLevelIndex { get; set; }
	}
}
