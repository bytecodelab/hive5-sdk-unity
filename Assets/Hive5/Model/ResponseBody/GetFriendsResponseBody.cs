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
	public class ListFriendsResponseBody : IResponseBody
	{
		public List<Friend> Friends { set; get; }			

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<Friend> friends;

			try
			{
				friends = JsonMapper.ToObject<List<Friend>>( json["friends"].ToJson());
			}
			catch (KeyNotFoundException)
			{
				friends = null;
			}

			return new ListFriendsResponseBody() {
				Friends = friends
			};
		}

	}
}

