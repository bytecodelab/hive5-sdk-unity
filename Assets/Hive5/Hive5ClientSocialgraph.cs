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
		/********************************************************************************
			SocialGraph API Group
		*********************************************************************************/
		
		/** 
		* @api {POST} UpdateFriends 친구 리스트 업데이트
		* @apiVersion 1.0.0
		* @apiName UpdateFriends
		* @apiGroup SocialGraph
		*
		* @apiParam {string} groupName A group name to be updated
		* @apiParam {string[]} friend_ids 친구 ID 리스트
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.UpdateFriends(friend_ids, callback);
		*/
		public void UpdateFriends(string groupName, string[] friend_ids, Callback callback)
		{
			// Hive5 API URL 초기화	
			var url = InitializeUrl(APIPath.UpdateFriends);
			
			// Request Body
			var requestBody = new {
				group=groupName,
				friends = friend_ids
			};
			
			// WWW 호출
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
		public void GetFriendsInfo(string[] platformUserIds, Callback callback)
		{
			var url = InitializeUrl (APIPath.GetFriendsInfo);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( platformUserIds, key => { parameters.Add( ParameterKey.PlatformUserId, key ); }); 
			
			// WWW 호출
            GetHttpAsync(url, parameters.data, GetFriendsInfoResponseBody.Load, callback);
		}

		/** 
		* @api {POST} AddFriends 친구 리스트 add
		* @apiVersion 1.0.0
		* @apiName AddFriends
		* @apiGroup SocialGraph
		*
		* @apiParam {string} groupName A group name which add friends into
		* @apiParam {string[]} friend_ids 친구 ID 리스트
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.AddFriends(groupName, friend_ids, callback);
		*/
		public void AddFriends(string groupName, string[] friend_ids, Callback callback)
		{
			// Hive5 API URL 초기화	
			var url = InitializeUrl(APIPath.AddFriends);
			
			// Request Body
			var requestBody = new {
				group=groupName,
				friends = friend_ids
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, AddFriendsResponseBody.Load, callback);
		}
	
		/** 
		* @api {POST} RemoveFriends 친구 리스트 remove
		* @apiVersion 1.0.0
		* @apiName RemoveFriends
		* @apiGroup SocialGraph
		*
		* @apiParam {string} groupName A group name which remove friends from
		* @apiParam {string[]} friend_ids 친구 ID 리스트
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.RemoveFriends(groupName, friend_ids, callback);
		*/
		public void RemoveFriends(string groupName, string[] friend_ids, Callback callback)
		{
			// Hive5 API URL 초기화	
			var url = InitializeUrl(APIPath.RemoveFriends);
			
			// Request Body
			var requestBody = new {
				group=groupName,
				friends = friend_ids
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, RemoveFriendsResponseBody.Load, callback);
		}

		/** 
		* @api {GET} GetFriends 친구 리스트 가져오기 from a group
		* @apiVersion 1.0.0
		* @apiName GetFriends
		* @apiGroup SocialGraph
		*
		* @apiParam {string} groupName a group name which retrieve friends from
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetFriends(groupName, CallBack callback);
		*/
		public void GetFriends(string groupName, Callback callback)
		{
			var url = InitializeUrl (APIPath.GetFriends);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("group", groupName);

			// WWW 호출
            GetHttpAsync(url, parameters.data, GetFriendsResponseBody.Load, callback);
		}
	}

}
