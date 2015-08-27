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
		/** 
		* @api {POST} RedeemCoupon 쿠폰 적용하기
		* @apiVersion 0.3.11-beta
		* @apiName RedeemCoupon
		* @apiGroup Coupon
		*
		* @apiParam {string} serial 쿠폰 시리얼 코드
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.ApplyCoupon(code callback);
		*/
		public void RedeemCoupon(string serial, Callback callback)
		{
            var url = Hive5Client.Instance.ComposeRequestUrl(string.Format(ApiPath.Coupon.RedeemCoupon, serial));

            PostHttpAsync(url, null, ApplyCouponResponseBody.Load, callback);
		}	
	}
}
