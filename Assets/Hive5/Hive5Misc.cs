using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Model;
using Hive5.Util;


namespace Hive5
{
	/// <summary>
	/// Hive5 Misc features
	/// </summary>
    public class Hive5Misc
    {
		/// <summary>
		/// Logs
		/// </summary>
		public void Logs(string eventType, string data, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl (ApiPath.Misc.Logs);
			
			var requestBody = new {
				event_type = eventType,
				data = data,
			};
			
			Hive5Http.Instance.PostHttpAsync(url, requestBody, LogsResponseBody.Load, callback);
		}
    }
}
