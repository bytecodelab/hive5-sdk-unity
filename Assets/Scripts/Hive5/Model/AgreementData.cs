using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Agreement data.
	/// </summary>
	public class AgreementData
	{
		public string version;
		public DateTime agreedAt;

		public AgreementData(string version, DateTime agreedAt)
		{
			this.version 	= version;
			this.agreedAt	= agreedAt;
		}
		
		public static Dictionary<string, AgreementData> Load(JsonData json)
		{
			var agreements = new Dictionary<string, AgreementData>();
			
			if (json == null || json.IsObject == false)
				return agreements;

			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				var version		= (string)json[key]["version"];
				var agreedAt 	= Date.ParseDateTime((string)json[key]["agreed_at"]);

				var agreement 	= new AgreementData(version, agreedAt);

				agreements.Add(key, agreement);
			}

			return agreements;
		}
	}
}
