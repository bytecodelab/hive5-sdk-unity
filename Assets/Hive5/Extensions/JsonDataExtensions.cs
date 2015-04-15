using LitJson;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace LitJson
{
    public static class JsonDataExtensions
    {
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

        public static int ToDateTime(this JsonData jsonData)
        {
            if (jsonData == null ||
                jsonData.IsString != false)
                return 0;

            return ParseDateTime(jsonData.ToString());
        }

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

        public static T[] ToArray<T>(this JsonData jsonData)
        {
            return jsonData.ToList<T>().ToArray<T>();
        }


        public static int ParseDateTime(string dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString) == true)
                return 0;

            int intValue = 0;
            int.TryParse(dateTimeString, out intValue);

            return intValue;
        }

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
    }
}
