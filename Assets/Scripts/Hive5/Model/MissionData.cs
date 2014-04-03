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
	public class MissionData
	{
		public static Dictionary<string, DateTime> Load(JsonData json)
		{
			var missions = new Dictionary<string, DateTime>();
			
			if (json == null || json.IsObject == false)
				return missions;
			
			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				var completedTime = Date.ParseDateTime((string)json[key]);
				missions.Add(key, completedTime);
			}
			
			return missions;
		}
	}
}
