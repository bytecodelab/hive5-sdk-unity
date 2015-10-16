using Hive5.Models;
using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Models
{
    public class ExecuteInsertResponseBody : CommonResponseBody
    {
        /// <summary>
        /// 생성된 데이터의 Id
        /// </summary>
        public long Id { get; set; }

        public static IResponseBody  Load(JsonData json)
        {
            ExecuteInsertResponseBody body = new ExecuteInsertResponseBody();

            if (json == null)
                return null;

            if (json.ContainsKey("id"))
            {
                body.Id = json["id"].ToLong();
            }

            return body;
        }
    }
}
