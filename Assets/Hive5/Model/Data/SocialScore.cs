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
        public User User { get; set; }
        public long? value { set; get; }
        public long? rank { set; get; }
        public List<HObject> objects { set; get; }
        public string extras { get; set; }

        /// <summary>
        /// Load the specified json.
        /// </summary>
        /// <param name="json">Json.</param>
        public static SocialScore Load(JsonData json)
        {
            long? value = 0;
            long? rank = 0;
            List<HObject> objects;
            string extrasJson = "";

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
                extrasJson = (string)json["extras"];
            }
            catch { }

            try
            {
                objects = HObject.LoadList(json["objects"]);
            }
            catch
            {
                objects = new List<HObject>();
            }

            return new SocialScore()
            {
                User = new User()
                {
                    platform = (string)json["user"]["platform"],
                    id = (string)json["user"]["id"],
                },
                value = value,
                rank = rank,
                extras = extrasJson,
                objects = objects,
            };
        }


        public static List<SocialScore> LoadList(JsonData json)
        {
            var scores = new List<SocialScore>();


            var scoresCount = json.Count;
            for (int currentCount = 0; currentCount < scoresCount; currentCount++)
            {
                scores.Add(SocialScore.Load(json[currentCount]));
            }


            return scores;
        }
    }
}
