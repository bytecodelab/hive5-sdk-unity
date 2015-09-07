using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// 약관 모델 클래스
	/// </summary>
	public class Agreement
	{
        /// <summary>
        /// 키
        /// </summary>
		public string Key { set; get; }
        /// <summary>
        /// 버전
        /// </summary>
		public string Version { set; get; }
        /// <summary>
        /// 동의한 시점
        /// </summary>
		public DateTime AgreedAt { set; get; }

		/// <summary>
		/// Agreenment 배열에 대한 json 데이터를 읽어 Agreement 리스트를 반환합니다.
		/// </summary>
		/// <param name="jsonData">Agreenment 배열 json 데이터</param>
		/// <returns>Agreement 리스트</returns>
		public static List<Agreement> LoadList(JsonData jsonData)
		{
			var agreements = new List<Agreement>();
			
			if (jsonData == null || jsonData.IsObject == false)
				return agreements;

			foreach (string key in (jsonData as System.Collections.IDictionary).Keys)
			{
				agreements.Add ( new Agreement() {
					Key = key,
					Version = (string)jsonData[key]["version"],
					AgreedAt = Date.ParseDateTime((string)jsonData[key]["agreed_at"])
				});
			}

			return agreements;
		}
	}
}
