using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Promotion data.
	/// </summary>
	public class Mail
	{
		public long id { set; get; }
		public string content { set; get; }
		public long rewardId { set; get; }
		public string[] tags { set; get; }

		public static Mail Load(JsonData json)
		{
			var id 			= (long)json["id"];
			var content 	= (string)json["content"];

			long rewardId = 0;
			try
			{
				rewardId 	= (long)json["reward_id"];
			}
			catch
			{
			}

			var tags 		= JsonMapper.ToObject<string[]> (json ["tags"].ToJson ());

			return new Mail () {
				id = id,
				content = content,
				rewardId = rewardId,
				tags = tags
			};
		}

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static List<Mail> LoadList(JsonData json)
		{
			var mails = new List<Mail>();
			
			if (json == null || json.IsArray == false)
				return mails;

			var listCount = json.Count;
			for (int currentCount = 0; currentCount < listCount; currentCount++) 
			{
				mails.Add(Mail.Load(JsonMapper.ToObject<LitJson.JsonData>(json[currentCount].ToJson())));
			}

			return mails;
		}
	}
}
