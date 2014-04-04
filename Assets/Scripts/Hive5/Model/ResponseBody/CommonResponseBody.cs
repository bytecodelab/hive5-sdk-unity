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
	public class CommonResponseBody : IResponseBody
	{
		public int 		resultCode { set; get; }
		public string 	resultMessage { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;
			
			var resultCode 	= (int)json["result_code"];
			string resultMessage;

			try
			{
				resultMessage	= (string)json["result_message"];
			}
			catch (KeyNotFoundException )
			{
				resultMessage	= null;
			}

			return new SetUserDataResponseBody() {
				resultCode 		= resultCode,
				resultMessage 	= resultMessage
			};
		}

	}
}

