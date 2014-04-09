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
	public class PrizedReward
	{
		public long id { set; get; }
		public long mailId  { set; get; }
		public List<Reward> changes { set; get; }
		public DateTime prizedAt { set; get; }

		public static PrizedReward Load(JsonData json)
		{
			return new PrizedReward () {
				id 		= (long)json["id"],
				mailId 	= (long)json["mail_id"],
				changes = Reward.LoadList(json["changes"]),
				prizedAt= Date.ParseDateTime((string)json["prized_at"])
			};
		}

	}
}
