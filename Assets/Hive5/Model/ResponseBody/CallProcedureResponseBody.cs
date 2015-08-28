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
	public class RunScriptResponseBody : IResponseBody
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

            JsonData callReturnJson = json["call_return"];
			return new RunScriptResponseBody() {
				CallReturn = callReturnJson == null? null : (callReturnJson.IsObject ?  callReturnJson.ToJson() : (string)callReturnJson),
			};
		}

	}
}

