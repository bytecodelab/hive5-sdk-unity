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
	public class GetScoresResponseBody : IResponseBody
	{
		public long ScoresCount { set; get; }
		public MyScore MyLastScore { set; get; }
		public List<Score> Scores { set; get; }			

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			var scoresCount = (int)json ["scores_count"];
			var myLastScore = MyScore.Load (json ["my_last_score"]);
			List<Score> scores;

			try
			{
				scores = Score.LoadList( json["scores"] );
			}
			catch (KeyNotFoundException)
			{
				scores = null;
			}

			return new GetScoresResponseBody() {
				ScoresCount = scoresCount,
				MyLastScore = myLastScore,
				Scores = scores
			};
		}

	}
}

