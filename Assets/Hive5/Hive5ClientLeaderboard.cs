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
#if UNITTEST
    public partial class Hive5Client : MockMonoSingleton<Hive5Client>
    {
#else
	public partial class Hive5Client : MonoSingleton<Hive5Client> {
#endif
        /********************************************************************************
			Leaderboard API Group
		*********************************************************************************/

        /** 
        * @api {POST} SubmitScore 점수 기록
        * @apiVersion 1.0.0-alpha
        * @apiName SubmitScore
        * @apiGroup Leaderboard
        *
        * @apiParam {string} leaderboardKey 리더보드 Key
        * @apiParam {long} score 기록 점수
        * @apiParam {long} extras 보조데이터(JSON)
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.SubmitScore(leaderboardKey, score, callback);
        */
        public void SubmitScore(string leaderboardKey, long score, object extras, Callback callback)
        {
			var url = InitializeUrl(string.Format(APIPath.LeaderboardSubmitScores, leaderboardKey));

            // Hive5 API 파라미터 셋팅
            var requestBody = new
            {
                score = score,
                extras = extras == null ? "" : extras
            };

            // WWW 호출
            PostHttpAsync(url, requestBody, SubmitScoreResponseBody.Load, callback);
        }

        /** 
        * @api {GET} GetScores 랭킹 가져오기
        * @apiVersion 1.0.0-alpha
        * @apiName GetScores
        * @apiGroup Leaderboard
        *
        * @apiParam {string} leaderboardKey 리더보드 Key
        * @apiParam {long} rankMin 랭킹 최저
        * @apiParam {long} rankMax 랭킹 최고
        * @apiParam {long} rangeMin 점수 범위 최저
        * @apiParam {long} rangeMax 점수 범위 최고
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.GetScores(leaderboardKey, rankMin, rankMax, rangeMin, rangeMax, callback);
        */
        public void GetScores(string leaderboardKey, List<string> objectClasses, long rankMin, long rankMax, long? rangeMin, long? rangeMax, Callback callback)
        {
            var url = InitializeUrl(string.Format(APIPath.LeaderboardScores, leaderboardKey));

            // Hive5 API 파라미터 셋팅
            TupleList<string, string> parameters = new TupleList<string, string>();
            parameters.Add("leaderboard_key", leaderboardKey);

            if (objectClasses != null)
            {
                foreach (var objectClass in objectClasses)
                {
                    parameters.Add("object_class", objectClass);
                }
            }


            parameters.Add("rank_min", rankMin.ToString());
            parameters.Add("rank_max", rankMax.ToString());
            if (rangeMin != null)
                parameters.Add("range_min", rangeMin.ToString());
            if (rangeMax != null)
                parameters.Add("range_max", rangeMax.ToString());

            // WWW 호출
            GetHttpAsync(url, parameters.data, GetScoresResponseBody.Load, callback);
        }

        /** 
        * @api {GET} GetMyScore 내 랭킹 확인
        * @apiVersion 1.0.0-alpha
        * @apiName GetMyScore
        * @apiGroup Leaderboard
        *
        * @apiParam {string} leaderboardKey 리더보드 Key
        * @apiParam {long} rankMin 랭킹 최저
        * @apiParam {long} rankMax 랭킹 최고
        * @apiParam {long} rangeMin 점수 범위 최저
        * @apiParam {long} rangeMax 점수 범위 최고
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.GetMyScore(leaderboardKey, rangeMin, rangeMax, callback);
        */
        public void GetMyScore(string leaderboardKey, long rangeMin, long rangeMax, Callback callback)
        {
            var url = InitializeUrl(string.Format(APIPath.LeaderboardMyScore, leaderboardKey));

            // Hive5 API 파라미터 셋팅
            TupleList<string, string> parameters = new TupleList<string, string>();
            parameters.Add("leaderboard_key", leaderboardKey);
            parameters.Add("range_min", rangeMin.ToString());
            parameters.Add("range_max", rangeMax.ToString());

            // WWW 호출
            GetHttpAsync(url, parameters.data, GetMyScoreResponseBody.Load, callback);
        }

        /** 
        * @api {GET} GetSocialScores 친구 랭킹 가져오기
        * @apiVersion 1.0.0-alpha
        * @apiName GetSocialScores
        * @apiGroup Leaderboard
        *
        * @apiParam {string} leaderboardKey 리더보드 Key
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {String} resultCode Error Code 참고
        * @apiSuccess {String} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.GetSocialScores(leaderboardKey, CallBack callback);
        */
        public void GetSocialScores(string leaderboardKey, List<string> objectClasses, Callback callback)
        {
            var url = InitializeUrl(string.Format(APIPath.LeaderboardSocialScores, leaderboardKey));

            // Hive5 API 파라미터 셋팅
            TupleList<string, string> parameters = new TupleList<string, string>();
            parameters.Add("leaderboard_key", leaderboardKey);

            if (objectClasses != null)
            {
                foreach (var objectClass in objectClasses)
                {
                    parameters.Add("object_class", objectClass);
                }
            }

            // WWW 호출
            GetHttpAsync(url, parameters.data, GetSocialScoresResponseBody.Load, callback);
        }
    }
}
