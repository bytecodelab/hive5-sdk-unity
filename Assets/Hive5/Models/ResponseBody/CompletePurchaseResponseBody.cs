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
	public class CompletePurchaseResponseBody : IResponseBody
	{
		public string CallReturn { get; set; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			string callReturn = string.Empty;

			try
			{
				callReturn =  json["call_return"].ToJson();
			}
			catch (KeyNotFoundException)
			{
			}

			return new CompletePurchaseResponseBody() {
				CallReturn = callReturn,
			};
		}

	}
}

