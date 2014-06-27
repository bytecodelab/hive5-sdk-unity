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
        {
            this.MessageCode = (int)WampMessageCode.PUBLISHED;
        }

        public static PublishedMessage Parse(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return null;

            var parts = LitJson.JsonMapper.ToObject<List<object>>(s);

            if (parts.Count != 3)
                return null;

            if (parts[1] is long == false ||
                parts[2] is long == false)
                return null;

            long requestId = (long)parts[1];
            long publicationId = (long)parts[1];

            var instance = new PublishedMessage()
            {
                RequestId = requestId,
                PublicationId = publicationId,
            };

            return instance;
        }

    }
}
