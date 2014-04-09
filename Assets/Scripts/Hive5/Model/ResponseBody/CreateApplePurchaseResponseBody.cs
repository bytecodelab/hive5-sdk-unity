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
	public class CreateApplePurchaseResponseBody : IResponseBody
	{
		public long		id { set; get; }
		
		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;
			
			long id = (long)json["id"];
			
			return new CreateApplePurchaseResponseBody() {
				id = id
			};
		}
		
	}
}

