using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Hive5
{
    public class SubscribedMessage : SpiderMessage
    {
        public long RequestId { get; set; }
        public long SubscriptionId { get; set; }

        public SubscribedMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.SUBSCRIBED;
        }

        public static SubscribedMessage Parse(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return null;

			var jsonData = LitJson.JsonMapper.ToObject(s);

            if (jsonData.Count != 3)
                return null;

            if ((jsonData[1].IsLong == false && jsonData[1].IsInt == false) ||
                jsonData[2].IsLong == false)
                return null;

            long requestId = jsonData[1].ToLong();
            long subscriptionId = jsonData[2].ToLong();

            var instance = new SubscribedMessage()
            {
                RequestId = requestId,
                SubscriptionId = subscriptionId,
            };

            return instance;
        }

    }
}
