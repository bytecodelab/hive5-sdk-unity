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
	public class GetMyScoreResponseBody : IResponseBody
	{
		public long ScoresCount { set; get; }
		public long Value { set; get; }
		public long Rank { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			var scoresCount = (long)json ["scores_count"];
			long value;
			long rank;

			try
			{
				value = (long)json["value"];
			}
			catch (KeyNotFoundException)
			{
				value = -1;
			}

			try
			{
				rank = (long)json["rank"];
			}
			catch (KeyNotFoundException)
			{
				rank = -1;
			}

			return new GetMyScoreResponseBody() {
				ScoresCount = scoresCount,
				Value = value,
				Rank = value,
			};
		}

	}
}

