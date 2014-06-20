using System;
using System.Collections.Generic;
using System.Text;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{

    /// <summary>
    /// Hobject.
    /// </summary>
    public class HObject
    {
        public long id { set; get; }
        public string @class { set; get; }
        public object changes { set; get; }

        /// <summary>
        /// Load the specified json.
        /// </summary>
        /// <param name="json">Json.</param>
        public static HObject Load(JsonData json)
        {
            var id = JsonHelper.ToLong(json, "id", -1);
            var @class = (string)json["class"];
            var changes = JsonHelper.ToObject(json, "changes", null);

            return new HObject()
            {
                id = id,
                @class = @class,
                changes = changes
            };
        }



        /// <summary>
        /// Loads the list.
        /// </summary>
        /// <returns>The list.</returns>
        /// <param name="json">Json.</param>
        public static List<HObject> LoadList(JsonData json)
        {
            var hobjects = new List<HObject>();

            if (json == null || json.IsArray == false)
                return hobjects;

            var listCount = json.Count;
            for (int currentCount = 0; currentCount < listCount; currentCount++)
            {
                hobjects.Add(HObject.Load(json[currentCount]));
            }

            return hobjects;
        }
    }
}
