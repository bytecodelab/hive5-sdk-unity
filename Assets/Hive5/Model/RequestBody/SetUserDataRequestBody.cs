using System;
using System.Collections.Generic;

namespace Hive5.Model
{
	/// <summary>
	/// Key operator value.
	/// </summary>
	public class Condition
	{
		public string key { set; get; }
		public string @operator { set; get; }
		public string value { set; get; }
	}

	/// <summary>
	/// Key value command.
	/// </summary>
	public struct KeyValueCommand
	{
		public string key { set; get;}
		public string value { set; get;}
		public string command { set; get;}
	}

	/// <summary>
	/// Request body set user data.
	/// </summary>
	public class SetUserDataRequest
	{
		public List<Condition> condition { set; get; }
		public List<KeyValueCommand> data { set; get; }
	}
}

