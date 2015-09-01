using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class SpiderTopic
    {
        public string TopicUri { get; set; }

        public SpiderTopic(string topicUri)
        {
            this.TopicUri = topicUri;
        }
    }
}
