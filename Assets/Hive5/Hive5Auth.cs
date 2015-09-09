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


namespace Hive5
{
    /// <summary>
    /// Hive5 인증에 관련된 모든 것을 포함한 클래스
    /// </summary>
    public class Hive5Auth
    {
        /// <summary>
        /// 접근토큰
        /// </summary>
        /// <remarks>로그인 성공 시 유효한 값을 갖는다.</remarks>
        public string AccessToken { get; private set; }
        /// <summary>
        /// 세션키
        /// </summary>
        /// <remarks>로그인 성공 시 유효한 값을 갖는다. 중복 로그인을 감지하는 용도로 사용.</remarks>
        public string SessionKey { get; private set; }
        /// <summary>
        /// 로그인 여부
        /// </summary>
        public bool IsLoggedIn { get; private set; }

        /// <summary>
        /// 로그인
        /// </summary>
        /// <param name="os">운영체제. 예) "android", "ios"</param>
        /// <param name="build">클라이언트 빌드 버전. 예) "1.0.0"</param>
        /// <param name="locale">국가언어코드. 예) "ko-KR"</param>
        /// <param name="user">사용자</param>
        /// <param name="callback">콜백함수</param>
        /// <code language="cs">
        /// Hive5Client.Auth.Login (OSType.Android, "1.0.0", "ko-KR", null, (response) => {
        ///	  Logger.Log ("response = "+ response.ResultData);
        ///	}
        /// </code>
        public void LogIn(string os, string build, string locale, User user, Callback callback)
        {
            // Hive5 API URL 초기화
            var url = Hive5Client.ComposeRequestUrl(ApiPath.Auth.LogIn);

            Logger.Log("login LoginState=" + this.IsLoggedIn);

            var requestBody = new
            {
                user = user,
                os = os,
                build = build,
                locale = locale
            };

            Hive5Http.Instance.PostHttpAsync(url, requestBody, LoginResponseBody.Load, (response) =>
            {
                if (response.ResultCode == Hive5ErrorCode.Success)
                {
                    var body = response.ResultData as LoginResponseBody;
                    if (body != null)
                    {
                        this.SetAccessToken(body.AccessToken, body.SessionKey);
                        if (string.IsNullOrEmpty(body.AccessToken) == false &&
                            string.IsNullOrEmpty(body.SessionKey) == false)
                        {
                            this.IsLoggedIn = true;
                        }
                    }
                }

                callback(response);
            });
        }

        /// <summary>
        /// 탈퇴
        /// </summary>
        /// <param name="callback">콜백 함수</param>
        /// <code language="cs">Hive5Client.Auth.Unregister(callback)</code>
        public void Unregister(Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(ApiPath.Auth.Unregister);
            Hive5Http.Instance.PostHttpAsync(url, null, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {POST} SwitchPlatform 로그인 플랫폼 바꾸기
        * @apiVersion 0.3.11-beta
        * @apiName SwitchPlatform
        * @apiGroup Auth
        *
        * @apiParam {string} platform 플랫폼타입 PlatformType.Kakao 등
        * @apiParam {string} userId 플랫폼 사용자 아이디
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.SwitchPlatform(PlatformType.Kakao, platformUserId, callback);
        */
        public void SwitchPlatform(string platform, string userId, Callback callback)
        {
            Logger.Log("SwitchPlatform called");

            var url = Hive5Client.ComposeRequestUrl(ApiPath.Auth.SwitchPlatform);

            var requestBody = new
            {
                user = new
                {
                    platform = platform,
                    id = userId
                }
            };

            Logger.Log(url);

            Hive5Http.Instance.PostHttpAsync(url, requestBody, SwitchPlatformResponseBody.Load, callback);
        }

        public void CreatePlatformAccount(string name, string password, Callback callback, string displayName = "", string email = "")
        {
            Logger.Log("CreatePlatformAccount called");

            if (string.IsNullOrEmpty(Hive5Config.XPlatformKey) == true)
                throw new NullReferenceException("Please fill Hive5Config.XPlatformKey");

            var url = Hive5Client.ComposeRequestUrl(ApiPath.Auth.CreatePlatformAccount);

            var requestBody = new
            {
                name = name,
                password = password,
                display_name = displayName,
                email = email,
            };

            Logger.Log(url);

            Hive5Http.Instance.PostHttpAsync(url, requestBody, CreatePlatformAccountResponseBody.Load, callback);
        }

        public void CheckPlatformNameAvailability(string name, Callback callback)
        {
            Logger.Log("CheckPlatformNameAvailability called");

            if (string.IsNullOrEmpty(Hive5Config.XPlatformKey) == true)
                throw new NullReferenceException("Please fill Hive5Config.XPlatformKey");

            var url = string.Format(Hive5Client.ComposeRequestUrl(ApiPath.Auth.CheckPlatformNameAvailability), name);

            Logger.Log(url);

            Hive5Http.Instance.GetHttpAsync(url, null, CheckPlatformNameAvailabilityResponseBody.Load, callback);
        }

        public void CheckPlatformEmailAvailablity(string email, Callback callback)
        {
            Logger.Log("CheckPlatformEmailAvailablity called");

            if (string.IsNullOrEmpty(Hive5Config.XPlatformKey) == true)
                throw new NullReferenceException("Please fill Hive5Config.XPlatformKey");

            var url = string.Format(Hive5Client.ComposeRequestUrl(ApiPath.Auth.CheckPlatformEmailAvailability), email);

            Logger.Log(url);

            Hive5Http.Instance.GetHttpAsync(url, null, CheckPlatformEmailAvailabilityResponseBody.Load, callback);
        }

        public void AuthenticatePlatformAccount(string name, string password, Callback callback)
        {
            Logger.Log("AuthenticatePlatformAccount called");

            if (string.IsNullOrEmpty(Hive5Config.XPlatformKey) == true)
                throw new NullReferenceException("Please fill Hive5Config.XPlatformKey");

            var url = Hive5Client.ComposeRequestUrl(ApiPath.Auth.AuthenticatePlatformAccount);

            var requestBody = new
            {
                name = name,
                password = password,
            };

            Logger.Log(url);

            Hive5Http.Instance.PostHttpAsync(url, requestBody, AuthenticatePlatformAccountResponseBody.Load, callback);
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
    }
}
