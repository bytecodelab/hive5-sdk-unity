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
	public partial class Hive5Client : MonoSingleton<Hive5Client> {

		/********************************************************************************
			Purchase API Group
		*********************************************************************************/
		
		/// <summary>
		/// Creates the google purchase.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void CreateNaverPurchase(string productCode, string paymentSequence, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.CreateNaverPurchase);
			
			var requestBody = new {
				product_code 		= productCode,
				payment_sequence	= paymentSequence
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, CreateNaverPurchaseResponseBody.Load, callback)
				);
		}
		
		/// <summary>
		/// Completes the google purchase.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="listPrice">List price.</param>
		/// <param name="purchasedPrice">Purchased price.</param>
		/// <param name="currency">Currency.</param>
		/// <param name="purchaseData">Purchase data.</param>
		/// <param name="signature">Signature.</param>
		/// <param name="callBack">Call back.</param>
		public void CompleteNaverPurchase(long id, long listPrice, long purchasedPrice, string currency, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.CompleteNaverPurchase, id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, CompleteNaverPurchaseResponseBody.Load, callback)
				);	
		}
		
		/// <summary>
		/// Creates the google purchase.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void CreateGooglePurchase(string productCode, string receiverPlatformUserId, string mailForReceiver, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.CreateGooglePurchase);
			
			var requestBody = new {
				product_code 				= productCode,
				receiver_platform_user_id	= receiverPlatformUserId,
				mail_for_receiver			= mailForReceiver
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, CreateGooglePurchaseResponseBody.Load, callback)
				);
		}
		
		/// <summary>
		/// Completes the google purchase.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="listPrice">List price.</param>
		/// <param name="purchasedPrice">Purchased price.</param>
		/// <param name="currency">Currency.</param>
		/// <param name="purchaseData">Purchase data.</param>
		/// <param name="signature">Signature.</param>
		/// <param name="callBack">Call back.</param>
		public void CompleteGooglePurchase(long id, long listPrice, long purchasedPrice, string currency, string purchaseData, string signature, CallBack callback)
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
			StartCoroutine (
				PostHttp(url, requestBody, CompleteGooglePurchaseResponseBody.Load, callback)
				);	
		}
		
		
		/// <summary>
		/// Creates the apple.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void CreateApplePurchase(string productCode, string receiverPlatformUserId, string mailForReceiver, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.CreateApplePurchase);
			
			var requestBody = new {
				product_code 				= productCode,
				receiver_platform_user_id	= receiverPlatformUserId,
				mail_for_receiver			= mailForReceiver
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, CreateApplePurchaseResponseBody.Load, callback)
				);
		}
		
		/// <summary>
		/// Completes the apple.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="listPrice">List price.</param>
		/// <param name="purchasedPrice">Purchased price.</param>
		/// <param name="currency">Currency.</param>
		/// <param name="receipt">Receipt.</param>
		/// <param name="isSandbox">If set to <c>true</c> is sandbox.</param>
		/// <param name="callBack">Call back.</param>
		public void CompleteApplePurchase(long id, long listPrice, long purchasedPrice, string currency, string receipt, bool isSandbox, CallBack callback)
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
			StartCoroutine (
				PostHttp(url, requestBody, CompleteApplePurchaseResponseBody.Load, callback)
				);
		}


	}

}
