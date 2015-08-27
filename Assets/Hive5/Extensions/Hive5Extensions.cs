using System;
using System.Collections.Generic;
using System.Collections;

namespace Hive5
{
	public static class Hive5Extensions
	{
		public static T GetKey<T> (this List<T> list, string key)
 		{
			return list [0];
		}
	}

	public interface Hive5Data
	{
		string key { set; get; }
	}
}
