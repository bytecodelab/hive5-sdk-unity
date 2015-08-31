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
    public static partial class Hive5Client {

        public static string AppKey { get; private set; }
		public static string Uuid			{ get; private set; }
        public static bool BlockDuplicatedCall { get; set;  }

        public static bool IsInitialized { get; private set; }

        public static bool IsDebugMode { get; private set; }

        /// <summary>
        /// API Host (Url, EndPoint)
        /// </summary>
		private static string _Host;

         /// <summary>
        /// Url for health checking
        /// </summary>
        public static string HealthCheckUrl { get; private set; }

        /// <summary>
        /// API Version
        /// </summary>
		public const string Version = "v6";

        public static Hive5Auth Auth { get; private set; }
        public static Hive5Coupon Coupon { get; private set; }
        public static Hive5DataTable DataTable { get; private set; }
        public static Hive5Forum Forum { get; private set; }
        public static Hive5Leaderboard Leaderboard { get; private set; }
        public static Hive5Mail Mail { get; private set; }
        public static Hive5Misc Misc { get; private set; }
        public static Hive5Purchase Purchase { get; private set; }
        public static Hive5Script Script { get; private set; }
        public static Hive5Settings Settings { get; private set; }
        public static Hive5SocialGraph SocialGraph { get; private set; }
        
        static Hive5Client()
        {
            ApiRequestManager.Instance.Ttl = Hive5Config.DuplicationApiCallExpirationPeriod;
            BlockDuplicatedCall = false;

            Auth = new Hive5Auth();
            Coupon = new Hive5Coupon();
            DataTable = new Hive5DataTable();
            Forum = new Hive5Forum();
            Leaderboard = new Hive5Leaderboard();
            Mail = new Hive5Mail();
            Misc = new Hive5Misc();
            Purchase = new Hive5Purchase();
            Script = new Hive5Script();
            Settings = new Hive5Settings();
            SocialGraph = new Hive5SocialGraph();
        }

        private static bool GetIsDuplicatedCall(Rid rid)
        {
            if (Hive5Client.BlockDuplicatedCall == false)
                return false;

            if (rid == null)
                return false;

            return ApiRequestManager.Instance.CheckRequestAllowed(rid);
        }

		/** 
		* @api {POST} Initialize SDK 초기화
		* @apiVersion 2.0.0
		* @apiName Initialize
		* @apiGroup Root
		*
		* @apiParam {string} uuid   디바이스 고유 UUID
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.Initialize ("your-uuid");
		*/
		public static void Initialize(string uuid)
		{
            Hive5Client.Uuid = uuid;

            Hive5Client._Host = Hive5Config.Host;
            Hive5Client.HealthCheckUrl = Hive5Config.HealthCheckUrl;
			Hive5Client.AppKey = Hive5Config.AppKey;

			Hive5Client.IsInitialized = true;
		}

		/// <summary>
		/// Sets the debug.
		/// </summary>
		public static void SetDebug()
		{
			IsDebugMode = true;
		}

		/// <summary>
		/// Gets base url.
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="path">Path.</param>
		internal static string ComposeRequestUrl(string path)
		{
             if (!Hive5Client.IsInitialized)
                throw new Exception("Not initialized. Please call Init method.");
			
			return String.Join("/", new String[] { Hive5Client._Host, Hive5Client.Version, path });	
		}
		
		/// <summary>
		/// Gets the headers.
		/// </summary>
		/// <returns>The headers.</returns>
		private static Dictionary<string, string> GetHeaders()
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			
			result.Add(HeaderKey.AppKey, Hive5Client.AppKey);
			result.Add(HeaderKey.Uuid, Hive5Client.Uuid);

			if (string.IsNullOrEmpty(Hive5Client.Auth.AccessToken) == false)
				result.Add(HeaderKey.Token, Hive5Client.Auth.AccessToken);

            if (string.IsNullOrEmpty(Hive5Client.Auth.SessionKey) == false)
                result.Add(HeaderKey.SessionKey, Hive5Client.Auth.SessionKey);
			
			return result;
		}

        ///// <summary>
        ///// Sets the UUID.
        ///// </summary>
        ///// <param name="uuid">UUID.</param>
        //private void SetUuid(string uuid)
        //{
        //    this.Uuid = uuid;
        //}

	}

}
