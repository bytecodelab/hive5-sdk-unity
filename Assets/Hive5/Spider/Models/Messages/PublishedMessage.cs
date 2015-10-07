using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Hive5.Spider.Models
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

            if ((parts[1].IsInt == false && parts[1].IsLong == false) ||
                (parts[2].IsInt == false && parts[2].IsLong == false))
                return null;

            long requestId = parts[1].ToLong();
            long publicationId = parts[2].ToLong();

            var instance = new PublishedMessage()
            {
                RequestId = requestId,
                PublicationId = publicationId,
            };

            return instance;
        }

    }
}
