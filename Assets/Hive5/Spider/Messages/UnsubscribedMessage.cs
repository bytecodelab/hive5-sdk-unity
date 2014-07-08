using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class UnsubscribedMessage : SpiderMessage
    {
        public long RequestId { get; set; }

        public UnsubscribedMessage()
        {
            this.MessageCode = (int)WampMessageCode.UNSUBSCRIBED;
        }

        public static UnsubscribedMessage Parse(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return null;

            var parts = LitJson.JsonMapper.ToObject(s);

            if (parts.Count != 2)
                return null;

            if ((parts[1].IsLong == false && parts[1].IsInt == false))
                return null;

            long requestId = JsonHelper.ToLong(parts[1], -1);

            var instance = new UnsubscribedMessage()
            {
                RequestId = requestId,
            };

            return instance;
        }

    }
}
