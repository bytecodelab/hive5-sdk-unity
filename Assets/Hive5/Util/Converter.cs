using System;
using System.Collections.Generic;
using System.Text;
using System.Globalization;
using Hive5.Util;

namespace Hive5.Util
{
	/// <summary>
	/// 변환기 클래스
	/// </summary>
	internal class Converter
	{
		/// <summary>
		/// DataOrder 열거형을 문자열로 변환합니다.
		/// </summary>
		/// <returns>DataOrder 열거형을 변환한 문자열</returns>
		/// <param name="order">DataOrder 열거형</param>
		internal static string OrderToString(DataOrder order)
		{
			string orderString = "";
			
			switch(order)
			{
			case DataOrder.ASC:
				orderString = "asc";
				break;
			case DataOrder.DESC:
				orderString = "dec";
				break;
			}
			
			return orderString;
		}

		/// <summary>
		/// 문자열을 DataOrder 열거형으로 변환하여 반환합니다.
		/// </summary>
		/// <returns>변환된 DataOrder열거형</returns>
		/// <param name="order">데이터 정렬을 의미하는 문자열 ["dec", "asc" 등]</param>
		internal static DataOrder StringToOrder(string order)
		{
			DataOrder returnOrder = DataOrder.DESC;
			
			switch(order.ToLower())
			{
				case "dec":
					returnOrder = DataOrder.DESC;
					break;
				case "asc":
					returnOrder = DataOrder.ASC;
					break;
			}
			
			return returnOrder;
		}
    }
}
