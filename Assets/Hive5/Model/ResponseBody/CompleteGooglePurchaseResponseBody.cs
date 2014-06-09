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
	public class CompleteGooglePurchaseResponseBody : IResponseBody
	{
		public List<Item> Items { set; get; }		

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static IResponseBody Load(JsonData json)
		{
			if (json == null)
				return null;

			var items = new List<Item> ();
			try
			{
				items = Item.LoadList( json["items"] );
			}
			catch (KeyNotFoundException)
			{
				items = null;
			}

			return new CompleteGooglePurchaseResponseBody() {
				Items = items
			};
		}

	}
}

