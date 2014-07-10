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

			int scoresCount;
			long value;
			long rank;

            try
            {
                scoresCount = (int)json["scores_count"];
            }
            catch(KeyNotFoundException)
            {
                scoresCount = 0;
            }

			try
			{
				value = (int)json["value"];
			}
			catch (KeyNotFoundException)
			{
				value = -1;
			}

			try
			{
				rank = (int)json["rank"];
			}
			catch (KeyNotFoundException)
			{
				rank = -1;
			}

			return new GetMyScoreResponseBody() {
				ScoresCount = scoresCount,
				Value = value,
				Rank = rank,
			};
		}

	}
}

