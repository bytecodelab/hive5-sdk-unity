using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class PublishMessage : SpiderRequestMessage
    {
        public PublishOptions Options { get; set; }

        public List<object> Arguments { get; private set; }

        public Dictionary<string, string> Contents { get; set; }


        public string TopicUri { get; set; }

        public PublishMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.PUBLISH;
            this.Arguments = new List<object>();
        }

        public override string ToMessageString()
        {
            List<object> messageObjects = new List<object>();
            messageObjects.Add(this.MessageCode);
            messageObjects.Add(this.RequestId);
            messageObjects.Add(this.Options);
            messageObjects.Add(this.TopicUri);
            messageObjects.Add(this.Arguments);
            messageObjects.Add(this.Contents);

            string jsonString = JsonHelper.ToJson(messageObjects);
            return jsonString;
        }
    }
}
