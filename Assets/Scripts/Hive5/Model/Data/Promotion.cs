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
	public class Promotion
	{
		public int id { set; get; }
		public string name { set; get; }
		public string applyUrl { set; get; }
		public string displayUrl { set; get; }
		public int order { set; get; }

		public static Promotion Load(JsonData json)
		{
			return new Promotion () {
				id = (int)json["id"],
				name = (string)json["name"],
				applyUrl = (string)json["apply_url"],
				displayUrl = (string)json["display_url"],
				order = (int)json["order"]
			};
		}

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static List<Promotion> LoadList(JsonData json)
		{
			var promotions = new List<Promotion>();
			
			if (json == null || json.IsArray == false)
				return promotions;

			var promotionCount = json.Count;
			for (int currentCount = 0; currentCount < promotionCount; currentCount++) 
			{
				promotions.Add(Promotion.Load(json[currentCount]));
			}

			return promotions;
		}
	}
}
