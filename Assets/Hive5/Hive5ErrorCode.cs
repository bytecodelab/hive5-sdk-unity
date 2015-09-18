using System;

namespace Hive5
{
	/// <summary>
	/// 에러코드
	/// </summary>
	public enum Hive5ErrorCode
	{
        /// <summary>
        /// Success
        /// </summary>
		Success = 0,

        /// <summary>
        /// 네트워크에러
        /// </summary>
		NetworkError = 99,
		HttpStatusContinue = 100,
		HttpStatusSwitchingProtocal = 101,
		HttpStatusOK = 200,
		HttpStatusCreated = 201,
		HttpStatusAccepted  = 202,
		HttpStatusNonAuthoritativeInformation = 203,
		HttpStatusNoContent = 204,
		HttpStatusResetContent = 205,
		HttpStatusPartialContent = 206,
		HttpStatusMultipleChoice = 300,
		HttpStatusMovedPermanently = 301,
		HttpStatusFound = 302,
		HttpStatusSeeOther = 303,
		HttpStatusNotMofified = 304,
		HttpStatusUseProxy = 305,
		HttpStatusUnused = 306,
		HttpStatusTemporaryRedirect = 307,
		HttpStatusPermanentRedirect = 308,
		HttpStatusBadRequest = 400,
		HttpStatusUnauthorized = 401,
		HttpStatusPaymentRequired = 402,
		HttpStatusForbidden = 403,
		HttpStatusNotFound = 404,
		HttpStatusMethodNotAllowed = 405,
		HttpStatusNotAcceptable = 406,
		HttpStatusProxyAuthenticationRequired = 407,
		HttpStatusRequestTimeout = 408,
		HttpStatusConflict = 409,
		HttpStatusGone = 410,
		HttpStatusLengthRequired = 411,
		HttpStatusPreconditionFailed = 412,
		HttpStatusRequestEntityTooLarge = 413,
		HttpStatusRequestURITooLong = 414,
		HttpStatusUnsupportedMediaType = 415,
		HttpStatusRequestedRangeNotSatisfiable = 416,
		HttpStatusExpectationFailed = 417,
		HttpStatusInternalServerError = 500,
		HttpStatusNotImplemented = 501,
		HttpStatusBadGateway = 502,
		HttpStatusServiceUnavailable = 503,
		HttpStatusGatewayTimeout = 504,
		HttpStatusHttpVersionNotSupported = 505,

        /// <summary>
        /// Invalid parameter
        /// </summary>
		InvalidParam = 1001,	
        /// <summary>
        /// Data dose not exist
        /// </summary>
		DataDoesNotExist = 1002,

        /// <summary>
        /// Invalid reward
        /// </summary>
		InvalidReward = 2101, 
        /// <summary>
        /// The reward was already accepted
        /// </summary>
        AlreadyAcceptedReward = 2102,
        /// <summary>
        /// Invalid tag pattern: tag에 comma(,)는 사용불가능하며, 최대 64글자가 허용된다.
        /// </summary>
        InvalidTagPattern = 2103,
        /// <summary>
        /// Invalid purchase status
        /// </summary>
		InvalidPurchaseStatus = 2201,
        /// <summary>
        /// Invalid payment sequence
        /// </summary>
		InvalidPaymentSequence = 2202,
        /// <summary>
        /// Invalid apple receipt
        /// </summary>
		InvalidAppleReceipt = 2203, 
        /// <summary>
        /// Invalid google purchase data
        /// </summary>
		InvalidGooglePurchaseData = 2204, 
        /// <summary>
        /// Invalid google signature
        /// </summary>
		InvalidGoogleSignature = 2205, 
        /// <summary>
        /// No google iap public key is registered
        /// </summary>
		NoGoogleIapPublicKey = 2206, 
        /// <summary>
        /// Invalid google iap public key
        /// </summary>
		InvalidGoogleIapPublicKey 	= 2207, 
        /// <summary>
        /// No kakao app auth info
        /// </summary>
        NoKakaoAppAuthInfo = 2208,
        /// <summary>
        /// No iap conversion
        /// </summary>
		NoIapConversion = 2303, 
        /// <summary>
        /// Expired coupon
        /// </summary>
        ExpiredCoupon = 2504,
        /// <summary>
        /// The player has already consumed the coupon
        /// </summary>
        ThePlayerHasAlreadyConsumedTheCoupon = 2505,
        /// <summary>
        /// The coupon is no more applicable
        /// </summary>
        TheCouponIsNoMoreApplicable = 2506,
        /// <summary>
        /// Not friend
        /// </summary>
		NotFriend = 2701, 
        /// <summary>
        /// Too many friends
        /// </summary>
        TooManyFriends = 2705,
        /// <summary>
        /// Already existing nickname
        /// </summary>
		AlreadyExistingNickname = 2801,
        /// <summary>
        /// Forbidden nickname
        /// </summary>
		ForbiddenNickname = 2802,

        /// <summary>
        /// Javascript Exception: Script(javascript) 실행 때 예외 발생. 내용은 result_message를 참조
        /// </summary>
		JsException	= 3001,
        /// <summary>
        /// Undefined Script: 정의되지 않은 스크립트
        /// </summary>
		UndefinedScript = 3101,
        /// <summary>
        /// Protected Script: REST로 외부에서 직접 실행시킬 수 없는 스크립트
        /// </summary>
		ProtectedScript = 3102,
        /// <summary>
        /// Undefined Library: 정의되지 않은 라이브러리
        /// </summary>
		ProtectedMethodClassDescriptor	= 3103,
        /// <summary>
        /// Object Not Found: load/save/destroy의 대상 객체를 찾을 수 없다
        /// </summary>
		ObjectNotFound = 3201,
        /// <summary>
        /// Invalid object
        /// </summary>
		InvalidObject = 3205,
        /// <summary>
        /// Already Existing Object: 이미 존재하는 Object임
        /// </summary>
        AlreadyExistingObject = 3206,
        /// <summary>
        /// Invalid Object Key: object의 key 명명 규칙에 위배
        /// </summary>
        InvalidObjectKey = 3207,
        /// <summary>
        /// Data Table Not Found: 정의된 Data Table이 없다
        /// </summary>
		UndefinedDataTable = 3301,
        /// <summary>
        /// Undefined AppData Key: 정의되지 않은 HAppCounter, HAppDictionary, HAppQueue Key
        /// </summary>
		UndefinedAppDataKey = 3501,
        /// <summary>
        /// Execution Timeout: 수행 시간 타임아웃
        /// </summary>
		ExecutionTimeout = 3901,
        /// <summary>
        /// Unsupported Library or Function: 지원되지 않는 라이브러리/함수
        /// </summary>
		UnsupportedLibraryOrFunction = 3903,
        /// <summary>
        /// Unsupported Data Type: 지원되지 않는 데이터 형식
        /// </summary>
		UnsupportedDataType = 3904,

        /// <summary>
        /// Already existing platform user name
        /// </summary>
		AlreadyExistingPlatformUserName = 4001,
        /// <summary>
        /// Already existing platform user email
        /// </summary>
		AlreadyExistingPlatformUserEmail = 4002,
        /// <summary>
        /// Invalid name or password
        /// </summary>
        InvalidNameOrPassword = 4003,

        /// <summary>
        /// Invalid push payload
        /// </summary>
        InvalidPayload = 5001,
        /// <summary>
        /// Invalid campaign status
        /// </summary>
        InvalidCampaignStatus = 5101,
        /// <summary>
        /// Invalid batch push status
        /// </summary>
        InvalidBatchPushStatus = 5301,

        /// <summary>
        /// Readonly forum: 읽기 전용 포럼에 쓰기, 수정, 삭제는 불가능
        /// </summary>
        ReadOnlyForum = 6001,
        /// <summary>
        /// Suspended forum: 사용 중단된 포럼
        /// </summary>
        SuspendedForum = 6002,

        /// <summary>
        /// App Object가 다른 이유로 이미 수정되어 저장이 불가능
        /// </summary>
        AppObjectAlreadyModified = 7001,

        /// <summary>
        /// The user has been disabled
        /// </summary>
		TheUserHasBeenBlocked = 8001,
        /// <summary>
        /// The session key is invalid
        /// </summary>
        TheSessionKeyIsInvalid = 8002,

        /// <summary>
        /// Internal Error: invalid app configuration
        /// </summary>
        InvalidAppConfiguration = 9001,

        /// <summary>
        /// Unknown error
        /// </summary>
		UnknownError = 9999,

        // SDK 에러코드 99xxxx
        DuplicatedApiCall = 990001
	}
}

