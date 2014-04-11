using System;

namespace Hive5
{
	/// <summary>
	/// Hive5 API server.
	/// </summary>
	public class APIServer
	{
		public static string RealHost		= "https://api.hive5.io";
		public static string BetaHost 		= "https://beta.api.hive5.io";
		public static string Version 		= "v4";
		
	}

	/// <summary>
	/// Hive5 API path.
	/// </summary>
	public class APIPath
	{
		public static string KakaoLogin 		= "auth/kakao";
		public static string AnonymousLogin 	= "auth/anonymous";
		public static string NaverLogin 		= "auth/naver";
		public static string UserData 			= "data";		// user data API
		public static string Item 				= "items";		// user item
		public static string GetMails			= "mails";
		public static string SubmitMail			= "mails";
		public static string ConsumeItem		= "items/consume";
	 	public static string Unregister			= "auth/delete";
	 	public static string Agreement			= "agreements";
		public static string ConvertItem 		= "items/convert/{0}";
		public static string GiftItem 			= "items/gift";
		public static string LeaderboardSubmitScores 	= "leaderboards/{0}/scores/{1}";
		public static string LeaderboardScores 			= "leaderboards/{0}/scores";
		public static string LeaderboardMyScore 		= "leaderboards/{0}/my_score";
		public static string LeaderboardSocialScores 	= "leaderboards/{0}/social_scores";
		public static string LeaderboardPrize 			= "leaderboards/prize/{0}";
		public static string MailCount 				= "mails/count";
		public static string UpdateMail 			= "mails/update/{0}";
		public static string DeleteMail 			= "mails/delete/{0}";
		public static string DeleteAllMail 			= "mails/delete_all";
		public static string AttachMailTag 			= "mails/{0}/add_tags";
		public static string DetachMailTag 			= "mails/{0}/remove_tags";
		public static string CompleteMission 		= "missions/complete/{0}";
	 	public static string BatchCompleteMission 	= "missions/batch_complete";
	 	public static string GetCompletedMissions 	= "missions/completed";
		public static string CreateNaverPurchase 	= "naver_purchases";
		public static string CompleteNaverPurchase 	= "naver_purchases/complete/{0}";
		public static string CreateGooglePurchase 	= "google_purchases";
		public static string CompleteGooglePurchase = "google_purchases/complete/{0}";
		public static string CreateApplePurchase 	= "apple_purchases";
		public static string CompleteApplePurchase 	= "apple_purchases/complete/{0}";
		public static string UpdatePushToken 		= "push_tokens";
	 	public static string Reward 				= "rewards/{0}";
		public static string ApplyReward 			= "rewards/apply/{0}";
	 	public static string ApplyAllReward 		= "rewards/apply_all";
		public static string InvalidateReward 		= "rewards/invalidate/{0}";
		public static string StartRound 			= "rounds/start?rule_id={0}";
		public static string EndRound 				= "rounds/end/{0}";
		public static string UpdateFriends 			= "friends/update";
	 	public static string GetFriendsInfo 		= "friends/info";
	}

	/// <summary>
	/// Hive5 parameter key.
	/// </summary>
	public class ParameterKey
	{
		public static string UserId 		= "user_id";
		public static string AccessToken 	= "access_token";
		public static string SdkVersion 	= "sdk_version";
		public static string OS 			= "os";
		public static string Key 			= "key";
		
		public static string UserDataKey 	= "user_data_key";
		public static string ItemKey 		= "key";
		public static string ConfigKey 		= "config_key";
	}

	/// <summary>
	/// Hive5 response key.
	/// </summary>
	public class ResponseKey
	{
		public static string ResultCode 	= "result_code";
		public static string ResultMessage 	= "result_message";
		public static string AccessToken 	= "access_token";
	}

	/// <summary>
	/// Hive5 header key.
	/// </summary>
	public class HeaderKey
	{
		public static string AppKey 		= "X-APP-KEY";		// 게임에 발급된 app key. 모든 API 호출에 필요. ex) a40e4122-XXXX-44a6-b916-68ed756f7XXX
		public static string Uuid 			= "X-AUTH-UUID";	// 디바이스 고유의 UUID. 모든 API 호출에 필요.
		public static string Token 			= "X-AUTH-TOKEN"; 	// 서버에서 발급한 인증 token. 사용자 식별이 필요한 API호출에 필요
		public static string ContentType	= "Content-Type";	// 요청 ContentType
	}	

	/// <summary>
	/// Hive5 header value.
	/// </summary>
	public class HeaderValue
	{
		public static string ContentType	= "application/json";	// 요청 ContentType
	}
	
	
	/// <summary>
	/// OS type.
	/// </summary>
	public class OSType
	{
		public static string Android	= "android";
		public static string Ios 		= "ios";
	}
	
	/// <summary>
	/// Platform type.
	/// </summary>
	public class PlatformType
	{
		public static string Kakao 		= "kakao";
		public static string Naver 		= "naver";
		public static string Facebook 	= "facebook";
		public static string Google 	= "google";
		public static string None 		= "none";
	}

}