using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Models;
using Hive5.Util;
using Hive5.Models;

namespace Hive5
{
    public delegate IResponseBody dataLoader (JsonData response);
	public delegate void Callback (Hive5Response hive5Response);

    /// <summary>
	/// Hive5의 모든 것을 담고 있는 정적 클래스
	/// </summary>
    public static partial class Hive5Client {
        /// <summary>
        /// 앱키
        /// </summary>
        /// <remarks>Hive5 콘솔에서 얻을 수 있는 정보.</remarks>
        public static string AppKey { get; private set; }

        /// <summary>
        /// 디바이스 고유아이디
        /// </summary>
		public static string Uuid			{ get; private set; }

        /// <summary>
        /// 중복호출을 방지할 것인가 여부
        /// </summary>
        public static bool BlockDuplicatedCall { get; set;  }

        /// <summary>
        /// 초기화되었는지 여부
        /// </summary>
        public static bool IsInitialized { get; private set; }

        /// <summary>
        /// 디버그 모드로 작동하고 있는지 여부
        /// </summary>
        public static bool IsDebugMode { get; private set; }

        /// <summary>
        /// API Host (Url, EndPoint)
        /// </summary>
		public static string Host { get; private set; }

        /// <summary>
        /// 서버 헬스 확인 주소(Url)
        /// </summary>
        public static string HealthCheckUrl { get; private set; }

        /// <summary>
        /// API Version
        /// </summary>
		public const string Version = "v6";

        /// <summary>
        /// Hive5Auth의 인스턴스
        /// </summary>
        public static Hive5Auth Auth { get; private set; }
        /// <summary>
        /// Hive5Coupon의 인스턴스
        /// </summary>
        public static Hive5Coupon Coupon { get; private set; }
        /// <summary>
        /// Hive5DataTable의 인스턴스
        /// </summary>
        public static Hive5DataTable DataTable { get; private set; }
        /// <summary>
        /// Forum의 인스턴스
        /// </summary>
        public static Hive5Forum Forum { get; private set; }
        /// <summary>
        /// Hive5Leaderboard의 인스턴스
        /// </summary>
        public static Hive5Leaderboard Leaderboard { get; private set; }
        /// <summary>
        /// Hive5Mail의 인스턴스
        /// </summary>
        public static Hive5Mail Mail { get; private set; }
        /// <summary>
        /// Hive5Purchase의 인스턴스
        /// </summary>
        public static Hive5Purchase Purchase { get; private set; }
        /// <summary>
        /// Hive5Script의 인스턴스
        /// </summary>
        public static Hive5Script Script { get; private set; }
        /// <summary>
        /// Hive5Settings의 인스턴스
        /// </summary>
        public static Hive5Settings Settings { get; private set; }
        /// <summary>
        /// Hive5SocialGraphs의 인스턴스
        /// </summary>
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
            Purchase = new Hive5Purchase();
            Script = new Hive5Script();
            Settings = new Hive5Settings();
            SocialGraph = new Hive5SocialGraph();
        }

        /// <summary>
        /// Rid로 중복호출인지 여부를 확인
        /// </summary>
        /// <param name="rid"></param>
        /// <returns></returns>
        private static bool GetIsDuplicatedCall(Rid rid)
        {
            if (Hive5Client.BlockDuplicatedCall == false)
                return false;

            if (rid == null)
                return false;

            return ApiRequestManager.Instance.CheckRequestAllowed(rid);
        }

        /// <summary>
        /// SDK 초기화하기
        /// </summary>
        /// <param name="uuid">디바이스 고유아이디</param>
        /// <code language="cs">
		/// Hive5Client.Initialize ("your-uuid");
        /// </code>
		public static void Initialize(string uuid)
		{
            Hive5Client.Uuid = uuid;

            Hive5Client.Host = Hive5Config.Host;
            Hive5Client.HealthCheckUrl = Hive5Config.HealthCheckUrl;
			Hive5Client.AppKey = Hive5Config.AppKey;

			Hive5Client.IsInitialized = true;
		}

		/// <summary>
		/// 디버그모드를 설정합니다.
		/// </summary>
		public static void SetDebugMode()
		{
			IsDebugMode = true;
		}

		/// <summary>
		/// 요청URL을 path를 이용하여 만들어 냅니다.
		/// </summary>
		/// <returns>요청URL</returns>
		/// <param name="path">중간경로</param>
		internal static string ComposeRequestUrl(string path)
		{
             if (!Hive5Client.IsInitialized)
                throw new Exception("Not initialized. Please call Init method.");
			
			return String.Join("/", new String[] { Hive5Client.Host, Hive5Client.Version, path });	
		}
	}
}
