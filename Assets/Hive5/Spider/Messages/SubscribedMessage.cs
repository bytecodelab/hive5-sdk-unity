using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class SubscribedMessage : SpiderMessage
    {
        public long RequestId { get; set; }
        public long SubscriptionId { get; set; }

        public SubscribedMessage()
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

            long requestId = JsonHelper.ToLong(jsonData[1], -1);
            long subscriptionId = JsonHelper.ToLong(jsonData[2], -1);

            var instance = new SubscribedMessage()
            {
                RequestId = requestId,
                SubscriptionId = subscriptionId,
            };

            return instance;
        }

    }
}
