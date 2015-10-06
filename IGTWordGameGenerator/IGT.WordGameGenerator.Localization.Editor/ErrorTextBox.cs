using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;

namespace IGT.WordGameGenerator.Localization.Editor
{
	public class ErrorTextBox : TextBox
	{
		public int Index { get; set; }
		public bool IsWarning { get; set; }
	}
}
