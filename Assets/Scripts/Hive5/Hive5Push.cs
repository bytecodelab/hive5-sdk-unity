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
	public class Hive5Push : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;


		/// <summary>
		/// Updates the token.
		/// </summary>
		/// <param name="platform">Platform.</param>
		/// <param name="token">Token.</param>
		/// <param name="callback">Callback.</param>
		public void updateToken(string platform, string token, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl("push_tokens");
			
			var requestBody = new {
				push_platform 	= platform,
				push_token 		= token
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
			);
			
		}

	}

}
