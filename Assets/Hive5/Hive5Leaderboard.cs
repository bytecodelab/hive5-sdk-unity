using UnityEngine;
using System;
using System.Collections.Generic;
using System.Text;
using System.Net;
using System.IO;
using System.Collections;
using LitJson;
using Hive5;
using Hive5.Models;
using Hive5.Util;


namespace Hive5
{
    /// <summary>
    /// Leaderboard에 대한 모든 기능을 포함하는 클래스
    /// </summary>
    public class Hive5Leaderboard
    {
        /// <summary>
        /// 점수를 리더보드에 제출합니다.
        /// </summary>
        /// <param name="leaderboardKey">리더보드 키</param>
        /// <param name="score">기록 점수</param>
        /// <param name="extrasJson">추가데이터 (JSON)</param>
        /// <param name="callback">콜백 함수</param>
        public void SubmitScore(string leaderboardKey, int score, string extrasJson, Callback callback)
        {
			var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Leaderboard.SubmitScore, leaderboardKey));

            var requestBody = new
            {
                score = score,
                extras = extrasJson,
            };

            Hive5Http.Instance.PostHttpAsync(url, requestBody, SubmitScoreResponseBody.Load, callback);
        }
        
        /// <summary>
        /// 랭킹 가져오기
        /// </summary>
        /// <param name="leaderboardKey">리더보드 키</param>
        /// <param name="objectKeys">HObject 키들</param>
        /// <param name="rankMin">랭킹 최저</param>
        /// <param name="rankMax">랭킹 최고</param>
        /// <param name="rangeMin">점수 범위 최저</param>
        /// <param name="rangeMax">점수 범위 최고</param>
        /// <param name="callback">콜백 함수</param>
        public void ListScores(string leaderboardKey, List<string> objectKeys, long rankMin, long rankMax, long? rangeMin, long? rangeMax, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Leaderboard.ListScores, leaderboardKey));

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("leaderboard_key", leaderboardKey);

            if (objectKeys != null)
            {
                parameters.Add("object_key", string.Join(",", objectKeys.ToArray()));
            }


            parameters.Add("rank_min", rankMin.ToString());
            parameters.Add("rank_max", rankMax.ToString());
            if (rangeMin != null)
                parameters.Add("range_min", rangeMin.ToString());
            if (rangeMax != null)
                parameters.Add("range_max", rangeMax.ToString());

            Hive5Http.Instance.GetHttpAsync(url, parameters, ListScoresResponseBody.Load, callback);
        }

        /// <summary>
        /// 내 랭킹을 얻어옵니다.
        /// </summary>
        /// <param name="leaderboardKey">리더보드 키</param>
        /// <param name="rangeMin">랭킹 최저</param>
        /// <param name="rangeMax">랭킹 최고</param>
        /// <param name="callback">콜백 함수</param>
        public void GetMyScore(string leaderboardKey, long rangeMin, long rangeMax, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Leaderboard.GetMyScore, leaderboardKey));

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("leaderboard_key", leaderboardKey);
            parameters.Add("range_min", rangeMin.ToString());
            parameters.Add("range_max", rangeMax.ToString());

            Hive5Http.Instance.GetHttpAsync(url, parameters, GetMyScoreResponseBody.Load, callback);
        }

        /// <summary>
        /// 친구들의 랭킹을 반환합니다.
        /// </summary>
        /// <param name="leaderboardKey">리더보드 키</param>
        /// <param name="objectKeys">HObject 키들</param>
        /// <param name="callback">콜백 함수</param>
        public void ListSocialScores(string leaderboardKey, List<string> objectKeys, Callback callback)
        {
            var url = Hive5Client.ComposeRequestUrl(string.Format(ApiPath.Leaderboard.ListSocialScores, leaderboardKey));

            Dictionary<string, string> parameters = new Dictionary<string, string>();
            parameters.Add("leaderboard_key", leaderboardKey);

            if (objectKeys != null)
            {
                parameters.Add("object_key", string.Join(",", objectKeys.ToArray()));
            }

            Hive5Http.Instance.GetHttpAsync(url, parameters, ListScoresResponseBody.Load, callback);
        }
    }
}
