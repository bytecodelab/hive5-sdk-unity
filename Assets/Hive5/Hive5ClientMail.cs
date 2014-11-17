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
			Mail API Group
		*********************************************************************************/

		/** 
		* @api {POST} CreateMail 메일 생성하기
		* @apiVersion 0.2.0
		* @apiName CreateMail
		* @apiGroup Mail
		*
		* @apiParam {string} content 메일 본문
		* @apiParam {string} friendPlatformUserId 받는사람 플랫폼 UserId
		* @apiParam {string[]} tags 메일 Tags
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateMail(content, friendPlatformUserId, tags, callback);
		*/
		[Obsolete("CreateMail without friendPlatform is deprecated, please use CreateMail with friendPlatform instead.")]
		public void CreateMail(string content, string friendPlatformUserId, string[] tags,  Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.SubmitMail));
			
			var requestBody = new {
				content	= content,
				platform_user_id = friendPlatformUserId,
				tags = tags
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, CreateMailResponseBody.Load, callback);
		}

			/** 
		* @api {POST} CreateMail 메일 생성하기
		* @apiVersion 0.2.0
		* @apiName CreateMail
		* @apiGroup Mail
		*
		* @apiParam {string} content 메일 본문
		* @apiParam {string} friendPlatform 받는사람 플랫폼
		* @apiParam {string} friendPlatformUserId 받는사람 플랫폼 UserId
		* @apiParam {string[]} tags 메일 Tags
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateMail(content, friendPlatformUserId, tags, callback);
		*/
		public void CreateMail(string content, string friendPlatform, string friendPlatformUserId, string[] tags,  Callback callback)
		{
			if (string.IsNullOrEmpty (friendPlatform) == true)
				throw new NullReferenceException ("friendPlatform should not be empty!");

			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.SubmitMail));
			
			var requestBody = new {
				content	= content,
				platform = friendPlatform,
				platform_user_id = friendPlatformUserId,
				tags = tags
			};
			
			// WWW 호출
			PostHttpAsync(url, requestBody, CreateMailResponseBody.Load, callback);
		}

		/** 
		* @api {GET} GetMails 메일 리스트 가져오기
		* @apiVersion 0.2.0
		* @apiName GetMails
		* @apiGroup Mail
		*
		* @apiParam {int} limit 받을 메일 갯수
		* @apiParam {string} order 메일 순서
		* @apiParam {long} afterMailId 특정 메일 이후의 리스트 받기 위한 mail id
		* @apiParam {string} tags 메일 Tag
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetMails(limit, order, afterMailId, tag, callback);
		*/
		public void GetMails(int limit, OrderType order, long afterMailId, string tag, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.GetMails);
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("limit", limit.ToString());
            parameters.Add ("order", Tool.OrderToString(order));

			if( afterMailId > 0 )
				parameters.Add ("after_mail_id", afterMailId.ToString());
			if( tag.Length > 0 )
				parameters.Add ("tag", tag);
			
			// WWW 호출
            GetHttpAsync(url, parameters.data, GetMailsResponseBody.Load, callback);
		}
		
		/** 
		* @api {GET} GetMailCount 메일 갯수 확인
		* @apiVersion 0.2.0
		* @apiName GetMailCount
		* @apiGroup Mail
		*
		* @apiParam {OrderType} order 메일 순서
		* @apiParam {long} afterMailId 특정 메일 이후의 리스트 받기 위한 mail id
		* @apiParam {string} tags 메일 Tag
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetMailCount(order, afterMailId, tag, callback);
		*/
		public void GetMailCount(OrderType order, long afterMailId, string tag, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.MailCount);
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("order", Tool.OrderToString(order));
			if(afterMailId > 0)
				parameters.Add ("after_mail_id", afterMailId.ToString());
			if(tag.Length > 0)
				parameters.Add ("tag", tag);
			
			// WWW 호출
            GetHttpAsync(url, parameters.data, GetMailCountResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} UpdateMail 메일 수정
		* @apiVersion 0.2.0
		* @apiName UpdateMail
		* @apiGroup Mail
		*
		* @apiParam {long} mailId 특정 메일 이후의 리스트 받기 위한 mail id
		* @apiParam {string} content 메일 본문
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.UpdateMail(mailId, content, callback);
		*/
		public void UpdateMail(long mailId, string content, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.UpdateMail, mailId));
			
			var requestBody = new {
				content	= content
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, CommonResponseBody.Load, callback);
		}

		/** 
		* @api {POST} DeleteMail 메일 삭제
		* @apiVersion 0.2.0
		* @apiName DeleteMail
		* @apiGroup Mail
		*
		* @apiParam {long} mailId 특정 메일 이후의 리스트 받기 위한 mail id
		* @apiParam {string} content 메일 본문
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.DeleteMail(mailId, callback);
		*/
		public void DeleteMail(long mailId, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.DeleteMail, mailId));
			
			// WWW 호출
            PostHttpAsync(url, new { }, CommonResponseBody.Load, callback);
		}

		/** 
		* @api {POST} DeleteAllMail 메일 전체(특정범위) 삭제
		* @apiVersion 0.2.0
		* @apiName DeleteAllMail
		* @apiGroup Mail
		*
		* @apiParam {long} fromMailId 삭제 메일 시작점 ID
		* @apiParam {long} toMailId 삭제 메일 끝점 ID
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.DeleteAllMail(fromMailId, toMailId, callback);
		*/
		public void DeleteAllMail(long fromMailId, long toMailId, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.DeleteAllMail));
			
			var requestBody = new {
				from_mail_id	= fromMailId,
				to_mail_id		= toMailId
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, CommonResponseBody.Load, callback);
		}

		/** 
		* @api {POST} AttachMailTags 메일 TAG 추가
		* @apiVersion 0.2.0
		* @apiName AttachMailTags
		* @apiGroup Mail
		*
		* @apiParam {long} mailId 메일 ID
		* @apiParam {string[]} tags 추가할 TAG
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.AttachMailTags(mailId, tags, callback);
		*/
		public void AttachMailTags(long mailId, string[] tags, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.AttachMailTag, mailId));
			
			var requestBody = new {
				tags = tags
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, AttachMailTagsResponseBody.Load, callback);
		}

		/** 
		* @api {POST} DetachMailTags 메일 TAG 제거
		* @apiVersion 0.2.0
		* @apiName DetachMailTags
		* @apiGroup Mail
		*
		* @apiParam {long} mailId 메일 ID
		* @apiParam {string[]} tags 제거할 TAGS
		* @apiParam {Callback} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.DetachMailTags(mailId, tags, callback);
		*/
		public void DetachMailTags(long mailId, string[] tags, Callback callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.DetachMailTag, mailId));
			
			var requestBody = new {
				tags = tags
			};
			
			// WWW 호출
            PostHttpAsync(url, requestBody, DetachMailTagsResponseBody.Load, callback);
		}

	}

}
