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
	/// Hive5 Mail features
	/// </summary>
    public class Hive5Mail
    {
        /** 
		* @api {POST} Create 메일 생성하기
		* @apiVersion 0.3.11-beta
		* @apiName Create
		* @apiGroup Mail
		*
		* @apiParam {string} content 메일 본문
		* @apiParam {User} receiver 받는사람
         * @apiParam {string} extrasJson 추가데이터 (JSON)
		* @apiParam {string[]} tags 메일 Tags
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {string} resultCode Error Code 참고
		* @apiSuccess {string} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateMail(content, receiver, tags, callback);
		*/
        public void Create(string content, User receiver, string extrasJson, string[] tags, Callback callback)
		{
			if (string.IsNullOrEmpty (receiver.platform) == true)
				throw new NullReferenceException ("friendPlatform should not be empty!");

			// Hive5 API URL 초기화
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

        /** 
        * @api {GET} List 메일 리스트 가져오기
        * @apiVersion 0.3.11-beta
        * @apiName List
        * @apiGroup Mail
        *
        * @apiParam {int} limit 받을 메일 갯수
        * @apiParam {string} tag 메일 Tag
        * @apiParam {string} order 메일 순서
        * @apiParam {long} afterMailId 특정 메일 이후의 리스트 받기 위한 mail id
		
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.ListMails(limit, tag, order, afterMailId, callback);
        */
        public void List(int limit, string tag, DataOrder order, long afterMailId, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(ApiPath.Mail.List);

            TupleList<string, string> parameters = new TupleList<string, string>();
            parameters.Add("limit", limit.ToString());
            parameters.Add("order", Converter.OrderToString(order));

            if (afterMailId > 0)
                parameters.Add("after_mail_id", afterMailId.ToString());
            if (string.IsNullOrEmpty(tag) == false)
                parameters.Add("tag", tag);

            Hive5Http.Instance.GetHttpAsync(url, parameters.data, ListMailsResponseBody.Load, callback);
        }

        /** 
        * @api {GET} CountMail 메일 갯수 확인
        * @apiVersion 0.3.11-beta
        * @apiName CountMail
        * @apiGroup Mail
        *
        * @apiParam {DataOrder} order 메일 순서
        * @apiParam {long} afterMailId 특정 메일 이후의 리스트 받기 위한 mail id
        * @apiParam {string} tags 메일 Tag
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.CountMail(order, afterMailId, tag, callback);
        */
        public void CountMail(DataOrder order, string afterMailId, string tag, Callback callback)
        {
            // Hive5 API URL 초기화
            var url = Hive5Client.ComposeRequestUrl(ApiPath.Mail.Count);

            TupleList<string, string> parameters = new TupleList<string, string>();
            parameters.Add("order", Converter.OrderToString(order));
            if (string.IsNullOrEmpty(afterMailId) == false)
                parameters.Add("after_mail_id", afterMailId);
            if (string.IsNullOrEmpty(tag) == false)
                parameters.Add("tag", tag);

            Hive5Http.Instance.GetHttpAsync(url, parameters.data, CountMailsResponseBody.Load, callback);
        }

        /** 
        * @api {POST} UpdateMail 메일 수정
        * @apiVersion 0.3.11-beta
        * @apiName UpdateMail
        * @apiGroup Mail
        *
        * @apiParam {string} mailId 특정 메일 이후의 리스트 받기 위한 mail id
        * @apiParam {string} content 메일 본문
        * @apiParam {string} extrasJson 보조데이터 (JSON) 
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.UpdateMail(mailId, content, callback);
        */
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

        /** 
        * @api {POST} DeleteMail 메일 삭제
        * @apiVersion 0.3.11-beta
        * @apiName DeleteMail
        * @apiGroup Mail
        *
        * @apiParam {string} mailId 특정 메일 이후의 리스트 받기 위한 mail id
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.DeleteMail(mailId, callback);
        */
        public void DeleteMail(string mailId, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.Delete, mailId));

            Hive5Http.Instance.DeleteHttpAsync(url, new { }, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {POST} DeleteMailOverLimit 제한 개수를 초과한 메일 삭제
        * @apiVersion 0.3.11-beta
        * @apiName DeleteMailOverLimit
        * @apiGroup Mail
        *
        * @apiParam {int} limit 메일 개수 제한
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.DeleteMailOverLimit(100, callback);
        */
        public void DeleteMailOverLimit(int limit, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.DeleteOverLimit, limit));

            Hive5Http.Instance.PostHttpAsync(url, new { }, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {POST} DeleteMail 메일 삭제
        * @apiVersion 0.3.11-beta
        * @apiName DeleteMail
        * @apiGroup Mail
        *
        * @apiParam {int} days 오래된 메일 삭제
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.DeleteMailOlderThan(7, callback);
        */
        public void DeleteMailOlderThan(int days, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.DeleteOlderThan, days));

            Hive5Http.Instance.PostHttpAsync(url, new { }, CommonResponseBody.Load, callback);
        }

        /** 
        * @api {POST} AddTags 메일 TAG 추가
        * @apiVersion 0.3.11-beta
        * @apiName AddTags
        * @apiGroup Mail
        *
        * @apiParam {string} mailId 메일 ID
        * @apiParam {string[]} tags 추가할 TAG
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.AddTags(mailId, tags, callback);
        */
        public void AddTags(string mailId, string[] tags, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Mail.AddTags, mailId));

            var requestBody = new
            {
                tags = tags
            };

            Hive5Http.Instance.PostHttpAsync(url, requestBody, AddMailTagsResponseBody.Load, callback);
        }

        /** 
        * @api {POST} RemoveTags 메일 TAG 제거
        * @apiVersion 0.3.11-beta
        * @apiName RemoveTags
        * @apiGroup Mail
        *
        * @apiParam {string} mailId 메일 ID
        * @apiParam {string[]} tags 제거할 TAGS
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.RemoveTags(mailId, tags, callback);
        */
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
