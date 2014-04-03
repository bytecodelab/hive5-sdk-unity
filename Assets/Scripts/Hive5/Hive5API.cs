using System;
using LitJson;
using Hive5.Core;
using Hive5.Model;

namespace Hive5
{
	/// <summary>
	/// Hive5 API.
	/// </summary>
	public class Hive5API
	{
		public delegate IResponseBody dataLoader (JsonData response);
		public delegate void CallBack (Hive5Response hive5Response);

		/// <summary>
		/// Sets the debug.
		/// </summary>
		public void setDebug()
		{
			var hive5 = Hive5Core.Instance;
			hive5.setDebug();
		}

		/// <summary>
		/// Init the specified appKey and uuid.
		/// </summary>
		/// <param name="appKey">App key.</param>
		/// <param name="uuid">UUID.</param>
		public static void Init(string appKey, string uuid)
		{
			var hive5 = Hive5Core.Instance;
			hive5.Init (appKey, uuid);
		}

	}
}

