using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Models
{
	/// <summary>
	/// Result of CreateThread
	/// </summary>
    public class CreateThreadResponseBody : ExecuteInsertResponseBody
    {
        public static IResponseBody  Load(JsonData json)
        {
            CreateThreadResponseBody body = new CreateThreadResponseBody();

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

