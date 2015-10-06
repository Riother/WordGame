using System;
using System.ComponentModel;

namespace IGT.WordGameGenerator.Localization.Division
{
	/// <summary>
	/// Localization manager for Divisions Panel
	/// </summary>
	[Serializable]
	public class DivisionsPanelUserControlLocalization : PanelLocalization
	{
		public string AddButton
		{
			get { return _addButton; }
			set
			{
				_addButton = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("AddButton"));
			}
		}
		public string NumberOfDivisions
		{
			get { return _numberOfDivisions; }
			set
			{
				_numberOfDivisions = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("NumberOfDivisions"));
			}
		}

		public DivisionsPanelUserControlLocalization()
		{
			_title = "Divisions";
			_instructions = "The list of divisions";
			_addButton = "Add Division";
			_numberOfDivisions = "Number of Divisions:";
			_division = new DivisionUserControlLocalization();
		}


		public DivisionUserControlLocalization Division
		{
			get { return _division; }
			set
			{
				_division = value ?? new DivisionUserControlLocalization();
				SendPropertyChanged(this, new PropertyChangedEventArgs("Division"));
			}
		}
		private string _addButton;
		private string _numberOfDivisions;
		private DivisionUserControlLocalization _division;
	}
}
