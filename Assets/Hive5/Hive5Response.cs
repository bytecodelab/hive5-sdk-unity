using System;
using LitJson;
using Hive5;
using Hive5.Models;

namespace Hive5
{
	/// <summary>
	/// Hive5 응답 모델 클래스
	/// </summary>
	public class Hive5Response
	{
		public delegate IResponseBody dataLoader (JsonData response);

		public Hive5ErrorCode 	ResultCode { set; get; }
		public string 			ResultMessage { set; get; }
		public IResponseBody	ResultData { set; get; }

		/// <summary>
		/// Load the specified loader and json.
		/// </summary>
		/// <param name="loader">Loader.</param>
		/// <param name="json">Json.</param>
		public static Hive5Response Load(dataLoader loader, string json)
		{
			JsonData response = JsonMapper.ToObject (json);

			Hive5ErrorCode resultCode 	= (Hive5ErrorCode) ((int)response["result_code"]);
			string resultMessage = response.ContainsKey("result_message") == false ? string.Empty : (string)response["result_message"];

			IResponseBody resultData = null;
			if (resultCode == 0) {
				resultData = loader (response);
			}

			return new Hive5Response () {
				ResultCode = resultCode,
				ResultMessage = resultMessage,
				ResultData = resultData
			};
		}
	}
}

