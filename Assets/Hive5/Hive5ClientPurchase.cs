using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;
using Hive5.Util;


namespace Hive5
{
	/// <summary>
	/// Hive5 client.
	/// </summary>
#if UNITTEST
    public partial class Hive5Client : MockMonoSingleton<Hive5Client> {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif
		/********************************************************************************
			Purchase API Group
		*********************************************************************************/
		
		/** 
		* @api {POST} CreateNaverPurchase 네이버 결제 시작
		* @apiVersion 0.2.0
		* @apiName CreateNaverPurchase
		* @apiGroup Purchase
		*
		* @apiParam {string} productCode 상품 코드
		* @apiParam {string} paymentSequence 결제 시퀀스
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateNaverPurchase(productCode, paymentSequence, callback);
		*/
		public void CreateNaverPurchase(string productCode, string paymentSequence, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.CreateNaverPurchase);
			
			var requestBody = new {
				product_code 		= productCode,
				payment_sequence	= paymentSequence
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, CreateNaverPurchaseResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} CompleteNaverPurchase 네이버 결제 완료
		* @apiVersion 0.2.0
		* @apiName CompleteNaverPurchase
		* @apiGroup Purchase
		*
		* @apiParam {long} id 결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)
		* @apiParam {long} listPrice 표시 가격
		* @apiParam {long} purchasedPrice 결제 가격
		* @apiParam {string} currency 화폐
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateNaverPurchase(productCode, paymentSequence, callback);
		*/
		public void CompleteNaverPurchase(long id, long listPrice, long purchasedPrice, string currency, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.CompleteNaverPurchase, id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, CompleteNaverPurchaseResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} CreateGooglePurchase 구글 결제 시작
		* @apiVersion 0.2.0
		* @apiName CreateGooglePurchase
		* @apiGroup Purchase
		*
		* @apiParam {string} productCode 상품 코드
		* @apiParam {string} receiverPlatformUserId 선물 받을 플랫폼 User ID
		* @apiParam {string} mailForReceiver 메일로 받을 경우
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateGooglePurchase(productCode, receiverPlatformUserId, mailForReceiver, callback);
		*/
		[Obsolete("CreateGooglePurchase without receiverPlatform is deprecated, please use CreateGooglePurchase with receiverPlatform instead.")]
		public void CreateGooglePurchase(string productCode, string receiverPlatformUserId, string mailForReceiver, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.CreateGooglePurchase);
			
			var requestBody = new {
				product_code 				= productCode,
				receiver_platform_user_id	= receiverPlatformUserId,
				mail_for_receiver			= mailForReceiver
			};
			
			// WWW 호출
			PostHttpAsync(url, requestBody, CreateGooglePurchaseResponseBody.Load, callback);
		}

		/** 
		* @api {POST} CreateGooglePurchase 구글 결제 시작
		* @apiVersion 0.2.0
		* @apiName CreateGooglePurchase
		* @apiGroup Purchase
		*
		* @apiParam {string} productCode 상품 코드
		* @apiParam {string} receiverPlatform 선물 받을 플랫폼
		* @apiParam {string} receiverPlatformUserId 선물 받을 플랫폼 User ID
		* @apiParam {string} mailForReceiver 메일로 받을 경우
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateGooglePurchase(productCode, receiverPlatformUserId, mailForReceiver, callback);
		*/
		public void CreateGooglePurchase(string productCode, string receiverPlatform, string receiverPlatformUserId, string mailForReceiver, Callback callback)
		{
			if (string.IsNullOrEmpty (receiverPlatform) == true)
				throw new NullReferenceException ("receiverPlatform should not be empty!");

			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.CreateGooglePurchase);
			
			var requestBody = new {
				product_code 				= productCode,
				receiver_platform 			= receiverPlatform,
				receiver_platform_user_id	= receiverPlatformUserId,
				mail_for_receiver			= mailForReceiver
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, CreateGooglePurchaseResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} CompleteGooglePurchase 구글 결제 완료
		* @apiVersion 0.2.0
		* @apiName CompleteGooglePurchase
		* @apiGroup Purchase
		*
		* @apiParam {long} id 결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)
		* @apiParam {long} listPrice 표시 가격
		* @apiParam {long} purchasedPrice 결제 가격
		* @apiParam {string} currency 화폐
		* @apiParam {string} purchaseData 결제 데이터 
		* @apiParam {string} signature 결제 데이터 검증용 sign
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CompleteGooglePurchase(id, listPrice, purchasedPrice, currency, purchaseData, signature, callback);
		*/
		public void CompleteGooglePurchase(long id, long listPrice, long purchasedPrice, string currency, string purchaseData, string signature, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.CompleteGooglePurchase, id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency,
				purchase_data	= purchaseData,
				signature		= signature
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, CompleteGooglePurchaseResponseBody.Load, callback);
		}
	
		/** 
		* @api {POST} CreateApplePurchase 애플 결제 시작
		* @apiVersion 0.2.0
		* @apiName CreateApplePurchase
		* @apiGroup Purchase
		*
		* @apiParam {string} productCode 상품 코드
		* @apiParam {string} receiverPlatformUserId 선물 받을 플랫폼 User ID
		* @apiParam {string} mailForReceiver 메일로 받을 경우
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateApplePurchase(productCode, receiverPlatformUserId, mailForReceiver, callback);
		*/
		[Obsolete("CreateApplePurchase without receiverPlatform is deprecated, please use CreateApplePurchase with receiverPlatform instead.")]
		public void CreateApplePurchase(string productCode, string receiverPlatformUserId, string mailForReceiver, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.CreateApplePurchase);
			
			var requestBody = new {
				product_code 				= productCode,
				receiver_platform_user_id	= receiverPlatformUserId,
				mail_for_receiver			= mailForReceiver
			};
			
			// WWW 호출
			PostHttpAsync(url, requestBody, CreateApplePurchaseResponseBody.Load, callback);
		}

		/** 
		* @api {POST} CreateApplePurchase 애플 결제 시작
		* @apiVersion 0.2.0
		* @apiName CreateApplePurchase
		* @apiGroup Purchase
		*
		* @apiParam {string} productCode 상품 코드
		* @apiParam {string} receiverPlatform 선물 받을 플랫폼
		* @apiParam {string} receiverPlatformUserId 선물 받을 플랫폼 User ID
		* @apiParam {string} mailForReceiver 메일로 받을 경우
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateApplePurchase(productCode, receiverPlatformUserId, mailForReceiver, callback);
		*/
		public void CreateApplePurchase(string productCode, string receiverPlatform, string receiverPlatformUserId, string mailForReceiver, Callback callback)
		{
			if (string.IsNullOrEmpty (receiverPlatform) == true)
				throw new NullReferenceException ("receiverPlatform should not be empty!");

			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.CreateApplePurchase);
			
			var requestBody = new {
				product_code 				= productCode,
				receiver_platform  			= receiverPlatform,
				receiver_platform_user_id	= receiverPlatformUserId,
				mail_for_receiver			= mailForReceiver
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, CreateApplePurchaseResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} CompleteApplePurchase 애플 결제 완료
		* @apiVersion 0.2.0
		* @apiName CompleteApplePurchase
		* @apiGroup Purchase
		*
		* @apiParam {long} id 결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)
		* @apiParam {long} listPrice 표시 가격
		* @apiParam {long} purchasedPrice 결제 가격
		* @apiParam {string} currency 화폐
		* @apiParam {string} receipt 영수증 데이터
		* @apiParam {bool} isSandbox sandbox 여부
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CompleteApplePurchase(id, listPrice, purchasedPrice, currency, receipt, isSandbox, callback);
		*/
		public void CompleteApplePurchase(long id, long listPrice, long purchasedPrice, string currency, string receipt, bool isSandbox, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.CompleteApplePurchase, id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency,
				receipt			= receipt,
				is_sandbox		= isSandbox
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, CompleteApplePurchaseResponseBody.Load, callback);
		}


	}

}
