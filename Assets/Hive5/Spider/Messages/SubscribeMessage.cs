using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class SubscribeMessage : SpiderRequestMessage
    {
        public TopicKind TopicKind { get; set; }

        public Dictionary<string, object> Options { get; set;  }

        public SubscribeMessage()
        {
            this.MessageCode = (int)WampMessageCode.SUBSCRIBE;
        }

        public override string ToJson()
        {
            List<object> messageObjects = new List<object>();
            messageObjects.Add(this.MessageCode);
            messageObjects.Add(this.RequestId);
            messageObjects.Add(this.Options != null ? this.Options : new Dictionary<string, object>());
            messageObjects.Add(TopicUris.GetTopicUri(this.TopicKind));

            string jsonMessage = JsonHelper.ToJson(messageObjects);
            return jsonMessage;
        }
    }
}
