using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public abstract class SpiderMessage
    {
        public int MessageCode { get; protected set; }

        public virtual string ToJson()
        {
            throw new NotImplementedException("해당 클래스에서 ToJson의 overrider가 필요합니다.");
        }

        public static List<JsonData> GetList(JsonData json)
        {
            var defaultList = new List<JsonData>();
            if (json == null ||
                json.IsArray == false)
                return defaultList;

            foreach (JsonData item in json)
            {
               defaultList.Add(item);
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
    }
}
