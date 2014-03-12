using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5.Modle;
using Hive5.Util;

namespace Hive5
{
	/// <summary>
	/// Hive5 client.
	/// </summary>
	public class Hive5Client : MonoSingleton<Hive5Client> {

		public delegate void apiCallBack (Hive5ResultCode resultCode, JsonData jsonObject);

		public string host 			= Hive5APIServer.betaHost;
		public string version 		= Hive5APIServer.version;
		public string appKey 		= "a40e4122-99d9-44a6-b916-68ed756f79d6";

		private string uuid 		= "88197948207226176";
		private string accessToken 	= "";

		private bool isDebug 		= false;
		private bool loginState		= false;

		private Hive5APIZone zone	= Hive5APIZone.Beta;

		protected Hive5Client () {}

		/// <summary>
		/// Sets the debug.
		/// </summary>
		/// <param name="debugFlag">If set to <c>true</c> debug flag.</param>
		public void setDebug()
		{
			isDebug = true;
		}

		/// <summary>
		/// Sets the zone.
		/// </summary>
		/// <param name="zone">Zone.</param>
		public void setZone(Hive5APIZone zone)
		{
			switch(zone)
			{
				// Beta Server
				case Hive5APIZone.Beta:
					host = Hive5APIServer.betaHost;
				break;

				// Real Server
				case Hive5APIZone.Real:
					host = Hive5APIServer.realHost;
				break;
			}

		}

		/// <summary>
		/// Sets the user data.
		/// </summary>
		/// <param name="requestBody">Request body.</param>
		/// <param name="callBack">Call back.</param>
		public void setUserData(object requestBody, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(Hive5APIPath.userData);

			// 코루틴 WWW 호출
			StartCoroutine( httpPost(url, requestBody, callBack) );
		}

		/// <summary>
		/// Gets the user data.
		/// </summary>
		/// <param name="dataKeys">Data keys.</param>
		/// <param name="callback">Callback.</param>
		public void getUserData(string dataKeys, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(Hive5APIPath.userData);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add( Hive5ParameterKey.key, dataKeys);

			StartCoroutine ( httpGet (url, parameters.data, callback) );
		}

		/// <summary>
		/// Kakao Login API
		/// </summary>
		/// <param name="userId">카카오 user id</param>
		/// <param name="accessToken">카카오 로그인 후 발급 받은 access token</param>
		/// <param name="sdkVersion">클라이언트에서 사용하고 있는 카카오 sdk의 버전</param>
		/// <param name="os">OS 구분자 'android' 또는 'ios'</param>
		/// <param name="userDataKey">로그인 후 가져와야할 사용자 user data의 key 목록</param>
		/// <param name="itemKey">로그인 후 가져와야할 사용자 item의 key 목록</param>
		/// <param name="configKey">로그인 후 가져와야할 사용자 configuration의 key 목록</param>
		/// <param name="completedMissions">로그인 후 가져와야 할 사용자 완료 Mission의 Key 목록</param>
		/// <param name="callback">콜백 함수</param>
		public void kakaoLogin(string userId, string accessToken, string sdkVersion, string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(Hive5APIPath.kakaoLogin);

			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add( Hive5ParameterKey.userId, userId );
			parameters.Add( Hive5ParameterKey.accessToken, accessToken );
			parameters.Add( Hive5ParameterKey.sdkVersion, sdkVersion );
			parameters.Add( Hive5ParameterKey.OS, os );

			Array.ForEach ( userDataKeys, key => { parameters.Add( Hive5ParameterKey.userDataKey, key ); } );
			Array.ForEach ( itemKeys, key => { parameters.Add( Hive5ParameterKey.itemKey, key ); } );
			Array.ForEach ( configKeys, key => { parameters.Add( Hive5ParameterKey.configKey, key ); } );
			
			// 코루틴 WWW 호출
			StartCoroutine(httpGet(url, parameters.data, (resultCode, response) => { 
				if (resultCode == Hive5ResultCode.Success)
					setAccessToken(response[Hive5ResponseKey.accessToken].ToString()); 

				callback(resultCode, response);
			}));
		}


		/// <summary>
		/// Anonymous Login API
		/// </summary>
		/// <param name="os">OS 구분자 'android' 또는 'ios'</param>
		/// <param name="userDataKey">로그인 후 가져와야할 사용자 user data의 key 목록</param>
		/// <param name="itemKey">로그인 후 가져와야할 사용자 item의 key 목록</param>
		/// <param name="configKey">로그인 후 가져와야할 사용자 configuration의 key 목록</param>
		/// <param name="callback">콜백 함수</param>
		public void anonymousLogin(string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(Hive5APIPath.anonymousLogin);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add(Hive5ParameterKey.OS, os);

			Array.ForEach ( userDataKeys, key => { parameters.Add( Hive5ParameterKey.userDataKey, key ); });
			Array.ForEach ( itemKeys, key => { parameters.Add( Hive5ParameterKey.itemKey, key ); });
			Array.ForEach ( configKeys, key => { parameters.Add( Hive5ParameterKey.configKey, key ); });
			
			// 코루틴 WWW 호출
			StartCoroutine(httpGet(url, parameters.data, (resultCode, response) => { 
				if (resultCode == Hive5ResultCode.Success)
					setAccessToken(response[Hive5ResponseKey.accessToken].ToString()); 
				
				callBack(resultCode, response);
			}));
		}


		/// <summary>
		/// Https the get.
		/// </summary>
		/// <returns>The get.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="callback">Callback.</param>
		private IEnumerator httpGet(string url, List<KeyValuePair<string, string>> parameters,  apiCallBack callBack)
		{
			// Hive5 API Header 설정
			var headers = new Hashtable();
			headers.Add(Hive5HeaderKey.appKey, appKey);
			headers.Add(Hive5HeaderKey.uuid, uuid);
			headers.Add(Hive5HeaderKey.token, accessToken);
			headers.Add(Hive5HeaderKey.contentType, Hive5HeaderValue.contentType);

			string queryString = "";		
			foreach (KeyValuePair<string, string> parameter in parameters)
			{
				if (queryString.Length > 0)	
				{
					queryString += "&";
				}
				
				queryString += parameter.Key + "=" + parameter.Value;
			}

			string newUrl = url;
			
			if (queryString.Length > 0)
			{
				newUrl = url + "?" + queryString;
			}

			WWW www = new WWW( newUrl, null, headers );
			yield return www;

			if(isDebug) Debug.Log ("www reuqest URL = " + newUrl);
			if(isDebug) Debug.Log ("www response = " + www.text);

			JsonData response = JsonMapper.ToObject (www.text);
			Hive5ResultCode resultCode = (Hive5ResultCode) ((int)response[Hive5ResponseKey.resultCode]);
			callBack(resultCode, response);
		}

		/// <summary>
		/// Https the post.
		/// </summary>
		/// <returns>The post.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		private IEnumerator httpPost(string url, object requestBody, apiCallBack callBack)
		{	
			// Hive5 API Header 설정
			var headers = new Hashtable();
			headers.Add(Hive5HeaderKey.appKey, appKey);
			headers.Add(Hive5HeaderKey.uuid, uuid);
			headers.Add(Hive5HeaderKey.token, accessToken);
			headers.Add(Hive5HeaderKey.contentType, Hive5HeaderValue.contentType);

			// Hive5 API json body 변환
			string jsonString = JsonMapper.ToJson (requestBody);						

			var encoding	= new System.Text.UTF8Encoding();

			// Hive5 API Request
			WWW www = new WWW(url, encoding.GetBytes(jsonString), headers); 
			yield return www;

			if(isDebug) Debug.Log ("www reuqest URL = " + url);
			if(isDebug) Debug.Log ("www request jsonBody= " + jsonString);
			if(isDebug) Debug.Log ("www response = " + www.text);
			
			JsonData response = JsonMapper.ToObject (www.text);
			Hive5ResultCode resultCode = (Hive5ResultCode) ((int)response[Hive5ResponseKey.resultCode]);
			callBack(resultCode, response);
		}
		
		/// <summary>
		/// Initializes the URL.
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="path">Path.</param>
		private string initializeUrl(string path)
		{
			return String.Join("/", new String[] { host, version, path });	
		}
		
		/// <summary>
		/// Gets the headers.
		/// </summary>
		/// <returns>The headers.</returns>
		private Dictionary<string, string> getHeaders()
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			
			result.Add(Hive5HeaderKey.appKey, appKey);
			result.Add(Hive5HeaderKey.uuid, uuid);
			
			if (accessToken.Length > 0)
				result.Add(Hive5HeaderKey.token, accessToken);
			
			return result;
		}

		/// <summary>
		/// Sets the access token.
		/// </summary>
		/// <param name="accessToken">Access token.</param>
		private void setAccessToken(string accessToken)
		{
			this.accessToken = accessToken;
		}

		/// <summary>
		/// Sets the UUID.
		/// </summary>
		/// <param name="uuid">UUID.</param>
		private void setUuid(string uuid)
		{
			this.uuid = uuid;
		}

	}

}
