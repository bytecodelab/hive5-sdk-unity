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
	public class Hive5Client : MonoSingleton<Hive5Client> {

		public delegate IResponseBody dataLoader (JsonData response);
		public delegate void CallBack (Hive5Response hive5Response);

		private string appKey		= "";
		private string uuid			= "";
		private string accessToken 	= "";

		private bool initState 	= false;
		private bool loginState = false; 
		private bool isDebug 	= false;

		public bool InitState { 
			get { return initState;} 
		}
		public bool LoginState { 
			get { return loginState;} 
		}

		private Hive5TimeZone timezone 	= Hive5TimeZone.UTC;
		private Hive5APIZone zone		= Hive5APIZone.Beta;
		private string host 	= APIServer.BetaHost;
		private string version 	= APIServer.Version;


		protected Hive5Client () {}

	
		/********************************************************************************
			Init API Group
		*********************************************************************************/

		/// <summary>
		/// Init the specified appKey and uuid.
		/// </summary>
		/// <param name="appKey">App key.</param>
		/// <param name="uuid">UUID.</param>
		public void Init(string appKey, string uuid)
		{
			this.appKey = appKey;
			this.uuid 	= uuid;
			this.initState = true;
		}


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
		public void login(string userId, string accessToken, string sdkVersion, string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, CallBack callback)
		{
			if (!InitState)
				return;
			
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.KakaoLogin);
			
			Debug.Log ("login LoginState=" + LoginState);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add( ParameterKey.UserId, userId );
			parameters.Add( ParameterKey.AccessToken, accessToken );
			parameters.Add( ParameterKey.SdkVersion, sdkVersion );
			parameters.Add( ParameterKey.OS, os );
			
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.UserDataKey, key ); } );
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.ItemKey, key ); } );
			Array.ForEach ( configKeys, key => { parameters.Add( ParameterKey.ConfigKey, key ); } );
			
			StartCoroutine (
				getHTTP(url, parameters.data, LoginResponseBody.Load, ( response ) => { 
				if ( response.resultCode == Hive5ResultCode.Success)
				{
					setAccessToken(((LoginResponseBody)response.resultData).accessToken);
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
		public void logout(string userId, string accessToken, CallBack callback)
		{
			
		}
		
		/// <summary>
		/// Unregister the specified userId, accessToken and callback.
		/// </summary>
		/// <param name="userId">User identifier.</param>
		/// <param name="accessToken">Access token.</param>
		/// <param name="callback">Callback.</param>
		public void unregister(CallBack callback)
		{
			var url = initializeUrl (APIPath.Unregister);

			// WWW 호출
			StartCoroutine (
				postHTTP (url, new {}, CommonResponseBody.Load, callback)
			);				
		}

		/// <summary>
		/// Agreements the specified generalVersion, partnershipVersion and callback.
		/// </summary>
		/// <param name="generalVersion">General version.</param>
		/// <param name="partnershipVersion">Partnership version.</param>
		/// <param name="callback">Callback.</param>
		public void submitAgreements(string generalVersion, string partnershipVersion, CallBack callback)
		{
			var url = initializeUrl (APIPath.Agreement);

			var requestBody = new {
				general_agreement = generalVersion,
				partnership_agreement	= partnershipVersion
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP (url, requestBody, CommonResponseBody.Load, callback)
			);	
		}

		public void getAgreements(CallBack callback)
		{
			var url = initializeUrl (APIPath.Agreement);

			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
			StartCoroutine (
				getHTTP (url, parameters.data, GetAgreementsResponseBody.Load, callback)
			);	
		}



		/********************************************************************************
			Item API Group
		*********************************************************************************/

		/// <summary>
		/// Get the specified itemKeys and callback.
		/// </summary>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="callback">Callback.</param>
		public void getItems(string[] itemKeys, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.Item);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			List<string> st = new List<string> ();

			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.ItemKey, key ); });
			
			// WWW 호출
			StartCoroutine (
				getHTTP (url, parameters.data, GetItemsResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Convert the specified itemConvertKey and callBack.
		/// </summary>
		/// <param name="itemConvertKey">Item convert key.</param>
		/// <param name="callBack">Call back.</param>
		public void convertItem(string itemConvertKey, CallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(String.Format(APIPath.ConvertItem,itemConvertKey));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, new {}, ConvertItemResponseBody.Load, callBack)
			);
		}
		
		/// <summary>
		/// Consume the specified requestBody and callBack.
		/// </summary>
		/// <param name="requestBody">Request body.</param>
		/// <param name="callBack">Call back.</param>
		public void consumeItem(object requestBody, CallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.ConsumeItem);
			
			// WWW 호출
			StartCoroutine (
				postHTTP (url, requestBody, ConsumeItemResponseBody.Load, callBack)
			);
		}
		
		public void giftItem(string platformUserId, string itemKey, int count, string mail, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.GiftItem);
			
			var requestBody = new {
				platform_user_id = platformUserId,
				Item	= itemKey,
				count	= count,
				mail	= mail
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP (url, requestBody, GiftItemResponseBody.Load, callback)
			);	
		}

		/********************************************************************************
			Leaderboard API Group
		*********************************************************************************/

		/// <summary>
		/// Submits the score.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="score">Score.</param>
		/// <param name="callback">Callback.</param>
		public void submitScore(long leaderboardId, long score, CallBack callback)
		{
			var url = initializeUrl (string.Format (APIPath.LeaderboardSubmitScores, leaderboardId, score));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				leaderboard_id 	= leaderboardId,
				score			= score
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, SubmitScoreResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Gets the scores.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="rankMin">Rank minimum.</param>
		/// <param name="rankMax">Rank max.</param>
		/// <param name="rangeMin">Range minimum.</param>
		/// <param name="rangeMax">Range max.</param>
		/// <param name="callback">Callback.</param>
		public void getScores(long leaderboardId, string[] itemKeys, string[] userDataKeys, long rankMin, long rankMax, long rangeMin, long rangeMax, CallBack callback)
		{
			var url = initializeUrl (string.Format (APIPath.LeaderboardScores, leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, GetScoresResponseBody.Load, callback) 
            );
		}
		
		/// <summary>
		/// Gets my score.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="rangeMin">Range minimum.</param>
		/// <param name="rangeMax">Range max.</param>
		/// <param name="callback">Callback.</param>
		public void getMyScore(long leaderboardId, long rangeMin, long rangeMax, CallBack callback)
		{
			var url = initializeUrl (string.Format (APIPath.LeaderboardMyScore, leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, GetMyScoreResponseBody.Load, callback) 
            );
		}
		
		/// <summary>
		/// Gets the social scores.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="callback">Callback.</param>
		public void getSocialScores(long leaderboardId, string[] itemKeys, string[] userDataKeys, CallBack callback)
		{
			var url = initializeUrl (string.Format (APIPath.LeaderboardSocialScores, leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.ItemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.UserDataKey, key ); });
			
			// WWW 호출
			StartCoroutine ( 
		    	getHTTP (url, parameters.data, GetSocialScoresResponseBody.Load, callback) 
		    );
		}
		
		/// <summary>
		/// Prize the specified leaderboardId and callback.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="callback">Callback.</param>
		public void prize(string leaderboardId, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.LeaderboardPrize, leaderboardId));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, new {}, PrizeResponseBody.Load, callback)
			);
		}

		/********************************************************************************
			Mail API Group
		*********************************************************************************/

		/// <summary>
		/// Update the specified mailId, content and callback.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
		public void createMail(string content, string friendPlatformUserId, string[] tags,  CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.SubmitMail));
			
			var requestBody = new {
				content	= content,
				platform_user_id = friendPlatformUserId,
				tags = tags
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CreateMailResponseBody.Load, callback)
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
		public void getMails(int limit, string order, long afterMailId, string tag, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.GetMails);
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("limit", limit.ToString());
			parameters.Add ("order", order);
			if( afterMailId > 0 )
				parameters.Add ("after_mail_id", afterMailId.ToString());
			if( tag.Length > 0 )
				parameters.Add ("tag", tag);
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, GetMailsResponseBody.Load, callback) 
            );
		}
		
		/// <summary>
		/// Getls the count.
		/// </summary>
		/// <param name="order">Order.</param>
		/// <param name="afterMailId">After mail identifier.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="callback">Callback.</param>
		public void getMailCount(OrderType order, long afterMailId, string tag, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.MailCount);
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("order", Tool.OrderToString(order));
			if(afterMailId > 0)
				parameters.Add ("after_mail_id", afterMailId.ToString());
			if(tag.Length > 0)
				parameters.Add ("tag", tag);
			
			// WWW 호출
			StartCoroutine ( 
		    	getHTTP (url, parameters.data, GetMailCountResponseBody.Load, callback) 
		    );
		}
		
		/// <summary>
		/// Update the specified mailId, content and callback.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
		public void updateMail(long mailId, string content, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.UpdateMail, mailId));
			
			var requestBody = new {
				content	= content
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CommonResponseBody.Load, callback)
			);
		}

		/// <summary>
		/// Update the specified mailId, content and callback.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
		public void deleteMail(long mailId, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.DeleteMail, mailId));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, new {}, CommonResponseBody.Load, callback)
			);
		}

		/// <summary>
		/// Update the specified mailId, content and callback.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
		public void deleteAllMail(long fromMailId, long toMailId, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.DeleteAllMail));
			
			var requestBody = new {
				from_mail_id	= fromMailId,
				to_mail_id		= toMailId
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CommonResponseBody.Load, callback)
			);
		}

		/// <summary>
		/// Attachs the mail tags.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="tags">Tags.</param>
		/// <param name="callback">Callback.</param>
		public void attachMailTags(long mailId, string[] tags, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.AttachMailTag, mailId));
			
			var requestBody = new {
				tags = tags
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, AttachMailTagsResponseBody.Load, callback)
			);
		}

		/// <summary>
		/// Attachs the mail tags.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="tags">Tags.</param>
		/// <param name="callback">Callback.</param>
		public void detachMailTags(long mailId, string[] tags, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.DetachMailTag, mailId));
			
			var requestBody = new {
				tags = tags
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, DetachMailTagsResponseBody.Load, callback)
				);
		}

		/********************************************************************************
			Mission API Group
		*********************************************************************************/

		/// <summary>
		/// Complete the specified missionKey and callback.
		/// </summary>
		/// <param name="missionKey">Mission key.</param>
		/// <param name="callback">Callback.</param>
		public void completeMission(string missionKey, CallBack callback)
		{
			var url = initializeUrl (string.Format(APIPath.CompleteMission, missionKey));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, new {}, CompleteMissionResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Batchs the complete.
		/// </summary>
		/// <param name="missionKeys">Mission keys.</param>
		/// <param name="callback">Callback.</param>
		public void batchCompleteMission(string[] missionKeys, CallBack callback)
		{
			var url = initializeUrl (APIPath.BatchCompleteMission);
			
			var requestBody = new {
				keys 	= missionKeys
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, BatchCompleteMissionResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Gets the completed.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public void getCompletedMissions(CallBack callback)
		{
			var url = initializeUrl (APIPath.GetCompletedMissions);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, GetCompletedMissionsResponseBody.Load, callback) 
            );
		}


		/********************************************************************************
			Purchase API Group
		*********************************************************************************/

		/// <summary>
		/// Creates the google purchase.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void createNaverPurchase(string productCode, string paymentSequence, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.CreateNaverPurchase);
			
			var requestBody = new {
				product_code 		= productCode,
				payment_sequence	= paymentSequence
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CreateNaverPurchaseResponseBody.Load, callback)
				);
		}
		
		/// <summary>
		/// Completes the google purchase.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="listPrice">List price.</param>
		/// <param name="purchasedPrice">Purchased price.</param>
		/// <param name="currency">Currency.</param>
		/// <param name="purchaseData">Purchase data.</param>
		/// <param name="signature">Signature.</param>
		/// <param name="callBack">Call back.</param>
		public void completeNaverPurchase(long id, long listPrice, long purchasedPrice, string currency, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.CompleteNaverPurchase, id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CompleteNaverPurchaseResponseBody.Load, callback)
				);	
		}

		/// <summary>
		/// Creates the google purchase.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void createGooglePurchase(string productCode, string receiverPlatformUserId, string mailForReceiver, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.CreateGooglePurchase);
			
			var requestBody = new {
				product_code 				= productCode,
				receiver_platform_user_id	= receiverPlatformUserId,
				mail_for_receiver			= mailForReceiver
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CreateGooglePurchaseResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Completes the google purchase.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="listPrice">List price.</param>
		/// <param name="purchasedPrice">Purchased price.</param>
		/// <param name="currency">Currency.</param>
		/// <param name="purchaseData">Purchase data.</param>
		/// <param name="signature">Signature.</param>
		/// <param name="callBack">Call back.</param>
		public void completeGooglePurchase(long id, long listPrice, long purchasedPrice, string currency, string purchaseData, string signature, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.CompleteGooglePurchase, id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency,
				purchase_data	= purchaseData,
				signature		= signature
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CompleteGooglePurchaseResponseBody.Load, callback)
			);	
		}
		
		
		/// <summary>
		/// Creates the apple.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void createApplePurchase(string productCode, string receiverPlatformUserId, string mailForReceiver, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.CreateApplePurchase);
			
			var requestBody = new {
				product_code 				= productCode,
				receiver_platform_user_id	= receiverPlatformUserId,
				mail_for_receiver			= mailForReceiver
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CreateApplePurchaseResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Completes the apple.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="listPrice">List price.</param>
		/// <param name="purchasedPrice">Purchased price.</param>
		/// <param name="currency">Currency.</param>
		/// <param name="receipt">Receipt.</param>
		/// <param name="isSandbox">If set to <c>true</c> is sandbox.</param>
		/// <param name="callBack">Call back.</param>
		public void completeApplePurchase(long id, long listPrice, long purchasedPrice, string currency, string receipt, bool isSandbox, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format(APIPath.CompleteApplePurchase, id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency,
				receipt			= receipt,
				is_sandbox		= isSandbox
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CompleteApplePurchaseResponseBody.Load, callback)
			);
		}

		/********************************************************************************
			Push API Group
		*********************************************************************************/

		/// <summary>
		/// Updates the token.
		/// </summary>
		/// <param name="platform">Platform.</param>
		/// <param name="token">Token.</param>
		/// <param name="callback">Callback.</param>
		public void updatePushToken(string platform, string token, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.UpdatePushToken);
			
			var requestBody = new {
				push_platform 	= platform,
				push_token 		= token
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, UpdatePushTokenResponseBody.Load, callback)
			);
			
		}


		/********************************************************************************
			Reward API Group
		*********************************************************************************/

		/// <summary>
		/// Get the specified rewardId and callback.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="callback">Callback.</param>
		public void getRewardInfo(long rewardId, CallBack callback)
		{
			var url = initializeUrl (string.Format (APIPath.Reward, rewardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, GetRewardInfoResponseBody.Load, callback) 
            );
		}
		
		/// <summary>
		/// Apply the specified rewardId, deleteMail and callback.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void applyReward(long rewardId, bool deleteMail, CallBack callback)
		{
			var url = initializeUrl (string.Format (APIPath.ApplyReward, rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, ApplyRewardResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Applies all.
		/// </summary>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void applyAllRewards(bool deleteMail, CallBack callback)
		{
			var url = initializeUrl (APIPath.ApplyAllReward);
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, ApplyAllRewardsResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Invalidate the specified rewardId, deleteMail and callback.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void invalidateReward(long rewardId, bool deleteMail, CallBack callback)
		{
			var url = initializeUrl (string.Format (APIPath.InvalidateReward, rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, InvalidateRewardResponseBody.Load, callback)
			);
		}

		/********************************************************************************
			Round API Group
		*********************************************************************************/

		/// <summary>
		/// Start the specified roundRuleId and callback.
		/// </summary>
		/// <param name="roundRuleId">Round rule identifier.</param>
		/// <param name="callback">Callback.</param>
		public void startRound(long roundRuleId,  CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(String.Format(APIPath.StartRound, roundRuleId));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, new {}, StartRoundResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// End the specified roundId, requestBody and callback.
		/// </summary>
		/// <param name="roundId">Round identifier.</param>
		/// <param name="requestBody">Request body.</param>
		/// <param name="callback">Callback.</param>
		public void endRound(long roundId, object requestBody, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(String.Format(APIPath.EndRound, roundId));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, EndRoundResponseBody.Load, callback)
			);
		}

		/********************************************************************************
			SocialGraph API Group
		*********************************************************************************/

		/// <summary>
		/// Updates the friends.
		/// </summary>
		/// <param name="friend_ids">Friend_ids.</param>
		/// <param name="callback">Callback.</param>
		public void updateFriends(string[] friend_ids, CallBack callback)
		{
			// Hive5 API URL 초기화	
			var url = initializeUrl(APIPath.UpdateFriends);
			
			// Request Body
			var requestBody = new {
				friends = friend_ids
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, UpdateFriendsResponseBody.Load, callback)
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
			var url = initializeUrl (APIPath.GetFriendsInfo);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( platformUserIds, key => { parameters.Add( "platform_user_id", key ); }); 
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.ItemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.UserDataKey, key ); });
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, GetFriendsInfoResponseBody.Load, callback) 
            );
		}

		/********************************************************************************
			UserData API Group
		*********************************************************************************/

		/// <summary>
		/// Set the specified key, value and callback.
		/// </summary>
		/// <param name="key">Key.</param>
		/// <param name="value">Value.</param>
		/// <param name="callback">Callback.</param>
		public void setUserData(string key, string value, CommandType command, CallBack callback)
		{
			Debug.Log ("set LoginState=" + LoginState);
			
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.UserData);
			
			var requestBody = new SetUserDataRequest ();
			var data = new List<KeyValueCommand> ();
			var userData = new KeyValueCommand () { key = key, value = value, command = Tool.CommandToString(command) };
			data.Add (userData);
			requestBody.condition = new List<Condition> ();
			requestBody.data = data;
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
			);
			
		}
		
		/// <summary>
		/// Get the specified dataKeys and callback.
		/// </summary>
		/// <param name="dataKeys">Data keys.</param>
		/// <param name="callback">Callback.</param>
		public void getUserData(string[] dataKeys, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.UserData);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( dataKeys, key => { parameters.Add( ParameterKey.Key, key ); } );
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, GetUserDataResponseBody.Load, callback) 
            );
		}
		
		
		/// <summary>
		/// Sets the batch.
		/// </summary>
		/// <param name="userData">User data.</param>
		/// <param name="callback">Callback.</param>
		/// 
		/// 
		/*
		public void setBatch(Dictionary<string, string> userData, Hive5Client.apiCallBack callback)
		{
			Debug.Log ("LoginState=" + LoginState);
			
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.userData);

			var dataList = new List<UserData>();
			foreach (KeyValuePair<string, string> rowData in userData) 
			{
				dataList.Add (new UserData(rowData.Key, rowData.Value));
			}

			Array data = dataList.ToArray ();

			var requestBody = new {
				data = dataList;
			};
			
			StartCoroutine (
				postHTTP(url, requestBody, callback)
			);
			
		}
		*/


		/********************************************************************************
			Internal API Group
		*********************************************************************************/


		/// <summary>
		/// Sets the debug.
		/// </summary>
		/// <param name="debugFlag">If set to <c>true</c> debug flag.</param>
		public void setDebug()
		{
			isDebug = true;
		}

		/// <summary>
		/// Sets the zone.
		/// </summary>
		/// <param name="zone">Zone.</param>
		public void setZone(Hive5APIZone zone)
		{
			switch(zone)
			{
				// Beta Server
				case Hive5APIZone.Beta:
					host = APIServer.BetaHost;
				break;

				// Real Server
				case Hive5APIZone.Real:
					host = APIServer.BetaHost;
				break;
			}

		}

		/// <summary>
		/// Asyncs the routine.
		/// </summary>
		/// <param name="routine">Routine.</param>
		public void asyncRoutine(IEnumerator routine)
		{
			// 코루틴 WWW 호출
			StartCoroutine(routine);
		}

		/// <summary>
		/// Https the get.
		/// </summary>
		/// <returns>The get.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="callback">Callback.</param>
		public IEnumerator getHTTP(string url, List<KeyValuePair<string, string>> parameters, Hive5Response.dataLoader loader, CallBack callBack)
		{
			// Hive5 API Header 설정
			var headers = new Hashtable();
			headers.Add(HeaderKey.AppKey, this.appKey);
			headers.Add(HeaderKey.Uuid, this.uuid);
			headers.Add(HeaderKey.Token, this.accessToken);
			headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);

			string queryString = "";		
			foreach (KeyValuePair<string, string> parameter in parameters)
			{
				if (queryString.Length > 0)	
				{
					queryString += "&";
				}
				
				queryString += parameter.Key + "=" + parameter.Value;
			}

			string newUrl = url;
			
			if (queryString.Length > 0)
			{
				newUrl = url + "?" + queryString;
			}

			WWW www = new WWW( newUrl, null, headers );
			yield return www;

			if(this.isDebug) Debug.Log ("www reuqest URL = " + newUrl);
			if(this.isDebug) Debug.Log ("www response = " + www.text);

			callBack (Hive5Response.Load (loader, www.text));
		}

		/// <summary>
		/// Https the post.
		/// </summary>
		/// <returns>The post.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		public IEnumerator postHTTP(string url, object requestBody, Hive5Response.dataLoader loader, CallBack callBack)
		{	
			// Hive5 API Header 설정
			var headers = new Hashtable();
			headers.Add(HeaderKey.AppKey, this.appKey);
			headers.Add(HeaderKey.Uuid, this.uuid);
			headers.Add(HeaderKey.Token, this.accessToken);
			headers.Add(HeaderKey.ContentType, HeaderValue.ContentType);

			// Hive5 API json body 변환
			string jsonString = JsonMapper.ToJson (requestBody);						

			var encoding	= new System.Text.UTF8Encoding();

			// Hive5 API Request
			WWW www = new WWW(url, encoding.GetBytes(jsonString), headers); 
			yield return www;

			if(this.isDebug) Debug.Log ("www reuqest URL = " + url);
			if(this.isDebug) Debug.Log ("www request jsonBody= " + jsonString);
			if(this.isDebug) Debug.Log ("www response = " + www.text);
			
			callBack (Hive5Response.Load (loader, www.text));
		}
		
		/// <summary>
		/// Initializes the URL.
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="path">Path.</param>
		public string initializeUrl(string path)
		{
			return String.Join("/", new String[] { this.host, this.version, path });	
		}
		
		/// <summary>
		/// Gets the headers.
		/// </summary>
		/// <returns>The headers.</returns>
		private Dictionary<string, string> getHeaders()
		{
			Dictionary<string, string> result = new Dictionary<string, string>();
			
			result.Add(HeaderKey.AppKey, this.appKey);
			result.Add(HeaderKey.Uuid, this.uuid);

			if (this.accessToken.Length > 0)
				result.Add(HeaderKey.Token, this.accessToken);
			
			return result;
		}

		/// <summary>
		/// Sets the access token.
		/// </summary>
		/// <param name="accessToken">Access token.</param>
		public void setAccessToken(string accessToken)
		{
			this.accessToken = accessToken;
		}

		/// <summary>
		/// Sets the UUID.
		/// </summary>
		/// <param name="uuid">UUID.</param>
		private void setUuid(string uuid)
		{
			this.uuid = uuid;
		}

	}

}
