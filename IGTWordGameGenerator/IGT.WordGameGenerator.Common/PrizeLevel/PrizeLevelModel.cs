using System;
using System.ComponentModel;

namespace IGT.WordGameGenerator.Common.PrizeLevel
{
	/// <summary>
	/// The prize level model
	/// </summary>
	[Serializable]
	public class PrizeLevelModel : INotifyPropertyChanged, IComparable<PrizeLevelModel>, INotifyValidationRequired
	{
		/// <summary>
		/// Property changed event
		/// </summary>
		[field: NonSerialized()]
		public event PropertyChangedEventHandler PropertyChanged;
		[field: NonSerialized()]
		public event ValidationRequested ValidationRequest;

		private PrizeLevelsModel _parent;
		private int _uniqueLetterCount = 3;
		private double _value;
		private int _index;
		

		/// <summary>
		/// Create a new prize level model
		/// </summary>
		/// <param name="parent">The parent of this model that contains this</param>
		public PrizeLevelModel(PrizeLevelsModel parent = null)
		{
			_parent = parent;
			if (_parent != null) _parent.PropertyChanged += ResetValue;
		}

		/// <summary>
		/// Sets project context for ErrorService management
		/// </summary>
		/// <param name="allModel">Project context data object</param>
		public void SetValidator(ValidationRequested validator)
		{
			ValidationRequest += validator;
		}

		/// <summary>
		/// Resets value of PrizeLevel
		/// </summary>
		/// <param name="sender">The GUI element that sent the event</param>
		/// <param name="e">The event itself</param>
		public void ResetValue(object sender, PropertyChangedEventArgs e)
		{
			if (_parent != null && e.PropertyName.Equals("PrizeLevelList")) Index = _parent.IndexOfPrizeLevel(this);
		}

		/// <summary>
		/// The prize value
		/// </summary>
		public double Value
		{
			get { return _value; }
			set
			{
				_value = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Value"));
				}
				Reorder();
			}
		}

		/// <summary>
		/// The unique letter count
		/// </summary>
		public int UniqueLetterCount
		{
			get { return _uniqueLetterCount; }
			set
			{
				if (value >= 3 && value <= 8)
				{
					_uniqueLetterCount = value;
					if (PropertyChanged != null)
					{
						PropertyChanged(this, new PropertyChangedEventArgs("UniqueLetterCount"));
					}
					if (ValidationRequest != null) ValidationRequest();
				}
			}
		}

		/// <summary>
		/// Reorders the parent
		/// </summary>
		public void Reorder()
		{
			if (_parent != null) _parent.Reorder();
		}

		/// <summary>
		/// Signal a delete to the parent
		/// </summary>
		/// <returns></returns>
		public bool Delete()
		{
			if (_parent != null)
			{
				_parent.RemovePrizeLevel(this);
			}
			return false;
		}

		/// <summary>
		/// Compares the current object with another object of the same type.
		/// </summary>
		/// <param name="other">An object to compare with this object.</param>
		/// <returns>A value that indicates the relative order of the objects being compared.
		///     The return value has the following meanings: Value Meaning Less than zero
		///     This object is less than the other parameter.Zero This object is equal to
		///     other. Greater than zero This object is greater than other.</returns>
		public int CompareTo(PrizeLevelModel other)
		{
			return -_value.CompareTo(other.Value);
		}

		/// <summary>
		/// Returns a string representation of the PrizeLevelModel
		/// </summary>
		/// <returns>String representation of PrizeLevelModel</returns>
		public override string ToString()
		{
			return new string(new char[] { (char)('A' + (int)Index) });
		}

		/// <summary>
		/// Index of PrizeLevel in the PrizeLevels panel
		/// </summary>
		public int Index
		{
			get { return _index; }
			set
			{
				_index = value;
				if (PropertyChanged != null)
				{
					PropertyChanged(this, new PropertyChangedEventArgs("Index"));
				}
			}
		}

		public override bool Equals(object obj)
		{
			if (obj as PrizeLevelModel != null)
			{
				return Index == (obj as PrizeLevelModel).Index;
			}
			return false;
		}

		public override int GetHashCode()
		{
			return base.GetHashCode();
		}
	}
}
