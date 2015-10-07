using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class ErrorMessage : SpiderMessage
    {
        public Dictionary<string, JsonData> Details { get; set; }

        public string ErrorUri { get; set; }

        public long RequestId { get; set; }

        public int MessageCodeOfError { get; set; }

        public ErrorMessage() : base()
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

            int messageCodeOfError = parts[1].ToInt();
            long requestId = parts[2].ToLong();
            string errorUri = (string)parts[4];

            return new ErrorMessage()
            {
                MessageCodeOfError = messageCodeOfError,
                RequestId = requestId,
                ErrorUri = errorUri,
                Details = parts[3] != null? parts[3].ToDictionary() : new Dictionary<string, JsonData>(),
            };
        }
    }
}
