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
	public class Agreement
	{
		public string 	key { set; get; }
		public string 	version { set; get; }
		public DateTime agreedAt { set; get; }

		/// <summary>
		/// Loads the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <param name="json">Json.</param>
		public static List<Agreement> LoadList(JsonData json)
		{
			var agreements = new List<Agreement>();
			
			if (json == null || json.IsObject == false)
				return agreements;

			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				agreements.Add ( new Agreement() {
					key = key,
					version = (string)json[key]["version"],
					agreedAt = Date.ParseDateTime((string)json[key]["agreed_at"])
				});
			}

			return agreements;
		}
	}
}
