using LitJson;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class EventMessage : SpiderMessage
    {
        public long SubscriptionId { get; set; }

        public long PublicationId { get; set; }

        public Dictionary<string, JsonData> Details { get; set; }

        public List<JsonData> Arguments { get; set; }

        public Dictionary<string, string> ArgumentsKw { get; set; }

        public EventMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.EVENT;
        }

        public static EventMessage Parse(string json)
        {
            if (string.IsNullOrEmpty(json) == true)
                return null;

			var parts = LitJson.JsonMapper.ToObject<LitJson.JsonData>(json);

            if (parts.Count < 4 ||
                parts.Count > 6)
                return null;

            long subscriptionId = JsonHelper.ToLong(parts[1], -1);
            long publicationId = JsonHelper.ToLong(parts[2], -1);
            Dictionary<string, JsonData> details = JsonHelper.GetDictionary(parts[3]);
			List<JsonData> arguments = null; //JsonHelper.GetList(parts[4]);
            Dictionary<string, string> argumentsKw  = JsonHelper.GetStringDictionary(parts[5]);
            

            return new EventMessage()
            {
                SubscriptionId = subscriptionId,
                PublicationId = publicationId,
                Details = details,
                Arguments= arguments,
                ArgumentsKw = argumentsKw,
            };
        }

        public TopicKind GetTopicKind()
        {
            // TODO: 종류를 판별할 방법이 없음

            return TopicKind.Channel;
        }
    }
}
