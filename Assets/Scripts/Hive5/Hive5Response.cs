using System;
using LitJson;
using Hive5.Model;

namespace Hive5
{
	/// <summary>
	/// Hive5 response.
	/// </summary>
	public class Hive5Response
	{
		public delegate IResponseBody dataLoader (JsonData response);

		public Hive5ResultCode 	resultCode { set; get; }
		public IResponseBody	resultData { set; get; }

		public static Hive5Response Load(dataLoader loader, string json)
		{
			JsonData response = JsonMapper.ToObject (json);

			Hive5ResultCode resultCode 	= (Hive5ResultCode) ((int)response[ResponseKey.resultCode]);
			IResponseBody resultData = loader(response);

			return new Hive5Response () {
				resultCode = resultCode,
				resultData = resultData
			};
		}
	}
}

