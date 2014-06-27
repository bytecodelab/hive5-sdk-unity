using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class ResultMessage : SpiderMessage
    {
        public long RequestId { get; set; }

        public Dictionary<string, object> Details { get; set; }

        public List<object> Arguments { get; set; }

        public Dictionary<string, object> ArgumentsKw { get; set; }


        public ResultMessage()
        {
            this.MessageCode = (int)WampMessageCode.RESULT;
        }

        public static ResultMessage Parse(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return null;

            JsonData json = JsonMapper.ToObject(s);
            if (json.Count < 3 ||
                json.Count > 5)
                return null;

            if (json[0].IsInt == false)
                return null;

            if (json[1].IsLong == false)
                return null;

            long requestId = (long)json[1];
            var details = json[2];

            List<object> arguments = null;
            //if (parts.Count > 3)
            //{
            //    arguments = parts[3] as List<object>;
            //}

            Dictionary<string, object> argumentsKw = null;
            //if (parts.Count > 4)
            //{
            //    argumentsKw = parts[4] as Dictionary<string, object>;
            //}


            var instance = new ResultMessage()
            {
                RequestId = requestId,
                //Details = details,
                Arguments = arguments,
                ArgumentsKw = argumentsKw,
            };

            return instance;



        }
    }
}
