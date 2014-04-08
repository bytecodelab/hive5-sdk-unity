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
		private string host 	= APIServer.betaHost;
		private string version 	= APIServer.version;


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
			var url = initializeUrl(APIPath.kakaoLogin);
			
			Debug.Log ("login LoginState=" + LoginState);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add( ParameterKey.userId, userId );
			parameters.Add( ParameterKey.accessToken, accessToken );
			parameters.Add( ParameterKey.sdkVersion, sdkVersion );
			parameters.Add( ParameterKey.OS, os );
			
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); } );
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); } );
			Array.ForEach ( configKeys, key => { parameters.Add( ParameterKey.configKey, key ); } );
			
			StartCoroutine (
				getHTTP(url, parameters.data, LoginDataResponseBody.Load, ( response ) => { 
				if ( response.resultCode == Hive5ResultCode.Success)
				{
					setAccessToken(((LoginDataResponseBody)response.resultData).accessToken);
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
		public void unregister(string userId, string accessToken, CallBack callback)
		{
			
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
			var url = initializeUrl("items");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });
			
			// WWW 호출
			StartCoroutine (
				getHTTP (url, parameters.data, CommonResponseBody.Load, callback)
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
			var url = initializeUrl(String.Format("items/convert/{0}",itemConvertKey));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, new {}, CommonResponseBody.Load, callBack)
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
			var url = initializeUrl(APIPath.consumeItem);
			
			// WWW 호출
			StartCoroutine (
				postHTTP (url, requestBody, CommonResponseBody.Load, callBack)
			);
		}
		
		public void giftItem(string platformUserId, string itemKey, int count, string mail, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("items/gift");
			
			var requestBody = new {
				platform_user_id = platformUserId,
				Item	= itemKey,
				count	= count,
				mail	= mail
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP (url, requestBody, CommonResponseBody.Load, callback)
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
			var url = initializeUrl (string.Format ("leaderboards/{0}/scores/{1}", leaderboardId, score));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				leaderboard_id 	= leaderboardId,
				score			= score
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CommonResponseBody.Load, callback)
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
			var url = initializeUrl (string.Format ("leaderboards/{0}/my_score", leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
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
			var url = initializeUrl (string.Format ("leaderboards/{0}/my_score", leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
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
			var url = initializeUrl (string.Format ("leaderboards/{0}/social_scores", leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); });
			
			// WWW 호출
			StartCoroutine ( 
		    	getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
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
			var url = initializeUrl(string.Format("leaderboards/prize/{0}", leaderboardId));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, new {}, CommonResponseBody.Load, callback)
			);
		}

		/********************************************************************************
			Mail API Group
		*********************************************************************************/

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
			var url = initializeUrl(APIPath.getMails);
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("limit", limit.ToString());
			parameters.Add ("order", order);
			parameters.Add ("after_mail_id", afterMailId.ToString());
			parameters.Add ("tag", tag);
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
            );
		}
		
		/// <summary>
		/// Getls the count.
		/// </summary>
		/// <param name="order">Order.</param>
		/// <param name="afterMailId">After mail identifier.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="callback">Callback.</param>
		public void getMailCount(string order, long afterMailId, string tag, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("mails/count");
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("order", order);
			parameters.Add ("after_mail_id", afterMailId.ToString());
			parameters.Add ("tag", tag);
			
			// WWW 호출
			StartCoroutine ( 
		    	getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
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
			var url = initializeUrl(string.Format("mails/update/{0}", mailId));
			
			var requestBody = new {
				content	= content
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CommonResponseBody.Load, callback)
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
			var url = initializeUrl (string.Format("missions/complete/{0}", missionKey));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, new {}, CommonResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Batchs the complete.
		/// </summary>
		/// <param name="missionKeys">Mission keys.</param>
		/// <param name="callback">Callback.</param>
		public void batchCompleteMission(string[] missionKeys, CallBack callback)
		{
			var url = initializeUrl ("missions/batch_complete");
			
			var requestBody = new {
				keys 	= missionKeys
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CommonResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Gets the completed.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public void getCompletedMissions(CallBack callback)
		{
			var url = initializeUrl ("missions/completed");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
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
		public void createGooglePurchase(string productCode, string receiverPlatformUserId, string mailForReceiver, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("google_purchases");
			
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
			var url = initializeUrl(string.Format("google_purchases/complete/{0}", id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency,
				purchase_data	= purchaseData,
				signature		= signature
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CommonResponseBody.Load, callback)
			);	
		}
		
		
		/// <summary>
		/// Creates the apple.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void createApplePurchase(string productCode, string receiverKakaoUserId, string mailForReceiver, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("apple_purchases");
			
			var requestBody = new {
				product_code 			= productCode,
				receiver_kakao_user_id	= receiverKakaoUserId,
				mail_for_receiver		= mailForReceiver
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CommonResponseBody.Load, callback)
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
			var url = initializeUrl(string.Format("apple_purchases/complete/{0}", id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency,
				receipt			= receipt,
				is_sandbox		= isSandbox
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, CommonResponseBody.Load, callback)
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
			var url = initializeUrl("push_tokens");
			
			var requestBody = new {
				push_platform 	= platform,
				push_token 		= token
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
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
			var url = initializeUrl (string.Format ("rewards/{0}", rewardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, GetUserDataResponseBody.Load, callback) 
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
			var url = initializeUrl (string.Format ("rewards/apply/{0}", rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
			);
		}
		
		/// <summary>
		/// Applies all.
		/// </summary>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void applyAllRewards(bool deleteMail, CallBack callback)
		{
			var url = initializeUrl ("rewards/apply_all");
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
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
			var url = initializeUrl (string.Format ("rewards/invalidate/{0}", rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
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
			var url = initializeUrl(String.Format("rounds/start?rule_id={0}", roundRuleId));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, new {}, SetUserDataResponseBody.Load, callback)
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
			var url = initializeUrl(String.Format("rounds/end/{0}", roundId));
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
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
			var url = initializeUrl("friends/update");
			
			// Request Body
			var requestBody = new {
				friends = friend_ids
			};
			
			// WWW 호출
			StartCoroutine (
				postHTTP(url, requestBody, SetUserDataResponseBody.Load, callback)
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
			var url = initializeUrl ("friends/info");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( platformUserIds, key => { parameters.Add( "platform_user_id", key ); }); 
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); });
			
			// WWW 호출
			StartCoroutine ( 
            	getHTTP (url, parameters.data, GetUserDataResponseBody.Load, callback) 
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
			var url = initializeUrl(APIPath.userData);
			
			var requestBody = new SetUserDataRequest ();
			var data = new List<KeyValueCommand> ();
			var userData = new KeyValueCommand () { key = key, value = value, command = Util.Util.getStringByCommandType(command) };
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
			var url = initializeUrl(APIPath.userData);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( dataKeys, key => { parameters.Add( ParameterKey.key, key ); } );
			
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
					host = APIServer.betaHost;
				break;

				// Real Server
				case Hive5APIZone.Real:
					host = APIServer.realHost;
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
			headers.Add(HeaderKey.appKey, this.appKey);
			headers.Add(HeaderKey.uuid, this.uuid);
			headers.Add(HeaderKey.token, this.accessToken);
			headers.Add(HeaderKey.contentType, HeaderValue.contentType);

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
			headers.Add(HeaderKey.appKey, this.appKey);
			headers.Add(HeaderKey.uuid, this.uuid);
			headers.Add(HeaderKey.token, this.accessToken);
			headers.Add(HeaderKey.contentType, HeaderValue.contentType);

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
			
			result.Add(HeaderKey.appKey, this.appKey);
			result.Add(HeaderKey.uuid, this.uuid);

			if (this.accessToken.Length > 0)
				result.Add(HeaderKey.token, this.accessToken);
			
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
