using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Models
{
    /// <summary>
    /// Result of GetDataTable
    /// </summary>
    public class GetDataTableResponseBody : CommonResponseBody
    {
        public bool UpToDate { get; set; }
        public string DataJson { get; set; }
        public int Revision { get; set; }

        /// <summary>
        /// Load the specified json.
        /// </summary>
        /// <param name="json">Json.</param>
        public static IResponseBody Load(JsonData json)
        {
            if (json == null)
                return null;

            bool upToDate = true;
            string dataJson = string.Empty;
            int revision = 0;

            try
            {
                upToDate = (bool)json["up_to_date"];
                if (json.ContainsKey("data"))
                {
                    dataJson = json["data"].ToJson();
                }
                if (json.ContainsKey("revision"))
                {
                    revision = json["revision"].ToInt();
                }
            }
            catch (KeyNotFoundException)
            {
            }

            return new GetDataTableResponseBody()
            {
                UpToDate = upToDate,
                DataJson = dataJson,
                Revision = revision,
            };
        }
    }
}
