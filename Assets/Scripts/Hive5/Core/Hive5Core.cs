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


namespace Hive5.Core
{
	/// <summary>
	/// Hive5 client.
	/// </summary>
	public class Hive5Core : MonoSingleton<Hive5Core> {

		private string appKey		= "";
		private string uuid			= "";
		private string accessToken 	= "";

		private bool initState 	= false;
		private bool loginState = false; 
		private bool isDebug 	= false;

		public bool InitState { 
			get { return initState;} 
		}
		public bool LoginState { 
			get { return loginState;} 
		}

		private Hive5TimeZone timezone 	= Hive5TimeZone.UTC;
		private Hive5APIZone zone		= Hive5APIZone.Beta;
		private string host 	= APIServer.betaHost;
		private string version 	= APIServer.version;


		protected Hive5Core () {}


		/// <summary>
		/// Init the specified appKey and uuid.
		/// </summary>
		/// <param name="appKey">App key.</param>
		/// <param name="uuid">UUID.</param>
		public void Init(string appKey, string uuid)
		{
			this.appKey = appKey;
			this.uuid 	= uuid;
			this.initState = true;
		}

		public void Login()
		{
			this.loginState = true;
		}

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

		/// <summary>
		/// Asyncs the routine.
		/// </summary>
		/// <param name="routine">Routine.</param>
		public void asyncRoutine(IEnumerator routine)
		{
			// 코루틴 WWW 호출
			StartCoroutine(routine);
		}

		/// <summary>
		/// Https the get.
		/// </summary>
		/// <returns>The get.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="callback">Callback.</param>
		public IEnumerator getHTTP(string url, List<KeyValuePair<string, string>> parameters, Hive5Response.dataLoader loader, Hive5API.CallBack callBack)
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

			callBack (Hive5Response.Load (loader, www.text));
		}

		/// <summary>
		/// Https the post.
		/// </summary>
		/// <returns>The post.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		public IEnumerator postHTTP(string url, object requestBody, Hive5Response.dataLoader loader, Hive5API.CallBack callBack)
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
			
			callBack (Hive5Response.Load (loader, www.text));
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
