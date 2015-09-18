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
    /// 우편함(In-game Mail)에 대한 모든 기능을 포함하는 클래스
    /// </summary>
    public class Hive5Mail
    {
        /// <summary>
        /// 조회할 메일 개수 최대값
        /// </summary>
        public const int MaxListMailCount = 20;

        /// <summary>
        /// 메일 생성하기
        /// </summary>
        /// <param name="content">메일 본문</param>
        /// <param name="receiver">받는사람</param>
        /// <param name="extrasJson">추가데이터 (JSON)</param>
        /// <param name="tags">메일 Tags</param>
        /// <param name="callback">콜백 함수</param>
        public void Create(string content, User receiver, string extrasJson, string[] tags, Callback callback)
		{
			if (string.IsNullOrEmpty (receiver.platform) == true)
				throw new NullReferenceException ("friendPlatform should not be empty!");

			var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.Create));
			
			var requestBody = new {
				content	= content,
                user = new {
				    platform = receiver.platform,
				    id = receiver.id,
                },
                extrasJson = extrasJson,
				tags = tags
			};
			
			Hive5Http.Instance.PostHttpAsync(url, requestBody, CreateMailResponseBody.Load, callback);
		}

        /// <summary>
        /// 우편함 메일 리스트 가져오기
        /// </summary>
        /// <param name="order">정렬 순서</param>
        /// <param name="offset">가져올 처음 위치</param>
        /// <param name="limit">가져올 개수 (최대 20개)</param>
        /// <param name="tag"></param>
        /// <param name="callback"></param>
        public void List(DataOrder order, int offset, int limit, string tag, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(ApiPath.Mail.List);

            int safeLimit = Math.Min(Hive5Mail.MaxListMailCount, limit);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("order", Converter.OrderToString(order));
            parameters.Add("offset", offset.ToString());
            parameters.Add("limit", safeLimit.ToString());
            if (string.IsNullOrEmpty(tag) == false)
                parameters.Add("tag", tag);

            Hive5Http.Instance.GetHttpAsync(url, parameters, ListMailsResponseBody.Load, callback);
        }

        /// <summary>
        /// 우편함 메일의 개수를 반환합니다.
        /// </summary>
        /// <param name="tag">메일 Tag</param>
        /// <param name="callback">콜백 함수</param>
        public void CountMail(string tag, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(ApiPath.Mail.Count);

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            if (string.IsNullOrEmpty(tag) == false)
                parameters.Add("tag", tag);

            Hive5Http.Instance.GetHttpAsync(url, parameters, CountMailsResponseBody.Load, callback);
        }
       
        /// <summary>
        /// 우편함 메일을 수정합니다.
        /// </summary>
        /// <param name="mailId">메일의 고유아이디</param>
        /// <param name="content">메일의 내용</param>
        /// <param name="extrasJson">추가데이터 (JSON)</param>
        /// <param name="callback">콜백 함수</param>
        public void UpdateMail(string mailId, string content, string extrasJson, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.Update, mailId));

            var requestBody = new
            {
                content = content,
                extras = extrasJson,
            };

            Hive5Http.Instance.PutHttpAsync(url, requestBody, CommonResponseBody.Load, callback);
        }

        /// <summary>
        /// 우편함의 메일을 삭제합니다.
        /// </summary>
        /// <param name="mailId">메일의 고유아이디</param>
        /// <param name="callback">콜백 함수</param>
        public void DeleteMail(string mailId, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.Delete, mailId));

            Hive5Http.Instance.DeleteHttpAsync(url, new { }, CommonResponseBody.Load, callback);
        }

        /// <summary>
        /// 제한 개수를 초과한 메일을 우편함에서 삭제
        /// </summary>
        /// <param name="limit">메일 개수 제한</param>
        /// <param name="callback">콜백 함수</param>
        public void DeleteMailOverLimit(int limit, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.DeleteOverLimit, limit));

            Hive5Http.Instance.PostHttpAsync(url, new { }, CommonResponseBody.Load, callback);
        }
        
        /// <summary>
        /// 오래된 메일을 우편함에서 삭제합니다.
        /// </summary>
        /// <param name="days">일수를 지정합니다. 1이라면 1일 이전의 메일을 모두 삭제합니다.</param>
        /// <param name="callback">콜백 함수</param>
        public void DeleteMailOlderThan(int days, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.DeleteOlderThan, days));

            Hive5Http.Instance.PostHttpAsync(url, new { }, CommonResponseBody.Load, callback);
        }

        /// <summary>
        /// 특정 메일에 여러 태그를 추가합니다.
        /// </summary>
        /// <param name="mailId">메일의 고유아이디</param>
        /// <param name="tags">추가할 태그의 배열</param>
        /// <param name="callback">콜백 함수</param>
        public void AddTags(string mailId, string[] tags, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.AddTags, mailId));

            var requestBody = new
            {
                tags = tags
            };

            Hive5Http.Instance.PostHttpAsync(url, requestBody, AddMailTagsResponseBody.Load, callback);
        }

        /// <summary>
        /// 특정 메일에 여러 태그를 제거합니다.
        /// </summary>
        /// <param name="mailId">메일의 고유아이디</param>
        /// <param name="tags">태그의 배열</param>
        /// <param name="callback">콜백 함수</param>
        public void RemoveTags(string mailId, string[] tags, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.RemoveTags, mailId));

            var requestBody = new
            {
                tags = tags
            };

            Hive5Http.Instance.PostHttpAsync(url, requestBody, RemoveMailTagsResponseBody.Load, callback);
        }
    }
}
