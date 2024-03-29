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
	public class SubmitScoreResponseBody : IResponseBody
	{
		public DateTime UpdatedAt { set; get; }	

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

            DateTime udpateAt;
            try 
            {
                udpateAt = DateTime.Parse((string)json["updated_at"]);
            }
            catch
            {
                udpateAt = default(DateTime);
            }

			return new SubmitScoreResponseBody() {
				UpdatedAt = udpateAt
			};
		}

	}
}

