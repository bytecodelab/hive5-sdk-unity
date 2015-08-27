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

		private bool _IsDebugMode 	= false;

        public bool IsInitialized { get; private set; }
        public bool IsLoggedIn { get; private set; }

        /// <summary>
        /// API Host (Url, EndPoint)
        /// </summary>
		private string _Host;

         /// <summary>
        /// Url for health checking
        /// </summary>
        public string HealthCheckUrl { get; private set; }

        /// <summary>
        /// API Version
        /// </summary>
		public const string Version = "v6";

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

        private bool GetIsDuplicatedCall(Rid rid)
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
        * @apiParam {string} host 접속 주소
		* @apiParam {string} appKey Hive5 발급 AppKey
		* @apiParam {string} uuid   디바이스 고유 UUID
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.Init ("your-uuid");
		*/
		public void Init(string uuid)
		{
            this.Uuid = uuid;

            this._Host = Hive5Config.Host;
            this.HealthCheckUrl = Hive5Config.HealthCheckUrl;
			this.AppKey = Hive5Config.AppKey;

			this.IsInitialized = true;
		}

		/// <summary>
		/// Hive5 client.
		/// </summary>
		public void Logs(string eventType, string data, Callback callback)
		{
			var url = Hive5Client.Instance.ComposeRequestUrl (ApiPath.Misc.Logs);
			
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
			_IsDebugMode = true;
		}

		/// <summary>
		/// Gets base url.
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="path">Path.</param>
		internal string ComposeRequestUrl(string path)
		{
             if (!IsInitialized)
                throw new Exception("Not initialized. Please call Init method.");
			
			return String.Join("/", new String[] { this._Host, Hive5Client.Version, path });	
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
