using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Mission data.
	/// </summary>
	public class Mission
	{
		public string key { set; get; }
		public DateTime completedTime { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static Mission Load(JsonData json)
		{
			return new Mission () {
				key = (string)json["key"],
				completedTime = Date.ParseDateTime((string)json["completed_at"])
			};
		}

		/// <summary>
		/// Loads the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <param name="json">Json.</param>
		public static List<Mission> LoadList(JsonData json)
		{
			var missions = new List<Mission>();
			
			if (json == null || json.IsObject == false)
				return missions;
			
			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				JsonData jsonObj = new JsonData();
				jsonObj["key"] = key;
				jsonObj["completed_at"] = (string)json[key];

				missions.Add (Mission.Load(jsonObj));
			}
			
			return missions;
		}
	}
}
