using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Model
{
    /// <summary>
    /// 포럼 쓰레드 모델 클래스
    /// </summary>
    public class ForumThread
    {
        /// <summary>
        /// 고유아이디
        /// </summary>
        public int Id { get; set; }
        /// <summary>
        /// 제목
        /// </summary>
        public string Title { get; set; }
        /// <summary>
        /// 내용
        /// </summary>
        public string Content { get; set; }
        /// <summary>
        /// 보조데이터
        /// </summary>
        public JsonData Extras { get; set; }
        /// <summary>
        /// 생성된 시점
        /// </summary>
        public DateTime CreatedAt { get; set; }

        /// <summary>
        /// ForumThread를 나타내는 Json 데이터를 읽어 ForumThread를 생성하여 반환함
        /// </summary>
        /// <param name="jsonData">ForumThread를 나타내는 Json 데이터</param>
        /// <returns>ForumThread</returns>
        public static ForumThread Load(JsonData jsonData)
        {
            ForumThread thread = new ForumThread()
            {
                Id = jsonData["id"].ToInt(),
                Title = (string)jsonData["title"],
                Content = (string)jsonData["content"],
                Extras = jsonData["extras"],
                CreatedAt = DateTime.Parse((string)jsonData["created_at"]),
            };

            return thread;
        }

        /// <summary>
        /// ForumThread 배열을 나타내는 Json 데이터를 읽어 ForumThread 리스트를 생성하여 반환함
        /// </summary>
        /// <param name="jsonData">ForumThread 배열을 나타내는 Json 데이터</param>
        /// <returns>ForumThread 리스트</returns>
        public static List<ForumThread> LoadList(JsonData jsonData)
        {
            if (jsonData == null)
                return new List<ForumThread>();

            var threads = new List<ForumThread>();
            for (int i = 0; i < jsonData.Count; i++)
            {
                threads.Add(ForumThread.Load(jsonData[i]));
            }

            return threads;
        }
    }
}
