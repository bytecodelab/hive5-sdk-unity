using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Config data.
	/// </summary>
	public class ConfigData
	{
		public static Dictionary<string, string> Load(JsonData json)
		{
			var missions = new Dictionary<string, string>();
			
			if (json == null)
				return missions;
			
			if (json.IsObject == false)
				throw new InvalidJsonException("invalid configData");
			
			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				var configValue = (string)json[key];
				missions.Add(key, configValue);
			}
			
			return missions;
		}
	}
}
