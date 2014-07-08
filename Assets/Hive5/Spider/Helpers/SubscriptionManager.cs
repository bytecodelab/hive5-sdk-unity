using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class SubscriptionManager
    {
        private static Dictionary<long, TopicKind> requestIdToTopicKind { get; set; }
        private static Dictionary<long, TopicKind> subscriptionIdToTopicKind { get; set; }

        static SubscriptionManager()
        {
            requestIdToTopicKind = new Dictionary<long, TopicKind>();
            subscriptionIdToTopicKind = new Dictionary<long, TopicKind>();
        }

        public static void ReportSubscribe(long requestId, TopicKind topicKind)
        {
            if (requestIdToTopicKind.ContainsKey(requestId) == true)
            {
                requestIdToTopicKind[requestId] = topicKind;
            }
            else
            {
                requestIdToTopicKind.Add(requestId, topicKind);
            }
        }

        public static void ReportSubscribed(long requestId, long subscriptionId)
        {
            TopicKind topicKind = GetTopicKindByRequestId(requestId);

            // RequestId 사전에서 삭제
            requestIdToTopicKind.Remove(requestId);

            if (subscriptionIdToTopicKind.ContainsKey(subscriptionId) == true)
            {
                subscriptionIdToTopicKind[subscriptionId] = topicKind;
            }
            else
            {
                subscriptionIdToTopicKind.Add(subscriptionId, topicKind);
            }
        }

        private static TopicKind GetTopicKindByRequestId(long requestId)
        {
            // 기본값 Channel
            TopicKind topicKind = TopicKind.Channel;

            requestIdToTopicKind.TryGetValue(requestId, out topicKind);

            return topicKind;
        }

        public static TopicKind GetTopicKindBySubscriptionId(long subscriptionId)
        {
            // 기본값 Channel
            TopicKind topicKind = TopicKind.Channel;

            subscriptionIdToTopicKind.TryGetValue(subscriptionId, out topicKind);

            return topicKind;
        }

        public static void Clear()
        {
            requestIdToTopicKind.Clear();
            subscriptionIdToTopicKind.Clear();
        }
    }
}
