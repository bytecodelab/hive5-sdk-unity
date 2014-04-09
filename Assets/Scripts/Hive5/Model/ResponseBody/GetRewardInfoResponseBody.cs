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
	public class GetRewardInfoResponseBody : IResponseBody
	{
		public List<Reward> rewards { set; get; }
		public bool valid { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<Reward> rewards;
			var valid = (bool)json ["valid"];

			try
			{
				rewards = Reward.LoadList( json["changes"] );
			}
			catch (KeyNotFoundException)
			{
				rewards = null;
			}

			return new GetRewardInfoResponseBody() {
				valid = valid,
				rewards = rewards
			};
		}

	}
}

