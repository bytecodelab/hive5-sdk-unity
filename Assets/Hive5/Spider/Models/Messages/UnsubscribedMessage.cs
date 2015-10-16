using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Hive5.Spider.Models
{
    public class UnsubscribedMessage : SpiderMessage
    {
        public long RequestId { get; set; }

        public UnsubscribedMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.UNSUBSCRIBED;
        }

        public static UnsubscribedMessage Parse(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return null;

			var parts = LitJson.JsonMapper.ToObject<LitJson.JsonData>(s);

            if (parts.Count != 2)
                return null;

            if ((parts[1].IsLong == false && parts[1].IsInt == false))
                return null;

            long requestId = parts[1].ToLong();

            var instance = new UnsubscribedMessage()
            {
                RequestId = requestId,
            };

            return instance;
        }

    }
}
