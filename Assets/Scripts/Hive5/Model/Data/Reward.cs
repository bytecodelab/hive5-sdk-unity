using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Promotion data.
	/// </summary>
	public class Reward
	{
		public string key { set; get; }
		public long value  { set; get; }
		public CommandType command { set; get; }

		public static Reward Load(JsonData json)
		{
			return new Reward () {
				key = (string)json["key"],
				value = (long)json["value"],
				command = Tool.StringToCommand((string)json["command"]),
			};
		}

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static List<Reward> LoadList(JsonData json)
		{
			var rewards = new List<Reward>();
			
			if (json == null || json.IsArray == false)
				return rewards;

			var listCount = json.Count;
			for (int currentCount = 0; currentCount < listCount; currentCount++) 
			{
				rewards.Add(Reward.Load(json[currentCount]));
			}

			return rewards;
		}
	}
}
