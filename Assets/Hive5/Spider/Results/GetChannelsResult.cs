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
                long appId = JsonHelper.ToLong(jsonData, "app_id", -1);
                int channelNumber = JsonHelper.ToInt(jsonData, "channel_number", -1);
                int sessionCount = JsonHelper.ToInt(jsonData, "session_count", 0);

                var channel = new SpiderChannel()
                {
                    app_id = appId,
                    channel_number = channelNumber,
                    session_count = sessionCount,
                };

                Channels.Add(channel);
            }
        }
    }
}
