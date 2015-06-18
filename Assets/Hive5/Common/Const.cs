using System;

namespace Hive5
{
	/// <summary>
	/// Hive5 API path.
	/// </summary>
	public class APIPath
	{
		public static string KakaoLogin 		= "auth/kakao";
		public static string PlatformLogin 		= "auth/login";
		public static string AnonymousLogin 	= "auth/anonymous";
		public static string NaverLogin 		= "auth/naver";
		public static string SwitchPlatform 	= "auth/switch";
		public static string UserData 			= "data";		// user data API
		public static string Item 				= "items";		// user item
		public static string GetMails			= "mails";
		public static string SubmitMail			= "mails";
		public static string Logs 				= "logs"; // add logs
		public static string ConsumeItem		= "items/consume";
	 	public static string Unregister			= "auth/delete";
	 	public static string Agreement			= "agreements";
		public static string CheckNicknameAvailability 	= "settings/nickname/is_available/{0}";
		public static string SetNickname 		= "settings/nickname/set";
		public static string ConvertItem 		= "items/convert/{0}";
		public static string GiftItem 			= "items/gift";
		public static string LeaderboardSubmitScores 	= "leaderboards/{0}/scores/{1}";
		public static string LeaderboardScores 			= "leaderboards/{0}/scores";
		public static string LeaderboardMyScore 		= "leaderboards/{0}/my_score";
		public static string LeaderboardSocialScores 	= "leaderboards/{0}/social_scores";
        public static string ApplyCoupon            = "coupons/{0}/apply";
		public static string MailCount 				= "mails/count";
		public static string UpdateMail 			= "mails/update/{0}";
		public static string DeleteMail 			= "mails/delete/{0}";
		public static string DeleteAllMail 			= "mails/delete_all";
		public static string AttachMailTag 			= "mails/{0}/add_tags";
		public static string DetachMailTag 			= "mails/{0}/remove_tags";
		public static string CompleteMission 		= "missions/complete/{0}";
	 	public static string GetCompletedMissions 	= "missions/completed";
		public static string CreateNaverPurchase 	= "naver_purchases";
		public static string CompleteNaverPurchase 	= "naver_purchases/complete/{0}";
		public static string CreateGooglePurchase 	= "google_purchases";
		public static string CompleteGooglePurchase = "google_purchases/complete/{0}";
		public static string CreateApplePurchase 	= "apple_purchases";
		public static string CompleteApplePurchase 	= "apple_purchases/complete/{0}";
		public static string UpdatePushToken 		= "pushes/register";
        public static string TogglePushAccept 		= "pushes/activate/{0}";
	 	public static string Reward 				= "rewards/{0}";
		public static string ApplyReward 			= "rewards/apply/{0}";
	 	public static string ApplyAllReward 		= "rewards/apply_all";
		public static string InvalidateReward 		= "rewards/invalidate/{0}";
		public static string CreatePlatformAccount  = "accounts";
		public static string CheckPlatformNameAvailability  = "accounts/is_available_name/{0}";
		public static string CheckPlatformEmailAvailability  = "accounts/is_available_email/{0}";
		public static string AuthenticatePlatformAccount  = "accounts/authenticate";
		public static string AddFriends 			= "friends/add";
		public static string RemoveFriends 			= "friends/remove";
		public static string UpdateFriends 			= "friends/update";
		public static string GetFriends 			= "friends";

	 	public static string GetFriendsInfo 		= "friends/info";
		public static string CallProcedure			= "procedures/call/{0}";
        public static string CallProcedureWithoutAuth	= "procedures/check/{0}";
		public static string GetObjects				= "objects";
		public static string CreateObjects 			= "objects/create";
		public static string SetObjects				= "objects/set";
		public static string DestoryObjects			= "objects/destroy";
	}

	/// <summary>
	/// Hive5 parameter key.
	/// </summary>
	public class ParameterKey
	{
		public static string AccessToken 	= "access_token";
		public static string PlatformUserId	= "platform_user_id";
		public static string PlatformSdkVersion = "platform_sdk_version";
		public static string OS 			= "os";
		public static string Key 			= "key";
		public static string Platform		= "platform";
		
		public static string UserDataKey 	= "user_data_key";
		public static string ItemKey 		= "key";
		public static string ConfigKey 		= "config_key";
		public static string ObjectKey		= "object_key";

		public static string Nickname = "nickname";
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
		public static string Token 			= "X-AUTH-TOKEN"; 	// 서버에서 발급한 인증 token. 사용자 식별이 필요한 API호출에 필요.
        public static string SessionKey     = "X-AUTH-SESSION";  // 중복 로그인 방지를 위한 세션키.
		public static string ContentType	= "Content-Type";	// 요청 ContentType
		public static string AcceptEncoding = "Accept-Encoding"; // 압축여부.
		public static string XPlatformKey  	= "X-PLATFORM-KEY"; 	// Account Platform
        public static string RequestId = "X-REQUEST-ID";
	}	

	/// <summary>
	/// Hive5 header value.
	/// </summary>
	public class HeaderValue
	{
		public static string ContentType	= "application/json";	// 요청 ContentType
		public static string Gzip 			= "gzip";
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
		public static string Anonymous 		= "anonymous";
        public static string Hive5 = "hive5";
		public static string Kakao 		= "kakao";
		public static string Naver 		= "naver";
		public static string Facebook 	= "facebook";
		public static string Google 	= "google";
		public static string QQ			= "qq";
		public static string WeChat 	= "wechat";
	}

}