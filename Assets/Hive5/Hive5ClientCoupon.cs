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
		    Coupon API Group
		*********************************************************************************/

		/** 
		* @api {POST} ApplyCoupon 쿠폰 적용하기
		* @apiVersion 1.0.0-alpha
		* @apiName ApplyCoupon
		* @apiGroup Coupon
		*
		* @apiParam {string} content 쿠폰 시리얼 코드
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.ApplyCoupon(code callback);
		*/
		public void ApplyCoupon(string code, Callback callback)
		{
            var url = InitializeUrl(string.Format(APIPath.ApplyCoupon, code));

            // WWW 호출
            PostHttpAsync(url, null, ApplyCouponResponseBody.Load, callback);
		}	
	}
}
