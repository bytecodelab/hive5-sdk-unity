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
	public class GetMailCountResponseBody : IResponseBody
	{
		public int count { set; get; }		

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			var count = (int)json ["count"];

			return new GetMailCountResponseBody() {
				count = count
			};
		}

	}
}

