using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class GetChannelsResult : CallResult
    {
        public List<SpiderChannel> Channels { get; set; }

        public GetChannelsResult(ResultMessage resultMessage)
        {
            Load(resultMessage);
        }

        protected override void Load(ResultMessage message)
        {
            Channels = new List<SpiderChannel>();
            foreach (var jsonData in message.Arguments)
            {
                long id = JsonHelper.ToLong(jsonData, "id", -1);
                int sessionCount = JsonHelper.ToInt(jsonData, "session_count", 0);

                var channel = new SpiderChannel()
                {
                    id = id,
                    session_count = sessionCount,
                };

                Channels.Add(channel);
            }
        }
    }
}
