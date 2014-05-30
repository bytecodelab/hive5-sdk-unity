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
			Auth API Group
		*********************************************************************************/
		
		/// <summary>
		/// Login the specified userId, accessToken, sdkVersion, os, userDataKeys, itemKeys, configKeys and callback.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <param name="accessToken">Access token.</param>
		/// <param name="sdkVersion">Sdk version.</param>
		/// <param name="os">Os.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="configKeys">Config keys.</param>
		/// <param name="callback">Callback.</param>
		public void Login(string os, string[] objectKeys, string[] configKeys, string platform, string platformUserId, string platformSDKVersion, CallBack callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = InitializeUrl(APIPath.PlatformLogin);
			
			Debug.Log ("login LoginState=" + LoginState);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add( ParameterKey.PlatformUserId, platformUserId );
			parameters.Add( ParameterKey.PlatformSdkVersion, platformSDKVersion );
			parameters.Add (ParameterKey.Platform, platform);
			parameters.Add( ParameterKey.OS, os );
			
			Array.ForEach ( objectKeys, key => { parameters.Add( ParameterKey.ObjectKey, key ); } );
			Array.ForEach ( configKeys, key => { parameters.Add( ParameterKey.ConfigKey, key ); } );
			
			StartCoroutine (
				GetHttp(url, parameters.data, LoginResponseBody.Load, ( response ) => { 
				if ( response.resultCode == Hive5ResultCode.Success)
				{
					SetAccessToken(((LoginResponseBody)response.resultData).accessToken);
				}
				this.loginState = true;
				callback(response);
			}
			));
			
		}
		
		
		/// <summary>
		/// Logout the specified userId, accessToken and callback.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <param name="accessToken">Access token.</param>
		/// <param name="callback">Callback.</param>
		public void Logout(string userId, string accessToken, CallBack callback)
		{
			
		}
		
		/// <summary>
		/// Unregister the specified userId, accessToken and callback.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <param name="accessToken">Access token.</param>
		/// <param name="callback">Callback.</param>
		public void Unregister(CallBack callback)
		{
			var url = InitializeUrl (APIPath.Unregister);
			
			// WWW 호출
			StartCoroutine (
				PostHttp (url, new {}, CommonResponseBody.Load, callback)
				);				
		}
		
		/// <summary>
		/// Agreements the specified generalVersion, partnershipVersion and callback.
		/// </summary>
		/// <param name="generalVersion">General version.</param>
		/// <param name="partnershipVersion">Partnership version.</param>
		/// <param name="callback">Callback.</param>
		public void SubmitAgreements(string generalVersion, string partnershipVersion, CallBack callback)
		{
			var url = InitializeUrl (APIPath.Agreement);
			
			var requestBody = new {
				general_agreement = generalVersion,
				partnership_agreement	= partnershipVersion
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp (url, requestBody, CommonResponseBody.Load, callback)
				);	
		}
		
		public void GetAgreements(CallBack callback)
		{
			var url = InitializeUrl (APIPath.Agreement);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
			StartCoroutine (
				GetHttp (url, parameters.data, GetAgreementsResponseBody.Load, callback)
				);	
		}


	}

}
