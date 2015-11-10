using Hive5.Spider.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5.Spider.Helpers
{
    public class SubscriptionManager
    {
        private Dictionary<long, SpiderTopic> _SubscribeRequestIdToTopic { get; set; }
        private Dictionary<long, SpiderTopic> _SubscriptionIdToTopic { get; set; }
        private Dictionary<SpiderTopic, long> _TopicToSubscriptionId { get; set; }

        private Dictionary<long, long> _UnsubscribeRequestIdToSubscriptionId { get; set;  }

        public SubscriptionManager()
        {
            _SubscribeRequestIdToTopic = new Dictionary<long, SpiderTopic>();
            _SubscriptionIdToTopic = new Dictionary<long, SpiderTopic>();
            _TopicToSubscriptionId = new Dictionary<SpiderTopic, long>();

            _UnsubscribeRequestIdToSubscriptionId = new Dictionary<long, long>();
        }

        public void ReportSubscribe(long requestId, SpiderTopic topic)
        {
            if (_SubscribeRequestIdToTopic.ContainsKey(requestId) == true)
            {
                _SubscribeRequestIdToTopic[requestId] = topic;
            }
            else
            {
                _SubscribeRequestIdToTopic.Add(requestId, topic);
            }
        }

        public void ReportUnsubscribe(long requestId, long subscriptionId)
        {
            if (_UnsubscribeRequestIdToSubscriptionId.ContainsKey(requestId) == true)
            {
                _UnsubscribeRequestIdToSubscriptionId[requestId] = subscriptionId;
            }
            else
            {
                _UnsubscribeRequestIdToSubscriptionId.Add(requestId, subscriptionId);
            }
        }

        public void ReportUnsubscribed(long requestId)
        {
            long subscriptionId = 0;

            if (_UnsubscribeRequestIdToSubscriptionId.TryGetValue(requestId, out subscriptionId) == false)
                return;

            _UnsubscribeRequestIdToSubscriptionId.Remove(requestId);
            CancelSubscription(subscriptionId);
        }

        private void CancelSubscription(long subscriptionId)
        {
            SpiderTopic topic = null;
            if (_SubscriptionIdToTopic.TryGetValue(subscriptionId, out topic) == false)
                return;

            _SubscriptionIdToTopic.Remove(subscriptionId);
            _TopicToSubscriptionId.Remove(topic);
        }

        public void ReportSubscribed(long requestId, long subscriptionId)
        {
            SpiderTopic topic = GetTopicByRequestId(requestId);

            // RequestId 사전에서 삭제
            _SubscribeRequestIdToTopic.Remove(requestId);

            if (_SubscriptionIdToTopic.ContainsKey(subscriptionId) == true)
            {
                _SubscriptionIdToTopic[subscriptionId] = topic; 
            }
            else
            {
                _SubscriptionIdToTopic.Add(subscriptionId, topic);
            }

            if (_TopicToSubscriptionId.ContainsKey(topic) == true)
            {
                _TopicToSubscriptionId[topic] = subscriptionId;
            }
            else
            {
                _TopicToSubscriptionId.Add(topic, subscriptionId);
            }
        }

        private SpiderTopic GetTopicByRequestId(long requestId)
        {
            SpiderTopic topic = null;
            _SubscribeRequestIdToTopic.TryGetValue(requestId, out topic);

            return topic;
        }

        /// <summary>
        /// subscriptionId를 조회하여 SpiderTopic을 반환합니다.
        /// </summary>
        /// <param name="subscriptionId">Subscription Id</param>
        /// <returns>SpiderTopic</returns>
        public SpiderTopic GetTopicBySubscriptionId(long subscriptionId)
        {
            SpiderTopic topic = null;

            _SubscriptionIdToTopic.TryGetValue(subscriptionId, out topic);

            return topic;
        }

        /// <summary>
        /// 토픽을 조회하여 subscriptionId를 반환합니다.
        /// </summary>
        /// <param name="topic">토픽</param>
        /// <returns>찾을 수 없는 경우 -1을 반환</returns>
        public long GetSubscriptionId(SpiderTopic topic)
        {
            if (topic == null)
                throw new NullReferenceException("topic should not be null!");

            long subscriptionId = -1;

            _TopicToSubscriptionId.TryGetValue(topic, out subscriptionId);

            return subscriptionId;
        }

        /// <summary>
        /// 모든 데이터를 비웁니다.
        /// </summary>
        public void Clear()
        {
            _SubscribeRequestIdToTopic.Clear();
            _SubscriptionIdToTopic.Clear();
            _TopicToSubscriptionId.Clear();
        }
    }
}
