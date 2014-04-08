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
	public class Item
	{
		public string key { set; get; }
		public string value  { set; get; }
		public bool? locked  { set; get; }
		public Recharge recharge  { set; get; }

		public static Item Load(JsonData json)
		{
			var key = (string)json ["key"];
			var value 	= (string)json["value"];
			bool? locked = null;
			
			try
			{
				locked = (bool)json["locked"];
			}
			catch (KeyNotFoundException )
			{
				locked = null;
			}
			
			Recharge recharge = null;
			
			try
			{
				recharge = Recharge.Load((JsonData)json["recharge_info"]);
			}
			catch (KeyNotFoundException )
			{
				recharge = null;
			}
			
			return new Item() {
				key = key, 
				value = value, 
				locked = locked, 
				recharge = recharge
			};
		}
		
		public static List<Item> LoadList(JsonData json)
		{
			var items = new List<Item>();
			
			if (json == null)
				return items;

			if (json.IsObject == false)
				throw new InvalidJsonException("invalid items");
			
			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				var value = (string)json[key]["value"];
				bool? locked = null;
				
				try
				{
					locked = (bool)json[key]["locked"];
				}
				catch (KeyNotFoundException )
				{
					locked = null;
				}
				
				Recharge recharge = null;

				try
				{
					recharge = Recharge.Load((JsonData)json[key]["recharge_info"]);
				}
				catch (KeyNotFoundException )
				{
					recharge = null;
				}

				items.Add ( new Item() {
					key = key,
					value = value,
					locked = locked,
					recharge = recharge
				});
			}

			return items;
		}
	}

	/// <summary>
	/// Recharge info.
	/// </summary>
	public class Recharge
	{
		public int max { set; get; }
		public int rechargesInSec { set; get; }
		public DateTime nextRechargesAt { set; get; }

		public static Recharge Load(JsonData json)
		{
			if (json == null)
				return null;
			
			var max = (int)json["max"];
			var rechargesInSec = (int)json["recharges_in_sec"];
			var nextRechargesAt = Date.ParseDateTime((string)json["next_recharges_at"]);
			
			return new Recharge () {
				max = max,
				rechargesInSec = rechargesInSec,
				nextRechargesAt = nextRechargesAt
			};
		}
	}
}
