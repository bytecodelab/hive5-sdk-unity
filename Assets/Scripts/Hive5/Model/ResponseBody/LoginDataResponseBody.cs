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


		public LoginDataResponseBody(int resultCode, string accessToken, int mailboxNewItemCount, long userId, Dictionary<string, ItemData> items, Dictionary<string, string> userData, Dictionary<string, DateTime> completedMissions, List<PromotionData> promotions, Dictionary<string, AgreementData> agreements)
		{
			this.resultCode 			= resultCode;
			this.accessToken 			= accessToken;
			this.mailboxNewItemCount	= mailboxNewItemCount;
			this.userId 				= userId;
			this.items 					= items;
			this.userData 				= userData;
			this.completedMissions 		= completedMissions;
			this.promotions				= promotions;
			this.agreements 			= agreements;
		}

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

			return new LoginDataResponseBody(resultCode, accessToken, mailboxNewItemCount, userId, items, userData, completedMissions, promotions, agreements);
		}

	}
}

