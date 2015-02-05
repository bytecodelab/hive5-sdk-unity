using LitJson;
using System;
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

        public static List<T> ToArray<T>(this JsonData jsonData)
        {
            List<T> list = new List<T>();
            return jsonData.Cast<T>().ToList<T>();
        }


        public static int ParseDateTime(string dateTimeString)
        {
            if (string.IsNullOrEmpty(dateTimeString) == true)
                return 0;

            int intValue = 0;
            int.TryParse(dateTimeString, out intValue);

            return intValue;
        }

    }
}
