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
	/// Hive5 client.
	/// </summary>
	public partial class Hive5Client : MonoSingleton<Hive5Client> {

		/********************************************************************************
			Mission API Group
		*********************************************************************************/
		
		/// <summary>
		/// Complete the specified missionKey and callback.
		/// </summary>
		/// <param name="missionKey">Mission key.</param>
		/// <param name="callback">Callback.</param>
		public void CompleteMission(string missionKey, CallBack callback)
		{
			var url = InitializeUrl (string.Format(APIPath.CompleteMission, missionKey));
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, new {}, CompleteMissionResponseBody.Load, callback)
				);
		}
		
		/// <summary>
		/// Batchs the complete.
		/// </summary>
		/// <param name="missionKeys">Mission keys.</param>
		/// <param name="callback">Callback.</param>
		public void BatchCompleteMission(string[] missionKeys, CallBack callback)
		{
			var url = InitializeUrl (APIPath.BatchCompleteMission);
			
			var requestBody = new {
				keys 	= missionKeys
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, BatchCompleteMissionResponseBody.Load, callback)
				);
		}
		
		/// <summary>
		/// Gets the completed.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public void GetCompletedMissions(CallBack callback)
		{
			var url = InitializeUrl (APIPath.GetCompletedMissions);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
			StartCoroutine ( 
			                GetHttp (url, parameters.data, GetCompletedMissionsResponseBody.Load, callback) 
			                );
		}


	}

}
