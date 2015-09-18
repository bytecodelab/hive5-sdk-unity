using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using LitJson;

namespace Hive5.Spider.Models
{
    public class WelcomeMessage : SpiderMessage
    {
        public long SessionId { get; set; }

        public List<SpiderTopic> Topics { get; set; }


        public WelcomeMessage() : base()
        {
            this.MessageCode = (int)WampMessageCode.WELCOME;
        }

        public static WelcomeMessage Parse(string s)
        {
            if (string.IsNullOrEmpty(s) == true)
                return null;

            var jsonRoot = JsonMapper.ToObject(s);
           
            long sessionId = jsonRoot[1].ToLong();

            var topicsJson = jsonRoot[2]["topics"];

            List<SpiderTopic> topics = new List<SpiderTopic>();
            for (int i = 0; i < topicsJson.Count; i++)
            {
                var topicJson = topicsJson[i];
                topics.Add(new SpiderTopic((string)topicJson));
            }

            var instance = new WelcomeMessage()
            {
                 SessionId = sessionId,
                 Topics = topics,
            };

            return instance;
        }
    }
}
