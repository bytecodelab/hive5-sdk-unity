using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Hive5.Util;

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

	/// <summary>
	/// Util.
	/// </summary>
	public class Tool
	{
		/// <summary>
		/// Commands to string.
		/// </summary>
		/// <returns>The to string.</returns>
		/// <param name="command">Command.</param>
		public static string CommandToString(CommandType command)
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

		/// <summary>
		/// Strings to command.
		/// </summary>
		/// <returns>The to command.</returns>
		/// <param name="command">Command.</param>
		public static CommandType StringToCommand(string command)
		{
			CommandType returnCommand = CommandType.SET;

			switch(command)
			{
				case "sec":
					returnCommand = CommandType.SET;
					break;
				case "inc":
					returnCommand = CommandType.INC;
					break;
				case "dec":
					returnCommand = CommandType.DEC;
					break;
			}

			return returnCommand;

		}

		/// <summary>
		/// Orders to string.
		/// </summary>
		/// <returns>The to string.</returns>
		/// <param name="order">Order.</param>
		public static string OrderToString(OrderType order)
		{
			string orderString = "";
			
			switch(order)
			{
			case OrderType.ASC:
				orderString = "asc";
				break;
			case OrderType.DESC:
				orderString = "dec";
				break;
			}
			
			return orderString;
		}

		/// <summary>
		/// Strings to order.
		/// </summary>
		/// <returns>The to order.</returns>
		/// <param name="order">Order.</param>
		public static OrderType StringToOrder(string order)
		{
			OrderType returnOrder = OrderType.DESC;
			
			switch(order)
			{
				case "dec":
					returnOrder = OrderType.DESC;
					break;
				case "asc":
					returnOrder = OrderType.ASC;
					break;
			}
			
			return returnOrder;
			
		}

	}
	
}
