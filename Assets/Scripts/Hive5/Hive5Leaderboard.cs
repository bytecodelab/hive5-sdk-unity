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
	public class Hive5Leaderboard : Hive5API {

		Hive5Core hive5 = Hive5Core.Instance;

		/// <summary>
		/// Submits the score.
		/// </summary>
		/// <param name="leaderboardId">Leaderboard identifier.</param>
		/// <param name="score">Score.</param>
		/// <param name="callback">Callback.</param>
		public void submitScore(long leaderboardId, long score, CallBack callback)
		{
			var url = hive5.initializeUrl (string.Format ("leaderboards/{0}/scores/{1}", leaderboardId, score));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				leaderboard_id 	= leaderboardId,
				score			= score
			};
			
			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, requestBody, CommonResponseBody.Load, callback)
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
			var url = hive5.initializeUrl (string.Format ("leaderboards/{0}/my_score", leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());

			// WWW 호출
			hive5.asyncRoutine ( 
            	hive5.getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
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
			var url = hive5.initializeUrl (string.Format ("leaderboards/{0}/my_score", leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());

			// WWW 호출
			hive5.asyncRoutine ( 
            	hive5.getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
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
			var url = hive5.initializeUrl (string.Format ("leaderboards/{0}/social_scores", leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			Array.ForEach ( itemKeys, key => { parameters.Add( ParameterKey.itemKey, key ); });
			Array.ForEach ( userDataKeys, key => { parameters.Add( ParameterKey.userDataKey, key ); });
			
			// WWW 호출
			hive5.asyncRoutine ( 
            	hive5.getHTTP (url, parameters.data, CommonResponseBody.Load, callback) 
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
			var url = hive5.initializeUrl(string.Format("leaderboards/prize/{0}", leaderboardId));

			// WWW 호출
			hive5.asyncRoutine (
				hive5.postHTTP(url, new {}, CommonResponseBody.Load, callback)
			);
		}

	}

}
