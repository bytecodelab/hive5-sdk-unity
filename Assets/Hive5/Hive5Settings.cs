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
    /// 설정에 대한 모든 기능을 포함하는 클래스
    /// </summary>
    public class Hive5Settings
    {
        /// <summary>
        /// 닉네임 사용 가능여부 확인
        /// </summary>
        /// <param name="nickname">닉네임</param>
        /// <param name="callback">콜백 함수</param>
        public void CheckNicknameAvailability(string nickname, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl (String.Format(ApiPath.Player.CheckNicknameAvailability, Hive5Http.EscapeData(nickname)));

			Hive5Http.Instance.GetHttpAsync(url, null, CommonResponseBody.Load, callback);
		}

		/// <summary>
        /// 닉네임을 설정합니다.
        /// </summary>
        /// <param name="nickname">닉네임</param>
        /// <param name="callback">콜백 함수</param>
		public void SetNickname(string nickname, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl (ApiPath.Player.SetNickname);
			
			var requestBody = new {
				nickname = nickname,
			};

            Hive5Http.Instance.PostHttpAsync(url, requestBody, SetNicknameResponseBody.Load, callback);
		}

        /// <summary>
        /// 푸시 알림 받기를 활성화합니다.
        /// </summary>
        /// <param name="callback"></param>
		public void ActivatePush(Callback callback)
		{
		    var url = Hive5Client.ComposeRequestUrl(ApiPath.Settings.ActivatePush);
		    Hive5Http.Instance.PostHttpAsync(url, null, ActivatePushResponseBody.Load, callback);
		}

        /// <summary>
        /// 푸시 알림 받기를 비활성화합니다.
        /// </summary>
        /// <param name="callback"></param>
		public void DeactivatePush(Callback callback)
		{
		    var url = Hive5Client.ComposeRequestUrl(ApiPath.Settings.DeactivatePush);
		    Hive5Http.Instance.PostHttpAsync(url, null, ActivatePushResponseBody.Load, callback);
		}

        /// <summary>
        /// 푸시 토큰을 업데이트합니다.
        /// </summary>
        /// <param name="platform">플랫폼</param>
        /// <param name="token">토큰</param>
        /// <param name="callback">콜백함수</param>
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
