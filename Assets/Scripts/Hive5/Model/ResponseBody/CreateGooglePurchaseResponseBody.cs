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
	public class CreateGooglePurchaseResponseBody : IResponseBody
	{
		public int 		resultCode { set; get; }
		public string 	resultMessage { set; get; }
		public long		id { set; get; }

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
			long id;

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
				id = (long)json["id"];
			}
			catch(Exception)
			{
				id = 0;
			}

			return new CreateGooglePurchaseResponseBody() {
				resultCode 		= resultCode,
				resultMessage 	= resultMessage,
				id = id
			};
		}

	}
}

