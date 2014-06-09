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
	public class SwitchPlatformResponseBody : IResponseBody
	{
		public int 		ResultCode;
		public string 	ResultMessage;

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static SwitchPlatformResponseBody Load(JsonData json)
		{
			
			if (json == null)
				return null;
			
			var resultCode 			= (int)json["result_code"];

		
			var resultMessage = string.Empty;
			try
			{
				if (json["result_message"] != null) {
					resultMessage = (string)json ["result_message"];
				}
			}
			catch {
						}


			return new SwitchPlatformResponseBody ()
			{
				ResultCode = resultCode, 
				ResultMessage = resultMessage,
			};
		}
		
	}
}
