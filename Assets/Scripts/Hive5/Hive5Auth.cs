using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Core;
using Hive5.Model;
using Hive5.Util;


namespace Hive5
{

	/// <summary>
	/// Hive5 auth.
	/// </summary>
	public class Hive5Auth : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;

		/// <summary>
		/// Login the specified userId, accessToken, sdkVersion, os, userDataKeys, itemKeys, configKeys and callback.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <param name="accessToken">Access token.</param>
		/// <param name="sdkVersion">Sdk version.</param>
		/// <param name="os">Os.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="configKeys">Config keys.</param>
		/// <param name="callback">Callback.</param>
		public void login(string userId, string accessToken, string sdkVersion, string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, Hive5API.CallBack callback)
		{
			if (!hive5.InitState)
				return;

			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(APIPath.kakaoLogin);

			Debug.Log ("login LoginState=" + hive5.LoginState);

			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add( ParameterKey.userId, userId );
			parameters.Add( ParameterKey.accessToken, accessToken );
			parameters.Add( ParameterKey.sdkVersion, sdkVersion );
			parameters.Add( ParameterKey.OS, os );

			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); } );
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); } );
			Array.ForEach ( configKeys, key => { parameters.Add( ParameterKey.configKey, key ); } );

			hive5.asyncRoutine (
				hive5.getHTTP(url, parameters.data, LoginDataResponseBody.Load, ( response ) => { 
				if ( response.resultCode == Hive5ResultCode.Success)

					hive5.setAccessToken(((LoginDataResponseBody)response.resultData).accessToken);
					hive5.Login();
					callback(response);
				}
			));

		}


		public void logout(string userId, string accessToken, string sdkVersion, string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, Hive5API.CallBack callback)
		{

		}


		public void unregister(string userId, string accessToken, string sdkVersion, string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, Hive5API.CallBack callback)
		{

		}

	}

}
