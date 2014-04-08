using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;

namespace Hive5.Util
{
	/// <summary>
	/// Date.
	/// </summary>
	public class Date
	{
		/// <summary>
		/// Parses the date time.
		/// </summary>
		/// <returns>The date time.</returns>
		/// <param name="dateTimeString">Date time string.</param>
		public static DateTime ParseDateTime(string dateTimeString)
		{
			string format = "yyyy-MM-dd'T'HH:mm:ss";
			return DateTime.ParseExact(dateTimeString, format, CultureInfo.InvariantCulture);
		}
	}

	/// <summary>
	/// Key value list.
	/// </summary>
	public class TupleList <TKey, TValue>  
	{
		public List<KeyValuePair<TKey, TValue>> data = new List<KeyValuePair<TKey, TValue>>();

		/// <summary>
		/// Add the specified key and value.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		public void Add(TKey key, TValue value)
		{
			if(value != null)
			{
				data.Add (new KeyValuePair<TKey, TValue> (key, value));
			}

		}
	}

	public class Util
	{
		public static string getStringByCommandType(CommandType command)
		{
			string commandString = "";

			switch(command)
			{
				case CommandType.SET:
					commandString = "set";
					break;
				case CommandType.INC:
					commandString = "inc";
					break;
				case CommandType.DEC:
					commandString = "dec";
					break;
			}

			return commandString;
		}

	}
	
}
