using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// Reset info.
	/// </summary>
	public class ResetInfo
	{
		public string period { set; get; }
		public string weekday { set; get; }
		public int hour { set; get; }
		public int minute { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static ResetInfo Load(JsonData json)
		{
			return new ResetInfo () {
				period 	= (string)json["period"],
				weekday	= (string)json["keekday"],
				hour	= (int)json["hour"],
				minute	= (int)json["minute"]
			};
		
		}
	}
}