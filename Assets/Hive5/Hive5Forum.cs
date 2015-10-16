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
    /// Forum에 대한 모든 기능을 포함하는 클래스
    /// </summary>
    public class Hive5Forum
    {
        /// <summary>
        /// 포럼 내 쓰레드 목록 얻어오기
        /// </summary>
        /// <param name="forumKey">포럼 키</param>
        /// <param name="offset">옵셋</param>
        /// <param name="limit">개수제한</param>
        /// <param name="callback">콜백 함수</param>
        /// <code language="cs">Hive5Client.Forum.ListThreads("your-forum-key", 0, 20, (response) => {
        ///     // your code here
        /// });
		public void ListThreads(string forumKey, DataOrder order, int? offset, int? limit, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Forum.ListThreads, forumKey));

            var parameter = new Dictionary<string, string>();
            if (offset != null)
            {
                parameter.Add("offset", offset.ToString());
            }

            if (limit != null)
            {
                parameter.Add("limit", limit.ToString());
            }

            switch (order)
            {
                case DataOrder.ASC:
                     parameter.Add("order", "asc");
                    break;
                default:
                case DataOrder.DESC:
                     parameter.Add("order", "dec");
                    break;
            }
         
            Hive5Http.Instance.GetHttpAsync(url, parameter, ListThreadsResponseBody.Load, callback);
        }

        /// <summary>
        /// 포럼 내 쓰레드의 개수를 반환합니다.
        /// </summary>
        /// <param name="forumKey">포럼 키</param>
        /// <param name="callback">콜백 함수</param>
        /// <code language="cs">Hive5Client.Forum.CountThreads("your-forum-key", (response) => {
        ///     // your code here
        /// });
        /// </code>
        public void CountThreads(string forumKey, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Forum.CountThreads, forumKey));

            Hive5Http.Instance.GetHttpAsync(url, null, CountThreadsResponseBody.Load, callback);
		}
        
        /// <summary>
        /// 포럼에 쓰레드를 생성합니다.
        /// </summary>
        /// <param name="forumKey">포럼 키</param>
        /// <param name="title">쓰레드 제목</param>
        /// <param name="content">쓰레드 내용</param>
        /// <param name="extrasJson">보조 데이터 (Json 형식)</param>
        /// <param name="callback">콜백 함수</param>
        /// <code language="cs">Hive5Client.Forum.CreateThread("your-forum-key", "thead-title", "thread-content", "{ heart: 2}", callback);
        /// </code>
        public void CreateThread(string forumKey, string title, string content, string extrasJson, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Forum.CreateThread, forumKey));

            var requestBody = new
            {
                title = title,
                content = content,
                extras = extrasJson
            };

            Hive5Http.Instance.PostHttpAsync(url, requestBody, CreateThreadResponseBody.Load, callback);
		}

        /// <summary>
        /// 포럼 쓰레드를 업데이트합니다.
        /// </summary>
        /// <param name="forumKey">포럼 키</param>
        /// <param name="threadId">쓰레드 고유아이디</param>
        /// <param name="title">쓰레드 제목</param>
        /// <param name="content">쓰레드 내용</param>
        /// <param name="extrasJson">보조 데이터 (Json 형식)</param>
        /// <param name="callback">콜백 함수</param>
        /// <code language="cs">Hive5Client.Forum.UpdateThread("your-forum-key", "thead-id", "thead-title", "thread-content", "{ heart: 2}", callback);
        /// </code>
        public void UpdateThread(string forumKey, long threadId, string title, string content, string extrasJson, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Forum.UpdateThread, forumKey, threadId));

            var requestBody = new
            {
                title = title,
                content = content,
                extras = extrasJson
            };

            Hive5Http.Instance.PutHttpAsync(url, requestBody, UpdateThreadResponseBody.Load, callback);
		}

        /// <summary>
        /// 포럼 내 쓰레드 삭제
        /// </summary>
        /// <param name="forumKey">포럼 키</param>
        /// <param name="threadId">쓰레드 고유아이디</param>
        /// <param name="callback">콜백 함수</param>
        /// <code language="cs">Hive5Client.Forum.DeleteThread("your-forum-key", "thead-id", callback);
        /// </code>
        public void DeleteThread(string forumKey, long threadId, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Forum.DeleteThread, forumKey));

            Hive5Http.Instance.DeleteHttpAsync(url, null, DeleteThreadResponseBody.Load, callback);
		}	
    }
}
