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
	public class Hive5Mission : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;

		/// <summary>
		/// Complete the specified missionKey and callback.
		/// </summary>
		/// <param name="missionKey">Mission key.</param>
		/// <param name="callback">Callback.</param>
		public void complete(string missionKey, CallBack callback)
		{
			var url = hive5.initializeUrl (string.Format("missions/complete/{0}", missionKey));

			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, new {}, CommonResponseBody.Load, callback)
			);;	
		}

		/// <summary>
		/// Batchs the complete.
		/// </summary>
		/// <param name="missionKeys">Mission keys.</param>
		/// <param name="callback">Callback.</param>
		public void batchComplete(string[] missionKeys, CallBack callback)
		{
			var url = hive5.initializeUrl ("missions/batch_complete");
			
			var requestBody = new {
				keys 	= missionKeys
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, CommonResponseBody.Load, callback)
			);;	
		}

		/// <summary>
		/// Gets the completed.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public void getCompleted(CallBack callback)
		{
			var url = hive5.initializeUrl ("missions/completed");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();

			// WWW 호출
			hive5.asyncRoutine ( 
            	hive5.getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
            );
		}

	}

}
