using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class JsonHelper
    {
        public static long ToLong(JsonData json, string propertyName, long defaultValue)
        {
            if (json == null)
                throw new NullReferenceException();

            JsonData jsonChild = json[propertyName];

            return ToLong(jsonChild, defaultValue);
        }

        public static long ToLong(JsonData json, long defaultValue)
        {
            if (json == null)
                throw new NullReferenceException();

            try
            {


                long value = 0;

                if (json.IsInt == true)
                {
                    value = (int)json;
                }
                else if (json.IsLong == true)
                {
                    value = (long)json;
                }
                else if (json.IsString == true)
                {
                    if (long.TryParse((string)json, out value) == false)
                        return defaultValue;
                }
                else
                {
                    return defaultValue;
                }

                return value;
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }

        }

        public static int ToInt(JsonData json, string propertyName, int defaultValue)
        {
            if (json == null)
                throw new NullReferenceException();

            JsonData jsonChild = json[propertyName];

            return ToInt(jsonChild, defaultValue);
        }

        public static int ToInt(JsonData json, int defaultValue)
        {
            if (json == null)
                throw new NullReferenceException();

            try
            {


                int value = 0;

                if (json.IsInt == true)
                {
                    value = (int)json;
                }
                else if (json.IsLong == true)
                {
                    value = (int)json;
                }
                else if (json.IsString == true)
                {
                    if (int.TryParse((string)json, out value) == false)
                        return defaultValue;
                }
                else
                {
                    return defaultValue;
                }

                return value;
            }
            catch (KeyNotFoundException)
            {
                return defaultValue;
            }

        }

        public static List<JsonData> GetList(JsonData json)
        {
            var defaultList = new List<JsonData>();
            if (json == null ||
                json.IsArray == false ||
			    json.Count == 0)
                return defaultList;

            //foreach (JsonData item in json)
			for (int i = 0; i < json.Count; i++) {
				defaultList.Add(json[i]);
			}

            return defaultList;
        }

        public static Dictionary<string, JsonData> GetDictionary(JsonData json)
        {
            var defaultDict = new Dictionary<string, JsonData>(); 

            if (json == null)
                return defaultDict;

            var dict = (json as System.Collections.IDictionary);
            if (dict == null)
                return defaultDict;

            foreach (string key in dict.Keys)
            {
                defaultDict.Add(key, json[key]);
            }

            return defaultDict;
        }

         public static Dictionary<string, string> GetStringDictionary(JsonData json)
        {
            var defaultDict = new Dictionary<string, string>(); 

            if (json == null)
                return defaultDict;

            var dict = (json as System.Collections.IDictionary);
            if (dict == null)
                return defaultDict;

            foreach (string key in dict.Keys)
            {
                defaultDict.Add(key, (string)json[key]);
            }

            return defaultDict;
        }

        public static object ToObject(JsonData json, string propertyName, object defaultValue)
        {
            if (json == null)
                throw new NullReferenceException();

            try
            {
                JsonData jsonChild = json[propertyName];
                object obj = (object)jsonChild;
                return obj;
            }
            catch
            {
                return defaultValue;
            }
        }

        public static string ToJson(List<object> messageObjects)
        {
            if (messageObjects == null)
                return string.Empty;

            string json = LitJson.JsonMapper.ToJson(messageObjects);
            return json;
        }
    }
}
