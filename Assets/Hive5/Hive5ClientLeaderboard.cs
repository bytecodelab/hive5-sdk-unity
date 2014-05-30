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
			Leaderboard API Group
		*********************************************************************************/
		
		/// <summary>
		/// Submits the score.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="score">Score.</param>
		/// <param name="callback">Callback.</param>
		public void SubmitScore(long leaderboardId, long score, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.LeaderboardSubmitScores, leaderboardId, score));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				leaderboard_id 	= leaderboardId,
				score			= score
			};
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, requestBody, SubmitScoreResponseBody.Load, callback)
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
		public void GetScores(long leaderboardId, string[] itemKeys, string[] userDataKeys, long rankMin, long rankMax, long? rangeMin, long? rangeMax, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.LeaderboardScores, leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("rank_min", rankMin.ToString());
			parameters.Add ("rank_max", rankMax.ToString());
			if (rangeMin != null)
				parameters.Add ("range_min", rangeMin.ToString());
			if (rangeMax != null)
				parameters.Add ("range_max", rangeMax.ToString());
			
			// WWW 호출
			StartCoroutine ( 
			                GetHttp (url, parameters.data, GetScoresResponseBody.Load, callback) 
			                );
		}
		
		/// <summary>
		/// Gets my score.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="rangeMin">Range minimum.</param>
		/// <param name="rangeMax">Range max.</param>
		/// <param name="callback">Callback.</param>
		public void GetMyScore(long leaderboardId, long rangeMin, long rangeMax, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.LeaderboardMyScore, leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());
			
			// WWW 호출
			StartCoroutine ( 
			                GetHttp (url, parameters.data, GetMyScoreResponseBody.Load, callback) 
			                );
		}
		
		/// <summary>
		/// Gets the social scores.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="itemKeys">Item keys.</param>
		/// <param name="userDataKeys">User data keys.</param>
		/// <param name="callback">Callback.</param>
		public void GetSocialScores(long leaderboardId, string[] itemKeys, string[] userDataKeys, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.LeaderboardSocialScores, leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.ItemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.UserDataKey, key ); });
			
			// WWW 호출
			StartCoroutine ( 
			                GetHttp (url, parameters.data, GetSocialScoresResponseBody.Load, callback) 
			                );
		}
		
		/// <summary>
		/// Prize the specified leaderboardId and callback.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="callback">Callback.</param>
		public void Prize(string leaderboardId, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.LeaderboardPrize, leaderboardId));
			
			// WWW 호출
			StartCoroutine (
				PostHttp(url, new {}, PrizeResponseBody.Load, callback)
				);
		}

	}

}
