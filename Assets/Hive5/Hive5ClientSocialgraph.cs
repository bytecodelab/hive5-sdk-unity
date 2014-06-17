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
#if TEST    
    public partial class Hive5Client : MockMonoSingleton<Hive5Client> {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif

		/********************************************************************************
			SocialGraph API Group
		*********************************************************************************/
		
		/** 
		* @api {POST} UpdateFriends 친구 리스트 업데이트
		* @apiVersion 1.0.0
		* @apiName UpdateFriends
		* @apiGroup SocialGraph
		*
		* @apiParam {string[]} friend_ids 친구 ID 리스트
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.UpdateFriends(friend_ids, callback);
		*/
		public void UpdateFriends(string[] friend_ids, CallBack callback)
		{
			// Hive5 API URL 초기화	
			var url = InitializeUrl(APIPath.UpdateFriends);
			
			// Request Body
			var requestBody = new {
				friends = friend_ids
			};
			
			PostHttpAsync(url, requestBody, UpdateFriendsResponseBody.Load, callback);
		}
		
		/** 
		* @api {GET} GetFriendsInfo 친구 리스트 가져오기
		* @apiVersion 1.0.0
		* @apiName GetFriendsInfo
		* @apiGroup SocialGraph
		*
		* @apiParam {string[]} friend_ids 친구 ID 리스트
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetFriendsInfo(platformUserIds, CallBack callback);
		*/
		public void GetFriendsInfo(string[] platformUserIds, CallBack callback)
		{
			var url = InitializeUrl (APIPath.GetFriendsInfo);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( platformUserIds, key => { parameters.Add( ParameterKey.PlatformUserId, key ); }); 
			
			// WWW 호출
            GetHttpAsync(url, parameters.data, GetFriendsInfoResponseBody.Load, callback);
		}


	}

}
