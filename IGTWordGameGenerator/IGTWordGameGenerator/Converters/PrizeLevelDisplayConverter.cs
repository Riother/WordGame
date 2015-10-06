using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Data;

namespace IGTWordGameGenerator.Converters
{
    /// <summary>
    /// Converts prizelevel index into a letter
    /// </summary>
	public class PrizeLevelDisplayConverter : IValueConverter
	{
        /// <summary>
        /// Returns the character representation of the index
        /// </summary>
        /// <param name="value">The index</param>
        /// <param name="targetType">The desired type (char)</param>
        /// <param name="parameter">Optional field that won't be used</param>
        /// <param name="culture">Specification to user location</param>
        /// <returns>Character of index if convertable, or the index</returns>
        public object Convert(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
		{
			object toReturn = value;
			if (value is int && value != null)
			{
				toReturn = new string(new char[] { (char)('A' + (value as int?).Value) });
			}
			return toReturn;
		}

        /// <summary>
        /// Returns the integer representation of the index char
        /// </summary>
        /// <param name="value">The char being converted</param>
        /// <param name="targetType">The desired type (int)</param>
        /// <param name="parameter">Optional field that won't be used</param>
        /// <param name="culture">Specification to user location</param>
        /// <returns>Integer if convertable, else returns the char</returns>
        public object ConvertBack(object value, Type targetType = null, object parameter = null, CultureInfo culture = null)
		{
			object toReturn = value;
			if (targetType.Equals(Type.GetType("int")))
			{
				if (value is string && (value as string).Length > 0)
				{
					toReturn = (int)(value as string)[0];
				}
			}
			return toReturn;
		}
	}
}
