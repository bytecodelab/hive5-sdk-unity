using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Models
{

	/// <summary>
	/// 플레이어의 추가데이터 가져오는 호출의 반환 클래스
	/// </summary>
	public class GetPlayerExtrasResponseBody : IResponseBody
	{
        /// <summary>
        /// 추가 데이터 (JSON)
        /// </summary>
		public string Extras { set; get; }
		

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

            string extras;

            try
            {
                extras = (string)json["extras"];
            }
            catch(KeyNotFoundException)
            {
                extras = "";
            }

			return new GetPlayerExtrasResponseBody() {
				Extras = extras
			};
		}

	}
}

