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
	public class GiftItemResponseBody : IResponseBody
	{
		public List<UserData> userData { set; get; }			

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<UserData> userData;

			try
			{
				userData = UserData.LoadList( json["data"] );
			}
			catch (KeyNotFoundException)
			{
				userData = null;
			}

			return new GiftItemResponseBody() {
				userData		= userData
			};
		}

	}
}

