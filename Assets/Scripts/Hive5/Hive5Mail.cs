using UnityEngine;
using System;
using System.Collections.Generic;
using System.Collections;
using System.Text;
using System.Net;
using System.IO;
using LitJson;
using Hive5;
using Hive5.Core;
using Hive5.Model;
using Hive5.Util;

namespace Hive5
{

	/// <summary>
	/// Hive5 user data.
	/// </summary>
	public class Hive5Mail : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;

		/// <summary>
		/// Get the specified limit, order, afterMailId, tag and callback.
		/// </summary>
		/// <param name="limit">Limit.</param>
		/// <param name="order">Order.</param>
		/// <param name="afterMailId">After mail identifier.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="callback">Callback.</param>
		public void get(int limit, string order, long afterMailId, string tag, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(APIPath.getMails);
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("limit", limit.ToString());
			parameters.Add ("order", order);
			parameters.Add ("after_mail_id", afterMailId.ToString());
			parameters.Add ("tag", tag);

			// WWW 호출
			hive5.asyncRoutine ( 
            	hive5.getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
            );
		}

		/// <summary>
		/// Getls the count.
		/// </summary>
		/// <param name="order">Order.</param>
		/// <param name="afterMailId">After mail identifier.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="callback">Callback.</param>
		public void getCount(string order, long afterMailId, string tag, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl("mails/count");
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("order", order);
			parameters.Add ("after_mail_id", afterMailId.ToString());
			parameters.Add ("tag", tag);

			// WWW 호출
			hive5.asyncRoutine ( 
		    	hive5.getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
		    );
		}

		/// <summary>
		/// Update the specified mailId, content and callback.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
		public void update(long mailId, string content, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = hive5.initializeUrl(string.Format("mails/update/{0}", mailId));
			
			var requestBody = new {
				content	= content
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, CommonResponseBody.Load, callback)
			);
		}

	}

}
