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
#if UNITTEST
    public partial class Hive5Client : MockMonoSingleton<Hive5Client> {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif
        /** 
        * @api {GET} Login 로그인
        * @apiVersion 0.3.11-beta
        * @apiName Login
        * @apiGroup Auth
        *
        * @apiParam {string} os OSType
        * @apiParam {string} build 클라이언트 빌드버전
        * @apiParam {string} locale 국가언어코드
        * @apiParam {string} platform 플랫폼 Type
        * @apiParam {string} userId 플랫폼 UserId(카카오 ID, GOOGLE ID, FACEBOOK ID ....)
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * string userId 		= "88197xxxx07226176";
        * string build = "1.0";
        * string locale = "en-US";
        * 
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.Login (OSType.Android, build, locale, PlatformType.Google, userId, response => {
        * 	Logger.Log ("response = "+ response.ResultData);
        * });
        */
        public void LogIn(string os, string build, string locale, string platform, string userId, Callback callback)
        {
            // Hive5 API URL 초기화
            var url = this.ComposeRequestUrl(ApiPath.Auth.LogIn);

            Logger.Log("login LoginState=" + this.IsLoggedIn);

            var requestBody = new
            {
                user = new
                {
                    platform = platform,
                    id = userId,
                },
                os = os,
                build = build,
                locale = locale
            };

            this.PostHttpAsync(url, requestBody, LoginResponseBody.Load, (response) =>
            {
                if (response.ResultCode == Hive5ErrorCode.Success)
                {
                    var body = response.ResultData as LoginResponseBody;
                    if(body != null)
                    {
                        this.SetAccessToken(body.AccessToken, body.SessionKey);
                    }
                }
                this.IsLoggedIn = true;
                callback(response);
            });
        }

       /** 
        * @api {POST} Unregister 탈퇴
        * @apiVersion 0.3.11-beta
        * @apiName Unregister
        * @apiGroup Auth
        *
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.Unregister(callback);
        */
        public void Unregister(Callback callback)
        {
            var url = this.ComposeRequestUrl(ApiPath.Auth.Unregister);

            PostHttpAsync(url, new { }, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {POST} SubmitAgreements 약관 동의
        * @apiVersion 0.3.11-beta
        * @apiName SubmitAgreements
        * @apiGroup Auth
        *
        * @apiParam {string} agreementName 약관의 이름이나 버전
        * @apiParam {string} agreementValue 약관에 동의한 내용
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.SubmitAgreements("1.5", "약관 동의내용", callback);
        */
        public void AcceptAgreement(string agreementName, string agreementValue, Callback callback)
        {
            var url = this.ComposeRequestUrl(ApiPath.Auth.Agreement);

            var requestBody = new
            {
                general_agreement = agreementName,
                partnership_agreement = agreementValue
            };

            // WWW 호출
            PostHttpAsync(url, requestBody, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {GET} ListAgreements 약관 동의 내역보기
        * @apiVersion 0.3.11-beta
        * @apiName ListAgreements
        * @apiGroup Auth
        *
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.GetAgreements(callback);
        */
        public void ListAgreements(Callback callback)
        {
            var url = this.ComposeRequestUrl(ApiPath.Auth.Agreement);

            // Hive5 API 파라미터 셋팅
            TupleList<string, string> parameters = new TupleList<string, string>();

            // WWW 호출           
            GetHttpAsync(url, parameters.data, GetAgreementsResponseBody.Load, callback);
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

            var url = this.ComposeRequestUrl(ApiPath.Auth.SwitchPlatform);

            var requestBody = new
            {
                user = new {
                    platform = platform,
                    id = userId
                }
            };

            Logger.Log(url);

            PostHttpAsync(url, requestBody, SwitchPlatformResponseBody.Load, callback);
        }

		public void CreatePlatformAccount(string name, string password, Callback callback, string displayName = "", string email = "")
		{
			Logger.Log("CreatePlatformAccount called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 


			var url = this.ComposeRequestUrl(ApiPath.Auth.CreatePlatformAccount);
			
			var requestBody = new
			{
				name = name,
				password = password,
				display_name = displayName,
				email = email,
			};
			
			Logger.Log(url);
			
			PostHttpAsync(url, requestBody, CreatePlatformAccountResponseBody.Load, callback);
		}

		public void CheckPlatformNameAvailability(string name, Callback callback)
		{
			Logger.Log("CheckPlatformNameAvailability called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 

			var url = string.Format(ComposeRequestUrl(ApiPath.Auth.CheckPlatformNameAvailability), name);
		
			Logger.Log(url);
		        
			GetHttpAsync(url, null, CheckPlatformNameAvailabilityResponseBody.Load, callback);
		}

		public void CheckPlatformEmailAvailablity(string email, Callback callback)
		{
			Logger.Log("CheckPlatformEmailAvailablity called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 

			var url = string.Format(ComposeRequestUrl(ApiPath.Auth.CheckPlatformEmailAvailability), email);
			
			Logger.Log(url);
			
			// WWW 호출           
			GetHttpAsync(url, null, CheckPlatformEmailAvailabilityResponseBody.Load, callback);
		}

		public void AuthenticatePlatformAccount(string name, string password, Callback callback)
		{
			Logger.Log("AuthenticatePlatformAccount called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 

			var url = this.ComposeRequestUrl(ApiPath.Auth.AuthenticatePlatformAccount);
			
			var requestBody = new
			{
				name = name,
				password = password,
			};
			
			Logger.Log(url);
			
			// WWW 호출
			PostHttpAsync(url, requestBody, AuthenticatePlatformAccountResponseBody.Load, callback);
		}
    }
}
