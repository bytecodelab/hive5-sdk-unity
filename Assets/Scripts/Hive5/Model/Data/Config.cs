using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Config data.
	/// </summary>
	public class Config
	{
		public string key { set; get; }
		public string value  { set; get; }

		/// <summary>
		/// Loads the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <param name="json">Json.</param>
		public static List<Config> LoadList(JsonData json)
		{
			var configs = new List<Config>();
			
			if (json == null)
				return configs;
			
			if (json.IsObject == false)
				throw new InvalidJsonException("invalid configData");
			
			foreach (string key in (json as System.Collections.IDictionary).Keys)
			{
				configs.Add( new Config() {
					key = key,
					value = (string)json[key]
				});
			}
			
			return configs;
		}
	}
}
