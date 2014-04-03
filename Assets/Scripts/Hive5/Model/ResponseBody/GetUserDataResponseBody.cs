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
	public class GetUserDataResponseBody : IResponseBody
	{
		public int 		resultCode { set; get; }
		public string 	resultMessage { set; get; }
		public Dictionary<string, string> userData { set; get; }			

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
			Dictionary<string, string> userData;

			try
			{
				resultMessage	= (string)json["result_message"];
			}
			catch (KeyNotFoundException )
			{
				resultMessage	= null;
			}

			try
			{
				userData = UserData.Load( json["data"] );
			}
			catch (KeyNotFoundException)
			{
				userData = null;
			}

			return new GetUserDataResponseBody() {
				resultCode 		= resultCode,
				resultMessage 	= resultMessage,
				userData		= userData
			};
		}

	}
}

