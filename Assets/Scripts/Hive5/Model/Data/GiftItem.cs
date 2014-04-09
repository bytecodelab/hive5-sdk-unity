using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Item data.
	/// </summary>
	public class GiftItem
	{
		public string key { set; get; }
		public DateTime afterAt { set; get; }

		public static GiftItem Load(JsonData json)
		{
			var key 	= (string)json ["key"];
			var afterAt = Date.ParseDateTime((string)json["after"]);

			return new GiftItem() {
				key = key, 
				afterAt = afterAt
			};
		}
		
		public static List<GiftItem> LoadList(JsonData json)
		{
			var items = new List<GiftItem>();
			
			if (json == null)
				return items;

			if (json.IsObject == false)
				throw new InvalidJsonException("invalid items");
			
			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				json[key]["key"] = key;
				items.Add ( GiftItem.Load(json[key]));
			}

			return items;
		}
	}
	
}
