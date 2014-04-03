using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// User data.
	/// </summary>
	public class UserData
	{
		public string key { set; get; }
		public string value { set; get; }

		public static Dictionary<string, string> Load(JsonData json)
		{
			var userData = new Dictionary<string, string>();
			
			if (json == null || json.IsObject == false)
				return userData;
			
			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				var completedTime = (string)json[key];
				userData.Add(key, completedTime);
			}
			
			return userData;
		}
	}
}
