using System;

namespace Hive5
{

	/// <summary>
	/// Hive5 result code.
	/// </summary>
	public enum Hive5ResultCode
	{
		Success 					= 0,
		InvalidParameter 			= 1001,	
		DataDoseNotExist 			= 1002,
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

	/// <summary>
	/// Hive5 API zone.
	/// </summary>
	public enum Hive5APIZone
	{
		Beta = 0,
		Real = 1
	}

}

