using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Models
{

	/// <summary>
	/// Remove Friends
	/// </summary>
	public class RemoveFriendsResponseBody : IResponseBody
	{	

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;


			return new RemoveFriendsResponseBody() {

			};
		}

	}
}

