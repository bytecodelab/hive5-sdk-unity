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
	public class CreateGooglePurchaseResponseBody : IResponseBody
	{
		public string Id { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			return new CreateGooglePurchaseResponseBody() {
				Id =  (string)json["id"],
			};
		}

	}
}

