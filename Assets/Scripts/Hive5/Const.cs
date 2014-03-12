using System;

namespace Hive5
{
	/// <summary>
	/// Hive5 header key.
	/// </summary>
	public class Hive5HeaderKey
	{
		public static string appKey 		= "X-APP-KEY";		// 게임에 발급된 app key. 모든 API 호출에 필요. ex) a40e4122-XXXX-44a6-b916-68ed756f7XXX
		public static string uuid 			= "X-AUTH-UUID";	// 디바이스 고유의 UUID. 모든 API 호출에 필요.
		public static string token 			= "X-AUTH-TOKEN"; 	// 서버에서 발급한 인증 token. 사용자 식별이 필요한 API호출에 필요
		public static string contentType	= "Content-Type";	// 요청 ContentType 키
	}
	
	/// <summary>
	/// Hive5 header value.
	/// </summary>
	public class Hive5HeaderValue
	{
		public static string contentType	= "application/json";	// 요청 ContentType 값
	}

	/// <summary>
	/// Hive5 API path.
	/// </summary>
	public class Hive5APIPath
	{
		public static string kakaoLogin 	= "auth/kakao";
		public static string anonymousLogin = "auth/anonymous";
		public static string naverLogin 	= "auth/naver";

		public static string userData 		= "data";	// user data API
		public static string userItem 		= "item";	// user item

	}

	/// <summary>
	/// Hive5 API server.
	/// </summary>
	public class Hive5APIServer
	{
		public static string realHost		= "https://api.hive5.io";
		public static string betaHost 		= "https://beta.api.hive5.io";
		public static string version 		= "v3";

	}
	
	/// <summary>
	/// Hive5 parameter key.
	/// </summary>
	public class Hive5ParameterKey
	{
		public static string userId 		= "user_id";
		public static string accessToken 	= "access_token";
		public static string sdkVersion 	= "sdk_version";
		public static string OS 			= "os";
		public static string key 			= "key";

		public static string userDataKey 	= "user_data_key";
		public static string itemKey 		= "item_key";
		public static string configKey 		= "config_key";
	}

	/// <summary>
	/// Hive5 response key.
	/// </summary>
	public class Hive5ResponseKey
	{
		public static string resultCode 	= "result_code";
		public static string accessToken 	= "access_token";
	}

	/// <summary>
	/// Hive5 platform type.
	/// </summary>
	public class Hive5PlatformType
	{
		public static string android = "android";
		public static string ios = "ios";
	}
}

