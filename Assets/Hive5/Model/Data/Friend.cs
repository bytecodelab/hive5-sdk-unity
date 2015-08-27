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
	public class Friend
	{
		public string platform { get; set; }
		public string id { set; get; }
		public List<HObject> objects { set; get; }

		public static Friend Load(JsonData json)
		{
			return new Friend () {
				platform = (string)json["platform"],
				id = (string)json["id"],
				objects = HObject.LoadList(json["objects"])
			};
		}

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static List<Friend> LoadList(JsonData json)
		{
			var friends = new List<Friend>();
			
			if (json == null || json.IsArray == false)
				return friends;

			var listCount = json.Count;
			for (int currentCount = 0; currentCount < listCount; currentCount++) 
			{
				friends.Add(Friend.Load(json[currentCount]));
			}

			return friends;
		}
	}
}
