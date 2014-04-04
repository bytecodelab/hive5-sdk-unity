using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net;
using System.IO;
using LitJson;
using Hive5;
using Hive5.Core;
using Hive5.Model;
using Hive5.Util;

namespace Hive5
{

	/// <summary>
	/// Hive5 user data.
	/// </summary>
	public class Hive5Round : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;

		/// <summary>
		/// Start the specified roundRuleId and callback.
		/// </summary>
		/// <param name="roundRuleId">Round rule identifier.</param>
		/// <param name="callback">Callback.</param>
		public void start(long roundRuleId,  CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(String.Format("rounds/start?rule_id={0}", roundRuleId));
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, new {}, SetUserDataResponseBody.Load, callback)
			);
		}

		/// <summary>
		/// End the specified roundId, requestBody and callback.
		/// </summary>
		/// <param name="roundId">Round identifier.</param>
		/// <param name="requestBody">Request body.</param>
		/// <param name="callback">Callback.</param>
		public void end(long roundId, object requestBody, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(String.Format("rounds/end/{0}", roundId));
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
			);
		}

	}

}
