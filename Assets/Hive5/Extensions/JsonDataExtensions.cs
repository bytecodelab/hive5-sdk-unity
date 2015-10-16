using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LitJson
{
    /// <summary>
    /// JsonData 클래스의 확장메서드를 포함하는 클래스
    /// </summary>
    public static class JsonDataExtensions
    {
        /// <summary>
        /// JsonData를 안전하게 long타입으로 변환
        /// </summary>
        /// <param name="jsonData">데이터 원본</param>
        /// <returns>long 타입 값</returns>
        public static long ToLong(this JsonData jsonData)
        {
            if (jsonData.IsLong)
                return (long)jsonData;

            if (jsonData.IsInt)
                return (int)jsonData;

            if (jsonData.IsString)
            {
                long longValue = 0;
                if (long.TryParse((string)jsonData, out longValue) == true)
                {
                    return longValue;
                }
            }
            
            throw new InvalidCastException();
        }

        /// <summary>
        /// JsonData를 안전하게 int타입으로 변환
        /// </summary>
        /// <param name="jsonData">데이터 원본</param>
        /// <returns>int 타입 값</returns>
        public static int ToInt(this JsonData jsonData)
        {
            if (jsonData.IsInt)
                return (int)jsonData;

            if (jsonData.IsString)
            {
                int intValue = 0;
                if (int.TryParse((string)jsonData, out intValue) == true)
                {
                    return intValue;
                }
            }
            throw new InvalidCastException();
        }

        /// <summary>
        /// JsonData를 안전하게 List&lt;T&gt;타입으로 변환
        /// </summary>
        /// <typeparam name="T">타입</typeparam>
        /// <param name="jsonData">데이터 원본</param>
        /// <returns>List&lt;T&gt;타입 값</returns>
        public static List<T> ToList<T>(this JsonData jsonData)
        {
            List<T> list = new List<T>();
            if (jsonData == null)
                return list;

            if (jsonData.IsArray == false)
                throw new InvalidCastException(jsonData.ToJson() + " is not array");

            List<object> tempList = new List<object>();
            for (int i = 0; i < jsonData.Count; i++)
            {
                var item = jsonData[i];
                if (typeof(T) == typeof(String) && item.IsString == true)
                    tempList.Add(item.ToString());
                else if (typeof(T) == typeof(int) && item.IsInt == true)
                    tempList.Add(item.ToInt());
                else if (typeof(T) == typeof(long) && (item.IsInt || item.IsLong))
                    tempList.Add(item.ToLong());
                else if (typeof(T) == typeof(bool) && item.IsBoolean)
                    tempList.Add((bool)item);
                else
                    throw new InvalidCastException(item.ToJson() + "is not " + typeof(T));
            }

            return tempList.OfType<T>().ToList();
        }

        /// <summary>
        /// JsonData를 안전하게 T[] 타입으로 변환
        /// </summary>
        /// <typeparam name="T">타입</typeparam>
        /// <param name="jsonData">데이터원본</param>
        /// <returns>T[] 타입 값</returns>
        public static T[] ToArray<T>(this JsonData jsonData)
        {
            return jsonData.ToList<T>().ToArray<T>();
        }

        /// <summary>
        /// 키(key)의 존재 여부를 반환
        /// </summary>
        /// <param name="data">확장메서드 숙주</param>
        /// <param name="key">키</param>
        /// <returns>존재 여부</returns>
        public static bool ContainsKey(this JsonData data,string key)
        {
            bool result = false;
            if(data == null)
                return result;
            if(!data.IsObject)
            {
                return result;
            }
            IDictionary tdictionary = data as IDictionary;
            if(tdictionary == null)
                return result;
            if(tdictionary.Contains(key))
            {
                result = true;
            }
            return result;
        }

        public static Dictionary<string, JsonData> ToDictionary(this JsonData data)
        {
            var defaultDict = new Dictionary<string, JsonData>(); 

            if (data == null)
                return defaultDict;

            var dict = (data as System.Collections.IDictionary);
            if (dict == null)
                return defaultDict;

            foreach (string key in dict.Keys)
            {
                defaultDict.Add(key, data[key]);
            }

            return defaultDict;
        }

        public static Dictionary<string, string> ToStringDictionary(this JsonData data)
        {
            var defaultDict = new Dictionary<string, string>(); 

            if (data == null)
                return defaultDict;

            var dict = (data as System.Collections.IDictionary);
            if (dict == null)
                return defaultDict;

            foreach (string key in dict.Keys)
            {
                defaultDict.Add(key, (string)data[key]);
            }

            return defaultDict;
        }
    }
}