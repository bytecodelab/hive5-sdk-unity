using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class SubscriptionManager
    {
        private static Dictionary<long, SpiderTopic> requestIdToTopic { get; set; }
        private static Dictionary<long, SpiderTopic> subscriptionIdToTopic { get; set; }

        static SubscriptionManager()
        {
            requestIdToTopic = new Dictionary<long, SpiderTopic>();
            subscriptionIdToTopic = new Dictionary<long, SpiderTopic>();
        }

        public static void ReportSubscribe(long requestId, SpiderTopic topic)
        {
            if (requestIdToTopic.ContainsKey(requestId) == true)
            {
                requestIdToTopic[requestId] = topic;
            }
            else
            {
                requestIdToTopic.Add(requestId, topic);
            }
        }

        public static void ReportSubscribed(long requestId, long subscriptionId)
        {
            SpiderTopic topic = GetTopicByRequestId(requestId);

            // RequestId 사전에서 삭제
            requestIdToTopic.Remove(requestId);

            if (subscriptionIdToTopic.ContainsKey(subscriptionId) == true)
            {
                subscriptionIdToTopic[subscriptionId] = topic;
            }
            else
            {
                subscriptionIdToTopic.Add(subscriptionId, topic);
            }
        }

        private static SpiderTopic GetTopicByRequestId(long requestId)
        {
            SpiderTopic topic = null;
            requestIdToTopic.TryGetValue(requestId, out topic);

            return topic;
        }

        public static SpiderTopic GetTopicBySubscriptionId(long subscriptionId)
        {
            SpiderTopic topic = null;

            subscriptionIdToTopic.TryGetValue(subscriptionId, out topic);

            return topic;
        }

        public static void Clear()
        {
            requestIdToTopic.Clear();
            subscriptionIdToTopic.Clear();
        }
    }
}
