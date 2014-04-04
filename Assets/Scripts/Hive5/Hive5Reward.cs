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
	public class Hive5Reward : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;

		/// <summary>
		/// Get the specified rewardId and callback.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="callback">Callback.</param>
		public void get(long rewardId, CallBack callback)
		{
			var url = hive5.initializeUrl (string.Format ("rewards/{0}", rewardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();

			// WWW 호출
			hive5.asyncRoutine ( 
            	hive5.getHTTP (url, parameters.data, GetUserDataResponseBody.Load, callback) 
            );
		}
		
		/// <summary>
		/// Apply the specified rewardId, deleteMail and callback.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void apply(long rewardId, bool deleteMail, CallBack callback)
		{
			var url = hive5.initializeUrl (string.Format ("rewards/apply/{0}", rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Applies all.
		/// </summary>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void applyAll(bool deleteMail, CallBack callback)
		{
			var url = hive5.initializeUrl ("rewards/apply_all");
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Invalidate the specified rewardId, deleteMail and callback.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void invalidate(long rewardId, bool deleteMail, CallBack callback)
		{
			var url = hive5.initializeUrl (string.Format ("rewards/invalidate/{0}", rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
			);
		}

	}

}
