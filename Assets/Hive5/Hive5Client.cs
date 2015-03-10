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
using Assets.Hive5.Model;


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

        


        public string AppKey { get; private set; }
		public string Uuid			{ get; private set; }
		public string AccessToken 	{ get; private set; }
        public string SessionKey { get; private set; }
        public bool BlockDuplicatedCall { get; set;  }

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
        public Hive5APIZone Zone { get; private set; }
		private string host;
		private string version;

#if !UNITTEST
        protected Hive5Client()
#else
        public Hive5Client()
            :base()
        
#endif 
        {
            ApiRequestManager.Instance.Ttl = Hive5Config.DuplicationApiCallExpirationPeriod;
            BlockDuplicatedCall = false;
        }

        public bool GetIsDuplicatedCall(Rid rid)
        {
            if (this.BlockDuplicatedCall == false)
                return false;

            if (rid == null)
                return false;

            return ApiRequestManager.Instance.CheckRequestAllowed(rid);
        }

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
			this.AppKey 	= appKey;
			this.Uuid 		= uuid;
            this.Zone       = zone;

            switch (zone)
            {
                default:
                case Hive5APIZone.Alpha:
                    {
                        this.host = Hive5Config.AlphaHost;
                    }
                    break;
                case Hive5APIZone.Beta:
                    {
                        this.host = Hive5Config.BetaHost;
                    }
                    break;
                case Hive5APIZone.Production:
                    {
                        this.host = Hive5Config.ProductionHost;
                    }
                    break;
            }

			this.version 	= Hive5Config.Version;
			this.initState 	= true;
		}

		/********************************************************************************
			Push API Group
		*********************************************************************************/

		/** 
		* @api {POST} UpdatePushToken Push 토큰 등록 및 업데이트
		* @apiVersion 0.3.11-beta
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

        /** 
		* @api {POST} UpdatePushToken Push 토큰 등록 및 업데이트
		* @apiVersion 0.4.4-beta
		* @apiName TogglePushAccept
		* @apiGroup Push
		*
		* @apiParam {bool} 수신여부
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.TogglePushAccept( true, callback)
		*/
		public void TogglePushAccept(bool accept, Callback callback)
		{
			// Hive5 API URL 초기화
            var url = string.Format(InitializeUrl(APIPath.TogglePushAccept), accept.ToString().ToLower());
			
			// WWW 호출
            PostHttpAsync(url, null, UpdateTogglePushAcceptResponseBody.Load, callback);
		}

		/********************************************************************************
			Procedures API Group
		*********************************************************************************/

		
		/** 
		* @api {POST} CallProcedure Procedure 호출
		* @apiVersion 0.3.11-beta
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
        [Obsolete("CallProcedure is deprecated, please use CallProcedure(string procedureName, string parameters,  Callback callback) instead.")]
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
        /// 
        /// </summary>
        /// <param name="procedureName"></param>
        /// <param name="parameterObject">내부적으로 json화 되어 전송된다.</param>
        /// <param name="callback"></param>
        public void CallProcedure(string procedureName, object parameterObject,  Callback callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(String.Format(APIPath.CallProcedure, WWW.EscapeURL(procedureName)));
			
			// WWW 호출
			PostHttpAsync(url, null, parameterObject, CallProcedureResponseBody.Load, callback);	
		}

        /** 
		* @api {POST} CallProcedureWithoutAuth 로그인하지 않고 Procedure 호출
		* @apiVersion 0.3.11-beta
		* @apiName CallProcedure
		* @apiGroup Procedure
		*
		* @apiParam {String} procedureName 호출 Procedure 이름
		* @apiParam {parameterObject} parameters 파라미터 오브젝트(내부적으로 JSON화 됨)
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
        * var parameters = new {
        *     value1 = 1,
        *     value2 = "abc",
        * }
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CallProcedureWithoutAuth(procedureName, parameters, callback)
		*/
        public void CallProcedureWithoutAuth(string procedureName, object parameterObject,  Callback callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(String.Format(APIPath.CallProcedureWithoutAuth, WWW.EscapeURL(procedureName)));
			
			// WWW 호출
			PostHttpAsync(url, null, parameterObject, CallProcedureResponseBody.Load, callback);	
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
					host = Hive5Config.BetaHost;
				break;

				// Real Server
				case Hive5APIZone.Production:
					host = Hive5Config.ProductionHost;
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
			
			result.Add(HeaderKey.AppKey, this.AppKey);
			result.Add(HeaderKey.Uuid, this.Uuid);

			if (this.AccessToken.Length > 0)
				result.Add(HeaderKey.Token, this.AccessToken);

            if (string.IsNullOrEmpty(this.SessionKey) == false)
                result.Add(HeaderKey.SessionKey, this.SessionKey);
			
			return result;
		}

		/// <summary>
		/// Sets the access token.
		/// </summary>
		/// <param name="accessToken">Access token.</param>
		public void SetAccessToken(string accessToken, string sessionKey)
		{
			this.AccessToken = accessToken;
            this.SessionKey = sessionKey;
		}

		/// <summary>
		/// Sets the UUID.
		/// </summary>
		/// <param name="uuid">UUID.</param>
		private void SetUuid(string uuid)
		{
			this.Uuid = uuid;
		}

	}

}
