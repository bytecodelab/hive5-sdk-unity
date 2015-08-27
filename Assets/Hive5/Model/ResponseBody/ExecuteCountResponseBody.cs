using Hive5.Model;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Model
{
    public class ExecuteCountResponseBody : CommonResponseBody
    {
        public int Count { get; set; }

        public static IResponseBody  Load(JsonData json)
        {
            ExecuteCountResponseBody body = new ExecuteCountResponseBody();

            if (json == null)
                return null;

            if (json.ContainsKey("count"))
            {
                body.Count = json["count"].ToInt();
            }

            return body;
        }
    }
}
