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
	/// Coupon에 대한 모든 기능을 포함하는 클래스
	/// </summary>
    public class Hive5Coupon
    {
		/// <summary>
        /// 쿠폰코드를 적용합니다.
        /// </summary>
        /// <param name="serial">쿠폰코드</param>
        /// <param name="callback">콜백 함수</param>
		public void Redeem(string serial, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Coupon.RedeemCoupon, serial));

            Hive5Http.Instance.PostHttpAsync(url, null, RedeemCouponResponseBody.Load, callback);
		}	
	}
}
