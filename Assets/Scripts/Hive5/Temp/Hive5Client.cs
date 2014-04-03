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

		public delegate void apiCallBack (Hive5ResultCode resultCode, JsonData jsonObject);

		public string host 			= APIServer.betaHost;
		public string version 		= APIServer.version;
		public string appKey 		= Hive5Config.appKey;

		private string uuid 		= "uuid";
		private string accessToken 	= "";

		private bool isDebug 		= false;
		private bool loginState		= false;

		private Hive5APIZone zone	= Hive5APIZone.Beta;

		protected Hive5Client () {}

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
		/// Sets the user data.
		/// </summary>
		/// <param name="requestBody">Request body.</param>
		/// <param name="callBack">Call back.</param>
		public void setUserData(object requestBody, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.userData);

			// 코루틴 WWW 호출
			StartCoroutine( postHTTP(url, requestBody, callBack) );
		}

		/// <summary>
		/// Gets the user data.
		/// </summary>
		/// <param name="dataKeys">Data keys.</param>
		/// <param name="callback">Callback.</param>
		public void getUserData(string[] dataKeys, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.userData);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( dataKeys, key => { parameters.Add( ParameterKey.key, key ); } );

			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Kakao Login API
		/// </summary>
		/// <param name="userId">카카오 user id</param>
		/// <param name="accessToken">카카오 로그인 후 발급 받은 access token</param>
		/// <param name="sdkVersion">클라이언트에서 사용하고 있는 카카오 sdk의 버전</param>
		/// <param name="os">OS 구분자 'android' 또는 'ios'</param>
		/// <param name="userDataKey">로그인 후 가져와야할 사용자 user data의 key 목록</param>
		/// <param name="itemKey">로그인 후 가져와야할 사용자 item의 key 목록</param>
		/// <param name="configKey">로그인 후 가져와야할 사용자 configuration의 key 목록</param>
		/// <param name="completedMissions">로그인 후 가져와야 할 사용자 완료 Mission의 Key 목록</param>
		/// <param name="callback">콜백 함수</param>
		public void loginKakao(string userId, string accessToken, string sdkVersion, string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.kakaoLogin);

			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add( ParameterKey.userId, userId );
			parameters.Add( ParameterKey.accessToken, accessToken );
			parameters.Add( ParameterKey.sdkVersion, sdkVersion );
			parameters.Add( ParameterKey.OS, os );

			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); } );
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); } );
			Array.ForEach ( configKeys, key => { parameters.Add( ParameterKey.configKey, key ); } );
			
			// 코루틴 WWW 호출
			StartCoroutine(getHTTP(url, parameters.data, (resultCode, response) => { 
				if (resultCode == Hive5ResultCode.Success)
					setAccessToken(response[ResponseKey.accessToken].ToString()); 

				callback(resultCode, response);
			}));
		}

		/// <summary>
		/// Login API
		/// </summary>
		/// <api version="V4"/>
		/// <param name="platform">Platform.</param>
		/// <param name="userId">User identifier.</param>
		/// <param name="accessToken">Access token.</param>
		/// <param name="sdkVersion">Sdk version.</param>
		/// <param name="os">Os.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="configKeys">Config keys.</param>
		/// <param name="callback">Callback.</param>
		public void login(string platform, string userId, string accessToken, string sdkVersion, string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.kakaoLogin);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add( ParameterKey.userId, userId );
			parameters.Add( ParameterKey.accessToken, accessToken );
			parameters.Add( ParameterKey.sdkVersion, sdkVersion );
			parameters.Add( ParameterKey.OS, os );
			
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); } );
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); } );
			Array.ForEach ( configKeys, key => { parameters.Add( ParameterKey.configKey, key ); } );
			
			// 코루틴 WWW 호출
			StartCoroutine(getHTTP(url, parameters.data, (resultCode, response) => { 
				if (resultCode == Hive5ResultCode.Success)
					setAccessToken(response[ResponseKey.accessToken].ToString()); 
				
				callback(resultCode, response);
			}));
		}


		/// <summary>
		/// Updates the friends.
		/// </summary>
		/// <param name="friend_ids">Friend_ids.</param>
		/// <param name="callBack">Call back.</param>
		public void updateFriends(string[] friend_ids, apiCallBack callBack)
		{
			// Hive5 API URL 초기화	
			var url = initializeUrl("friends/update");

			var requestBody = new {
				friends = friend_ids
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));
		}

		/// <summary>
		/// Updates the kakao friends.
		/// </summary>
		/// <param name="appFriendsInfo">App friends info.</param>
		/// <param name="callBack">Call back.</param>
		public void updateKakaoFriends(JsonData appFriendsInfo, apiCallBack callBack)
		{
			// Hive5 API URL 초기화	
			var url = initializeUrl("kakao_friends");

			var requestBody = JsonMapper.ToObject ("{\"app_friends_info\":"+appFriendsInfo.ToJson()+"}");

			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));
		}


		/// <summary>
		/// Anonymous Login API
		/// </summary>
		/// <param name="os">OS 구분자 'android' 또는 'ios'</param>
		/// <param name="userDataKey">로그인 후 가져와야할 사용자 user data의 key 목록</param>
		/// <param name="itemKey">로그인 후 가져와야할 사용자 item의 key 목록</param>
		/// <param name="configKey">로그인 후 가져와야할 사용자 configuration의 key 목록</param>
		/// <param name="callback">콜백 함수</param>
		public void loginAnonymous(string os, string[] userDataKeys, string[] itemKeys, string[] configKeys, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.anonymousLogin);
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add(ParameterKey.OS, os);

			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); });
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });
			Array.ForEach ( configKeys, key => { parameters.Add( ParameterKey.configKey, key ); });
			
			// 코루틴 WWW 호출
			StartCoroutine(getHTTP(url, parameters.data, (resultCode, response) => { 
				if (resultCode == Hive5ResultCode.Success)
					setAccessToken(response[ResponseKey.accessToken].ToString()); 
				
				callBack(resultCode, response);
			}));
		}

		/// <summary>
		/// Rounds the start.
		/// </summary>
		/// <param name="roundRuleId">Round rule identifier.</param>
		/// <param name="callBack">Call back.</param>
		public void startRound(long roundRuleId,  apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(String.Format("rounds/start?rule_id={0}", roundRuleId));
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, new {}, callBack));
		}
		
		/// <summary>
		/// Rounds the end.
		/// </summary>
		/// <param name="roundId">Round identifier.</param>
		/// <param name="callBack">Call back.</param>
		public void endRounds(long roundId, object requestBody, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(String.Format("rounds/end/{0}", roundId));
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));
		}
		
		
		/// <summary>
		/// Consumes the item.
		/// </summary>
		/// <param name="requestBody">Request body.</param>
		/// <param name="callBack">Call back.</param>
		public void consumeItem(object requestBody, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.consumeItem);
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));
		}

		/// <summary>
		/// Gets the items.
		/// </summary>
		/// <param name="requestBody">Request body.</param>
		/// <param name="callback">Callback.</param>
		public void getItems(string[] itemKeys, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("items");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });
			
			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Sets the item convert.
		/// </summary>
		/// <param name="itemConvertKey">Item convert key.</param>
		/// <param name="callBack">Call back.</param>
		public void convertItem(string itemConvertKey,  apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(String.Format("items/convert/{0}",itemConvertKey));
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, new {}, callBack));
		}



		/// <summary>
		/// Posts the mail.
		/// </summary>
		/// <param name="content">Content.</param>
		/// <param name="platformUserId">Platform user identifier.</param>
		/// <param name="tags">Tags.</param>
		/// <param name="callBack">Call back.</param>
		public void createMail(string content, string platformUserId, string[] tags, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.postMail);


			var requestBody = new {
				content = content,
				paltform_user_id = platformUserId,
				tags = tags
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));
		}

		/// <summary>
		/// Gets the mails.
		/// </summary>
		/// <param name="limit">Limit.</param>
		/// <param name="order">Order.</param>
		/// <param name="afterMailId">After mail identifier.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="callback">Callback.</param>
		public void getMails(int limit, string order, long afterMailId, string tag, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(APIPath.getMails);

			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("limit", limit.ToString());
			parameters.Add ("order", order);
			parameters.Add ("after_mail_id", afterMailId.ToString());
			parameters.Add ("tag", tag);
			
			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}


		/// <summary>
		/// Gets the mail count.
		/// </summary>
		/// <param name="order">Order.</param>
		/// <param name="afterMailId">After mail identifier.</param>
		/// <param name="tag">Tag.</param>
		/// <param name="callback">Callback.</param>
		public void getMailCount(string order, long afterMailId, string tag, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("mails/count");
			
			TupleList<string, string> parameters = new TupleList<string, string> ();
			parameters.Add ("order", order);
			parameters.Add ("after_mail_id", afterMailId.ToString());
			parameters.Add ("tag", tag);
			
			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Updates the mail.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
		public void updateMail(long mailId, string content, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format("mails/update/{0}", mailId));
			
			var requestBody = new {
				content	= content
			};
			
			// 코루틴 WWW 호출
			StartCoroutine (postHTTP (url, requestBody, callback));
		}

		/// <summary>
		/// Deletes the mail.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="content">Content.</param>
		/// <param name="callback">Callback.</param>
		public void deleteMail(long mailId, string content, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format("mails/update/{0}", mailId));
			
			var requestBody = new {
				content	= content
			};
			
			// 코루틴 WWW 호출
			StartCoroutine (postHTTP (url, requestBody, callback));
		}

		public void batchDeleteMail(long fromMailId, long toMailId, apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("mails/delete_all");
			
			var requestBody = new {
				from_mail_id	= fromMailId,
				to_mail_id		= toMailId
			};
			
			// 코루틴 WWW 호출
			StartCoroutine (postHTTP (url, requestBody, callback));
		}


		/// <summary>
		/// Attachs the mail tag.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="tags">Tags.</param>
		/// <param name="callback">Callback.</param>
		public void attachMailTag(long mailId, string[] tags,  apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format("mails/{0}/add_tags", mailId));
			
			var requestBody = new {
				tags	= tags
			};
			
			// 코루틴 WWW 호출
			StartCoroutine (postHTTP (url, requestBody, callback));
		}

		/// <summary>
		/// Detachs the mail tag.
		/// </summary>
		/// <param name="mailId">Mail identifier.</param>
		/// <param name="tags">Tags.</param>
		/// <param name="callback">Callback.</param>
		public void detachMailTag(long mailId, string[] tags,  apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format("mails/{0}/remove_tags", mailId));
			
			var requestBody = new {
				tags	= tags
			};
			
			// 코루틴 WWW 호출
			StartCoroutine (postHTTP (url, requestBody, callback));
		}


		/// <summary>
		/// Registers the push token.
		/// </summary>
		/// <param name="platform">Platform.</param>
		/// <param name="token">Token.</param>
		/// <param name="callback">Callback.</param>
		public void registerPushToken(string platform, string token, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("push_tokens");

			var requestBody = new {
				push_platform 	= platform,
				push_token 		= token
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));

		}


		/// <summary>
		/// Agreements the specified generalAgreement, partnershipAgreement and callBack.
		/// </summary>
		/// <param name="generalAgreement">General agreement.</param>
		/// <param name="partnershipAgreement">Partnership agreement.</param>
		/// <param name="callBack">Call back.</param>
		public void agreements(string generalAgreement, string partnershipAgreement, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("agreements");
			
			var requestBody = new {
				general_agreement 		= generalAgreement,
				partnership_agreement 	= partnershipAgreement
			};

			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));
			
		}

		/// <summary>
		/// Gets the agreements.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public void getAgreements(apiCallBack callback)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("agreements");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Deletes the auth.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public void deleteAuth(apiCallBack callback)
		{
			var url = initializeUrl("auth/delete");

			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, new {}, callback));
		}

		/// <summary>
		/// Gets the configurations.
		/// </summary>
		/// <param name="configKeys">Config keys.</param>
		/// <param name="callback">Callback.</param>
		public void getConfigurations(string[] configKeys, apiCallBack callback)
		{
			var url = initializeUrl ("configurations");

			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( configKeys, key => { parameters.Add( ParameterKey.configKey, key ); });

			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Logs the specified callback.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public void logs(apiCallBack callback)
		{
			var url = initializeUrl("auth/delete");
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, new {}, callback));
		}

		/// <summary>
		/// Gifts the item.
		/// </summary>
		/// <param name="kakao_user_id">Kakao_user_id.</param>
		/// <param name="itemKey">Item key.</param>
		/// <param name="count">Count.</param>
		/// <param name="mail">Mail.</param>
		/// <param name="callBack">Call back.</param>
		public void giftItem(string kakao_user_id, string itemKey, int count, string mail, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("items/gift");
			
			var requestBody = new {
				kakao_user_id 	= kakao_user_id,
				Item			= itemKey,
				count			= count,
				mail			= mail
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));	
		}


		/// <summary>
		/// Completes the mission.
		/// </summary>
		/// <param name="missionKey">Mission key.</param>
		/// <param name="callback">Callback.</param>
		public void completeMission(string missionKey, apiCallBack callback)
		{
			var url = initializeUrl (string.Format("missions/complete/{0}", missionKey));
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, new {}, callback));	
		}

		/// <summary>
		/// Batchs the complete missions.
		/// </summary>
		/// <param name="missionKeys">Mission keys.</param>
		/// <param name="callback">Callback.</param>
		public void batchCompleteMissions(string[] missionKeys, apiCallBack callback)
		{
			var url = initializeUrl ("missions/batch_complete");

			var requestBody = new {
				keys 	= missionKeys
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callback));	
		}

		/// <summary>
		/// Gets the completed missions.
		/// </summary>
		/// <param name="callback">Callback.</param>
		public void getCompletedMissions(apiCallBack callback)
		{
			var url = initializeUrl ("missions/completed");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}


		/// <summary>
		/// Gets the reward.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="callback">Callback.</param>
		public void getReward(long rewardId, apiCallBack callback)
		{
			var url = initializeUrl (string.Format ("rewards/{0}", rewardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			
			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Applies the reward.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void applyReward(long rewardId, bool deleteMail, apiCallBack callback)
		{
			var url = initializeUrl (string.Format ("rewards/apply/{0}", rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callback));	
		}

		/// <summary>
		/// Applies all reward.
		/// </summary>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void applyAllReward(bool deleteMail, apiCallBack callback)
		{
			var url = initializeUrl ("rewards/apply_all");
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callback));	
		}

		/// <summary>
		/// Invalidates the reward.
		/// </summary>
		/// <param name="rewardId">Reward identifier.</param>
		/// <param name="deleteMail">If set to <c>true</c> delete mail.</param>
		/// <param name="callback">Callback.</param>
		public void invalidateReward(long rewardId, bool deleteMail, apiCallBack callback)
		{
			var url = initializeUrl (string.Format ("rewards/invalidate/{0}", rewardId));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				delete_mail 	= deleteMail
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callback));	
		}

		/// <summary>
		/// Submits the score.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="score">Score.</param>
		/// <param name="callback">Callback.</param>
		public void submitScore(long leaderboardId, long score, apiCallBack callback)
		{
			var url = initializeUrl (string.Format ("leaderboards/{0}/scores/{1}", leaderboardId, score));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				leaderboard_id 	= leaderboardId,
				score			= score
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callback));	
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
		public void getScores(long leaderboardId, string[] itemKeys, string[] userDataKeys, long rankMin, long rankMax, long rangeMin, long rangeMax, apiCallBack callback)
		{
			var url = initializeUrl (string.Format ("leaderboards/{0}/my_score", leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());
			
			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Gets my score.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="rangeMin">Range minimum.</param>
		/// <param name="rangeMax">Range max.</param>
		/// <param name="callback">Callback.</param>
		public void getMyScore(long leaderboardId, long rangeMin, long rangeMax, apiCallBack callback)
		{
			var url = initializeUrl (string.Format ("leaderboards/{0}/my_score", leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());

			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Gets the social scores.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="callback">Callback.</param>
		public void getSocialScores(long leaderboardId, string[] itemKeys, string[] userDataKeys, apiCallBack callback)
		{
			var url = initializeUrl (string.Format ("leaderboards/{0}/social_scores", leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); });
			
			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Gets the friends info.
		/// </summary>
		/// <param name="platformUserId">Platform user identifier.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="callback">Callback.</param>
		public void getFriendsInfo(string[] platformUserIds, string[] itemKeys, string[] userDataKeys, apiCallBack callback)
		{
			var url = initializeUrl ("friends/info");
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			Array.ForEach ( platformUserIds, key => { parameters.Add( "platform_user_id", key ); }); 
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); });

			StartCoroutine ( getHTTP (url, parameters.data, callback) );
		}

		/// <summary>
		/// Prize the specified leaderboardId and callBack.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="callBack">Call back.</param>
		public void prize(string leaderboardId, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format("leaderboards/prize/{0}", leaderboardId));
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, new {}, callBack));	
		}

		/// <summary>
		/// Creates the apple purchase.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void createApplePurchase(string productCode, string receiverKakaoUserId, string mailForReceiver, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("apple_purchases");
			
			var requestBody = new {
				product_code 			= productCode,
				receiver_kakao_user_id	= receiverKakaoUserId,
				mail_for_receiver		= mailForReceiver
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));	
		}


		/// <summary>
		/// Creates the google purchase.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="receiverKakaoUserId">Receiver kakao user identifier.</param>
		/// <param name="mailForReceiver">Mail for receiver.</param>
		/// <param name="callBack">Call back.</param>
		public void createGooglePurchase(string productCode, string receiverKakaoUserId, string mailForReceiver, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("google_purchases");
			
			var requestBody = new {
				product_code 			= productCode,
				receiver_kakao_user_id	= receiverKakaoUserId,
				mail_for_receiver		= mailForReceiver
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));	
		}


		/// <summary>
		/// Creates the naver purchase.
		/// </summary>
		/// <param name="productCode">Product code.</param>
		/// <param name="payement_sequence">Payement_sequence.</param>
		/// <param name="callBack">Call back.</param>
		public void createNaverPurchase(string productCode, string payement_sequence, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl("naver_purchases");
			
			var requestBody = new {
				product_code 		= productCode,
				payement_sequence	= payement_sequence
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));	
		}


		/// <summary>
		/// Completes the naver purchase.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="listPrice">List price.</param>
		/// <param name="purchasedPrice">Purchased price.</param>
		/// <param name="currency">Currency.</param>
		/// <param name="callBack">Call back.</param>
		public void completeNaverPurchase(long id, long listPrice, long purchasedPrice, string currency, apiCallBack callBack)
		{
			// Hive5 API URL 초기화
			var url = initializeUrl(string.Format("naver_purchases/complete/{0}", id));
			
			var requestBody = new {
				list_price 		= listPrice,
				purchased_price	= purchasedPrice,
				currency		= currency
			};
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));	
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
		public void completeGooglePurchase(long id, long listPrice, long purchasedPrice, string currency, string purchaseData, string signature, apiCallBack callBack)
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
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));	
		}


		/// <summary>
		/// Completes the apple purchase.
		/// </summary>
		/// <param name="id">Identifier.</param>
		/// <param name="listPrice">List price.</param>
		/// <param name="purchasedPrice">Purchased price.</param>
		/// <param name="currency">Currency.</param>
		/// <param name="receipt">Receipt.</param>
		/// <param name="isSandbox">If set to <c>true</c> is sandbox.</param>
		/// <param name="callBack">Call back.</param>
		public void completeApplePurchase(long id, long listPrice, long purchasedPrice, string currency, string receipt, bool isSandbox, apiCallBack callBack)
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
			
			// 코루틴 WWW 호출
			StartCoroutine(postHTTP(url, requestBody, callBack));	
		}





		/// <summary>
		/// Https the get.
		/// </summary>
		/// <returns>The get.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		/// <param name="callback">Callback.</param>
		private IEnumerator getHTTP(string url, List<KeyValuePair<string, string>> parameters,  apiCallBack callBack)
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

			if(isDebug) Debug.Log ("www reuqest URL = " + newUrl);
			if(isDebug) Debug.Log ("www response = " + www.text);

			JsonData response = JsonMapper.ToObject (www.text);
			Hive5ResultCode resultCode = (Hive5ResultCode) ((int)response[ResponseKey.resultCode]);
			callBack(resultCode, response);
		}

		/// <summary>
		/// Https the post.
		/// </summary>
		/// <returns>The post.</returns>
		/// <param name="url">URL.</param>
		/// <param name="parameters">Parameters.</param>
		private IEnumerator postHTTP(string url, object requestBody, apiCallBack callBack)
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

			if(isDebug) Debug.Log ("www reuqest URL = " + url);
			if(isDebug) Debug.Log ("www request jsonBody= " + jsonString);
			if(isDebug) Debug.Log ("www response = " + www.text);
			
			JsonData response = JsonMapper.ToObject (www.text);
			Hive5ResultCode resultCode = (Hive5ResultCode) ((int)response[ResponseKey.resultCode]);
			callBack(resultCode, response);
		}
		
		/// <summary>
		/// Initializes the URL.
		/// </summary>
		/// <returns>The URL.</returns>
		/// <param name="path">Path.</param>
		private string initializeUrl(string path)
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
		private void setAccessToken(string accessToken)
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
