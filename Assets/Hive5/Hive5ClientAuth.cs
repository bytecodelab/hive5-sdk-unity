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
        * @apiVersion 0.2.0
        * @apiName Login
        * @apiGroup Auth
        *
        * @apiParam {string} os OSType
        * @apiParam {string[]} objectKeys object key 리스트
        * @apiParam {string[]} configKeys config key 리스트
        * @apiParam {string} platform 플랫폼 Type
        * @apiParam {string} platformUserId 플랫폼 UserId(카카오 ID, GOOGLE ID, FACEBOOK ID ....)
        * @apiParam {string} platformSDKVersion 플랫폼 Version( 3, 4, 5...)
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * string userId 		= "88197xxxx07226176";
        * string sdkVersion 	= "3";
        * 
        * var objectKeys 	= new string[] {""};
        * var configKeys 	= new string[] {"time_event","last_version"};
        * 
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.Login (OSType.Android, objectKeys, configKeys, PlatformType.Google, userId, sdkVersion, response => {
        * 	Logger.Log ("response = "+ response.ResultData);
        * });
        */
        public void Login(string os, string[] objectKeys, string[] configKeys, string platform, string platformUserId, string platformSDKVersion, Callback callback)
        {
            if (!InitState)
                return;

            // Hive5 API URL 초기화
            var url = InitializeUrl(APIPath.PlatformLogin);

            Logger.Log("login LoginState=" + LoginState);

            // Hive5 API 파라미터 셋팅
            TupleList<string, string> parameters = new TupleList<string, string>();
            parameters.Add(ParameterKey.PlatformUserId, platformUserId);
            parameters.Add(ParameterKey.PlatformSdkVersion, platformSDKVersion);
            parameters.Add(ParameterKey.Platform, platform);
            parameters.Add(ParameterKey.OS, os);

            Array.ForEach(objectKeys, key => { parameters.Add(ParameterKey.ObjectKey, key); });
            Array.ForEach(configKeys, key => { parameters.Add(ParameterKey.ConfigKey, key); });

            GetHttpAsync(url, parameters.data, LoginResponseBody.Load, (response) =>
            {
                if (response.ResultCode == Hive5ResultCode.Success)
                {
                    SetAccessToken(((LoginResponseBody)response.ResultData).AccessToken);
                }
                this.loginState = true;
                callback(response);
            }
        );

        }


        /** 
        * @api {post} Logout 로그아웃
        * @apiVersion 0.2.0
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
            string accessToken = this.accessToken;

            throw new NotImplementedException();
        }

        /** 
        * @api {POST} Unregister 탈퇴
        * @apiVersion 0.2.0
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
        * @apiVersion 0.2.0
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
        * @apiVersion 0.2.0
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
        * @apiVersion 0.2.0
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

    }
}
