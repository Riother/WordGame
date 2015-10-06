using IGT.WordGameGenerator.Localization;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Serialization;

namespace IGT.WordGameGenerator.Services.Localization
{
	public class LocalizationHelpers
	{
		public static MainWindowLocalization LoadLocalization(string filename)
		{
			using (var reader = new StreamReader(filename))
			{
				var deSerializer = new XmlSerializer(typeof(MainWindowLocalization));
				return deSerializer.Deserialize(reader) as MainWindowLocalization;
			}
		}

		public static bool SaveLocalization(string filename, MainWindowLocalization localization)
		{
			using (var writer = new StreamWriter(filename, false))
			{
				var serializer = new XmlSerializer(typeof(MainWindowLocalization));
				serializer.Serialize(writer, localization);
				return true;
			}
		}
	}
}
