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
	public class GetCompletedMissionsResponseBody : IResponseBody
	{
		public List<Mission> Missions { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<Mission> missions;

			try
			{
				missions = Mission.LoadList( json["completed_missions"] );
			}
			catch (KeyNotFoundException)
			{
				missions = null;
			}

			return new GetCompletedMissionsResponseBody() {
				Missions = missions
			};
		}

	}
}

