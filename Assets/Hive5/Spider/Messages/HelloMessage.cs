using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class HelloMessage : SpiderMessage
    {
        public SpiderRealm Realm { get; set; }

        public HelloDetail Detail { get; set; }

        public HelloMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.HELLO;
        }

        public override string ToMessageString()
        {
            List<object> messageObjects = new List<object>();
            messageObjects.Add(this.MessageCode);
            messageObjects.Add(this.Realm.ToString());
            messageObjects.Add(Detail);

            string jsonMessage = JsonHelper.ToJson(messageObjects);
            return jsonMessage;
        }
    }
}
