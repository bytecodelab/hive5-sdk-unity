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
		public int 		MailboxNewItemCount;

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
			
			var userId 				= (int)json["user_id"];
			var mailboxNewItemCount = (int)json["mailbox_new_item_count"];
			var accessToken 		= (string)json["access_token"];
			var completedMissions	= Mission.LoadList( json["completed_missions"] );
			var configs 			= Config.LoadList( json ["configs"] );
			var promotions			= Promotion.LoadList ( json ["promotions"] );
			var agreements 			= Agreement.LoadList ( json ["agreements"] );
			
			
			return new LoginResponseBody ()
			{
				AccessToken = accessToken, 
				MailboxNewItemCount = mailboxNewItemCount, 
				UserId = userId, 
				CompletedMissions = completedMissions, 
				Promotions = promotions, 
				Agreements = agreements,
				Configs = configs,
			};
		}
		
	}
}
