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
		public long 	UserId;
		public string 	AccessToken;
        public string   SessionKey;
		public int NewMailCount;

		public string Platform;
		public string PlatformUserId;

		public List<Config> 	Configs;				
		public List<Mission> 	CompletedMissions;	
		public List<Agreement> 	Agreements;			
		public List<Promotion> 	Promotions;			
		
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
			var completedMissions	= Mission.LoadList( json["completed_missions"] );
			var configs 			= Config.LoadList( json ["configs"] );
			var promotions			= Promotion.LoadList ( json ["promotions"] );
			var agreements 			= Agreement.LoadList ( json ["agreements"] );
			var platform = (string)json ["platform"];
			var platformUserId = (string)json ["platform_user_id"];
			
			return new LoginResponseBody ()
			{
				AccessToken = accessToken, 
                SessionKey = sessionKey,
				NewMailCount = newMailCount, 
				Platform = platform,
				PlatformUserId = platformUserId,
				CompletedMissions = completedMissions, 
				Promotions = promotions, 
				Agreements = agreements,
				Configs = configs,
			};
		}
		
	}
}
