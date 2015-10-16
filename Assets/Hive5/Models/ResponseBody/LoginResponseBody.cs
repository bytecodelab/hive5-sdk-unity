using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Models
{
	
	/// <summary>
	/// Login data.
	/// </summary>
	public class LoginResponseBody : IResponseBody
	{
        /// <summary>
        /// �׼��� ��ū
        /// </summary>
		public string 	AccessToken;
        /// <summary>
        /// ����Ű
        /// </summary>
        public string   SessionKey;
        /// <summary>
        /// Ȯ�� ���� ���ϰ���
        /// </summary>
		public int 		NewMailCount;
        /// <summary>
        /// �г���
        /// </summary>
        public string Nickname;
        /// <summary>
        /// ����� �ĺ���
        /// </summary>
        public User User;
        /// <summary>
        /// �÷��̾� �߰�������
        /// </summary>
        public string Extras;
		
		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static LoginResponseBody Load(JsonData json)
		{
			
			if (json == null)
				return null;

            var sessionKey = (string)json["session_key"];
			var newMailCount = (int)json["new_mail_count"];
			var accessToken = (string)json["access_token"];
			var userPlatform = (string)json["user"]["platform"];
			var userId = (string)json ["user"]["id"];
            var nickname = json["nickname"] ==null ? string.Empty : (string)json["nickname"];
            var extras = json.ContainsKey("extras") == true ? (string)json["extras"] : string.Empty;
			
			return new LoginResponseBody ()
			{
				AccessToken = accessToken, 
                SessionKey = sessionKey,
				NewMailCount = newMailCount, 
                User = new User() {
                    platform = userPlatform,
                    id = userId,
                },
                Nickname = nickname,
                Extras = extras,
			};
		}
		
	}
}
