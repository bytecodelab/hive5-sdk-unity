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
	public class GetSocialScoresResponseBody : IResponseBody
	{
		public string lastPrizedAt { set; get; }
		public ResetInfo resetInfo { set; get; }
		public MyScore myLastScore { set; get; }
		public List<Score> scores { set; get; }
		public long scoresCount { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			return new GetSocialScoresResponseBody() {
				lastPrizedAt	= (string)json["last_prized_at"],
				resetInfo 		= ResetInfo.Load (json["reset_info"]),
				myLastScore 	= MyScore.Load(json["my_last_score"]),
				scores 			= Score.LoadList(json["scores"]),
				scoresCount 	= (long)json["scores_count"]
			};
		}

	}
}

