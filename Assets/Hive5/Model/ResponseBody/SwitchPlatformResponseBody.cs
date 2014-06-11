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
	public class SwitchPlatformResponseBody : IResponseBody
	{
		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static SwitchPlatformResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			return new SwitchPlatformResponseBody ()
			{
			};
		}
		
	}
}
