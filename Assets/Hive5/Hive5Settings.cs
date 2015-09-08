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
	/// Hive5 Settings features
	/// </summary>
    public class Hive5Settings
    {
		/** 
        * @api {GET} CheckNicknameAvailability 닉네임 사용 가능여부 확인
        * @apiVersion 0.3.11-beta
        * @apiName CheckNicknameAvailability
        * @apiGroup Settings
        *
        * @apiParam {string} nickname 닉네임
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.CheckNicknameAvailability("gilbert", callback);
        */
		public void CheckNicknameAvailability(string nickname, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl (String.Format(ApiPath.Settings.CheckNicknameAvailability, Hive5Http.EscapeData(nickname)));

			Hive5Http.Instance.GetHttpAsync(url, null, CommonResponseBody.Load, callback);
		}

		/** 
        * @api {POST} SetNickname 닉네임 설정
        * @apiVersion 0.3.11-beta
        * @apiName SetNickname
        * @apiGroup Settings
        *
        * @apiParam {string} nickname 닉네임
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.SetNickname("gilbert", callback);
        */
		public void SetNickname(string nickname, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl (ApiPath.Settings.SetNickname);
			
			var requestBody = new {
				nickname = nickname,
			};

            Hive5Http.Instance.PostHttpAsync(url, requestBody, SetNicknameResponseBody.Load, callback);
		}

        /** 
		* @api {POST} ActivatePush Push 받기 활성화
		* @apiVersion 0.4.4-beta
		* @apiName ActivatePush
		* @apiGroup Settings
		*
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.ActivatePush(callback)
		*/
		public void ActivatePush(Callback callback)
		{
		    var url = Hive5Client.ComposeRequestUrl(ApiPath.Settings.ActivatePush);
		    Hive5Http.Instance.PostHttpAsync(url, null, ActivatePushResponseBody.Load, callback);
		}

        /** 
		* @api {POST} DeactivatePush Push 받기 비활성화
		* @apiVersion 0.4.4-beta
		* @apiName DeactivatePush
		* @apiGroup Settings
		*
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.DeactivatePush(callback)
		*/
		public void DeactivatePush(Callback callback)
		{
		    var url = Hive5Client.ComposeRequestUrl(ApiPath.Settings.DeactivatePush);
		    Hive5Http.Instance.PostHttpAsync(url, null, ActivatePushResponseBody.Load, callback);
		}

	   /** 
		* @api {POST} UpdatePushToken Push 토큰 등록 및 업데이트
		* @apiVersion 0.3.11-beta
		* @apiName UpdatePushToken
		* @apiGroup Push
		*
		* @apiParam {string} platform 플랫폼 Type( Android, iOS )
		* @apiParam {string} token push 토큰
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.UpdatePushToken( platform, token, callback)
		*/
		public void UpdatePushToken(string platform, string token, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(ApiPath.Settings.UpdatePushToken);
			
			var requestBody = new {
				push_platform 	= platform,
				push_token = token
			};
			
			Hive5Http.Instance.PostHttpAsync(url, requestBody, UpdatePushTokenResponseBody.Load, callback);
		}
	}
}
