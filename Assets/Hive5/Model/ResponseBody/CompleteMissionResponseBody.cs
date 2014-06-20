using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{

	/// <summary>
	/// Login data.
	/// </summary>
	public class CompleteMissionResponseBody : IResponseBody
	{
		public long RewardId { set; get; }
		public long MailId { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

            long rewardId = -1;

            try
            {
                rewardId = (int)json ["reward_id"];
            }
            catch
            {
                rewardId = -1;
            }

            long mailId = -1;

            try
            {
                mailId = (int)json ["mail_id"];
            }
            catch
            {
                mailId = -1;
            }

			return new CompleteMissionResponseBody() {
				RewardId = rewardId,
				MailId = mailId,
			};
		}

	}
}

