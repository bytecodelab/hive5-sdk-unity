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
	public class Hive5Core : MonoSingleton<Hive5Core> {

		public delegate void apiCallBack (Hive5Response hive5Response);

		public string host 			= APIServer.betaHost;
		public string version 		= APIServer.version;
		public string appKey 		= Hive5Config.appKey;

		private string uuid 		= "uuid";
		private string accessToken 	= null;

		private bool isDebug 		= false;
		private bool loginState		= false;

		private Hive5TimeZone timezone 	= Hive5TimeZone.UTC;
		private Hive5APIZone zone		= Hive5APIZone.Beta;

		protected Hive5Core () {}

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
					host = APIServer.betaHost;
				break;

				// Real Server
				case Hive5APIZone.Real:
					host = APIServer.realHost;
				break;
			}

		}

		public void asyncRoutine(IEnumerator routine)
		{
			// 코루틴 WWW 호출
			StartCoroutine(routine);
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
		public void loginKakao(string userId, string accessToken, string sdkVersion, string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.kakaoLogin);

			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add( ParameterKey.userId, userId );
			parameters.Add( ParameterKey.accessToken, accessToken );
			parameters.Add( ParameterKey.sdkVersion, sdkVersion );
			parameters.Add( ParameterKey.OS, os );

			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); } );
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); } );
			Array.ForEach ( configKeys, key => { parameters.Add( ParameterKey.configKey, key ); } );
			
			// 코루틴 WWW 호출
			StartCoroutine(getHTTP(url, parameters.data, ( response ) => { 
				if ( response.resultCode == Hive5ResultCode.Success)
					setAccessToken(((LoginData)response.resultData).accessToken); 

				callback(response);
			}));
		}


		/// <summary>
		/// Https the get.
		/// </summary>
		/// <returns>The get.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="callback">Callback.</param>
		public IEnumerator getHTTP(string url, List<KeyValuePair<string, string>> parameters,  apiCallBack callBack)
		{
			// Hive5 API Header 설정
			var headers = new Hashtable();
			headers.Add(HeaderKey.appKey, this.appKey);
			headers.Add(HeaderKey.uuid, this.uuid);
			headers.Add(HeaderKey.token, this.accessToken);
			headers.Add(HeaderKey.contentType, HeaderValue.contentType);

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

			if(this.isDebug) Debug.Log ("www reuqest URL = " + newUrl);
			if(this.isDebug) Debug.Log ("www response = " + www.text);

			callBack (Hive5Response.Load (www.text));
		}

		/// <summary>
		/// Https the post.
		/// </summary>
		/// <returns>The post.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		public IEnumerator postHTTP(string url, object requestBody, apiCallBack callBack)
		{	
			// Hive5 API Header 설정
			var headers = new Hashtable();
			headers.Add(HeaderKey.appKey, this.appKey);
			headers.Add(HeaderKey.uuid, this.uuid);
			headers.Add(HeaderKey.token, this.accessToken);
			headers.Add(HeaderKey.contentType, HeaderValue.contentType);

			// Hive5 API json body 변환
			string jsonString = JsonMapper.ToJson (requestBody);						

			var encoding	= new System.Text.UTF8Encoding();

			// Hive5 API Request
			WWW www = new WWW(url, encoding.GetBytes(jsonString), headers); 
			yield return www;

			if(this.isDebug) Debug.Log ("www reuqest URL = " + url);
			if(this.isDebug) Debug.Log ("www request jsonBody= " + jsonString);
			if(this.isDebug) Debug.Log ("www response = " + www.text);
			
			callBack (Hive5Response.Load (www.text));
		}
		
		/// <summary>
		/// Initializes the URL.
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="path">Path.</param>
		public string initializeUrl(string path)
		{
			return String.Join("/", new String[] { this.host, this.version, path });	
		}
		
		/// <summary>
		/// Gets the headers.
		/// </summary>
		/// <returns>The headers.</returns>
		private Dictionary<string, string> getHeaders()
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			
			result.Add(HeaderKey.appKey, this.appKey);
			result.Add(HeaderKey.uuid, this.uuid);

			if (this.accessToken.Length > 0)
				result.Add(HeaderKey.token, this.accessToken);
			
			return result;
		}

		/// <summary>
		/// Sets the access token.
		/// </summary>
		/// <param name="accessToken">Access token.</param>
		public void setAccessToken(string accessToken)
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
