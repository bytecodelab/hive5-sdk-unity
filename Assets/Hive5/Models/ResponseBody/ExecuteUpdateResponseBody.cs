using Hive5.Models;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Models
{
    public class ExecuteUpdateResponseBody : CommonResponseBody
    {
        public int AffectedCount { get; set; }

        public static IResponseBody  Load(JsonData json)
        {
            ExecuteUpdateResponseBody body = new ExecuteUpdateResponseBody();

            if (json == null)
                return null;

            if (json.ContainsKey("affected_count"))
            {
                body.AffectedCount = json["affected_count"].ToInt();
            }

            return body;
        }
    }
}
