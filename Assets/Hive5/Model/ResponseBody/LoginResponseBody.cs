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
		public long 	userId;
		public string 	accessToken;
		public int 		mailboxNewItemCount;

		public List<UserData>	userData;				
		public List<Item> 		items;				
		public List<Config> 	configs;				
		public List<Mission> 	completedMissions;	
		public List<Agreement> 	agreements;			
		public List<Promotion> 	promotions;			

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static LoginResponseBody Load(JsonData json)
		{

			if (json == null)
				return null;
			
			var resultCode 			= (int)json["result_code"];
			var userId 				= (int)json["user_id"];
			var mailboxNewItemCount = (int)json["mailbox_new_item_count"];
			var accessToken 		= (string)json["access_token"];
			var completedMissions	= Mission.LoadList( json["completed_missions"] );
			var configs 			= Config.LoadList( json ["configs"] );
			var promotions			= Promotion.LoadList ( json ["promotions"] );
			var agreements 			= Agreement.LoadList ( json ["agreements"] );


			return new LoginResponseBody ()
			{
				accessToken = accessToken, 
				mailboxNewItemCount = mailboxNewItemCount, 
				userId = userId, 
				completedMissions = completedMissions, 
				promotions = promotions, 
				agreements = agreements
			};
		}

	}
}

