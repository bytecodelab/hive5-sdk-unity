using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider
{
    /// <summary>
    /// 구독용 Topic 종류
    /// </summary>
    public enum TopicKind
    {
        Channel,
        Private,
        Notice,
        System, // Hive5 System
    };

    public class TopicUris
    {
        private static readonly string TopicPrefix = "io.hive5.spider.topic";

        private static readonly string TopicNameChannel = "channel";
        private static readonly string TopicNamePrivate = "private";
        private static readonly string TopicNameNotice = "notice";
        private static readonly string TopicNameSystem = "system";

        public static string GetTopicUri(TopicKind kind)
        {
            string topicName = "";
            switch (kind)
            {
                case TopicKind.Channel:
                    topicName = TopicNameChannel;
                    break;

                case TopicKind.Notice:
                    topicName = TopicNameNotice;
                    break;

                case TopicKind.Private:
                    topicName = TopicNamePrivate;
                    break;

                case TopicKind.System:
                    topicName = TopicNameSystem;
                    break;
            }

            return string.Concat(TopicUris.TopicPrefix, ".", topicName);
        }

    }
}
