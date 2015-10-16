using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Models
{
	/// <summary>
	/// 게임내우편 모델 클래스
	/// </summary>
	public class Mail
	{
        /// <summary>
        /// Mail의 고유아이디
        /// </summary>
		public long id { set; get; }
        /// <summary>
        /// 내용
        /// </summary>
		public string content { set; get; }
        /// <summary>
        /// 추가 데이터
        /// </summary>
        public string extras { get; set; }
        /// <summary>
        /// 보상이 있는지의 여부
        /// </summary>
		public bool reward { set; get; }

        /// <summary>
        /// 보상을 수락한 시점
        /// </summary>
        public DateTime? reward_accepted_at { get; set; }

        /// <summary>
        /// 태그 목록
        /// </summary>
		public string[] tags { set; get; }

        /// <summary>
        /// Mail를 나타내는 Json 데이터를 읽어 Mail를 생성하여 반환함
        /// </summary>
        /// <param name="jsonData">Mail를 나타내는 Json 데이터</param>
        /// <returns>Mail 인스턴스</returns>
		public static Mail Load(JsonData jsonData)
		{
			var mail = new Mail () {
				id =  jsonData["id"].ToLong(),
				content = (string)jsonData["content"],
                extras = jsonData["extras"] == null ? string.Empty : jsonData["extras"].ToJson(),
				reward = (bool)jsonData["reward"],
				tags = JsonMapper.ToObject<string[]> (jsonData ["tags"].ToJson ()),
                //reward_accepted_at = jsonData.ContainsKey("reward_accepted_at") == true ? DateTime.Parse((string)jsonData["reward_accepted_at"]) : null,
			};

            if (jsonData.ContainsKey("reward_accepted_at") == true)
            {
                mail.reward_accepted_at = DateTime.Parse((string)jsonData["reward_accepted_at"]);
            }
            return mail;
		}

		/// <summary>
		/// Mail 배열을 나타내는 Json 데이터를 읽어 Mail 리스트를 생성하여 반환함
		/// </summary>
		/// <param name="jsonData">Mail 배열을 나타내는 Json 데이터</param>
        /// <returns>Mail 인스턴스 리스트</returns>
		public static List<Mail> LoadList(JsonData jsonData)
		{
			var mails = new List<Mail>();
			
			if (jsonData == null || jsonData.IsArray == false)
				return mails;

			for (int i = 0; i < jsonData.Count; i++) 
			{
				mails.Add(Mail.Load(jsonData[i]));
			}

			return mails;
		}
	}
}
