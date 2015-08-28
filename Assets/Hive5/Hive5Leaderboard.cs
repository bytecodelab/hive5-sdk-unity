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
       /** 
        * @api {POST} SubmitScore 점수 기록
        * @apiVersion 0.3.11-beta
        * @apiName SubmitScore
        * @apiGroup Leaderboard
        *
        * @apiParam {string} leaderboardKey 리더보드 Key
        * @apiParam {long} score 기록 점수
        * @apiParam {string} extrasJson 보조데이터 (JSON)
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.SubmitScore(leaderboardKey, score, callback);
        */
        public void SubmitScore(string leaderboardKey, int score, string extrasJson, Callback callback)
        {
			var url = Hive5Client.Instance.ComposeRequestUrl(string.Format(ApiPath.Leaderboard.SubmitScore, leaderboardKey));

            var requestBody = new
            {
                score = score,
                extras = extrasJson,
            };

            PostHttpAsync(url, requestBody, SubmitScoreResponseBody.Load, callback);
        }

        /** 
        * @api {GET} ListScores 랭킹 가져오기
        * @apiVersion 0.3.11-beta
        * @apiName ListScores
        * @apiGroup Leaderboard
        *
        * @apiParam {string} leaderboardKey 리더보드 Key
        * @apiParam {long} rankMin 랭킹 최저
        * @apiParam {long} rankMax 랭킹 최고
        * @apiParam {long} rangeMin 점수 범위 최저
        * @apiParam {long} rangeMax 점수 범위 최고
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.ListScores(leaderboardKey, rankMin, rankMax, rangeMin, rangeMax, callback);
        */
        public void ListScores(string leaderboardKey, List<string> objectClasses, long rankMin, long rankMax, long? rangeMin, long? rangeMax, Callback callback)
        {
            var url = Hive5Client.Instance.ComposeRequestUrl(string.Format(ApiPath.Leaderboard.ListScores, leaderboardKey));

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
            GetHttpAsync(url, parameters.data, ListScoresResponseBody.Load, callback);
        }

        /** 
        * @api {GET} GetMyScore 내 랭킹 확인
        * @apiVersion 0.3.11-beta
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
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.GetMyScore(leaderboardKey, rangeMin, rangeMax, callback);
        */
        public void GetMyScore(string leaderboardKey, long rangeMin, long rangeMax, Callback callback)
        {
            var url = Hive5Client.Instance.ComposeRequestUrl(string.Format(ApiPath.Leaderboard.GetMyScore, leaderboardKey));

            // Hive5 API 파라미터 셋팅
            TupleList<string, string> parameters = new TupleList<string, string>();
            parameters.Add("leaderboard_key", leaderboardKey);
            parameters.Add("range_min", rangeMin.ToString());
            parameters.Add("range_max", rangeMax.ToString());

            // WWW 호출
            GetHttpAsync(url, parameters.data, GetMyScoreResponseBody.Load, callback);
        }

        /** 
        * @api {GET} ListSocialScores 친구 랭킹 가져오기
        * @apiVersion 0.3.11-beta
        * @apiName ListSocialScores
        * @apiGroup Leaderboard
        *
        * @apiParam {string} leaderboardKey 리더보드 Key
        * @apiParam {Callback} callback 콜백 함수
        *
        * @apiSuccess {string} resultCode Error Code 참고
        * @apiSuccess {string} resultMessage 요청 실패시 메시지
        * @apiExample Example usage:
        * Hive5Client hive5 = Hive5Client.Instance;
        * hive5.ListSocialScores(leaderboardKey, CallBack callback);
        */
        public void ListSocialScores(string leaderboardKey, List<string> objectClasses, Callback callback)
        {
            var url = Hive5Client.Instance.ComposeRequestUrl(string.Format(ApiPath.Leaderboard.ListSocialScores, leaderboardKey));

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
