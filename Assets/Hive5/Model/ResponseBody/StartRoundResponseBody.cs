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
	public class StartRoundResponseBody : IResponseBody
	{
		public long		Id { set; get; }
		public List<UserData> 	UserData { set; get; }			
		public List<Item> 		Items { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			var id = (long)json ["id"];
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

			return new StartRoundResponseBody() {
				UserData		= userData,
				Items			= items,
				Id				= id
			};
		}

	}
}

