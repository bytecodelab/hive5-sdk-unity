using System;

namespace Hive5
{
    /// <summary>
    /// Hive5 API path.
    /// </summary>
    public class ApiPath
    {
        public static class Auth
        {
            public static string LogIn = "auth/log_in";
            public static string SwitchPlatform = "auth/switch";
            public static string Unregister = "auth/delete";
            public static string Agreement = "agreements";
            public static string CreatePlatformAccount = "accounts";
            public static string CheckPlatformNameAvailability = "accounts/is_available_name/{0}";
            public static string CheckPlatformEmailAvailability = "accounts/is_available_email/{0}";
            public static string AuthenticatePlatformAccount = "accounts/authenticate";
        }
        
        public static class Coupon
        {
            public static string RedeemCoupon = "coupons/{0}/apply";
        }
        
        public static class DataTable
        {
            public static string GetDataTable = "data_table/{0}";
        }

        public static class Forum
        {
            public static string ListThreads = "forums/{0}/threads";
            public static string CountThreads = "forums/{0}/threads/count";
            public static string CreateThread = "forums/{0}/threads";
            public static string UpdateThread = "forums/{0}/threads/{1}";
            public static string DeleteThread = "forums/{0}/threads/{1}";
        }

        public static class Leaderboard
        {
            public static string GetMyScore = "leaderboards/{0}/my_score";
            public static string ListScores = "leaderboards/{0}/scores";
            public static string ListSocialScores = "leaderboards/{0}/social_scores";
            public static string SubmitScore = "leaderboards/{0}/scores";
        }

        public static class Mail
        {
            public static string AddTags = "mails/{0}/add_tags";
            public static string AcceptReward = "mails/{0}/rewards/apply";
            public static string Count = "mails/count";
            public static string Create = "mails";
            public static string Delete = "mails/{0}";
            public static string DeleteOverLimit = "mails/delete_more_than/{0}";
            public static string DeleteOlderThan = "mails/delete_older_than/{0}";
            public static string List = "mails";
            public static string RemoveTags = "mails/{0}/remove_tags";
            public static string Update = "mails/{0}";
        }

        public static class Misc
        {
             public static string Logs = "logs"; 
        }

        public static class Purchase {
            public static string CreateNaverPurchase = "naver_purchases";
            public static string CompleteNaverPurchase = "naver_purchases/complete/{0}";
            public static string GetNaverPurchaseStatus = "naver_purchases/{0}";
            public static string CreateGooglePurchase = "google_purchases";
            public static string CompleteGooglePurchase = "google_purchases/complete/{0}";
            public static string GetGooglePurchaseStatus = "google_purchases/{0}";
            public static string CreateApplePurchase = "apple_purchases";
            public static string CompleteApplePurchase = "apple_purchases/complete/{0}";
            public static string GetApplePurchaseStatus = "apple_purchases/{0}";
        }

        public static class Settings
        {
            public static string CheckNicknameAvailability = "settings/nickname/is_available/{0}";
            public static string SetNickname = "settings/nickname/set";
            public static string UpdatePushToken = "settings/push_tokens/update";
            public static string ActivatePush = "settings/pushes/activate";
            public static string DeactivatePush = "settings/pushes/deactivate";
        }

        public static class Script { 
            public static string RunScript = "scripts/run/{0}";
            public static string CheckScript = "scripts/check/{0}";
        }

        public static class SocialGraph { 
            public static string AddFriends = "friends/add";
            public static string RemoveFriends = "friends/remove";
            public static string UpdateFriends = "friends/update";
            public static string ListFriends = "friends";
        }
    }

    /// <summary>
    /// Hive5 parameter key.
    /// </summary>
    public class ParameterKey
    {
        public static string AccessToken = "access_token";
        public static string PlatformUserId = "platform_user_id";
        public static string PlatformSdkVersion = "platform_sdk_version";
        public static string OS = "os";
        public static string Key = "key";
        public static string Platform = "platform";

        public static string UserDataKey = "user_data_key";
        public static string ItemKey = "key";
        public static string ConfigKey = "config_key";
        public static string ObjectKey = "object_key";

        public static string Nickname = "nickname";
    }

    /// <summary>
    /// Hive5 response key.
    /// </summary>
    public class ResponseKey
    {
        public static string ResultCode = "result_code";
        public static string ResultMessage = "result_message";
        public static string AccessToken = "access_token";
    }

    /// <summary>
    /// Hive5 header key.
    /// </summary>
    public class HeaderKey
    {
        public static string AppKey = "X-APP-KEY";		// 게임에 발급된 app key. 모든 API 호출에 필요. ex) a40e4122-XXXX-44a6-b916-68ed756f7XXX
        public static string Uuid = "X-AUTH-UUID";	// 디바이스 고유의 UUID. 모든 API 호출에 필요.
        public static string Token = "X-AUTH-TOKEN"; 	// 서버에서 발급한 인증 token. 사용자 식별이 필요한 API호출에 필요.
        public static string SessionKey = "X-AUTH-SESSION";  // 중복 로그인 방지를 위한 세션키.
        public static string ContentType = "Content-Type";	// 요청 ContentType
        public static string AcceptEncoding = "Accept-Encoding"; // 압축여부.
        public static string XPlatformKey = "X-PLATFORM-KEY"; 	// Account Platform
        public static string RequestId = "X-REQUEST-ID";
    }

    /// <summary>
    /// Hive5 header value.
    /// </summary>
    public class HeaderValue
    {
        public static string ContentType = "application/json";	// 요청 ContentType
        public static string Gzip = "gzip";
    }


    /// <summary>
    /// OS type.
    /// </summary>
    public class OSType
    {
        public static string Android = "android";
        public static string Ios = "ios";
    }

    /// <summary>
    /// Platform type.
    /// </summary>
    public class PlatformType
    {
        public static string None = "none";
        public static string Kakao = "kakao";
        public static string Naver = "naver";
        public static string Facebook = "facebook";
        public static string Google = "google";
        public static string QQ = "qq";
        public static string WeChat = "wechat";
    }

}