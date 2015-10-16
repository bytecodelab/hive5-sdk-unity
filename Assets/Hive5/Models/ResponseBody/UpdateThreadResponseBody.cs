using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Models
{
	/// <summary>
	/// Result of UpdateThreadResponseBody
	/// </summary>
    public class UpdateThreadResponseBody : ExecuteUpdateResponseBody
    {
        public static IResponseBody  Load(JsonData json)
        {
            UpdateThreadResponseBody body = new UpdateThreadResponseBody();

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

