using System;

namespace Hive5
{
	/// <summary>
	/// Hive5 result code.
	/// </summary>
	public enum Hive5ResultCode
	{
		Success 					= 0,
		NetworkError 				= 99,
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
		InvalidParameter 			= 1001,	
		DataDoesNotExist 			= 1002,

		InvalidReward 				= 2101, 

		InvalidPurchaseStatus 		= 2201,
		InvalidPaymentSequence 		= 2202,
		InvalidAppleReceipt 		= 2203, 
		InvalidGooglePurchaseData 	= 2204, 
		InvalidGoogleSignature 		= 2205, 
		NoGoogleIapPublicKeyIsRegistered = 2206, 
		InvalidGoogleIapPublicKey 	= 2207, 
        NoKakaoAppAuthInfo          = 2208,

		NoIapConversion 			= 2303, 

		MissionAlreadyCompleted 	= 2401, 

        PromotionCodeAlreadyConsumed = 2501,
        InvalidPromotionCode = 2502,

		//InvalidRoundStatus 			= 2601, 

		NotFriend 					= 2701, 
		TheItemIsNotAbleToGift 		= 2702, 
		TheItemCannotGiftBecauseItHasRecentlyGifted = 2703, 
		TooManyCountToGift 			= 2704, 

		AlreadyExistingNickname		= 2801,
		ForbiddenNickname			= 2802,

		JavascriptExceptionOnProcedure	= 3001,

		UndefinedProcedure				= 3101,
		ProtectedProcedure				= 3102,
		ProtectedMethodClassDescriptor	= 3103,
		UndefinedLibrary				= 3104,

		ObjectNoFound					= 3201,
		SingletonCanNotBeDestroyed		= 3202,
		InvalidObjectField				= 3203,
		TooManyObjectFields				= 3204,

		DataTableNotFound				= 3301,

		InvalidReturn					= 3401,

		UndefinedAppDataKey				= 3501,

		ExecutionTimeout				= 3901,
		StackOverflow					= 3902,
		UnsupportedLibraryOrFunction	= 3903,
		UnsupportedDataType				= 3904,

		AlreadyExistingPlatformUserName 	= 4001,
		AlreadyExistingPlatformUserEmail 	= 4002,
        InvalidNameOrPassword               = 4003,

        InvalidPayload                  = 5001,

		TheUserHasBeenBlocked 		    = 8001,
        TheSessionKeyIsInvalid          = 8002,

        InvalidAppConfiguration         = 9001,
        InvalidServiceName              = 9002,
        InvalidJobItem                  = 9003,

        NotImplemented                  = 9998,

		UnknownError 				    = 9999
	}
}

