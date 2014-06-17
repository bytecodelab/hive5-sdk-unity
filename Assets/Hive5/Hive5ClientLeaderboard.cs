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
#if TEST    
    public partial class Hive5Client : MockMonoSingleton<Hive5Client> {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif

		/********************************************************************************
			Leaderboard API Group
		*********************************************************************************/
		
		/** 
		* @api {POST} SubmitScore 점수 기록
		* @apiVersion 1.0.0
		* @apiName SubmitScore
		* @apiGroup Leaderboard
		*
		* @apiParam {long} leaderboardId 리더보드 ID
		* @apiParam {long} score 기록 점수
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.SubmitScore(leaderboardId, score, callback);
		*/
		public void SubmitScore(long leaderboardId, long score, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.LeaderboardSubmitScores, leaderboardId, score));
			
			// Hive5 API 파라미터 셋팅
			var requestBody = new {
				leaderboard_id 	= leaderboardId,
				score			= score
			};
			
			// WWW 호출
			PostHttpAsync(url, requestBody, SubmitScoreResponseBody.Load, callback);
		}
		
		/** 
		* @api {GET} GetScores 랭킹 가져오기
		* @apiVersion 1.0.0
		* @apiName GetScores
		* @apiGroup Leaderboard
		*
		* @apiParam {long} leaderboardId 리더보드 ID
		* @apiParam {long} rankMin 랭킹 최저
		* @apiParam {long} rankMax 랭킹 최고
		* @apiParam {long} rangeMin 점수 범위 최저
		* @apiParam {long} rangeMax 점수 범위 최고
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetScores(leaderboardId, rankMin, rankMax, rangeMin, rangeMax, callback);
		*/
		public void GetScores(long leaderboardId, long rankMin, long rankMax, long? rangeMin, long? rangeMax, CallBack callback)
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
            GetHttpAsync(url, parameters.data, GetScoresResponseBody.Load, callback);
		}
		
		/** 
		* @api {GET} GetMyScore 내 랭킹 확인
		* @apiVersion 1.0.0
		* @apiName GetMyScore
		* @apiGroup Leaderboard
		*
		* @apiParam {long} leaderboardId 리더보드 ID
		* @apiParam {long} rankMin 랭킹 최저
		* @apiParam {long} rankMax 랭킹 최고
		* @apiParam {long} rangeMin 점수 범위 최저
		* @apiParam {long} rangeMax 점수 범위 최고
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetMyScore(leaderboardId, rangeMin, rangeMax, callback);
		*/
		public void GetMyScore(long leaderboardId, long rangeMin, long rangeMax, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.LeaderboardMyScore, leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());
			parameters.Add ("range_min", rangeMin.ToString());
			parameters.Add ("range_max", rangeMax.ToString());
			
			// WWW 호출
            GetHttpAsync(url, parameters.data, GetMyScoreResponseBody.Load, callback);
		}
		
		/** 
		* @api {GET} GetSocialScores 친구 랭킹 가져오기
		* @apiVersion 1.0.0
		* @apiName GetSocialScores
		* @apiGroup Leaderboard
		*
		* @apiParam {long} leaderboardId 리더보드 ID
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.GetSocialScores(leaderboardId, CallBack callback);
		*/
		public void GetSocialScores(long leaderboardId, CallBack callback)
		{
			var url = InitializeUrl (string.Format (APIPath.LeaderboardSocialScores, leaderboardId));
			
			// Hive5 API 파라미터 셋팅
			TupleList<string, string> parameters = new TupleList<string, string>();
			parameters.Add ("leaderboard_id", leaderboardId.ToString());

			// WWW 호출
            GetHttpAsync(url, parameters.data, GetSocialScoresResponseBody.Load, callback);
		}
		
		/** 
		* @api {POST} Prize 리더보드 보상 받기
		* @apiVersion 1.0.0
		* @apiName Prize
		* @apiGroup Leaderboard
		*
		* @apiParam {long} leaderboardId 리더보드 ID
		* @apiParam {CallBack} callback 콜백 함수
		*
		* @apiSuccess {String} resultCode Error Code 참고
		* @apiSuccess {String} resultMessage 요청 실패시 메시지
		* @apiExample Example usage:
		* Hive5Client hive5 = Hive5Client.Instance;
		* hive5.Prize(leaderboardId, callback);
		*/
		public void Prize(long leaderboardId, CallBack callback)
		{
			// Hive5 API URL 초기화
			var url = InitializeUrl(string.Format(APIPath.LeaderboardPrize, leaderboardId));
			
			// WWW 호출
            PostHttpAsync(url, new { }, PrizeResponseBody.Load, callback);
		}

	}

}
