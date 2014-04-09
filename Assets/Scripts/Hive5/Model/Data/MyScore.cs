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
	public class MyScore
	{
		public long value { set; get; }
		public DateTime scoredAt { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static MyScore Load(JsonData json)
		{
			return new MyScore () {
				value = (long)json["value"],
				scoredAt = Date.ParseDateTime((string)json["scored_at"])
			};
		
		}

		public static List<MyScore> LoadList(JsonData json)
		{
			var scores = new List<MyScore> ();

			var scoresCount = json.Count;
			for (int currentCount = 0; currentCount < scoresCount; currentCount++) 
			{
				scores.Add(MyScore.Load(json[currentCount]));
			}

			return scores;
		}
	}
}