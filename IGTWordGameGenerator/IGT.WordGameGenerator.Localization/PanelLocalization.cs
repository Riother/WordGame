using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Localization
{
	[Serializable]
	public class PanelLocalization : MainLocalization
	{
		public string Instructions
		{
			get { return _instructions; }
			set
			{
				_instructions = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("Instructions"));
			}
		}

		protected string _instructions;
	}
}
