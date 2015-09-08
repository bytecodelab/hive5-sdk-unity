using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Models;
using Hive5.Util;


namespace Hive5
{
	/// <summary>
	/// Hive5 Purchase features
	/// </summary>
    public class Hive5Purchase
    {
		/** 
		* @api {POST} CreateNaverPurchase 네이버 결제 시작
		* @apiVersion 0.3.11-beta
		* @apiName CreateNaverPurchase
		* @apiGroup Purchase
		*
		* @apiParam {string} productCode 상품 코드
		* @apiParam {string} paymentSequence 결제 시퀀스
        * @apiParam {string} receiverPlatform 선물 받을 플랫폼, 자신에게 보낼 경우 비움
		* @apiParam {long} receiverId 선물 받을 플랫폼 User ID, 자신에게 보낼 경우 비움
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateNaverPurchase(productCode, paymentSequence, callback);
		*/
		public void CreateNaverPurchase(string productCode, string paymentSequence, string receiverPlatform, long receiverId, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = Hive5Client.ComposeRequestUrl(ApiPath.Purchase.CreateNaverPurchase);
			
			var requestBody = new {
				product_code 		= productCode,
				payment_sequence	= paymentSequence,
                receiver = new
                {
                    platform = receiverPlatform,
                    id = receiverId,
                }
			};
			
			// WWW 호출
            Hive5Http.Instance.PostHttpAsync(url, requestBody, CreateNaverPurchaseResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} CompleteNaverPurchase 네이버 결제 완료
		* @apiVersion 0.3.11-beta
		* @apiName CompleteNaverPurchase
		* @apiGroup Purchase
		*
		* @apiParam {long} id 결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)
		* @apiParam {long} listPrice 표시 가격
		* @apiParam {long} purchasedPrice 결제 가격
		* @apiParam {string} currency 화폐
        * @apiParam {string} paramsJson 구매 완료 script에 전달할 params
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateNaverPurchase(productCode, paymentSequence, callback);
		*/
		public void CompleteNaverPurchase(long id, long listPrice, long purchasedPrice, string currency, string paramsJson, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Purchase.CompleteNaverPurchase, id));
			
            // params 란 predefined 를 사용하기 힘들기 때문에
            // List<KeyValuePair<string, string>>를 쓸 수 밖에 없음
            List<KeyValuePair<string, string>> requestBody = new List<KeyValuePair<string,string>>()
            {
                new KeyValuePair<string, string>("list_price", listPrice.ToString()),
                new KeyValuePair<string, string>("purchased_price", purchasedPrice.ToString()),
                new KeyValuePair<string, string>("currency", currency.ToString()),
                new KeyValuePair<string, string>("params", paramsJson.ToString()),
            };

            Hive5Http.Instance.PostHttpAsync(url, requestBody, CompleteNaverPurchaseResponseBody.Load, callback);
		}

        /** 
		* @api {POST} GetNaverPurchaseStatus 구글 결제 상태확인
		* @apiVersion 0.3.11-beta
		* @apiName GetNaverPurchaseStatus
		* @apiGroup Purchase
		*
		* @apiParam {long} id 결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetNaverPurchaseStatus(id, callback);
		*/
        public void GetNaverPurchaseStatus(long id, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Purchase.GetNaverPurchaseStatus, id));

            Hive5Http.Instance.GetHttpAsync(url, null, GetPurchaseStatusResponseBody.Load, callback);
        }
         
		/** 
		* @api {POST} CreateGooglePurchase 구글 결제 시작
		* @apiVersion 0.3.11-beta
		* @apiName CreateGooglePurchase
		* @apiGroup Purchase
		*
		* @apiParam {string} productCode 상품 코드
		* @apiParam {User} receiver 선물 받을 유저
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateGooglePurchase(productCode, PlatformType.Google, receiver, callback);
		*/
		public void CreateGooglePurchase(string productCode, User receiver, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(ApiPath.Purchase.CreateGooglePurchase);
			
			var requestBody = new {
				product_code = productCode,
				receiver = receiver,
#if UNITTEST
                test = true,
#endif
			};
			
            Hive5Http.Instance.PostHttpAsync(url, requestBody, CreateGooglePurchaseResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} CompleteGooglePurchase 구글 결제 완료
		* @apiVersion 0.3.11-beta
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
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CompleteGooglePurchase(id, listPrice, purchasedPrice, currency, purchaseData, signature, callback);
		*/
		public void CompleteGooglePurchase(string id, long listPrice, long purchasedPrice, string currency, string purchaseData, string signature, string paramsJson, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Purchase.CompleteGooglePurchase, id));
			
            // params 란 predefined 를 사용하기 힘들기 때문에
            // List<KeyValuePair<string, string>>를 쓸 수 밖에 없음
            List<KeyValuePair<string, string>> requestBody = new List<KeyValuePair<string,string>>()
            {
                new KeyValuePair<string, string>("list_price", listPrice.ToString()),
                new KeyValuePair<string, string>("purchased_price", purchasedPrice.ToString()),
                new KeyValuePair<string, string>("currency", currency.ToString()),
                new KeyValuePair<string, string>("purchase_data", purchaseData.ToString()),
                new KeyValuePair<string, string>("signature", signature.ToString()),
                new KeyValuePair<string, string>("params", paramsJson.ToString()),
            };
			
            Hive5Http.Instance.PostHttpAsync(url, requestBody, CompleteGooglePurchaseResponseBody.Load, callback);
		}

        /** 
		* @api {POST} GetGooglePurchaseStatus 구글 결제 상태확인
		* @apiVersion 0.3.11-beta
		* @apiName GetGooglePurchaseStatus
		* @apiGroup Purchase
		*
		* @apiParam {long} id 결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetGooglePurchaseStatus(id, callback);
		*/
        public void GetGooglePurchaseStatus(long id, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Purchase.GetGooglePurchaseStatus, id));

            Hive5Http.Instance.GetHttpAsync(url, null, GetPurchaseStatusResponseBody.Load, callback);
        }

		/** 
		* @api {POST} CreateApplePurchase 애플 결제 시작
		* @apiVersion 0.3.11-beta
		* @apiName CreateApplePurchase
		* @apiGroup Purchase
		*
		* @apiParam {string} productCode 상품 코드
		* @apiParam {string} receiverPlatform 선물 받을 플랫폼, 자신에게 보낼 경우 비움
		* @apiParam {long} receiverId 선물 받을 플랫폼 User ID, 자신에게 보낼 경우 비움.
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateApplePurchase(productCode, PlatformType.Google, receiverPlatformUserId, callback);
		*/
		public void CreateApplePurchase(string productCode, string receiverPlatform, long receiverId, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(ApiPath.Purchase.CreateApplePurchase);
			
			var requestBody = new {
				product_code = productCode,
                receiver = new {
				    platform = receiverPlatform,
				    id	= receiverId,
                }
			};
			
            Hive5Http.Instance.PostHttpAsync(url, requestBody, CreateApplePurchaseResponseBody.Load, callback);
		}
		
	   /** 
		* @api {POST} CompleteApplePurchase 애플 결제 완료
		* @apiVersion 0.3.11-beta
		* @apiName CompleteApplePurchase
		* @apiGroup Purchase
		*
		* @apiParam {long} id 결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)
		* @apiParam {long} listPrice 표시 가격
		* @apiParam {long} purchasedPrice 결제 가격
		* @apiParam {string} currency 화폐
		* @apiParam {string} receipt 영수증 데이터
		* @apiParam {bool} isSandbox sandbox 여부
        * @apiParam {string} paramsJson 스크립트에 전달하고 싶은 파라미터
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CompleteApplePurchase(id, listPrice, purchasedPrice, currency, receipt, isSandbox, callback);
		*/
		public void CompleteApplePurchase(long id, long listPrice, long purchasedPrice, string currency, string receipt, bool isSandbox, string paramsJson, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Purchase.CompleteApplePurchase, id));
			
            // params 란 predefined 를 사용하기 힘들기 때문에
            // List<KeyValuePair<string, string>>를 쓸 수 밖에 없음
            List<KeyValuePair<string, string>> requestBody = new List<KeyValuePair<string,string>>()
            {
                new KeyValuePair<string, string>("list_price", listPrice.ToString()),
                new KeyValuePair<string, string>("purchased_price", purchasedPrice.ToString()),
                new KeyValuePair<string, string>("currency", currency.ToString()),
                new KeyValuePair<string, string>("receipt", receipt.ToString()),
                new KeyValuePair<string, string>("is_sandbox", isSandbox.ToString()),
                new KeyValuePair<string, string>("params", paramsJson.ToString()),
            };
			
            Hive5Http.Instance.PostHttpAsync(url, requestBody, CompleteApplePurchaseResponseBody.Load, callback);
		}

       /** 
		* @api {POST} GetApplePurchaseStatus 애플 결제 상태확인
		* @apiVersion 0.3.11-beta
		* @apiName GetApplePurchaseStatus
		* @apiGroup Purchase
		*
		* @apiParam {long} id 결제 ID(결제 시작 SDK 호출하여 응답 받은 ID)
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetApplePurchaseStatus(id, callback);
		*/
        public void GetApplePurchaseStatus(long id, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Purchase.GetApplePurchaseStatus, id));

            Hive5Http.Instance.GetHttpAsync(url, null, GetPurchaseStatusResponseBody.Load, callback);
        }
	}
}
