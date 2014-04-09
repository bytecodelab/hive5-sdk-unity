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
		public long rewardId { set; get; }
		public long mailId { set; get; }
		public List<Item> items { set; get; }

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
				rewardId = rewardId,
				mailId = mailId,
				items = items
			};
		}

	}
}

