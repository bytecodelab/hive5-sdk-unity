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
	public class Hive5SocialGraph : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;

		/// <summary>
		/// Updates the friends.
		/// </summary>
		/// <param name="friend_ids">Friend_ids.</param>
		/// <param name="callback">Callback.</param>
		public void updateFriends(string[] friend_ids, CallBack callback)
		{
			// Hive5 API URL 초기화	
			var url = hive5.initializeUrl("friends/update");

			// Request Body
			var requestBody = new {
				friends = friend_ids
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
			);
		}

		/// <summary>
		/// Gets the friends info.
		/// </summary>
		/// <param name="platformUserIds">Platform user identifiers.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="callback">Callback.</param>
		public void getFriendsInfo(string[] platformUserIds, string[] itemKeys, string[] userDataKeys, CallBack callback)
		{
			var url = hive5.initializeUrl ("friends/info");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( platformUserIds, key => { parameters.Add( "platform_user_id", key ); }); 
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); });

			// WWW 호출
			hive5.asyncRoutine ( 
	    		hive5.getHTTP (url, parameters.data, GetUserDataResponseBody.Load, callback) 
	    	);
		}

	}

}
