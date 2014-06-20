using System;
using System.Collections.Generic;
using LitJson;
using Hive5;
using Hive5.Util;

namespace Hive5.Model
{

    /// <summary>
    /// Add Friends
    /// </summary>
    public class GetObjectsResponseBody : IResponseBody
    {
        public List<HObject> Objects { get; set; }

        /// <summary>
        /// Load the specified json.
        /// </summary>
        /// <param name="json">Json.</param>
        public static IResponseBody Load(JsonData json)
        {
            if (json == null)
                return null;

            List<HObject> objects = new List<HObject>();

            try
            {
                objects = HObject.LoadList(json["objects"]);
            }
            catch
            {

            }

            return new GetObjectsResponseBody()
            {
                Objects = objects
            };
        }

    }
}

