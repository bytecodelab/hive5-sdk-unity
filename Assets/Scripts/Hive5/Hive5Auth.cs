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
	public class Hive5Auth {

		Hive5Core hive5 = Hive5Core.Instance;

		public void login(string userId, string accessToken, string sdkVersion, string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, Hive5Core.apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(APIPath.kakaoLogin);

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
				hive5.getHTTP(url, parameters.data, ( response ) => { 
				if ( response.resultCode == Hive5ResultCode.Success)
					hive5.setAccessToken(((LoginData)response.resultData).accessToken); 
				
				callback(response);
				})
			);

		}

	}

}
