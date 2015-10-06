using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace IGT.WordGameGenerator.Localization
{
	[Serializable]
	public class MainLocalization : INotifyPropertyChanged
	{
		[field: NonSerialized()]
		public event PropertyChangedEventHandler PropertyChanged;

		public MainLocalization()
		{
			_title = "Word Game Generator";
		}

		public string Title
		{
			get { return _title; }
			set
			{
				_title = value;
				SendPropertyChanged(this, new PropertyChangedEventArgs("Title"));
			}
		}

		protected void SendPropertyChanged(object sender, PropertyChangedEventArgs args)
		{
			if (PropertyChanged != null)
				PropertyChanged(sender, args);
		}

		protected string _title;
	}
}
