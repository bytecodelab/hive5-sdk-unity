using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Models
{
    public class SubscribeMessage : SpiderRequestMessage
    {
        public SpiderTopic Topic { get; set; }

        public Dictionary<string, object> Options { get; set;  }

        public SubscribeMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.SUBSCRIBE;
        }

        public override string ToMessageString()
        {
            List<object> messageObjects = new List<object>();
            messageObjects.Add(this.MessageCode);
            messageObjects.Add(this.RequestId);
            messageObjects.Add(this.Options != null ? this.Options : new Dictionary<string, object>());
            messageObjects.Add(this.Topic.TopicUri);

            string jsonMessage = JsonHelper.ToJson(messageObjects);
            return jsonMessage;
        }
    }
}
