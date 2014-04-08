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
	public class GetAgreementsResponseBody : IResponseBody
	{
		public List<Agreement> agreements { set; get; }			

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			List<Agreement> agreements;

			try
			{
				agreements = Agreement.LoadList( json["agreements"] );
			}
			catch (KeyNotFoundException)
			{
				agreements = null;
			}

			return new GetAgreementsResponseBody() {
				agreements		= agreements
			};
		}

	}
}

