using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;


namespace Hive5.Model
{
    /// <summary>
    /// 점수 모델 클래스
    /// </summary>
    public class Score
    {
        /// <summary>
        /// 사용자
        /// </summary>
        public User User { get; set; }
        /// <summary>
        /// 점수
        /// </summary>
        public long value { set; get; }
        /// <summary>
        /// 순위
        /// </summary>
        public long rank { set; get; }
        /// <summary>
        /// 요청받은 HObject
        /// </summary>
        public string objects { set; get; }
        /// <summary>
        /// 추가 데이터(Submit 때 세팅한)
        /// </summary>
        public string extras { get; set; }

        /// <summary>
        /// Score를 나타내는 Json 데이터를 읽어 Score 인스턴스를 생성하여 반환함
        /// </summary>
        /// <param name="jsonData">Score를 나타내는 Json 데이터</param>
        /// <returns>Score 인스턴스</returns>
        public static Score Load(JsonData jsonData)
        {
            return new Score()
            {
                User = new User()
                {
                    platform = (string)jsonData["user"]["platform"],
                    id = (string)jsonData["user"]["id"],
                },
                value = jsonData["value"].ToLong(),
                rank = jsonData["rank"].ToLong(),
                extras = jsonData["extras"].ToJson(),
                objects = jsonData["objects"].ToJson(),
            };
        }

        /// <summary>
        /// Score 배열을 나타내는 Json 데이터를 읽어 Score 인스턴스 리스트를 생성하여 반환함
        /// </summary>
        /// <param name="jsonData">Score 배열을 나타내는 Json 데이터</param>
        /// <returns>Score 인스턴스 리스트</returns>
        public static List<Score> LoadList(JsonData jsonData)
        {
            var scores = new List<Score>();

            if (jsonData == null || jsonData.IsArray == false)
				return scores;

            for (int i = 0; i < jsonData.Count; i++)
            {
                scores.Add(Score.Load(jsonData[i]));
            }
            return scores;
        }
    }
}
