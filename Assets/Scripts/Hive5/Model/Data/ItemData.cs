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
	public class ItemData
	{
		public string key;
		public int value;
		public bool? locked;
		public RechargeInfo regargeInfo;
		
		public ItemData(string key, int value, bool? locked, RechargeInfo rechargeInfo)
		{
			this.key = key;
			this.value = value;
			this.locked = locked;
			this.regargeInfo = rechargeInfo;
		}
		
		public static Dictionary<string, ItemData> Load(JsonData json)
		{
			var items = new Dictionary<string, ItemData>();
			
			if (json == null)
				return items;
			
			if (json.IsObject == false)
				throw new InvalidJsonException("invalid items");
			
			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				var value = (int)json[key]["value"];
				bool? locked = null;
				
				try
				{
					locked = (bool)json[key]["locked"];
				}
				catch (KeyNotFoundException )
				{
					locked = null;
				}
				
				RechargeInfo rechargeInfo = null;
				
				try
				{
					rechargeInfo = RechargeInfo.Load((JsonData)json[key]["recharge_info"]);
				}
				catch (KeyNotFoundException )
				{
					rechargeInfo = null;
				}
				
				var item = new ItemData(key, value, locked, rechargeInfo);
				items.Add(key, item);
			}

			return items;
		}
	}

	/// <summary>
	/// Recharge info.
	/// </summary>
	public class RechargeInfo
	{
		public int max;
		public int rechargesInSec;
		public DateTime nextRechargesAt;
		
		public RechargeInfo(int max, int rechargesInSec, DateTime nextRechargesAt)
		{
			this.max = max;
			this.rechargesInSec = rechargesInSec;
			this.nextRechargesAt = nextRechargesAt;
		}
		
		public static RechargeInfo Load(JsonData json)
		{
			if (json == null)
				return null;
			
			var max = (int)json["max"];
			var rechargesInSec = (int)json["recharges_in_sec"];
			var nextRechargesAt = Date.ParseDateTime((string)json["next_recharges_at"]);
			
			return new RechargeInfo(max, rechargesInSec, nextRechargesAt);
		}
	}
}
