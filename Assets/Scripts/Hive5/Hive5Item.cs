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
	public class Hive5Item : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;

		/// <summary>
		/// Get the specified itemKeys and callback.
		/// </summary>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="callback">Callback.</param>
		public void get(string[] itemKeys, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl("items");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });

			// WWW 호출
			hive5.asyncRoutine (
				hive5.getHTTP (url, parameters.data, CommonResponseBody.Load, callback)
			);
		}

		/// <summary>
		/// Convert the specified itemConvertKey and callBack.
		/// </summary>
		/// <param name="itemConvertKey">Item convert key.</param>
		/// <param name="callBack">Call back.</param>
		public void convert(string itemConvertKey, CallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(String.Format("items/convert/{0}",itemConvertKey));
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, new {}, CommonResponseBody.Load, callBack)
			);
		}

		/// <summary>
		/// Consume the specified requestBody and callBack.
		/// </summary>
		/// <param name="requestBody">Request body.</param>
		/// <param name="callBack">Call back.</param>
		public void consume(object requestBody, CallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(APIPath.consumeItem);
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP (url, requestBody, CommonResponseBody.Load, callBack)
			);
		}

		public void gift(string platformUserId, string itemKey, int count, string mail, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl("items/gift");
			
			var requestBody = new {
				platform_user_id = platformUserId,
				Item	= itemKey,
				count	= count,
				mail	= mail
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP (url, requestBody, CommonResponseBody.Load, callback)
			);	
		}


	}

}
