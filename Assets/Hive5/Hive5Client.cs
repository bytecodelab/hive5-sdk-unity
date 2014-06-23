﻿using UnityEngine;
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
    public delegate IResponseBody dataLoader (JsonData response);
	public delegate void Callback (Hive5Response hive5Response);

    /// <summary>
	/// Hive5 client.
	/// </summary>
#if UNITTEST
    public partial class Hive5Client : MockMonoSingleton<Hive5Client> {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif

        
#if !UNITTEST
        protected Hive5Client() { }
#endif

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

		/********************************************************************************
			Init API Group
		*********************************************************************************/

		/** 
		* @api {POST} Init SDK 초기화
		* @apiVersion 2.0.0
		* @apiName Init
		* @apiGroup Init
		*
		* @apiParam {String} appKey Hive5 발급 AppKey
		* @apiParam {String} uuid   디바이스 고유 UUID
		* @apiParam {Hive5APIZone} zone 접속 서버 선택(Beta OR Real)
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.Init ( "a40e4122-99d9-44a6-xxxx-68ed756f79d6", "747474747", Hive5APIZone.Beta );
		*/
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

		/** 
		* @api {POST} UpdatePushToken Push 토큰 등록 및 업데이트
		* @apiVersion 0.2.0
		* @apiName UpdatePushToken
		* @apiGroup Push
		*
		* @apiParam {String} platform 플랫폼 Type( Android, iOS )
		* @apiParam {String} token push 토큰
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.UpdatePushToken( platform, token, callback)
		*/
		public void UpdatePushToken(string platform, string token, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.UpdatePushToken);
			
			var requestBody = new {
				push_platform 	= platform,
				push_token 		= token
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, UpdatePushTokenResponseBody.Load, callback);
		}


		/********************************************************************************
			Procedures API Group
		*********************************************************************************/

		
		/** 
		* @api {POST} CallProcedure Procedure 호출
		* @apiVersion 0.2.0
		* @apiName CallProcedure
		* @apiGroup Procedure
		*
		* @apiParam {String} procedureName 호출 Procedure 이름
		* @apiParam {TupleList&#60;string, string&#62;} parameters 파라미터
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CallProcedure(procedureName, parameters, callback)
		*/
		public void CallProcedure(string procedureName, TupleList<string, string> parameters,  Callback callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(String.Format(APIPath.CallProcedure, WWW.EscapeURL(procedureName)));
			
			// WWW 호출

		    if (parameters == null)
			{
				PostHttpAsync(url, null, new {}, CallProcedureResponseBody.Load, callback);	
			}
			else
			{
				PostHttpAsync(url, parameters.data, new {}, CallProcedureResponseBody.Load, callback);
			}
		}

		/// <summary>
		/// Hive5 client.
		/// </summary>
		public void Logs(string eventType, string data, Callback callback)
		{
			var url = InitializeUrl (APIPath.Logs);
			
			var requestBody = new {
				event_type = eventType,
				data = data,
			};
			
			// WWW 호출
			PostHttpAsync(url, requestBody, LogsResponseBody.Load, callback);
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
