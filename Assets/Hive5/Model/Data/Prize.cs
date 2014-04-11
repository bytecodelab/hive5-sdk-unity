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
	public class Prize
	{
		public DateTime seasonFrom { set; get; }
		public DateTime seasonTo { set; get; }
		public long score { set; get; }
		public long rank { set; get; }
		public long scoresCount { set; get; }
		public List<Score> topScores { set; get; }
		public PrizedReward reward { set; get; }
		public DateTime prizedAt { set; get; }

		public static Prize Load(JsonData json)
		{
			return new Prize () {
				seasonFrom 	= Date.ParseDateTime((string)json["season_from"]),
				seasonTo 	= Date.ParseDateTime((string)json["season_to"]),
				prizedAt 	= Date.ParseDateTime((string)json["prized_at"]),
				score 	= (long)json["score"],
				rank 	= (long)json["rank"],
				scoresCount = (long)json["scores_count"],
				topScores 	= Score.LoadList(json["top_scores"]),
				reward 		= PrizedReward.Load (json["reward"])

			};
		}

	}
}
