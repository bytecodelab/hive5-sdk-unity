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
	public class AddMailTagsResponseBody : IResponseBody
	{
		public List<string> Tags { set; get; }			

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<string> tags = JsonMapper.ToObject<List<string>> (json ["tags"].ToJson());

			return new AddMailTagsResponseBody() {
				Tags = tags
			};
		}

	}
}

