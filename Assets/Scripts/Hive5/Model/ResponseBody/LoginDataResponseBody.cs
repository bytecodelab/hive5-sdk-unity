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
	public class LoginDataResponseBody : IResponseBody
	{
		public int 		resultCode;
		public long 	userId;
		public string 	accessToken;
		public int 		mailboxNewItemCount;

		public Dictionary<string, string> userData;				
		public Dictionary<string, ItemData> items;				
		public Dictionary<string, string> configs;				
		public Dictionary<string, DateTime> completedMissions;	
		public Dictionary<string, AgreementData> agreements;			
		public List<PromotionData> promotions;					

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;
			
			var resultCode 			= (int)json["result_code"];
			var userId 				= (int)json["user_id"];
			var mailboxNewItemCount = (int)json["mailbox_new_item_count"];
			var accessToken 		= (string)json["access_token"];

			var items 				= ItemData.Load( json ["items"] );
			var userData			= UserData.Load( json["user_data"] );
			var completedMissions	= MissionData.Load( json["completed_missions"] );
			var configs 			= ConfigData.Load( json ["configs"] );
			var promotions			= PromotionData.Load ( json ["promotions"] );
			var agreements 			= AgreementData.Load ( json ["agreements"] );

			return new LoginDataResponseBody ()
			{
				resultCode = resultCode, 
				accessToken = accessToken, 
				mailboxNewItemCount = mailboxNewItemCount, 
				userId = userId, 
				items = items, 
				userData = userData, 
				completedMissions = completedMissions, 
				promotions = promotions, 
				agreements = agreements
			};
		}

	}
}

