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
	public class PromotionData
	{
		public int id;
		public string name;
		public string applyUrl;
		public string displayUrl;
		public int order;


		public PromotionData(int id, string name, string applyUrl, string displayUrl, int order)
		{
			this.id 		= id;
			this.name 		= name;
			this.applyUrl 	= applyUrl;
			this.displayUrl = displayUrl;
			this.order 		= order;
		}


		public static List<PromotionData> Load(JsonData json)
		{
			var promotions = new List<PromotionData>();
			
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

				promotions.Add( new PromotionData(id, name, applyUrl, displayUrl, order));
			}

			return promotions;
		}
	}
}
