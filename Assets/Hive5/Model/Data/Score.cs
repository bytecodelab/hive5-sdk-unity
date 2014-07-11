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
		public long? value { set; get; }
		public long? rank { set; get; }
        public List<HObject> objects { get; set; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static Score Load(JsonData json)
		{
            long? value = 0;
            long? rank = 0;
            List<HObject> objects = null;

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
                objects = HObject.LoadList(json["objects"]);
            }
            catch
            {
                objects = null;
            }


			return new Score () {
				platformUserId = (string)json["platform_user_id"],
				value = value,
				rank = rank,
                objects = objects,
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