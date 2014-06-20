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

            try
            {
                JsonData jsonChild = json[propertyName];

                long value = 0;

                if (jsonChild.IsInt == true)
                {
                    value = (int)jsonChild;
                }
                else if (jsonChild.IsLong == true)
                {
                    value = (long)jsonChild;
                }
                else if (jsonChild.IsString == true)
                {
                    if (long.TryParse((string)jsonChild, out value) == false)
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
    }
}
