using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class ResultMessage : SpiderMessage
    {
        public long RequestId { get; set; }

        public Dictionary<string, JsonData> Details { get; set; }

        public List<JsonData> Arguments { get; set; }

        public Dictionary<string, JsonData> ArgumentsKw { get; set; }


        public ResultMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.RESULT;
        }

        public static ResultMessage Parse(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return null;

			JsonData json = JsonMapper.ToObject<LitJson.JsonData>(s);
            if (json.Count < 3 ||
                json.Count > 5)
                return null;

            if (json[0].IsInt == false)
                return null;

            
            if (json[1].IsLong == false &&
                json[1].IsInt == false)
                return null;

            long requestId = json[1].ToLong(); 

            Dictionary<string, JsonData> details = json[2] != null ? json[2].ToDictionary() : new Dictionary<string, JsonData>();

            List<JsonData> arguments = new List<JsonData>();
            if (json.Count > 3 && json[3] != null && json[3].IsArray == true)
            {
                arguments = json[3].ToList<JsonData>();
            }

            Dictionary<string, JsonData> argumentsKw = new Dictionary<string,JsonData>();
            if (json.Count > 4 && json[4] != null)
            {
                argumentsKw = json[4].ToDictionary();
            }

            var instance = new ResultMessage()
            {
                RequestId = requestId,
                Details = details,
                Arguments = arguments,
                ArgumentsKw = argumentsKw,
            };

            return instance;



        }
    }
}
