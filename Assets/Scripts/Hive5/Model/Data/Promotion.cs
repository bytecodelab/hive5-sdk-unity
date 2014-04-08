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
			var id 			= (int)json["id"];
			var name 		= (string)json["name"];
			var applyUrl 	= (string)json["apply_url"];
			var displayUrl 	= (string)json["display_url"];
			var order 		= (int)json["order"];

			return new Promotion () {
				id = id,
				name = name,
				applyUrl = applyUrl,
				displayUrl = displayUrl,
				order = order
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
				var id 			= (int)json[currentCount]["id"];
				var name 		= (string)json[currentCount]["name"];
				var applyUrl 	= (string)json[currentCount]["apply_url"];
				var displayUrl 	= (string)json[currentCount]["display_url"];
				var order 		= (int)json[currentCount]["order"];

				promotions.Add(Promotion.Load(json[currentCount]));
			}

			return promotions;
		}
	}
}
