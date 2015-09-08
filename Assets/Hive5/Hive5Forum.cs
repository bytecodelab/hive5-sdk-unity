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
	/// Hive5 Forum features
	/// </summary>
    public class Hive5Forum
    {
        /** 
		* @api {POST} ListThreads 포럼쓰레드 목록 얻어오기
		* @apiVersion 0.3.11-beta
		* @apiName ListThreads
		* @apiGroup Forum
		*
		* @apiParam {string} forumKey 포럼 키
        * @apiParam {integer?} offset 옵셋
        * @apiParam {integer?} limit 개수제한
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.ListThreads("your-forum-key", callback);
		*/
		public void ListThreads(string forumKey, int? offset, int? limit, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Forum.ListThreads, forumKey));

            var parameter = new List<KeyValuePair<string, string>>();
            if (offset != null)
            {
                parameter.Add(new KeyValuePair<string, string>("offset", offset.ToString()));
            }

            if (limit != null)
            {
                parameter.Add(new KeyValuePair<string, string>("limit", limit.ToString()));
            }
         
            Hive5Http.Instance.GetHttpAsync(url, parameter, ListThreadsResponseBody.Load, callback);
		}	

        /** 
		* @api {POST} CountThreads 포럼쓰레드 개수
		* @apiVersion 0.3.11-beta
		* @apiName CountThreads
		* @apiGroup Forum
		*
		* @apiParam {string} forumKey 포럼 키
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CountThreads("your-forum-key", callback);
		*/
		public void CountThreads(string forumKey, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Forum.CountThreads, forumKey));

            Hive5Http.Instance.GetHttpAsync(url, null, CountThreadsResponseBody.Load, callback);
		}	

       /** 
		* @api {POST} CreateThread 포럼쓰레드 쓰기
		* @apiVersion 0.3.11-beta
		* @apiName CreateThread
		* @apiGroup Forum
		*
		* @apiParam {string} forumKey 포럼 키
		* @apiParam {string} title 쓰레드 제목
		* @apiParam {string} content 쓰레드 내용
		* @apiParam {string} extrasJson 보조 데이터 (Json 형식)
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateThread("your-forum-key", "thead-title", "thread-content", "{ heart: 2}", callback);
		*/
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

        /** 
		* @api {POST} UpdateThread 포럼쓰레드 쓰기
		* @apiVersion 0.3.11-beta
		* @apiName UpdateThread
		* @apiGroup Forum
		*
		* @apiParam {string} forumKey 포럼 키
        * @apiParam {long} threadId 쓰레드 고유아이디
        * @apiParam {string} title 쓰레드 제목
		* @apiParam {string} content 쓰레드 내용
		* @apiParam {string} extrasJson 보조 데이터 (Json 형식)
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateThread("your-forum-key", "thead-title", "thread-content", "{ heart: 2}", callback);
		*/
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

        /** 
		* @api {POST} DeleteThread 포럼쓰레드 삭제
		* @apiVersion 0.3.11-beta
		* @apiName DeleteThread
		* @apiGroup Forum
		*
		* @apiParam {string} forumKey 포럼 키
		* @apiParam {long} threadId 쓰레드 고유아이디
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.DeleteThread("your-forum-key", deletingThreadId, callback);
		*/
		public void DeleteThread(string forumKey, long threadId, Callback callback)
		{
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Forum.DeleteThread, forumKey));

            Hive5Http.Instance.DeleteHttpAsync(url, null, DeleteThreadResponseBody.Load, callback);
		}	
    }
}
