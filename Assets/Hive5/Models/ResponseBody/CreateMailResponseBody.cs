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
	public class CreateMailResponseBody : IResponseBody
	{
		public string Id	{ set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static CreateMailResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			return new CreateMailResponseBody() {
				Id	=  (string)json ["id"]
			};
		}

	}
}

