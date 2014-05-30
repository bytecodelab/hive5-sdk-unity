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
			SocialGraph API Group
		*********************************************************************************/
		
		/// <summary>
		/// Updates the friends.
		/// </summary>
		/// <param name="friend_ids">Friend_ids.</param>
		/// <param name="callback">Callback.</param>
		public void UpdateFriends(string[] friend_ids, CallBack callback)
		{
			// Hive5 API URL 초기화	
			var url = InitializeUrl(APIPath.UpdateFriends);
			
			// Request Body
			var requestBody = new {
				friends = friend_ids
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, UpdateFriendsResponseBody.Load, callback)
				);
		}
		
		/// <summary>
		/// Gets the friends info.
		/// </summary>
		/// <param name="platformUserIds">Platform user identifiers.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="callback">Callback.</param>
		public void GetFriendsInfo(string[] platformUserIds, string[] itemKeys, string[] userDataKeys, CallBack callback)
		{
			var url = InitializeUrl (APIPath.GetFriendsInfo);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( platformUserIds, key => { parameters.Add( "platform_user_id", key ); }); 
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.ItemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.UserDataKey, key ); });
			
			// WWW 호출
			StartCoroutine ( 
			                GetHttp (url, parameters.data, GetFriendsInfoResponseBody.Load, callback) 
			                );
		}


	}

}
