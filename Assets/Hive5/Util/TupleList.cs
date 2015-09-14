using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Util
{
    /// <summary>
	/// 키, 밸류 리스트
	/// </summary>
	public class TupleList <TKey, TValue>  
	{
		public List<KeyValuePair<TKey, TValue>> data = new List<KeyValuePair<TKey, TValue>>();

		/// <summary>
		/// 키, 밸류 한쌍을 리스트에 추가한다.
		/// </summary>
		/// <param name="key">키</param>
		/// <param name="value">밸류</param>
		public void Add(TKey key, TValue value)
		{
			if(value != null)
			{
				data.Add (new KeyValuePair<TKey, TValue> (key, value));
			}
		}
	}
}
