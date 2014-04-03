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
		public Hive5ResultCode 	resultCode;
		public IHive5ResultData	resultData;

		public Hive5Response(Hive5ResultCode resultCode, IHive5ResultData resultData)
		{
			this.resultCode = resultCode;
			this.resultData = resultData;
		}

		public static Hive5Response Load(string json)
		{
			JsonData response = JsonMapper.ToObject (json);

			Hive5ResultCode resultCode 	= (Hive5ResultCode) ((int)response[ResponseKey.resultCode]);
			IHive5ResultData resultData 	= LoginData.Load (response);

			return new Hive5Response (resultCode, resultData);
		}
	}
}

