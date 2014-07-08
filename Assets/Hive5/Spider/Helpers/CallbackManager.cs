using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Hive5
{
    public static class CallbackManager
    {
        public static Dictionary<long, SubscribeCallback> SubscribeRequestIdToCallback { get; private set; }

        public static Dictionary<long, SpiderCallback> UnsubscribeRequestIdToCallback { get; private set; }

        public static Dictionary<long, SendMessageCallback> PublishRequestIdToCallback { get; private set; }

        public static Dictionary<long, CallResultCallbackNode> CallRequestIdToCallbackNode { get; private set; }


        static CallbackManager()
        {
            SubscribeRequestIdToCallback = new Dictionary<long, SubscribeCallback>();
            UnsubscribeRequestIdToCallback = new Dictionary<long, SpiderCallback>();
            PublishRequestIdToCallback = new Dictionary<long, SendMessageCallback>();
            CallRequestIdToCallbackNode = new Dictionary<long, CallResultCallbackNode>();
        }
    }
}
