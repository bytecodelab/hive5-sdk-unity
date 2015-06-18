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
    public partial class Hive5Client : MockMonoSingleton<Hive5Client>
    {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif
        /********************************************************************************
			Auth API Group
		*********************************************************************************/

        /** 
        * @api {GET} Login 로그인
        * @apiVersion 1.0.0-alpha
        * @apiName Login
        * @apiGroup Auth
        *
        * @apiParam {string} os OSType(android, ios)
        * @apiParam {string} userPlatform 플랫폼 Type
        * @apiParam {string} userId 플랫폼 UserId(카카오 ID, GOOGLE ID, FACEBOOK ID ....)
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * string userPlatform = "kakao";
        * string userId 		= "88197xxxx07226176";
        * 
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.Login (OSType.Android, userPlatform, userId, response => {
        * 	Logger.Log ("response = "+ response.ResultData);
        * });
        */
        public void Login(string os, string userPlatform, string userId, Callback callback)
        {
            if (!InitState)
                return;

            // Hive5 API URL 초기화
            var url = InitializeUrl(APIPath.PlatformLogin);

            Logger.Log("login LoginState=" + LoginState);

            var requestBody = new
            {
                user = new { 
                    userPlatform = userPlatform,
                    userId = userId,
                },
                os = os,
            };'

            PostHttpAsync(url, requestBody, LoginResponseBody.Load, (response) =>
            {
                if (response.ResultCode == Hive5ResultCode.Success)
                {
                    var body = response.ResultData as LoginResponseBody;
                    if(body != null)
                    {
                        SetAccessToken(body.AccessToken, body.SessionKey);
                    }
                }
                this.loginState = true;
                callback(response);
            });
        }

        /** 
        * @api {post} Logout 로그아웃
        * @apiVersion 1.0.0-alpha
        * @apiName Logout
        * @apiGroup Auth
        *
        * @apiParam {String} userId 유저 ID
        * @apiParam {String} accessToken Login SDK 에서 응답 받은 accessToken
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.Logout(userId, accessToken, callback);
        */
        public void Logout(string userId, Callback callback)
        {
            string accessToken = string.Empty;
            this.SessionKey = string.Empty;

            throw new NotImplementedException();
        }

        /** 
        * @api {POST} Unregister 탈퇴
        * @apiVersion 1.0.0-alpha
        * @apiName Unregister
        * @apiGroup Auth
        *
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.Unregister(callback);
        */
        public void Unregister(Callback callback)
        {
            var url = InitializeUrl(APIPath.Unregister);

            // WWW 호출
            PostHttpAsync(url, new { }, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {POST} SubmitAgreements 약관 동의
        * @apiVersion 1.0.0-alpha
        * @apiName SubmitAgreements
        * @apiGroup Auth
        *
        * @apiParam {string} generalVersion 약관 버전
        * @apiParam {string} partnershipVersion 파트너쉽 버전
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.SubmitAgreements(generalVersion, partnershipVersion, callback);
        */
        public void SubmitAgreements(string generalVersion, string partnershipVersion, Callback callback)
        {
            var url = InitializeUrl(APIPath.Agreement);

            var requestBody = new
            {
                general_agreement = generalVersion,
                partnership_agreement = partnershipVersion
            };

            // WWW 호출
            PostHttpAsync(url, requestBody, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {GET} GetAgreements 약관 동의 내역보기
        * @apiVersion 1.0.0-alpha
        * @apiName GetAgreements
        * @apiGroup Auth
        *
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.GetAgreements(callback);
        */
        public void GetAgreements(Callback callback)
        {
            var url = InitializeUrl(APIPath.Agreement);

            // Hive5 API 파라미터 셋팅
            TupleList<string, string> parameters = new TupleList<string, string>();

            // WWW 호출           
            GetHttpAsync(url, parameters.data, GetAgreementsResponseBody.Load, callback);
        }

        /** 
        * @api {POST} SwitchPlatform 로그인 플랫폼 바꾸기
        * @apiVersion 1.0.0-alpha
        * @apiName SwitchPlatform
        * @apiGroup Auth
        *
        * @apiParam {string} platformType 플랫폼타입 PlatformType.Kakao 등
        * @apiParam {string} platformUserId 플랫폼 사용자 아이디
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.SwitchPlatform(PlatformType.Kakao, platformUserId, callback);
        */
        public void SwitchPlatform(string platformType, string platformUserId, Callback callback)
        {
            
            Logger.Log("SwitchPlatform called");

            var url = InitializeUrl(APIPath.SwitchPlatform);

            var requestBody = new
            {
                platform = platformType,
                platform_user_id = platformUserId
            };

            Logger.Log(url);

            // WWW 호출
            PostHttpAsync(url, requestBody, SwitchPlatformResponseBody.Load, callback);
        }

		public void CreatePlatformAccount(string name, string password, Callback callback, string displayName = "", string email = "")
		{
			Logger.Log("CreatePlatformAccount called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 


			var url = InitializeUrl(APIPath.CreatePlatformAccount);
			
			var requestBody = new
			{
				name = name,
				password = password,
				display_name = displayName,
				email = email,
			};
			
			Logger.Log(url);
			
			// WWW 호출
			PostHttpAsync(url, requestBody, CreatePlatformAccountResponseBody.Load, callback);
		}

		public void CheckPlatformNameAvailability(string name, Callback callback)
		{
			Logger.Log("CheckPlatformNameAvailability called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 

			var url = string.Format(InitializeUrl(APIPath.CheckPlatformNameAvailability), name);
		
			Logger.Log(url);
		
			// WWW 호출           
			GetHttpAsync(url, null, CheckPlatformNameAvailabilityResponseBody.Load, callback);
		}

		public void CheckPlatformEmailAvailablity(string email, Callback callback)
		{
			Logger.Log("CheckPlatformEmailAvailablity called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 

			var url = string.Format(InitializeUrl(APIPath.CheckPlatformEmailAvailability), email);
			
			Logger.Log(url);
			
			// WWW 호출           
			GetHttpAsync(url, null, CheckPlatformEmailAvailabilityResponseBody.Load, callback);
		}

		public void AuthenticatePlatformAccount(string name, string password, Callback callback)
		{
			Logger.Log("AuthenticatePlatformAccount called");
			
			if (string.IsNullOrEmpty (Hive5Config.XPlatformKey) == true)
				throw new NullReferenceException ("Please fill Hive5Config.XPlatformKey"); 

			var url = InitializeUrl(APIPath.AuthenticatePlatformAccount);
			
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
