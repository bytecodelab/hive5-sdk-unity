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
	public class BatchCompleteMissionResponseBody : IResponseBody
	{
		public List<CompletedMission> complatedMissions { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<CompletedMission> completedMissions;
			List<Item> items;


			try
			{
				completedMissions = CompletedMission.LoadList( json["completed"] );
			}
			catch (KeyNotFoundException)
			{
				completedMissions = null;
			}

			return new BatchCompleteMissionResponseBody() {
				complatedMissions = completedMissions
			};
		}

	}
}

