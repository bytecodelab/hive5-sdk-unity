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

            GetPurchaseStatusResponseBody body = new GetPurchaseStatusResponseBody()
            {
                Purchase = new Purchase()
                {
                    Id = json["id"].ToString(),
                    ProductCode = json["product_code"].ToString(),
                    Status = json["status"].ToString(),
                },
            };

            return body;
        }
    }
}

