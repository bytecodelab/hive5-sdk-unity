using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Models
{

	/// <summary>
	/// Login data.
	/// </summary>
	public class ListSocialScoresResponseBody : IResponseBody
	{
		public string LastPrizedAt { set; get; }
		public List<Score> Scores { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<Score> scores;
			
			try
			{
				scores = Score.LoadList( json["scores"] );
			}
			catch (KeyNotFoundException)
			{
				scores = null;
			}

			return new ListSocialScoresResponseBody() {
				Scores 			= scores,
			};
		}

	}
}

