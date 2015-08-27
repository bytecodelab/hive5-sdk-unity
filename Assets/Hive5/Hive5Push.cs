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
		* @api {POST} Activate Push 토큰 등록 및 업데이트
		* @apiVersion 0.4.4-beta
		* @apiName Activate
		* @apiGroup Push
		*
		* @apiParam {bool} 수신여부
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.TogglePushAccept( true, callback)
		*/
		public void Activate(bool activeFlag, Callback callback)
		{
		    var url = string.Format(Hive5Client.Instance.ComposeRequestUrl(ApiPath.Settings.TogglePushAccept), activeFlag.ToString().ToLower());
		    Hive5Client.Instance.PostHttpAsync(url, null, PushActivateResponseBody.Load, callback);
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
			var url = Hive5Client.Instance.ComposeRequestUrl(ApiPath.Settings.UpdatePushToken);
			
			var requestBody = new {
				push_platform 	= platform,
				push_token = token
			};
			
			Hive5Client.Instance.PostHttpAsync(url, requestBody, UpdatePushTokenResponseBody.Load, callback);
		}
    }
}
