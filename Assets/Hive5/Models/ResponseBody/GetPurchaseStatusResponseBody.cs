using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Models
{
    /// <summary>
    /// Result of ListThreads
    /// </summary>
    public class GetPurchaseStatusResponseBody : CommonResponseBody
    {
        public Purchase Purchase { get; set; }

        public static IResponseBody Load(JsonData json)
        {
            if (json == null)
                return null;

            Purchase purchase = null;
            if (json.ContainsKey("purchase"))
            {
                var purchaseJson = json["purchase"];
                purchase = new Purchase()
                {
                    Id = purchaseJson["id"].ToString(),
                    ProductCode = purchaseJson["product_code"].ToString(),
                    Status = purchaseJson["status"].ToString(),
                };
            }

            GetPurchaseStatusResponseBody body = new GetPurchaseStatusResponseBody()
            {
                Purchase = purchase,
            };

            return body;
        }
    }
}

