using Hive5.Spider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Hive5.Spider
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
                long appId = jsonData.ContainsKey("app_id") ? jsonData.ToLong() : -1;
                int channelNumber = jsonData.ContainsKey("channel_number") ? jsonData.ToInt() : -1;
                int sessionCount = jsonData.ContainsKey("session_count") ? jsonData.ToInt() : 0;

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
