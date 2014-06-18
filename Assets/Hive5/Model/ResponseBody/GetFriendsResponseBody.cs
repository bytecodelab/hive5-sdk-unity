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
	public class GetFriendsResponseBody : IResponseBody
	{
		public List<UserData> Friends { set; get; }			

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<UserData> friends;

			try
			{
				friends = Hive5.Model.UserData.LoadList( json["friends"] );
			}
			catch (KeyNotFoundException)
			{
				friends = null;
			}

			return new GetFriendsResponseBody() {
				Friends		= friends
			};
		}

	}
}

