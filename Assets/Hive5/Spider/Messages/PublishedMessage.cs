using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class PublishedMessage : SpiderMessage
    {
        public long RequestId { get; set; }
        public long PublicationId { get; set; }

        public PublishedMessage()
            : base()
        {
            this.MessageCode = (int)WampMessageCode.PUBLISHED;
        }

        public static PublishedMessage Parse(string json)
        {
            if (string.IsNullOrEmpty(json) == true)
                return null;

			var parts = LitJson.JsonMapper.ToObject<LitJson.JsonData>(json);

            if (parts.Count != 3)
                return null;

            if (parts[1].IsLong == false ||
                parts[2].IsLong == false)
                return null;

            long requestId = JsonHelper.ToLong(parts[1], -1);
            long publicationId = JsonHelper.ToLong(parts[2], -1);

            var instance = new PublishedMessage()
            {
                RequestId = requestId,
                PublicationId = publicationId,
            };

            return instance;
        }

    }
}
