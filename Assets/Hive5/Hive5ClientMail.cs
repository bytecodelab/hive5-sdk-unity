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

		/// <summary>
		/// Update the specified mailId, content and callback.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
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

		/// <summary>
		/// Get the specified limit, order, afterMailId, tag and callback.
		/// </summary>
		/// <param name="limit">Limit.</param>
		/// <param name="order">Order.</param>
		/// <param name="afterMailId">After mail identifier.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="callback">Callback.</param>
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
		
		/// <summary>
		/// Getls the count.
		/// </summary>
		/// <param name="order">Order.</param>
		/// <param name="afterMailId">After mail identifier.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="callback">Callback.</param>
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
		
		/// <summary>
		/// Update the specified mailId, content and callback.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
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

		/// <summary>
		/// Update the specified mailId, content and callback.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
		public void DeleteMail(long mailId, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.DeleteMail, mailId));
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, new {}, CommonResponseBody.Load, callback)
			);
		}

		/// <summary>
		/// Update the specified mailId, content and callback.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
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

		/// <summary>
		/// Attachs the mail tags.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="tags">Tags.</param>
		/// <param name="callback">Callback.</param>
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

		/// <summary>
		/// Attachs the mail tags.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="tags">Tags.</param>
		/// <param name="callback">Callback.</param>
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
