using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Mission data.
	/// </summary>
	public class CompletedMission
	{
		public string key { set; get; }
		public long rewardId { set; get; }
		public long mailId { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static CompletedMission Load(JsonData json)
		{
			return new CompletedMission () {
				key = (string)json["key"],
				rewardId = (long)json["reward_id"],
				mailId = (long)json["mail_id"]
			};
		}

		/// <summary>
		/// Loads the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <param name="json">Json.</param>
		public static List<CompletedMission> LoadList(JsonData json)
		{
			var completedMissions = new List<CompletedMission>();
			
			if (json == null || json.IsObject == false)
				return completedMissions;
			
			var listCount = json.Count;
			for (int currentCount = 0; currentCount < listCount; currentCount++) 
			{
				completedMissions.Add(CompletedMission.Load(json[currentCount]));
			}
			
			return completedMissions;
		}
	}
}
