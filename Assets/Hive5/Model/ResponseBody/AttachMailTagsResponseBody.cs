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
	public class AttachMailTagsResponseBody : IResponseBody
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

			return new AttachMailTagsResponseBody() {
				Tags = tags
			};
		}

	}
}

