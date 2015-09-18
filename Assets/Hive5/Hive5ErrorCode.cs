using System;

namespace Hive5
{
	/// <summary>
	/// �����ڵ�
	/// </summary>
	public enum Hive5ErrorCode
	{
        /// <summary>
        /// Success
        /// </summary>
		Success = 0,

        /// <summary>
        /// ��Ʈ��ũ����
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
        /// Invalid tag pattern: tag�� comma(,)�� ���Ұ����ϸ�, �ִ� 64���ڰ� ���ȴ�.
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
        /// Javascript Exception: Script(javascript) ���� �� ���� �߻�. ������ result_message�� ����
        /// </summary>
		JsException	= 3001,
        /// <summary>
        /// Undefined Script: ���ǵ��� ���� ��ũ��Ʈ
        /// </summary>
		UndefinedScript = 3101,
        /// <summary>
        /// Protected Script: REST�� �ܺο��� ���� �����ų �� ���� ��ũ��Ʈ
        /// </summary>
		ProtectedScript = 3102,
        /// <summary>
        /// Undefined Library: ���ǵ��� ���� ���̺귯��
        /// </summary>
		ProtectedMethodClassDescriptor	= 3103,
        /// <summary>
        /// Object Not Found: load/save/destroy�� ��� ��ü�� ã�� �� ����
        /// </summary>
		ObjectNotFound = 3201,
        /// <summary>
        /// Invalid object
        /// </summary>
		InvalidObject = 3205,
        /// <summary>
        /// Already Existing Object: �̹� �����ϴ� Object��
        /// </summary>
        AlreadyExistingObject = 3206,
        /// <summary>
        /// Invalid Object Key: object�� key ��� ��Ģ�� ����
        /// </summary>
        InvalidObjectKey = 3207,
        /// <summary>
        /// Data Table Not Found: ���ǵ� Data Table�� ����
        /// </summary>
		UndefinedDataTable = 3301,
        /// <summary>
        /// Undefined AppData Key: ���ǵ��� ���� HAppCounter, HAppDictionary, HAppQueue Key
        /// </summary>
		UndefinedAppDataKey = 3501,
        /// <summary>
        /// Execution Timeout: ���� �ð� Ÿ�Ӿƿ�
        /// </summary>
		ExecutionTimeout = 3901,
        /// <summary>
        /// Unsupported Library or Function: �������� �ʴ� ���̺귯��/�Լ�
        /// </summary>
		UnsupportedLibraryOrFunction = 3903,
        /// <summary>
        /// Unsupported Data Type: �������� �ʴ� ������ ����
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
        /// Readonly forum: �б� ���� ������ ����, ����, ������ �Ұ���
        /// </summary>
        ReadOnlyForum = 6001,
        /// <summary>
        /// Suspended forum: ��� �ߴܵ� ����
        /// </summary>
        SuspendedForum = 6002,

        /// <summary>
        /// App Object�� �ٸ� ������ �̹� �����Ǿ� ������ �Ұ���
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

        // SDK �����ڵ� 99xxxx
        DuplicatedApiCall = 990001
	}
}

