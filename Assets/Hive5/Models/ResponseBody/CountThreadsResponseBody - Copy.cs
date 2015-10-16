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
    public class CountThreadsResponseBody : ExecuteCountResponseBody
    {
        public static IResponseBody  Load(JsonData json)
        {
            CountThreadsResponseBody body = new CountThreadsResponseBody();

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

