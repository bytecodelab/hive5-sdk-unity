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

            long requestId = JsonHelper.ToLong(json[1], -1); 

            Dictionary<string, JsonData> details = JsonHelper.GetDictionary(json[2]);

            List<JsonData> arguments = new List<JsonData>();
            if (json.Count > 3)
            {
                arguments = JsonHelper.GetList(json[3]);
            }

            Dictionary<string, JsonData> argumentsKw = new Dictionary<string,JsonData>();
            if (json.Count > 4)
            {
                argumentsKw = JsonHelper.GetDictionary(json[4]);
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
