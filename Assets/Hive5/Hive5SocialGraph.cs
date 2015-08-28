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
		* @api {POST} UpdateFriends 친구 리스트 업데이트
		* @apiVersion 0.3.11-beta
		* @apiName UpdateFriends
		* @apiGroup SocialGraph
		*
		* @apiParam {string} groupName A group name to be updated
		* @apiParam {List<Friend>} friends 친구 리스트
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.UpdateFriends("my-friend", friends, callback);
		*/
		public void UpdateFriends(string groupName, List<Friend> friends, Callback callback)
		{
			var url = Hive5Client.Instance.ComposeRequestUrl(ApiPath.SocialGraph.UpdateFriends);
			
			var requestBody = new {
				group=groupName,
				friends = friends,
			};
			
            PostHttpAsync(url, requestBody, UpdateFriendsResponseBody.Load, callback);
		}
	
		/** 
		* @api {POST} AddFriends 친구 리스트 add
		* @apiVersion 0.3.11-beta
		* @apiName AddFriends
		* @apiGroup SocialGraph
		*
		* @apiParam {string} groupName A group name which add friends into
		* @apiParam {List<Friend>} friends 친구 리스트
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.AddFriends(groupName, friends, callback);
		*/
		public void AddFriends(string groupName, List<User> friends, Callback callback)
		{
			var url = Hive5Client.Instance.ComposeRequestUrl(ApiPath.SocialGraph.AddFriends);
			
			var requestBody = new {
				group=groupName,
				friends = friends,
			};
			
            PostHttpAsync(url, requestBody, AddFriendsResponseBody.Load, callback);
		}
	
		/** 
		* @api {POST} RemoveFriends 친구 리스트 remove
		* @apiVersion 0.3.11-beta
		* @apiName RemoveFriends
		* @apiGroup SocialGraph
		*
		* @apiParam {string} groupName A group name which remove friends from
		* @apiParam {List<Friend>} friends 친구리스트
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.RemoveFriends(groupName, friends, callback);
		*/
		public void RemoveFriends(string groupName, List<Friend> friends, Callback callback)
		{
			var url = Hive5Client.Instance.ComposeRequestUrl(ApiPath.SocialGraph.RemoveFriends);
			
			var requestBody = new {
				group=groupName,
				friends = friends,
			};
		
            PostHttpAsync(url, requestBody, RemoveFriendsResponseBody.Load, callback);
		}

		/** 
		* @api {GET} ListFriends 친구 리스트 가져오기 from a group
		* @apiVersion 0.3.11-beta
		* @apiName ListFriends
		* @apiGroup SocialGraph
		*
		* @apiParam {string} groupName a group name which retrieve friends from
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetFriends(groupName, CallBack callback);
		*/
		public void ListFriends(string groupName, Callback callback)
		{
			var url = Hive5Client.Instance.ComposeRequestUrl (ApiPath.SocialGraph.ListFriends);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> requestParam = new TupleList<string, string>();
			requestParam.Add ("group", groupName);

			// WWW 호출
            GetHttpAsync(url, requestParam.data, ListFriendsResponseBody.Load, callback);
		}
	}

}
