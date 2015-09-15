using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public class CallbackManager
    {
        public Dictionary<long, SubscribeCallback> SubscribeRequestIdToCallback { get; private set; }

        public Dictionary<long, SpiderCallback> UnsubscribeRequestIdToCallback { get; private set; }

        public Dictionary<long, SendMessageCallback> PublishRequestIdToCallback { get; private set; }

        public Dictionary<long, CallResultCallbackNode> CallRequestIdToCallbackNode { get; private set; }


        public CallbackManager()
        {
            SubscribeRequestIdToCallback = new Dictionary<long, SubscribeCallback>();
            UnsubscribeRequestIdToCallback = new Dictionary<long, SpiderCallback>();
            PublishRequestIdToCallback = new Dictionary<long, SendMessageCallback>();
            CallRequestIdToCallbackNode = new Dictionary<long, CallResultCallbackNode>();
        }
    }
}
