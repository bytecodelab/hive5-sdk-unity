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
        /// 플레이어 추가 데이터 세팅하기
        /// </summary>
        /// <remarks>
        /// 플레이어의 추가 데이터를 세팅합니다. 약관동의 내역이나, 이메일 등의 정보를 자유로운 형식으로 저장할 수 있으며, 로그인 결과에 이 데이터가 포함됩니다.
        /// </remarks>
        /// <param name="extras">추가 데이터(JSON)</param>
        /// <param name="callback">콜백 함수</param>
		public void SetExtras(string extras, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl (ApiPath.Player.SetExtras);
			
			var requestBody = new {
				extras = extras,
			};

            Hive5Http.Instance.PostHttpAsync(url, requestBody, CommonResponseBody.Load, callback);
		}

        /// <summary>
        /// 플레이어 추가 데이터 가져오기
        /// </summary>
        /// <remarks>
        /// 세팅된 플레이어 추가데이터를 가져옵니다.
        /// </remarks>
        /// <param name="callback">콜백 함수</param>
		public void GetExtras(Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl (ApiPath.Player.SetExtras);
			
            Hive5Http.Instance.GetHttpAsync(url, null, GetPlayerExtrasResponseBody.Load, callback);
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
        /// <param name="platform">플랫폼 (gcm, apns 등)</param>
        /// <param name="token">토큰</param>
        /// <param name="callback">콜백함수</param>
		public void UpdatePushToken(string platform, string token, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(ApiPath.Settings.UpdatePushToken);
			
			var requestBody = new {
				platform = platform,
				token = token
			};
			
			Hive5Http.Instance.PostHttpAsync(url, requestBody, UpdatePushTokenResponseBody.Load, callback);
		}

        /// <summary>
        /// 플레이어 메타데이터 업데이트하기
        /// </summary>
        /// <remarks>
        /// 플레이어의 메타데이터를 세팅합니다. 
        /// </remarks>
        /// <param name="metadata">메타데이터</param>
        /// <param name="callback">콜백 함수</param>
		public void UpdateMetadata(PlayerMetadata metadata, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl (ApiPath.Player.UpdateMetadata);

            Hive5Http.Instance.PostHttpAsync(url, metadata.ToRequestBody(), CommonResponseBody.Load, callback);
		}
	}
}
