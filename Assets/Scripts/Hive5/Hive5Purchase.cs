using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net;
using System.IO;
using LitJson;
using Hive5;
using Hive5.Core;
using Hive5.Model;
using Hive5.Util;

namespace Hive5
{

	/// <summary>
	/// Hive5 user data.
	/// </summary>
	public class Hive5Purchase : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;


		/// <summary>
		/// Creates the apple.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void createApple(string productCode, string receiverKakaoUserId, string mailForReceiver, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl("apple_purchases");
			
			var requestBody = new {
				product_code 			= productCode,
				receiver_kakao_user_id	= receiverKakaoUserId,
				mail_for_receiver		= mailForReceiver
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, CommonResponseBody.Load, callback)
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
		public void completeApple(long id, long listPrice, long purchasedPrice, string currency, string receipt, bool isSandbox, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(string.Format("apple_purchases/complete/{0}", id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency,
				receipt			= receipt,
				is_sandbox		= isSandbox
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, CommonResponseBody.Load, callback)
			);
		}


	}

}
