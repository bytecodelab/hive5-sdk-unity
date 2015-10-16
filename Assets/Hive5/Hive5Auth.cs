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
        /// <param name="platformParams">플랫폼 파라미터(JSON)</param>
        /// <param name="callback">콜백함수</param>
        /// <code language="cs">
        /// Hive5Client.Auth.Login ("android", "1.0.0", "ko-KR", null, (response) => {
        ///	  Logger.Log ("response = "+ response.ResultData);
        ///	}
        /// </code>
        public void LogIn(string os, string build, string locale, User user, string platformParams, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(ApiPath.Auth.LogIn);

            Logger.Log("login LoginState=" + this.IsLoggedIn);

            var requestBody = new
            {
                user = user,
                platform_params = platformParams,
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

        /// <summary>
        /// SwitchPlatform 로그인 플랫폼 바꾸기
        /// </summary>
        /// <param name="platform">플랫폼 종류 예) "facebook" 등</param>
        /// <param name="userId">플랫폼 사용자 고유아이디</param>
        /// <param name="callback">콜백 함수</param>
        /// <code language="cs">Hive5Client.Auth.SwitchPlatform("facebook", userId, (response) => {
        ///     Logger.Log ("response = "+ response.ResultData);
        /// });</code>
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

        ///// <summary>
        ///// 사용자정의 Platform에 계정을 생성합니다.
        ///// </summary>
        ///// <param name="name">이름</param>
        ///// <param name="password">패스워드</param>
        ///// <param name="callback">콜백 함수</param>
        ///// <param name="displayName">표시이름</param>
        ///// <param name="email">이메일</param>
        ///// <code language="cs">Hive5Client.Auth.CreatePlatformAccount("johndoe", "password", (response) => {
        /////     Logger.Log ("response = "+ response.ResultData);
        ///// }, "John Doe", "johndoe@bytecodelab.com");</code>
        //public void CreatePlatformAccount(string name, string password, Callback callback, string displayName = "", string email = "")
        //{
        //    Logger.Log("CreatePlatformAccount called");

        //    if (string.IsNullOrEmpty(Hive5Config.XPlatformKey) == true)
        //        throw new NullReferenceException("Please fill Hive5Config.XPlatformKey");

        //    var url = Hive5Client.ComposeRequestUrl(ApiPath.Auth.CreatePlatformAccount);

        //    var requestBody = new
        //    {
        //        name = name,
        //        password = password,
        //        display_name = displayName,
        //        email = email,
        //    };

        //    Logger.Log(url);

        //    Hive5Http.Instance.PostHttpAsync(url, requestBody, CreatePlatformAccountResponseBody.Load, callback);
        //}

        ///// <summary>
        ///// 사용자정의 플랫폼 계정시스템에게 해당 이름의 사용가능 여부를 확인합니다.
        ///// </summary>
        ///// <remarks>이미 존재하는 이름이거나, 입력한 이름이 금지어인 경우 사용불가</remarks>
        ///// <param name="name">이름</param>
        ///// <param name="callback">콜백 함수</param>
        //public void CheckPlatformNameAvailability(string name, Callback callback)
        //{
        //    Logger.Log("CheckPlatformNameAvailability called");

        //    if (string.IsNullOrEmpty(Hive5Config.XPlatformKey) == true)
        //        throw new NullReferenceException("Please fill Hive5Config.XPlatformKey");

        //    var url = string.Format(Hive5Client.ComposeRequestUrl(ApiPath.Auth.CheckPlatformNameAvailability), name);

        //    Logger.Log(url);

        //    Hive5Http.Instance.GetHttpAsync(url, null, CheckPlatformNameAvailabilityResponseBody.Load, callback);
        //}

        ///// <summary>
        ///// 사용자정의 플랫폼 계정시스템에게 해당 이메일의 사용가능 여부를 확인합니다.
        ///// </summary>
        ///// <remarks>이미 존재하는 이메일인 경우 사용불가</remarks>
        ///// <param name="email">이메일</param>
        ///// <param name="callback">콜백 함수</param>
        //public void CheckPlatformEmailAvailablity(string email, Callback callback)
        //{
        //    Logger.Log("CheckPlatformEmailAvailablity called");

        //    if (string.IsNullOrEmpty(Hive5Config.XPlatformKey) == true)
        //        throw new NullReferenceException("Please fill Hive5Config.XPlatformKey");

        //    var url = string.Format(Hive5Client.ComposeRequestUrl(ApiPath.Auth.CheckPlatformEmailAvailability), email);

        //    Logger.Log(url);

        //    Hive5Http.Instance.GetHttpAsync(url, null, CheckPlatformEmailAvailabilityResponseBody.Load, callback);
        //}

        ///// <summary>
        ///// 사용자정의 플랫폼으로 로그인합니다.
        ///// </summary>
        ///// <remarks>이름과 패스워드를 이용한 2-pass authentication</remarks>
        ///// <param name="name">이름</param>
        ///// <param name="password">패스워드</param>
        ///// <param name="callback">콜백 함수</param>
        //public void AuthenticatePlatformAccount(string name, string password, Callback callback)
        //{
        //    Logger.Log("AuthenticatePlatformAccount called");

        //    if (string.IsNullOrEmpty(Hive5Config.XPlatformKey) == true)
        //        throw new NullReferenceException("Please fill Hive5Config.XPlatformKey");

        //    var url = Hive5Client.ComposeRequestUrl(ApiPath.Auth.AuthenticatePlatformAccount);

        //    var requestBody = new
        //    {
        //        name = name,
        //        password = password,
        //    };

        //    Logger.Log(url);

        //    Hive5Http.Instance.PostHttpAsync(url, requestBody, AuthenticatePlatformAccountResponseBody.Load, callback);
        //}

        /// <summary>
        /// accessToken과 sessionKey를 수동으로 설정합니다.
        /// </summary>
        /// <param name="accessToken">인증 시스템에서 발급하는 접근토큰</param>
        /// <param name="sessionKey">인증 시스템에서 발급하는 세션키</param>
        public void SetAccessToken(string accessToken, string sessionKey)
        {
            this.AccessToken = accessToken;
            this.SessionKey = sessionKey;
        }
    }
}
