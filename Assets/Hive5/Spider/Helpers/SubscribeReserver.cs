using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class SubscribeReserver
    {
        private SpiderCallback _DoneCallback;
        private Dictionary<string, bool> _SubscriptionResults = new Dictionary<string, bool>();

        public SubscribeReserver (List<SpiderTopic> topics, SpiderCallback doneCallback)
	    {
            _DoneCallback = doneCallback;

            foreach (var topic in topics)
	        {
                _SubscriptionResults.Add(topic.TopicUri, false);
	        }
	    }

        public void Confirm(SpiderTopic topic)
        {
            Confirm(topic.TopicUri);
        }

        public void Confirm(string topicUri)
        {
            if (_SubscriptionResults.Count == null)
                return;

            _SubscriptionResults.Remove(topicUri);

            if (_SubscriptionResults.Count == 0)
            {
                Hive5Spider.Instance.IsConnected = true;

                if (_DoneCallback != null)
                {
                    _DoneCallback(true);
                } 
            }    
        }
    }
}
