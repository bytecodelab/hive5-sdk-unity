using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Result of DeleteThread
	/// </summary>
    public class DeleteThreadResponseBody : ExecuteUpdateResponseBody
    {
        public static IResponseBody  Load(JsonData json)
        {
            DeleteThreadResponseBody body = new DeleteThreadResponseBody();

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

