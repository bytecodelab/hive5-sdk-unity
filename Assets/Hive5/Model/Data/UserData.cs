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

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static UserData Load(JsonData json)
		{
			return new UserData () {
				key = (string)json["key"],
				value = (string)json["value"]
			};
		}

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static List<UserData> LoadList(JsonData json)
		{
			var userData = new List<UserData>();
			
			if (json == null || json.IsObject == false)
				return userData;
			
			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				JsonData jsonObj = new JsonData();
				jsonObj["key"] 		= key;
				jsonObj["value"] 	= (string)json[key]; 
				userData.Add (UserData.Load (jsonObj));
			}
			
			return userData;
		}
	}
}
