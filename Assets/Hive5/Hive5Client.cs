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

		public delegate IResponseBody dataLoader (JsonData response);
		public delegate void CallBack (Hive5Response hive5Response);

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
		private string host;
		private string version;

		protected Hive5Client () {}
	
		/********************************************************************************
			Init API Group
		*********************************************************************************/

		/// <summary>
		/// Init the specified appKey and uuid.
		/// </summary>
		/// <param name="appKey">App key.</param>
		/// <param name="uuid">UUID.</param>
		public void Init(string appKey, string uuid, Hive5APIZone zone)
		{
			this.appKey 	= appKey;
			this.uuid 		= uuid;

			if (Hive5APIZone.Beta == zone)
				this.host 	= APIServer.BetaHost;
			else if (Hive5APIZone.Production == zone)
				this.host 	= APIServer.ProductionHost;

			this.version 	= APIServer.Version;
			this.initState 	= true;
		}

		/********************************************************************************
			Push API Group
		*********************************************************************************/

		/// <summary>
		/// Updates the token.
		/// </summary>
		/// <param name="platform">Platform.</param>
		/// <param name="token">Token.</param>
		/// <param name="callback">Callback.</param>
		public void UpdatePushToken(string platform, string token, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.UpdatePushToken);
			
			var requestBody = new {
				push_platform 	= platform,
				push_token 		= token
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, UpdatePushTokenResponseBody.Load, callback)
			);
			
		}


		/********************************************************************************
			Procedures API Group
		*********************************************************************************/

		
		/// <summary>
		/// Calls the procedure.
		/// </summary>
		/// <param name="procedureName">Procedure name.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="callback">Callback.</param>
		public void CallProcedure(string procedureName, TupleList<string, string> parameters,  CallBack callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(String.Format(APIPath.CallProcedure,procedureName));
			
			// WWW 호출

		    if (parameters == null)
			{
			    StartCoroutine(PostHttp(url, null, new {}, CommonResponseBody.Load, callback));	
			}
			else
			{
				StartCoroutine(PostHttp(url, parameters.data, new {}, CommonResponseBody.Load, callback));
			}
		}


		/********************************************************************************
			Internal API Group
		*********************************************************************************/


		/// <summary>
		/// Sets the debug.
		/// </summary>
		/// <param name="debugFlag">If set to <c>true</c> debug flag.</param>
		public void SetDebug()
		{
			isDebug = true;
		}

		/// <summary>
		/// Sets the zone.
		/// </summary>
		/// <param name="zone">Zone.</param>
		public void SetZone(Hive5APIZone zone)
		{
			switch(zone)
			{
				// Beta Server
				case Hive5APIZone.Beta:
					host = APIServer.BetaHost;
				break;

				// Real Server
				case Hive5APIZone.Production:
					host = APIServer.ProductionHost;
				break;
			}

		}

		/// <summary>
		/// Asyncs the routine.
		/// </summary>
		/// <param name="routine">Routine.</param>
		public void AsyncRoutine(IEnumerator routine)
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
		public IEnumerator GetHttp(string url, List<KeyValuePair<string, string>> parameters, Hive5Response.dataLoader loader, CallBack callBack)
		{
			// Hive5 API Header 설정
			var headers = new Hashtable();
			headers.Add(HeaderKey.AppKey, this.appKey);
			headers.Add(HeaderKey.Uuid, this.uuid);
			headers.Add(HeaderKey.Token, this.accessToken);
			headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);

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
		/// Build QueryString
		/// </summary>
		/// <returns>The query string.</returns>
		/// <param name="parameters">Parameters.</param>
		private string GetQueryString(List<KeyValuePair<string, string>> parameters)
		{
			if (parameters == null)
			    return string.Empty;

			// Using StringBuilder is faster than concating string by + operator repeatly 
			StringBuilder sb = new StringBuilder ();
			foreach (KeyValuePair<string, string> parameter in parameters)
			{
				if (sb.Length > 0)	
				{
					sb.Append("&");
				}
				
				sb.Append(parameter.Key + "=" + parameter.Value);
			}

			return sb.ToString ();
		}

		/// <summary>
		/// Https the post.
		/// </summary>
		/// <returns>The post.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		public IEnumerator PostHttp(string url, List<KeyValuePair<string, string>> parameters, object requestBody, Hive5Response.dataLoader loader, CallBack callBack)
		{	
			// Hive5 API Header 설정
			var headers = new Hashtable();
			headers.Add(HeaderKey.AppKey, this.appKey);
			headers.Add(HeaderKey.Uuid, this.uuid);
			headers.Add(HeaderKey.Token, this.accessToken);
			headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);
			
			// Hive5 API json body 변환
			string jsonString = JsonMapper.ToJson (requestBody);
			
			var encoding	= new System.Text.UTF8Encoding();
			
			
			string queryString = GetQueryString(parameters);		
			
			string newUrl = url;
			
			if (queryString.Length > 0)
			{
				newUrl = url + "?" + queryString;
			}
			
			// Hive5 API Request
			WWW www = new WWW(newUrl, encoding.GetBytes(jsonString), headers); 
			yield return www;
			
			if(this.isDebug) Debug.Log ("www reuqest URL = " + url);
			if(this.isDebug) Debug.Log ("www request jsonBody= " + jsonString);
			if(this.isDebug) Debug.Log ("www response = " + www.text);
			
			callBack (Hive5Response.Load (loader, www.text));
		}

		/// <summary>
		/// Https the post.
		/// </summary>
		/// <returns>The post.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		public IEnumerator PostHttp(string url, object requestBody, Hive5Response.dataLoader loader, CallBack callBack)
		{	
			// Hive5 API Header 설정
			var headers = new Hashtable();
			headers.Add(HeaderKey.AppKey, this.appKey);
			headers.Add(HeaderKey.Uuid, this.uuid);
			headers.Add(HeaderKey.Token, this.accessToken);
			headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);

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
		public string InitializeUrl(string path)
		{
			return String.Join("/", new String[] { this.host, this.version, path });	
		}
		
		/// <summary>
		/// Gets the headers.
		/// </summary>
		/// <returns>The headers.</returns>
		private Dictionary<string, string> GetHeaders()
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			
			result.Add(HeaderKey.AppKey, this.appKey);
			result.Add(HeaderKey.Uuid, this.uuid);

			if (this.accessToken.Length > 0)
				result.Add(HeaderKey.Token, this.accessToken);
			
			return result;
		}

		/// <summary>
		/// Sets the access token.
		/// </summary>
		/// <param name="accessToken">Access token.</param>
		public void SetAccessToken(string accessToken)
		{
			this.accessToken = accessToken;
		}

		/// <summary>
		/// Sets the UUID.
		/// </summary>
		/// <param name="uuid">UUID.</param>
		private void SetUuid(string uuid)
		{
			this.uuid = uuid;
		}

	}

}
