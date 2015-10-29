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
		
		public void CreatePurchase(string platform, string platformParams, string productCode, User receiver, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(ApiPath.Purchase.CreatePurchase);
			
			var requestBody = new {
                platform = platform,
                platform_params = platformParams,
				product_code = productCode,
				receiver = receiver,
			};
			
            Hive5Http.Instance.PostHttpAsync(url, requestBody, CreatePurchaseResponseBody.Load, callback);
		}
		
		
		public void CompletePurchase(string id, string platform, object platformParams, long listPrice, long purchasedPrice, string currency, object extraParams, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Purchase.CompletePurchase, id));

            // params 란 predefined 를 사용하기 힘들기 때문에
            // Dictionary<string, string>를 쓸 수 밖에 없음
            Dictionary<string, object> requestBody = new Dictionary<string, object>();
            requestBody.Add("platform", platform);
            requestBody.Add("platform_params", platformParams);
            requestBody.Add("list_price", listPrice);
            requestBody.Add("purchased_price", purchasedPrice);
            requestBody.Add("currency", currency);
            requestBody.Add("params", extraParams);
            
            Hive5Http.Instance.PostHttpAsync(url, requestBody, CompletePurchaseResponseBody.Load, callback);
		}
        
        public void GetPurchaseStatus(long id, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Purchase.GetPurchaseStatus, id));

            Hive5Http.Instance.GetHttpAsync(url, null, GetPurchaseStatusResponseBody.Load, callback);
        }
	}
}
