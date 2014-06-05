using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{

	/// <summary>
	/// Login data.
	/// </summary>
	public class CompleteMissionResponseBody : IResponseBody
	{
		public long RewardId { set; get; }
		public long MailId { set; get; }
		public List<Item> Items { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			var rewardId = (long)json ["reward_id"];
			var mailId = (long)json ["mail_id"];

			List<Item> items;

			try
			{
				items = Item.LoadList( json["items"] );
			}
			catch (KeyNotFoundException)
			{
				items = null;
			}

			return new CompleteMissionResponseBody() {
				RewardId = rewardId,
				MailId = mailId,
				Items = items
			};
		}

	}
}

