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
	public class PrizeResponseBody : IResponseBody
	{
		public Prize Prized { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			Prize prized;

			try
			{
				prized = Prize.Load (json ["prized"]);
			}
			catch (KeyNotFoundException)
			{
				prized = null;
			}

			return new PrizeResponseBody() {
				Prized = prized
			};
		}

	}
}

