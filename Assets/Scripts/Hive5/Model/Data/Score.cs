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
		public DateTime scoredAt { set; get; }
		public long value { set; get; }
		public long rank { set; get; }
		public List<UserData> userData { set; get; }
		public List<Item> items { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static Score Load(JsonData json)
		{
			return new Score () {
				platformUserId = (string)json["platform_user_id"],
				value = ((int)json["value"]),
				rank = ((int)json["rank"]),
				userData = UserData.LoadList(json["user_data"]),
				scoredAt = Date.ParseDateTime((string)json["scored_at"])
			};
		
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