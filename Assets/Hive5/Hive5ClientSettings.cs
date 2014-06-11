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

		/*
		 * TODO: gilbert will add comments on windows later
		 */
		public void CheckNicknameAvailability(string nickname, CallBack callback)
		{
			var url = InitializeUrl (String.Format(APIPath.CheckNicknameAvailability, WWW.EscapeURL(nickname)));

			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string> ();

			// WWW 호출
			StartCoroutine (
				GetHttp (url, parameters.data, CheckNicknameAvailabilityResponseBody.Load, callback)
			);
		}

		/*
		 * TODO: gilbert will add comments on windows later
		 */
		public void SetNickname(string nickname, CallBack callback)
		{
			var url = InitializeUrl (APIPath.SetNickname);
			
			var requestBody = new {
				nickname = nickname,
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp (url, requestBody, SetNicknameResponseBody.Load, callback)
				);
		}
	}

}
