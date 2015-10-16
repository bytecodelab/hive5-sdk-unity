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

			List<Friend> friends = new List<Friend>();

			try
			{
                for (int i = 0; i < json["friends"].Count; i++)
                {
                    var userJson = json["friends"][i]["user"];
                    friends.Add(new Friend()
                    {
                         id = (string)userJson["id"],
                         platform = (string)userJson["platform"]
                    });
                }

				
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

