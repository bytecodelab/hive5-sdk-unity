using Hive5.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class SpiderTopic
    {
        public const string UserTopicPrefix = "io.hive5.spider.topic.user";
        public const string ZoneTopicPrefix = "io.hive5.spider.topic.zone";

        public string TopicUri { get; set; }

        public SpiderTopic(string topicUri)
        {
            this.TopicUri = topicUri;
        }

        public static SpiderTopic CreateUserTopic(User user)
        {
            var topicUri = string.Format("{0}.{1}.{2}", UserTopicPrefix, user.platform, user.id);
            return new SpiderTopic(topicUri);
        }
    }
}
