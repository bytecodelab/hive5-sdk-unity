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
	public class EndRoundResponseBody : IResponseBody
	{
		public List<UserData> UserData { set; get; }			
		public List<Item> Items { set; get; }
		public DateTime ScoreUpdatedAt { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			DateTime scoreUpdatedAt = new DateTime();

			List<UserData> userData;
			List<Item> items;

			try
			{
				userData = Hive5.Model.UserData.LoadList( json["user_data"] );
			}
			catch (KeyNotFoundException)
			{
				userData = null;
			}

			try
			{
				items = Item.LoadList( json["items"] );
			}
			catch (KeyNotFoundException)
			{
				items = null;
			}

			if(json["score_updated_at"] != null)
				scoreUpdatedAt = Date.ParseDateTime( (string)json["score_updated_at"] );

			return new EndRoundResponseBody() {
				UserData		= userData,
				Items 			= items,
				ScoreUpdatedAt 	= scoreUpdatedAt
			};
		}

	}
}

