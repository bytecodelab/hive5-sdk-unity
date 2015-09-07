using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{
	/// <summary>
	/// 친구 모델 클래스
	/// </summary>
	public class Friend : User
	{
        /// <summary>
        /// Friend를 나타내는 Json 데이터를 읽어 Friend를 생성하여 반환함
        /// </summary>
        /// <param name="jsonData">Friend를 나타내는 Json 데이터</param>
        /// <returns>Friend 클래스 인스턴스</returns>
		public static Friend Load(JsonData jsonData)
		{
			return new Friend () {
				platform = (string)jsonData["platform"],
				id = (string)jsonData["id"],
			};
		}

		/// <summary>
		/// Friend 배열을 나타내는 Json 데이터를 읽어 Friend 리스트를 생성하여 반환함
		/// </summary>
		/// <param name="jsonData">Friend 배열을 나타내는 Json 데이터</param>
        /// <returns>Friend 클래스 인스턴스 리스트</returns>
		public static List<Friend> LoadList(JsonData jsonData)
		{
			var friends = new List<Friend>();
			
			if (jsonData == null || jsonData.IsArray == false)
				return friends;

			for (var i = 0; i < jsonData.Count; i++) 
			{
				friends.Add(Friend.Load(jsonData[i]));
			}

			return friends;
		}
	}
}
