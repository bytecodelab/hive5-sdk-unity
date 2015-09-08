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
	public class ListMailsResponseBody : IResponseBody
	{
		public List<Mail> Mails { set; get; }			

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<Mail> mails;

			try
			{
				mails = Mail.LoadList(JsonMapper.ToObject<LitJson.JsonData>(json["mails"].ToJson()));
			}
			catch (KeyNotFoundException)
			{
				mails = null;
			}

			return new ListMailsResponseBody() {
				Mails = mails
			};
		}

	}
}

