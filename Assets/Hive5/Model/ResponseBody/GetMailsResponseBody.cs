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
	public class GetMailsResponseBody : IResponseBody
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
				mails = Mail.LoadList( json["mails"] );
			}
			catch (KeyNotFoundException)
			{
				mails = null;
			}

			return new GetMailsResponseBody() {
				Mails = mails
			};
		}

	}
}

