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
	public partial class Hive5Client : MonoSingleton<Hive5Client> {

		/********************************************************************************
			Mail API Group
		*********************************************************************************/

		/** 
		* @api {public Method} CreateMail 메일 생성하기
		* @apiVersion 1.0.0
		* @apiName void CreateMail(string content, string friendPlatformUserId, string[] tags,  CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {string} content 메일 본문
		* @apiParam {string} friendPlatformUserId 받는사람 플랫폼 UserId
		* @apiParam {string[]} tags 메일 Tags
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.CreateMail(content, friendPlatformUserId, tags, callback);
		*/
		public void CreateMail(string content, string friendPlatformUserId, string[] tags,  CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.SubmitMail));
			
			var requestBody = new {
				content	= content,
				platform_user_id = friendPlatformUserId,
				tags = tags
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, CreateMailResponseBody.Load, callback)
			);
		}

		/** 
		* @api {public Method} GetMails 메일 리스트 가져오기
		* @apiVersion 1.0.0
		* @apiName void GetMails(int limit, string order, long afterMailId, string tag, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {int} limit 받을 메일 갯수
		* @apiParam {string} order 메일 순서
		* @apiParam {long} afterMailId 특정 메일 이후의 리스트 받기 위한 mail id
		* @apiParam {string} tags 메일 Tag
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetMails(limit, order, afterMailId, tag, callback);
		*/
		public void GetMails(int limit, string order, long afterMailId, string tag, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.GetMails);
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("limit", limit.ToString());
			parameters.Add ("order", order);
			if( afterMailId > 0 )
				parameters.Add ("after_mail_id", afterMailId.ToString());
			if( tag.Length > 0 )
				parameters.Add ("tag", tag);
			
			// WWW 호출
			StartCoroutine ( 
            	GetHttp (url, parameters.data, GetMailsResponseBody.Load, callback) 
            );
		}
		
		/** 
		* @api {public Method} GetMailCount 메일 갯수 확인
		* @apiVersion 1.0.0
		* @apiName void GetMailCount(OrderType order, long afterMailId, string tag, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {OrderType} order 메일 순서
		* @apiParam {long} afterMailId 특정 메일 이후의 리스트 받기 위한 mail id
		* @apiParam {string} tags 메일 Tag
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetMailCount(order, afterMailId, tag, callback);
		*/
		public void GetMailCount(OrderType order, long afterMailId, string tag, CallBack callback)
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
			StartCoroutine ( 
		    	GetHttp (url, parameters.data, GetMailCountResponseBody.Load, callback) 
		    );
		}
		
		/** 
		* @api {public Method} UpdateMail 메일 수정
		* @apiVersion 1.0.0
		* @apiName void UpdateMail(long mailId, string content, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {long} mailId 특정 메일 이후의 리스트 받기 위한 mail id
		* @apiParam {string} content 메일 본문
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.UpdateMail(mailId, content, callback);
		*/
		public void UpdateMail(long mailId, string content, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.UpdateMail, mailId));
			
			var requestBody = new {
				content	= content
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, CommonResponseBody.Load, callback)
			);
		}

		/** 
		* @api {public Method} DeleteMail 메일 삭제
		* @apiVersion 1.0.0
		* @apiName void DeleteMail(long mailId, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {long} mailId 특정 메일 이후의 리스트 받기 위한 mail id
		* @apiParam {string} content 메일 본문
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.DeleteMail(mailId, callback);
		*/
		public void DeleteMail(long mailId, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.DeleteMail, mailId));
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, new {}, CommonResponseBody.Load, callback)
			);
		}

		/** 
		* @api {public Method} DeleteAllMail 메일 전체(특정범위) 삭제
		* @apiVersion 1.0.0
		* @apiName void DeleteAllMail(long fromMailId, long toMailId, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {long} fromMailId 삭제 메일 시작점 ID
		* @apiParam {long} toMailId 삭제 메일 끝점 ID
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.DeleteAllMail(fromMailId, toMailId, callback);
		*/
		public void DeleteAllMail(long fromMailId, long toMailId, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.DeleteAllMail));
			
			var requestBody = new {
				from_mail_id	= fromMailId,
				to_mail_id		= toMailId
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, CommonResponseBody.Load, callback)
			);
		}

		/** 
		* @api {public Method} AttachMailTags 메일 TAG 추가
		* @apiVersion 1.0.0
		* @apiName void AttachMailTags(long mailId, string[] tags, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {long} mailId 메일 ID
		* @apiParam {string[]} tags 추가할 TAG
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.AttachMailTags(mailId, tags, callback);
		*/
		public void AttachMailTags(long mailId, string[] tags, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.AttachMailTag, mailId));
			
			var requestBody = new {
				tags = tags
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, AttachMailTagsResponseBody.Load, callback)
			);
		}

		/** 
		* @api {public Method} DetachMailTags 메일 TAG 제거
		* @apiVersion 1.0.0
		* @apiName void DetachMailTags(long mailId, string[] tags, CallBack callback)
		* @apiGroup Hive5Client
		*
		* @apiParam {long} mailId 메일 ID
		* @apiParam {string[]} tags 제거할 TAGS
		* @apiParam {CallBack) callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.DetachMailTags(mailId, tags, callback);
		*/
		public void DetachMailTags(long mailId, string[] tags, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.DetachMailTag, mailId));
			
			var requestBody = new {
				tags = tags
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, DetachMailTagsResponseBody.Load, callback)
				);
		}

	}

}
