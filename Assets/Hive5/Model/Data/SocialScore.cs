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
	public class SocialScore
	{
		public string platformUserId { set; get; }
		public DateTime? scoredAt { set; get; }
		public long? value { set; get; }
		public long? rank { set; get; }
		public List<HObject> objects { set; get; }


		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static SocialScore Load(JsonData json)
		{
            long? value = 0;
            long? rank = 0;
            DateTime? scoredAt;
            List<HObject> objects;

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

            try
            {
                objects = HObject.LoadList(json["objects"]);
            }
            catch
            {
                objects = new List<HObject>();
            }



			if (json ["scored_at"] != null) {
				return new SocialScore () {
					platformUserId = (string)json["platform_user_id"],
					value = value,
					rank = rank,
					scoredAt = scoredAt,
                    objects = objects,
				};
			} 
			else 
			{
				return new SocialScore();
			}


		}


		public static List<SocialScore> LoadList(JsonData json)
		{
			var scores = new List<SocialScore> ();


			var scoresCount = json.Count;
			for (int currentCount = 0; currentCount < scoresCount; currentCount++) 
			{
				scores.Add(SocialScore.Load(json[currentCount]));
			}


			return scores;
		}
	}
}
