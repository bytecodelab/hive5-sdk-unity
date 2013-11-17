using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Hive5
{
	public class Util
	{
		public static DateTime ParseDateTime(string dateTimeString)
		{
			string format = "yyyy-MM-dd'T'HH:mm:ss";
			return DateTime.ParseExact(dateTimeString, format, CultureInfo.InvariantCulture);
		}
	}
}
