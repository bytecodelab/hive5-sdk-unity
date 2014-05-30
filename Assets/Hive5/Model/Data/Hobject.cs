using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{

	/// <summary>
	/// Hobject.
	/// </summary>
	public class Hobject
	{
		public long id { set; get; }
		public string @class { set; get; }
		public object changes { set; get; }

		/// <summary>
		/// Load the specified json.
		/// </summary>
		/// <param name="json">Json.</param>
		public static Hobject Load(JsonData json)
		{
			var id 		= (long)json["id"];
			var @class 	= (string)json["class"];
			var changes = (object)json["changes"];

			return new Hobject () {
				id 		= id,
				@class 	= @class,
				changes = changes
			};
		}

		/// <summary>
		/// Loads the list.
		/// </summary>
		/// <returns>The list.</returns>
		/// <param name="json">Json.</param>
		public static List<Hobject> LoadList(JsonData json)
		{
			var hobjects = new List<Hobject>();
			
			if (json == null || json.IsArray == false)
				return hobjects;

			var listCount = json.Count;
			for (int currentCount = 0; currentCount < listCount; currentCount++) 
			{
				hobjects.Add(Hobject.Load(json[currentCount]));
			}

			return hobjects;
		}
	}
}
