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
	public class LoginResponseBody : IResponseBody
	{
		public string 	AccessToken;
        public string   SessionKey;
		public int NewMailCount;

		public string Platform;
		public string PlatformUserId;

		public List<Agreement> 	Agreements;			
		
		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static LoginResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

            var sessionKey = (string)json["session_key"];
			var newMailCount = (int)json["new_mail_count"];
			var accessToken 		= (string)json["access_token"];

			var agreements 			= Agreement.LoadList ( json ["agreements"] );
			var platform = (string)json["user"]["platform"];
			var platformUserId = (string)json ["user"]["id"];
			
			return new LoginResponseBody ()
			{
				AccessToken = accessToken, 
                SessionKey = sessionKey,
				NewMailCount = newMailCount, 
				Platform = platform,
				PlatformUserId = platformUserId,
				Agreements = agreements,
			};
		}
		
	}
}
