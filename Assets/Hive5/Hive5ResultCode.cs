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
		ConditionsAreNotMet 		= 2001,
		TooManyConditions 			= 2002, //Max 5
		TooManyGetDataRequest 		= 2003, //Max 10
		TooManySetDataRequest 		= 2004,	//Max 10
		InvalidReward 				= 2101, 
		InvalidPurchaseStatus 		= 2201,
		InvalidPaymentSequence 		= 2202,
		InvalidAppleReceipt 		= 2203, 
		InvalidGooglePurchaseData 	= 2204, 
		InvalidGoogleSignature 		= 2205, 
		NoGoogleIapPublicKeyIsRegistered = 2206, 
		InvalidGoogleIapPublicKey 	= 2207, 
		NoItemConversionIsDefined 	= 2301,
		NotEnoughItem 				= 2302,
		NoIapConversion 			= 2303, 
		MissionAlreadyCompleted 	= 2401, 
		InvalidRoundStatus 			= 2601, 
		NotFriend 					= 2701, 
		TheItemIsNotAbleToGift 		= 2702, 
		TheItemCannotGiftBecauseItHasRecentlyGifted = 2703, 
		TooManyCountToGift 			= 2704, 
		TheUserHasBeenDisabled 		= 8001,
		UnknownError 				= 9999
	}
}

