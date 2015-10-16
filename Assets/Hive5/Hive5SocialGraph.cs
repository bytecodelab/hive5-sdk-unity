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
    /// 친구에 대한 모든 기능을 포함하는 클래스
    /// </summary>
    public class Hive5SocialGraph
    {
		/// <summary>
        /// 친구리스트를 업데이트합니다.
        /// </summary>
        /// <param name="groupName">친구 그룹명</param>
        /// <param name="friends">친구 목록</param>
        /// <param name="callback">콜백 함수</param>
		public void UpdateFriends(string groupName, List<Friend> friends, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(ApiPath.SocialGraph.UpdateFriends);
			
			var requestBody = new {
				group=groupName,
				friends = friends,
			};
			
            Hive5Http.Instance.PostHttpAsync(url, requestBody, UpdateFriendsResponseBody.Load, callback);
		}

        /// <summary>
        /// 친구리스트에 친구들을 추가합니다.
        /// </summary>
        /// <param name="groupName">친구 그룹명</param>
        /// <param name="friends">추가할 친구 목록</param>
        /// <param name="callback">콜백 함수</param>
        public void AddFriends(string groupName, List<User> friends, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(ApiPath.SocialGraph.AddFriends);
			
			var requestBody = new {
				group=groupName,
				friends = friends,
			};
			
            Hive5Http.Instance.PostHttpAsync(url, requestBody, AddFriendsResponseBody.Load, callback);
		}

        /// <summary>
        /// 친구 리스트에서 친구를 제거합니다.
        /// </summary>
        /// <param name="groupName">친구 그룹명</param>
        /// <param name="friends">제거할 친구 목록</param>
        /// <param name="callback">콜백 함수</param>
        public void RemoveFriends(string groupName, List<Friend> friends, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl(ApiPath.SocialGraph.RemoveFriends);
			
			var requestBody = new {
				group=groupName,
				friends = friends,
			};
		
            Hive5Http.Instance.PostHttpAsync(url, requestBody, RemoveFriendsResponseBody.Load, callback);
		}

		/// <summary>
        /// 친구 목록 얻어오기
        /// </summary>
        /// <param name="groupName">친구 그룹명</param>
        /// <param name="callback">콜백 함수</param>
		public void ListFriends(string groupName, Callback callback)
		{
			var url = Hive5Client.ComposeRequestUrl (ApiPath.SocialGraph.ListFriends);
			
			Dictionary<string, string> requestParam = new Dictionary<string, string>();
			requestParam.Add ("group", groupName);

            Hive5Http.Instance.GetHttpAsync(url, requestParam, ListFriendsResponseBody.Load, callback);
		}
	}

}
