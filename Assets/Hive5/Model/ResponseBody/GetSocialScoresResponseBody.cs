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
		public string LastPrizedAt { set; get; }
		public ResetInfo ResetInfo { set; get; }
		public MyScore MyLastScore { set; get; }
		public List<Score> Scores { set; get; }
		public long ScoresCount { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			return new GetSocialScoresResponseBody() {
				ResetInfo 		= ResetInfo.Load (json["reset_info"]),
				MyLastScore 	= MyScore.Load(json["my_last_score"]),
				Scores 			= Score.LoadList(json["scores"]),
				ScoresCount 	= (int)json["scores_count"]
			};
		}

	}
}

