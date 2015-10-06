using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IGTWordGameGenerator.Converters
{
    /// <summary>
    /// Converts textbox text values into an actual value
    /// </summary>
	public class StringToNumberConverter : IValueConverter
	{
        /// <summary>
        /// Converts string into a double
        /// </summary>
        /// <param name="value">The string to be converted</param>
        /// <param name="targetType">The desired type to be converted to (double)</param>
        /// <param name="parameter">Optional thing that won't be used</param>
        /// <param name="culture">Specification to user location</param>
        /// <returns>Double result from string if convertable, else returns the string</returns>
		public object Convert(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			object toReturn = value;
			if(targetType.Equals(Type.GetType("double")))
			{
				double doubleValue;
				double.TryParse(value.ToString(), out doubleValue);
				toReturn = doubleValue;
			}
			return toReturn;
		}

        /// <summary>
        /// Converts double into a string
        /// </summary>
        /// <param name="value">The double to be converted</param>
        /// <param name="targetType">The desired type to be converted to (string)</param>
        /// <param name="parameter">Optional thing that won't be used</param>
        /// <param name="culture">Specification to user location</param>
        /// <returns>Returns string of double if convertable, else returns the double</returns>
		public object ConvertBack(object value, Type targetType, object parameter, System.Globalization.CultureInfo culture)
		{
			object toReturn = value;
			if (targetType.Equals(Type.GetType("string")))
			{
				toReturn = value.ToString();
			}
			return toReturn;
		}
	}
}
