using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class ErrorMessage : SpiderMessage
    {
        public Dictionary<string, JsonData> Details { get; set; }

        public string ErrorUri { get; set; }

        public long RequestId { get; set; }

        public int MessageCodeOfError { get; set; }

        public ErrorMessage()
        {
            this.MessageCode = (int)WampMessageCode.ERROR;
        }

        public static ErrorMessage Parse(string json)
        {
            if (string.IsNullOrEmpty(json) == true)
                return null;

			var parts = LitJson.JsonMapper.ToObject<LitJson.JsonData>(json);

            if (parts.Count != 5)
                return null;

            if (parts[1].IsInt == false ||
                parts[2].IsLong == false ||
                parts[4].IsString == false)
                return null;

            int messageCodeOfError = JsonHelper.ToInt(parts[1], -1);
            long requestId = JsonHelper.ToLong(parts[2], -1);
            string errorUri = (string)parts[4];

            return new ErrorMessage()
            {
                MessageCodeOfError = messageCodeOfError,
                RequestId = requestId,
                ErrorUri = errorUri,
                Details = JsonHelper.GetDictionary(parts[3]),
            };
        }
    }
}
