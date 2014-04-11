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
		public long scoresCount { set; get; }
		public long value { set; get; }
		public long rank { set; get; }

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
				scoresCount = scoresCount,
				value = value,
				rank = value,
			};
		}

	}
}

