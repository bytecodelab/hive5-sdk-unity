using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Config data.
	/// </summary>
	public class Score
	{
		public string platformUserId { set; get; }
		public DateTime? scoredAt { set; get; }
		public long? value { set; get; }
		public long? rank { set; get; }
		public List<UserData> userData { set; get; }
		public List<Item> items { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static Score Load(JsonData json)
		{
            long? value = 0;
            long? rank = 0;
            DateTime? scoredAt;

            try
            {
                value = (int)json["value"];
            }
            catch
            {
                value = null;
            }

            try
            {
                rank = (int)json["rank"];
            }
            catch
            {
                rank = null;
            }

             try
            {
                scoredAt = Date.ParseDateTime((string)json["scored_at"]);
            }
            catch
            {
                scoredAt = null;
            }



			if (json ["scored_at"] != null) {
				return new Score () {
					platformUserId = (string)json["platform_user_id"],
					value = value,
					rank = rank,
					scoredAt = scoredAt,
				};
			} 
			else 
			{
				return new Score();
			}
		
		}

		public static List<Score> LoadList(JsonData json)
		{
			var scores = new List<Score> ();

			var scoresCount = json.Count;
			for (int currentCount = 0; currentCount < scoresCount; currentCount++) 
			{
				scores.Add(Score.Load(json[currentCount]));
			}

			return scores;
		}
	}
}